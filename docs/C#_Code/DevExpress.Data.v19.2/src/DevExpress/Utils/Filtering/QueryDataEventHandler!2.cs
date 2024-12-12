namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void QueryDataEventHandler<TEventArgs, TData>(object sender, TEventArgs e) where TEventArgs: QueryDataEventArgs<TData> where TData: MetricAttributesData;
}

