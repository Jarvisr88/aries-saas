namespace DevExpress.XtraReports.Expressions
{
    using DevExpress.Data.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class BasicExpressionBindingCollection : ExpressionBindingCollectionBase<BasicExpressionBinding>
    {
        internal void AddExpression(string propertyName, string expression)
        {
            base.Add(new BasicExpressionBinding(propertyName, expression));
        }

        internal static bool CorrespondsTo(BasicExpressionBinding binding, string propertyName) => 
            string.Equals(binding.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase);

        internal string GetExpression(string propertyName)
        {
            string expression;
            BasicExpressionBinding binding1 = this[propertyName];
            if (binding1 != null)
            {
                expression = binding1.Expression;
            }
            else
            {
                BasicExpressionBinding local1 = binding1;
                expression = null;
            }
            string local2 = expression;
            string text2 = local2;
            if (local2 == null)
            {
                string local3 = local2;
                text2 = string.Empty;
            }
            return text2;
        }

        internal List<BasicExpressionBinding> GetExpressions()
        {
            List<BasicExpressionBinding> list = new List<BasicExpressionBinding>(1);
            foreach (BasicExpressionBinding binding in base.Items)
            {
                if (!string.IsNullOrEmpty(binding.Expression))
                {
                    list.Add(binding);
                }
            }
            return list;
        }

        internal void RemoveExpression(string propertyName)
        {
            int index = base.Items.FindIndex<BasicExpressionBinding>(item => CorrespondsTo(item, propertyName));
            if (index >= 0)
            {
                base.Items.RemoveAt(index);
            }
        }

        public BasicExpressionBinding this[string propertyName]
        {
            get
            {
                BasicExpressionBinding binding2;
                using (IEnumerator<BasicExpressionBinding> enumerator = base.Items.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            BasicExpressionBinding current = enumerator.Current;
                            if (string.IsNullOrEmpty(current.Expression) || !string.Equals(propertyName, current.PropertyName, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }
                            binding2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return binding2;
            }
        }
    }
}

