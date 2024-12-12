namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class ErrorHelper
    {
        private const string err001 = "The {0} cannot resolve the IProvideValueTarget service.";
        private const string err002 = "The {0} can only be set on {1} of a DependencyObject.";
        private const string err003 = "The {0} cannot be used in styles.";
        private const string err004 = "Cannot resolve the '{0}' type.";
        private const string Err101 = "The TwoWay or OneWayToSource binding mode requires the {0} property to be set in complex {1}s.";
        private const string err102 = "The {0} property is specified in the short form, but the {1} expression contains no binding operands.";
        private const string err103 = "The {0} property is specified in the short form, but the {1} expression contains more than one binding operand.";
        private const string err104 = "The {0} cannot resolve the target type during back conversion.";
        private const string report001 = "The '{0}' property is not found on object '{1}'.";
        private const string report002 = "The '{0}({1})' method is not found on object '{2}'.";
        private static Dictionary<string, string> parserErrorReplacementMapping;

        static ErrorHelper()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("Ident ", "identifier ");
            dictionary1.Add("Int ", "integer ");
            dictionary1.Add("Float ", "float ");
            dictionary1.Add("String ", "string ");
            dictionary1.Add("??? expected", "invalid expression");
            dictionary1.Add("invalid DXBinding", "invalid expression");
            dictionary1.Add("invalid Back_ExprRoot", "invalid expression");
            dictionary1.Add("invalid TypeExpr", "invalid type expression");
            dictionary1.Add("invalid AtomRootExpr", "invalid identifier expression");
            dictionary1.Add("invalid AtomExpr", "invalid identifier expression");
            dictionary1.Add("invalid Back_AtomExpr", "invalid identifier expression");
            dictionary1.Add("invalid Execute_AtomExpr", "invalid identifier expression");
            dictionary1.Add("invalid ConstExpr", "invalid constant expression");
            dictionary1.Add("invalid RelativeExpr", "invalid expression");
            dictionary1.Add("invalid TypeIdentExpr", "invalid identifier expression");
            dictionary1.Add("invalid NextIdentExpr", "invalid identifier expression");
            dictionary1.Add("invalid Back_AssignLeft", "invalid assignment expression");
            dictionary1.Add("invalid Back_RelativeValueExpr", "invalid expression");
            dictionary1.Add("invalid Execute_Ident", "invalid identifier expression");
            dictionary1.Add("invalid Execute_RelativeExpr", "invalid expression");
            dictionary1.Add("invalid Event_AtomExpr", "invalid identifier expression");
            dictionary1.Add("invalid Event_Ident", "invalid identifier expression");
            dictionary1.Add("invalid Event_RelativeExpr", "invalid expression");
            parserErrorReplacementMapping = dictionary1;
        }

        public static string Err001(object markup) => 
            $"The {GetMarkupName(markup.GetType())} cannot resolve the IProvideValueTarget service.";

        public static string Err002(object markup) => 
            $"The {GetMarkupName(markup.GetType())} can only be set on {GetMarkupTarget(markup.GetType())} of a DependencyObject.";

        public static string Err003(object markup) => 
            $"The {GetMarkupName(markup.GetType())} cannot be used in styles.";

        public static string Err004(string type) => 
            $"Cannot resolve the '{type}' type.";

        public static string Err101_TwoWay() => 
            $"The TwoWay or OneWayToSource binding mode requires the {NameDXBinding + "." + NameDXBindingBackExpr} property to be set in complex {NameDXBinding}s.";

        public static string Err102() => 
            $"The {NameDXBinding + "." + NameDXBindingBackExpr} property is specified in the short form, but the {NameDXBinding + "." + NameDXBindingExpr} expression contains no binding operands.";

        public static string Err103() => 
            $"The {NameDXBinding + "." + NameDXBindingBackExpr} property is specified in the short form, but the {NameDXBinding + "." + NameDXBindingExpr} expression contains more than one binding operand.";

        public static string Err104() => 
            $"The {NameDXBinding} cannot resolve the target type during back conversion.";

        private static string GetMarkupName(Type markup) => 
            markup.Name.Replace("Extension", "");

        private static string GetMarkupTarget(Type markup)
        {
            if ((GetMarkupName(markup) == "DXBinding") || (GetMarkupName(markup) == "DXCommand"))
            {
                return "a DependencyProperty";
            }
            if (GetMarkupName(markup) != "DXEvent")
            {
                throw new NotImplementedException();
            }
            return "an event";
        }

        public static string Report001(string property, Type objectType) => 
            $"The '{property}' property is not found on object '{objectType.Name}'.";

        public static string Report002(string method, Type[] methodArgs, Type objectType)
        {
            string local3;
            if ((methodArgs == null) || !methodArgs.Any<Type>())
            {
                local3 = string.Empty;
            }
            else
            {
                Func<Type, string> selector = <>c.<>9__37_0;
                if (<>c.<>9__37_0 == null)
                {
                    Func<Type, string> local1 = <>c.<>9__37_0;
                    selector = <>c.<>9__37_0 = x => (x != null) ? x.Name : "null";
                }
                Func<string, string, string> func = <>c.<>9__37_1;
                if (<>c.<>9__37_1 == null)
                {
                    Func<string, string, string> local2 = <>c.<>9__37_1;
                    func = <>c.<>9__37_1 = (x, y) => x + ", " + y;
                }
                local3 = methodArgs.Select<Type, string>(selector).Aggregate<string>(func);
            }
            string str = local3;
            return $"The '{method}({str})' method is not found on object '{objectType.Name}'.";
        }

        public static string ReportBindingError(string baseMessage, string expr, string backExpr)
        {
            Tuple<string, string>[] exprs = new Tuple<string, string>[] { new Tuple<string, string>(NameDXBindingExpr, expr), new Tuple<string, string>(NameDXBindingBackExpr, backExpr) };
            return ReportCore(NameDXBinding + " error:", baseMessage, exprs);
        }

        public static string ReportCommandError(string baseMessage, string execute, string canExecute)
        {
            Tuple<string, string>[] exprs = new Tuple<string, string>[] { new Tuple<string, string>(NameDXCommandExecute, execute), new Tuple<string, string>(NameDXCommandCanExecute, canExecute) };
            return ReportCore(NameDXCommand + " error:", baseMessage, exprs);
        }

        private static string ReportCore(string prefix, string baseMessage, Tuple<string, string>[] exprs)
        {
            Func<Tuple<string, string>, string> selector = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<Tuple<string, string>, string> local1 = <>c.<>9__41_0;
                selector = <>c.<>9__41_0 = delegate (Tuple<string, string> x) {
                    string str2 = x.Item2;
                    string format = x.Item1 + "='{0}'";
                    return !string.IsNullOrEmpty(str2) ? string.Format(format, str2) : null;
                };
            }
            Func<string, bool> predicate = <>c.<>9__41_1;
            if (<>c.<>9__41_1 == null)
            {
                Func<string, bool> local2 = <>c.<>9__41_1;
                predicate = <>c.<>9__41_1 = x => !string.IsNullOrEmpty(x);
            }
            Func<string, string, string> func = <>c.<>9__41_2;
            if (<>c.<>9__41_2 == null)
            {
                Func<string, string, string> local3 = <>c.<>9__41_2;
                func = <>c.<>9__41_2 = (x, y) => $"{x}, {y}";
            }
            string str = exprs.Select<Tuple<string, string>, string>(selector).Where<string>(predicate).Aggregate<string>(func);
            return (!string.IsNullOrEmpty(str) ? $"{prefix} {baseMessage} {str}." : baseMessage);
        }

        public static string ReportEventError(string baseMessage, string expr)
        {
            Tuple<string, string>[] exprs = new Tuple<string, string>[] { new Tuple<string, string>(NameDXEventHandler, expr) };
            return ReportCore(NameDXEvent + " error:", baseMessage, exprs);
        }

        internal static string ReportParserError(int pos, string msg, ParserMode mode)
        {
            foreach (KeyValuePair<string, string> pair in parserErrorReplacementMapping)
            {
                msg = msg.Replace(pair.Key, pair.Value);
            }
            string nameDXBinding = string.Empty;
            switch (mode)
            {
                case ParserMode.BindingExpr:
                case ParserMode.BindingBackExpr:
                    nameDXBinding = NameDXBinding;
                    break;

                case ParserMode.CommandExecute:
                case ParserMode.CommandCanExecute:
                    nameDXBinding = NameDXCommand;
                    break;

                case ParserMode.Event:
                    nameDXBinding = NameDXEvent;
                    break;

                default:
                    throw new NotImplementedException();
            }
            nameDXBinding = nameDXBinding + " error";
            if (pos != -1)
            {
                nameDXBinding = nameDXBinding + " " + $"(position {pos})";
            }
            return (nameDXBinding + ": " + msg + ".");
        }

        private static string NameDXBinding =>
            "DXBinding";

        private static string NameDXBindingExpr =>
            "Expr";

        private static string NameDXBindingBackExpr =>
            "BackExpr";

        private static string NameDXCommand =>
            "DXCommand";

        private static string NameDXCommandExecute =>
            "Execute";

        private static string NameDXCommandCanExecute =>
            "CanExecute";

        private static string NameDXEvent =>
            "DXEvent";

        private static string NameDXEventHandler =>
            "Handler";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ErrorHelper.<>c <>9 = new ErrorHelper.<>c();
            public static Func<Type, string> <>9__37_0;
            public static Func<string, string, string> <>9__37_1;
            public static Func<Tuple<string, string>, string> <>9__41_0;
            public static Func<string, bool> <>9__41_1;
            public static Func<string, string, string> <>9__41_2;

            internal string <Report002>b__37_0(Type x) => 
                (x != null) ? x.Name : "null";

            internal string <Report002>b__37_1(string x, string y) => 
                x + ", " + y;

            internal string <ReportCore>b__41_0(Tuple<string, string> x)
            {
                string str2 = x.Item2;
                string format = x.Item1 + "='{0}'";
                return (!string.IsNullOrEmpty(str2) ? string.Format(format, str2) : null);
            }

            internal bool <ReportCore>b__41_1(string x) => 
                !string.IsNullOrEmpty(x);

            internal string <ReportCore>b__41_2(string x, string y) => 
                $"{x}, {y}";
        }
    }
}

