namespace DMEWorks.Forms
{
    using DMEWorks.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public static class Utilities
    {
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (ShowUnhadledException(e.Exception) == DialogResult.Abort)
            {
                Application.Exit();
            }
        }

        public static void ClearErrors(Control parent, ErrorProvider provider)
        {
            for (int i = 0; i < parent.Controls.Count; i++)
            {
                Control control = parent.Controls[i];
                ClearErrors(control, provider);
                provider.SetError(control, "");
            }
        }

        public static string[] EnumerateErrors(Control parent, ErrorProvider provider)
        {
            List<string> list = new List<string>(0x40);
            IntEnumerateErrors(parent, provider, list);
            return list.ToArray();
        }

        public static int EnumerateErrors(Control parent, ErrorProvider provider, StringBuilder builder)
        {
            int num = 0;
            for (int i = 0; i < parent.Controls.Count; i++)
            {
                Control control = parent.Controls[i];
                num += EnumerateErrors(control, provider, builder);
                string error = provider.GetError(control);
                if (!string.IsNullOrEmpty(error))
                {
                    num++;
                    if (builder != null)
                    {
                        if (0 < builder.Length)
                        {
                            builder.Append(Environment.NewLine);
                        }
                        builder.Append(error);
                    }
                }
            }
            return num;
        }

        public static string GetLocalPath(string fileName)
        {
            Uri uri = new Uri(fileName);
            return (uri.LocalPath + uri.Fragment);
        }

        private static void IntEnumerateErrors(Control parent, ErrorProvider provider, IList<string> list)
        {
            for (int i = 0; i < parent.Controls.Count; i++)
            {
                Control control = parent.Controls[i];
                IntEnumerateErrors(control, provider, list);
                string error = provider.GetError(control);
                if (!string.IsNullOrEmpty(error))
                {
                    list.Add(error);
                }
            }
        }

        private static void InternalTraceAndShowException(Form owner, Exception exception, string title)
        {
            string message;
            string str3;
            string str = (owner != null) ? owner.Text : string.Empty;
            if (exception is UserNotifyException)
            {
                TraceHelper.TraceInfo(exception.Message);
                if (exception.InnerException != null)
                {
                    TraceHelper.TraceException(exception.InnerException);
                }
                message = exception.Message;
                str3 = title;
            }
            else
            {
                TraceHelper.TraceException(exception);
                if (exception is OutOfMemoryException)
                {
                    throw exception;
                }
                if (exception is StackOverflowException)
                {
                    throw exception;
                }
                message = TraceHelper.FormatException(exception);
                str3 = string.IsNullOrEmpty(title) ? "Unhandled exception" : title;
            }
            string caption = !string.IsNullOrEmpty(str) ? (!string.IsNullOrEmpty(str3) ? (str + " : " + str3) : str) : str3;
            MessageBox.Show(owner, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        public static void SafeInvoke(this Form form, Action action)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            try
            {
                action();
            }
            catch (Exception exception)
            {
                form.ShowException(exception);
            }
        }

        public static void SafeInvoke(this UserControl control, Action action)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            try
            {
                action();
            }
            catch (Exception exception)
            {
                control.ShowException(exception);
            }
        }

        public static void ShowException(this Form form, Exception exception)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            InternalTraceAndShowException(form, exception, string.Empty);
        }

        public static void ShowException(this UserControl control, Exception exception)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            InternalTraceAndShowException(control.ParentForm, exception, string.Empty);
        }

        public static void ShowException(this Form form, Exception exception, string title)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            InternalTraceAndShowException(form, exception, title);
        }

        public static void ShowException(this UserControl control, Exception exception, string title)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            InternalTraceAndShowException(control.ParentForm, exception, title);
        }

        public static DialogResult ShowUnhadledException(Exception exception)
        {
            TraceHelper.TraceException(exception);
            new FileIOPermission(PermissionState.Unrestricted).Assert();
            try
            {
                using (StringWriter writer = new StringWriter())
                {
                    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    int index = 0;
                    while (true)
                    {
                        if (index >= assemblies.Length)
                        {
                            Trace.WriteLine(writer.ToString());
                            Trace.Flush();
                            break;
                        }
                        AssemblyName name = assemblies[index].GetName();
                        string fileVersion = "NotAvailable";
                        try
                        {
                            if (!string.IsNullOrEmpty(name.EscapedCodeBase) && (string.Compare(new Uri(name.EscapedCodeBase).Scheme, "file", true) == 0))
                            {
                                fileVersion = FileVersionInfo.GetVersionInfo(GetLocalPath(name.EscapedCodeBase)).FileVersion;
                            }
                        }
                        catch (FileNotFoundException)
                        {
                        }
                        writer.WriteLine("{0}", name.Name);
                        writer.WriteLine("    Assembly Version: {0}", name.Version);
                        writer.WriteLine("    Win32 Version: {0}", fileVersion);
                        writer.WriteLine("    CodeBase: {0}", name.EscapedCodeBase);
                        writer.WriteLine("----------------------------------------");
                        index++;
                    }
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            using (ThreadExceptionDialog dialog = new ThreadExceptionDialog(exception))
            {
                return dialog.DialogResult;
            }
        }

        public static TControl SourceControl<TControl>(this ContextMenuStrip contextMenu) where TControl: Control
        {
            for (Control control = contextMenu.SourceControl; control != null; control = control.Parent)
            {
                TControl local = control as TControl;
                if (local != null)
                {
                    return local;
                }
            }
            return default(TControl);
        }

        public static void TraceAndShowException(Exception exception)
        {
            InternalTraceAndShowException(null, exception, string.Empty);
        }

        public static void WrappedInvoke(this Form form, Action action)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            try
            {
                action();
            }
            catch (Exception exception)
            {
                InternalTraceAndShowException(form, exception, string.Empty);
            }
        }
    }
}

