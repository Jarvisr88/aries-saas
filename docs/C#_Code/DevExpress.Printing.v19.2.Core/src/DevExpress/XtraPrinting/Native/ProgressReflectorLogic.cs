namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class ProgressReflectorLogic
    {
        private ProgressReflector progressReflector;
        protected bool shouldAutocreateRange;
        private float scaleFactor;
        private float rangeValue;
        private float progressValue;

        public ProgressReflectorLogic(ProgressReflector progressReflector);
        private float GetPositionFromValue(float value);
        private float GetValueFromPosition(float position);
        public virtual void InitializeRange(int maximum);
        public virtual void MaximizeRange();
        internal void Reset();
        public void SetPosition(float value);
        protected void SetPositionCore(int value);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetProgressReflector(ProgressReflector progressReflector);

        public virtual float RangeValue { get; set; }

        public virtual bool CanAutocreateRange { get; set; }

        protected float ProgressValue { get; set; }

        protected float ScaleFactor { get; set; }

        protected Deque<float> Ranges { get; }

        protected int Maximum { get; }

        protected int Position { get; }
    }
}

