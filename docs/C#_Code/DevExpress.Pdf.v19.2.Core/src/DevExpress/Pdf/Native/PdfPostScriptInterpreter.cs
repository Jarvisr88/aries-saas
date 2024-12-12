namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptInterpreter
    {
        private readonly PdfStack stack;
        private readonly Stack<PdfPostScriptDictionary> dictionaryStack;
        private readonly PdfPostScriptDictionary fontDirectory;
        private readonly PdfPostScriptDictionary systemDictionary;
        private readonly PdfPostScriptDictionary userDictionary;
        private readonly bool forceCharstringTermination;
        private PdfPostScriptFileParser parser;
        private bool fileIsClosed;

        public PdfPostScriptInterpreter() : this(false)
        {
        }

        public PdfPostScriptInterpreter(bool forceCharstringTermination)
        {
            this.stack = new PdfStack();
            this.dictionaryStack = new Stack<PdfPostScriptDictionary>();
            this.fontDirectory = new PdfPostScriptDictionary(0);
            this.systemDictionary = new PdfPostScriptDictionary(0);
            this.userDictionary = new PdfPostScriptDictionary(0);
            this.forceCharstringTermination = forceCharstringTermination;
        }

        public void CloseFile()
        {
            this.fileIsClosed = true;
        }

        public void Execute(IEnumerable<object> operators)
        {
            foreach (object obj2 in operators)
            {
                this.Execute(obj2);
            }
        }

        public void Execute(byte[] data)
        {
            this.parser = new PdfPostScriptFileParser(data);
            bool flag = false;
            while (true)
            {
                if (!this.fileIsClosed)
                {
                    object obj2 = this.parser.ReadNextObject();
                    if (obj2 != null)
                    {
                        PdfPostScriptGetDictionaryElementOperator @operator = obj2 as PdfPostScriptGetDictionaryElementOperator;
                        if (flag && (@operator == null))
                        {
                            PdfPostScriptType1FontOperator operator2 = obj2 as PdfPostScriptType1FontOperator;
                            if ((operator2 == null) || (operator2.Kind != PdfPostScriptType1FontOperatorKind.Def))
                            {
                                this.Execute(new PdfPostScriptType1FontOperator(PdfPostScriptType1FontOperatorKind.Def));
                            }
                            flag = false;
                        }
                        if ((@operator == null) || !string.IsNullOrEmpty(@operator.Key))
                        {
                            if (this.forceCharstringTermination)
                            {
                                PdfPostScriptType1FontOperator operator3 = obj2 as PdfPostScriptType1FontOperator;
                                flag = (operator3 != null) && (operator3.Kind == PdfPostScriptType1FontOperatorKind.Read);
                            }
                            this.Execute(obj2);
                            continue;
                        }
                    }
                }
                return;
            }
        }

        private void Execute(object obj)
        {
            PdfPostScriptOperator @operator = obj as PdfPostScriptOperator;
            if (@operator == null)
            {
                this.stack.Push(obj);
            }
            else
            {
                @operator.Execute(this);
            }
        }

        public PdfStack Stack =>
            this.stack;

        public Stack<PdfPostScriptDictionary> DictionaryStack =>
            this.dictionaryStack;

        public PdfPostScriptDictionary FontDirectory =>
            this.fontDirectory;

        public PdfPostScriptDictionary SystemDictionary =>
            this.systemDictionary;

        public PdfPostScriptDictionary UserDictionary =>
            this.userDictionary;

        public PdfPostScriptFileParser Parser =>
            this.parser;
    }
}

