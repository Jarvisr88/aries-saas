namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Windows.Controls;

    public interface IGroupGenerator
    {
        void ApplyGroupInfo(string name, GroupView view, Orientation orientation);
        IGroupGenerator CreateNestedGroup(string name, GroupView view, Orientation orientation);
        void OnAfterGenerateContent();

        EditorsGeneratorBase EditorsGenerator { get; }
    }
}

