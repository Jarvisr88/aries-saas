namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ActionExecutor
    {
        private readonly Queue<Action<IEnumerator>> actions;

        public ActionExecutor();
        public void AddAction(Action action);
        public void AddAction(Action<IEnumerator> action);
        private bool RunAction();
        private bool RunNextAction();

        private class ActionEnumerator : IEnumerator
        {
            private readonly ActionExecutor executor;

            public ActionEnumerator(ActionExecutor executor);
            public bool MoveNext();
            public void Reset();

            public object Current { get; }
        }
    }
}

