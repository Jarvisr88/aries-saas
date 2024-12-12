namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    public class BindingExpressionUncommonField
    {
        private static IBindingExpressionUncommonField ItemValueBindingExpression = typeof(Selector).Wrap<ISelectorWrapper>().ItemValueBindingExpression;

        public static void ClearValue(DependencyObject dependencyObject)
        {
            ItemValueBindingExpression.ClearValue(dependencyObject);
        }

        public static BindingExpression GetValue(DependencyObject dependencyObject) => 
            ItemValueBindingExpression.GetValue(dependencyObject);

        public static void SetValue(DependencyObject dependencyObject, BindingExpression bindingExpr)
        {
            ItemValueBindingExpression.SetValue(dependencyObject, bindingExpr);
        }

        [BindingFlags(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance), Wrapper]
        public interface IBindingExpressionUncommonField : BindingExpressionUncommonField.IUncommonField<BindingExpression>
        {
            void ClearValue(DependencyObject dependencyObject);
            void SetValue(DependencyObject dependencyObject, BindingExpression bindingExpr);
        }

        [Wrapper, BindingFlags(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)]
        public interface ISelectorWrapper
        {
            [FieldAccessor]
            BindingExpressionUncommonField.IBindingExpressionUncommonField ItemValueBindingExpression { get; }
        }

        [BindingFlags(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance), Wrapper]
        public interface IUncommonField<T>
        {
            BindingExpression GetValue(DependencyObject dependencyObject);
        }
    }
}

