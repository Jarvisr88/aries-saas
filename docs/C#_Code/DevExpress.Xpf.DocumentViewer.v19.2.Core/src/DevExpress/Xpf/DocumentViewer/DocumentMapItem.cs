namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class DocumentMapItem : BindableBase, IDocumentMapItem
    {
        private bool isExpanded;

        public DocumentMapItem(int id, int parentID, string title)
        {
            this.Id = id;
            this.ParentId = parentID;
            this.Title = title;
            this.Command = new DelegateCommand(new Action(this.CommandExecuted));
        }

        protected virtual void CommandExecuted()
        {
        }

        protected virtual void IsExpandedChanged()
        {
        }

        public int Id { get; private set; }

        public int ParentId { get; private set; }

        public string Title { get; private set; }

        public bool IsExpanded
        {
            get => 
                this.isExpanded;
            set => 
                base.SetProperty<bool>(ref this.isExpanded, value, "IsExpanded", new Action(this.IsExpandedChanged));
        }

        public ICommand Command { get; private set; }
    }
}

