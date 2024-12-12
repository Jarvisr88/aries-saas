namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DfaWave : IEnumerable
    {
        internal readonly Dictionary<State, bool> states;
        internal readonly State finalState;
        internal object[] placeHoldersInfo;
        private string sample;
        internal bool hashCodeCalculated;
        private int hashCode;
        internal AutoCompleteInfo autoCompleteInfo;

        public DfaWave(State finalState);
        internal void Add(State state);
        public void AddStateWithEmptyTransitionsTargets(State state);
        private void CalculatePlaceHoldersInfo();
        public bool Contains(State state);
        public override bool Equals(object obj);
        public AutoCompleteInfo GetAutoCompleteInfo();
        private DfaWave.GetAutoCompleteInfoTransitionsProcessingResult GetAutoCompleteInfoTransitionsProcessing();
        public IEnumerator GetEnumerator();
        public override int GetHashCode();
        public DfaWave GetNextWave(char input);
        public string GetOptimisticHint();
        public object[] GetPlaceHoldersInfo();
        private static object[] MergeMasks(object[] firstMask, object[] secondMask);
        private ICollection<DfaWave.PlaceHoldersPredictAssociation> PlaceHoldersPredictGetNextHolders(ICollection<DfaWave.PlaceHoldersPredictAssociation> completeHolders, ICollection<DfaWave.PlaceHoldersPredictAssociation> currentHolders);

        public int Count { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DfaWave.<>c <>9;
            public static Comparison<DfaWave.PlaceHoldersPredictAssociation> <>9__18_0;

            static <>c();
            internal int <CalculatePlaceHoldersInfo>b__18_0(DfaWave.PlaceHoldersPredictAssociation a, DfaWave.PlaceHoldersPredictAssociation b);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct GetAutoCompleteInfoTransitionsProcessingResult
        {
            public bool NonExactsFound;
            public bool ExactCharFound;
            public char ExactChar;
        }

        private class PlaceHoldersPredictAssociation
        {
            public readonly object[] PlaceHolders;
            public readonly DevExpress.Data.Mask.State State;
            public readonly DfaWave.PlaceHoldersPredictAssociation PassedStatesSource;
            public readonly string OptimisticHint;

            public PlaceHoldersPredictAssociation(DevExpress.Data.Mask.State state);
            public PlaceHoldersPredictAssociation(DfaWave.PlaceHoldersPredictAssociation prevHolder, Transition transition);
            private static bool CanReachPast(DevExpress.Data.Mask.State nextState, DevExpress.Data.Mask.State targetState, DevExpress.Data.Mask.State pastState, IDictionary<DevExpress.Data.Mask.State, bool> states);
            public bool CanSkip(DevExpress.Data.Mask.State suspectState, DevExpress.Data.Mask.State finalState);
            private bool IsPassed(DevExpress.Data.Mask.State state);
        }
    }
}

