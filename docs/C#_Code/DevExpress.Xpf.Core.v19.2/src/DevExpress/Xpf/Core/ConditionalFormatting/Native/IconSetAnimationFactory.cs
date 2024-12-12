namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class IconSetAnimationFactory : ConditionalAnimationFactoryBase
    {
        private const double FadeOutOpacity = 0.0;
        private const double FadeInOpacity = 1.0;

        [IteratorStateMachine(typeof(<CreateIconOpacityAnimationFactories>d__7))]
        private IEnumerable<DoubleAnimationFactory> CreateIconOpacityAnimationFactories(double[] opacities)
        {
            <CreateIconOpacityAnimationFactories>d__7 d__1 = new <CreateIconOpacityAnimationFactories>d__7(-2);
            d__1.<>4__this = this;
            d__1.<>3__opacities = opacities;
            return d__1;
        }

        protected override FormatConditionBaseInfo GetConditionInfo() => 
            this.Condition;

        protected internal override List<ITimelineFactory> GetTimelineFormatFactories()
        {
            List<ITimelineFactory> timelineFormatFactories = base.GetTimelineFormatFactories();
            double[] numArray1 = new double[2];
            numArray1[1] = 1.0;
            double[] opacities = numArray1;
            foreach (DoubleAnimationFactory factory in this.CreateIconOpacityAnimationFactories(opacities))
            {
                timelineFormatFactories.Add(factory);
            }
            return timelineFormatFactories;
        }

        public FormatConditionBaseInfo Condition { get; set; }

        [CompilerGenerated]
        private sealed class <CreateIconOpacityAnimationFactories>d__7 : IEnumerable<DoubleAnimationFactory>, IEnumerable, IEnumerator<DoubleAnimationFactory>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private DoubleAnimationFactory <>2__current;
            private int <>l__initialThreadId;
            public IconSetAnimationFactory <>4__this;
            private double[] opacities;
            public double[] <>3__opacities;
            private int <i>5__1;

            [DebuggerHidden]
            public <CreateIconOpacityAnimationFactories>d__7(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.opacities.Length)
                {
                    return false;
                }
                DoubleAnimationFactory factory = new DoubleAnimationFactory(this.<>4__this.AnimationSettings, AnimationPropertyPaths.CreateIconOpacityPath(), this.opacities[this.<i>5__1]) {
                    DurationMultiplicator = 0.5,
                    Generation = this.<i>5__1
                };
                this.<>2__current = factory;
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            IEnumerator<DoubleAnimationFactory> IEnumerable<DoubleAnimationFactory>.GetEnumerator()
            {
                IconSetAnimationFactory.<CreateIconOpacityAnimationFactories>d__7 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new IconSetAnimationFactory.<CreateIconOpacityAnimationFactories>d__7(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.opacities = this.<>3__opacities;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.ConditionalFormatting.Native.DoubleAnimationFactory>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            DoubleAnimationFactory IEnumerator<DoubleAnimationFactory>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

