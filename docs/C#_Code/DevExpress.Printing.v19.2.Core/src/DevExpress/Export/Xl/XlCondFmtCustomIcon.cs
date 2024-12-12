namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlCondFmtCustomIcon
    {
        public XlCondFmtCustomIcon(int id, XlCondFmtIconSetType iconSetType)
        {
            int num = XlCondFmtRuleIconSet.IconSetCountTable[iconSetType] - 1;
            if ((id < 0) || (id > num))
            {
                throw new ArgumentOutOfRangeException($"Custom icon id out of range 0...{num}");
            }
            this.Id = id;
            this.IconSetType = iconSetType;
        }

        public int Id { get; private set; }

        public XlCondFmtIconSetType IconSetType { get; private set; }
    }
}

