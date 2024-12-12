namespace DevExpress.Office.Utils
{
    using System;
    using System.Windows.Forms;

    public abstract class OfficeCursor
    {
        private readonly System.Windows.Forms.Cursor cursor;

        protected OfficeCursor(System.Windows.Forms.Cursor cursor)
        {
            this.cursor = cursor;
        }

        public System.Windows.Forms.Cursor Cursor =>
            this.cursor;
    }
}

