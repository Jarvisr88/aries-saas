namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void QueryFilterCriteriaEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs: QueryFilterCriteriaEventArgs;
}

