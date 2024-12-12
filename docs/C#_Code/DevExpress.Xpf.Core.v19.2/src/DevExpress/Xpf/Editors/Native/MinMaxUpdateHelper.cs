namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class MinMaxUpdateHelper
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty MinValueProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty MaxValueProperty;

        public MinMaxUpdateHelper(BaseEdit editor, DependencyProperty minProperty, DependencyProperty maxProperty)
        {
            this.Editor = editor;
            this.MinValueProperty = minProperty;
            this.MaxValueProperty = maxProperty;
        }

        public void Update<T>(MinMaxUpdateSource updateSource) where T: struct, IComparable
        {
            if ((updateSource == MinMaxUpdateSource.MinChanged) && ((this.MaxValue != null) && (this.MinValue.CompareTo(this.MaxValue) > 0)))
            {
                this.MaxValue = this.MinValue;
            }
            if ((updateSource == MinMaxUpdateSource.MaxChanged) && ((this.MinValue != null) && (this.MaxValue.CompareTo(this.MinValue) < 0)))
            {
                this.MinValue = this.MaxValue;
            }
            if (updateSource == MinMaxUpdateSource.ISupportInitialize)
            {
                T local;
                if (this.MaxValue != null)
                {
                    local = default(T);
                    if (this.MaxValue.CompareTo(local) < 0)
                    {
                        this.MaxValue = ((this.MinValue == null) || (this.MinValue.CompareTo(this.MaxValue) <= 0)) ? this.MaxValue : this.MinValue;
                        goto TR_0004;
                    }
                }
                if (this.MinValue != null)
                {
                    local = default(T);
                    if (this.MinValue.CompareTo(local) > 0)
                    {
                        this.MinValue = ((this.MaxValue == null) || (this.MaxValue.CompareTo(this.MinValue) >= 0)) ? this.MinValue : this.MaxValue;
                    }
                }
            }
        TR_0004:
            if ((this.MinValue != null) && (!this.MinValue.IsInfinity && !Equals(this.MinValue.Value, this.Editor.GetValue(this.MinValueProperty))))
            {
                this.Editor.SetCurrentValue(this.MinValueProperty, this.MinValue.Value);
            }
            if ((this.MaxValue != null) && (!this.MaxValue.IsInfinity && !Equals(this.MaxValue.Value, this.Editor.GetValue(this.MaxValueProperty))))
            {
                this.Editor.SetCurrentValue(this.MaxValueProperty, this.MaxValue.Value);
            }
            this.Editor.EditStrategy.SyncWithValue();
            this.Editor.DoValidate();
        }

        private BaseEdit Editor { get; set; }

        public IComparableWrapper MinValue { get; set; }

        public IComparableWrapper MaxValue { get; set; }
    }
}

