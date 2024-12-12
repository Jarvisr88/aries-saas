using Devart.Common;
using Devart.Data.MySql;
using DMEWorks;
using DMEWorks.Controls;
using DMEWorks.Core;
using DMEWorks.Core.Extensions;
using DMEWorks.CrystalReports;
using DMEWorks.Data;
using DMEWorks.Forms;
using DMEWorks.Reports;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

public class ClassGlobalObjects
{
    private const string CrLf = "\r\n";
    private static FormMain FFormMain;
    private static string FDMEWorksFolder = "";
    private static FormDisconnectionAlert FFormDisconnectionAlert = null;
    private static readonly Regex regexCommandLine = new Regex("(?:\\\\\"|[^\" ]+|\"(?:\\\\\"|[^\"]|\"\")*\"|\"(?:\\\\\"|[^\"]|\"\")*\\z)+", RegexOptions.Singleline);
    private static string F_FolderApplicationData;
    private static string F_FolderLog;
    private static string F_FolderProcessedData;

    public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        if (DMEWorks.Forms.Utilities.ShowUnhadledException(e.Exception) == DialogResult.Abort)
        {
            Application.Exit();
        }
    }

    public static void CloseForm(Form Form, object Param)
    {
        if ((Form != null) && !Form.IsDisposed)
        {
            Form.Close();
        }
    }

    private static void ControlAddress_DefaultFindClick(object sender, EventArgs e)
    {
        ControlAddress address = sender as ControlAddress;
        if (address != null)
        {
            string text = address.txtZip.Text;
            string str2 = new string(text.Where<char>(((_Closure$__.$I43-0 == null) ? (_Closure$__.$I43-0 = new Func<char, bool>(_Closure$__.$I._Lambda$__43-0)) : _Closure$__.$I43-0)).ToArray<char>());
            if (str2.Length >= 5)
            {
                List<string> zipList = new List<string> {
                    str2.ToString()
                };
                if (5 < str2.Length)
                {
                    zipList.Add(str2.Insert(5, "-"));
                    zipList.Add(str2.Substring(0, 5));
                }
                Tuple<string, string> tuple = InternalFindZipCode(zipList);
                if (tuple != null)
                {
                    address.txtCity.Text = tuple.Item1;
                    address.txtState.Text = tuple.Item2;
                }
                else if (MessageBox.Show("System can'not find zip code specified.\r\nWould you like to add it yourself?", "Find Zip Code", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    FormParameters @params = new FormParameters("Zip", text);
                    ShowForm(FormFactories.FormZipCode(), @params);
                }
            }
        }
    }

    private static void ControlAddress_DefaultMapClick(object sender, MapProviderEventArgs e)
    {
        ControlAddress control = sender as ControlAddress;
        if (control != null)
        {
            try
            {
                Uri pointUri = e.Provider.GetPointUri(control.Address);
                pointUri ??= e.Provider.GetHomePage();
                if (pointUri != null)
                {
                    OpenUrl(pointUri.AbsoluteUri);
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                control.ShowException(ex);
                ProjectData.ClearProjectError();
            }
        }
    }

    public static void ConvertReportsDescriptionToNewFormat()
    {
        if (File.Exists(Cache.CrystalReports.CustomFileName))
        {
            Trace.Write("File containg custom reports descriptions (" + Cache.CrystalReports.CustomFileName + ") already exists");
        }
        else if (!File.Exists(Cache.CrystalReports.DefaultFileName))
        {
            Trace.Write("File containg default reports descriptions (" + Cache.CrystalReports.DefaultFileName + ") does not exist");
        }
        else
        {
            string path = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DME"), "DME Works"), "DMEWorks.Data.mdb");
            if (!File.Exists(path))
            {
                Trace.Write("File (" + path + ") does not exist");
                path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DMEWorks.Data.mdb");
                if (!File.Exists(path))
                {
                    Trace.Write("File (" + path + ") does not exist");
                    return;
                }
            }
            try
            {
                OleDbConnectionStringBuilder builder1 = new OleDbConnectionStringBuilder();
                builder1.Provider = "Microsoft.Jet.OLEDB.4.0";
                builder1.DataSource = path;
                List<Report> list = new List<Report>();
                using (OleDbConnection connection = new OleDbConnection(builder1.ToString()))
                {
                    using (OleDbCommand command = new OleDbCommand("SELECT\r\n  FileName\r\n, Name\r\n, Category\r\n, IsSystemReport\r\nFROM tbl_crystalreport", connection))
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Report report1 = new Report();
                                report1.Category = reader.GetString("Category");
                                report1.FileName = reader.GetString("FileName");
                                report1.IsSystem = reader.GetBoolean("IsSystemReport").GetValueOrDefault(false);
                                report1.Name = reader.GetString("Name");
                                Report item = report1;
                                list.Add(item);
                            }
                        }
                    }
                }
                Cache.CrystalReports.ReplaceWith(list.ToArray());
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                TraceHelper.TraceException(exception);
                if (exception is OutOfMemoryException)
                {
                    throw;
                }
                if (exception is StackOverflowException)
                {
                    throw;
                }
                MessageBox.Show(TraceHelper.FormatException(exception), "Converting report descriptions", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ProjectData.ClearProjectError();
            }
        }
    }

    public static string FindReport(string ReportFileName)
    {
        string str;
        string path = Path.Combine(CustomReportsFolder, ReportFileName + ".rpt");
        if (File.Exists(path))
        {
            str = path;
        }
        else
        {
            path = Path.Combine(DefaultReportsFolder, ReportFileName + ".rpt");
            str = !File.Exists(path) ? string.Empty : path;
        }
        return str;
    }

    public static void Globals_DatabaseChanged(object sender, Globals.DatabaseChangedEventArgs args)
    {
        _Closure$__8-0 e$__- = new _Closure$__8-0 {
            $VB$Local_array = args.TableNames.Select<string, string>(((_Closure$__.$I8-0 == null) ? (_Closure$__.$I8-0 = new Func<string, string>(_Closure$__.$I._Lambda$__8-0)) : _Closure$__.$I8-0)).ToArray<string>()
        };
        frmMain.Invoke(new VB$AnonymousDelegate_0(e$__-._Lambda$__1));
    }

    public static void Globals_Disconnected(object sender, EventArgs e)
    {
        frmMain.Invoke((_Closure$__.$I38-0 == null) ? (_Closure$__.$I38-0 = new VB$AnonymousDelegate_0(_Closure$__.$I._Lambda$__38-0)) : _Closure$__.$I38-0);
        Cache.Clear();
    }

    public static void Globals_Disconnecting(object sender, EventArgs e)
    {
        frmMain.Invoke((_Closure$__.$I37-0 == null) ? (_Closure$__.$I37-0 = new VB$AnonymousDelegate_0(_Closure$__.$I._Lambda$__37-0)) : _Closure$__.$I37-0);
    }

    private static Tuple<string, string> InternalFindZipCode(IEnumerable<string> zipList)
    {
        if (zipList == null)
        {
            throw new ArgumentNullException("zipList");
        }
        using (MySqlConnection connection = new MySqlConnection(ConnectionString_MySql))
        {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand("", connection))
            {
                IEnumerator<string> enumerator;
                command.CommandText = "SELECT Zip, State, City FROM tbl_zipcode WHERE Zip = :Zip";
                MySqlParameter parameter = new MySqlParameter("Zip", MySqlType.VarChar, 10);
                command.Parameters.Add(parameter);
                try
                {
                    enumerator = zipList.GetEnumerator();
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        string current = enumerator.Current;
                        parameter.Value = current;
                        using (MySqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                return Tuple.Create<string, string>(NullableConvert.ToString(reader["City"]), NullableConvert.ToString(reader["State"]));
                            }
                        }
                    }
                }
                finally
                {
                    if (enumerator != null)
                    {
                        enumerator.Dispose();
                    }
                }
            }
        }
        return null;
    }

    public static void InternalNotifyDatabaseChanged(string[] TableNames, bool local)
    {
        IEnumerator enumerator;
        if (local)
        {
            Cache.HandleDatabaseChanged(TableNames);
        }
        try
        {
            enumerator = Application.OpenForms.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Form current = (Form) enumerator.Current;
                if (!current.IsDisposed)
                {
                    if (local)
                    {
                        HandleDatabaseChangedAttribute.InvokeHandles(current, TableNames);
                    }
                    FormMaintainBase base2 = current as FormMaintainBase;
                    if (base2 != null)
                    {
                        base2.HandleDatabaseChanged(TableNames, local);
                    }
                }
            }
        }
        finally
        {
            if (enumerator is IDisposable)
            {
                (enumerator as IDisposable).Dispose();
            }
        }
    }

    public static void LoadCombobox(Combobox combobox, string sql, string DisplayMember = "Name", string ValueMember = "ID")
    {
        DataTable dataTable = new DataTable("Table");
        DataRow row = dataTable.NewRow();
        dataTable.Rows.Add(row);
        row.AcceptChanges();
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, ConnectionString_MySql))
        {
            adapter.AcceptChangesDuringFill = true;
            adapter.MissingSchemaAction = MissingSchemaAction.Add;
            adapter.Fill(dataTable);
        }
        Functions.AssignDatasource(combobox, dataTable, DisplayMember, ValueMember);
    }

    public static void LoadCombobox(ComboBox combobox, string sql, string DisplayMember = "Name", string ValueMember = "ID")
    {
        using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, ConnectionString_MySql))
        {
            DataTable dataTable = new DataTable("Table");
            DataRow row = dataTable.NewRow();
            dataTable.Rows.Add(row);
            row.AcceptChanges();
            adapter.AcceptChangesDuringFill = true;
            adapter.Fill(dataTable);
            Functions.AssignDatasource(combobox, dataTable, DisplayMember, ValueMember);
        }
    }

    [STAThread]
    public static void Main(string[] Args)
    {
        F_FolderApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!Directory.Exists(F_FolderApplicationData))
        {
            Directory.CreateDirectory(F_FolderApplicationData);
        }
        F_FolderApplicationData = Path.Combine(F_FolderApplicationData, "DME Works");
        if (!Directory.Exists(F_FolderApplicationData))
        {
            Directory.CreateDirectory(F_FolderApplicationData);
        }
        F_FolderProcessedData = Path.Combine(F_FolderApplicationData, "Processed");
        if (!Directory.Exists(F_FolderProcessedData))
        {
            Directory.CreateDirectory(F_FolderProcessedData);
        }
        F_FolderLog = Path.Combine(F_FolderApplicationData, "Log");
        if (!Directory.Exists(F_FolderLog))
        {
            Directory.CreateDirectory(F_FolderLog);
        }
        string str = $"log[{DateTime.Now:yyyy-MM-dd HH-mm-ss}].log";
        System.Diagnostics.TextWriterTraceListener listener = new System.Diagnostics.TextWriterTraceListener(Path.Combine(F_FolderLog, str));
        Trace.Listeners.Add(listener);
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        Trace.WriteLine("Version: " + executingAssembly.GetName().Version.ToString());
        ConvertReportsDescriptionToNewFormat();
        ControlAddress.DefaultFindClick += new EventHandler(ClassGlobalObjects.ControlAddress_DefaultFindClick);
        ControlAddress.DefaultMapClick += new EventHandler<MapProviderEventArgs>(ClassGlobalObjects.ControlAddress_DefaultMapClick);
        Globals.Disconnecting += new EventHandler(ClassGlobalObjects.Globals_Disconnecting);
        Globals.Disconnected += new EventHandler(ClassGlobalObjects.Globals_Disconnected);
        Globals.DatabaseChanged += new EventHandler<Globals.DatabaseChangedEventArgs>(ClassGlobalObjects.Globals_DatabaseChanged);
        Globals.StartBackgroundWorker();
        try
        {
            frmMain.Show();
            if (FormTips.ShowAtStartup)
            {
                using (FormTips tips = new FormTips())
                {
                    tips.Owner = frmMain;
                    tips.ShowInTaskbar = false;
                    tips.ShowDialog();
                }
            }
            Application.ThreadException += new ThreadExceptionEventHandler(ClassGlobalObjects.Application_ThreadException);
            Application.Run(frmMain);
        }
        catch (Exception exception1)
        {
            Exception ex = exception1;
            ProjectData.SetProjectError(ex);
            Trace.Write(ex, "Unhandled Exception");
            ProjectData.ClearProjectError();
        }
    }

    private static void Monitor_TraceEvent(object sender, MonitorEventArgs e)
    {
        if ((e.EventType == MonitorEventType.Execute) && (e.TracePoint == MonitorTracePoint.BeforeEvent))
        {
            using (StringWriter writer = new StringWriter())
            {
                StringReader reader2;
                writer.NewLine = Environment.NewLine;
                StringReader reader = new StringReader(e.Description);
                goto TR_0015;
            TR_000A:
                TraceHelper.TraceInfo(writer.ToString());
                return;
            TR_000E:
                try
                {
                    while (true)
                    {
                        string str2 = reader2.ReadLine();
                        if (str2 != null)
                        {
                            writer.WriteLine(str2);
                        }
                        else
                        {
                            goto TR_000A;
                        }
                        break;
                    }
                }
                finally
                {
                    if (reader2 != null)
                    {
                        reader2.Dispose();
                    }
                }
                goto TR_000E;
            TR_0011:
                if (string.IsNullOrWhiteSpace(e.ExtraInfo))
                {
                    goto TR_000A;
                }
                else
                {
                    reader2 = new StringReader(e.Description);
                }
                goto TR_000E;
            TR_0015:
                try
                {
                    while (true)
                    {
                        string str = reader.ReadLine();
                        if (str != null)
                        {
                            writer.WriteLine(str);
                        }
                        else
                        {
                            goto TR_0011;
                        }
                        break;
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                }
                goto TR_0015;
            }
        }
    }

    public static void NotifyDatabaseChanged(params string[] TableNames)
    {
        InternalNotifyDatabaseChanged(TableNames, true);
        Globals.NotifyTablesChanged(TableNames);
    }

    public static void OnPaymentAdded()
    {
        string str = null;
        if ((RegistrySettings.GetUserString("OnPaymentAdded", ref str) || RegistrySettings.GetMachineString("OnPaymentAdded", ref str)) && (str != null))
        {
            str = str.Trim();
            Match match = regexCommandLine.Match(str);
            if (match.Success)
            {
                using (Process process = new Process())
                {
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.FileName = match.Value;
                    match = match.NextMatch();
                    if (match.Success)
                    {
                        process.StartInfo.Arguments = str.Substring(match.Index);
                    }
                    process.Start();
                }
            }
        }
    }

    public static void OpenUrl(string url)
    {
        using (Process process = new Process())
        {
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = url;
            process.StartInfo.Verb = "open";
            process.Start();
        }
    }

    internal static void ReloadCompany()
    {
        Globals.ReloadCompany();
    }

    public static void ShowDisconnectionAlert()
    {
        if ((FFormDisconnectionAlert == null) || FFormDisconnectionAlert.IsDisposed)
        {
            FormDisconnectionAlert alert1 = new FormDisconnectionAlert();
            alert1.MdiParent = frmMain;
            FFormDisconnectionAlert = alert1;
        }
        if (FFormDisconnectionAlert.Visible)
        {
            FFormDisconnectionAlert.BringToFront();
        }
        else
        {
            FFormDisconnectionAlert.Show();
        }
    }

    public static void ShowFileReport(string ReportFileName, ReportParameters Params, bool Modal)
    {
        frmMain.ShowFileReport(ReportFileName, Params, Modal);
    }

    public static void ShowFileReport(string ReportFileName, string DatasetFileName, ReportParameters Params, bool Modal)
    {
        frmMain.ShowFileReport(ReportFileName, DatasetFileName, Params, Modal);
    }

    public static void ShowForm(FormFactory Factory)
    {
        frmMain.ShowForm(Factory, null, false);
    }

    public static void ShowForm(FormFactory Factory, FormParameters Params)
    {
        frmMain.ShowForm(Factory, Params, false);
    }

    public static void ShowForm(FormFactory Factory, FormParameters Params, bool Modal)
    {
        frmMain.ShowForm(Factory, Params, Modal);
    }

    public static void ShowReport(string ReportFileName)
    {
        frmMain.ShowReport(ReportFileName, null, false);
    }

    public static void ShowReport(string ReportFileName, ReportParameters Params)
    {
        frmMain.ShowReport(ReportFileName, Params, false);
    }

    public static void ShowReport(string ReportFileName, ReportParameters Params, bool Modal)
    {
        frmMain.ShowReport(ReportFileName, Params, Modal);
    }

    internal static FormMain frmMain
    {
        get
        {
            FFormMain ??= new FormMain();
            return FFormMain;
        }
    }

    public static string DMEWorksFolder
    {
        get
        {
            if (FDMEWorksFolder == "")
            {
                FDMEWorksFolder = Assembly.GetExecutingAssembly().EscapedCodeBase;
                FDMEWorksFolder = Path.GetDirectoryName(DMEWorks.Forms.Utilities.GetLocalPath(FDMEWorksFolder));
            }
            return FDMEWorksFolder;
        }
    }

    public static string DefaultReportsFolder =>
        Path.Combine(DMEWorksFolder, "Reports");

    public static string CustomReportsFolder =>
        Path.Combine(DMEWorksFolder, "Custom");

    public static string ConnectionString_MySql =>
        Globals.ConnectionString;

    internal static int? DefaultPOSTypeID
    {
        get
        {
            int? locationPOSTypeID = Globals.LocationPOSTypeID;
            if (locationPOSTypeID == null)
            {
                locationPOSTypeID = Globals.CompanyPOSTypeID;
                if (locationPOSTypeID == null)
                {
                    locationPOSTypeID = 12;
                }
            }
            return locationPOSTypeID;
        }
    }

    internal static int? DefaultWarehouseID
    {
        get
        {
            int? locationWarehouseID = Globals.LocationWarehouseID;
            if (locationWarehouseID == null)
            {
                locationWarehouseID = Globals.CompanyWarehouseID;
            }
            return locationWarehouseID;
        }
    }

    internal static int? DefaultTaxRateID
    {
        get
        {
            int? locationTaxRateID = Globals.LocationTaxRateID;
            if (locationTaxRateID == null)
            {
                locationTaxRateID = Globals.CompanyTaxRateID;
            }
            return locationTaxRateID;
        }
    }

    public static string FolderApplicationData =>
        F_FolderApplicationData;

    public static string FolderLog =>
        F_FolderLog;

    public static string FolderProcessedData =>
        F_FolderProcessedData;

    [Serializable, CompilerGenerated]
    internal sealed class _Closure$__
    {
        public static readonly ClassGlobalObjects._Closure$__ $I = new ClassGlobalObjects._Closure$__();
        public static Func<string, string> $I8-0;
        public static VB$AnonymousDelegate_0 $I37-0;
        public static VB$AnonymousDelegate_0 $I38-0;
        public static Func<char, bool> $I43-0;

        internal void _Lambda$__37-0()
        {
            ClassGlobalObjects.ShowDisconnectionAlert();
        }

        internal void _Lambda$__38-0()
        {
            ClassGlobalObjects.frmMain.ForEachForm(new FormMain.FormRepeater(ClassGlobalObjects.CloseForm), null);
        }

        internal bool _Lambda$__43-0(char c) => 
            char.IsDigit(c);

        internal string _Lambda$__8-0(string s) => 
            TableName.Normalize(s);
    }

    [CompilerGenerated]
    internal sealed class _Closure$__8-0
    {
        public string[] $VB$Local_array;

        internal void _Lambda$__1()
        {
            ClassGlobalObjects.InternalNotifyDatabaseChanged(this.$VB$Local_array, false);
        }
    }
}

