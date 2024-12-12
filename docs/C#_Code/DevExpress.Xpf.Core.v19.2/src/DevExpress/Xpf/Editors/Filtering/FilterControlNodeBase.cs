namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class FilterControlNodeBase : Control, IFilterControlNavigationNode
    {
        private static ThreadLocal<HashSet<string>> svgIconsNames = new ThreadLocal<HashSet<string>>(delegate {
            HashSet<string> set1 = new HashSet<string>();
            set1.Add("anyof");
            set1.Add("beginswith");
            set1.Add("between");
            set1.Add("contains");
            set1.Add("doesnotcontain");
            set1.Add("doesnotequal");
            set1.Add("endswith");
            set1.Add("equals");
            set1.Add("greater");
            set1.Add("greaterorequal");
            set1.Add("isnotnullorempty");
            set1.Add("isnullorempty");
            set1.Add("less");
            set1.Add("lessorequal");
            set1.Add("like");
            set1.Add("noneof");
            set1.Add("notbetween");
            set1.Add("notlike");
            return set1;
        });

        public FilterControlNodeBase()
        {
            this.ButtonMenu = new PopupMenu();
            this.ButtonMenu.IsBranchHeader = true;
            this.ButtonMenu.ItemClickBehaviour = PopupItemClickBehaviour.CloseCurrentBranch;
            BarNameScope.SetIsScopeOwner(this.ButtonMenu, true);
        }

        protected BarButtonItem AddGroupItemToPopupMenu(ContentControl button, string name, object content, ICommand command, GroupType parameter, string imageName)
        {
            switch (parameter)
            {
                case GroupType.And:
                    if ((this.Node.Owner.AllowedGroupFilters & AllowedGroupFilters.And) == AllowedGroupFilters.And)
                    {
                        break;
                    }
                    return null;

                case GroupType.Or:
                    if ((this.Node.Owner.AllowedGroupFilters & AllowedGroupFilters.Or) == AllowedGroupFilters.Or)
                    {
                        break;
                    }
                    return null;

                case GroupType.NotAnd:
                    if ((this.Node.Owner.AllowedGroupFilters & AllowedGroupFilters.NotAnd) == AllowedGroupFilters.NotAnd)
                    {
                        break;
                    }
                    return null;

                case GroupType.NotOr:
                    if ((this.Node.Owner.AllowedGroupFilters & AllowedGroupFilters.NotOr) == AllowedGroupFilters.NotOr)
                    {
                        break;
                    }
                    return null;

                default:
                    break;
            }
            return this.AddItemToPopupMenu(button, name, content, command, parameter, imageName);
        }

        protected virtual void AddItemsToPopupMenu(ContentControl button)
        {
        }

        protected BarButtonItem AddItemToPopupMenu(ContentControl button, string name, object content, ICommand command, object parameter, string imageName) => 
            this.AddItemToPopupMenu(button, name, content, null, null, command, parameter, imageName);

        protected BarButtonItem AddItemToPopupMenu(ILinksHolder menu, ContentControl button, string name, object content, ICommand command, object parameter, string imageName) => 
            this.AddItemToPopupMenu(menu, button, name, content, null, null, command, parameter, imageName);

        protected BarButtonItem AddItemToPopupMenu(ContentControl button, string name, object content, DataTemplate template, DataTemplateSelector templateSelector, ICommand command, object parameter, string imageName) => 
            this.AddItemToPopupMenu(this.ButtonMenu, button, name, content, template, templateSelector, command, parameter, imageName);

        protected BarButtonItem AddItemToPopupMenu(ILinksHolder menu, ContentControl button, string name, object content, DataTemplate template, DataTemplateSelector templateSelector, ICommand command, object parameter, string imageName)
        {
            BarButtonItem item1 = new BarButtonItem();
            item1.Name = name;
            item1.IsPrivate = true;
            item1.CommandParameter = parameter;
            item1.Command = command;
            item1.Content = content;
            item1.ContentTemplate = template;
            BarButtonItem item = item1;
            item.ContentTemplateSelector = templateSelector;
            if (!string.IsNullOrEmpty(imageName))
            {
                item.Glyph = GetImage(imageName);
            }
            if (ReferenceEquals(menu, this.ButtonMenu))
            {
                menu.Items.Add(item);
            }
            else
            {
                menu.Items.Add(item);
            }
            return item;
        }

        protected BarItemLinkSeparator AddSeparatorToPopupMenu() => 
            this.AddSeparatorToPopupMenu(this.ButtonMenu);

        protected BarItemLinkSeparator AddSeparatorToPopupMenu(ILinksHolder menu)
        {
            BarItemLinkSeparator item = new BarItemLinkSeparator();
            menu.Links.Add(item);
            return item;
        }

        protected BarSubItem AddSubMenuToPopupMenu(ContentControl button, string name, object content)
        {
            BarSubItem item1 = new BarSubItem();
            item1.Name = name;
            item1.Content = content;
            item1.IsPrivate = true;
            BarSubItem item = item1;
            this.ButtonMenu.Items.Add(item);
            return item;
        }

        internal void ButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            ContentControl button = sender as ContentControl;
            button.Focus();
            try
            {
                if (this.ClosePopup())
                {
                    return;
                }
            }
            finally
            {
                this.ButtonMenu.Items.Clear();
            }
            this.CreatePopupMenu(button);
            this.ButtonMenu.Opened += new EventHandler(this.MenuOpened);
            this.ButtonMenu.Closed += new EventHandler(this.MenuClosed);
            this.ButtonMenu.ShowPopup(button);
        }

        protected bool ClosePopup()
        {
            if (!this.ButtonMenu.IsOpen)
            {
                return false;
            }
            this.ButtonMenu.ClosePopup();
            return true;
        }

        protected void CreatePopupMenu(ContentControl button)
        {
            if ((this.Node != null) && ((this.Node.Owner != null) && (this.Node.Owner.FilterColumns != null)))
            {
                this.AddItemsToPopupMenu(button);
            }
        }

        void IFilterControlNavigationNode.ProcessKeyDown(KeyEventArgs e, UIElement focusedChild)
        {
            this.NavigationProcessKeyDown(e, focusedChild);
        }

        bool IFilterControlNavigationNode.ShowPopupMenu(UIElement child) => 
            this.NavigationShowPopupMenu(child);

        public static ImageSource GetImage(string imageName)
        {
            if (!ApplicationThemeHelper.UseDefaultSvgImages || !svgIconsNames.Value.Contains(imageName.ToLower()))
            {
                return ImageHelper.CreateImageFromCoreEmbeddedResource("Editors.Images.FilterControl." + imageName + ".png");
            }
            SvgImageSourceExtension extension = new SvgImageSourceExtension {
                Uri = GetImageUri(imageName)
            };
            return (ImageSource) extension.ProvideValue(null);
        }

        public static Uri GetImageUri(string imageName) => 
            new Uri("pack://application:,,,/DevExpress.Xpf.Core.v19.2;component/Editors/Images/FilterControl/ClauseSvgImages/" + imageName + ".svg");

        internal void MenuClosed(object sender, EventArgs e)
        {
            this.ButtonMenu.Closed -= new EventHandler(this.MenuClosed);
            this.ButtonMenu.Items.Clear();
        }

        internal void MenuOpened(object sender, EventArgs e)
        {
            this.ButtonMenu.Opened -= new EventHandler(this.MenuOpened);
        }

        protected virtual void NavigationProcessKeyDown(KeyEventArgs e, UIElement focusedChild)
        {
            if (FilterControlKeyboardHelper.IsDeleteKey(e) && (this.NavigationParentNode != null))
            {
                this.Node.Owner.RemoveNode(this.Node);
                e.Handled = true;
            }
        }

        protected virtual bool NavigationShowPopupMenu(UIElement child)
        {
            this.ButtonMouseUp(child, null);
            return true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Node.SetVisualNode(this);
        }

        protected void RemoveButtonMouseUpEventHandler(ContentControl button)
        {
            if (button != null)
            {
                button.RemoveMouseUpHandler(new MouseButtonEventHandler(this.ButtonMouseUp));
            }
        }

        internal PopupMenu ButtonMenu { get; set; }

        public NodeBase Node =>
            base.DataContext as NodeBase;

        protected virtual IList<UIElement> NavigationChildrenCore =>
            new List<UIElement>();

        protected IList<UIElement> NavigationChildren
        {
            get
            {
                IList<UIElement> navigationChildrenCore = this.NavigationChildrenCore;
                for (int i = navigationChildrenCore.Count - 1; i >= 0; i--)
                {
                    if (!UIElementHelper.IsVisible(navigationChildrenCore[i]))
                    {
                        navigationChildrenCore.RemoveAt(i);
                    }
                }
                return navigationChildrenCore;
            }
        }

        protected virtual IFilterControlNavigationNode NavigationParentNode
        {
            get
            {
                GroupNode parentNode = (GroupNode) this.Node.ParentNode;
                return parentNode?.VisualNode;
            }
        }

        protected virtual IList<IFilterControlNavigationNode> NavigationSubNodes =>
            new List<IFilterControlNavigationNode>();

        IList<UIElement> IFilterControlNavigationNode.Children =>
            this.NavigationChildren;

        IFilterControlNavigationNode IFilterControlNavigationNode.ParentNode =>
            this.NavigationParentNode;

        IList<IFilterControlNavigationNode> IFilterControlNavigationNode.SubNodes =>
            this.NavigationSubNodes;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilterControlNodeBase.<>c <>9 = new FilterControlNodeBase.<>c();

            internal HashSet<string> <.cctor>b__44_0()
            {
                HashSet<string> set1 = new HashSet<string>();
                set1.Add("anyof");
                set1.Add("beginswith");
                set1.Add("between");
                set1.Add("contains");
                set1.Add("doesnotcontain");
                set1.Add("doesnotequal");
                set1.Add("endswith");
                set1.Add("equals");
                set1.Add("greater");
                set1.Add("greaterorequal");
                set1.Add("isnotnullorempty");
                set1.Add("isnullorempty");
                set1.Add("less");
                set1.Add("lessorequal");
                set1.Add("like");
                set1.Add("noneof");
                set1.Add("notbetween");
                set1.Add("notlike");
                return set1;
            }
        }
    }
}

