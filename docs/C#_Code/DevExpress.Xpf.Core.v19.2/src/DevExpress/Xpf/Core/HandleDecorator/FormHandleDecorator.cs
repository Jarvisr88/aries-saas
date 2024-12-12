namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class FormHandleDecorator : Decorator
    {
        public FormHandleDecorator(SolidColorBrush activeColor, SolidColorBrush inactiveColor, Thickness decoratorOffset, StructDecoratorMargins structDecoratorMargins, bool startupActiveState) : base(activeColor, inactiveColor, decoratorOffset, structDecoratorMargins, startupActiveState)
        {
        }

        protected override HandleDecoratorWindow CreateHandleDecoratorWindow(bool startupActive) => 
            new FormDecoratorWindow(this, startupActive);
    }
}

