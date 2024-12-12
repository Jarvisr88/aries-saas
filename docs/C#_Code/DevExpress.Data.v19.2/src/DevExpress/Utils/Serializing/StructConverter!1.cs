namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    public abstract class StructConverter<T> : IOneTypeObjectConverter2, IOneTypeObjectConverter
    {
        protected StructConverter()
        {
        }

        public virtual bool CanConvertFromString(string str) => 
            (str != null) && !str.StartsWith("@");

        protected string[] ConvertValuesToStrings(T[] values)
        {
            string[] strArray = values as string[];
            if (strArray == null)
            {
                strArray = new string[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    strArray[i] = this.ElementToString(values[i]);
                }
            }
            return strArray;
        }

        protected abstract object CreateObject(T[] values);
        protected abstract string ElementToString(T obj);
        public object FromString(string str)
        {
            string[] strArray = this.SplitValue(str);
            T[] values = strArray as T[];
            if (values == null)
            {
                values = new T[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    values[i] = this.ToType(strArray[i]);
                }
            }
            return this.CreateObject(values);
        }

        protected abstract T[] GetValues(object obj);
        protected virtual string[] SplitValue(string value)
        {
            char[] separator = new char[] { this.Delimiter };
            return value.Split(separator);
        }

        public string ToString(object obj)
        {
            T[] values = this.GetValues(obj);
            string[] strArray = this.ConvertValuesToStrings(values);
            return ((strArray.Length != 1) ? string.Join(this.Delimiter.ToString(), strArray) : strArray[0]);
        }

        protected abstract T ToType(string str);

        public abstract System.Type Type { get; }

        protected virtual char Delimiter =>
            ',';
    }
}

