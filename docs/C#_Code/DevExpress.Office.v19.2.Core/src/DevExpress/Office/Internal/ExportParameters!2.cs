namespace DevExpress.Office.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ExportParameters<TFormat, TResult>
    {
        private readonly Dictionary<Type, object> container;

        public ExportParameters(TFormat documentFormat, string targetUri, System.Text.Encoding encoding)
        {
            this.container = new Dictionary<Type, object>();
            this.DocumentFormat = documentFormat;
            this.TargetUri = targetUri;
            this.Encoding = encoding;
        }

        public void AddParameter<T>(T parameter)
        {
            this.container.Add(parameter.GetType(), parameter);
        }

        public void AddParameter<T>(Type type, T parameter)
        {
            this.container.Add(type, parameter);
        }

        public T GetParameter<T>()
        {
            object obj2;
            if (this.container.TryGetValue(typeof(T), out obj2))
            {
                return (T) obj2;
            }
            return default(T);
        }

        public TFormat DocumentFormat { get; private set; }

        public string TargetUri { get; private set; }

        public IExportManagerService<TFormat, TResult> ExportManagerService { get; set; }

        public System.Text.Encoding Encoding { get; private set; }
    }
}

