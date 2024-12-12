namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public static class MoveTypeExtension
    {
        public static InsertType ToInsertType(this MoveType type) => 
            ((type == MoveType.Right) || (type == MoveType.Bottom)) ? InsertType.After : InsertType.Before;

        public static Orientation ToOrientation(this MoveType type) => 
            ((type == MoveType.Left) || (type == MoveType.Right)) ? Orientation.Horizontal : Orientation.Vertical;
    }
}

