namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    internal class DecoratorData<TDecoratorService> : DecoratorDataBase
    {
        public DecoratorData(Func<TDecorator> createDecoratorCallback, Func<TDecorator, TDecoratorService> createServiceCallback, Func<TDecoratorService> getNullServiceCallback);
        public override IBarNameScopeDecorator CreateDecorator();
        public override object CreateService(object decorator);
        public override object GetNullService();

        private Func<TDecorator> CreateDecoratorCallback { get; set; }

        private Func<TDecorator, TDecoratorService> CreateServiceCallback { get; set; }

        private Func<TDecoratorService> GetNullServiceCallback { get; set; }
    }
}

