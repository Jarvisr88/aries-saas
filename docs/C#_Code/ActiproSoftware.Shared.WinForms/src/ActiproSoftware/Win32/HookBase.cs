namespace ActiproSoftware.Win32
{
    using #aXd;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class HookBase : IDisposable
    {
        private IntPtr #aue = IntPtr.Zero;
        private #Sqe #bue;
        private const int #F0d = 1;

        private IntPtr #wxe(int #7G, IntPtr #8Ff, IntPtr #Uqb)
        {
            bool flag = this.OnHookCallback(#7G, #8Ff, #Uqb);
            return (((#7G < 0) || ((#7G >= 0) && !flag)) ? #Bi.#Dxe(this.#aue, #7G, #8Ff, #Uqb) : new IntPtr(1));
        }

        internal HookBase(int hookId)
        {
            #Sqe sqe = new #Sqe(this.#wxe);
            this.#bue = sqe;
            this.#aue = #Bi.#Gxe(hookId, this.#bue, IntPtr.Zero, #Bi.#Ixe());
        }

        public void Dispose()
        {
            if (this.#aue != IntPtr.Zero)
            {
                #Bi.#Hxe(this.#aue);
                this.#aue = IntPtr.Zero;
            }
        }

        protected virtual bool OnHookCallback(int code, IntPtr wParam, IntPtr lParam) => 
            false;

        internal delegate IntPtr #Sqe(int code, IntPtr wParam, IntPtr lParam);
    }
}

