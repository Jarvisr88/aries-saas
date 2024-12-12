namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FormatEditorFillViewModel : FormatEditorItemViewModel
    {
        protected FormatEditorFillViewModel(IDialogContext column) : base(column)
        {
        }

        public override void Clear()
        {
            this.Background = null;
            base.HasChanged = false;
        }

        public override void InitFromFormat(Format format)
        {
            Color? brushColor = base.GetBrushColor(format.Background);
            if (brushColor != null)
            {
                this.Background = new Color?(brushColor.Value);
            }
        }

        public override void SetFormatProperties(Format format)
        {
            ManagerHelperBase.SetProperty(format, Format.BackgroundProperty, base.CreateBrush(this.Background));
        }

        public static Func<IDialogContext, FormatEditorFillViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, FormatEditorFillViewModel>(Expression.Lambda<Func<IDialogContext, FormatEditorFillViewModel>>(Expression.New((ConstructorInfo) methodof(FormatEditorFillViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual Color? Background { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Fill);
    }
}

