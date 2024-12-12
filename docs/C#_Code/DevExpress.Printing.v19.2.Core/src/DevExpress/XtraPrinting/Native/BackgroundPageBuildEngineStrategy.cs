namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BackgroundPageBuildEngineStrategy
    {
        private static readonly Pair<int, int>[] bufferSizes;

        public abstract event EventHandler Tick;

        static BackgroundPageBuildEngineStrategy();
        protected BackgroundPageBuildEngineStrategy();
        public abstract void BeginInvoke(Action0 method);
        public static int CalcBufferSize(int count);
        public virtual int GetBufferSize(int count);
    }
}

