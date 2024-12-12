namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    internal class DecoratorData : DecoratorDataBase
    {
        public DecoratorData(Func<TDecorator> createDecoratorCallback);
        public override IBarNameScopeDecorator CreateDecorator();
        public override object CreateService(object decorator);
        public override object GetNullService();

        private Func<TDecorator> CreateDecoratorCallback { get; set; }
    }
}

