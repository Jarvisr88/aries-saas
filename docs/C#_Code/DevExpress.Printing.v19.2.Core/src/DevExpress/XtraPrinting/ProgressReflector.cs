namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    public class ProgressReflector
    {
        private Deque<float> ranges = new Deque<float>();
        private int fPosition;
        protected int fMaximum = 100;
        private ProgressReflectorLogic logic;
        private int updates;
        private EventHandler onPositionChanged;

        [Description("Occurs after the position of the current range has been changed.")]
        public event EventHandler PositionChanged
        {
            add
            {
                this.onPositionChanged = (this.onPositionChanged + value) as EventHandler;
            }
            remove
            {
                this.onPositionChanged = (this.onPositionChanged - value) as EventHandler;
            }
        }

        protected virtual void BeginUpdate()
        {
            this.updates++;
        }

        private float CalcSum(float[] values)
        {
            float num = 0f;
            for (int i = 0; i < values.Length; i++)
            {
                num += values[i];
            }
            return num;
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector property instead.")]
        public static void DisableReflector()
        {
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector property instead.")]
        public static void EnableReflector()
        {
        }

        protected virtual void EndUpdate()
        {
            this.updates--;
        }

        public void EnsureRangeDecrement(Action0 action)
        {
            int rangeCount = this.RangeCount;
            try
            {
                action();
            }
            finally
            {
                if (rangeCount == this.RangeCount)
                {
                    this.MaximizeRange();
                }
            }
        }

        internal void FinalizeRangeCount()
        {
            this.BeginUpdate();
            try
            {
                if (this.RangeCount > 1)
                {
                    float item = this.Ranges.PopBack();
                    this.Ranges.Clear();
                    this.Ranges.PushBack(item);
                    this.Logic.SetPosition(this.Maximum - item);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        internal void IncrementRangeValue(int value)
        {
            for (int i = 0; i < value; i++)
            {
                float rangeValue = this.RangeValue;
                this.RangeValue = rangeValue + 1f;
            }
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector.InitializeRange method instead.")]
        public static void Initialize(int maximum)
        {
        }

        public virtual void InitializeRange(int maximum)
        {
            if ((this.Ranges.Count != 0) || this.CanAutocreateRange)
            {
                this.Logic.InitializeRange(maximum);
                this.InitializeRangeCore(maximum);
            }
        }

        protected virtual void InitializeRangeCore(int maximum)
        {
        }

        public virtual void MaximizeRange()
        {
            if ((this.Ranges.Count != 0) || this.CanAutocreateRange)
            {
                this.Logic.MaximizeRange();
                this.MaximizeRangeCore();
            }
        }

        protected virtual void MaximizeRangeCore()
        {
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector.MaximizeRange method instead.")]
        public static void MaximizeValue()
        {
        }

        private void RaisePositionChanged(ProgressReflector sender, EventArgs e)
        {
            if (this.onPositionChanged != null)
            {
                this.onPositionChanged(sender, e);
            }
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector property instead. To see an updated example, refer to http://www.devexpress.com/example=E906.")]
        public static void RegisterReflector(ProgressReflector value)
        {
        }

        internal virtual void Reset()
        {
            this.ranges.Clear();
            this.Logic.Reset();
            this.PositionCore = 0;
        }

        protected internal virtual void SetPosition(int value)
        {
            this.PositionCore = value;
            this.RaisePositionChanged(this, EventArgs.Empty);
        }

        public virtual void SetProgressRanges(float[] ranges)
        {
            if (ranges == null)
            {
                throw new ArgumentException("ranges");
            }
            this.Reset();
            float num = this.CalcSum(ranges);
            for (int i = 0; i < ranges.Length; i++)
            {
                this.ranges.PushBack((ranges[i] * this.MaximumCore) / num);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetProgressRanges(float[] ranges, ProgressReflectorLogic logic)
        {
            this.SetProgressRanges(ranges);
            this.logic = logic;
            this.Logic.SetProgressReflector(this);
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector.SetProgressRanges method instead.")]
        public static void SetRanges(float[] ranges)
        {
        }

        [Obsolete("This method is now obsolete. You should use the PrintingSystemBase.ProgressReflector property instead. To see an updated example, refer to http://www.devexpress.com/example=E906.")]
        public static void UnregisterReflector(ProgressReflector value)
        {
        }

        [Description("Gets or sets a value which reflects the state of a process being tracked by this ProgressReflector."), Obsolete("This property is now obsolete. You should use the PrintingSystemBase.ProgressReflector.RangeValue property instead.")]
        public static float Value
        {
            get => 
                0f;
            set
            {
            }
        }

        protected bool Updating =>
            this.updates > 0;

        public ProgressReflectorLogic Logic
        {
            get
            {
                this.logic ??= new ProgressReflectorLogic(this);
                return this.logic;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                this.logic = value;
            }
        }

        public int RangeCount =>
            this.Ranges.Count;

        protected internal Deque<float> Ranges =>
            this.ranges;

        protected internal virtual int PositionCore
        {
            get => 
                this.fPosition;
            set => 
                this.fPosition = value;
        }

        protected internal virtual int MaximumCore
        {
            get => 
                this.fMaximum;
            set => 
                this.fMaximum = value;
        }

        public int Position =>
            this.PositionCore;

        public int Maximum =>
            this.MaximumCore;

        [Description("Gets or sets a value which reflects the state of a process within the current range.")]
        public virtual float RangeValue
        {
            get => 
                this.Logic.RangeValue;
            set => 
                this.Logic.RangeValue = value;
        }

        public bool CanAutocreateRange
        {
            get => 
                this.Logic.CanAutocreateRange;
            set => 
                this.Logic.CanAutocreateRange = value;
        }
    }
}

