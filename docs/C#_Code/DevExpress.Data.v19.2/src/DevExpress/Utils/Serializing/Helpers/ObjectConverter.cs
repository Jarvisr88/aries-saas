namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public class ObjectConverter
    {
        private static ObjectConverterImplementation instance;

        internal static int GetIndexOfDelimiter(string str, int index) => 
            Instance.GetIndexOfDelimiter(str, index);

        internal static string GetNextPart(string str, ref int index) => 
            Instance.GetNextPart(str, ref index);

        public static object IntStringToStructure(string str, Type type) => 
            Instance.IntStringToStructure(str, type);

        public static string IntStructureToString(object obj) => 
            Instance.IntStructureToString(obj);

        public static string ObjectToString(object obj) => 
            Instance.ObjectToString(obj);

        public static object StringToObject(string str, Type type) => 
            Instance.StringToObject(str, type);

        public static ObjectConverterImplementation Instance
        {
            get
            {
                instance ??= new ObjectConverterImplementation();
                return instance;
            }
        }
    }
}

