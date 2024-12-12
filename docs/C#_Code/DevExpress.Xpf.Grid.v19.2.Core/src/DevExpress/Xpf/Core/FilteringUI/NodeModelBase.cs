namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using System;
    using System.Windows.Input;

    public abstract class NodeModelBase : ViewModelBase
    {
        internal NodeModelBase()
        {
        }

        internal DevExpress.Xpf.Core.FilteringUI.INavigationService GetNavigationService() => 
            this.GetService<DevExpress.Xpf.Core.FilteringUI.INavigationService>();

        internal T Match<T>(Func<GroupNodeModel, T> group, Func<NodeModelBase, T> otherwise) => 
            this.Match<T>(l => otherwise(l), g => group(g), ce => otherwise(ce));

        internal T Match<T>(Func<LeafNodeModel, T> leaf, Func<GroupNodeModel, T> group, Func<CustomExpressionNodeModel, T> customExpression)
        {
            LeafNodeModel arg = this as LeafNodeModel;
            if (arg != null)
            {
                return leaf(arg);
            }
            GroupNodeModel model2 = this as GroupNodeModel;
            if (model2 != null)
            {
                return group(model2);
            }
            CustomExpressionNodeModel model3 = this as CustomExpressionNodeModel;
            if (model3 == null)
            {
                throw new InvalidOperationException();
            }
            return customExpression(model3);
        }

        public abstract ICommand RemoveCommand { get; }
    }
}

