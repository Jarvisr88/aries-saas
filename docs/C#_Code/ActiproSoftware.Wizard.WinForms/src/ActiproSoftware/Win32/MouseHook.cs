namespace ActiproSoftware.Win32
{
    using #aXd;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class MouseHook : HookBase
    {
        private IMouseHookCallback #cue;

        public MouseHook(IMouseHookCallback hookOwner) : base(7)
        {
            this.#cue = hookOwner;
        }

        protected override bool OnHookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == 0)
            {
                #Bi.#Uqe uqe = (#Bi.#Uqe) Marshal.PtrToStructure(lParam, Type.GetTypeFromHandle(typeof(#Bi.#Uqe).TypeHandle));
                MouseHookEventArgs e = new MouseHookEventArgs(uqe.#Qqb, wParam.ToInt32(), uqe.#Wue, new Point(uqe.#Vue.#Zn, uqe.#Vue.#0n), uqe.#fue);
                this.#cue.OnMouseHookEvent(e);
                if (e.Handled)
                {
                    return true;
                }
            }
            return base.OnHookCallback(code, wParam, lParam);
        }
    }
}

