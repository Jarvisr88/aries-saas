namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class EditorSettingsProvider
    {
        private ConcurrentDictionary<Type, CreateEditorMethod2> createEditor;
        private ConcurrentDictionary<Type, CreateEditorSettingsMethod> createEditorSettings;

        static EditorSettingsProvider()
        {
            Default = CreateDefaultSettingsProvider();
            Default.RegisterEditors();
        }

        private static EditorSettingsProvider CreateDefaultSettingsProvider() => 
            new EditorSettingsProvider();

        public IBaseEdit CreateEditor(Type editorSettings, EditorOptimizationMode optimizationMode = 1)
        {
            IBaseEdit edit;
            if (!editorSettings.IsSubclassOf(typeof(BaseEditSettings)))
            {
                throw new ArgumentException("Wrong base class supplied", "editor");
            }
            return (!this.CreateEditorCore(editorSettings, optimizationMode, out edit) ? null : edit);
        }

        private bool CreateEditorCore(Type editorSettings, EditorOptimizationMode optimizationMode, out IBaseEdit editor)
        {
            editor = null;
            if (optimizationMode == EditorOptimizationMode.Extended)
            {
                editor = new InplaceBaseEdit();
                return true;
            }
            bool isOptimized = optimizationMode == EditorOptimizationMode.Simple;
            CreateEditorMethod2 method = null;
            if (this.createEditor.TryGetValue(editorSettings, out method))
            {
                editor = method(isOptimized);
                return true;
            }
            while (editorSettings != null)
            {
                editorSettings = editorSettings.BaseType;
                if ((editorSettings != null) && this.createEditor.TryGetValue(editorSettings, out method))
                {
                    editor = method(isOptimized);
                    return true;
                }
            }
            return false;
        }

        public BaseEditSettings CreateEditorSettings(Type editor)
        {
            if (!editor.IsSubclassOf(typeof(BaseEdit)))
            {
                throw new ArgumentException("Wrong base class supplied", "editor");
            }
            CreateEditorSettingsMethod method = null;
            if (this.createEditorSettings.TryGetValue(editor, out method))
            {
                return method();
            }
            while (editor != null)
            {
                editor = editor.BaseType;
                if ((editor != null) && this.createEditorSettings.TryGetValue(editor, out method))
                {
                    return method();
                }
            }
            return null;
        }

        public bool IsCompatible(IBaseEdit editCore, BaseEditSettings editSettings) => 
            editSettings.CreateEditor(false, EmptyDefaultEditorViewInfo.Instance, EditorOptimizationMode.Simple).GetType() == editCore.GetType();

        protected void RegisterEditor(Type editor, Type editorSettings, CreateEditorMethod2 createEditorMethod, CreateEditorSettingsMethod createEditorSettingsMethod)
        {
            this.createEditor[editorSettings] = createEditorMethod;
            this.createEditorSettings[editor] = createEditorSettingsMethod;
        }

        private void RegisterEditors()
        {
            // Unresolved stack state at '00000832'
        }

        public virtual void RegisterUserEditor(Type editor, Type editorSettings, CreateEditorMethod createEditorMethod, CreateEditorSettingsMethod createEditorSettingsMethod)
        {
            this.RegisterEditor(editor, editorSettings, optimized => createEditorMethod(), createEditorSettingsMethod);
        }

        public virtual void RegisterUserEditor2(Type editor, Type editorSettings, CreateEditorMethod2 createEditorMethod, CreateEditorSettingsMethod createEditorSettingsMethod)
        {
            this.RegisterEditor(editor, editorSettings, createEditorMethod, createEditorSettingsMethod);
        }

        public static EditorSettingsProvider Default { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorSettingsProvider.<>c <>9 = new EditorSettingsProvider.<>c();
            public static CreateEditorMethod2 <>9__8_0;
            public static CreateEditorSettingsMethod <>9__8_1;
            public static CreateEditorMethod2 <>9__8_2;
            public static CreateEditorSettingsMethod <>9__8_3;
            public static CreateEditorMethod2 <>9__8_4;
            public static CreateEditorSettingsMethod <>9__8_5;
            public static CreateEditorMethod2 <>9__8_6;
            public static CreateEditorSettingsMethod <>9__8_7;
            public static CreateEditorMethod2 <>9__8_8;
            public static CreateEditorSettingsMethod <>9__8_9;
            public static CreateEditorMethod2 <>9__8_10;
            public static CreateEditorSettingsMethod <>9__8_11;
            public static CreateEditorMethod2 <>9__8_12;
            public static CreateEditorSettingsMethod <>9__8_13;
            public static CreateEditorMethod2 <>9__8_14;
            public static CreateEditorSettingsMethod <>9__8_15;
            public static CreateEditorMethod2 <>9__8_16;
            public static CreateEditorSettingsMethod <>9__8_17;
            public static CreateEditorMethod2 <>9__8_18;
            public static CreateEditorSettingsMethod <>9__8_19;
            public static CreateEditorMethod2 <>9__8_20;
            public static CreateEditorSettingsMethod <>9__8_21;
            public static CreateEditorMethod2 <>9__8_22;
            public static CreateEditorSettingsMethod <>9__8_23;
            public static CreateEditorMethod2 <>9__8_24;
            public static CreateEditorSettingsMethod <>9__8_25;
            public static CreateEditorMethod2 <>9__8_26;
            public static CreateEditorSettingsMethod <>9__8_27;
            public static CreateEditorMethod2 <>9__8_28;
            public static CreateEditorSettingsMethod <>9__8_29;
            public static CreateEditorMethod2 <>9__8_30;
            public static CreateEditorSettingsMethod <>9__8_31;
            public static CreateEditorMethod2 <>9__8_32;
            public static CreateEditorSettingsMethod <>9__8_33;
            public static CreateEditorMethod2 <>9__8_34;
            public static CreateEditorSettingsMethod <>9__8_35;
            public static CreateEditorMethod2 <>9__8_36;
            public static CreateEditorSettingsMethod <>9__8_37;
            public static CreateEditorMethod2 <>9__8_38;
            public static CreateEditorSettingsMethod <>9__8_39;
            public static CreateEditorMethod2 <>9__8_40;
            public static CreateEditorSettingsMethod <>9__8_41;
            public static CreateEditorMethod2 <>9__8_42;
            public static CreateEditorSettingsMethod <>9__8_43;
            public static CreateEditorMethod2 <>9__8_44;
            public static CreateEditorSettingsMethod <>9__8_45;
            public static CreateEditorMethod2 <>9__8_46;
            public static CreateEditorSettingsMethod <>9__8_47;

            internal IBaseEdit <RegisterEditors>b__8_0(bool optimized) => 
                new PopupColorEdit();

            internal BaseEditSettings <RegisterEditors>b__8_1() => 
                new PopupColorEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_10(bool optimized) => 
                new PasswordBoxEdit();

            internal BaseEditSettings <RegisterEditors>b__8_11() => 
                new PasswordBoxEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_12(bool optimized) => 
                new TrackBarEdit();

            internal BaseEditSettings <RegisterEditors>b__8_13() => 
                new TrackBarEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_14(bool optimized) => 
                new RatingEdit();

            internal BaseEditSettings <RegisterEditors>b__8_15() => 
                new RatingEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_16(bool optimized) => 
                new ImageEdit();

            internal BaseEditSettings <RegisterEditors>b__8_17() => 
                new ImageEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_18(bool optimized) => 
                new PopupImageEdit();

            internal BaseEditSettings <RegisterEditors>b__8_19() => 
                new PopupImageEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_2(bool optimized) => 
                new ColorEdit();

            internal IBaseEdit <RegisterEditors>b__8_20(bool optimized) => 
                new ProgressBarEdit();

            internal BaseEditSettings <RegisterEditors>b__8_21() => 
                new ProgressBarEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_22(bool optimized) => 
                new FontEdit();

            internal BaseEditSettings <RegisterEditors>b__8_23() => 
                new FontEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_24(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new CheckEdit());

            internal BaseEditSettings <RegisterEditors>b__8_25() => 
                new CheckEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_26(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new ButtonEdit());

            internal BaseEditSettings <RegisterEditors>b__8_27() => 
                new ButtonEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_28(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new PopupCalcEdit());

            internal BaseEditSettings <RegisterEditors>b__8_29() => 
                new CalcEditSettings();

            internal BaseEditSettings <RegisterEditors>b__8_3() => 
                new ColorEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_30(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new TextEdit());

            internal BaseEditSettings <RegisterEditors>b__8_31() => 
                new TextEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_32(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new ComboBoxEdit());

            internal BaseEditSettings <RegisterEditors>b__8_33() => 
                new ComboBoxEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_34(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new DateEdit());

            internal BaseEditSettings <RegisterEditors>b__8_35() => 
                new DateEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_36(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new SpinEdit());

            internal BaseEditSettings <RegisterEditors>b__8_37() => 
                new SpinEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_38(bool optimized) => 
                new SparklineEdit();

            internal BaseEditSettings <RegisterEditors>b__8_39() => 
                new SparklineEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_4(bool optimized) => 
                new ListBoxEdit();

            internal IBaseEdit <RegisterEditors>b__8_40(bool optimized) => 
                new BarCodeEdit();

            internal BaseEditSettings <RegisterEditors>b__8_41() => 
                new BarCodeEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_42(bool optimized) => 
                new HyperlinkEdit();

            internal BaseEditSettings <RegisterEditors>b__8_43() => 
                new HyperlinkEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_44(bool optimized) => 
                new ToggleSwitchEdit();

            internal BaseEditSettings <RegisterEditors>b__8_45() => 
                new ToggleSwitchEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_46(bool optimized) => 
                new AutoSuggestEdit();

            internal BaseEditSettings <RegisterEditors>b__8_47() => 
                new AutoSuggestEditSettings();

            internal BaseEditSettings <RegisterEditors>b__8_5() => 
                new ListBoxEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_6(bool optimized) => 
                new MemoEdit();

            internal BaseEditSettings <RegisterEditors>b__8_7() => 
                new MemoEditSettings();

            internal IBaseEdit <RegisterEditors>b__8_8(bool optimized) => 
                optimized ? ((IBaseEdit) new InplaceBaseEdit(true)) : ((IBaseEdit) new PopupBaseEdit());

            internal BaseEditSettings <RegisterEditors>b__8_9() => 
                new PopupBaseEditSettings();
        }
    }
}

