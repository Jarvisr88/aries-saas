namespace DevExpress.Utils.About
{
    using System;
    using System.IO;

    internal class UAlgoPathInfo
    {
        private string lastExceptionFileName;
        private string path;

        public static bool CheckFolder(string path)
        {
            bool flag;
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            try
            {
                if (Directory.Exists(path))
                {
                    flag = true;
                }
                else
                {
                    Directory.CreateDirectory(path);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private static string GetLastExceptionReportFileName()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DevExpress";
            return (CheckFolder(path) ? (path + @"\LastException" + UAlgo.Version + ".log") : null);
        }

        private static string GetPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DevExpress";
            return (CheckFolder(path) ? (path + @"\udx" + UAlgo.Version + ".log") : null);
        }

        public string LastExceptionReportFileName
        {
            get
            {
                this.lastExceptionFileName ??= GetLastExceptionReportFileName();
                return this.lastExceptionFileName;
            }
        }

        public string Path
        {
            get
            {
                this.path ??= GetPath();
                return this.path;
            }
        }
    }
}

