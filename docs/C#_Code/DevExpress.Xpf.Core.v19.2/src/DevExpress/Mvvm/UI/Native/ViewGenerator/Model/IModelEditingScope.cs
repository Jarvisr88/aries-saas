namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;

    public interface IModelEditingScope : IDisposable
    {
        void Complete();
        void Revert();
        void Update();

        string Description { get; set; }
    }
}

