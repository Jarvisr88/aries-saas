namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LayerSeparators : ElementPool<LayerSeparator>
    {
        public const int SeparatorZIndex = -100;
        public const int TopMostSeparatorZIndex = 100;
        private bool _AreInteractive;
        private bool _AreVisible;

        public LayerSeparators(Panel container) : base(container)
        {
        }

        public LayerSeparator Add(Orientation kind, Rect bounds)
        {
            LayerSeparator separator = base.Add();
            separator.Kind = kind;
            separator.Measure(SizeHelper.Infinite);
            separator.Arrange(bounds);
            return separator;
        }

        public void BringToFront()
        {
            foreach (LayerSeparator separator in base.Items)
            {
                separator.SetZIndex(100);
            }
        }

        private void CheckStandardItem()
        {
            if (this.AreVisible)
            {
                this.CreateStandardItem();
            }
            else
            {
                this.DestroyStandardItem();
            }
        }

        protected override LayerSeparator CreateItem()
        {
            LayerSeparator element = base.CreateItem();
            element.IsInteractive = this.AreInteractive;
            element.SetZIndex(-100);
            return element;
        }

        private void CreateStandardItem()
        {
            if (this.StandardItem == null)
            {
                this.StandardItem = this.CreateItem();
                base.Container.Children.Add(this.StandardItem);
            }
        }

        private void DestroyStandardItem()
        {
            if (this.StandardItem != null)
            {
                base.Container.Children.Remove(this.StandardItem);
                this.StandardItem = null;
            }
        }

        [IteratorStateMachine(typeof(<GetInternalElements>d__7))]
        public IEnumerable<UIElement> GetInternalElements()
        {
            <GetInternalElements>d__7 d__1 = new <GetInternalElements>d__7(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        protected override void OnItemStyleChanged()
        {
            base.OnItemStyleChanged();
            if (this.StandardItem != null)
            {
                this.StandardItem.SetValueIfNotDefault(FrameworkElement.StyleProperty, base.ItemStyle);
            }
        }

        public void SendToBack()
        {
            foreach (LayerSeparator separator in base.Items)
            {
                separator.SetZIndex(-100);
            }
        }

        public bool AreInteractive
        {
            get => 
                this._AreInteractive;
            set
            {
                if (this._AreInteractive != value)
                {
                    this._AreInteractive = value;
                    foreach (LayerSeparator separator in base.Items)
                    {
                        separator.IsInteractive = this.AreInteractive;
                    }
                }
            }
        }

        public bool AreVisible
        {
            get => 
                this._AreVisible;
            set
            {
                if (this.AreVisible != value)
                {
                    this._AreVisible = value;
                    this.CheckStandardItem();
                }
            }
        }

        public double SeparatorThickness =>
            this.StandardItem.Thickness;

        protected LayerSeparator StandardItem { get; private set; }

        [CompilerGenerated]
        private sealed class <GetInternalElements>d__7 : IEnumerable<UIElement>, IEnumerable, IEnumerator<UIElement>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private UIElement <>2__current;
            private int <>l__initialThreadId;
            public LayerSeparators <>4__this;

            [DebuggerHidden]
            public <GetInternalElements>d__7(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num != 0)
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                }
                else
                {
                    this.<>1__state = -1;
                    if (this.<>4__this.StandardItem != null)
                    {
                        this.<>2__current = this.<>4__this.StandardItem;
                        this.<>1__state = 1;
                        return true;
                    }
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<UIElement> IEnumerable<UIElement>.GetEnumerator()
            {
                LayerSeparators.<GetInternalElements>d__7 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new LayerSeparators.<GetInternalElements>d__7(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<System.Windows.UIElement>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            UIElement IEnumerator<UIElement>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

