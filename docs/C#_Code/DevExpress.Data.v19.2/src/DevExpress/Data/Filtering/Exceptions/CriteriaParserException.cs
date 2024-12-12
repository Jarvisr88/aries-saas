namespace DevExpress.Data.Filtering.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class CriteriaParserException : Exception
    {
        private int line;
        private int column;

        public CriteriaParserException(string explanation);
        protected CriteriaParserException(SerializationInfo info, StreamingContext context);
        public CriteriaParserException(string explanation, int line, int column);

        public int Line { get; set; }

        public int Column { get; set; }
    }
}

