namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    internal class DefaultValueParameter : IClientParameter
    {
        private string description;
        private bool isDescriptionChanged;
        private object value;
        private bool isValueChanged;
        private bool visible;
        private bool isVisibleChanged;

        public DefaultValueParameter(string path)
        {
            Guard.ArgumentNotNull(path, "path");
            this.Path = path;
            this.Name = ExtractName(path);
        }

        public void CopyFrom(IClientParameter parameter)
        {
            this.description = parameter.Description;
            this.isDescriptionChanged = false;
            this.isValueChanged = false;
            this.value = parameter.Value;
            this.isVisibleChanged = false;
            this.visible = parameter.Visible;
        }

        public void CopyTo(IClientParameter parameter)
        {
            if (this.isDescriptionChanged)
            {
                parameter.Description = this.Description;
            }
            if (this.isValueChanged)
            {
                parameter.Value = this.Value;
            }
            if (this.isVisibleChanged)
            {
                parameter.Visible = this.Visible;
            }
        }

        private static string ExtractName(string path)
        {
            int startIndex = path.LastIndexOf('.');
            if (startIndex == -1)
            {
                return path;
            }
            startIndex++;
            return path.Substring(startIndex);
        }

        public string Description
        {
            get => 
                this.description;
            set
            {
                this.description = value;
                this.isDescriptionChanged = true;
            }
        }

        public string Name { get; private set; }

        public System.Type Type
        {
            get
            {
                object obj1 = this.Value;
                if (obj1 != null)
                {
                    return obj1.GetType();
                }
                object local1 = obj1;
                return null;
            }
        }

        public object Value
        {
            get => 
                this.value;
            set
            {
                this.value = value;
                this.isValueChanged = true;
            }
        }

        public bool MultiValue =>
            this.value is IEnumerable;

        public bool AllowNull =>
            (this.value == null) || (Nullable.GetUnderlyingType(this.value.GetType()) != null);

        public bool Visible
        {
            get => 
                this.visible;
            set
            {
                this.visible = value;
                this.isVisibleChanged = true;
            }
        }

        internal string Path { get; private set; }
    }
}

