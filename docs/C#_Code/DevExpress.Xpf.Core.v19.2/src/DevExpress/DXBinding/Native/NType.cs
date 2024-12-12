namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class NType : NIdentBase
    {
        public static string[] PrimitiveTypes;

        static NType()
        {
            string[] textArray1 = new string[14];
            textArray1[0] = "sbyte";
            textArray1[1] = "byte";
            textArray1[2] = "short";
            textArray1[3] = "ushort";
            textArray1[4] = "int";
            textArray1[5] = "uint";
            textArray1[6] = "long";
            textArray1[7] = "ulong";
            textArray1[8] = "float";
            textArray1[9] = "double";
            textArray1[10] = "decimal";
            textArray1[11] = "bool";
            textArray1[12] = "object";
            textArray1[13] = "string";
            PrimitiveTypes = textArray1;
        }

        public NType(string name, NIdentBase next, NKind kind, NIdentBase ident) : base(name, next)
        {
            this.Kind = kind;
            this.Ident = ident;
        }

        public static bool IsPrimitiveType(string type) => 
            PrimitiveTypes.Contains<string>(type);

        public NKind Kind { get; set; }

        public NIdentBase Ident { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimitive =>
            IsPrimitiveType(base.Name);

        public enum NKind
        {
            Type,
            TypeOf,
            Static,
            Attached
        }
    }
}

