namespace DevExpress.Utils.Serializing
{
    using System;

    public abstract class StructStringConverter : StructConverter<string>
    {
        protected StructStringConverter()
        {
        }

        protected override string ElementToString(string obj) => 
            obj;

        protected override string ToType(string str)
        {
            char[] trimChars = new char[] { ' ' };
            return str.Trim(trimChars);
        }
    }
}

