namespace Devart.Common
{
    using System;
    using System.ComponentModel;

    public class DbLoaderColumn : MarshalByRefObject
    {
        private string a;
        private int b;
        private int c;
        private int d;

        public DbLoaderColumn()
        {
            this.a = string.Empty;
        }

        public DbLoaderColumn(string name, int size, int precision, int scale)
        {
            this.a = string.Empty;
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.a = name;
            this.b = size;
            this.c = precision;
            this.d = scale;
        }

        public override string ToString() => 
            this.Name;

        public string Name
        {
            get => 
                this.a;
            set
            {
                if (this.a == null)
                {
                    throw new ArgumentNullException("name");
                }
                this.a = value;
            }
        }

        [DefaultValue(0)]
        public virtual int Size
        {
            get => 
                this.b;
            set => 
                this.b = value;
        }

        [DefaultValue(0)]
        public virtual int Precision
        {
            get => 
                this.c;
            set => 
                this.c = value;
        }

        [DefaultValue(0)]
        public int Scale
        {
            get => 
                this.d;
            set => 
                this.d = value;
        }
    }
}

