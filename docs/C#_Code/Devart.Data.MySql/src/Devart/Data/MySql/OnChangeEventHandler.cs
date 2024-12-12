namespace Devart.Data.MySql
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void OnChangeEventHandler(object sender, MySqlTableChangeEventArgs e);
}

