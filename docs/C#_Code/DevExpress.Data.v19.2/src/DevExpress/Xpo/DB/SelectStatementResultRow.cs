namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class SelectStatementResultRow
    {
        [XmlIgnore]
        public object[] Values;

        public SelectStatementResultRow() : this(new object[0])
        {
        }

        public SelectStatementResultRow(object[] values)
        {
            this.Values = values;
        }

        public SelectStatementResultRow Clone()
        {
            object[] destinationArray = new object[this.Values.Length];
            Array.Copy(this.Values, destinationArray, destinationArray.Length);
            return new SelectStatementResultRow(destinationArray);
        }

        [XmlArrayItem(typeof(bool)), XmlArrayItem(typeof(byte)), XmlArrayItem(typeof(sbyte)), XmlArrayItem(typeof(char)), XmlArrayItem(typeof(decimal)), XmlArrayItem(typeof(double)), XmlArrayItem(typeof(float)), XmlArrayItem(typeof(int)), XmlArrayItem(typeof(uint)), XmlArrayItem(typeof(long)), XmlArrayItem(typeof(ulong)), XmlArrayItem(typeof(short)), XmlArrayItem(typeof(ushort)), XmlArrayItem(typeof(Guid)), XmlArrayItem(typeof(string)), XmlArrayItem(typeof(DateTime)), XmlArrayItem(typeof(TimeSpan)), XmlArrayItem(typeof(byte[])), XmlArrayItem(typeof(NullValue))]
        public object[] XmlValues
        {
            get
            {
                object[] objArray = (object[]) this.Values.Clone();
                for (int i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i] == null)
                    {
                        objArray[i] = NullValue.Value;
                    }
                    else
                    {
                        string str = objArray[i] as string;
                        if (str != null)
                        {
                            objArray[i] = OperandValue.FormatString(str);
                        }
                    }
                }
                return objArray;
            }
            set
            {
                object[] objArray = (object[]) value.Clone();
                for (int i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i] is NullValue)
                    {
                        objArray[i] = null;
                    }
                    else
                    {
                        string str = objArray[i] as string;
                        if (str != null)
                        {
                            objArray[i] = OperandValue.ReformatString(str);
                        }
                    }
                }
                this.Values = objArray;
            }
        }
    }
}

