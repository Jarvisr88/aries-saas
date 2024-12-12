namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SpinEditSettingsEx : SpinEditSettings
    {
        public static readonly DependencyProperty EditValueTypeProperty = DependencyProperty.Register("EditValueType", typeof(System.Type), typeof(SpinEditSettingsEx), new PropertyMetadata(typeof(decimal)));

        static SpinEditSettingsEx()
        {
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(SpinEditEx), typeof(SpinEditSettingsEx), (CreateEditorMethod) (() => new SpinEditEx()), (CreateEditorSettingsMethod) (() => new SpinEditSettingsEx()));
        }

        public override IBaseEdit CreateEditor(bool assignEditorSettings, IDefaultEditorViewInfo defaultViewInfo, EditorOptimizationMode optimizationMode)
        {
            BaseEdit edit = (BaseEdit) base.CreateEditor(assignEditorSettings, defaultViewInfo, optimizationMode);
            if (this.EditValueType != null)
            {
                edit.EditValueType = this.EditValueType;
            }
            return edit;
        }

        public System.Type EditValueType
        {
            get => 
                (System.Type) base.GetValue(EditValueTypeProperty);
            set => 
                base.SetValue(EditValueTypeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SpinEditSettingsEx.<>c <>9 = new SpinEditSettingsEx.<>c();

            internal IBaseEdit <.cctor>b__0_0() => 
                new SpinEditEx();

            internal BaseEditSettings <.cctor>b__0_1() => 
                new SpinEditSettingsEx();
        }
    }
}

