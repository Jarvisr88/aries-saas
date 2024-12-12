namespace ActiproSoftware.WinUICore.Input
{
    using #H;
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class InputAdapter
    {
        private InputAdapterEventKinds #2ni;
        private WeakReference #3ni;

        public event EventHandler<InputPointerEventArgs> PointerCaptureLost;

        public event EventHandler<InputPointerEventArgs> PointerEntered;

        public event EventHandler<InputPointerEventArgs> PointerExited;

        public event EventHandler<InputPointerEventArgs> PointerMoved;

        public event EventHandler<InputPointerButtonEventArgs> PointerPressed;

        public event EventHandler<InputPointerButtonEventArgs> PointerReleased;

        public event EventHandler<InputPointerWheelEventArgs> PointerWheelChanged;

        private void #koi(object #xhb, EventArgs #yhb)
        {
            IInputElement originalSource = #xhb as IInputElement;
            if ((originalSource != null) && !originalSource.IsMouseCaptured)
            {
                EventHandler<InputPointerEventArgs> handler = this.#8ni;
                if (handler != null)
                {
                    InputPointerEventArgs e = new InputPointerEventArgs(originalSource, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                    handler(#xhb, e);
                }
            }
        }

        private void #loi(object #xhb, EventArgs #yhb)
        {
            EventHandler<InputPointerEventArgs> handler = this.#9ni;
            if (handler != null)
            {
                handler(#xhb, new InputPointerEventArgs(this.TargetElement, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0)));
            }
        }

        private void #loi(object #xhb, MouseEventArgs #yhb)
        {
            EventHandler<InputPointerEventArgs> handler = this.#9ni;
            if (handler != null)
            {
                handler(#xhb, new InputPointerEventArgs(this.TargetElement, #yhb));
            }
        }

        private void #moi(object #xhb, EventArgs #yhb)
        {
            EventHandler<InputPointerEventArgs> handler = this.#aoi;
            if (handler != null)
            {
                handler(#xhb, new InputPointerEventArgs(this.TargetElement, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0)));
            }
        }

        private void #moi(object #xhb, MouseEventArgs #yhb)
        {
            EventHandler<InputPointerEventArgs> handler = this.#aoi;
            if (handler != null)
            {
                handler(#xhb, new InputPointerEventArgs(this.TargetElement, #yhb));
            }
        }

        private static InputPointerButtonKind #N5j(MouseEventArgs #yhb)
        {
            MouseButtons button;
            if (2 == 0)
            {
                MouseButtons button = #yhb.Button;
            }
            else
            {
                button = #yhb.Button;
            }
            if (button <= MouseButtons.Right)
            {
                if (button == MouseButtons.Left)
                {
                    return InputPointerButtonKind.Primary;
                }
                if (button == MouseButtons.Right)
                {
                    return InputPointerButtonKind.Secondary;
                }
            }
            else
            {
                if (button == MouseButtons.Middle)
                {
                    return InputPointerButtonKind.Middle;
                }
                if (button == MouseButtons.XButton1)
                {
                    return InputPointerButtonKind.Extended1;
                }
                if (button == MouseButtons.XButton2)
                {
                    return InputPointerButtonKind.Extended2;
                }
            }
            return InputPointerButtonKind.Other;
        }

        private void #noi(object #xhb, MouseEventArgs #yhb)
        {
            EventHandler<InputPointerEventArgs> handler = this.#Kni;
            if (handler != null)
            {
                handler(#xhb, new InputPointerEventArgs(this.TargetElement, #yhb));
            }
        }

        private void #ooi(object #xhb, MouseEventArgs #yhb)
        {
            InputPointerButtonKind buttonKind = #N5j(#yhb);
            EventHandler<InputPointerButtonEventArgs> handler = this.#boi;
            if (handler != null)
            {
                handler(#xhb, new InputPointerButtonEventArgs(this.TargetElement, #yhb, buttonKind));
            }
        }

        private void #poi(object #xhb, MouseEventArgs #yhb)
        {
            InputPointerButtonKind buttonKind = #N5j(#yhb);
            EventHandler<InputPointerButtonEventArgs> handler = this.#Lni;
            if (handler != null)
            {
                handler(#xhb, new InputPointerButtonEventArgs(this.TargetElement, #yhb, buttonKind));
            }
        }

        private void #qoi(object #xhb, MouseEventArgs #yhb)
        {
            EventHandler<InputPointerWheelEventArgs> handler = this.#Mni;
            if (handler != null)
            {
                handler(#xhb, new InputPointerWheelEventArgs(this.TargetElement, #yhb));
            }
        }

        public InputAdapter(IInputElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(#G.#eg(0x5d7));
            }
            this.#3ni = new WeakReference(element);
        }

        public bool CapturePointer(InputPointerEventArgs e) => 
            this.CapturePointer(e, this.TargetElement);

        public bool CapturePointer(InputPointerEventArgs e, IInputElement targetElement)
        {
            if ((e == null) || ((targetElement == null) || (e.DeviceKind != InputDeviceKind.Mouse)))
            {
                return false;
            }
            targetElement.CaptureMouse();
            return true;
        }

        public bool IsAttachedTo(InputAdapterEventKinds kind) => 
            (this.#2ni & kind) == kind;

        public void ReleasePointerCaptures()
        {
            IInputElement targetElement = this.TargetElement;
            if ((targetElement != null) && targetElement.IsMouseCaptured)
            {
                targetElement.ReleaseMouseCapture();
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public InputAdapterEventKinds AttachedEventKinds
        {
            get => 
                this.#2ni;
            set
            {
                if (this.#2ni != value)
                {
                    Control targetElement = this.TargetElement as Control;
                    UIElement element = this.TargetElement as UIElement;
                    if ((targetElement == null) && (element == null))
                    {
                        value = InputAdapterEventKinds.None;
                    }
                    if (this.IsAttached)
                    {
                        if (targetElement != null)
                        {
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerCaptureLost))
                            {
                                targetElement.MouseCaptureChanged -= new EventHandler(this.#koi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerEntered))
                            {
                                targetElement.MouseEnter -= new EventHandler(this.#loi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerExited))
                            {
                                targetElement.MouseLeave -= new EventHandler(this.#moi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerMoved))
                            {
                                targetElement.MouseMove -= new MouseEventHandler(this.#noi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerPressed))
                            {
                                targetElement.MouseDown -= new MouseEventHandler(this.#ooi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerReleased))
                            {
                                targetElement.MouseUp -= new MouseEventHandler(this.#poi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerWheelChanged))
                            {
                                targetElement.MouseWheel -= new MouseEventHandler(this.#qoi);
                            }
                        }
                        if (element != null)
                        {
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerCaptureLost))
                            {
                                UIControl control2 = ((ILogicalTreeNode) element).FindAncestor(typeof(UIControl)) as UIControl;
                                if (control2 != null)
                                {
                                    control2.MouseCaptureChanged -= new EventHandler(this.#koi);
                                }
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerEntered))
                            {
                                element.MouseEnter -= new MouseEventHandler(this.#loi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerExited))
                            {
                                element.MouseLeave -= new MouseEventHandler(this.#moi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerMoved))
                            {
                                element.MouseMove -= new MouseEventHandler(this.#noi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerPressed))
                            {
                                element.MouseDown -= new MouseEventHandler(this.#ooi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerReleased))
                            {
                                element.MouseUp -= new MouseEventHandler(this.#poi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerWheelChanged))
                            {
                                element.MouseWheel -= new MouseEventHandler(this.#qoi);
                            }
                        }
                    }
                    this.#2ni = value;
                    if (this.IsAttached)
                    {
                        if (targetElement != null)
                        {
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerCaptureLost))
                            {
                                targetElement.MouseCaptureChanged += new EventHandler(this.#koi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerEntered))
                            {
                                targetElement.MouseEnter += new EventHandler(this.#loi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerExited))
                            {
                                targetElement.MouseLeave += new EventHandler(this.#moi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerMoved))
                            {
                                targetElement.MouseMove += new MouseEventHandler(this.#noi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerPressed))
                            {
                                targetElement.MouseDown += new MouseEventHandler(this.#ooi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerReleased))
                            {
                                targetElement.MouseUp += new MouseEventHandler(this.#poi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerWheelChanged))
                            {
                                targetElement.MouseWheel += new MouseEventHandler(this.#qoi);
                            }
                        }
                        if (element != null)
                        {
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerCaptureLost))
                            {
                                UIControl control3 = ((ILogicalTreeNode) element).FindAncestor(typeof(UIControl)) as UIControl;
                                if (control3 != null)
                                {
                                    control3.MouseCaptureChanged += new EventHandler(this.#koi);
                                }
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerEntered))
                            {
                                element.MouseEnter += new MouseEventHandler(this.#loi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerExited))
                            {
                                element.MouseLeave += new MouseEventHandler(this.#moi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerMoved))
                            {
                                element.MouseMove += new MouseEventHandler(this.#noi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerPressed))
                            {
                                element.MouseDown += new MouseEventHandler(this.#ooi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerReleased))
                            {
                                element.MouseUp += new MouseEventHandler(this.#poi);
                            }
                            if (this.IsAttachedTo(InputAdapterEventKinds.PointerWheelChanged))
                            {
                                element.MouseWheel += new MouseEventHandler(this.#qoi);
                            }
                        }
                    }
                }
            }
        }

        public bool IsAttached =>
            this.#2ni != InputAdapterEventKinds.None;

        public IInputElement TargetElement =>
            ((this.#3ni == null) || !this.#3ni.IsAlive) ? null : (this.#3ni.Target as IInputElement);
    }
}

