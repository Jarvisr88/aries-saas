namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class RenderStrategy
    {
        private string state;
        private DevExpress.Xpf.Core.Native.RenderInfo renderInfo;
        private IEnumerable<IRenderInfo> currentRenderInfos;
        private ChromeStateProviderBase stateProvider;
        private FrameworkElement stateSource;
        private Rect bounds;

        public event EventHandler RequestRender;

        public event ValueChangedEventHandler<string> StateChanged;

        public RenderStrategy();
        protected internal IEnumerable<string> GetParentStates();
        protected virtual IRenderInfo GetRenderInfo(string state);
        protected void InitializeStateProviderSource();
        protected virtual void OnBoundsChanged(Rect oldValue);
        protected virtual void OnStateChanged(string oldValue);
        protected virtual void OnStateProviderChanged(ChromeStateProviderBase oldValue);
        protected virtual void OnStateSourceChanged(FrameworkElement oldValue);
        protected void RaiseRequestRender();
        public void Render(DrawingContext dc);
        public void Render(DrawingContext dc, Rect bounds);

        public string State { get; set; }

        public DevExpress.Xpf.Core.Native.RenderInfo RenderInfo { get; set; }

        public ChromeStateProviderBase StateProvider { get; set; }

        public FrameworkElement StateSource { get; set; }

        public Rect Bounds { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderStrategy.<>c <>9;
            public static Func<IRenderInfo, int> <>9__28_1;

            static <>c();
            internal int <OnStateChanged>b__28_1(IRenderInfo x);
        }
    }
}

