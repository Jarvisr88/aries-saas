namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;

    public class LayoutGroupInfo : ILayoutElementGenerator
    {
        public LayoutGroupInfo(string name, GroupView? view, System.Windows.Controls.Orientation? orientation)
        {
            this.Children = new List<ILayoutElementGenerator>();
            this.Name = name;
            this.View = view;
            this.Orientation = orientation;
        }

        protected LayoutGroupInfo CreateGroupInfo(string name, GroupView? view, System.Windows.Controls.Orientation? orientation)
        {
            Type[] types = new Type[] { typeof(string), typeof(GroupView?), typeof(System.Windows.Controls.Orientation?) };
            object[] parameters = new object[] { name, view, orientation };
            return (LayoutGroupInfo) base.GetType().GetConstructor(types).Invoke(parameters);
        }

        void ILayoutElementGenerator.CreateElement(ILayoutElementFactory factory)
        {
            factory.CreateGroup(this);
        }

        protected LayoutGroupInfo FindGroupInfo(string name) => 
            (LayoutGroupInfo) this.Children.FirstOrDefault<ILayoutElementGenerator>(c => ((c is LayoutGroupInfo) && (((LayoutGroupInfo) c).Name == name)));

        protected LayoutGroupInfo FindOrCreateGroupInfo(string name, GroupView? view, System.Windows.Controls.Orientation? orientation)
        {
            LayoutGroupInfo item = this.FindGroupInfo(name);
            if (item == null)
            {
                item = this.CreateGroupInfo(name, view, orientation);
                item.Parent = this;
                this.Children.Add(item);
            }
            return item;
        }

        protected virtual System.Windows.Controls.Orientation GetDefaultOrientation() => 
            ((this.GetView() != GroupView.Group) || (this.Parent.GetView() == GroupView.Tabs)) ? System.Windows.Controls.Orientation.Vertical : this.Parent.GetOrientation().OrthogonalValue();

        protected virtual GroupView GetDefaultView() => 
            (this.Parent.GetView() != GroupView.Tabs) ? GroupView.GroupBox : GroupView.Group;

        public static string[] GetGroupDescriptions(string path)
        {
            char[] separator = new char[] { LayoutGroupInfoConstants.GroupPathSeparator };
            return path.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public LayoutGroupInfo GetGroupInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return this;
            }
            LayoutGroupInfo info = this;
            string[] groupDescriptions = GetGroupDescriptions(path);
            for (int i = 0; i < groupDescriptions.Length; i++)
            {
                string str;
                GroupView? nullable;
                System.Windows.Controls.Orientation? nullable2;
                this.GetGroupParameters(groupDescriptions[i], out str, out nullable, out nullable2);
                info = info.FindOrCreateGroupInfo(str, nullable, nullable2);
            }
            return info;
        }

        protected virtual System.Windows.Controls.Orientation? GetGroupOrientation(ref string description)
        {
            System.Windows.Controls.Orientation? nullable = null;
            if (description.Length != 0)
            {
                char ch = description[description.Length - 1];
                if (ch == LayoutGroupInfoConstants.HorizontalGroupMark)
                {
                    nullable = new System.Windows.Controls.Orientation?(System.Windows.Controls.Orientation.Horizontal);
                }
                else if (ch == LayoutGroupInfoConstants.VerticalGroupMark)
                {
                    nullable = new System.Windows.Controls.Orientation?(System.Windows.Controls.Orientation.Vertical);
                }
            }
            if (nullable != null)
            {
                description = description.Substring(0, description.Length - 1);
            }
            return nullable;
        }

        protected virtual void GetGroupParameters(string description, out string name, out GroupView? view, out System.Windows.Controls.Orientation? orientation)
        {
            description = description.Trim();
            view = this.GetGroupView(ref description);
            orientation = this.GetGroupOrientation(ref description);
            name = description;
        }

        protected virtual GroupView? GetGroupView(ref string description)
        {
            GroupView? nullable = null;
            if (description.Length >= 2)
            {
                char ch = description[0];
                char ch2 = description[description.Length - 1];
                if ((ch == LayoutGroupInfoConstants.BorderlessGroupMarkStart) && (ch2 == LayoutGroupInfoConstants.BorderlessGroupMarkEnd))
                {
                    nullable = new GroupView?(GroupView.Group);
                }
                else if ((ch == LayoutGroupInfoConstants.GroupBoxMarkStart) && (ch2 == LayoutGroupInfoConstants.GroupBoxMarkEnd))
                {
                    nullable = new GroupView?(GroupView.GroupBox);
                }
                else if ((ch == LayoutGroupInfoConstants.TabbedGroupMarkStart) && (ch2 == LayoutGroupInfoConstants.TabbedGroupMarkEnd))
                {
                    nullable = new GroupView?(GroupView.Tabs);
                }
            }
            if (nullable != null)
            {
                description = description.Substring(1, description.Length - 2);
            }
            return nullable;
        }

        public System.Windows.Controls.Orientation GetOrientation()
        {
            System.Windows.Controls.Orientation? orientation = this.Orientation;
            return ((orientation != null) ? orientation.GetValueOrDefault() : this.GetDefaultOrientation());
        }

        public GroupView GetView()
        {
            GroupView? view = this.View;
            return ((view != null) ? view.GetValueOrDefault() : this.GetDefaultView());
        }

        public List<ILayoutElementGenerator> Children { get; private set; }

        public string Name { get; private set; }

        public System.Windows.Controls.Orientation? Orientation { get; private set; }

        public LayoutGroupInfo Parent { get; private set; }

        public GroupView? View { get; private set; }
    }
}

