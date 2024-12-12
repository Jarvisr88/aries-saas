namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class Parser : ParserBase
    {
        public const int _EOF = 0;
        public const int _Ident = 1;
        public const int _Int = 2;
        public const int _Float = 3;
        public const int _String = 4;
        public const int maxT = 0x48;
        private NRoot root;

        public Parser(Scanner scanner, IParserErrorHandler errorHandler) : base(scanner, errorHandler)
        {
        }

        private void AddExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.MulExpr(out res);
            while ((base.la.kind == 0x23) || (base.la.kind == 0x24))
            {
                if (base.la.kind == 0x23)
                {
                    base.Get();
                    this.MulExpr(out base2);
                    res = new NBinary(NBinary.NKind.Plus, res, base2);
                    continue;
                }
                base.Get();
                this.MulExpr(out base2);
                res = new NBinary(NBinary.NKind.Minus, res, base2);
            }
        }

        private void AndExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.BitOrExpr(out res);
            while ((base.la.kind == 12) || (base.la.kind == 13))
            {
                if (base.la.kind == 12)
                {
                    base.Get();
                }
                else
                {
                    base.Get();
                }
                this.BitOrExpr(out base2);
                res = new NBinary(NBinary.NKind.AndAlso, res, base2);
            }
        }

        private void Assign_Expr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            NBase base3 = null;
            string[] textArray1 = new string[] { "=" };
            if (base.TokenExists(0, textArray1))
            {
                this.Expr(out base2);
                base.Expect(6);
                this.Expr(out base3);
                res = new NAssign(base2, base3);
            }
            else if (base.StartOf(1))
            {
                this.Expr(out res);
            }
            else
            {
                base.SynErr(0x4b);
            }
        }

        private void Assign_ExprRoot()
        {
            NBase base2;
            this.root = new NRoot();
            this.Assign_Expr(out base2);
            while (base.la.kind == 5)
            {
                base.Get();
            }
            this.root.Exprs.Add(base2);
            while (true)
            {
                string[] textArray1 = new string[] { ";" };
                if (!base.TokenEquals(0, textArray1) || base.TokenEquals(1, new int[1]))
                {
                    return;
                }
                this.Assign_Expr(out base2);
                while (true)
                {
                    if (base.la.kind != 5)
                    {
                        this.root.Exprs.Add(base2);
                        break;
                    }
                    base.Get();
                }
            }
        }

        private void AtomExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(5))
            {
                this.ConstExpr(out res);
            }
            else if (base.StartOf(6))
            {
                this.RelativeExpr(out res);
            }
            else if (base.la.kind == 0x3a)
            {
                this.TypeOfExpr(out res);
            }
            else if (base.la.kind == 0x3d)
            {
                this.TypeNewExpr(out res);
            }
            else if (this.NextIs_TypeExpr(1))
            {
                this.TypeIdentExpr(out res, true);
            }
            else if (this.NextIs_MethodExpr(1))
            {
                this.MethodExpr(out res);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out res);
            }
            else if (base.la.kind == 0x3e)
            {
                this.IndexExpr(out res);
            }
            else if (base.la.kind != 0x2a)
            {
                base.SynErr(0x4f);
            }
            else
            {
                base.Get();
                if (this.NextIs_AttachedPropExpr(0))
                {
                    this.TypeIdentExpr(out res, false);
                    ((NType) res).Kind = NType.NKind.Attached;
                }
                else if (base.StartOf(1))
                {
                    this.Expr(out res);
                }
                else
                {
                    base.SynErr(0x4e);
                }
                base.Expect(0x2b);
            }
            this.ReadNextIdents(ref res, true);
        }

        private void AtomRootExpr(out NBase res)
        {
            res = null;
            if (this.Mode == ParserMode.BindingExpr)
            {
                this.AtomExpr(out res);
            }
            else if (this.Mode == ParserMode.BindingBackExpr)
            {
                this.Back_AtomExpr(out res);
            }
            else if (this.Mode == ParserMode.CommandExecute)
            {
                this.Execute_AtomExpr(out res);
            }
            else if (this.Mode == ParserMode.CommandCanExecute)
            {
                this.CanExecute_AtomExpr(out res);
            }
            else if (this.Mode == ParserMode.Event)
            {
                this.Event_AtomExpr(out res);
            }
            else
            {
                base.SynErr(0x4d);
            }
        }

        private void Back_Assign(out NBase res)
        {
            NBase base2;
            NBase base3;
            res = null;
            this.Back_AssignLeft(out base2);
            base.Expect(6);
            this.Expr(out base3);
            res = new NAssign((NIdentBase) base2, base3);
        }

        private void Back_AssignLeft(out NBase res)
        {
            res = null;
            if (base.StartOf(6))
            {
                this.RelativeExpr(out res);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out res);
            }
            else if (base.la.kind == 0x3e)
            {
                this.IndexExpr(out res);
            }
            else if (base.la.kind != 0x2a)
            {
                base.SynErr(0x59);
            }
            else
            {
                base.Get();
                this.TypeIdentExpr(out res, false);
                ((NType) res).Kind = NType.NKind.Attached;
                base.Expect(0x2b);
            }
            this.ReadNextIdents(ref res, false);
        }

        private void Back_AtomExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(5))
            {
                this.ConstExpr(out res);
            }
            else if ((base.la.kind == 0x43) || (base.la.kind == 0x44))
            {
                this.Back_RelativeValueExpr(out res);
            }
            else if (base.la.kind == 0x3a)
            {
                this.TypeOfExpr(out res);
            }
            else if (base.la.kind == 0x3d)
            {
                this.TypeNewExpr(out res);
            }
            else if (this.NextIs_TypeExpr(1))
            {
                this.TypeIdentExpr(out res, true);
            }
            else if (base.la.kind != 0x2a)
            {
                base.SynErr(80);
            }
            else
            {
                base.Get();
                this.Expr(out res);
                base.Expect(0x2b);
            }
            this.ReadNextIdents(ref res, true);
        }

        private void Back_ExprRoot()
        {
            NBase base2;
            this.root = new NRoot();
            string[] textArray1 = new string[] { "=" };
            if (!base.TokenExists(0, textArray1))
            {
                if (base.StartOf(1))
                {
                    this.Expr(out base2);
                    this.root.Exprs.Add(base2);
                }
                else
                {
                    base.SynErr(0x4a);
                }
            }
            else
            {
                this.Back_Assign(out base2);
                while (true)
                {
                    if (base.la.kind != 5)
                    {
                        this.root.Exprs.Add(base2);
                        while (true)
                        {
                            string[] textArray2 = new string[] { ";" };
                            if (!base.TokenEquals(0, textArray2))
                            {
                                break;
                            }
                            if (base.TokenEquals(1, new int[1]))
                            {
                                return;
                            }
                            this.Back_Assign(out base2);
                            while (true)
                            {
                                if (base.la.kind != 5)
                                {
                                    this.root.Exprs.Add(base2);
                                    break;
                                }
                                base.Get();
                            }
                        }
                        break;
                    }
                    base.Get();
                }
            }
        }

        private void Back_RelativeValueExpr(out NBase res)
        {
            res = null;
            if (base.la.kind == 0x43)
            {
                base.Get();
            }
            else if (base.la.kind == 0x44)
            {
                base.Get();
            }
            else
            {
                base.SynErr(90);
            }
            res = new NRelative(base.t.val, null, NRelative.NKind.Value);
        }

        private void BitAndExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.EqlExpr(out res);
            while (base.la.kind == 0x10)
            {
                base.Get();
                this.EqlExpr(out base2);
                res = new NBinary(NBinary.NKind.And, res, base2);
            }
        }

        private void BitOrExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.BitXorExpr(out res);
            while (base.la.kind == 14)
            {
                base.Get();
                this.BitXorExpr(out base2);
                res = new NBinary(NBinary.NKind.Or, res, base2);
            }
        }

        private void BitXorExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.BitAndExpr(out res);
            while (base.la.kind == 15)
            {
                base.Get();
                this.BitAndExpr(out base2);
                res = new NBinary(NBinary.NKind.Xor, res, base2);
            }
        }

        private void CanExecute_AtomExpr(out NBase res)
        {
            this.Execute_AtomExpr(out res);
        }

        private void CanExecute_ExprRoot()
        {
            this.ExprRoot();
        }

        private void ConditionExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            NBase base3 = null;
            this.NullCoalescingExpr(out res);
            if (base.la.kind == 7)
            {
                base.Get();
                this.Expr(out base2);
                base.Expect(8);
                this.Expr(out base3);
                res = new NTernary(NTernary.NKind.Condition, res, base2, base3);
            }
        }

        private void ConstExpr(out NBase res)
        {
            res = null;
            int kind = base.la.kind;
            switch (kind)
            {
                case 2:
                    base.Get();
                    res = new NConstant(NConstant.NKind.Integer, ParserHelper.ParseInt(base.t.val));
                    return;

                case 3:
                    base.Get();
                    res = new NConstant(NConstant.NKind.Float, ParserHelper.ParseFloat(base.t.val));
                    return;

                case 4:
                    base.Get();
                    res = new NConstant(NConstant.NKind.String, ParserHelper.ParseString(base.t.val));
                    return;
            }
            switch (kind)
            {
                case 0x40:
                    base.Get();
                    res = new NConstant(NConstant.NKind.Boolean, ParserHelper.ParseBool(base.t.val));
                    return;

                case 0x41:
                    base.Get();
                    res = new NConstant(NConstant.NKind.Boolean, ParserHelper.ParseBool(base.t.val));
                    return;

                case 0x42:
                    base.Get();
                    res = new NConstant(NConstant.NKind.Null, null);
                    return;
            }
            base.SynErr(0x55);
        }

        protected override ErrorsBase CreateErrorHandler(IParserErrorHandler errorHandler) => 
            new Errors(errorHandler);

        private void DXBinding()
        {
            if (this.Mode == ParserMode.BindingExpr)
            {
                this.ExprRoot();
            }
            else if (this.Mode == ParserMode.BindingBackExpr)
            {
                this.Back_ExprRoot();
            }
            else if (this.Mode == ParserMode.CommandExecute)
            {
                this.Execute_ExprRoot();
            }
            else if (this.Mode == ParserMode.CommandCanExecute)
            {
                this.CanExecute_ExprRoot();
            }
            else if (this.Mode == ParserMode.Event)
            {
                this.Event_ExprRoot();
            }
            else
            {
                base.SynErr(0x49);
            }
            base.Expect(0);
        }

        private void EqlExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.RelExpr(out res);
            while (base.StartOf(2))
            {
                if ((base.la.kind != 0x11) && (base.la.kind != 0x12))
                {
                    if (base.la.kind == 0x13)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    this.RelExpr(out base2);
                    res = new NBinary(NBinary.NKind.Equal, res, base2);
                    continue;
                }
                if (base.la.kind == 0x11)
                {
                    base.Get();
                }
                else
                {
                    base.Get();
                }
                this.RelExpr(out base2);
                res = new NBinary(NBinary.NKind.NotEqual, res, base2);
            }
        }

        private void Event_AtomExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(5))
            {
                this.ConstExpr(out res);
            }
            else if (base.StartOf(8))
            {
                this.Event_RelativeExpr(out res);
            }
            else if (base.la.kind == 0x3a)
            {
                this.TypeOfExpr(out res);
            }
            else if (base.la.kind == 0x3d)
            {
                this.TypeNewExpr(out res);
            }
            else if (this.NextIs_TypeExpr(1))
            {
                this.TypeIdentExpr(out res, true);
            }
            else if (this.NextIs_MethodExpr(1))
            {
                this.MethodExpr(out res);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out res);
            }
            else if (base.la.kind == 0x3e)
            {
                this.IndexExpr(out res);
            }
            else if (base.la.kind != 0x2a)
            {
                base.SynErr(0x54);
            }
            else
            {
                base.Get();
                if (this.NextIs_TypeExpr(1))
                {
                    this.TypeIdentExpr(out res, false);
                    ((NType) res).Kind = NType.NKind.Attached;
                }
                else if (base.StartOf(1))
                {
                    this.Expr(out res);
                }
                else
                {
                    base.SynErr(0x53);
                }
                base.Expect(0x2b);
            }
            this.ReadNextIdents(ref res, true);
        }

        private void Event_ExprRoot()
        {
            this.Assign_ExprRoot();
        }

        private void Event_RelativeExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(6))
            {
                this.RelativeExpr(out res);
            }
            else if (base.la.kind == 70)
            {
                base.Get();
                res = new NRelative(base.t.val, null, NRelative.NKind.Sender);
            }
            else if (base.la.kind != 0x47)
            {
                base.SynErr(0x5c);
            }
            else
            {
                base.Get();
                res = new NRelative(base.t.val, null, NRelative.NKind.Args);
            }
        }

        private void Execute_AtomExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(5))
            {
                this.ConstExpr(out res);
            }
            else if (base.StartOf(7))
            {
                this.Execute_RelativeExpr(out res);
            }
            else if (base.la.kind == 0x3a)
            {
                this.TypeOfExpr(out res);
            }
            else if (base.la.kind == 0x3d)
            {
                this.TypeNewExpr(out res);
            }
            else if (this.NextIs_TypeExpr(1))
            {
                this.TypeIdentExpr(out res, true);
            }
            else if (this.NextIs_MethodExpr(1))
            {
                this.MethodExpr(out res);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out res);
            }
            else if (base.la.kind == 0x3e)
            {
                this.IndexExpr(out res);
            }
            else if (base.la.kind != 0x2a)
            {
                base.SynErr(0x52);
            }
            else
            {
                base.Get();
                if (this.NextIs_TypeExpr(1))
                {
                    this.TypeIdentExpr(out res, false);
                    ((NType) res).Kind = NType.NKind.Attached;
                }
                else if (base.StartOf(1))
                {
                    this.Expr(out res);
                }
                else
                {
                    base.SynErr(0x51);
                }
                base.Expect(0x2b);
            }
            this.ReadNextIdents(ref res, true);
        }

        private void Execute_ExprRoot()
        {
            this.Assign_ExprRoot();
        }

        private void Execute_RelativeExpr(out NBase res)
        {
            res = null;
            if (base.StartOf(6))
            {
                this.RelativeExpr(out res);
            }
            else if (base.la.kind != 0x45)
            {
                base.SynErr(0x5b);
            }
            else
            {
                base.Get();
                res = new NRelative(base.t.val, null, NRelative.NKind.Parameter);
            }
        }

        private void Expr(out NBase res)
        {
            this.ConditionExpr(out res);
        }

        private void ExprRoot()
        {
            NBase base2;
            this.root = new NRoot();
            this.Expr(out base2);
            this.root.Exprs.Add(base2);
        }

        protected override int GetMaxTokenKind() => 
            0x48;

        protected override bool[,] GetSet() => 
            new bool[,] { { 
                true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false
            }, { 
                false, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, true, true, false, false, false, true, true, true, false, true, true, true, true,
                true, true, true, true, true, true, true, true, true, false, true, false, true, true, true, false,
                true, true, true, true, true, true, true, true, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, true, true, true, true, true, true, true, true, true, true, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true,
                true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false
            }, { 
                false, false, true, true, true, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                true, true, true, false, false, false, false, false, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true,
                true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true,
                true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false, true, false, false, false, false
            }, { 
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true,
                true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false, false, true, true, false, false
            } };

        private void IdentExpr(out NBase res)
        {
            res = null;
            base.Expect(1);
            res = new NIdent(base.t.val, null);
        }

        private void IndexExpr(out NBase res)
        {
            res = null;
            NArgs args = new NArgs();
            NBase base2 = null;
            base.Expect(0x3e);
            this.Expr(out base2);
            args.Add(base2);
            while (base.la.kind == 0x39)
            {
                base.Get();
                this.Expr(out base2);
                args.Add(base2);
            }
            base.Expect(0x3f);
            res = new NIndex(null, args);
        }

        protected override bool IsEOF(int n) => 
            n == 0;

        private void MethodArgsExpr(out NArgs res)
        {
            res = new NArgs();
            base.Expect(0x2a);
            if (base.StartOf(1))
            {
                NBase base2;
                this.Expr(out base2);
                res.Add(base2);
                while (base.la.kind == 0x39)
                {
                    base.Get();
                    this.Expr(out base2);
                    res.Add(base2);
                }
            }
            base.Expect(0x2b);
        }

        private void MethodExpr(out NBase res)
        {
            NArgs args;
            res = null;
            base.Expect(1);
            string val = base.t.val;
            this.MethodArgsExpr(out args);
            res = new NMethod(val, null, args);
        }

        private void MulExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.UnaryExpr(out res);
            while ((base.la.kind == 0x25) || ((base.la.kind == 0x26) || (base.la.kind == 0x27)))
            {
                if (base.la.kind == 0x25)
                {
                    base.Get();
                    this.UnaryExpr(out base2);
                    res = new NBinary(NBinary.NKind.Mul, res, base2);
                    continue;
                }
                if (base.la.kind == 0x26)
                {
                    base.Get();
                    this.UnaryExpr(out base2);
                    res = new NBinary(NBinary.NKind.Div, res, base2);
                    continue;
                }
                base.Get();
                this.UnaryExpr(out base2);
                res = new NBinary(NBinary.NKind.Mod, res, base2);
            }
        }

        private void NextIdentExpr(out NBase res, bool allowMethod)
        {
            res = null;
            base.Expect(0x3b);
            if (base.la.kind == 0x2a)
            {
                base.Get();
                this.TypeIdentExpr(out res, false);
                base.Expect(0x2b);
                ((NType) res).Kind = NType.NKind.Attached;
            }
            else if (allowMethod && this.NextIs_MethodExpr(1))
            {
                this.MethodExpr(out res);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out res);
            }
            else
            {
                base.SynErr(0x58);
            }
        }

        private bool NextIs_AttachedPropExpr(int pos)
        {
            int num;
            string[] textArray1 = new string[] { "(" };
            if (!base.TokenEquals(pos, textArray1))
            {
                return false;
            }
            if (!this.NextIs_TypeExpr(pos + 1, out num))
            {
                return false;
            }
            string[] textArray2 = new string[] { "." };
            if (!base.TokenEquals((pos + 1) + num, textArray2))
            {
                return false;
            }
            int[] numArray1 = new int[] { 1 };
            if (!base.TokenEquals((pos + 2) + num, numArray1))
            {
                return false;
            }
            string[] textArray3 = new string[] { ")" };
            return base.TokenEquals((pos + 3) + num, textArray3);
        }

        private bool NextIs_IdentExpr(int pos)
        {
            int[] numArray1 = new int[] { 1 };
            if (!base.TokenEquals(pos, numArray1))
            {
                return false;
            }
            string[] textArray1 = new string[] { "(" };
            return !base.TokenEquals(pos + 1, textArray1);
        }

        private bool NextIs_MethodExpr(int pos)
        {
            int[] numArray1 = new int[] { 1 };
            if (!base.TokenEquals(pos, numArray1))
            {
                return false;
            }
            string[] textArray1 = new string[] { "(" };
            return base.TokenEquals(pos + 1, textArray1);
        }

        private bool NextIs_TypeExpr(int pos)
        {
            int num;
            return this.NextIs_TypeExpr(pos, out num);
        }

        private bool NextIs_TypeExpr(int pos, out int length)
        {
            length = 0;
            if (base.TokenEquals(pos + length, NType.PrimitiveTypes))
            {
                length++;
                string[] textArray1 = new string[] { "?" };
                if (base.TokenEquals(pos + length, textArray1))
                {
                    length++;
                }
                return true;
            }
            string[] textArray2 = new string[] { "$" };
            if (base.TokenEquals(pos + length, textArray2))
            {
                int[] numArray1 = new int[] { 1 };
                if (base.TokenEquals((pos + length) + 1, numArray1))
                {
                    length += 2;
                    string[] textArray3 = new string[] { ":" };
                    if (base.TokenEquals(pos + length, textArray3))
                    {
                        int[] numArray2 = new int[] { 1 };
                        if (base.TokenEquals((pos + length) + 1, numArray2))
                        {
                            length += 2;
                            string[] textArray4 = new string[] { "?" };
                            if (base.TokenEquals(pos + length, textArray4))
                            {
                                length++;
                            }
                        }
                    }
                    return true;
                }
            }
            length = 0;
            return false;
        }

        private bool NextIs_TypeExprWrapped(int pos, string lParen, string rParen)
        {
            int num;
            string[] textArray1 = new string[] { lParen };
            if (!base.TokenEquals(pos, textArray1))
            {
                return false;
            }
            if (!this.NextIs_TypeExpr(pos + 1, out num))
            {
                return false;
            }
            string[] textArray2 = new string[] { rParen };
            return base.TokenEquals((pos + 1) + num, textArray2);
        }

        private void NullCoalescingExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.OrExpr(out res);
            while (base.la.kind == 9)
            {
                base.Get();
                this.OrExpr(out base2);
                res = new NBinary(NBinary.NKind.Coalesce, res, base2);
            }
        }

        private void OrExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.AndExpr(out res);
            while ((base.la.kind == 10) || (base.la.kind == 11))
            {
                if (base.la.kind == 10)
                {
                    base.Get();
                }
                else
                {
                    base.Get();
                }
                this.AndExpr(out base2);
                res = new NBinary(NBinary.NKind.OrElse, res, base2);
            }
        }

        protected override void ParseRoot()
        {
            this.DXBinding();
            base.Expect(0);
        }

        protected override void Pragmas()
        {
        }

        private void ReadNextIdents(ref NBase n, bool allowMethod)
        {
            NBase base4;
            NIdentBase base2 = n as NIdentBase;
            NBase res = null;
            while (base.la.kind == 0x3e)
            {
                this.IndexExpr(out res);
                if (base2 == null)
                {
                    n = base4 = new NExprIdent(n, null);
                    base2 = (NIdentBase) base4;
                }
                base2 = base2.Next = (NIdentBase) res;
            }
            while (base.la.kind == 0x3b)
            {
                this.NextIdentExpr(out res, allowMethod);
                if (base2 == null)
                {
                    n = base4 = new NExprIdent(n, null);
                    base2 = (NIdentBase) base4;
                }
                for (base2 = base2.Next = (NIdentBase) res; base.la.kind == 0x3e; base2 = base2.Next = (NIdentBase) res)
                {
                    this.IndexExpr(out res);
                }
            }
        }

        private void RelativeExpr(out NBase res)
        {
            res = null;
            switch (base.la.kind)
            {
                case 0x2c:
                case 0x2d:
                    if (base.la.kind == 0x2c)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Context);
                    return;

                case 0x2e:
                case 0x2f:
                    if (base.la.kind == 0x2e)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Self);
                    return;

                case 0x30:
                case 0x31:
                    if (base.la.kind == 0x30)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Parent);
                    return;

                case 50:
                case 0x33:
                    if (base.la.kind == 50)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Element);
                    base.Expect(0x2a);
                    base.Expect(1);
                    ((NRelative) res).ElementName = base.t.val;
                    base.Expect(0x2b);
                    return;

                case 0x34:
                case 0x35:
                    if (base.la.kind == 0x34)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Resource);
                    base.Expect(0x2a);
                    base.Expect(1);
                    ((NRelative) res).ResourceName = base.t.val;
                    base.Expect(0x2b);
                    return;

                case 0x36:
                    base.Get();
                    res = new NRelative(base.t.val, null, NRelative.NKind.Reference);
                    base.Expect(0x2a);
                    base.Expect(1);
                    ((NRelative) res).ReferenceName = base.t.val;
                    base.Expect(0x2b);
                    return;

                case 0x37:
                case 0x38:
                    NBase base2;
                    if (base.la.kind == 0x37)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    res = new NRelative(base.t.val, null, NRelative.NKind.Ancestor);
                    base.Expect(0x2a);
                    this.TypeExpr(out base2);
                    ((NRelative) res).AncestorType = (NType) base2;
                    if (base.la.kind == 0x39)
                    {
                        base.Get();
                        base.Expect(2);
                        ((NRelative) res).AncestorLevel = new int?((int) ParserHelper.ParseInt(base.t.val));
                    }
                    base.Expect(0x2b);
                    return;
            }
            base.SynErr(0x56);
        }

        private void RelExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.ShiftExpr(out res);
            while (base.StartOf(3))
            {
                int kind = base.la.kind;
                switch (kind)
                {
                    case 0x15:
                    case 0x16:
                    {
                        if (base.la.kind == 0x15)
                        {
                            base.Get();
                        }
                        else
                        {
                            base.Get();
                        }
                        this.ShiftExpr(out base2);
                        res = new NBinary(NBinary.NKind.Less, res, base2);
                        continue;
                    }
                    case 0x17:
                    case 0x18:
                    {
                        if (base.la.kind == 0x17)
                        {
                            base.Get();
                        }
                        else
                        {
                            base.Get();
                        }
                        this.ShiftExpr(out base2);
                        res = new NBinary(NBinary.NKind.Greater, res, base2);
                        continue;
                    }
                    case 0x19:
                    case 0x1a:
                    {
                        if (base.la.kind == 0x19)
                        {
                            base.Get();
                        }
                        else
                        {
                            base.Get();
                        }
                        this.ShiftExpr(out base2);
                        res = new NBinary(NBinary.NKind.LessOrEqual, res, base2);
                        continue;
                    }
                    case 0x1b:
                    case 0x1c:
                    {
                        if (base.la.kind == 0x1b)
                        {
                            base.Get();
                        }
                        else
                        {
                            base.Get();
                        }
                        this.ShiftExpr(out base2);
                        res = new NBinary(NBinary.NKind.GreaterOrEqual, res, base2);
                        continue;
                    }
                    case 0x1d:
                    {
                        base.Get();
                        this.TypeExpr(out base2);
                        res = new NCast(NCast.NKind.Is, res, (NType) base2);
                        continue;
                    }
                    case 30:
                    {
                        base.Get();
                        this.TypeExpr(out base2);
                        res = new NCast(NCast.NKind.As, res, (NType) base2);
                        continue;
                    }
                }
            }
        }

        private void ShiftExpr(out NBase res)
        {
            res = null;
            NBase base2 = null;
            this.AddExpr(out res);
            while (base.StartOf(4))
            {
                if ((base.la.kind != 0x1f) && (base.la.kind != 0x20))
                {
                    if (base.la.kind == 0x21)
                    {
                        base.Get();
                    }
                    else
                    {
                        base.Get();
                    }
                    this.AddExpr(out base2);
                    res = new NBinary(NBinary.NKind.ShiftRight, res, base2);
                    continue;
                }
                if (base.la.kind == 0x1f)
                {
                    base.Get();
                }
                else
                {
                    base.Get();
                }
                this.AddExpr(out base2);
                res = new NBinary(NBinary.NKind.ShiftLeft, res, base2);
            }
        }

        private void TypeExpr(out NBase res)
        {
            res = null;
            string name = string.Empty;
            if (base.TokenEquals(1, NType.PrimitiveTypes))
            {
                base.Expect(1);
                name = base.t.val;
            }
            else if (base.la.kind != 60)
            {
                base.SynErr(0x4c);
            }
            else
            {
                base.Get();
                base.Expect(1);
                name = base.t.val;
                if (base.la.kind == 8)
                {
                    base.Get();
                    base.Expect(1);
                    name = name + ":" + base.t.val;
                }
            }
            res = new NType(name, null, NType.NKind.Type, null);
            if (base.la.kind == 7)
            {
                base.Get();
                ((NType) res).IsNullable = true;
            }
        }

        private void TypeIdentExpr(out NBase res, bool allowMethod)
        {
            res = null;
            NBase base2 = null;
            this.TypeExpr(out res);
            base.Expect(0x3b);
            ((NType) res).Kind = NType.NKind.Static;
            if (allowMethod && this.NextIs_MethodExpr(1))
            {
                this.MethodExpr(out base2);
            }
            else if (base.la.kind == 1)
            {
                this.IdentExpr(out base2);
            }
            else
            {
                base.SynErr(0x57);
            }
            ((NType) res).Ident = (NIdentBase) base2;
        }

        private void TypeNewExpr(out NBase res)
        {
            NBase base2;
            NArgs args;
            res = null;
            base.Expect(0x3d);
            this.TypeExpr(out base2);
            this.MethodArgsExpr(out args);
            res = new NNew((NType) base2, null, args);
        }

        private void TypeOfExpr(out NBase res)
        {
            base.Expect(0x3a);
            base.Expect(0x2a);
            this.TypeExpr(out res);
            base.Expect(0x2b);
            ((NType) res).Kind = NType.NKind.TypeOf;
        }

        private void UnaryExpr(out NBase res)
        {
            res = null;
            List<NUnaryBase> source = new List<NUnaryBase>();
            while (true)
            {
                if (base.StartOf(1))
                {
                    if (base.la.kind == 0x23)
                    {
                        base.Get();
                        source.Add(new NUnary(NUnary.NKind.Plus, null));
                        continue;
                    }
                    if (base.la.kind == 0x24)
                    {
                        base.Get();
                        source.Add(new NUnary(NUnary.NKind.Minus, null));
                        continue;
                    }
                    if (base.la.kind == 40)
                    {
                        base.Get();
                        source.Add(new NUnary(NUnary.NKind.NotBitwise, null));
                        continue;
                    }
                    if (base.la.kind == 0x29)
                    {
                        base.Get();
                        source.Add(new NUnary(NUnary.NKind.Not, null));
                        continue;
                    }
                    if (this.NextIs_TypeExprWrapped(1, "(", ")"))
                    {
                        NBase base2;
                        base.Expect(0x2a);
                        this.TypeExpr(out base2);
                        source.Add(new NCast(NCast.NKind.Cast, null, (NType) base2));
                        base.Expect(0x2b);
                        continue;
                    }
                }
                this.AtomRootExpr(out res);
                for (int i = 0; i < (source.Count - 1); i++)
                {
                    source[i].Value = source[i + 1];
                }
                if (source.Count > 0)
                {
                    source.Last<NUnaryBase>().Value = res;
                    res = source.First<NUnaryBase>();
                }
                return;
            }
        }

        public ParserMode Mode { get; set; }

        public NRoot Root =>
            this.root;
    }
}

