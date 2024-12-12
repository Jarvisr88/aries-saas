namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    public abstract class ReflectorBarBase : ProgressReflector
    {
        private void EnableVisible();
        protected void Initialize();
        protected override void InitializeRangeCore(int maximum);
        protected abstract void Invalidate(bool withChildren);
        protected override void MaximizeRangeCore();
        internal override void Reset();
        protected internal override void SetPosition(int value);

        public abstract bool Visible { get; set; }
    }
}

