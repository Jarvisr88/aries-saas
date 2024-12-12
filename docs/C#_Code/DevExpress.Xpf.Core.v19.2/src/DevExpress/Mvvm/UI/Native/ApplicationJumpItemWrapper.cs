namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Windows.Shell;

    public static class ApplicationJumpItemWrapper
    {
        public static void FillWrapProperties(JumpItem jumpItem)
        {
            ApplicationJumpTaskWrap wrap = jumpItem as ApplicationJumpTaskWrap;
            if (wrap != null)
            {
                wrap.CustomCategory = wrap.ApplicationJumpTask.CustomCategory;
                wrap.Title = wrap.ApplicationJumpTask.Title;
                wrap.Description = wrap.ApplicationJumpTask.Description;
            }
            else
            {
                ApplicationJumpPathWrap wrap2 = jumpItem as ApplicationJumpPathWrap;
                if (wrap2 == null)
                {
                    throw new ArgumentException(string.Empty, "jumpItem");
                }
                wrap2.CustomCategory = wrap2.ApplicationJumpPath.CustomCategory;
                wrap2.Path = wrap2.ApplicationJumpPath.Path;
            }
        }

        public static string GetJumpItemCommandId(JumpItem jumpItem)
        {
            ApplicationJumpTaskWrap wrap = jumpItem as ApplicationJumpTaskWrap;
            return wrap?.ApplicationJumpTask.CommandId;
        }

        public static ApplicationJumpItemInfo Unwrap(JumpItem jumpItem)
        {
            ApplicationJumpTaskWrap wrap = jumpItem as ApplicationJumpTaskWrap;
            if (wrap != null)
            {
                return wrap.ApplicationJumpTask;
            }
            ApplicationJumpPathWrap wrap2 = jumpItem as ApplicationJumpPathWrap;
            if (wrap2 == null)
            {
                throw new ArgumentException(string.Empty, "jumpItem");
            }
            return wrap2.ApplicationJumpPath;
        }

        public static JumpItem Wrap(ApplicationJumpItemInfo applicationJumpItem)
        {
            ApplicationJumpTaskInfo applicationJumpTask = applicationJumpItem as ApplicationJumpTaskInfo;
            if (applicationJumpTask != null)
            {
                return new ApplicationJumpTaskWrap(applicationJumpTask);
            }
            ApplicationJumpPathInfo applicationJumpPath = applicationJumpItem as ApplicationJumpPathInfo;
            if (applicationJumpPath == null)
            {
                throw new ArgumentException(string.Empty, "applicationJumpItem");
            }
            return new ApplicationJumpPathWrap(applicationJumpPath);
        }
    }
}

