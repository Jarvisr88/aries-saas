namespace DevExpress.Data
{
    using System;

    public static class StackTraceHelper
    {
        public static bool CheckStackFrame(string methodName, Type type);
        public static bool CheckStackFrame(string methodName, Type type, int start, int maxCount);
        public static bool CheckStackFrameByName(string methodName, string typeName);
        public static bool CheckStackFrameByName(string methodName, string typeName, int start, int maxCount);
    }
}

