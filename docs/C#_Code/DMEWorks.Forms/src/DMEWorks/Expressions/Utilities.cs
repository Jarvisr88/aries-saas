namespace DMEWorks.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public static class Utilities
    {
        private static readonly Dictionary<Type, PrimitiveType> map;

        static Utilities()
        {
            Dictionary<Type, PrimitiveType> dictionary1 = new Dictionary<Type, PrimitiveType>();
            dictionary1.Add(typeof(string), PrimitiveType.String);
            dictionary1.Add(typeof(sbyte), PrimitiveType.Int);
            dictionary1.Add(typeof(short), PrimitiveType.Int);
            dictionary1.Add(typeof(int), PrimitiveType.Int);
            dictionary1.Add(typeof(long), PrimitiveType.Int);
            dictionary1.Add(typeof(byte), PrimitiveType.Int);
            dictionary1.Add(typeof(ushort), PrimitiveType.Int);
            dictionary1.Add(typeof(uint), PrimitiveType.Int);
            dictionary1.Add(typeof(ulong), PrimitiveType.Int);
            dictionary1.Add(typeof(sbyte?), PrimitiveType.Int);
            dictionary1.Add(typeof(short?), PrimitiveType.Int);
            dictionary1.Add(typeof(int?), PrimitiveType.Int);
            dictionary1.Add(typeof(long?), PrimitiveType.Int);
            dictionary1.Add(typeof(byte?), PrimitiveType.Int);
            dictionary1.Add(typeof(ushort?), PrimitiveType.Int);
            dictionary1.Add(typeof(uint?), PrimitiveType.Int);
            dictionary1.Add(typeof(ulong?), PrimitiveType.Int);
            dictionary1.Add(typeof(decimal), PrimitiveType.Decimal);
            dictionary1.Add(typeof(decimal?), PrimitiveType.Decimal);
            dictionary1.Add(typeof(double), PrimitiveType.Float);
            dictionary1.Add(typeof(float), PrimitiveType.Float);
            dictionary1.Add(typeof(double?), PrimitiveType.Float);
            dictionary1.Add(typeof(float?), PrimitiveType.Float);
            dictionary1.Add(typeof(DateTime), PrimitiveType.Date);
            dictionary1.Add(typeof(DateTimeOffset), PrimitiveType.Date);
            dictionary1.Add(typeof(DateTime?), PrimitiveType.Date);
            dictionary1.Add(typeof(DateTimeOffset?), PrimitiveType.Date);
            dictionary1.Add(typeof(bool), PrimitiveType.Bool);
            dictionary1.Add(typeof(bool?), PrimitiveType.Bool);
            dictionary1.Add(typeof(char), PrimitiveType.Char);
            dictionary1.Add(typeof(char?), PrimitiveType.Char);
            map = dictionary1;
        }

        public static Expression BuildFilter(IEnumerable<Expression> expressions, string searchString)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException("expressions");
            }
            if (searchString == null)
            {
                throw new ArgumentNullException("searchString");
            }
            char[] separator = new char[] { ' ' };
            string[] strArray = searchString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 0)
            {
                return null;
            }
            List<Expression> list = new List<Expression>();
            foreach (string str in strArray)
            {
                Expression item = BuildSimpleFilter(expressions, str);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            if (list.Count == 0)
            {
                return null;
            }
            Expression left = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                left = Expression.AndAlso(left, list[i]);
            }
            return left;
        }

        private static Expression BuildSimpleFilter(IEnumerable<Expression> expressions, string searchString)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException("expressions");
            }
            List<PrimitiveComparisonExpression> list = new List<PrimitiveComparisonExpression>();
            foreach (Expression expression2 in expressions)
            {
                PrimitiveType primitiveType = GetPrimitiveType(expression2);
                switch (primitiveType)
                {
                    case PrimitiveType.Date:
                    {
                        DateTime time;
                        if (!TryParseDate(searchString, out time))
                        {
                            continue;
                        }
                        list.Add(new PrimitiveComparisonExpression(expression2, time));
                        continue;
                    }
                    case PrimitiveType.Decimal:
                    {
                        decimal num;
                        if (!TryParseDecimal(searchString, out num))
                        {
                            continue;
                        }
                        list.Add(new PrimitiveComparisonExpression(expression2, num));
                        continue;
                    }
                    case PrimitiveType.Float:
                    {
                        double num2;
                        if (!TryParseFloat(searchString, out num2))
                        {
                            continue;
                        }
                        list.Add(new PrimitiveComparisonExpression(expression2, num2));
                        continue;
                    }
                    case PrimitiveType.Int:
                    {
                        long num3;
                        if (!TryParseInt(searchString, out num3))
                        {
                            continue;
                        }
                        list.Add(new PrimitiveComparisonExpression(expression2, num3));
                        continue;
                    }
                    case PrimitiveType.String:
                    {
                        list.Add(new PrimitiveComparisonExpression(expression2, searchString));
                        continue;
                    }
                }
            }
            if (list.Count == 0)
            {
                return null;
            }
            Expression left = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                left = Expression.OrElse(left, list[i]);
            }
            return left;
        }

        internal static PrimitiveType GetPrimitiveType(Expression expression)
        {
            PrimitiveType type;
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            PrimitiveExpression expression2 = expression as PrimitiveExpression;
            if (expression2 != null)
            {
                return expression2.GetPrimitiveType();
            }
            LambdaExpression expression3 = expression as LambdaExpression;
            if (expression3 == null)
            {
                throw new NotSupportedException("Type of the expression is not supported");
            }
            if (!TryGetPrimitiveType(expression3.ReturnType, out type))
            {
                throw new NotSupportedException("Return type of the lambda expression is not supported");
            }
            return type;
        }

        internal static bool TryGetPrimitiveType(Type type, out PrimitiveType value)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return map.TryGetValue(type, out value);
        }

        private static bool TryParseDate(string text, out DateTime value)
        {
            if (Regex.IsMatch(text, @"^\d{1,2}/\d{1,2}/\d{4}$"))
            {
                return DateTime.TryParseExact(text, "M'/'d'/'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
            }
            if (Regex.IsMatch(text, @"^\d{4}-\d{1,2}-\d{1,2}$"))
            {
                return DateTime.TryParseExact(text, "yyyy'-'M'-'d", CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
            }
            value = new DateTime();
            return false;
        }

        private static bool TryParseDecimal(string text, out decimal value) => 
            decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);

        private static bool TryParseFloat(string text, out double value) => 
            double.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);

        private static bool TryParseInt(string text, out long value) => 
            long.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);
    }
}

