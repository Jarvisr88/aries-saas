namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class IconItemViewModel
    {
        private IconViewModel owner;

        protected IconItemViewModel(IconViewModel owner)
        {
            this.owner = owner;
        }

        internal IconSetElement CreateElement()
        {
            IconSetElement element1 = new IconSetElement();
            element1.Threshold = this.Threshold;
            element1.ThresholdComparisonType = this.GetThresholdComparisonType();
            element1.Icon = this.Icon;
            return element1;
        }

        internal ThresholdComparisonType GetThresholdComparisonType()
        {
            IconComparisonType comparisonType = this.ComparisonType;
            if (comparisonType == IconComparisonType.GreaterOrEqual)
            {
                return ThresholdComparisonType.GreaterOrEqual;
            }
            if (comparisonType != IconComparisonType.Greater)
            {
                throw new InvalidOperationException();
            }
            return ThresholdComparisonType.Greater;
        }

        protected void OnIconChanged()
        {
            this.owner.SetCustomExportStyle();
        }

        protected void UpdateNextItemDescription()
        {
            this.owner.UpdateNextItemDescription(this);
        }

        public static Func<IconViewModel, IconItemViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IconViewModel), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IconViewModel, IconItemViewModel>(Expression.Lambda<Func<IconViewModel, IconItemViewModel>>(Expression.New((ConstructorInfo) methodof(IconItemViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual ImageSource Icon { get; set; }

        public virtual IEnumerable<ImageSource> Icons { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateNextItemDescription")]
        public virtual IconComparisonType ComparisonType { get; set; }

        [BindableProperty(OnPropertyChangedMethodName="UpdateNextItemDescription")]
        public virtual double Threshold { get; set; }

        public virtual string Description { get; set; }

        public bool HasBottomLimit =>
            !(this.Threshold == double.NegativeInfinity);
    }
}

