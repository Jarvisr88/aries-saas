namespace DevExpress.Xpf.Editors
{
    using DevExpress.XtraPrinting.BarCode;
    using System;

    public abstract class BarCodeStyleSettings<T> : BarCodeStyleSettings where T: BarCodeGeneratorBase, new()
    {
        private T barCodeGeneratorBase;

        public BarCodeStyleSettings()
        {
            this.barCodeGeneratorBase = Activator.CreateInstance<T>();
        }

        public override BarCodeGeneratorBase GeneratorBase =>
            this.Generator;

        protected T Generator =>
            this.barCodeGeneratorBase;
    }
}

