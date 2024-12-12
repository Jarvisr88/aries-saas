namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Media.Animation;

    public class SelectionIndicatorTransition
    {
        private readonly List<DoubleAnimation> animations;
        private readonly SelectionIndicatorDecorator owner;
        private SelectionIndicator fromValue;
        private SelectionIndicator toValue;

        public SelectionIndicatorTransition(SelectionIndicatorDecorator owner);
        protected virtual void BeginUpdateAnimation();
        protected virtual DoubleAnimation CreateAnimation(double to);
        protected virtual void EnqueueAction(Action action);
        protected virtual void OnLastAnimationOnCompleted(DoubleAnimation sender);
        private bool ShouldSkip();
        protected virtual void Skip();
        public void Translate(SelectionIndicator fromValue, SelectionIndicator toValue);
        protected virtual void UpdateAnimation();

        protected bool AnimationInProgress { get; }

        private class SelectionAction : IAggregateAction, IAction
        {
            private Action action;
            private MethodInfo methodInfo;

            public SelectionAction(Action action);
            void IAction.Execute();
            bool IAggregateAction.CanAggregate(IAction action);
        }
    }
}

