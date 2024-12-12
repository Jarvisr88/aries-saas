namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabHeadersPanel : BaseHeadersPanel
    {
        public static readonly DependencyProperty AllowChildrenMeasureProperty;
        private TabbedPaneHeaderLayoutCalculator calculatorCore;

        static TabHeadersPanel()
        {
            new DependencyPropertyRegistrator<TabHeadersPanel>().Register<bool>("AllowChildrenMeasure", ref AllowChildrenMeasureProperty, true, (dObj, e) => ((TabHeadersPanel) dObj).OnAllowChildrenMeasureChanged((bool) e.OldValue, (bool) e.NewValue), null);
        }

        protected override ITabHeaderLayoutResult Measure(ITabHeaderLayoutCalculator calculator, ITabHeaderLayoutOptions options)
        {
            if (!this.AllowChildrenMeasure)
            {
                this.Calculator.InnerCalculator = calculator;
                calculator = this.Calculator;
            }
            return base.Measure(calculator, options);
        }

        protected virtual void OnAllowChildrenMeasureChanged(bool oldValue, bool newValue)
        {
            this.Calculator.AllowChildrenMeasure = newValue;
        }

        public bool AllowChildrenMeasure
        {
            get => 
                (bool) base.GetValue(AllowChildrenMeasureProperty);
            set => 
                base.SetValue(AllowChildrenMeasureProperty, value);
        }

        private TabbedPaneHeaderLayoutCalculator Calculator
        {
            get
            {
                TabbedPaneHeaderLayoutCalculator calculatorCore = this.calculatorCore;
                if (this.calculatorCore == null)
                {
                    TabbedPaneHeaderLayoutCalculator local1 = this.calculatorCore;
                    calculatorCore = this.calculatorCore = new TabbedPaneHeaderLayoutCalculator();
                }
                return calculatorCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabHeadersPanel.<>c <>9 = new TabHeadersPanel.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TabHeadersPanel) dObj).OnAllowChildrenMeasureChanged((bool) e.OldValue, (bool) e.NewValue);
            }
        }

        private class TabbedPaneHeaderLayoutCalculator : ITabHeaderLayoutCalculator
        {
            private static readonly ITabHeaderLayoutResult EmptyLayoutResult = new EmptyTabHeaderLayoutResult();

            public ITabHeaderLayoutResult Calc(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options)
            {
                ITabHeaderLayoutResult emptyLayoutResult = this.InnerCalculator.Calc(headers, options);
                if (!this.AllowChildrenMeasure)
                {
                    ITabHeaderInfo[] infoArray = headers;
                    int index = 0;
                    while (true)
                    {
                        if (index >= infoArray.Length)
                        {
                            emptyLayoutResult = EmptyLayoutResult;
                            break;
                        }
                        ITabHeaderInfo info = infoArray[index];
                        info.IsVisible = false;
                        index++;
                    }
                }
                return emptyLayoutResult;
            }

            public ITabHeaderLayoutCalculator InnerCalculator { get; set; }

            public bool AllowChildrenMeasure { get; set; }

            private class EmptyTabHeaderLayoutResult : ITabHeaderLayoutResult
            {
                public bool HasScroll =>
                    false;

                public Rect[] Headers =>
                    new Rect[0];

                public bool IsEmpty =>
                    true;

                public IScrollResult ScrollResult =>
                    null;

                public System.Windows.Size Size =>
                    new System.Windows.Size();
            }
        }
    }
}

