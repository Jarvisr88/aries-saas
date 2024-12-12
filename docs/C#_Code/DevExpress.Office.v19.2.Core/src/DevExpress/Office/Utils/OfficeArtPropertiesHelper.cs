namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class OfficeArtPropertiesHelper
    {
        public static OfficeDrawingPartBase FindPart(CompositeOfficeDrawingPartBase drawing, int headerTypeCode)
        {
            OfficeDrawingPartBase base3;
            using (List<OfficeDrawingPartBase>.Enumerator enumerator = drawing.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        OfficeDrawingPartBase current = enumerator.Current;
                        if (current.HeaderTypeCode != headerTypeCode)
                        {
                            continue;
                        }
                        base3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return base3;
        }

        public static TElement[] GetArrayOfElements<TElement, TType>(OfficeArtPropertiesBase artProperties) where TType: OfficeDrawingTypedMsoArrayPropertyBase<TElement>
        {
            TType propertyByType = GetPropertyByType(artProperties, typeof(TType)) as TType;
            return propertyByType?.GetElements();
        }

        public static TElement[] GetArrayOfElements<TElement, TType>(IEnumerable<object> properties) where TType: class, IOfficeDrawingTypedMsoArrayPropertyBase<TElement>
        {
            TType propertyByType = GetPropertyByType(properties, typeof(TType)) as TType;
            return propertyByType?.GetElements();
        }

        public static IOfficeDrawingProperty GetPropertyByType(OfficeArtPropertiesBase prop, Type propertyType)
        {
            for (int i = 0; i < prop.Properties.Count; i++)
            {
                if (propertyType.IsInstanceOfType(prop.Properties[i]))
                {
                    return prop.Properties[i];
                }
            }
            return null;
        }

        public static object GetPropertyByType(IEnumerable<object> prop, Type propertyType) => 
            prop.FirstOrDefault<object>(p => propertyType.IsInstanceOfType(p));

        public static void Merge(OfficeArtPropertiesBase artProperties, OfficeArtPropertiesBase commonArtProperties)
        {
            if (commonArtProperties != null)
            {
                for (int i = 0; i < commonArtProperties.Properties.Count; i++)
                {
                    IOfficeDrawingProperty other = commonArtProperties.Properties[i];
                    IOfficeDrawingProperty propertyByType = GetPropertyByType(artProperties, other.GetType());
                    if (propertyByType != null)
                    {
                        propertyByType.Merge(other);
                    }
                    else
                    {
                        artProperties.Properties.Add(other);
                    }
                }
                for (int j = 0; j < artProperties.Properties.Count; j++)
                {
                    artProperties.Properties[j].Execute(artProperties);
                }
            }
        }
    }
}

