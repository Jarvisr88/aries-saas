namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(GridColumnWidthConverter))]
    public struct GridColumnWidth : IEquatable<GridColumnWidth>
    {
        private double _value;
        private GridColumnUnitType _unitType;
        public GridColumnWidth(double value) : this(value, GridColumnUnitType.Pixel)
        {
        }

        public GridColumnWidth(double value, GridColumnUnitType unitType)
        {
            this._value = value;
            this._unitType = unitType;
        }

        public double Value =>
            this._value;
        public GridColumnUnitType UnitType =>
            this._unitType;
        public bool IsAbsolute =>
            this.UnitType == GridColumnUnitType.Pixel;
        public bool IsStar =>
            this.UnitType == GridColumnUnitType.Star;
        public static implicit operator GridColumnWidth(double value) => 
            new GridColumnWidth(value);

        public static bool operator ==(GridColumnWidth value1, GridColumnWidth value2) => 
            (value1.Value == value2.Value) && (value1.UnitType == value2.UnitType);

        public static bool operator !=(GridColumnWidth value1, GridColumnWidth value2) => 
            !(value1 == value2);

        public override bool Equals(object obj) => 
            (obj is GridColumnWidth) && (this == ((GridColumnWidth) obj));

        public override int GetHashCode() => 
            ((int) this.Value) + this.UnitType;

        bool IEquatable<GridColumnWidth>.Equals(GridColumnWidth other) => 
            this == other;

        public override string ToString() => 
            GridColumnWidthConverter.ToString(this, CultureInfo.InvariantCulture);
    }
}

