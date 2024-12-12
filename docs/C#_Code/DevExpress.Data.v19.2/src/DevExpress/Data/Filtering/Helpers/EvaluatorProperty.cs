namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public class EvaluatorProperty
    {
        private EvaluatorProperty subProperty;
        public readonly int UpDepth;
        public readonly string PropertyPath;
        private string[] tokenized;

        protected EvaluatorProperty(string sourcePath);
        public static int CalcCollectionPropertyDepth(string prop);
        public static EvaluatorProperty Create(OperandProperty property);
        public static bool GetIsThisProperty(string propertyName);
        public static int GetPropertySeparatorDotPos(string property);
        public static int GetPropertySeparatorDotPos(string property, int startPos);
        public static string[] Split(string prop);

        public EvaluatorProperty SubProperty { get; }

        public string[] PropertyPathTokenized { get; }
    }
}

