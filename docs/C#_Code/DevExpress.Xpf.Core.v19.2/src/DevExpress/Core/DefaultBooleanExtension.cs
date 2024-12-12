namespace DevExpress.Core
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public static class DefaultBooleanExtension
    {
        public static bool GetValue(this DefaultBoolean db, bool defaultValue) => 
            db.HasValue() ? db.Value() : defaultValue;

        public static bool HasValue(this DefaultBoolean db) => 
            db != DefaultBoolean.Default;

        public static bool? ToBool(this DefaultBoolean db)
        {
            if (db != DefaultBoolean.Default)
            {
                return new bool?(db == DefaultBoolean.True);
            }
            return null;
        }

        public static DefaultBoolean ToDefaultBoolean(this bool? b) => 
            (b == null) ? DefaultBoolean.Default : (b.Value ? DefaultBoolean.True : DefaultBoolean.False);

        public static bool Value(this DefaultBoolean db) => 
            db == DefaultBoolean.True;
    }
}

