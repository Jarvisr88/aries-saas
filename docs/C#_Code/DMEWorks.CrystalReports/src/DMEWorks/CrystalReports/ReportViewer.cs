namespace DMEWorks.CrystalReports
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Shared;
    using CrystalDecisions.Windows.Forms;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class ReportViewer : Form
    {
        private IContainer components;
        private CrystalReportViewer viewer;

        public ReportViewer()
        {
            this.InitializeComponent();
        }

        private static void ApplyParameters(ReportDocument report, ReportParameters @params)
        {
            if (@params != null)
            {
                foreach (ParameterField field in report.ParameterFields)
                {
                    object obj2;
                    if (field.ReportName != "")
                    {
                        continue;
                    }
                    field.CurrentValues.Clear();
                    string key = EscapeParamName(field.Name);
                    if (@params.TryGetValue(key, out obj2))
                    {
                        if (!(obj2 is ICollection))
                        {
                            if (obj2 is DBNull)
                            {
                                if (!field.EnableNullValue)
                                {
                                    continue;
                                }
                                field.CurrentValues.Add((ParameterValue) CreateValue());
                                continue;
                            }
                            if (obj2 != null)
                            {
                                field.CurrentValues.Add((ParameterValue) CreateValue(obj2));
                                continue;
                            }
                            if (!field.EnableNullValue)
                            {
                                continue;
                            }
                            field.CurrentValues.Add((ParameterValue) CreateValue());
                            continue;
                        }
                        foreach (object obj3 in (ICollection) obj2)
                        {
                            field.CurrentValues.Add((ParameterValue) CreateValue(obj3));
                        }
                    }
                }
            }
        }

        private static ParameterDiscreteValue CreateValue() => 
            new ParameterDiscreteValue();

        private static ParameterDiscreteValue CreateValue(object value)
        {
            ParameterDiscreteValue value1 = new ParameterDiscreteValue();
            value1.Value = value;
            return value1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private static string EscapeParamName(string paramname)
        {
            if (paramname == null)
            {
                return "{?}";
            }
            StringBuilder builder = new StringBuilder();
            if (!paramname.StartsWith("{?"))
            {
                builder.Append("{?");
            }
            builder.Append(paramname);
            if (!paramname.EndsWith("}"))
            {
                builder.Append("}");
            }
            return builder.ToString();
        }

        public static void FixReport(string srcFileName, DataSource dataSource, string dstFileName)
        {
            if (!File.Exists(srcFileName))
            {
                throw new FileNotFoundException("File does not exist", srcFileName);
            }
            if (dataSource == null)
            {
                throw new ArgumentNullException("DataSource");
            }
            ReportDocument report = new ReportDocument();
            report.Load(srcFileName, OpenReportMethod.OpenReportByTempCopy);
            Utilities.FixAliases(report);
            dataSource.ApplyLogonInfo(report);
            report.SaveAs(dstFileName, false);
        }

        private void InitializeComponent()
        {
            this.viewer = new CrystalReportViewer();
            base.SuspendLayout();
            this.viewer.ActiveViewIndex = -1;
            this.viewer.BorderStyle = BorderStyle.FixedSingle;
            this.viewer.Cursor = Cursors.Default;
            this.viewer.Dock = DockStyle.Fill;
            this.viewer.Location = new Point(0, 0);
            this.viewer.Name = "viewer";
            this.viewer.ShowCloseButton = false;
            this.viewer.Size = new Size(0x278, 0x1c5);
            this.viewer.TabIndex = 0;
            this.viewer.ToolPanelView = ToolPanelViewType.None;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x1c5);
            base.Controls.Add(this.viewer);
            base.Name = "ReportViewer";
            this.Text = "Report Viewer";
            base.ResumeLayout(false);
        }

        private static ReportDocument InitializeReport(string reportFileName, DataSource dataSource, ReportParameters @params)
        {
            if (!File.Exists(reportFileName))
            {
                throw new FileNotFoundException("File does not exist", reportFileName);
            }
            if (dataSource == null)
            {
                throw new ArgumentNullException("DataSource");
            }
            ReportDocument report = new ReportDocument();
            report.Load(reportFileName, OpenReportMethod.OpenReportByTempCopy);
            dataSource.ApplyLogonInfo(report);
            ApplyParameters(report, @params);
            return report;
        }

        public void LoadReport(string reportFileName, DataSource dataSource, ReportParameters @params)
        {
            this.viewer.ReportSource = InitializeReport(reportFileName, dataSource, @params);
            this.viewer.Zoom(100);
        }

        public static void SavePdf(string reportFileName, DataSource dataSource, ReportParameters @params, string fileName)
        {
            ExportOptions options = new ExportOptions {
                ExportDestinationType = ExportDestinationType.DiskFile
            };
            DiskFileDestinationOptions options1 = new DiskFileDestinationOptions();
            options1.DiskFileName = fileName;
            options.ExportDestinationOptions = options1;
            options.ExportFormatType = ExportFormatType.PortableDocFormat;
            options.ExportFormatOptions = null;
            InitializeReport(reportFileName, dataSource, @params).Export(options);
        }

        public static void SaveRpt(string reportFileName, DataSource dataSource, ReportParameters @params, string fileName)
        {
            ExportOptions options = new ExportOptions {
                ExportDestinationType = ExportDestinationType.DiskFile
            };
            DiskFileDestinationOptions options1 = new DiskFileDestinationOptions();
            options1.DiskFileName = fileName;
            options.ExportDestinationOptions = options1;
            options.ExportFormatType = ExportFormatType.CrystalReport;
            options.ExportFormatOptions = null;
            InitializeReport(reportFileName, dataSource, @params).Export(options);
        }

        public static void SaveTxt(string reportFileName, DataSource dataSource, ReportParameters @params, string fileName)
        {
            ExportOptions options = new ExportOptions {
                ExportDestinationType = ExportDestinationType.DiskFile
            };
            DiskFileDestinationOptions options1 = new DiskFileDestinationOptions();
            options1.DiskFileName = fileName;
            options.ExportDestinationOptions = options1;
            options.ExportFormatType = ExportFormatType.Text;
            TextFormatOptions options2 = new TextFormatOptions();
            options2.CharactersPerInch = 0x10;
            options2.LinesPerPage = 80;
            options.ExportFormatOptions = options2;
            InitializeReport(reportFileName, dataSource, @params).Export(options);
        }
    }
}

