namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Async;
    using System;
    using System.Collections;

    public class AsyncOperationCompletedHelper2
    {
        private OperationCompleted completed;
        private static readonly object Token = new object();

        private AsyncOperationCompletedHelper2(OperationCompleted completed)
        {
            this.completed = completed;
        }

        public static void AppendCompletedDelegate(Command command, OperationCompleted next)
        {
            if (next != null)
            {
                AsyncOperationCompletedHelper2 helper;
                if (!command.TryGetTag<AsyncOperationCompletedHelper2>(Token, out helper))
                {
                    throw new InvalidOperationException($"'{command}' command did not have AsyncCompletedHelper tag to append next continuation");
                }
                helper.completed += next;
            }
        }

        private static OperationCompleted Combine(params OperationCompleted[] delegates)
        {
            if ((delegates == null) || (delegates.Length == 0))
            {
                return null;
            }
            Delegate delegate2 = delegates[0];
            for (int i = 1; i < delegates.Length; i++)
            {
                delegate2 += delegates[i];
            }
            return (OperationCompleted) delegate2;
        }

        public static DictionaryEntry GetCommandParameter(OperationCompleted completed) => 
            new DictionaryEntry(Token, new AsyncOperationCompletedHelper2(completed));

        public static DictionaryEntry GetCommandParameter(OperationCompleted[] completed) => 
            GetCommandParameter(Combine(completed));

        public static OperationCompleted GetCompletedDelegate(Command command)
        {
            AsyncOperationCompletedHelper2 helper;
            return (!command.TryGetTag<AsyncOperationCompletedHelper2>(Token, out helper) ? null : helper.completed);
        }
    }
}

