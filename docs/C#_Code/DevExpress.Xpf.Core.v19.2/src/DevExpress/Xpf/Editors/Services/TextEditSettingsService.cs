namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;
    using System.Runtime.CompilerServices;

    public class TextEditSettingsService : BaseEditingSettingsService
    {
        public TextEditSettingsService(BaseEdit editor) : base(editor)
        {
        }

        private TextEditPropertyProvider PropertyProvider =>
            (TextEditPropertyProvider) base.PropertyProvider;

        private TextEditStrategy EditStrategy =>
            (TextEditStrategy) base.EditStrategy;

        public override bool AllowSpin =>
            this.EditStrategy.AllowSpin;

        public override bool IsInLookUpMode
        {
            get
            {
                Func<bool> fallback = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<bool> local1 = <>c.<>9__7_1;
                    fallback = <>c.<>9__7_1 = () => false;
                }
                return (base.OwnerEdit as LookUpEditBase).Return<LookUpEditBase, bool>(x => (this.PropertyProvider.TextInputSettings.AllowRejectUnknownValues || (!string.IsNullOrEmpty(x.ValueMember) || !string.IsNullOrEmpty(x.DisplayMember))), fallback);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEditSettingsService.<>c <>9 = new TextEditSettingsService.<>c();
            public static Func<bool> <>9__7_1;

            internal bool <get_IsInLookUpMode>b__7_1() => 
                false;
        }
    }
}

