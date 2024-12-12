namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    [BindingFlags(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static), Wrapper]
    public interface IBindingExpressionStatic
    {
        BindingExpressionBase CreateUntargetedBindingExpression(DependencyObject d, BindingBase binding);
    }
}

