namespace DevExpress.Emf
{
    using System;
    using System.Text;

    public class EmfPlusFont : EmfPlusObject
    {
        private readonly float emSize;
        private readonly DXGraphicsUnit unitType;
        private readonly DXFontStyle fontStyle;
        private readonly string fontFamily;

        public EmfPlusFont(EmfPlusReader reader)
        {
            reader.ReadInt32();
            this.emSize = reader.ReadSingle();
            this.unitType = (DXGraphicsUnit) reader.ReadInt32();
            this.fontStyle = (DXFontStyle) reader.ReadInt32();
            reader.ReadInt32();
            int num = reader.ReadInt32();
            this.fontFamily = Encoding.Unicode.GetString(reader.ReadBytes(num * 2));
        }

        public EmfPlusFont(string fontFamily, float emSize, DXFontStyle fontStyle, DXGraphicsUnit unitType)
        {
            this.fontFamily = fontFamily;
            this.emSize = emSize;
            this.fontStyle = fontStyle;
            this.unitType = unitType;
        }

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(-608169982);
            writer.Write(this.emSize);
            writer.Write((int) this.unitType);
            writer.Write((int) this.fontStyle);
            writer.Write(0);
            writer.Write(this.fontFamily.Length);
            writer.Write(Encoding.Unicode.GetBytes(this.fontFamily));
        }

        public float EmSize =>
            this.emSize;

        public DXGraphicsUnit UnitType =>
            this.unitType;

        public DXFontStyle FontStyle =>
            this.fontStyle;

        public string FontFamily =>
            this.fontFamily;

        public override EmfPlusObjectType Type =>
            EmfPlusObjectType.ObjectTypeFont;

        public override int Size =>
            0x18 + (this.fontFamily.Length * 2);
    }
}

