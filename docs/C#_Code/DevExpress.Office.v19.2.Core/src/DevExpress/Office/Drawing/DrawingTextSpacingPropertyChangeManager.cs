namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;

    public class DrawingTextSpacingPropertyChangeManager : IDrawingTextSpacing
    {
        private int index;
        private readonly IDrawingTextSpacingsChanger info;

        public DrawingTextSpacingPropertyChangeManager(IDrawingTextSpacingsChanger info)
        {
            Guard.ArgumentNotNull(info, "ITextSpacingsChanger");
            this.info = info;
        }

        protected internal IDrawingTextSpacing GetFormatInfo(int index)
        {
            this.index = index;
            return this;
        }

        public DrawingTextSpacingValueType Type
        {
            get => 
                this.info.GetType(this.index);
            set => 
                this.info.SetType(this.index, value);
        }

        public int Value
        {
            get => 
                this.info.GetValue(this.index);
            set => 
                this.info.SetValue(this.index, value);
        }
    }
}

