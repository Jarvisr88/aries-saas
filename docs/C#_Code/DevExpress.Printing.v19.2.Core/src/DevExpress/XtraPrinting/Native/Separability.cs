namespace DevExpress.XtraPrinting.Native
{
    using System;

    [Flags]
    public enum Separability
    {
        public const Separability None = Separability.None;,
        public const Separability Horz = Separability.Horz;,
        public const Separability HorzForced = Separability.HorzForced;,
        public const Separability Vert = Separability.Vert;,
        public const Separability VertForced = Separability.VertForced;,
        public const Separability VertHorz = Separability.VertHorz;,
        public const Separability VertHorzForced = Separability.VertHorzForced;
    }
}

