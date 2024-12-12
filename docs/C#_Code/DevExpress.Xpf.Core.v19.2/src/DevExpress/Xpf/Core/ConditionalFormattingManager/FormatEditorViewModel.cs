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

    public class FormatEditorViewModel : ManagerViewModelBase
    {
        protected FormatEditorViewModel(IDialogContext context, bool allowTextDecorations) : base(context)
        {
            FormatEditorFontViewModel model = FormatEditorFontViewModel.Factory(context);
            model.AllowTextDecorations = allowTextDecorations;
            FormatEditorItemViewModel[] modelArray1 = new FormatEditorItemViewModel[] { model, FormatEditorFillViewModel.Factory(context), FormatEditorIconViewModel.Factory(context) };
            this.ViewModels = modelArray1;
        }

        [Command(false)]
        public Format CreateFormat()
        {
            Format format = new Format();
            bool flag = false;
            foreach (FormatEditorItemViewModel model in this.ViewModels)
            {
                if (model.HasChanged)
                {
                    flag = true;
                    model.SetFormatProperties(format);
                }
            }
            return (flag ? format : null);
        }

        [Command(false)]
        public void Init(Format format)
        {
            if (format != null)
            {
                foreach (FormatEditorItemViewModel model in this.ViewModels)
                {
                    model.InitFromFormat(format);
                }
            }
        }

        public static Func<IDialogContext, bool, FormatEditorViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                ParameterExpression expression2 = Expression.Parameter(typeof(bool), "allowTextDecorations");
                Expression[] expressionArray1 = new Expression[] { expression, expression2 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
                return ViewModelSource.Factory<IDialogContext, bool, FormatEditorViewModel>(Expression.Lambda<Func<IDialogContext, bool, FormatEditorViewModel>>(Expression.New((ConstructorInfo) methodof(FormatEditorViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public IEnumerable<FormatEditorItemViewModel> ViewModels { get; private set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_FormatCells);
    }
}

