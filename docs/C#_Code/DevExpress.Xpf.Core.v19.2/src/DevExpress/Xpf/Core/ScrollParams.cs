namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Controls.Primitives;

    public class ScrollParams
    {
        private double _Min;
        private double _Max;
        private double _PageSize;
        private double _Position;
        private double _LargeStep;
        private double _SmallStep;

        public event Action<ScrollParams> Change;

        public event ScrollingEventHandler Scrolling;

        public void Assign(ScrollParams value)
        {
            this.Min = value.Min;
            this.Max = value.Max;
            this.PageSize = value.PageSize;
            this.Position = value.Position;
            this.LargeStep = value.LargeStep;
            this.SmallStep = value.SmallStep;
        }

        public void AssignTo(ScrollBar scrollBar)
        {
            scrollBar.Minimum = this.Min;
            scrollBar.Maximum = this.MaxPosition;
            scrollBar.ViewportSize = this.PageSize;
            scrollBar.Value = this.Position;
            scrollBar.SmallChange = this.SmallStep;
            scrollBar.LargeChange = this.LargeStep;
        }

        public double CorrectPosition(double position) => 
            Math.Max(this.Min, Math.Min(position, this.MaxPosition));

        protected virtual void DoChanged()
        {
            if (this.Change != null)
            {
                this.Change(this);
            }
        }

        public void DoLargeStep(bool forward)
        {
            this.DoScroll(ScrollKind.LargeStep, delegate {
                if (forward)
                {
                    this.Position += this.LargeStep;
                }
                else
                {
                    this.Position -= this.LargeStep;
                }
            });
        }

        protected void DoScroll(ScrollKind kind, ScrollProcedure scroll)
        {
            double position = this.Position;
            scroll();
            if (this.Position != position)
            {
                this.DoScrolled(kind);
            }
        }

        protected virtual void DoScrolled(ScrollKind kind)
        {
            if (this.Scrolling != null)
            {
                this.Scrolling(this, kind);
            }
        }

        public void DoSmallStep(bool forward)
        {
            this.DoScroll(ScrollKind.SmallStep, delegate {
                if (forward)
                {
                    this.Position += this.SmallStep;
                }
                else
                {
                    this.Position -= this.SmallStep;
                }
            });
        }

        public void Scroll(double position, bool isRelative)
        {
            this.DoScroll(ScrollKind.ExactPosition, delegate {
                if (isRelative)
                {
                    this.RelativePosition = position;
                }
                else
                {
                    this.Position = position;
                }
            });
        }

        public double Min
        {
            get => 
                this._Min;
            set
            {
                if (this._Min != value)
                {
                    this._Min = value;
                    this.DoChanged();
                }
            }
        }

        public double Max
        {
            get => 
                this._Max;
            set
            {
                if (this._Max != value)
                {
                    this._Max = value;
                    this.DoChanged();
                }
            }
        }

        public double PageSize
        {
            get => 
                this._PageSize;
            set
            {
                if (this._PageSize != value)
                {
                    this._PageSize = value;
                    this.DoChanged();
                }
            }
        }

        public double Position
        {
            get => 
                this.CorrectPosition(this._Position);
            set
            {
                value = Math.Max(this.Min, value);
                value = Math.Min(value, this.Max);
                if (this._Position != value)
                {
                    this._Position = value;
                    this.DoChanged();
                }
            }
        }

        public double SmallStep
        {
            get => 
                (this._SmallStep != 0.0) ? this._SmallStep : Math.Ceiling((double) (this.PageSize / 10.0));
            set => 
                this._SmallStep = value;
        }

        public double LargeStep
        {
            get => 
                (this._LargeStep != 0.0) ? this._LargeStep : this.PageSize;
            set => 
                this._LargeStep = value;
        }

        public bool Enabled =>
            ((this.Max - this.Min) - this.PageSize) >= 1.0;

        public double MaxPosition =>
            Math.Max(this.Min, this.Max - this.PageSize);

        public double RelativePageSize =>
            this.PageSize / (this.Max - this.Min);

        public double RelativePosition
        {
            get => 
                (this.Position - this.Min) / (this.Max - this.Min);
            set => 
                this.Position = Math.Round((double) (this.Min + (value * (this.Max - this.Min))));
        }

        protected delegate void ScrollProcedure();
    }
}

