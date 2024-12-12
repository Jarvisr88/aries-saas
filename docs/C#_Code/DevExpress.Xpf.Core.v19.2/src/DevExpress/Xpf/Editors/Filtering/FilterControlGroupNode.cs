namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class FilterControlGroupNode : FilterControlNodeBase
    {
        public FilterControlGroupNode()
        {
            this.SetDefaultStyleKey(typeof(FilterControlGroupNode));
            this.ShowOnlyCommandMenu = false;
            this.AddCondtionNodeCommand = DelegateCommandFactory.Create<object>(obj => this.AddCondtionNode(), false);
            this.PressCommandButtonCommand = DelegateCommandFactory.Create<object>(obj => this.PressCommandButton(), false);
            this.ChangeTypeNodeCommand = DelegateCommandFactory.Create<GroupType>(groupType => this.ChangeTypeNode(groupType), false);
            this.AddGroupNodeCommand = DelegateCommandFactory.Create<object>(obj => this.AddGroupNode(), false);
            this.RemoveNodeCommand = DelegateCommandFactory.Create<object>(obj => this.RemoveNode(), false);
            this.ClearAllNodesCommand = DelegateCommandFactory.Create<object>(obj => this.ClearAllNodes(), false);
        }

        private void AddCommandsItems(ContentControl button)
        {
            if ((this.GroupNode.Owner.AllowedGroupFilters != AllowedGroupFilters.None) || (this.GroupNode.SubNodes.Count == 0))
            {
                base.AddItemToPopupMenu(button, "AddCondtion", EditorLocalizer.GetString(EditorStringId.FilterGroupAddCondition), this.AddCondtionNodeCommand, null, "AddCondition");
            }
            if (!base.Node.Owner.SupportDomainDataSource && (this.GroupNode.Owner.AllowedGroupFilters != AllowedGroupFilters.None))
            {
                base.AddItemToPopupMenu(button, "AddGroup", EditorLocalizer.GetString(EditorStringId.FilterGroupAddGroup), this.AddGroupNodeCommand, null, "AddGroup");
            }
            if ((this.GroupNode.Owner.AllowedGroupFilters != AllowedGroupFilters.None) || (this.GroupNode.SubNodes.Count == 0))
            {
                base.AddSeparatorToPopupMenu();
            }
            if (this.GroupNode.ParentNode != null)
            {
                base.AddItemToPopupMenu(button, "RemoveGroup", EditorLocalizer.GetString(EditorStringId.FilterGroupRemoveGroup), this.RemoveNodeCommand, null, "RemoveGroup");
            }
            else
            {
                base.AddItemToPopupMenu(button, "ClearAll", EditorLocalizer.GetString(EditorStringId.FilterGroupClearAll), this.ClearAllNodesCommand, null, "ClearAll");
            }
        }

        internal void AddCondtionNode()
        {
            ClauseNode node = this.GroupNode.Owner.AddClauseNode(this.GroupNode);
            if (node != null)
            {
                this.GroupNode.Owner.FocusNodeChild(node, 0);
            }
            this.UpdateAddButtonVisibility();
        }

        internal void AddGroupNode()
        {
            NodeBase node = this.GroupNode.Owner.AddGroup(this.GroupNode);
            if (node != null)
            {
                this.GroupNode.Owner.FocusNodeChild(node, 0);
            }
        }

        protected override void AddItemsToPopupMenu(ContentControl button)
        {
            base.AddItemsToPopupMenu(button);
            if (!this.ShowGroupCommandsIcon || (this.ShowGroupCommandsIcon && !this.ShowOnlyCommandMenu))
            {
                this.AddTypeItems(button);
            }
            if (!this.ShowGroupCommandsIcon && (this.GroupNode.Owner.AllowedGroupFilters != AllowedGroupFilters.None))
            {
                base.AddSeparatorToPopupMenu();
            }
            if (!this.ShowGroupCommandsIcon || (this.ShowGroupCommandsIcon && this.ShowOnlyCommandMenu))
            {
                this.AddCommandsItems(button);
            }
            this.ShowOnlyCommandMenu = false;
        }

        private void AddTypeItems(ContentControl button)
        {
            base.AddGroupItemToPopupMenu(button, "And", EditorLocalizer.GetString(EditorStringId.FilterGroupAnd), this.ChangeTypeNodeCommand, GroupType.And, "And");
            base.AddGroupItemToPopupMenu(button, "Or", EditorLocalizer.GetString(EditorStringId.FilterGroupOr), this.ChangeTypeNodeCommand, GroupType.Or, "Or");
            if (!base.Node.Owner.SupportDomainDataSource)
            {
                base.AddGroupItemToPopupMenu(button, "NotAnd", EditorLocalizer.GetString(EditorStringId.FilterGroupNotAnd), this.ChangeTypeNodeCommand, GroupType.NotAnd, "NotAnd");
                base.AddGroupItemToPopupMenu(button, "NotOr", EditorLocalizer.GetString(EditorStringId.FilterGroupNotOr), this.ChangeTypeNodeCommand, GroupType.NotOr, "NotOr");
            }
        }

        private void ChangeTypeNode(GroupType groupType)
        {
            this.GroupNode.NodeType = groupType;
        }

        private void ClearAllNodes()
        {
            this.GroupNode.Owner.ClearAll();
            this.UpdateAddButtonVisibility();
        }

        protected override void NavigationProcessKeyDown(KeyEventArgs e, UIElement focusedChild)
        {
            if (!FilterControlKeyboardHelper.IsAddKey(e))
            {
                base.NavigationProcessKeyDown(e, focusedChild);
            }
            else
            {
                this.AddCondtionNode();
                e.Handled = true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.RemoveButtonMouseUpEventHandler(this.TypeButton);
            this.TypeButton = base.GetTemplateChild("PART_TypeControl") as ContentControl;
            this.TypeButton.AddMouseUpHandler(new MouseButtonEventHandler(this.ButtonMouseUp));
            this.AddButton = base.GetTemplateChild("PART_AddButton") as Button;
            this.AddButton.Command = this.AddCondtionNodeCommand;
            this.UpdateAddButtonVisibility();
            this.CommandsButton = base.GetTemplateChild("PART_GroupCommandsButton") as Button;
        }

        private void PressCommandButton()
        {
            this.ShowOnlyCommandMenu = true;
            base.ButtonMouseUp(this.TypeButton, null);
        }

        private void RemoveNode()
        {
            this.GroupNode.Owner.RemoveNode(this.GroupNode);
            this.UpdateAddButtonVisibility();
        }

        internal void UpdateAddButtonVisibility()
        {
            if (this.GroupNode == null)
            {
                this.AddButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.AddButton.Visibility = ((this.GroupNode.Owner.AllowedGroupFilters != AllowedGroupFilters.None) || (this.GroupNode.SubNodes.Count == 0)) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        internal DevExpress.Xpf.Editors.Filtering.GroupNode GroupNode =>
            base.Node as DevExpress.Xpf.Editors.Filtering.GroupNode;

        private ContentControl TypeButton { get; set; }

        private Button AddButton { get; set; }

        private Button CommandsButton { get; set; }

        private bool ShowGroupCommandsIcon =>
            this.GroupNode.Owner.ShowGroupCommandsIcon;

        private bool ShowOnlyCommandMenu { get; set; }

        public ICommand AddCondtionNodeCommand { get; private set; }

        public ICommand PressCommandButtonCommand { get; private set; }

        public ICommand ChangeTypeNodeCommand { get; private set; }

        public ICommand AddGroupNodeCommand { get; private set; }

        public ICommand RemoveNodeCommand { get; private set; }

        public ICommand ClearAllNodesCommand { get; private set; }

        protected override IList<UIElement> NavigationChildrenCore
        {
            get
            {
                List<UIElement> list = new List<UIElement>();
                if (this.TypeButton != null)
                {
                    list.Add(this.TypeButton);
                }
                return list;
            }
        }

        protected override IList<IFilterControlNavigationNode> NavigationSubNodes
        {
            get
            {
                List<IFilterControlNavigationNode> list2 = new List<IFilterControlNavigationNode>();
                foreach (INode node in this.GroupNode.SubNodes)
                {
                    list2.Add(((NodeBase) node).VisualNode);
                }
                return list2;
            }
        }
    }
}

