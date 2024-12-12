namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Media;

    public static class ApplicationJumpListExtensions
    {
        public static ApplicationJumpPathInfo Add(this IApplicationJumpList jumpList, string jumpPath)
        {
            ApplicationJumpPathInfo info;
            try
            {
                info = jumpList.Add(null, jumpPath);
            }
            catch (InvalidApplicationJumpItemException exception)
            {
                throw new InvalidOperationException("", exception);
            }
            return info;
        }

        public static ApplicationJumpPathInfo Add(this IApplicationJumpList jumpList, string customCategory, string jumpPath)
        {
            if (jumpList == null)
            {
                throw new ArgumentNullException("jumpList");
            }
            ApplicationJumpPathInfo info1 = new ApplicationJumpPathInfo();
            info1.Path = jumpPath;
            info1.CustomCategory = customCategory;
            ApplicationJumpPathInfo item = info1;
            try
            {
                jumpList.Add(item);
            }
            catch (InvalidApplicationJumpItemException exception)
            {
                throw new InvalidOperationException("", exception);
            }
            return item;
        }

        public static ApplicationJumpTaskInfo Add(this IApplicationJumpList jumpList, string title, ImageSource icon, Action action, string commandId = null) => 
            jumpList.Add(null, title, icon, null, action, commandId);

        public static ApplicationJumpTaskInfo Add(this IApplicationJumpList jumpList, string customCategory, string title, ImageSource icon, Action action, string commandId = null) => 
            jumpList.Add(customCategory, title, icon, null, action, commandId);

        public static ApplicationJumpTaskInfo Add(this IApplicationJumpList jumpList, string title, ImageSource icon, string description, Action action, string commandId = null) => 
            jumpList.Add(null, title, icon, description, action, commandId);

        public static ApplicationJumpTaskInfo Add(this IApplicationJumpList jumpList, string customCategory, string title, ImageSource icon, string description, Action action, string commandId = null)
        {
            if (jumpList == null)
            {
                throw new ArgumentNullException("jumpList");
            }
            ApplicationJumpTaskInfo info1 = new ApplicationJumpTaskInfo();
            info1.CustomCategory = customCategory;
            info1.Title = title;
            info1.Icon = icon;
            info1.Description = description;
            info1.CommandId = commandId;
            info1.Action = action;
            ApplicationJumpTaskInfo item = info1;
            try
            {
                jumpList.Add(item);
            }
            catch (ApplicationJumpTaskInvalidIconException exception)
            {
                throw new ArgumentException("", "icon", exception);
            }
            catch (InvalidApplicationJumpItemException exception2)
            {
                throw new InvalidOperationException("", exception2);
            }
            return item;
        }

        public static ApplicationJumpTaskInfo AddOrReplace(this IApplicationJumpList jumpList, string title, ImageSource icon, Action action, string commandId = null) => 
            jumpList.AddOrReplace(null, title, icon, null, action, commandId);

        public static ApplicationJumpTaskInfo AddOrReplace(this IApplicationJumpList jumpList, string customCategory, string title, ImageSource icon, Action action, string commandId = null) => 
            jumpList.AddOrReplace(customCategory, title, icon, null, action, commandId);

        public static ApplicationJumpTaskInfo AddOrReplace(this IApplicationJumpList jumpList, string title, ImageSource icon, string description, Action action, string commandId = null) => 
            jumpList.AddOrReplace(null, title, icon, description, action, commandId);

        public static ApplicationJumpTaskInfo AddOrReplace(this IApplicationJumpList jumpList, string customCategory, string title, ImageSource icon, string description, Action action, string commandId = null)
        {
            if (jumpList == null)
            {
                throw new ArgumentNullException("jumpList");
            }
            ApplicationJumpTaskInfo info1 = new ApplicationJumpTaskInfo();
            info1.CustomCategory = customCategory;
            info1.Title = title;
            info1.Icon = icon;
            info1.Description = description;
            info1.CommandId = commandId;
            info1.Action = action;
            ApplicationJumpTaskInfo jumpTask = info1;
            try
            {
                jumpList.AddOrReplace(jumpTask);
            }
            catch (ApplicationJumpTaskInvalidIconException exception)
            {
                throw new ArgumentException("", "icon", exception);
            }
            catch (InvalidApplicationJumpItemException exception2)
            {
                throw new InvalidOperationException("", exception2);
            }
            return jumpTask;
        }

        public static void AddRange(this IApplicationJumpList jumpList, IEnumerable<ApplicationJumpItemInfo> jumpItems)
        {
            if (jumpList == null)
            {
                throw new ArgumentNullException("jumpList");
            }
            List<Exception> list = null;
            foreach (ApplicationJumpItemInfo info in jumpItems)
            {
                try
                {
                    jumpList.Add(info);
                }
                catch (InvalidApplicationJumpItemException exception)
                {
                    if (list == null)
                    {
                        list = new List<Exception>();
                    }
                    list.Add(exception);
                }
            }
            if (list != null)
            {
                throw new InvalidOperationException("", new AggregateException(list.ToArray()));
            }
        }
    }
}

