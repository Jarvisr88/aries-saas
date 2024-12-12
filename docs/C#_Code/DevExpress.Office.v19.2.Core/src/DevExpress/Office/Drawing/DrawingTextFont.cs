namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using System;

    public class DrawingTextFont : ICloneable<DrawingTextFont>, ISupportsCopyFrom<DrawingTextFont>, IDrawingBullet
    {
        private readonly IDocumentModel documentModel;
        private readonly InvalidateProxy innerParent = new InvalidateProxy();
        public const byte DefaultCharset = 1;
        public const byte DefaultPitchFamily = 0;
        private const int TypefaceIndex = 0;
        private const int PanoseIndex = 1;
        private const int CharsetIndex = 0;
        private const int PitchFamilyIndex = 1;
        private string[] stringArray;
        private byte[] byteArray;

        public DrawingTextFont(IDocumentModel documentModel)
        {
            this.documentModel = documentModel;
            this.stringArray = new string[] { string.Empty, string.Empty };
            byte[] buffer1 = new byte[2];
            buffer1[0] = 1;
            this.byteArray = buffer1;
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            this.documentModel.History.Add(item);
            item.Execute();
        }

        public void Clear()
        {
            this.documentModel.BeginUpdate();
            try
            {
                this.Typeface = string.Empty;
                this.Panose = string.Empty;
                this.Charset = 1;
                this.PitchFamily = 0;
            }
            finally
            {
                this.documentModel.EndUpdate();
            }
        }

        public DrawingTextFont Clone()
        {
            DrawingTextFont font = new DrawingTextFont(this.documentModel);
            font.CopyFrom(this);
            return font;
        }

        public void CopyFrom(DrawingTextFont value)
        {
            this.stringArray[0] = value.stringArray[0];
            this.stringArray[1] = value.stringArray[1];
            this.byteArray[0] = value.byteArray[0];
            this.byteArray[1] = value.byteArray[1];
        }

        IDrawingBullet IDrawingBullet.CloneTo(IDocumentModel documentModel)
        {
            DrawingTextFont font = new DrawingTextFont(documentModel);
            font.CopyFrom(this);
            return font;
        }

        public override bool Equals(object obj)
        {
            DrawingTextFont font = obj as DrawingTextFont;
            return ((font != null) ? ((this.stringArray[0] == font.stringArray[0]) && ((this.stringArray[1] == font.stringArray[1]) && ((this.byteArray[0] == font.byteArray[0]) && (this.byteArray[1] == font.byteArray[1])))) : false);
        }

        public override int GetHashCode() => 
            ((this.stringArray[0].GetHashCode() ^ this.stringArray[1].GetHashCode()) ^ this.byteArray[0]) ^ this.byteArray[1];

        private void SetByteArray(int index, byte value)
        {
            if (this.byteArray[index] != value)
            {
                this.ApplyHistoryItem(new DrawingTextFontByteChangedHistoryItem(this, index, this.byteArray[index], value));
            }
        }

        protected internal void SetByteCore(int index, byte value)
        {
            this.byteArray[index] = value;
            this.innerParent.Invalidate();
        }

        private void SetStringArray(int index, string value)
        {
            if (this.stringArray[index] != value)
            {
                this.ApplyHistoryItem(new DrawingTextFontStringChangedHistoryItem(this, index, this.stringArray[index], value));
            }
        }

        protected internal void SetStringCore(int index, string value)
        {
            this.stringArray[index] = value;
            this.innerParent.Invalidate();
        }

        public void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDocumentModel DocumentModel =>
            this.documentModel;

        protected internal ISupportsInvalidate Parent
        {
            get => 
                this.innerParent.Target;
            set => 
                this.innerParent.Target = value;
        }

        public string Typeface
        {
            get => 
                this.stringArray[0];
            set => 
                this.SetStringArray(0, value);
        }

        public string Panose
        {
            get => 
                this.stringArray[1];
            set => 
                this.SetStringArray(1, value);
        }

        public byte Charset
        {
            get => 
                this.byteArray[0];
            set => 
                this.SetByteArray(0, value);
        }

        public byte PitchFamily
        {
            get => 
                this.byteArray[1];
            set => 
                this.SetByteArray(1, value);
        }

        public bool IsDefault =>
            string.IsNullOrEmpty(this.stringArray[0]) && (string.IsNullOrEmpty(this.stringArray[1]) && ((this.byteArray[0] == 1) && (this.byteArray[1] == 0)));

        DrawingBulletType IDrawingBullet.Type =>
            DrawingBulletType.Typeface;
    }
}

