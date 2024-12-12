namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class EditSettingsChangedEventHandler<TOwner> : WeakEventHandler<TOwner, EventArgs, EventHandler> where TOwner: class
    {
        private static Action<WeakEventHandler<TOwner, EventArgs, EventHandler>, object> action;
        private static Func<WeakEventHandler<TOwner, EventArgs, EventHandler>, EventHandler> create;

        static EditSettingsChangedEventHandler()
        {
            EditSettingsChangedEventHandler<TOwner>.action = delegate (WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o) {
                ((BaseEditSettings) o).EditSettingsChanged -= h.Handler;
            };
            EditSettingsChangedEventHandler<TOwner>.create = h => new EventHandler(h.OnEvent);
        }

        public EditSettingsChangedEventHandler(TOwner owner, Action<TOwner, object, EventArgs> onEventAction) : base(owner, onEventAction, EditSettingsChangedEventHandler<TOwner>.action, EditSettingsChangedEventHandler<TOwner>.create)
        {
        }

        public void Subscribe(BaseEditSettings settings)
        {
            settings.EditSettingsChanged += base.Handler;
        }

        public void Unsubscribe(BaseEditSettings settings)
        {
            settings.EditSettingsChanged -= base.Handler;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditSettingsChangedEventHandler<TOwner>.<>c <>9;

            static <>c()
            {
                EditSettingsChangedEventHandler<TOwner>.<>c.<>9 = new EditSettingsChangedEventHandler<TOwner>.<>c();
            }

            internal void <.cctor>b__5_0(WeakEventHandler<TOwner, EventArgs, EventHandler> h, object o)
            {
                ((BaseEditSettings) o).EditSettingsChanged -= h.Handler;
            }

            internal EventHandler <.cctor>b__5_1(WeakEventHandler<TOwner, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);
        }
    }
}

