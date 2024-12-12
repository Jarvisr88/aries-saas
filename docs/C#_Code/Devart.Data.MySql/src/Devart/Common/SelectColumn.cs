namespace Devart.Common
{
    using System;

    public sealed class SelectColumn : SelectStatementNode
    {
        private string a;
        private string b;
        private string c;
        private string d;
        private string e;
        private string f;

        public SelectColumn(string name) : this(string.Empty, string.Empty, string.Empty, name, string.Empty)
        {
        }

        public SelectColumn(string schema, string table, string name, string alias) : this(string.Empty, schema, table, name, alias, null, -1, -1)
        {
        }

        public SelectColumn(string database, string schema, string table, string name, string alias) : this(database, schema, table, name, alias, null, -1, -1)
        {
        }

        internal SelectColumn(string A_0, string A_1, string A_2, string A_3, string A_4, string A_5, int A_6, int A_7)
        {
            this.a = A_0;
            this.b = A_1;
            this.c = A_2;
            this.d = A_3;
            this.f = A_5;
            base.a = A_6;
            base.b = A_7;
            this.e = A_4;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !ReferenceEquals(base.GetType(), obj.GetType()))
            {
                return false;
            }
            SelectColumn column = (SelectColumn) obj;
            return ((column.e == this.e) && ((column.d == this.d) && ((column.b == this.b) && ((column.c == this.c) && (column.a == this.a)))));
        }

        public override int GetHashCode() => 
            (((this.a.GetHashCode() ^ this.b.GetHashCode()) ^ this.c.GetHashCode()) ^ this.d.GetHashCode()) ^ this.e.GetHashCode();

        public override string ToString() => 
            this.ToString(" ");

        internal override string ToString(string asKeyword)
        {
            string str = ((asKeyword == null) || Utils.IsEmpty(this.e)) ? null : (asKeyword + this.e);
            if (!Utils.IsEmpty(this.Expression))
            {
                return (this.f + str);
            }
            str = this.d + str;
            if (!Utils.IsEmpty(this.c))
            {
                str = this.c + "." + str;
                if (!Utils.IsEmpty(this.b))
                {
                    str = this.b + "." + str;
                }
                if (!Utils.IsEmpty(this.a))
                {
                    str = this.a + "." + str;
                }
            }
            return str;
        }

        public string Database
        {
            get => 
                this.a;
            set
            {
                this.a = value;
                base.g();
            }
        }

        public string Schema
        {
            get => 
                this.b;
            set
            {
                this.b = value;
                base.g();
            }
        }

        public string Table
        {
            get => 
                this.c;
            set
            {
                this.c = value;
                base.g();
            }
        }

        public string Name
        {
            get => 
                this.d;
            set
            {
                this.d = value;
                base.g();
            }
        }

        public string Alias
        {
            get => 
                this.e;
            set
            {
                this.e = value;
                base.g();
            }
        }

        public string Expression
        {
            get => 
                this.f;
            set
            {
                this.f = value;
                if (this.f != null)
                {
                    string str;
                    this.a = (string) (str = null);
                    string text1 = str;
                    string text2 = this.b = text1;
                    this.d = this.c = text2;
                }
                base.g();
            }
        }
    }
}

