namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptType1FontOperator : PdfPostScriptOperator
    {
        public const string RDToken = "RD";
        public const string RDAlternativeToken = "-|";
        public const string NDToken = "ND";
        public const string NDAlternativeToken = "|-";
        public const string NPToken = "NP";
        public const string NPAlternativeToken = "|";
        private static readonly string[] readTokens = new string[] { "RD", "-|" };
        private static readonly string[] defTokens = new string[] { "ND", "|-" };
        private static readonly string[] pufTokens = new string[] { "NP", "|" };
        private readonly string[] tokens;
        private readonly PdfPostScriptType1FontOperatorKind kind;

        public PdfPostScriptType1FontOperator(PdfPostScriptType1FontOperatorKind kind)
        {
            this.kind = kind;
            switch (kind)
            {
                case PdfPostScriptType1FontOperatorKind.Read:
                    this.tokens = readTokens;
                    return;

                case PdfPostScriptType1FontOperatorKind.Def:
                    this.tokens = defTokens;
                    return;

                case PdfPostScriptType1FontOperatorKind.Put:
                    this.tokens = pufTokens;
                    return;
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            foreach (string str in this.tokens)
            {
                if (Execute(interpreter, str))
                {
                    return;
                }
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        private static bool Execute(PdfPostScriptInterpreter interpreter, string token)
        {
            bool flag;
            using (Stack<PdfPostScriptDictionary>.Enumerator enumerator = interpreter.DictionaryStack.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPostScriptDictionary current = enumerator.Current;
                        if (!Execute(interpreter, current, token))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return Execute(interpreter, interpreter.UserDictionary, token);
                    }
                    break;
                }
            }
            return flag;
        }

        private static bool Execute(PdfPostScriptInterpreter interpreter, PdfPostScriptDictionary dictionary, string token)
        {
            if (!dictionary.ContainsKey(token))
            {
                return false;
            }
            IList<object> list = dictionary[token] as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            interpreter.Execute((IEnumerable<object>) list);
            return true;
        }

        public PdfPostScriptType1FontOperatorKind Kind =>
            this.kind;
    }
}

