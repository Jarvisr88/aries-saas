namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    public class CriteriaLexerToken
    {
        private DevExpress.Data.Filtering.CriteriaOperator criteriaOperator;
        private int token;
        private int position;
        private int length;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ListName;

        public CriteriaLexerToken(DevExpress.Data.Filtering.CriteriaOperator criteriaOperator, int token, int pos, int len);
        public static DevExpress.Data.Filtering.Helpers.TokenType GetNextTokenType(DevExpress.Data.Filtering.Helpers.TokenType previousToken);
        protected virtual DevExpress.Data.Filtering.Helpers.TokenType ToTokenType();

        public DevExpress.Data.Filtering.CriteriaOperator CriteriaOperator { get; }

        public int Token { get; }

        public int Position { get; }

        public int Length { get; }

        public int PositionEnd { get; }

        public DevExpress.Data.Filtering.Helpers.TokenType TokenType { get; }
    }
}

