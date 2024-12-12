namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public class SerializationDiffCalculator
    {
        public static XtraPropertyInfo[] CalculateDiff(XtraPropertyInfo[] prevSnapShot, XtraPropertyInfo[] currentSnapShot)
        {
            XtraPropertyCollection propertys = new XtraPropertyCollection();
            propertys.AddRange(prevSnapShot);
            XtraPropertyCollection propertys2 = new XtraPropertyCollection();
            propertys2.AddRange(currentSnapShot);
            return CalculateDiffCore(propertys, propertys2).ToArray();
        }

        public static XtraPropertyInfoCollection CalculateDiffCore(IXtraPropertyCollection prevSnapShot, IXtraPropertyCollection currentSnapShot)
        {
            XtraPropertyInfoCollection infos = new XtraPropertyInfoCollection();
            int count = currentSnapShot.Count;
            if (prevSnapShot.Count == count)
            {
                for (int i = 0; i < count; i++)
                {
                    infos.AddRange(CalculatePropertyDiff(prevSnapShot[i], currentSnapShot[i]));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    infos.Add(currentSnapShot[i]);
                }
            }
            return infos;
        }

        protected internal static XtraPropertyInfoCollection CalculatePropertyDiff(XtraPropertyInfo prev, XtraPropertyInfo current)
        {
            if (prev.ChildProperties == null)
            {
                if (Equals(prev.Value, current.Value))
                {
                    return new XtraPropertyInfoCollection();
                }
                return new XtraPropertyInfoCollection { current };
            }
            if (prev.Value == null)
            {
                XtraPropertyInfoCollection infos = CalculateDiffCore(prev.ChildProperties, current.ChildProperties);
                if (infos.Count > 0)
                {
                    XtraPropertyInfo item = new XtraPropertyInfo(current.Name, current.PropertyType, current.Value, current.IsKey);
                    item.ChildProperties.AddRange(infos.ToArray());
                    infos.Clear();
                    infos.Add(item);
                }
                return infos;
            }
            if (Equals(prev.Value, current.Value) && (CalculateDiffCore(prev.ChildProperties, current.ChildProperties).Count <= 0))
            {
                return new XtraPropertyInfoCollection();
            }
            return new XtraPropertyInfoCollection { current };
        }
    }
}

