namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class CardsPanelInfo
    {
        private CardsPanel panel;

        public CardsPanelInfo(CardsPanel panel)
        {
            this.panel = panel;
        }

        protected virtual CardsPanel Panel =>
            this.panel;

        public virtual DevExpress.Xpf.Core.Alignment Alignment =>
            this.Panel.CardAlignment;

        public virtual System.Windows.Controls.Orientation Orientation =>
            this.Panel.Orientation;

        public virtual SizeHelperBase SizeHelper =>
            SizeHelperBase.GetDefineSizeHelper(this.Orientation);

        public virtual double FixedSize =>
            this.Panel.FixedSize;

        public virtual int MaxCardCountInRow =>
            this.Panel.MaxCardCountInRow;

        public virtual double SeparatorThickness =>
            this.Panel.SeparatorThickness;

        public virtual Thickness CardMargin =>
            this.Panel.CardMargin;
    }
}

