namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=true)]
    public class DependsOnPropertiesAttribute : Attribute
    {
        public DependsOnPropertiesAttribute(string prop1)
        {
            string[] properties = new string[] { prop1 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2)
        {
            string[] properties = new string[] { prop1, prop2 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3)
        {
            string[] properties = new string[] { prop1, prop2, prop3 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4)
        {
            string[] properties = new string[] { prop1, prop2, prop3, prop4 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5)
        {
            string[] properties = new string[] { prop1, prop2, prop3, prop4, prop5 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6)
        {
            string[] properties = new string[] { prop1, prop2, prop3, prop4, prop5, prop6 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6, string prop7)
        {
            string[] properties = new string[] { prop1, prop2, prop3, prop4, prop5, prop6, prop7 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6, string prop7, string prop8)
        {
            string[] properties = new string[] { prop1, prop2, prop3, prop4, prop5, prop6, prop7, prop8 };
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6, string prop7, string prop8, string prop9)
        {
            string[] properties = new string[9];
            properties[0] = prop1;
            properties[1] = prop2;
            properties[2] = prop3;
            properties[3] = prop4;
            properties[4] = prop5;
            properties[5] = prop6;
            properties[6] = prop7;
            properties[7] = prop8;
            properties[8] = prop9;
            this.Init(properties);
        }

        public DependsOnPropertiesAttribute(string prop1, string prop2, string prop3, string prop4, string prop5, string prop6, string prop7, string prop8, string prop9, string prop10)
        {
            string[] properties = new string[10];
            properties[0] = prop1;
            properties[1] = prop2;
            properties[2] = prop3;
            properties[3] = prop4;
            properties[4] = prop5;
            properties[5] = prop6;
            properties[6] = prop7;
            properties[7] = prop8;
            properties[8] = prop9;
            properties[9] = prop10;
            this.Init(properties);
        }

        private void Init(params string[] properties)
        {
            this.Properties = properties;
        }

        public string[] Properties { get; private set; }
    }
}

