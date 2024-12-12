namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Core.Native.Serialization;
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;

    public class PSUpdatedObjects
    {
        public static readonly string TextProperty = "Text";
        public static readonly string TextValueProperty = "TextValue";
        public static readonly string NavigationPageIdProperty = "NavigationPageId";
        public static readonly string NavigationBrickIndicesProperty = "NavigationBrickIndices";
        public static readonly string NavigationBrickBoundsProperty = "NavigationBrickBounds";
        private Dictionary<int, PropertyValues> properties = new Dictionary<int, PropertyValues>();

        public void Clear()
        {
            this.properties = new Dictionary<int, PropertyValues>();
        }

        public string GetValue(StoredID objectID, string property) => 
            this.properties[objectID.Id][property];

        public T GetValue<T>(StoredID objectID, string property)
        {
            T defaultValue = default(T);
            return this.properties[objectID.Id].Get<T>(property, defaultValue);
        }

        private void RestoreNavigation(VisualBrick vb, PropertyValues values, SerializedPageDataList serializedPageDataList)
        {
            if (vb != null)
            {
                vb.NavigationPageId = values.Get<long>(NavigationPageIdProperty, vb.NavigationPageId);
                if (vb.NavigationPageId >= 0L)
                {
                    vb.NavigationPair = BrickPagePair.Create(values.Get<int[]>(NavigationBrickIndicesProperty, vb.NavigationPair.BrickIndices), serializedPageDataList.GetPageIndex(vb.NavigationPageId), vb.NavigationPageId, values.Get<RectangleF>(NavigationBrickBoundsProperty, vb.NavigationPair.BrickBounds));
                }
            }
        }

        public void RestorePageBrickValues(BrickBase brick, SerializedPageDataList serializedPageDataList)
        {
            PropertyValues values;
            VisualBrick vb = brick as VisualBrick;
            if (this.properties.TryGetValue(brick.StoredId, out values))
            {
                this.RestoreNavigation(vb, values, serializedPageDataList);
                this.RestoreText(vb as TextBrickBase, values);
                this.RestoreTextValue(vb as TextBrickBase, values);
            }
        }

        public void RestorePageBrickValues(Page page, SerializedPageDataList serializedPageDataList)
        {
            NestedBrickIterator iterator = new NestedBrickIterator(page.InnerBrickList);
            while (iterator.MoveNext())
            {
                this.RestorePageBrickValues(iterator.CurrentBrick, serializedPageDataList);
            }
        }

        private void RestoreText(TextBrickBase textbrick, PropertyValues values)
        {
            if (textbrick != null)
            {
                textbrick.Text = values.Get<string>(TextProperty, textbrick.Text);
            }
        }

        private void RestoreTextValue(TextBrickBase textbrick, PropertyValues values)
        {
            if (textbrick != null)
            {
                textbrick.TextValue = values.Get<object>(TextValueProperty, textbrick.TextValue);
            }
        }

        public void UpdateProperty(StoredID objectID, string property, RectangleF value)
        {
            this.UpdateProperty(objectID, property, DictionarySerializeExt.RectangleFToString(value));
        }

        public void UpdateProperty(StoredID objectID, string property, int value)
        {
            this.UpdateProperty(objectID, property, value.ToString(CultureInfo.InvariantCulture));
        }

        public void UpdateProperty(StoredID objectID, string property, long value)
        {
            this.UpdateProperty(objectID, property, value.ToString(CultureInfo.InvariantCulture));
        }

        public void UpdateProperty(StoredID objectID, string property, object value)
        {
            this.UpdateProperty(objectID, property, DictionarySerializeExt.SerializeObjectToString(value));
        }

        public void UpdateProperty(StoredID objectID, string property, string value)
        {
            PropertyValues values;
            if (this.properties.TryGetValue(objectID.Id, out values))
            {
                values[property] = value;
            }
            else
            {
                PropertyValues values1 = new PropertyValues();
                values1.Add(property, value);
                this.properties[objectID.Id] = values1;
            }
        }

        [XtraSerializableProperty]
        public string SerializedValues
        {
            get => 
                this.properties.Serialize();
            set => 
                this.properties.Deserialize<int, PropertyValues>(value);
        }

        public int Count =>
            this.properties.Count;

        internal class PropertyValues : Dictionary<string, string>
        {
            public T Get<T>(string name, T defaultValue)
            {
                string str;
                return (!base.TryGetValue(name, out str) ? defaultValue : ((T) str.ParseValue<T>()));
            }
        }
    }
}

