namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using System;
    using System.Collections;

    public class AsyncOperationCompletedHelper
    {
        private OperationCompleted completed;
        private static readonly object Token;

        static AsyncOperationCompletedHelper();
        private AsyncOperationCompletedHelper(OperationCompleted completed);
        public static void AppendCompletedDelegate(Command command, OperationCompleted next);
        private static OperationCompleted Combine(params OperationCompleted[] delegates);
        public static DictionaryEntry GetCommandParameter(OperationCompleted completed);
        public static DictionaryEntry GetCommandParameter(OperationCompleted[] completed);
        public static OperationCompleted GetCompletedDelegate(Command command);
    }
}

