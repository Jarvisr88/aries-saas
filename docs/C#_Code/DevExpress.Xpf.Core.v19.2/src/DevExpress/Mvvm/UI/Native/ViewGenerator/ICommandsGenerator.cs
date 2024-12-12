namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Entity.Model;
    using System;

    public interface ICommandsGenerator
    {
        void GenerateCommand(IEdmPropertyInfo property);
    }
}

