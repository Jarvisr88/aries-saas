namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface IImageEdit : IInputElement
    {
        object GetDataFromImage(ImageSource source);
        void Load();
        void Save();
    }
}

