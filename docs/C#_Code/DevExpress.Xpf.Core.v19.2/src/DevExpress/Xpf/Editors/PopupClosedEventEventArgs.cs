namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class PopupClosedEventEventArgs : EventArgs
    {
        private bool? postValue;

        public PopupClosedEventEventArgs(UITypeEditorValue value)
        {
            this.Value = value;
        }

        public UITypeEditorValue Value { get; private set; }

        public bool Handled { get; set; }

        public bool? PostValue
        {
            get => 
                this.postValue;
            set
            {
                bool? postValue = this.postValue;
                bool? nullable2 = value;
                if (!((postValue.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((postValue != null) == (nullable2 != null)) : false))
                {
                    this.postValue = value;
                    this.Value.ForcePost = value;
                }
            }
        }
    }
}

