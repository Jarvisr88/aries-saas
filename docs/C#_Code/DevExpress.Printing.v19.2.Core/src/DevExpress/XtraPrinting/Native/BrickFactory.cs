namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class BrickFactory
    {
        private static readonly Dictionary<string, IBrickFactory> factories;
        private static readonly IBrickFactory DefaultFactory;

        public static event BrickResolveEventHandler BrickResolve;

        static BrickFactory();
        internal static Brick CreateBrick(XtraItemEventArgs e);
        internal static Brick CreateBrick(string brickType);
        internal static BrickStyle CreateBrickStyle(XtraItemEventArgs e);
        internal static string GetStringProperty(XtraItemEventArgs e, string propertyName);
        private static Brick RaiseBrickResolve(string brickType);
        public static void RegisterFactory(string brickType, IBrickFactory factory);
        public static void SetFactory(string brickType, IBrickFactory factory);
    }
}

