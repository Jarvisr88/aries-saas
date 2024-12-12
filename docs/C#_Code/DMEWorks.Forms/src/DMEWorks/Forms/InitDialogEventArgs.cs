namespace DMEWorks.Forms
{
    using System;

    public class InitDialogEventArgs : EventArgs
    {
        private readonly SearchableGridAppearance _appearance;
        private string _caption;

        public InitDialogEventArgs(SearchableGridAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException("appearance");
            }
            this._appearance = appearance;
        }

        public SearchableGridAppearance Appearance =>
            this._appearance;

        public string Caption
        {
            get => 
                this._caption;
            set => 
                this._caption = value;
        }
    }
}

