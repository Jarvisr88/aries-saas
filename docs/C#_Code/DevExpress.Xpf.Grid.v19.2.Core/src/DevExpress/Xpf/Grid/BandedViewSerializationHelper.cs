namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class BandedViewSerializationHelper
    {
        private Dictionary<string, BandBase> namedBands = new Dictionary<string, BandBase>();
        private Dictionary<string, BandBase> namedSerializationBands = new Dictionary<string, BandBase>();
        private Dictionary<string, BandBase> headersBands = new Dictionary<string, BandBase>();
        private Dictionary<string, ColumnBase> namedColumns = new Dictionary<string, ColumnBase>();
        private Dictionary<string, ColumnBase> fieldNamedColumns = new Dictionary<string, ColumnBase>();

        public BandedViewSerializationHelper(DataControlBase dataControl)
        {
            this.CanRemoveOldColumns = dataControl.GetRemoveOldColumns();
            this.CanAddNewColumns = dataControl.GetAddNewColumns();
            this.SaveNamedElements(dataControl);
        }

        private void ChangeBandsOwner(BandBase band, IBandsOwner newOwner)
        {
            if ((band != null) && ((newOwner != null) && !ReferenceEquals(band.Owner, newOwner)))
            {
                band.Owner.BandsCore.Remove(band);
                newOwner.BandsCore.Add(band);
                band.Owner = newOwner;
            }
        }

        public void ClearCollection(XtraItemRoutedEventArgs e)
        {
            IList collection = e.Collection as IList;
            if (collection != null)
            {
                List<string> list2 = new List<string>();
                List<string> list3 = new List<string>();
                List<string> list4 = new List<string>();
                List<string> list5 = new List<string>();
                bool useFieldNameForSerialization = false;
                DataControlBase dataControlBaseFromOwner = this.GetDataControlBaseFromOwner(e.Owner);
                if (dataControlBaseFromOwner != null)
                {
                    useFieldNameForSerialization = dataControlBaseFromOwner.UseFieldNameForSerialization;
                }
                if (e.Item.ChildProperties != null)
                {
                    foreach (XtraPropertyInfo info in e.Item.ChildProperties)
                    {
                        string serializationName = this.GetSerializationName(info);
                        if (!string.IsNullOrWhiteSpace(serializationName))
                        {
                            list2.Add(serializationName);
                            continue;
                        }
                        string serializationFieldName = this.GetSerializationFieldName(info);
                        if (useFieldNameForSerialization && !string.IsNullOrWhiteSpace(serializationFieldName))
                        {
                            list3.Add(serializationFieldName);
                            continue;
                        }
                        string serializationBandName = this.GetSerializationBandName(info);
                        if (!string.IsNullOrWhiteSpace(serializationBandName))
                        {
                            list4.Add(serializationBandName);
                            continue;
                        }
                        object bandHeader = this.GetBandHeader(info);
                        if ((bandHeader is string) && !string.IsNullOrWhiteSpace((string) bandHeader))
                        {
                            list5.Add((string) bandHeader);
                        }
                    }
                }
                int index = collection.Count - 1;
                while (true)
                {
                    while (true)
                    {
                        if (index < 0)
                        {
                            return;
                        }
                        BaseColumn column = (BaseColumn) collection[index];
                        if (string.IsNullOrWhiteSpace(column.Name) || !list2.Contains(column.Name))
                        {
                            BandBase base3 = column as BandBase;
                            ColumnBase base4 = column as ColumnBase;
                            if (base3 == null)
                            {
                                if (base4 == null)
                                {
                                    collection.RemoveAt(index);
                                    break;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(base3.BandSerializationName) && !list4.Contains(base3.BandSerializationName))
                                {
                                    collection.RemoveAt(index);
                                    break;
                                }
                                if (string.IsNullOrWhiteSpace(base3.BandSerializationName) && ((base3.Header is string) && !list5.Contains((string) base3.Header)))
                                {
                                    collection.RemoveAt(index);
                                    break;
                                }
                            }
                            if ((base3 == null) && ((base4 == null) || (string.IsNullOrWhiteSpace(base4.FieldName) || !list3.Contains(base4.FieldName))))
                            {
                                collection.RemoveAt(index);
                                break;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(column.Name) && !list2.Contains(column.Name))
                        {
                            collection.RemoveAt(index);
                        }
                        break;
                    }
                    index--;
                }
            }
        }

        public void FindBand(XtraFindCollectionItemEventArgs e, IBandsOwner newOwner)
        {
            BandBase base2 = null;
            string serializationName = this.GetSerializationName(e.Item);
            if (!string.IsNullOrWhiteSpace(serializationName))
            {
                this.namedBands.TryGetValue(serializationName, out base2);
                this.ChangeBandsOwner(base2, newOwner);
            }
            else
            {
                string serializationBandName = this.GetSerializationBandName(e.Item);
                if (serializationBandName != null)
                {
                    this.namedSerializationBands.TryGetValue(serializationBandName, out base2);
                    if (!ReferenceEquals(base2, newOwner))
                    {
                        this.ChangeBandsOwner(base2, newOwner);
                    }
                }
                else
                {
                    object bandHeader = this.GetBandHeader(e.Item);
                    if ((bandHeader != null) && (bandHeader is string))
                    {
                        this.headersBands.TryGetValue((string) bandHeader, out base2);
                        if (!ReferenceEquals(base2, newOwner))
                        {
                            this.ChangeBandsOwner(base2, newOwner);
                        }
                    }
                }
            }
            e.CollectionItem = base2;
        }

        public void FindColumn(XtraFindCollectionItemEventArgs e)
        {
            ColumnBase base2 = null;
            string serializationName = this.GetSerializationName(e.Item);
            if (!string.IsNullOrWhiteSpace(serializationName))
            {
                this.namedColumns.TryGetValue(serializationName, out base2);
            }
            else
            {
                DataControlBase dataControlBaseFromOwner = this.GetDataControlBaseFromOwner(e.Owner);
                if ((dataControlBaseFromOwner != null) && dataControlBaseFromOwner.UseFieldNameForSerialization)
                {
                    string serializationFieldName = this.GetSerializationFieldName(e.Item);
                    if (!string.IsNullOrWhiteSpace(serializationFieldName))
                    {
                        this.fieldNamedColumns.TryGetValue(serializationFieldName, out base2);
                    }
                }
            }
            this.MoveColumn(base2, e.Collection as IList);
            e.CollectionItem = base2;
        }

        private object GetBandHeader(XtraPropertyInfo item) => 
            this.GetSerializationProperty(item, "Header");

        private DataControlBase GetDataControlBaseFromOwner(object owner)
        {
            if (owner == null)
            {
                return null;
            }
            DataControlBase base2 = owner as DataControlBase;
            if (base2 != null)
            {
                return base2;
            }
            BandBase base3 = owner as BandBase;
            return (((base3 == null) || (base3.Owner == null)) ? null : base3.Owner.DataControl);
        }

        private string GetSerializationBandName(XtraPropertyInfo item) => 
            this.GetSerializationProperty(item, "BandSerializationName");

        private string GetSerializationFieldName(XtraPropertyInfo item) => 
            this.GetSerializationProperty(item, "FieldName");

        private string GetSerializationName(XtraPropertyInfo item) => 
            this.GetSerializationProperty(item, "Name");

        private string GetSerializationProperty(XtraPropertyInfo item, string propertyName)
        {
            if (item.ChildProperties == null)
            {
                return null;
            }
            string str = null;
            XtraPropertyInfo info = item.ChildProperties[propertyName];
            if ((info != null) && (info.Value != null))
            {
                str = info.Value.ToString();
            }
            return str;
        }

        private void MoveColumn(ColumnBase column, IList collection)
        {
            if ((column != null) && (collection != null))
            {
                if (column.ParentBand != null)
                {
                    if (ReferenceEquals(column.ParentBand.ColumnsCore, collection))
                    {
                        return;
                    }
                    column.ParentBand.ColumnsCore.Remove(column);
                }
                if (!collection.Contains(column))
                {
                    collection.Add(column);
                }
            }
        }

        private void SaveNamedElements(DataControlBase dataControl)
        {
            if (dataControl.BandsLayoutCore != null)
            {
                dataControl.BandsLayoutCore.ForeachBand(delegate (BandBase band) {
                    if (!string.IsNullOrWhiteSpace(band.Name))
                    {
                        this.namedBands.Add(band.Name, band);
                    }
                    else if ((band.BandSerializationName != null) && !this.namedSerializationBands.ContainsKey(band.BandSerializationName))
                    {
                        this.namedSerializationBands.Add(band.BandSerializationName, band);
                    }
                    else if ((band.Header is string) && ((((string) band.Header) != null) && !this.headersBands.ContainsKey((string) band.Header)))
                    {
                        this.headersBands.Add((string) band.Header, band);
                    }
                });
            }
            foreach (ColumnBase base2 in dataControl.ColumnsCore)
            {
                if (!string.IsNullOrWhiteSpace(base2.Name))
                {
                    this.namedColumns.Add(base2.Name, base2);
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(base2.FieldName) && !this.fieldNamedColumns.ContainsKey(base2.FieldName))
                {
                    this.fieldNamedColumns.Add(base2.FieldName, base2);
                }
            }
        }

        public bool CanRemoveOldColumns { get; private set; }

        public bool CanAddNewColumns { get; private set; }
    }
}

