namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ContentHostPresenterBase : ContentPresenter
    {
        private List<ContentHostPresenterBase.ContentHostStorage> ContentHostStorages;

        static ContentHostPresenterBase();
        public ContentHostPresenterBase();
        protected ISelectorBase GetOwnerControl();
        protected void Init(string hostName, Func<FrameworkElement> getHostChild);
        private void OnContentChanged(object oldValue, object newValue);
        protected override void OnInitialized(EventArgs e);
        internal void SetContentHost(string hostName, ContentHostBase value);
        protected void UpdateHostChild(string hostName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContentHostPresenterBase.<>c <>9;
            public static Action<ContentHostBase> <>9__3_0;

            static <>c();
            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <OnContentChanged>b__3_0(ContentHostBase x);
        }

        private class ContentHostStorage
        {
            private ContentHostBase host;
            private Func<FrameworkElement> GetHostChild;

            public ContentHostStorage(string hostName, Func<FrameworkElement> getHostChild);
            private void OnHostChanged(ContentHostBase oldValue, ContentHostBase newValue);
            public void UpdateHostChild();

            public ContentHostBase Host { get; set; }

            public string Name { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ContentHostPresenterBase.ContentHostStorage.<>c <>9;
                public static Action<ContentHostBase> <>9__11_0;

                static <>c();
                internal void <OnHostChanged>b__11_0(ContentHostBase x);
            }
        }
    }
}

