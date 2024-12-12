namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    [Obsolete("This class is now obsolete. You should use the CheckBoxBrick for printing checkboxes.")]
    public static class CheckImages
    {
        private static Image checkImage;
        private static Image uncheckImage;
        private static Image checkGrayImage;

        static CheckImages();
        public static Image GetImage(CheckState checkState);
    }
}

