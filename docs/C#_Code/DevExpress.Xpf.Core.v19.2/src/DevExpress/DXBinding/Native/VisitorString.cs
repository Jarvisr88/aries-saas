namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class VisitorString : VisitorBase<string>
    {
        private static readonly Dictionary<NConstant.NKind, string> constantKindToStringMapping;
        private static readonly Dictionary<NBinary.NKind, string> binaryKindToStringMapping;
        private static readonly Dictionary<NUnary.NKind, string> unaryKindToStringMapping;
        private static readonly Dictionary<NCast.NKind, string> castKindToStringMapping;
        private static readonly Dictionary<NTernary.NKind, string> ternaryKindToStringMapping;
        private static readonly string identToString;
        private static readonly string nextIdentToString;
        private static readonly string methodToString;
        private static readonly string newToString;
        private static readonly string nextMethodArgToString;
        private static readonly string exprIdentToString;
        private static readonly string indexToString;
        private static readonly Dictionary<NType.NKind, string> typeKindToStringMapping;
        private static readonly string typeToString;
        private static readonly string typeNullableToString;
        private static readonly Dictionary<NRelative.NKind, string> relativeKindToStringMapping;
        private static readonly string assignToString;
        private static readonly string nextExprToString;

        static VisitorString()
        {
            Dictionary<NConstant.NKind, string> dictionary1 = new Dictionary<NConstant.NKind, string>();
            dictionary1.Add(NConstant.NKind.Null, "null");
            dictionary1.Add(NConstant.NKind.Integer, "{0}");
            dictionary1.Add(NConstant.NKind.Float, "{0}");
            dictionary1.Add(NConstant.NKind.Boolean, "{0}");
            dictionary1.Add(NConstant.NKind.String, "\"{0}\"");
            constantKindToStringMapping = dictionary1;
            Dictionary<NBinary.NKind, string> dictionary2 = new Dictionary<NBinary.NKind, string>();
            dictionary2.Add(NBinary.NKind.Mul, "{0}*{1}");
            dictionary2.Add(NBinary.NKind.Div, "{0}/{1}");
            dictionary2.Add(NBinary.NKind.Mod, "{0}%{1}");
            dictionary2.Add(NBinary.NKind.Plus, "{0}+{1}");
            dictionary2.Add(NBinary.NKind.Minus, "{0}-{1}");
            dictionary2.Add(NBinary.NKind.And, "{0}&{1}");
            dictionary2.Add(NBinary.NKind.Or, "{0}|{1}");
            dictionary2.Add(NBinary.NKind.Xor, "{0}^{1}");
            dictionary2.Add(NBinary.NKind.ShiftLeft, "{0}<<{1}");
            dictionary2.Add(NBinary.NKind.ShiftRight, "{0}>>{1}");
            dictionary2.Add(NBinary.NKind.AndAlso, "{0}&&{1}");
            dictionary2.Add(NBinary.NKind.OrElse, "{0}||{1}");
            dictionary2.Add(NBinary.NKind.Equal, "{0}=={1}");
            dictionary2.Add(NBinary.NKind.NotEqual, "{0}!={1}");
            dictionary2.Add(NBinary.NKind.Coalesce, "{0}??{1}");
            binaryKindToStringMapping = dictionary2;
            Dictionary<NUnary.NKind, string> dictionary3 = new Dictionary<NUnary.NKind, string>();
            dictionary3.Add(NUnary.NKind.Plus, "{0}");
            dictionary3.Add(NUnary.NKind.Minus, "-{0}");
            dictionary3.Add(NUnary.NKind.Not, "!{0}");
            unaryKindToStringMapping = dictionary3;
            Dictionary<NCast.NKind, string> dictionary4 = new Dictionary<NCast.NKind, string>();
            dictionary4.Add(NCast.NKind.Cast, "({1}){0}");
            dictionary4.Add(NCast.NKind.Is, "({0} is {1})");
            dictionary4.Add(NCast.NKind.As, "({0} as {1})");
            castKindToStringMapping = dictionary4;
            Dictionary<NTernary.NKind, string> dictionary5 = new Dictionary<NTernary.NKind, string>();
            dictionary5.Add(NTernary.NKind.Condition, "{0}?{1}:{2}");
            ternaryKindToStringMapping = dictionary5;
            identToString = "{0}";
            nextIdentToString = ".{0}";
            methodToString = "{0}({1})";
            newToString = "new {0}({1})";
            nextMethodArgToString = ",{0}";
            exprIdentToString = "({0})";
            indexToString = "[{0}]";
            Dictionary<NType.NKind, string> dictionary6 = new Dictionary<NType.NKind, string>();
            dictionary6.Add(NType.NKind.Type, "{0}");
            dictionary6.Add(NType.NKind.TypeOf, "typeof({0})");
            dictionary6.Add(NType.NKind.Static, "{0}.{1}");
            dictionary6.Add(NType.NKind.Attached, "({0}.{1})");
            typeKindToStringMapping = dictionary6;
            typeToString = "{0}";
            typeNullableToString = "{0}?";
            Dictionary<NRelative.NKind, string> dictionary7 = new Dictionary<NRelative.NKind, string>();
            dictionary7.Add(NRelative.NKind.Context, "");
            dictionary7.Add(NRelative.NKind.Self, "{0}");
            dictionary7.Add(NRelative.NKind.Parent, "{0}");
            dictionary7.Add(NRelative.NKind.Resource, "{0}({1})");
            dictionary7.Add(NRelative.NKind.Element, "{0}({1})");
            dictionary7.Add(NRelative.NKind.Ancestor, "{0}({1})");
            dictionary7.Add(NRelative.NKind.Value, "{0}");
            dictionary7.Add(NRelative.NKind.Parameter, "{0}");
            dictionary7.Add(NRelative.NKind.Sender, "{0}");
            dictionary7.Add(NRelative.NKind.Args, "{0}");
            relativeKindToStringMapping = dictionary7;
            assignToString = "{0}={1}";
            nextExprToString = ";{0}";
        }

        protected VisitorString()
        {
        }

        protected override string Assign(NAssign n, string value) => 
            string.Format(assignToString, this.Visit(n.Left), this.Visit(n.Expr));

        protected override string Binary(NBinary n, string left, string right) => 
            string.Format(binaryKindToStringMapping[n.Kind], left, right);

        protected override string Cast(NCast n, string value) => 
            string.Format(castKindToStringMapping[n.Kind], value, this.Visit(n.Type));

        public static string CombineArgs(IEnumerable<string> args)
        {
            if ((args == null) || (args.Count<string>() == 0))
            {
                return null;
            }
            Func<string, string, string> func = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<string, string, string> local1 = <>c.<>9__41_0;
                func = <>c.<>9__41_0 = (x, y) => x + string.Format(nextMethodArgToString, y);
            }
            return args.Aggregate<string>(func);
        }

        public static string CombineIdents(IEnumerable<string> idents)
        {
            if ((idents == null) || (idents.Count<string>() == 0))
            {
                return null;
            }
            Func<string, string, string> func = <>c.<>9__40_0;
            if (<>c.<>9__40_0 == null)
            {
                Func<string, string, string> local1 = <>c.<>9__40_0;
                func = <>c.<>9__40_0 = (x, y) => x + string.Format(nextIdentToString, y);
            }
            return idents.Aggregate<string>(func);
        }

        protected override string Constant(NConstant n) => 
            string.Format(constantKindToStringMapping[n.Kind], n.Value?.ToString());

        protected override string ExprIdent(string from, NExprIdent n) => 
            string.Format(exprIdentToString, base.ExprIdent(from, n));

        protected override string Ident(string from, NIdent n) => 
            string.Format(identToString, n.Name);

        protected override string Index(string from, NIndex n, IEnumerable<string> indexArgs) => 
            string.Format(indexToString, CombineArgs(indexArgs));

        protected override string Method(string from, NMethod n, IEnumerable<string> methodArgs) => 
            string.Format(methodToString, n.Name, CombineArgs(methodArgs));

        protected override string Relative(string from, NRelative n)
        {
            string[] textArray3;
            if (n.Kind == NRelative.NKind.Resource)
            {
                return string.Format(relativeKindToStringMapping[n.Kind], n.Name, n.ResourceName);
            }
            if (n.Kind == NRelative.NKind.Element)
            {
                return string.Format(relativeKindToStringMapping[n.Kind], n.Name, n.ElementName);
            }
            if (n.Kind != NRelative.NKind.Ancestor)
            {
                return string.Format(relativeKindToStringMapping[n.Kind], n.Name);
            }
            if (n.AncestorLevel == null)
            {
                textArray3 = new string[] { this.Type(n.AncestorType) };
            }
            else
            {
                textArray3 = new string[] { this.Type(n.AncestorType), n.AncestorLevel.ToString() };
            }
            string[] args = textArray3;
            return string.Format(relativeKindToStringMapping[n.Kind], n.Name, CombineArgs(args));
        }

        public static string Resolve(NRoot n)
        {
            Func<string, string, string> func = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<string, string, string> local1 = <>c.<>9__38_0;
                func = <>c.<>9__38_0 = (x, y) => x + string.Format(nextExprToString, y);
            }
            return new VisitorString().RootVisit(n).Aggregate<string>(func);
        }

        public static string ResolveIdent(NIdentBase n, bool recursive)
        {
            VisitorString str = new VisitorString();
            return (recursive ? str.RootIdent(n) : str.RootIdentCore(null, n));
        }

        protected override string RootIdent(NIdentBase n) => 
            CombineIdents(from x in n.Unfold() select base.RootIdentCore(null, x));

        protected override string Ternary(NTernary n, string first, string second, string third) => 
            string.Format(ternaryKindToStringMapping[n.Kind], first, second, third);

        private string Type(NType n)
        {
            string str = string.Empty;
            if (n.Ident != null)
            {
                str = base.RootIdentCore(null, n.Ident);
            }
            string str2 = n.IsNullable ? string.Format(typeNullableToString, n.Name) : string.Format(typeToString, n.Name);
            return string.Format(typeKindToStringMapping[n.Kind], str2, str);
        }

        protected override string Type_Attached(string from, NType n) => 
            this.Type(n);

        protected override string Type_New(string from, NNew n, IEnumerable<string> args) => 
            string.Format(newToString, n.Type.IsNullable ? (n.Name + "?") : n.Name, CombineArgs(args));

        protected override string Type_StaticIdent(string from, NType n) => 
            this.Type(n);

        protected override string Type_StaticMethod(string from, NType n, IEnumerable<string> methodArgs) => 
            this.Type(n);

        protected override string Type_Type(string from, NType n) => 
            this.Type(n);

        protected override string Type_TypeOf(string from, NType n) => 
            this.Type(n);

        protected override string Unary(NUnary n, string value) => 
            string.Format(unaryKindToStringMapping[n.Kind], value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisitorString.<>c <>9 = new VisitorString.<>c();
            public static Func<string, string, string> <>9__38_0;
            public static Func<string, string, string> <>9__40_0;
            public static Func<string, string, string> <>9__41_0;

            internal string <CombineArgs>b__41_0(string x, string y) => 
                x + string.Format(VisitorString.nextMethodArgToString, y);

            internal string <CombineIdents>b__40_0(string x, string y) => 
                x + string.Format(VisitorString.nextIdentToString, y);

            internal string <Resolve>b__38_0(string x, string y) => 
                x + string.Format(VisitorString.nextExprToString, y);
        }
    }
}

