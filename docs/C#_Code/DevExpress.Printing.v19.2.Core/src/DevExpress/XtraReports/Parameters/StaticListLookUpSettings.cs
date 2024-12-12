namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraReports.Parameters.StaticListLookUpSettings")]
    public class StaticListLookUpSettings : LookUpSettings, IXtraSupportDeserializeCollectionItem
    {
        private LookUpValueCollection lookUpValues = new LookUpValueCollection();

        object IXtraSupportDeserializeCollectionItem.CreateCollectionItem(string propertyName, XtraItemEventArgs e) => 
            (propertyName != "LookUpValues") ? null : new LookUpValue();

        void IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem(string propertyName, XtraSetItemIndexEventArgs e)
        {
            if (propertyName == "LookUpValues")
            {
                this.LookUpValues.Add((LookUpValue) e.Item.Value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 0, XtraSerializationFlags.Cached), Editor("DevExpress.XtraReports.Design.LookUpValuesEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), Description("Stores a report parameter's list of static values."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraReports.Parameters.StaticListLookUpSettings.LookUpValues"), DXDisplayNameIgnore(IgnoreRecursionOnly=true)]
        public virtual LookUpValueCollection LookUpValues =>
            this.lookUpValues;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object DataSource
        {
            get => 
                this.LookUpValues;
            set
            {
                throw new NotSupportedException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object DataAdapter
        {
            get => 
                null;
            set
            {
                throw new NotSupportedException();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string DataMember
        {
            get => 
                string.Empty;
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}

