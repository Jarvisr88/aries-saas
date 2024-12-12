namespace DevExpress.Data.Filtering
{
    using System;

    public class CriteriaCompilerRefResult
    {
        public readonly CriteriaCompilerLocalContext LocalContext;
        public readonly string SubProperty;

        public CriteriaCompilerRefResult(CriteriaCompilerLocalContext _LocalContext, string _SubProperty);

        public bool IsCollection { get; }
    }
}

