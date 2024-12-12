namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class VisitorType : VisitorBase<IEnumerable<VisitorType.TypeInfo>>
    {
        private static readonly Dictionary<string, Type> primitiveTypeMapping;
        private bool fullPack;

        static VisitorType()
        {
            Dictionary<string, Type> dictionary = new Dictionary<string, Type> {
                { 
                    "sbyte",
                    typeof(sbyte)
                },
                { 
                    "byte",
                    typeof(byte)
                },
                { 
                    "short",
                    typeof(short)
                },
                { 
                    "ushort",
                    typeof(ushort)
                },
                { 
                    "int",
                    typeof(int)
                },
                { 
                    "uint",
                    typeof(uint)
                },
                { 
                    "long",
                    typeof(long)
                },
                { 
                    "ulong",
                    typeof(ulong)
                },
                { 
                    "float",
                    typeof(float)
                },
                { 
                    "double",
                    typeof(double)
                },
                { 
                    "decimal",
                    typeof(decimal)
                },
                { 
                    "bool",
                    typeof(bool)
                },
                { 
                    "object",
                    typeof(object)
                },
                { 
                    "string",
                    typeof(string)
                }
            };
            primitiveTypeMapping = dictionary;
        }

        protected override IEnumerable<TypeInfo> Assign(NAssign n, IEnumerable<TypeInfo> value) => 
            this.Visit(n.Left).Union<TypeInfo>(value);

        protected override IEnumerable<TypeInfo> Binary(NBinary n, IEnumerable<TypeInfo> left, IEnumerable<TypeInfo> right) => 
            left.Union<TypeInfo>(right);

        protected override IEnumerable<TypeInfo> Cast(NCast n, IEnumerable<TypeInfo> value) => 
            value.Union<TypeInfo>(this.Type_Type(null, n.Type));

        protected override IEnumerable<TypeInfo> Constant(NConstant n) => 
            new TypeInfo[0];

        protected override IEnumerable<TypeInfo> Ident(IEnumerable<TypeInfo> from, NIdent n) => 
            new TypeInfo[0];

        protected override IEnumerable<TypeInfo> Index(IEnumerable<TypeInfo> from, NIndex n, IEnumerable<IEnumerable<TypeInfo>> indexArgs) => 
            MakePlain<TypeInfo>(indexArgs);

        protected override IEnumerable<TypeInfo> Method(IEnumerable<TypeInfo> from, NMethod n, IEnumerable<IEnumerable<TypeInfo>> methodArgs) => 
            MakePlain<TypeInfo>(methodArgs);

        protected override IEnumerable<TypeInfo> Relative(IEnumerable<TypeInfo> from, NRelative n) => 
            (n.Kind != NRelative.NKind.Ancestor) ? ((IEnumerable<TypeInfo>) new TypeInfo[0]) : this.Type_Type(from, n.AncestorType);

        public static IEnumerable<TypeInfo> Resolve(NRoot expr, NRoot expr2, bool fullPack = true)
        {
            VisitorType type = new VisitorType {
                fullPack = fullPack
            };
            IEnumerable<TypeInfo> first = MakePlain<TypeInfo>(type.RootVisit(expr));
            if (expr2 != null)
            {
                type.fullPack = true;
                first = first.Union<TypeInfo>(MakePlain<TypeInfo>(type.RootVisit(expr2)));
            }
            return first;
        }

        public static Type ResolveType(TypeInfo type, ITypeResolver typeResolver, IErrorHandler errorHandler)
        {
            Type nullableType = NType.IsPrimitiveType(type.Name) ? primitiveTypeMapping[type.Name] : typeResolver.ResolveType(type.Name);
            if (!type.IsNullable)
            {
                return nullableType;
            }
            if (Nullable.GetUnderlyingType(nullableType) != null)
            {
                errorHandler.Throw(ErrorHelper.Err004(type.Name), null);
                return null;
            }
            Type[] typeArguments = new Type[] { nullableType };
            return typeof(Nullable<>).MakeGenericType(typeArguments);
        }

        protected override IEnumerable<TypeInfo> RootIdent(NIdentBase n)
        {
            NIdentBase base2;
            IEnumerable<TypeInfo> res = new TypeInfo[0];
            VisitorOperand.ReduceIdent(n, delegate (NType x) {
                res = res.Union<TypeInfo>(this.Type_Type(null, x));
                return typeof(object);
            }, out base2, this.fullPack);
            n = base2;
            while (n != null)
            {
                if (!this.CanContinue(n))
                {
                    return new TypeInfo[0];
                }
                res = res.Union<TypeInfo>(base.RootIdentCore(res, n));
                n = n.Next;
            }
            return res;
        }

        protected override IEnumerable<TypeInfo> Ternary(NTernary n, IEnumerable<TypeInfo> first, IEnumerable<TypeInfo> second, IEnumerable<TypeInfo> third) => 
            first.Union<TypeInfo>(second).Union<TypeInfo>(third);

        protected override IEnumerable<TypeInfo> Type_Attached(IEnumerable<TypeInfo> from, NType n) => 
            this.Type_Type(from, n);

        protected override IEnumerable<TypeInfo> Type_New(IEnumerable<TypeInfo> from, NNew n, IEnumerable<IEnumerable<TypeInfo>> args) => 
            this.Type_Type(from, n.Type).Union<TypeInfo>(MakePlain<TypeInfo>(args));

        protected override IEnumerable<TypeInfo> Type_StaticIdent(IEnumerable<TypeInfo> from, NType n) => 
            this.Type_Type(from, n);

        protected override IEnumerable<TypeInfo> Type_StaticMethod(IEnumerable<TypeInfo> from, NType n, IEnumerable<IEnumerable<TypeInfo>> methodArgs) => 
            this.Type_Type(from, n).Union<TypeInfo>(MakePlain<TypeInfo>(methodArgs));

        protected override IEnumerable<TypeInfo> Type_Type(IEnumerable<TypeInfo> from, NType n) => 
            new TypeInfo[] { new TypeInfo(n) };

        protected override IEnumerable<TypeInfo> Type_TypeOf(IEnumerable<TypeInfo> from, NType n) => 
            this.Type_Type(from, n);

        protected override IEnumerable<TypeInfo> Unary(NUnary n, IEnumerable<TypeInfo> value) => 
            value;

        [StructLayout(LayoutKind.Sequential)]
        public struct TypeInfo
        {
            public readonly string Name;
            public readonly bool IsNullable;
            public TypeInfo(string name, bool isNullable)
            {
                this.Name = name;
                this.IsNullable = isNullable;
            }

            public TypeInfo(NType type)
            {
                this.Name = type.Name;
                this.IsNullable = type.IsNullable;
            }
        }
    }
}

