namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlTypes;

    public abstract class DbParameterBase : DbParameter
    {
        private object a;
        private ParameterDirection b;
        private bool c;
        private string d;
        private object e;
        private int f;
        private string g;
        private bool h;
        private DataRowVersion i;
        private object j;

        protected DbParameterBase()
        {
        }

        protected DbParameterBase(DbParameterBase source)
        {
            Utils.CheckArgumentNull(source, "source");
            source.a(this);
            ICloneable j = this.j as ICloneable;
            if (j != null)
            {
                this.j = j.Clone();
            }
        }

        private void a(DbParameterBase A_0)
        {
            A_0.d = this.d;
            A_0.j = this.j;
            A_0.b = this.b;
            A_0.f = this.f;
            A_0.g = this.g;
            A_0.i = this.i;
            A_0.h = this.h;
            A_0.c = this.c;
        }

        internal object a(object A_0, object A_1)
        {
            object e = this.e;
            if (A_1 == e)
            {
                this.e = A_0;
            }
            return e;
        }

        public void CopyTo(DbParameter destination)
        {
            Utils.CheckArgumentNull(destination, "destination");
            this.a((DbParameterBase) destination);
        }

        protected virtual void PropertyChanging()
        {
        }

        internal void ResetParent()
        {
            this.e = null;
        }

        protected void ResetSize()
        {
            if (this.f != 0)
            {
                this.PropertyChanging();
                this.f = 0;
            }
        }

        protected bool ShouldSerializeSize() => 
            this.f != 0;

        public override string ToString() => 
            this.ParameterName;

        protected virtual byte ValuePrecision(object value) => 
            0;

        protected virtual byte ValueScale(object value) => 
            !(value is decimal) ? 0 : ((byte) ((decimal.GetBits((decimal) value)[3] & 0xff0000) >> 0x10));

        protected virtual int ValueSize(object value)
        {
            if (!Utils.IsNull(value))
            {
                string str = value as string;
                if (str != null)
                {
                    return str.Length;
                }
                byte[] buffer = value as byte[];
                if (buffer != null)
                {
                    return buffer.Length;
                }
                char[] chArray = value as char[];
                if (chArray != null)
                {
                    return chArray.Length;
                }
                if ((value is byte) || (value is char))
                {
                    return 1;
                }
            }
            return 0;
        }

        protected object CoercedValue
        {
            get => 
                this.a;
            set => 
                this.a = value;
        }

        [y("DataParameterdirection"), Category("Data"), RefreshProperties(RefreshProperties.All)]
        public override ParameterDirection Direction
        {
            get
            {
                ParameterDirection b = this.b;
                return ((b != ((ParameterDirection) 0)) ? b : ParameterDirection.Input);
            }
            set
            {
                if (this.b != value)
                {
                    switch (value)
                    {
                        case ParameterDirection.Input:
                        case ParameterDirection.Output:
                        case ParameterDirection.InputOutput:
                        case ParameterDirection.ReturnValue:
                            this.PropertyChanging();
                            this.b = value;
                            return;
                    }
                    throw new InvalidOperationException(Devart.Common.g.a("InvalidParameterDirection", value));
                }
            }
        }

        public override bool IsNullable
        {
            get => 
                this.c;
            set => 
                this.c = value;
        }

        [y("DataParameterparameterName"), Category("DataCategory_Data")]
        public override string ParameterName
        {
            get
            {
                string d = this.d;
                return ((d != null) ? d : string.Empty);
            }
            set
            {
                if ((value != null) && ((value.Length > 0) && (value[0] == ':')))
                {
                    value = value.Substring(1);
                }
                if (this.d != value)
                {
                    this.PropertyChanging();
                    this.d = value;
                }
            }
        }

        [y("DbDataParametersize"), Category("DataCategory_Data")]
        public override int Size
        {
            get
            {
                int f = this.f;
                return this.ValueSize(this.Value);
            }
            set
            {
                if (this.f != value)
                {
                    if (value < -1)
                    {
                        throw new InvalidOperationException(Devart.Common.g.a("InvalidSizeValue", value));
                    }
                    this.PropertyChanging();
                    this.f = value;
                }
            }
        }

        [y("DataParametersourceColumn"), Category("DataCategory_Update")]
        public override string SourceColumn
        {
            get
            {
                string g = this.g;
                return ((g != null) ? g : string.Empty);
            }
            set => 
                this.g = value;
        }

        public override bool SourceColumnNullMapping
        {
            get => 
                this.h;
            set => 
                this.h = value;
        }

        [Category("DataCategory_Update"), y("DataParametersourceVersion")]
        public override DataRowVersion SourceVersion
        {
            get
            {
                DataRowVersion i = this.i;
                return ((i != ((DataRowVersion) 0)) ? i : DataRowVersion.Current);
            }
            set
            {
                if (value > DataRowVersion.Current)
                {
                    if ((value != DataRowVersion.Proposed) && (value != DataRowVersion.Default))
                    {
                        goto TR_0000;
                    }
                }
                else if ((value != DataRowVersion.Original) && (value != DataRowVersion.Current))
                {
                    goto TR_0000;
                }
                this.i = value;
                return;
            TR_0000:
                throw new InvalidOperationException(Devart.Common.g.a("InvalidDataRowVersion", value));
            }
        }

        [RefreshProperties(RefreshProperties.All), TypeConverter(typeof(StringConverter)), Category("DataCategory_Data"), y("DataParameter_Value")]
        public override object Value
        {
            get => 
                this.j;
            set
            {
                if (Utils.MonoDetected && (this.SourceColumnNullMapping && (this.DbType == DbType.Int32)))
                {
                    value = (((value == null) || ReferenceEquals(DBNull.Value, value)) || ((value is INullable) && ((INullable) value).IsNull)) ? 1 : 0;
                }
                this.a = null;
                this.j = value;
            }
        }
    }
}

