namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CriteriaLexerTokenHelper
    {
        private readonly string data;
        private readonly List<CriteriaLexerToken> tokenList;

        public CriteriaLexerTokenHelper(string data);
        private List<CriteriaLexerToken> Analyze();
        public string ConvertConstants(ConvertConstantDelegate convertMethod);
        private string ConvertConstantToken(ConvertConstantDelegate convertMethod, CriteriaLexerToken propertyToken, CriteriaLexerToken token, string listName);
        public string ConvertParameters(bool propertyToParameter, Dictionary<string, string> renamingMap);
        private string ConvertParameterToken(bool propertyToParameter, Dictionary<string, string> renamingMap, CriteriaLexerToken token, string listName);
        public string ConvertProperties(ConvertPropertyDelegate convertMethod);
        public string ConvertProperties(bool forceBrackets, ConvertPropertyDelegate convertMethod);
        private string ConvertPropertyToken(bool forceBrackets, ConvertPropertyDelegate convertMethod, CriteriaLexerToken token, string listName);
        private string ConvertTokens(bool forceBrackets, Func<CriteriaLexerToken, string, string> convertTokenFunc);
        public CriteriaLexerToken FindToken(int position);
        protected int FindTokenIndex(int position);
        public CriteriaLexerToken GetNeighborToken(int position, bool leftNeighbor);
        public void GetNeighborTokenRange(int position, bool leftNeighbor, out int startPosition, out int length);
        public TokenType GetNeighborTokenType(int position, bool leftNeighbor);
        public TokenType GetNextTokenType(int position);
        internal string GetPropertyName(CriteriaLexerToken token);
        public void GetTokenPositionLength(int position, out int startPosition, out int length);
        protected string GetTokenText(CriteriaLexerToken token);
        public string GetTokenText(int position);
        public TokenType GetTokenType(int position);
        public object GetTokenValue(int position);
        public bool IsAggregate(int position);
        public void UpdateListProperty(bool forceBrackets);

        public List<CriteriaLexerToken> TokenList { get; }

        protected string Data { get; }
    }
}

