namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class ContentDetailDescriptor : DetailDescriptorBase
    {
        public static readonly DependencyProperty HeaderContentProperty;

        static ContentDetailDescriptor()
        {
            Type ownerType = typeof(ContentDetailDescriptor);
            HeaderContentProperty = DependencyPropertyManager.Register("HeaderContent", typeof(object), ownerType, new FrameworkPropertyMetadata(null));
        }

        internal override DetailInfoWithContent CreateRowDetailInfo(RowDetailContainer container) => 
            new ContentDetailInfo(this, container);

        internal override DataControlBase GetChildDataControl(DataControlBase dataControl, int rowHandle, object detailRow) => 
            null;

        internal override void OnDetach()
        {
        }

        internal override void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod = null)
        {
        }

        internal override void UpdateOriginationDataControls(Action<DataControlBase> updateMethod)
        {
        }

        [TypeConverter(typeof(ObjectConverter))]
        public object HeaderContent
        {
            get => 
                base.GetValue(HeaderContentProperty);
            set => 
                base.SetValue(HeaderContentProperty, value);
        }
    }
}

