﻿namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderBadgeStub : RenderControlBase
    {
        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new RenderBadgeStubContext(this);

        protected override FrameworkElement CreateFrameworkElement(FrameworkRenderElementContext context) => 
            null;

        [IteratorStateMachine(typeof(<GetChildren>d__2))]
        protected override IEnumerable<IFrameworkRenderElement> GetChildren() => 
            new <GetChildren>d__2(-2);

        [CompilerGenerated]
        private sealed class <GetChildren>d__2 : IEnumerable<IFrameworkRenderElement>, IEnumerable, IEnumerator<IFrameworkRenderElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IFrameworkRenderElement <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetChildren>d__2(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IFrameworkRenderElement> IEnumerable<IFrameworkRenderElement>.GetEnumerator()
            {
                RenderBadgeStub.<GetChildren>d__2 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new RenderBadgeStub.<GetChildren>d__2(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.Core.Native.IFrameworkRenderElement>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IFrameworkRenderElement IEnumerator<IFrameworkRenderElement>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}
