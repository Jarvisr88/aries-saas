namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class DefaultCustomUIFilterFactory : ICustomUIFilterFactory
    {
        internal static readonly ICustomUIFilterFactory Instance = new DefaultCustomUIFilterFactory();
        private static readonly MatchCase[] matches;

        static DefaultCustomUIFilterFactory()
        {
            MatchCase[] caseArray1 = new MatchCase[15];
            caseArray1[0] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterNone.Match), (filterType, getServiceProvider) => new CustomUIFilterNone(filterType, getServiceProvider));
            caseArray1[1] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterBinary.Match), (filterType, getServiceProvider) => new CustomUIFilterBinary(filterType, getServiceProvider));
            caseArray1[2] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterNull.Match), (filterType, getServiceProvider) => new CustomUIFilterNull(filterType, getServiceProvider));
            caseArray1[3] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterBetween.Match), (filterType, getServiceProvider) => new CustomUIFilterBetween(filterType, getServiceProvider));
            caseArray1[4] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterAggregate.Match), (filterType, getServiceProvider) => new CustomUIFilterAggregate(filterType, getServiceProvider));
            caseArray1[5] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterSequence.Match), (filterType, getServiceProvider) => new CustomUIFilterSequence(filterType, getServiceProvider));
            caseArray1[6] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterTextFunction.Match), (filterType, getServiceProvider) => new CustomUIFilterTextFunction(filterType, getServiceProvider));
            caseArray1[7] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterBlank.Match), (filterType, getServiceProvider) => new CustomUIFilterBlank(filterType, getServiceProvider));
            caseArray1[8] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterLike.Match), (filterType, getServiceProvider) => new CustomUIFilterLike(filterType, getServiceProvider));
            caseArray1[9] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterIsSameDay.Match), (filterType, getServiceProvider) => new CustomUIFilterIsSameDay(filterType, getServiceProvider));
            caseArray1[10] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterDateFunction.Match), (filterType, getServiceProvider) => new CustomUIFilterDateFunction(filterType, getServiceProvider));
            caseArray1[11] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterDatePeriod.Match), (filterType, getServiceProvider) => new CustomUIFilterDatePeriod(filterType, getServiceProvider));
            caseArray1[12] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterDatePeriods.Match), (filterType, getServiceProvider) => new CustomUIFilterDatePeriods(filterType, getServiceProvider));
            caseArray1[13] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterCustom.Match), (filterType, getServiceProvider) => new CustomUIFilterCustom(filterType, getServiceProvider));
            caseArray1[14] = new MatchCase(new Func<CustomUIFilterType, bool>(CustomUIFilterUserDefined.Match), (filterType, getServiceProvider) => new CustomUIFilterUserDefined(filterType, getServiceProvider));
            matches = caseArray1;
        }

        private DefaultCustomUIFilterFactory()
        {
        }

        ICustomUIFilter ICustomUIFilterFactory.Create(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider)
        {
            for (int i = 0; i < matches.Length; i++)
            {
                ICustomUIFilter filter;
                if (matches[i].TryCreate(filterType, getServiceProvider, out filter))
                {
                    return filter;
                }
            }
            throw new NotSupportedException(filterType.ToString());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultCustomUIFilterFactory.<>c <>9 = new DefaultCustomUIFilterFactory.<>c();

            internal ICustomUIFilter <.cctor>b__5_0(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterNone(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_1(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterBinary(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_10(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterDateFunction(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_11(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterDatePeriod(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_12(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterDatePeriods(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_13(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterCustom(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_14(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterUserDefined(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_2(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterNull(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_3(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterBetween(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_4(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterAggregate(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_5(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterSequence(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_6(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterTextFunction(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_7(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterBlank(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_8(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterLike(filterType, getServiceProvider);

            internal ICustomUIFilter <.cctor>b__5_9(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) => 
                new CustomUIFilterIsSameDay(filterType, getServiceProvider);
        }

        private sealed class MatchCase
        {
            private readonly Func<CustomUIFilterType, bool> match;
            private readonly Func<CustomUIFilterType, Func<IServiceProvider>, ICustomUIFilter> create;

            public MatchCase(Func<CustomUIFilterType, bool> match, Func<CustomUIFilterType, Func<IServiceProvider>, ICustomUIFilter> initializer)
            {
                this.match = match;
                this.create = initializer;
            }

            public bool TryCreate(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider, out ICustomUIFilter filter)
            {
                filter = null;
                if (this.match(filterType))
                {
                    filter = this.create(filterType, getServiceProvider);
                }
                return (filter != null);
            }
        }
    }
}

