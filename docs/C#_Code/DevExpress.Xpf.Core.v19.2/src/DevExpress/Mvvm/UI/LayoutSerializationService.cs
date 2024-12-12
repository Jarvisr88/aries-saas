namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutSerializationService : ServiceBase, ILayoutSerializationService
    {
        public void Deserialize(string state)
        {
            if (string.IsNullOrEmpty(state))
            {
                this.Deserialize(this.InitialState);
            }
            else
            {
                SerializationHelper.DeserializeFromString(state, delegate (Stream x) {
                    DXOptionsLayout options = new DXOptionsLayout();
                    options.AcceptNestedObjects = AcceptNestedObjects.IgnoreChildrenOfDisabledObjects;
                    DXSerializer.Deserialize(this.GetSerializationTarget(), x, string.Empty, options);
                });
            }
        }

        protected virtual FrameworkElement GetSerializationTarget() => 
            base.AssociatedObject;

        protected override void OnAttached()
        {
            base.OnAttached();
            EventHandler handler = null;
            handler = delegate (object s, EventArgs e) {
                this.InitialState = this.Serialize();
                this.AssociatedObject.Initialized -= handler;
            };
            base.AssociatedObject.Initialized += handler;
        }

        public string Serialize()
        {
            FrameworkElement target = this.GetSerializationTarget();
            string layoutVersion = (target == null) ? "" : DXSerializer.GetLayoutVersion(target);
            return SerializationHelper.SerializeToString(delegate (Stream x) {
                DXOptionsLayout options = new DXOptionsLayout();
                options.LayoutVersion = layoutVersion;
                options.AcceptNestedObjects = AcceptNestedObjects.IgnoreChildrenOfDisabledObjects;
                DXSerializer.Serialize(target, x, string.Empty, options);
            });
        }

        public string InitialState { get; private set; }
    }
}

