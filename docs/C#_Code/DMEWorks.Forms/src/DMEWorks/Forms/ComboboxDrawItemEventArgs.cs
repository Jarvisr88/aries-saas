namespace DMEWorks.Forms
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    public class ComboboxDrawItemEventArgs : DrawItemEventArgs
    {
        private readonly DataRowView _row;
        private readonly string _text;

        internal ComboboxDrawItemEventArgs(DrawItemEventArgs args, DataRowView row, string text) : base(args.Graphics, args.Font, args.Bounds, args.Index, args.State, args.ForeColor, args.BackColor)
        {
            this._row = row;
            this._text = text;
        }

        public DataRowView Row =>
            this._row;

        public string Text =>
            this._text;
    }
}

