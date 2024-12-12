namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class CustomDialogServiceEditorBehavior : DialogServiceEditorBehaviorBase
    {
        protected override void ProcessClickForFrameworkElementInternal(FrameworkElement owner, object editValue)
        {
            ICustomDialogService service = base.DialogServiceTemplate.With<DataTemplate, ICustomDialogService>(new Func<DataTemplate, ICustomDialogService>(TemplateHelper.LoadFromTemplate<ICustomDialogService>));
            if (service != null)
            {
                AssignableServiceHelper2<DependencyObject, ICustomDialogService>.DoServiceAction(owner, service, delegate (ICustomDialogService service) {
                    UITypeEditorValue editableValue = this.GetEditableValue(owner, editValue);
                    this.RaiseDialogShown(editableValue);
                    AfterDialogServiceDialogClosedEventArgs args = this.RaiseDialogClosed(editableValue, service.ShowDialog(this.Title, editableValue));
                    if (editableValue.IsModified ? ((Action<ICustomDialogService>) true) : ((args.PostValue == null) ? ((Action<ICustomDialogService>) false) : ((Action<ICustomDialogService>) args.PostValue.Value)))
                    {
                        this.PostEditValue(owner, args.Value.Value);
                    }
                });
            }
        }
    }
}

