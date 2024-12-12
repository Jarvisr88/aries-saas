namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class VisitorOperand : VisitorBase<IEnumerable<Operand>>
    {
        private readonly bool fullPack;
        private readonly IErrorHandler errorHandler;
        private readonly Func<NType, Type> typeResolver;
        private List<Operand> operands;

        protected VisitorOperand(Func<NType, Type> typeResolver, IErrorHandler errorHandler, bool fullPack)
        {
            this.fullPack = fullPack;
            this.typeResolver = typeResolver;
            this.errorHandler = errorHandler;
            this.operands = new List<Operand>();
        }

        protected override IEnumerable<Operand> Assign(NAssign n, IEnumerable<Operand> value) => 
            this.Visit(n.Left).Union<Operand>(value);

        private void BackAssigns(IEnumerable<NAssign> assigns)
        {
            IEnumerable<Operand> backOperands = new Operand[0];
            Func<NAssign, NBase> selector = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<NAssign, NBase> local1 = <>c.<>9__10_0;
                selector = <>c.<>9__10_0 = x => x.Left;
            }
            assigns.Select<NAssign, NBase>(selector).Select<NBase, IEnumerable<Operand>>(new Func<NBase, IEnumerable<Operand>>(this.Visit)).ToList<IEnumerable<Operand>>().ForEach(x => backOperands = backOperands.Union<Operand>(x));
            Action<Operand> action = <>c.<>9__10_2;
            if (<>c.<>9__10_2 == null)
            {
                Action<Operand> local2 = <>c.<>9__10_2;
                action = <>c.<>9__10_2 = x => x.SetMode(true);
            }
            backOperands.ToList<Operand>().ForEach(action);
            foreach (Operand operand in backOperands)
            {
                if (this.operands.Contains(operand))
                {
                    this.operands[this.operands.IndexOf(operand)].SetMode(true);
                    continue;
                }
                this.operands.Add(operand);
            }
        }

        private void BackExpr(NBase n)
        {
            if (this.operands.Count<Operand>() == 0)
            {
                this.errorHandler.Throw(ErrorHelper.Err102(), null);
            }
            else if (this.operands.Count<Operand>() > 1)
            {
                this.errorHandler.Throw(ErrorHelper.Err103(), null);
            }
            else
            {
                this.operands[0].SetMode(true);
            }
        }

        protected override IEnumerable<Operand> Binary(NBinary n, IEnumerable<Operand> left, IEnumerable<Operand> right) => 
            left.Union<Operand>(right);

        protected override bool CanContinue(NBase n) => 
            !this.errorHandler.HasError;

        protected override IEnumerable<Operand> Cast(NCast n, IEnumerable<Operand> value) => 
            value;

        protected override IEnumerable<Operand> Constant(NConstant n) => 
            new Operand[0];

        private static Operand FullPack_ReduceIdent(NIdentBase n, Func<NType, Type> typeResolver, out NIdentBase rest)
        {
            if ((n is NNew) || ((n is NExprIdent) || ((n is NRelative) && ((((NRelative) n).Kind == NRelative.NKind.Value) || ((((NRelative) n).Kind == NRelative.NKind.Parameter) || ((((NRelative) n).Kind == NRelative.NKind.Sender) || (((NRelative) n).Kind == NRelative.NKind.Args)))))))
            {
                rest = n;
                return null;
            }
            rest = (n is NRelative) ? n.Next : n;
            List<string> idents = new List<string>();
            while ((rest != null) && ((rest is NIdent) || ((rest is NType) && (((NType) rest).Kind == NType.NKind.Attached))))
            {
                idents.Add(VisitorString.ResolveIdent(rest, false));
                rest = rest.Next;
            }
            string path = VisitorString.CombineIdents(idents);
            if ((path == null) && (n is NMethod))
            {
                path = string.Empty;
            }
            return Operand.CreateOperand(path, n as NRelative, typeResolver);
        }

        private IEnumerable<Operand> FullPack_RootIdent(NIdentBase n)
        {
            NIdentBase base2;
            IEnumerable<Operand> res = new Operand[0];
            Operand operand = ReduceIdent(n, this.typeResolver, out base2, true);
            if (operand != null)
            {
                Operand[] operandArray1 = new Operand[] { operand };
                res = operandArray1;
            }
            if (base2 == null)
            {
                return res;
            }
            Func<NIdentBase, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<NIdentBase, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = x => (x is NExprIdent) || ((x is NMethod) || ((x is NIndex) || ((x is NType) || (x is NNew))));
            }
            Func<NIdentBase, IEnumerable<NBase>> selector = <>c.<>9__7_1;
            switch (<>c.<>9__7_1)
            {
                case (null):
                {
                    Func<NIdentBase, IEnumerable<NBase>> local2 = <>c.<>9__7_1;
                    selector = <>c.<>9__7_1 = delegate (NIdentBase x) {
                        switch (x)
                        {
                            case NExprIdent _:
                                return new NBase[] { ((NExprIdent) x).Expr };
                        }
                        if (x is NMethod)
                        {
                            return ((NMethod) x).Args;
                        }
                        if (x is NIndex)
                        {
                            return ((NIndex) x).Args;
                        }
                        if (x is NType)
                        {
                            return !(((NType) x).Ident is NMethod) ? ((IEnumerable<NBase>) new NBase[0]) : ((IEnumerable<NBase>) ((NMethod) ((NType) x).Ident).Args);
                        }
                        if (!(x is NNew))
                        {
                            throw new NotImplementedException();
                        }
                        return ((NNew) x).Args;
                    };
                    break;
                }
            }
            Func<IEnumerable<NBase>, IEnumerable<NBase>> func3 = <>c.<>9__7_2;
            if (<>c.<>9__7_2 == null)
            {
                Func<IEnumerable<NBase>, IEnumerable<NBase>> local3 = <>c.<>9__7_2;
                func3 = <>c.<>9__7_2 = x => x;
            }
            base2.Unfold().OfType<NIdentBase>().Where<NIdentBase>(predicate).Select<NIdentBase, IEnumerable<NBase>>(selector).SelectMany<IEnumerable<NBase>, NBase>(func3).ToList<NBase>().ForEach(delegate (NBase x) {
                res = res.Union<Operand>(this.Visit(x));
            });
            return res.ToList<Operand>();
        }

        private static Operand NotFullPack_ReduceIdent(NIdentBase n, Func<NType, Type> typeResolver, out NIdentBase rest)
        {
            if (!(n is NNew) && (!(n is NExprIdent) && (!(n is NRelative) || ((((NRelative) n).Kind != NRelative.NKind.Value) && ((((NRelative) n).Kind != NRelative.NKind.Parameter) && ((((NRelative) n).Kind != NRelative.NKind.Sender) && (((NRelative) n).Kind != NRelative.NKind.Args)))))))
            {
                rest = (n is NRelative) ? n.Next : n;
                return (((n is NType) || ((n is NNew) || (n is NExprIdent))) ? Operand.CreateOperand(null, null, typeResolver) : Operand.CreateOperand(string.Empty, n as NRelative, typeResolver));
            }
            rest = n;
            return null;
        }

        private IEnumerable<Operand> NotFullPack_RootIdent(NIdentBase n)
        {
            IEnumerable<Operand> res = new Operand[0];
            NRelative relative = n as NRelative;
            if (relative != null)
            {
                if ((relative.Kind != NRelative.NKind.Value) && ((relative.Kind != NRelative.NKind.Parameter) && ((relative.Kind != NRelative.NKind.Sender) && (relative.Kind != NRelative.NKind.Args))))
                {
                    Operand[] operandArray1 = new Operand[] { Operand.CreateOperand(string.Empty, relative, this.typeResolver) };
                    res = operandArray1;
                }
            }
            else if ((n is NIdent) || ((n is NMethod) || (n is NIndex)))
            {
                Operand[] operandArray2 = new Operand[] { Operand.CreateOperand(string.Empty, null, this.typeResolver) };
                res = operandArray2;
            }
            Func<NIdentBase, bool> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<NIdentBase, bool> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = x => (x is NExprIdent) || ((x is NMethod) || ((x is NIndex) || ((x is NType) || (x is NNew))));
            }
            Func<NIdentBase, IEnumerable<NBase>> selector = <>c.<>9__8_1;
            switch (<>c.<>9__8_1)
            {
                case (null):
                {
                    Func<NIdentBase, IEnumerable<NBase>> local2 = <>c.<>9__8_1;
                    selector = <>c.<>9__8_1 = delegate (NIdentBase x) {
                        switch (x)
                        {
                            case NExprIdent _:
                                return new NBase[] { ((NExprIdent) x).Expr };
                        }
                        if (x is NMethod)
                        {
                            return ((NMethod) x).Args;
                        }
                        if (x is NIndex)
                        {
                            return ((NIndex) x).Args;
                        }
                        if (x is NType)
                        {
                            return !(((NType) x).Ident is NMethod) ? ((IEnumerable<NBase>) new NBase[0]) : ((IEnumerable<NBase>) ((NMethod) ((NType) x).Ident).Args);
                        }
                        if (!(x is NNew))
                        {
                            throw new NotImplementedException();
                        }
                        return ((NNew) x).Args;
                    };
                    break;
                }
            }
            Func<IEnumerable<NBase>, IEnumerable<NBase>> func3 = <>c.<>9__8_2;
            if (<>c.<>9__8_2 == null)
            {
                Func<IEnumerable<NBase>, IEnumerable<NBase>> local3 = <>c.<>9__8_2;
                func3 = <>c.<>9__8_2 = x => x;
            }
            n.Unfold().OfType<NIdentBase>().Where<NIdentBase>(predicate).Select<NIdentBase, IEnumerable<NBase>>(selector).SelectMany<IEnumerable<NBase>, NBase>(func3).ToList<NBase>().ForEach(delegate (NBase x) {
                res = res.Union<Operand>(this.Visit(x));
            });
            return res.ToList<Operand>();
        }

        public static Operand ReduceIdent(NIdentBase n, Func<NType, Type> typeResolver, out NIdentBase rest, bool fullPack = true) => 
            !fullPack ? NotFullPack_ReduceIdent(n, typeResolver, out rest) : FullPack_ReduceIdent(n, typeResolver, out rest);

        public static IEnumerable<Operand> Resolve(IEnumerable<NRoot> exprs, NRoot backExpr, Func<NType, Type> typeResolver, IErrorHandler errorHandler, bool fullPack = true)
        {
            VisitorOperand me = new VisitorOperand(typeResolver, errorHandler, fullPack);
            IEnumerable<Operand> collection = MakePlain<Operand>(from x in exprs select MakePlain<Operand>(me.RootVisit(x)));
            me.operands = new List<Operand>(collection);
            if (backExpr != null)
            {
                me.RootVisitBack(backExpr);
            }
            return me.operands;
        }

        protected override IEnumerable<Operand> RootIdent(NIdentBase n) => 
            !this.fullPack ? this.NotFullPack_RootIdent(n) : this.FullPack_RootIdent(n);

        private void RootVisitBack(NRoot backExpr)
        {
            if (!this.errorHandler.HasError)
            {
                if ((backExpr.Exprs.Count<NBase>() == 1) && !(backExpr.Expr is NAssign))
                {
                    this.BackExpr(backExpr.Expr);
                }
                else
                {
                    this.BackAssigns(backExpr.Exprs.Cast<NAssign>());
                }
            }
        }

        protected override IEnumerable<Operand> Ternary(NTernary n, IEnumerable<Operand> first, IEnumerable<Operand> second, IEnumerable<Operand> third) => 
            first.Union<Operand>(second).Union<Operand>(third);

        protected override IEnumerable<Operand> Unary(NUnary n, IEnumerable<Operand> value) => 
            value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisitorOperand.<>c <>9 = new VisitorOperand.<>c();
            public static Func<NIdentBase, bool> <>9__7_0;
            public static Func<NIdentBase, IEnumerable<NBase>> <>9__7_1;
            public static Func<IEnumerable<NBase>, IEnumerable<NBase>> <>9__7_2;
            public static Func<NIdentBase, bool> <>9__8_0;
            public static Func<NIdentBase, IEnumerable<NBase>> <>9__8_1;
            public static Func<IEnumerable<NBase>, IEnumerable<NBase>> <>9__8_2;
            public static Func<NAssign, NBase> <>9__10_0;
            public static Action<Operand> <>9__10_2;

            internal NBase <BackAssigns>b__10_0(NAssign x) => 
                x.Left;

            internal void <BackAssigns>b__10_2(Operand x)
            {
                x.SetMode(true);
            }

            internal bool <FullPack_RootIdent>b__7_0(NIdentBase x) => 
                (x is NExprIdent) || ((x is NMethod) || ((x is NIndex) || ((x is NType) || (x is NNew))));

            internal IEnumerable<NBase> <FullPack_RootIdent>b__7_1(NIdentBase x)
            {
                switch (x)
                {
                    case (NExprIdent _):
                        return new NBase[] { ((NExprIdent) x).Expr };
                        break;
                }
                if (x is NMethod)
                {
                    return ((NMethod) x).Args;
                }
                if (x is NIndex)
                {
                    return ((NIndex) x).Args;
                }
                if (x is NType)
                {
                    return (!(((NType) x).Ident is NMethod) ? ((IEnumerable<NBase>) new NBase[0]) : ((IEnumerable<NBase>) ((NMethod) ((NType) x).Ident).Args));
                }
                if (!(x is NNew))
                {
                    throw new NotImplementedException();
                }
                return ((NNew) x).Args;
            }

            internal IEnumerable<NBase> <FullPack_RootIdent>b__7_2(IEnumerable<NBase> x) => 
                x;

            internal bool <NotFullPack_RootIdent>b__8_0(NIdentBase x) => 
                (x is NExprIdent) || ((x is NMethod) || ((x is NIndex) || ((x is NType) || (x is NNew))));

            internal IEnumerable<NBase> <NotFullPack_RootIdent>b__8_1(NIdentBase x)
            {
                switch (x)
                {
                    case (NExprIdent _):
                        return new NBase[] { ((NExprIdent) x).Expr };
                        break;
                }
                if (x is NMethod)
                {
                    return ((NMethod) x).Args;
                }
                if (x is NIndex)
                {
                    return ((NIndex) x).Args;
                }
                if (x is NType)
                {
                    return (!(((NType) x).Ident is NMethod) ? ((IEnumerable<NBase>) new NBase[0]) : ((IEnumerable<NBase>) ((NMethod) ((NType) x).Ident).Args));
                }
                if (!(x is NNew))
                {
                    throw new NotImplementedException();
                }
                return ((NNew) x).Args;
            }

            internal IEnumerable<NBase> <NotFullPack_RootIdent>b__8_2(IEnumerable<NBase> x) => 
                x;
        }
    }
}

