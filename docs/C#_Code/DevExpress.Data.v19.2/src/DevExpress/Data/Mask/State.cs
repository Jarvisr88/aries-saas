namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class State
    {
        private readonly List<Transition> transitions;
        private IDictionary<State, bool> reachableStatesDictionary;

        public State();
        public void AddTransition(Transition transition);
        public bool CanReach(State state);
        private static void CollectReachableStates(State nextState, IDictionary<State, bool> states);
        public ICollection<State> GetReachableStates();

        public ICollection Transitions { get; }

        private IDictionary<State, bool> ReachableStatesDictionary { get; }
    }
}

