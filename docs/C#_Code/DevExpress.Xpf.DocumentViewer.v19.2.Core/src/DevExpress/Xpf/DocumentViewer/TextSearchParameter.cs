namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class TextSearchParameter : BindableBase
    {
        private string text;
        private bool wholeWord;
        private bool isCaseSensitive;
        private TextSearchDirection searchSearchDirection;
        private int currentPage;

        public TextSearchParameter()
        {
            this.CurrentPage = 1;
            this.SearchDirection = TextSearchDirection.Forward;
        }

        public string Text
        {
            get => 
                this.text;
            set => 
                base.SetProperty<string>(ref this.text, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TextSearchParameter)), (MethodInfo) methodof(TextSearchParameter.get_Text)), new ParameterExpression[0]));
        }

        public bool WholeWord
        {
            get => 
                this.wholeWord;
            set => 
                base.SetProperty<bool>(ref this.wholeWord, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(TextSearchParameter)), (MethodInfo) methodof(TextSearchParameter.get_WholeWord)), new ParameterExpression[0]));
        }

        public bool IsCaseSensitive
        {
            get => 
                this.isCaseSensitive;
            set => 
                base.SetProperty<bool>(ref this.isCaseSensitive, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(TextSearchParameter)), (MethodInfo) methodof(TextSearchParameter.get_IsCaseSensitive)), new ParameterExpression[0]));
        }

        public TextSearchDirection SearchDirection
        {
            get => 
                this.searchSearchDirection;
            set => 
                base.SetProperty<TextSearchDirection>(ref this.searchSearchDirection, value, Expression.Lambda<Func<TextSearchDirection>>(Expression.Property(Expression.Constant(this, typeof(TextSearchParameter)), (MethodInfo) methodof(TextSearchParameter.get_SearchDirection)), new ParameterExpression[0]));
        }

        public int CurrentPage
        {
            get => 
                this.currentPage;
            set => 
                base.SetProperty<int>(ref this.currentPage, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(TextSearchParameter)), (MethodInfo) methodof(TextSearchParameter.get_CurrentPage)), new ParameterExpression[0]));
        }
    }
}

