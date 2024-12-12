// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.DatabaseUpdater
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

using Devart.Common;
using Devart.Data.MySql;
using DMEWorks.Core;
using DMEWorks.Core.Extensions;
using DMEWorks.Data;
using DMEWorks.Data.MySql;
using DMEWorks.Serials;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace DMEWorks.Database.Data
{
  internal abstract class DatabaseUpdater
  {
    private readonly DatabaseUpdater.StatusReporter logger = new DatabaseUpdater.StatusReporter();

    public MySqlConnectionInfo CnnInfo { get; }

    protected abstract IReadOnlyCollection<Update> Updates { get; }

    public DatabaseUpdater(MySqlConnectionInfo cnnInfo)
    {
      this.CnnInfo = cnnInfo ?? throw new ArgumentNullException(nameof (cnnInfo));
    }

    private IEnumerable<Update> GetUpdatesFor(string version)
    {
      if (!Scripts.IsUpdate(version))
        throw new Exception("Incorrect version format '" + version + "'");
      return this.Updates.Where<Update>((System.Func<Update, bool>) (u => Scripts.IsUpdate(u.Version) && 0 < UpdateComparer.CompareVersions(u.Version, version)));
    }

    private string GetVersionQuery()
    {
      return this.Updates.Single<Update>((System.Func<Update, bool>) (u => u.IsVersion())).Query;
    }

    public static DatabaseUpdater Create(MySqlConnectionInfo cnnInfo)
    {
      if (cnnInfo == null)
        throw new ArgumentNullException(nameof (cnnInfo));
      if (string.Equals(cnnInfo.Database, "dmeworks", StringComparison.OrdinalIgnoreCase))
        return (DatabaseUpdater) new DatabaseUpdater.Dmeworks(cnnInfo);
      return string.Equals(cnnInfo.Database, "repository", StringComparison.OrdinalIgnoreCase) ? (DatabaseUpdater) new DatabaseUpdater.Repository(cnnInfo) : (DatabaseUpdater) new DatabaseUpdater.Company(cnnInfo);
    }

    public event EventHandler<StatusEventArgs> StatusChanged
    {
      add => this.logger.StatusChanged += value;
      remove => this.logger.StatusChanged -= value;
    }

    private MySqlConnection CreateDatabaseConnection()
    {
      return new MySqlConnection(this.CnnInfo.GetDatabaseConnectionString());
    }

    private MySqlConnection CreateServerConnection()
    {
      return new MySqlConnection(this.CnnInfo.GetServerConnectionString());
    }

    private static MySqlCommand CreateCommand(MySqlConnection cnn)
    {
      MySqlCommand command = new MySqlCommand("", cnn);
      command.CommandTimeout = 600;
      return command;
    }

    private void ApplyUpdates(
      MySqlConnection connection,
      System.Func<Update, bool> filter,
      string operation)
    {
      using (this.logger.BeginOperation(operation))
      {
        foreach (Update update in (IEnumerable<Update>) this.Updates.Where<Update>(filter).OrderBy<Update, Update>((System.Func<Update, Update>) (u => u), (IComparer<Update>) UpdateComparer.@default))
        {
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(connection))
          {
            this.logger.LogStatus("Apply '" + update.Version + "'");
            Scripts.ExecuteScript(command, update.Query);
          }
        }
      }
    }

    private void InternalUpdateSchema(MySqlConnection cnn)
    {
      using (this.logger.BeginOperation("Update Schema"))
      {
        string version;
        using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
        {
          this.logger.LogStatus("Get Version");
          command.CommandText = this.GetVersionQuery();
          version = Convert.ToString(command.ExecuteScalar());
        }
        foreach (Update update in (IEnumerable<Update>) this.GetUpdatesFor(version).OrderBy<Update, Update>((System.Func<Update, Update>) (u => u), (IComparer<Update>) UpdateComparer.@default))
        {
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
          {
            this.logger.LogStatus("Apply '" + update.Version + "'");
            Scripts.ExecuteScript(command, update.Query);
          }
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
          {
            command.CommandText = "REPLACE INTO tbl_variables (Name, Value) VALUES ('Version', :Value);";
            MySqlParameterCollection parameters = command.Parameters;
            MySqlParameter mySqlParameter = new MySqlParameter("Value", MySqlType.VarChar, 50);
            mySqlParameter.Value = (object) update.Version;
            parameters.Add(mySqlParameter);
            command.ExecuteNonQuery();
          }
        }
      }
      this.ApplyUpdates(cnn, (System.Func<Update, bool>) (u => u.IsRoutine()), "Create Routines");
    }

    private void InternalCreateTables(MySqlConnection cnn)
    {
      this.ApplyUpdates(cnn, (System.Func<Update, bool>) (u => u.IsInitial()), "Create Tables");
    }

    private void InternalUpdateData(MySqlConnection cnn)
    {
      this.ApplyUpdates(cnn, (System.Func<Update, bool>) (u => u.IsExecute()), "Update Data");
    }

    private void InternalCreateDatabase()
    {
      using (this.logger.BeginOperation("Create Database"))
      {
        using (MySqlConnection serverConnection = this.CreateServerConnection())
        {
          serverConnection.Open();
          DatabaseUtilities.CreateDatabase(serverConnection, this.CnnInfo.Database);
        }
      }
    }

    private void InternalGrantPermissions()
    {
      using (MySqlConnection serverConnection = this.CreateServerConnection())
      {
        serverConnection.Open();
        DatabaseUtilities.GrantPermissions(serverConnection, this.CnnInfo.Database);
      }
    }

    public void BackupDatabase(string path)
    {
      using (this.logger.BeginOperation("Backup Database"))
      {
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          DatabaseUtilities.BackupDatabase(databaseConnection, path, (ScriptErrorEventHandler) null, (MySqlDumpProgressEventHandler) null);
        }
      }
    }

    public abstract void CreateDatabase(SerialData? serial);

    public abstract void UpdateDatabase(SerialData? serial, bool updateData);

    public abstract void LeaveIntactDatabase(SerialData? serial);

    public string GetSerialNumber()
    {
      try
      {
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(databaseConnection))
          {
            command.CommandText = "SELECT Value FROM tbl_variables WHERE Name = 'Serial'";
            using (MySqlDataReader mySqlDataReader = command.ExecuteReader())
            {
              if (mySqlDataReader.Read())
              {
                if (!(mySqlDataReader[0] is string empty))
                  empty = string.Empty;
                return empty;
              }
            }
          }
        }
      }
      catch
      {
      }
      return string.Empty;
    }

    public DatabaseState GetDatabaseState()
    {
      try
      {
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(databaseConnection))
          {
            command.CommandText = this.GetVersionQuery();
            using (MySqlDataReader mySqlDataReader = command.ExecuteReader())
            {
              if (!mySqlDataReader.Read())
                return DatabaseState.IncorrectStructure;
              if (!(mySqlDataReader[0] is string empty))
                empty = string.Empty;
              string version = empty;
              if (!Scripts.IsUpdate(version))
                return DatabaseState.IncorrectStructure;
              return this.GetUpdatesFor(version).Any<Update>() ? DatabaseState.OldVersion : DatabaseState.SameVersion;
            }
          }
        }
      }
      catch (MySqlException ex)
      {
        switch (ex.Code)
        {
          case 1044:
            throw;
          case 1049:
            return DatabaseState.DoesNotExist;
          case 1051:
            return DatabaseState.IncorrectStructure;
          case 1146:
            return DatabaseState.IncorrectStructure;
          default:
            throw;
        }
      }
    }

    private class Operation : IDisposable
    {
      private readonly Action disposeAction;

      public Operation(Action disposeAction)
      {
        this.disposeAction = disposeAction ?? throw new ArgumentNullException(nameof (disposeAction));
      }

      public void Dispose() => this.disposeAction();
    }

    private class StatusReporter
    {
      private readonly List<string> operationStack = new List<string>(16);

      public event EventHandler<StatusEventArgs> StatusChanged;

      private void DoStatusChanged(StatusEventArgs e)
      {
        EventHandler<StatusEventArgs> statusChanged = this.StatusChanged;
        if (statusChanged == null)
          return;
        statusChanged((object) this, e);
      }

      private void EndOperation()
      {
        this.LogStatus("Finished");
        if (0 >= this.operationStack.Count)
          return;
        this.operationStack.RemoveAt(this.operationStack.Count - 1);
      }

      public IDisposable BeginOperation(string operation)
      {
        this.operationStack.Add(operation);
        this.LogStatus("Started");
        return (IDisposable) new DatabaseUpdater.Operation(new Action(this.EndOperation));
      }

      public void LogStatus(string Status)
      {
        if (0 < this.operationStack.Count)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("[");
          for (int index = 0; index < this.operationStack.Count; ++index)
          {
            if (0 < index)
              stringBuilder.Append("/");
            stringBuilder.Append(this.operationStack[index]);
          }
          stringBuilder.Append("] ");
          stringBuilder.Append(Status);
          this.DoStatusChanged(new StatusEventArgs(stringBuilder.ToString()));
        }
        else
          this.DoStatusChanged(new StatusEventArgs(Status));
      }
    }

    internal class Company : DatabaseUpdater
    {
      protected override IReadOnlyCollection<Update> Updates { get; }

      public Company(MySqlConnectionInfo cnnInfo)
        : base(cnnInfo)
      {
        this.Updates = (IReadOnlyCollection<Update>) Scripts.CreateUpdateCollection("DMEWorks.Database.Scripts.Company.Archive.", "DMEWorks.Database.Scripts.Company.Current.");
      }

      private void InternalApplySerial(MySqlConnection cnn, SerialData? serial)
      {
        if (!serial.HasValue)
          return;
        using (this.logger.BeginOperation("Apply Serial"))
        {
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
          {
            command.CommandText = "REPLACE INTO tbl_variables (Name, Value) VALUES ('Serial', :Value);";
            MySqlParameterCollection parameters = command.Parameters;
            MySqlParameter mySqlParameter = new MySqlParameter("Value", MySqlType.VarChar, 50);
            mySqlParameter.Value = (object) serial.ToString();
            parameters.Add(mySqlParameter);
            command.ExecuteNonQuery();
          }
        }
      }

      private void InternalUpdatePayments(MySqlConnection cnn)
      {
        using (this.logger.BeginOperation("Update payments"))
        {
          int val1 = 0;
label_2:
          Tuple<int, string, Decimal?>[] array;
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
          {
            command.CommandText = "SELECT it.ID, it.Extra\r\n, CASE WHEN tt.Name = 'Payment' THEN it.Amount ELSE null END as Paid\r\nFROM tbl_invoice_transaction as it\r\n     LEFT JOIN tbl_invoice_transactiontype as tt ON it.transactionTypeID = tt.ID\r\nWHERE it.Extra != ''\r\n  AND it.Extra NOT LIKE '<values>%</values>'\r\n  AND it.Extra NOT LIKE '<values%/>'\r\n  AND it.ID > :Start\r\nORDER BY it.ID LIMIT 0, 500";
            MySqlParameterCollection parameters = command.Parameters;
            MySqlParameter mySqlParameter = new MySqlParameter("Start", MySqlType.Int);
            mySqlParameter.Value = (object) val1;
            parameters.Add(mySqlParameter);
            array = command.ExecuteList<Tuple<int, string, Decimal?>>((System.Func<IDataRecord, Tuple<int, string, Decimal?>>) (r => Tuple.Create<int, string, Decimal?>(r.GetInt32(0), r.GetString(1), NullableConvert.ToDecimal(r.GetValue(2))))).ToArray();
          }
          if (array.Length == 0)
            return;
          using (MySqlCommand command = DatabaseUpdater.CreateCommand(cnn))
          {
            command.CommandText = "UPDATE tbl_invoice_transaction SET Extra = :New WHERE ID = :ID AND Extra = :Old";
            command.Parameters.AddRange((Array) new MySqlParameter[3]
            {
              new MySqlParameter(":ID", MySqlType.Int),
              new MySqlParameter(":New", MySqlType.Text),
              new MySqlParameter(":Old", MySqlType.Text)
            });
            foreach (Tuple<int, string, Decimal?> tuple in array)
            {
              PaymentExtraData plain = PaymentExtraData.ParsePlain(tuple.Item2);
              if (!plain.Paid.HasValue)
                plain.Paid = tuple.Item3;
              command.Parameters[0].Value = (object) tuple.Item1;
              command.Parameters[1].Value = (object) plain.ToString();
              command.Parameters[2].Value = (object) tuple.Item2;
              command.ExecuteNonQuery();
              val1 = Math.Max(val1, tuple.Item1);
            }
            goto label_2;
          }
        }
      }

      public void CreateDatabase(SerialData? serial, bool addSamples)
      {
        this.InternalCreateDatabase();
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalCreateTables(databaseConnection);
          if (addSamples)
            this.ApplyUpdates(databaseConnection, (System.Func<Update, bool>) (u => u.IsSample()), "Add Samples");
          this.InternalUpdateSchema(databaseConnection);
          this.InternalApplySerial(databaseConnection, serial);
          this.InternalUpdateData(databaseConnection);
          this.InternalUpdatePayments(databaseConnection);
        }
      }

      public override void CreateDatabase(SerialData? serial) => this.CreateDatabase(serial, false);

      public override void UpdateDatabase(SerialData? serial, bool updateData)
      {
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalUpdateSchema(databaseConnection);
          this.InternalApplySerial(databaseConnection, serial);
          if (!updateData)
            return;
          this.InternalUpdateData(databaseConnection);
          this.InternalUpdatePayments(databaseConnection);
        }
      }

      public override void LeaveIntactDatabase(SerialData? serial)
      {
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalApplySerial(databaseConnection, serial);
        }
      }
    }

    internal class Dmeworks : DatabaseUpdater
    {
      protected override IReadOnlyCollection<Update> Updates { get; }

      public Dmeworks(MySqlConnectionInfo cnnInfo)
        : base(cnnInfo)
      {
        this.Updates = (IReadOnlyCollection<Update>) Scripts.CreateUpdateCollection("DMEWorks.Database.Scripts.Dmeworks.Archive.", "DMEWorks.Database.Scripts.Dmeworks.Current.");
      }

      public void CreateDatabase(SerialData? serial, bool addCompanies, bool addDoctors)
      {
        this.InternalCreateDatabase();
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalCreateTables(databaseConnection);
          if (addCompanies)
            this.ApplyUpdates(databaseConnection, (System.Func<Update, bool>) (u => u.IsCompany()), "Add Insurance Companies");
          if (addDoctors)
            this.ApplyUpdates(databaseConnection, (System.Func<Update, bool>) (u => u.IsDoctor()), "Add Doctors");
          this.InternalUpdateSchema(databaseConnection);
          this.InternalUpdateData(databaseConnection);
        }
      }

      public override void CreateDatabase(SerialData? serial)
      {
        this.CreateDatabase(serial, false, false);
      }

      public override void UpdateDatabase(SerialData? serial, bool updateData)
      {
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalUpdateSchema(databaseConnection);
          if (!updateData)
            return;
          this.InternalUpdateData(databaseConnection);
        }
      }

      public override void LeaveIntactDatabase(SerialData? serial)
      {
        this.InternalGrantPermissions();
      }
    }

    internal class Repository : DatabaseUpdater
    {
      protected override IReadOnlyCollection<Update> Updates { get; }

      public Repository(MySqlConnectionInfo cnnInfo)
        : base(cnnInfo)
      {
        this.Updates = (IReadOnlyCollection<Update>) Scripts.CreateUpdateCollection("DMEWorks.Database.Scripts.Repository.Archive.", "DMEWorks.Database.Scripts.Repository.Current.");
      }

      public override void CreateDatabase(SerialData? serial)
      {
        this.InternalCreateDatabase();
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalCreateTables(databaseConnection);
          this.InternalUpdateSchema(databaseConnection);
          this.InternalUpdateData(databaseConnection);
        }
      }

      public override void UpdateDatabase(SerialData? serial, bool updateData)
      {
        this.InternalGrantPermissions();
        using (MySqlConnection databaseConnection = this.CreateDatabaseConnection())
        {
          databaseConnection.Open();
          this.InternalUpdateSchema(databaseConnection);
          if (!updateData)
            return;
          this.InternalUpdateData(databaseConnection);
        }
      }

      public override void LeaveIntactDatabase(SerialData? serial)
      {
        this.InternalGrantPermissions();
      }
    }
  }
}
