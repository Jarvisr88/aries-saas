namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class FormatEditorIconItemViewModel
    {
        protected FormatEditorIconItemViewModel(ImageSource icon)
        {
            this.Icon = icon;
        }

        public static Func<ImageSource, FormatEditorIconItemViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(ImageSource), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<ImageSource, FormatEditorIconItemViewModel>(Expression.Lambda<Func<ImageSource, FormatEditorIconItemViewModel>>(Expression.New((ConstructorInfo) methodof(FormatEditorIconItemViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public ImageSource Icon { get; private set; }

        public virtual bool IsChecked { get; set; }
    }
}

