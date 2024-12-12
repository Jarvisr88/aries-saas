namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public abstract class DXGridDataController : GridDataController
    {
        private static Action<DXGridDataController, object, ListChangedEventArgs> onChanged;
        private ListChangedWeakEventHandler<DXGridDataController> listChangedHandler;
        private static bool _DisableThreadingProblemsDetection;
        internal const string CrossThreadExceptionMessage = "A cross-thread operation is detected. For more information, refer to https://documentation.devexpress.com/WPF/11765/Controls-and-Libraries/Data-Grid/Binding-to-Data/Managing-Multi-Thread-Data-Updates";

        static DXGridDataController()
        {
            onChanged = (owner, o, e) => owner.OnListChanged(o, e);
            _DisableThreadingProblemsDetection = false;
        }

        protected DXGridDataController()
        {
        }

        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            if ((this.Dispatcher == null) || this.Dispatcher.CheckAccess())
            {
                base.OnBindingListChanged(sender, e);
            }
            else if (DisableThreadingProblemsDetection)
            {
                this.Dispatcher.BeginInvoke(() => this.OnBindingListChanged(sender, e), new object[0]);
            }
            else
            {
                this.Dispatcher.BeginInvoke(new Action(DXGridDataController.ThrowCrossThreadException), new object[0]);
                ThrowCrossThreadException();
            }
        }

        protected override void SubscribeListChanged(INotificationProvider provider, object list)
        {
            if (list is IBindingList)
            {
                (list as IBindingList).ListChanged += this.ListChangedHandler.Handler;
            }
        }

        private static void ThrowCrossThreadException()
        {
            throw new InvalidOperationException("A cross-thread operation is detected. For more information, refer to https://documentation.devexpress.com/WPF/11765/Controls-and-Libraries/Data-Grid/Binding-to-Data/Managing-Multi-Thread-Data-Updates");
        }

        protected override void UnsubscribeListChanged(INotificationProvider provider, object list)
        {
            if (list is IBindingList)
            {
                (list as IBindingList).ListChanged -= this.ListChangedHandler.Handler;
            }
        }

        protected override bool UseFirstRowTypeWhenPopulatingColumns(Type rowType) => 
            rowType.FullName == ListDataControllerHelper.UseFirstRowTypeWhenPopulatingColumnsTypeName;

        private ListChangedWeakEventHandler<DXGridDataController> ListChangedHandler
        {
            get
            {
                this.listChangedHandler ??= new ListChangedWeakEventHandler<DXGridDataController>(this, onChanged);
                return this.listChangedHandler;
            }
        }

        [Obsolete("Threading problems detection is enabled by default")]
        public static bool DisableThreadingProblemsDetection
        {
            get => 
                _DisableThreadingProblemsDetection;
            set => 
                _DisableThreadingProblemsDetection = value;
        }

        protected abstract System.Windows.Threading.Dispatcher Dispatcher { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXGridDataController.<>c <>9 = new DXGridDataController.<>c();

            internal void <.cctor>b__17_0(DXGridDataController owner, object o, ListChangedEventArgs e)
            {
                owner.OnListChanged(o, e);
            }
        }
    }
}

