namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfType1CharstringInterpreter : PdfPostScriptInterpreter
    {
        private readonly IList<PdfType1CharstringSubroutine> fontSubroutines;
        private readonly PdfStack charstringStack = new PdfStack();
        private bool characterEnded;
        private bool returnFromSubroutine;

        protected PdfType1CharstringInterpreter(IList<PdfType1CharstringSubroutine> fontSubroutines)
        {
            this.fontSubroutines = fontSubroutines;
        }

        protected abstract void CallOtherSubr(int index, IList<object> parameters);
        public void CallOtherSubr(int index, int n)
        {
            IList<object> parameters = new List<object>(n);
            for (int i = 0; (i < n) && (this.CharstringStack.Count != 0); i++)
            {
                object obj2 = this.CharstringStack.Pop(true);
                base.Stack.Push(obj2);
                parameters.Insert(0, obj2);
            }
            this.CallOtherSubr(index, parameters);
        }

        public void CallSubr(IEnumerable<IPdfType1CharstringToken> tokens)
        {
            if (tokens != null)
            {
                foreach (IPdfType1CharstringToken token in tokens)
                {
                    token.Execute(this);
                    if (this.returnFromSubroutine)
                    {
                        this.returnFromSubroutine = false;
                        break;
                    }
                }
            }
        }

        public abstract void ClosePath();
        public virtual void EndCharacter()
        {
            this.characterEnded = true;
        }

        public void Execute(IEnumerable<IPdfType1CharstringToken> tokens)
        {
            if (tokens != null)
            {
                foreach (IPdfType1CharstringToken token in tokens)
                {
                    token.Execute(this);
                    if (this.characterEnded)
                    {
                        break;
                    }
                }
            }
        }

        public abstract void RelativeCurveTo(double dx1, double dy1, double dx2, double dy2, double dx3, double dy3);
        public abstract void RelativeLineTo(double dx, double dy);
        public abstract void RelativeMoveTo(double dx, double dy);
        public void Return()
        {
            this.returnFromSubroutine = true;
        }

        public abstract void Seac(double asb, double adx, double ady, int bchar, int achar);
        public void SetSidebearing(double sbx, double wx)
        {
            this.SetSidebearing(sbx, 0.0, wx, 0.0);
        }

        public abstract void SetSidebearing(double sbx, double sby, double wx, double wy);

        public IList<PdfType1CharstringSubroutine> FontSubroutines =>
            this.fontSubroutines;

        public PdfStack CharstringStack =>
            this.charstringStack;
    }
}

