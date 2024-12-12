namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    internal static class FilterEdtiorDialogShowHelper
    {
        public static void Show(DependencyObject owner, FilteringUIContext context, string propertyName, string title, DataTemplate filterEditorDialogServiceTemplate, DataTemplate filterEditorTemplate)
        {
            if (context != null)
            {
                AssignableServiceHelper2<DataViewBase, IDialogService>.DoServiceAction(owner, filterEditorDialogServiceTemplate, delegate (IDialogService service) {
                    FilterEditorDialogViewModel viewModel = new FilterEditorDialogViewModel(context, propertyName, filterEditorTemplate);
                    MessageBoxResult? defaultButton = null;
                    defaultButton = null;
                    List<UICommand> dialogCommands = UICommand.GenerateFromMessageBoxButton(MessageBoxButton.OKCancel, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton);
                    dialogCommands[0].Command = new DelegateCommand<CancelEventArgs>(_ => viewModel.Apply());
                    UICommand item = new UICommand();
                    item.Caption = EditorLocalizer.GetString(EditorStringId.Apply);
                    item.IsCancel = false;
                    item.IsDefault = false;
                    item.Command = new DelegateCommand<CancelEventArgs>(delegate (CancelEventArgs e) {
                        e.Cancel = true;
                        viewModel.Apply();
                    });
                    dialogCommands.Add(item);
                    service.ShowDialog(dialogCommands, title, viewModel);
                });
            }
        }
    }
}

