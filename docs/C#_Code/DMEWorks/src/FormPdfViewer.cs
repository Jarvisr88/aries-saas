using DevExpress.Xpf.Bars;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.PdfViewer;
using DMEWorks.Forms;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

[DesignerGenerated]
public class FormPdfViewer : DmeForm
{
    private IContainer components;
    private readonly PdfViewerControl m_viewer;

    public FormPdfViewer(byte[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException("data");
        }
        this.InitializeComponent();
        Dispatcher currentDispatcher = Dispatcher.CurrentDispatcher;
        currentDispatcher.UnhandledException -= new DispatcherUnhandledExceptionEventHandler(FormPdfViewer.Dispatcher_UnhandledException);
        currentDispatcher.UnhandledException += new DispatcherUnhandledExceptionEventHandler(FormPdfViewer.Dispatcher_UnhandledException);
        SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xe0, 0xe0, 0xe0));
        brush.Freeze();
        PdfCommandProvider provider = new PdfCommandProvider();
        RemoveAction item = new RemoveAction();
        item.ElementName = "bOpen";
        provider.Actions.Add(item);
        RemoveAction action2 = new RemoveAction();
        action2.ElementName = "bOpenFromWeb";
        provider.Actions.Add(action2);
        RemoveAction action3 = new RemoveAction();
        action3.ElementName = "bOpen";
        provider.RibbonActions.Add(action3);
        RemoveAction action4 = new RemoveAction();
        action4.ElementName = "bOpenFromWeb";
        provider.RibbonActions.Add(action4);
        this.m_viewer = new PdfViewerControl();
        this.m_viewer.BeginInit();
        this.m_viewer.DocumentSource = new MemoryStream(data, false);
        this.m_viewer.Background = brush;
        this.m_viewer.CommandBarStyle = CommandBarStyle.Bars;
        this.m_viewer.CommandProvider = provider;
        this.m_viewer.CursorMode = CursorModeType.SelectTool;
        this.m_viewer.ShowStartScreen = false;
        this.m_viewer.ManipulationBoundaryFeedback += new EventHandler<ManipulationBoundaryFeedbackEventArgs>(this.PdfViewerControl_ManipulationBoundaryFeedback);
        this.m_viewer.EndInit();
        this.WpfHost.Child = this.m_viewer;
    }

    public static void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = Utilities.ShowUnhadledException(e.Exception) != DialogResult.Abort;
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
        try
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
        }
        finally
        {
            base.Dispose(disposing);
        }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
        this.WpfHost = new ElementHost();
        base.SuspendLayout();
        this.WpfHost.Dock = DockStyle.Fill;
        this.WpfHost.Location = new Point(0, 0);
        this.WpfHost.Name = "WpfHost";
        this.WpfHost.Size = new Size(0x270, 0x1b9);
        this.WpfHost.TabIndex = 0;
        this.WpfHost.Child = null;
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x270, 0x1b9);
        base.Controls.Add(this.WpfHost);
        base.Name = "FormPdfViewer";
        this.Text = "Pdf Viewer";
        base.ResumeLayout(false);
    }

    public void PdfViewerControl_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
    {
        e.Handled = true;
    }

    [field: AccessedThroughProperty("WpfHost")]
    private ElementHost WpfHost { get; [MethodImpl(MethodImplOptions.Synchronized)]
    set; }
}

