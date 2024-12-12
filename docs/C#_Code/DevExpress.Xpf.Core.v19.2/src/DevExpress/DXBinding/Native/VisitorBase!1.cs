namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal abstract class VisitorBase<T>
    {
        protected VisitorBase()
        {
        }

        private IEnumerable<T> Args(IEnumerable<NBase> args)
        {
            List<T> list = new List<T>();
            foreach (NBase base2 in args)
            {
                list.Add(this.Visit(base2));
            }
            return list;
        }

        protected abstract T Assign(NAssign n, T value);
        protected abstract T Binary(NBinary n, T left, T right);
        protected virtual bool CanContinue(NBase n) => 
            true;

        protected abstract T Cast(NCast n, T value);
        protected abstract T Constant(NConstant n);
        protected virtual T ExprIdent(T from, NExprIdent n) => 
            this.Visit(n.Expr);

        protected virtual T Ident(T from, NIdent n)
        {
            throw new NotImplementedException();
        }

        protected virtual T Index(T from, NIndex n, IEnumerable<T> indexArgs)
        {
            throw new NotImplementedException();
        }

        protected static IEnumerable<TEl> MakePlain<TEl>(IEnumerable<IEnumerable<TEl>> list)
        {
            if ((list == null) || ((list.Count<IEnumerable<TEl>>() == 0) || list.Contains<IEnumerable<TEl>>(null)))
            {
                return new TEl[0];
            }
            Func<IEnumerable<TEl>, IEnumerable<TEl>, IEnumerable<TEl>> func = <>c__30<T, TEl>.<>9__30_0;
            if (<>c__30<T, TEl>.<>9__30_0 == null)
            {
                Func<IEnumerable<TEl>, IEnumerable<TEl>, IEnumerable<TEl>> local1 = <>c__30<T, TEl>.<>9__30_0;
                func = <>c__30<T, TEl>.<>9__30_0 = (x, y) => (IEnumerable<TEl>) ((IEnumerable<TEl>) x).Union<TEl>(((IEnumerable<TEl>) y));
            }
            return list.Aggregate<IEnumerable<TEl>>(func).ToList<TEl>();
        }

        protected virtual T Method(T from, NMethod n, IEnumerable<T> methodArgs)
        {
            throw new NotImplementedException();
        }

        protected virtual T Relative(T from, NRelative n)
        {
            throw new NotImplementedException();
        }

        protected abstract T RootIdent(NIdentBase n);
        protected T RootIdentCore(T from, NIdentBase n)
        {
            if (this.CanContinue(n))
            {
                if (n is NIdent)
                {
                    return this.Ident(from, (NIdent) n);
                }
                if (n is NRelative)
                {
                    return this.Relative(from, (NRelative) n);
                }
                if (n is NMethod)
                {
                    IEnumerable<T> methodArgs = this.Args(((NMethod) n).Args);
                    if (this.CanContinue(n))
                    {
                        return this.Method(from, (NMethod) n, methodArgs);
                    }
                    return default(T);
                }
                if (n is NType)
                {
                    return this.Type(from, (NType) n);
                }
                if (n is NExprIdent)
                {
                    return this.ExprIdent(from, (NExprIdent) n);
                }
                if (n is NIndex)
                {
                    IEnumerable<T> indexArgs = this.Args(((NIndex) n).Args);
                    if (this.CanContinue(n))
                    {
                        return this.Index(from, (NIndex) n, indexArgs);
                    }
                    return default(T);
                }
                if (!(n is NNew))
                {
                    throw new NotImplementedException();
                }
                IEnumerable<T> args = this.Args(((NNew) n).Args);
                if (this.CanContinue(n))
                {
                    return this.Type_New(from, (NNew) n, args);
                }
            }
            return default(T);
        }

        protected IEnumerable<T> RootVisit(NRoot n) => 
            n.Exprs.Select<NBase, T>(new Func<NBase, T>(this.Visit)).ToList<T>();

        protected abstract T Ternary(NTernary n, T first, T second, T third);
        private T Type(T from, NType n)
        {
            switch (n.Kind)
            {
                case NType.NKind.Type:
                    return this.Type_Type(from, n);

                case NType.NKind.TypeOf:
                    return this.Type_TypeOf(from, n);

                case NType.NKind.Static:
                    return this.Type_Static(from, n);

                case NType.NKind.Attached:
                    return this.Type_Attached(from, n);
            }
            throw new NotImplementedException();
        }

        protected virtual T Type_Attached(T from, NType n)
        {
            throw new NotImplementedException();
        }

        protected virtual T Type_New(T from, NNew n, IEnumerable<T> args)
        {
            throw new NotImplementedException();
        }

        private T Type_Static(T from, NType n)
        {
            if (n.Ident is NIdent)
            {
                return this.Type_StaticIdent(from, n);
            }
            if (!(n.Ident is NMethod))
            {
                throw new NotImplementedException();
            }
            return this.Type_StaticMethod(from, n, this.Args(((NMethod) n.Ident).Args));
        }

        protected virtual T Type_StaticIdent(T from, NType n)
        {
            throw new NotImplementedException();
        }

        protected virtual T Type_StaticMethod(T from, NType n, IEnumerable<T> methodArgs)
        {
            throw new NotImplementedException();
        }

        protected virtual T Type_Type(T from, NType n)
        {
            throw new NotImplementedException();
        }

        protected virtual T Type_TypeOf(T from, NType n)
        {
            throw new NotImplementedException();
        }

        protected abstract T Unary(NUnary n, T value);
        protected virtual T Visit(NBase n)
        {
            if (!this.CanContinue(n))
            {
                return default(T);
            }
            switch (n)
            {
                case (NIdentBase _):
                    return this.RootIdent((NIdentBase) n);
                    break;
            }
            if (n is NConstant)
            {
                return this.Constant((NConstant) n);
            }
            if (n is NBinary)
            {
                return this.VisitBinary((NBinary) n);
            }
            if (n is NUnary)
            {
                return this.VisitUnary((NUnary) n);
            }
            if (n is NCast)
            {
                return this.VisitCast((NCast) n);
            }
            if (n is NTernary)
            {
                return this.VisitTernary((NTernary) n);
            }
            if (!(n is NAssign))
            {
                throw new NotImplementedException();
            }
            return this.VisitAssign((NAssign) n);
        }

        protected virtual T VisitAssign(NAssign n)
        {
            T local = this.Visit(n.Expr);
            if (this.CanContinue(n))
            {
                return this.Assign(n, local);
            }
            return default(T);
        }

        protected virtual T VisitBinary(NBinary n)
        {
            T left = this.Visit(n.Left);
            T right = this.Visit(n.Right);
            if (this.CanContinue(n))
            {
                return this.Binary(n, left, right);
            }
            return default(T);
        }

        protected virtual T VisitCast(NCast n)
        {
            T local = this.Visit(n.Value);
            if (this.CanContinue(n))
            {
                return this.Cast(n, local);
            }
            return default(T);
        }

        protected virtual T VisitTernary(NTernary n)
        {
            T first = this.Visit(n.First);
            T second = this.Visit(n.Second);
            T third = this.Visit(n.Third);
            if (this.CanContinue(n))
            {
                return this.Ternary(n, first, second, third);
            }
            return default(T);
        }

        protected virtual T VisitUnary(NUnary n)
        {
            T local = this.Visit(n.Value);
            if (this.CanContinue(n))
            {
                return this.Unary(n, local);
            }
            return default(T);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__30<TEl>
        {
            public static readonly VisitorBase<T>.<>c__30<TEl> <>9;
            public static Func<IEnumerable<TEl>, IEnumerable<TEl>, IEnumerable<TEl>> <>9__30_0;

            static <>c__30()
            {
                VisitorBase<T>.<>c__30<TEl>.<>9 = new VisitorBase<T>.<>c__30<TEl>();
            }

            internal IEnumerable<TEl> <MakePlain>b__30_0(IEnumerable<TEl> x, IEnumerable<TEl> y) => 
                x.Union<TEl>(y);
        }
    }
}

