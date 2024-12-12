namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections.Generic;

    public class PdfAcroFormRadioGroupFieldController<TRect>
    {
        private readonly List<PdfAcroFormRadioGroupButton<TRect>> buttons;
        private int selectedIndex;

        public PdfAcroFormRadioGroupFieldController()
        {
            this.buttons = new List<PdfAcroFormRadioGroupButton<TRect>>();
            this.selectedIndex = -1;
        }

        private PdfAcroFormRadioGroupFieldController(List<PdfAcroFormRadioGroupButton<TRect>> buttons, int selectedIndex)
        {
            this.buttons = new List<PdfAcroFormRadioGroupButton<TRect>>();
            this.selectedIndex = -1;
            this.buttons = buttons;
            this.selectedIndex = selectedIndex;
        }

        public void AddButton(string name, TRect rect)
        {
            PdfAcroFormField.ValidateName(name);
            if (rect == null)
            {
                throw new ArgumentNullException("rect");
            }
            this.buttons.Add(new PdfAcroFormRadioGroupButton<TRect>(name, rect));
        }

        public void ClearButtons()
        {
            this.buttons.Clear();
        }

        public PdfAcroFormRadioGroupFieldController<TTarget> Convert<TTarget>(Func<TRect, TTarget> converter)
        {
            List<PdfAcroFormRadioGroupButton<TTarget>> buttons = new List<PdfAcroFormRadioGroupButton<TTarget>>(this.ButtonCount);
            foreach (PdfAcroFormRadioGroupButton<TRect> button in this.buttons)
            {
                buttons.Add(new PdfAcroFormRadioGroupButton<TTarget>(button.Name, converter(button.Rect)));
            }
            return new PdfAcroFormRadioGroupFieldController<TTarget>(buttons, this.selectedIndex);
        }

        public PdfAcroFormRadioGroupButton<TRect> GetButton(int index) => 
            this.buttons[index];

        public int SelectedIndex
        {
            get => 
                this.selectedIndex;
            set
            {
                if (this.buttons.Count == 0)
                {
                    throw new ArgumentOutOfRangeException("value", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgCantSetSelectedIndexWithoutValues));
                }
                if ((value < 0) || (value > this.buttons.Count))
                {
                    throw new ArgumentOutOfRangeException("value", string.Format(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectSelectedIndexValue), this.buttons.Count - 1));
                }
                this.selectedIndex = value;
            }
        }

        public int ButtonCount =>
            this.buttons.Count;
    }
}

