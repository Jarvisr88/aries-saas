namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    public class CriteriaToStringVisitResult
    {
        private const string NullCriteriaResult = "()";
        public readonly string Result;
        public readonly CriteriaPriorityClass Priority;
        public static readonly CriteriaToStringVisitResult Null;

        static CriteriaToStringVisitResult();
        public CriteriaToStringVisitResult(string result);
        public CriteriaToStringVisitResult(string result, CriteriaPriorityClass priorityClass);
        public string GetEnclosedResult();
        public string GetEnclosedResultOnGreater(CriteriaPriorityClass basePriority);
        public string GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass basePriority);

        public bool IsNull { get; }
    }
}

