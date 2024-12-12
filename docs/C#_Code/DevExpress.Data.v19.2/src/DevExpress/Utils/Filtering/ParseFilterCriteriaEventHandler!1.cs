namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void ParseFilterCriteriaEventHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs: ParseFilterCriteriaEventArgs;
}

