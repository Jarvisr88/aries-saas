namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class DocumentViewModelHelper
    {
        public static object GetTitle(object documentContentOrDocumentViewModel)
        {
            IDocumentContent content = documentContentOrDocumentViewModel as IDocumentContent;
            if (content != null)
            {
                return content.Title;
            }
            IDocumentViewModel model = documentContentOrDocumentViewModel as IDocumentViewModel;
            if (model == null)
            {
                throw new ArgumentException("", "documentContentOrDocumentViewModel");
            }
            return model.Title;
        }

        public static bool IsDocumentContentOrDocumentViewModel(object viewModel) => 
            (viewModel is IDocumentContent) || (viewModel is IDocumentViewModel);

        public static void OnClose(object documentContentOrDocumentViewModel, CancelEventArgs e)
        {
            IDocumentContent content = documentContentOrDocumentViewModel as IDocumentContent;
            if (content != null)
            {
                content.OnClose(e);
            }
            else
            {
                IDocumentViewModel model = documentContentOrDocumentViewModel as IDocumentViewModel;
                if (model != null)
                {
                    e.Cancel = !model.Close();
                }
            }
        }

        public static void OnDestroy(object documentContentOrDocumentViewModel)
        {
            IDocumentContent content = documentContentOrDocumentViewModel as IDocumentContent;
            if (content != null)
            {
                content.OnDestroy();
            }
        }

        public static bool TitlePropertyHasImplicitImplementation(object documentContentOrDocumentViewModel)
        {
            ParameterExpression expression;
            IDocumentContent content = documentContentOrDocumentViewModel as IDocumentContent;
            if (content != null)
            {
                expression = Expression.Parameter(typeof(IDocumentContent), "i");
                ParameterExpression[] expressionArray1 = new ParameterExpression[] { expression };
                return ExpressionHelper.PropertyHasImplicitImplementation<IDocumentContent, object>(content, Expression.Lambda<Func<IDocumentContent, object>>(Expression.Property(expression, (MethodInfo) methodof(IDocumentContent.get_Title)), expressionArray1), true);
            }
            IDocumentViewModel model = documentContentOrDocumentViewModel as IDocumentViewModel;
            if (model == null)
            {
                throw new ArgumentException("", "documentContentOrDocumentViewModel");
            }
            expression = Expression.Parameter(typeof(IDocumentViewModel), "i");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            return ExpressionHelper.PropertyHasImplicitImplementation<IDocumentViewModel, object>(model, Expression.Lambda<Func<IDocumentViewModel, object>>(Expression.Property(expression, (MethodInfo) methodof(IDocumentViewModel.get_Title)), parameters), true);
        }
    }
}

