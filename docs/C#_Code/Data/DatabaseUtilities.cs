// Decompiled with JetBrains decompiler
// Type: DMEWorks.Database.Data.DatabaseUtilities
// Assembly: DMEWorks.Database, Version=2020.10.2.207, Culture=neutral, PublicKeyToken=null
// MVID: 05D8068A-6DAA-4D2C-9804-9ED26FFEA88C
// Assembly location: C:\Program Files (x86)\DME\Database Manager\DMEWorks.Database.exe

using Devart.Common;
using Devart.Data.MySql;
using DMEWorks.Data.MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace DMEWorks.Database.Data
{
  internal static class DatabaseUtilities
  {
    private static void InitializeFullBackup(MySqlDump dump)
    {
      if (dump == null)
        throw new ArgumentNullException(nameof (dump));
      dump.CommitBatchSize = 16384;
      dump.DisableKeys = true;
      dump.GenerateHeader = false;
      dump.IncludeDrop = false;
      dump.IncludeLock = true;
      dump.IncludeUse = false;
      dump.Mode = DumpMode.All;
      dump.ObjectTypes = MySqlDumpObjects.Tables;
      dump.QuoteIdentifier = true;
      dump.UseExtSyntax = true;
    }

    public static void BackupDatabase(
      MySqlConnection src,
      TextWriter dst,
      ScriptErrorEventHandler onError,
      MySqlDumpProgressEventHandler onProgress)
    {
      using (MySqlDump dump = new MySqlDump(src))
      {
        DatabaseUtilities.InitializeFullBackup(dump);
        dump.Error += onError;
        dump.Progress += onProgress;
        dump.Backup(dst);
      }
    }

    public static void BackupDatabase(
      MySqlConnection src,
      string dstFilename,
      ScriptErrorEventHandler onError,
      MySqlDumpProgressEventHandler onProgress)
    {
      using (StreamWriter dst = new StreamWriter(dstFilename, false, Encoding.UTF8))
      {
        DatabaseUtilities.BackupDatabase(src, (TextWriter) dst, onError, onProgress);
        dst.Flush();
      }
    }

    public static void BackupTable(
      MySqlConnection src,
      string tableName,
      TextWriter dst,
      ScriptErrorEventHandler onError,
      MySqlDumpProgressEventHandler onProgress)
    {
      string query = "SELECT * FROM " + MySqlUtilities.QuoteIdentifier(tableName);
      using (MySqlDump dump = new MySqlDump(src))
      {
        DatabaseUtilities.InitializeFullBackup(dump);
        dump.Error += onError;
        dump.Progress += onProgress;
        dump.BackupQuery(query, dst);
      }
    }

    public static void BackupTable(
      MySqlConnection src,
      string tableName,
      string dstFilename,
      ScriptErrorEventHandler onError,
      MySqlDumpProgressEventHandler onProgress)
    {
      using (StreamWriter dst = new StreamWriter(dstFilename, false, Encoding.UTF8))
      {
        DatabaseUtilities.BackupTable(src, tableName, (TextWriter) dst, onError, onProgress);
        dst.Flush();
      }
    }

    public static void RestoreData(
      Stream src,
      MySqlConnection dst,
      ScriptErrorEventHandler onError,
      ScriptProgressEventHandler onProgress)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand()
      {
        Connection = dst
      })
      {
        mySqlCommand.CommandText = "/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40103 SET TIME_ZONE='+00:00' */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;";
        mySqlCommand.ExecuteNonQuery();
      }
      DatabaseUtilities.SkipUseStatementScript useStatementScript1 = new DatabaseUtilities.SkipUseStatementScript();
      useStatementScript1.Connection = dst;
      using (DatabaseUtilities.SkipUseStatementScript useStatementScript2 = useStatementScript1)
      {
        useStatementScript2.Error += onError;
        useStatementScript2.Progress += onProgress;
        useStatementScript2.Open(src);
        useStatementScript2.Execute();
      }
      using (MySqlCommand mySqlCommand = new MySqlCommand()
      {
        Connection = dst
      })
      {
        mySqlCommand.CommandText = "/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = "/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;";
        mySqlCommand.ExecuteNonQuery();
      }
    }

    public static void RestoreData(
      string srcFilename,
      MySqlConnection dst,
      ScriptErrorEventHandler onError,
      ScriptProgressEventHandler onProgress)
    {
      using (FileStream src = new FileStream(srcFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
        DatabaseUtilities.RestoreData((Stream) src, dst, onError, onProgress);
    }

    public static void CreateDatabase(MySqlConnection server, string databaseName)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand()
      {
        Connection = server
      })
      {
        mySqlCommand.CommandText = "USE `mysql`;";
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = string.Format("DROP DATABASE IF EXISTS `{0}`;", (object) databaseName);
        mySqlCommand.ExecuteNonQuery();
        mySqlCommand.CommandText = string.Format("CREATE DATABASE `{0}` CHARACTER SET = latin1 COLLATE = latin1_general_ci;", (object) databaseName);
        mySqlCommand.ExecuteNonQuery();
      }
    }

    public static void GrantPermissions(MySqlConnection server, string databaseName)
    {
      bool flag = false;
      using (MySqlCommand mySqlCommand = new MySqlCommand()
      {
        Connection = server
      })
      {
        mySqlCommand.CommandText = "SELECT /*!80000 1 + */ 0 AS Is80;\r\n";
        flag = Convert.ToInt32(mySqlCommand.ExecuteScalar()) != 0;
      }
      string str1 = MySqlUtilities.QuoteIdentifier(databaseName);
      string str2 = MySqlUtilities.QuoteIdentifier("DMEUser");
      string str3 = MySqlUtilities.QuoteString("DMEPassword");
      using (MySqlCommand mySqlCommand = new MySqlCommand()
      {
        Connection = server
      })
      {
        mySqlCommand.CommandText = string.Format(flag ? "CREATE USER IF NOT EXISTS {1}@localhost IDENTIFIED BY {2};\r\nCREATE USER IF NOT EXISTS {1}@'%'       IDENTIFIED BY {2};\r\nGRANT SHOW DATABASES ON *.*\r\nTO {1}@localhost,\r\n   {1}@'%';\r\nGRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE, CREATE TEMPORARY TABLES ON {0}.*\r\nTO {1}@localhost,\r\n   {1}@'%';\r\n" : "GRANT SHOW DATABASES ON *.*\r\nTO {1}@localhost IDENTIFIED BY {2},   {1}@'%'       IDENTIFIED BY {2};\r\nGRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE, CREATE TEMPORARY TABLES ON {0}.*\r\nTO {1}@localhost IDENTIFIED BY {2},\r\n   {1}@'%'       IDENTIFIED BY {2};\r\n", (object) str1, (object) str2, (object) str3);
        mySqlCommand.ExecuteNonQuery();
      }
    }

    public static string[] GetDatabases(
      this MySqlOdbcDsnInfo dsnInfo,
      string username,
      string password)
    {
      if (dsnInfo == null)
        throw new ArgumentNullException(nameof (dsnInfo));
      List<string> stringList = new List<string>(16);
      using (MySqlConnection connection = new MySqlConnection(dsnInfo.Server.GetConnectionString(username, password)))
      {
        connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand("SHOW DATABASES", connection))
        {
          using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.SingleResult))
          {
            while (mySqlDataReader.Read())
            {
              if (mySqlDataReader.GetValue(0) is string database && !MySqlServerInfo.IsSystemDatabase(database))
                stringList.Add(database);
            }
          }
        }
        return stringList.ToArray();
      }
    }

    private class SkipUseStatementScript : MySqlScript
    {
      protected override bool CanExecuteStatement(SqlStatement sqlStatement)
      {
        return ((MySqlStatement) sqlStatement).MySqlStatementType != MySqlStatementType.Use && base.CanExecuteStatement(sqlStatement);
      }
    }
  }
}
