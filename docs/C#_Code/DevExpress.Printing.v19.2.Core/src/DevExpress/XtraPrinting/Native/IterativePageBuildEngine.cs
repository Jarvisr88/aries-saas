namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class IterativePageBuildEngine : PageBuildEngine
    {
        private HashSet<long> pages;
        protected YPageContentEngine pageContentEngine;

        public event Action0 AfterBuild;

        public IterativePageBuildEngine(PrintingDocument document);
        public override void Abort();
        protected override void Build();
        protected virtual void BuildPage();
        private void BuildPageCore();
        public virtual int GetBufferSize(int count);
        protected void Iterate();
        public void OnAfterBuild();
        protected virtual void StopIteration();

        protected bool CanIterate { get; set; }
    }
}

