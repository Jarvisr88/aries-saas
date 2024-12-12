namespace ActiproSoftware.WinUICore.Rendering
{
    using #H;
    using System;
    using System.Runtime.CompilerServices;

    public class CanvasDrawEventArgs : EventArgs
    {
        public CanvasDrawEventArgs(CanvasDrawContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(#G.#eg(0x62d));
            }
            this.Context = context;
        }

        public CanvasDrawContext Context { get; private set; }
    }
}

