namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;

    internal class b : EnumConverter
    {
        public b() : base(typeof(ConnectionState))
        {
        }

        public override bool a(ITypeDescriptorContext A_0) => 
            true;

        public override TypeConverter.StandardValuesCollection b(ITypeDescriptorContext A_0)
        {
            ConnectionState[] values = new ConnectionState[2];
            values[0] = ConnectionState.Open;
            return new TypeConverter.StandardValuesCollection(values);
        }
    }
}

