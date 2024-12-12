namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Input;

    public class RecentFileViewModel : BindableBase
    {
        private string name;
        private object documentSource;
        private ICommand command;
        private Uri smallGlyph;

        public RecentFileViewModel()
        {
            this.SmallGlyph = DevExpress.Xpf.DocumentViewer.UriHelper.GetUri(Assembly.GetExecutingAssembly().GetName().Name, @"\Images\RecentDocument_16x16.png");
        }

        public bool Equals(RecentFileViewModel obj) => 
            Equals(obj.DocumentSource, this.DocumentSource);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            RecentFileViewModel model = obj as RecentFileViewModel;
            return ((model != null) ? this.Equals(model) : false);
        }

        public override int GetHashCode() => 
            this.DocumentSource.GetHashCode();

        public Uri SmallGlyph
        {
            get => 
                this.smallGlyph;
            set => 
                base.SetProperty<Uri>(ref this.smallGlyph, value, Expression.Lambda<Func<Uri>>(Expression.Property(Expression.Constant(this, typeof(RecentFileViewModel)), (MethodInfo) methodof(RecentFileViewModel.get_SmallGlyph)), new ParameterExpression[0]));
        }

        public ICommand Command
        {
            get => 
                this.command;
            set => 
                base.SetProperty<ICommand>(ref this.command, value, Expression.Lambda<Func<ICommand>>(Expression.Property(Expression.Constant(this, typeof(RecentFileViewModel)), (MethodInfo) methodof(RecentFileViewModel.get_Command)), new ParameterExpression[0]));
        }

        public string Name
        {
            get => 
                this.name;
            set => 
                base.SetProperty<string>(ref this.name, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(RecentFileViewModel)), (MethodInfo) methodof(RecentFileViewModel.get_Name)), new ParameterExpression[0]));
        }

        public object DocumentSource
        {
            get => 
                this.documentSource;
            set => 
                base.SetProperty<object>(ref this.documentSource, value, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(RecentFileViewModel)), (MethodInfo) methodof(RecentFileViewModel.get_DocumentSource)), new ParameterExpression[0]));
        }
    }
}

