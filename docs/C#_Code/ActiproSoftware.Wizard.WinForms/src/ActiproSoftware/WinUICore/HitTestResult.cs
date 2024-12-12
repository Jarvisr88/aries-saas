namespace ActiproSoftware.WinUICore
{
    using System;

    public abstract class HitTestResult
    {
        private IUIElement #irb;

        public HitTestResult(IUIElement element)
        {
            this.#irb = element;
        }

        public IUIElement Element =>
            this.#irb;
    }
}

