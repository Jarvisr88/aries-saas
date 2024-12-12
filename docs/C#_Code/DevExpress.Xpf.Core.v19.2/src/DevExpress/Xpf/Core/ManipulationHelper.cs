namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class ManipulationHelper
    {
        private static Vector emptyVector = new Vector(0.0, 0.0);
        public Point accumulator;

        public ManipulationHelper(IManipulationClient client)
        {
            this.Client = client;
            this.DesiredDeceleration = 960.0;
            this.BoundaryFeedbackTime = 200.0;
            this.AllowVerticalScrolling = true;
        }

        public ManipulationHelper(IScrollInfo scrollInfo) : this(new ScrollInfoManipulationClient(scrollInfo))
        {
        }

        private Vector GetOverDelta()
        {
            Vector vector = new Vector();
            if (this.AllowHorizontalScrolling && (this.ScrollValue.X < this.Client.GetMinScrollValue().X))
            {
                vector.X = this.Client.GetMinScrollValue().X - this.ScrollValue.X;
            }
            if (this.AllowVerticalScrolling && (this.ScrollValue.Y < this.Client.GetMinScrollValue().Y))
            {
                vector.Y = this.Client.GetMinScrollValue().Y - this.ScrollValue.Y;
            }
            if (this.AllowHorizontalScrolling && (this.ScrollValue.X > this.Client.GetMaxScrollValue().X))
            {
                vector.X = this.Client.GetMaxScrollValue().X - this.ScrollValue.X;
            }
            if (this.AllowVerticalScrolling && (this.ScrollValue.Y > this.Client.GetMaxScrollValue().Y))
            {
                vector.Y = this.Client.GetMaxScrollValue().Y - this.ScrollValue.Y;
            }
            return vector;
        }

        private Vector GetOverDelta2(double t)
        {
            Vector vector = new Vector();
            if (this.AllowHorizontalScrolling)
            {
                vector.X = (this.MaxOverDelta.X + (this.Velocity.X * t)) - ((this.BackDeceleration.X * t) * t);
            }
            if (this.AllowVerticalScrolling)
            {
                vector.Y = (this.MaxOverDelta.Y + (this.Velocity.Y * t)) - ((this.BackDeceleration.Y * t) * t);
            }
            return vector;
        }

        public virtual void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            Vector minScrollDelta;
            this.ScrollValue = new Vector(this.ScrollValue.X - e.DeltaManipulation.Translation.X, this.ScrollValue.Y - e.DeltaManipulation.Translation.Y);
            PointHelper.Offset(ref this.accumulator, e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            IManipulationClientEx client = this.Client as IManipulationClientEx;
            if (client != null)
            {
                if (!this.AllowHorizontalScrolling || (client.GetMinScrollDelta().X <= Math.Abs(this.accumulator.X)))
                {
                    if (this.AllowVerticalScrolling)
                    {
                        minScrollDelta = client.GetMinScrollDelta();
                        if (minScrollDelta.Y > Math.Abs(this.accumulator.Y))
                        {
                            goto TR_0000;
                        }
                    }
                }
                else
                {
                    goto TR_0000;
                }
            }
            this.Client.Scroll(this.ScrollValue);
            if (!this.IsObjectBounded)
            {
                if ((this.AllowHorizontalScrolling && ((this.ScrollValue.X > this.Client.GetMaxScrollValue().X) || (this.ScrollValue.X < this.Client.GetMinScrollValue().X))) || (this.AllowVerticalScrolling && ((this.ScrollValue.Y > this.Client.GetMaxScrollValue().Y) || (this.ScrollValue.Y < this.Client.GetMinScrollValue().Y))))
                {
                    this.BoundTicks = DateTime.Now.Ticks;
                    this.MaxOverDelta = new Vector(0.0, 0.0);
                    this.IsObjectBounded = true;
                }
            }
            else
            {
                long num = (DateTime.Now.Ticks - this.BoundTicks) / 0x2710L;
                if (num >= this.BoundaryFeedbackTime)
                {
                    e.Complete();
                }
                else
                {
                    Vector overDelta = this.GetOverDelta();
                    if (num > (this.BoundaryFeedbackTime * 0.5))
                    {
                        if (this.MaxOverDelta == emptyVector)
                        {
                            this.MaxOverDelta = overDelta;
                            this.Velocity = e.Velocities.LinearVelocity;
                            minScrollDelta = this.Velocity;
                            this.BackDeceleration = new Vector((this.MaxOverDelta.X + (this.Velocity.X * (this.BoundaryFeedbackTime * 0.5))) / ((0.25 * this.BoundaryFeedbackTime) * this.BoundaryFeedbackTime), (this.MaxOverDelta.Y + (minScrollDelta.Y * (this.BoundaryFeedbackTime * 0.5))) / ((0.25 * this.BoundaryFeedbackTime) * this.BoundaryFeedbackTime));
                        }
                        overDelta = this.GetOverDelta2(num - (this.BoundaryFeedbackTime * 0.5));
                    }
                    minScrollDelta = new Vector();
                    minScrollDelta = new Vector();
                    e.ReportBoundaryFeedback(new ManipulationDelta(overDelta, 0.0, minScrollDelta, minScrollDelta));
                }
            }
            e.Handled = true;
            return;
        TR_0000:
            e.Handled = true;
        }

        public virtual void OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = this.DesiredDeceleration / 1000000.0;
            e.Handled = true;
        }

        public virtual void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.Client.GetManipulationContainer();
            this.ScrollValue = this.Client.GetScrollValue();
            this.IsObjectBounded = false;
            this.accumulator = new Point();
            e.Handled = true;
        }

        public bool AllowVerticalScrolling { get; set; }

        public bool AllowHorizontalScrolling { get; set; }

        public double BoundaryFeedbackTime { get; set; }

        public double DesiredDeceleration { get; set; }

        private IManipulationClient Client { get; set; }

        protected long BoundTicks { get; private set; }

        private Vector ScrollValue { get; set; }

        protected Vector MaxOverDelta { get; private set; }

        protected Vector Velocity { get; private set; }

        protected Vector BackDeceleration { get; private set; }

        protected bool IsObjectBounded { get; private set; }
    }
}

