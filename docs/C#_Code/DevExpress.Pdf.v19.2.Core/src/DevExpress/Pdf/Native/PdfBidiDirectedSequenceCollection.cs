namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfBidiDirectedSequenceCollection : List<PdfBidiSequence>
    {
        private PdfBidiTextDirection direction;

        public PdfBidiDirectedSequenceCollection(PdfBidiTextDirection direction)
        {
            this.direction = direction;
        }

        public string GetDirectedString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.direction != PdfBidiTextDirection.RightToLeft)
            {
                foreach (PdfBidiSequence sequence2 in this)
                {
                    sequence2.AppendTo(builder);
                }
            }
            else
            {
                for (int i = base.Count - 1; i >= 0; i--)
                {
                    PdfBidiSequence sequence = base[i];
                    if (sequence.CharacterClass == PdfBidiCharacterClass.Numeric)
                    {
                        sequence.AppendTo(builder);
                    }
                    else
                    {
                        sequence.AppendMirroredTo(builder);
                    }
                }
            }
            return builder.ToString();
        }

        public PdfBidiTextDirection Direction
        {
            get => 
                this.direction;
            set => 
                this.direction = value;
        }
    }
}

