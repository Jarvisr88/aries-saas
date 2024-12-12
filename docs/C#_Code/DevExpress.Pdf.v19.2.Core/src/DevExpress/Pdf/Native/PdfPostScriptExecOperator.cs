namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptExecOperator : PdfPostScriptOperator
    {
        public const string Token = "exec";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            object[] objArray1 = new object[] { interpreter.Stack.Pop(true) };
            interpreter.Execute((IEnumerable<object>) objArray1);
        }
    }
}

