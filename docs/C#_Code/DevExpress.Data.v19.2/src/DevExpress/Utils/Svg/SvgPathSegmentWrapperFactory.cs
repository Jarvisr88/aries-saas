namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class SvgPathSegmentWrapperFactory : ISvgPathSegmentWrapperFactory
    {
        public static readonly ISvgPathSegmentWrapperFactory Default = new SvgPathSegmentWrapperFactory();
        private readonly IDictionary<Type, Func<SvgPathSegment, SvgPathSegmentWrapper>> providers = new Dictionary<Type, Func<SvgPathSegment, SvgPathSegmentWrapper>>();

        public SvgPathSegmentWrapperFactory()
        {
            Func<SvgPathSegment, SvgPathSegmentWrapper> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = segment => new SvgPathMoveToSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathMoveToSegment), func1);
            Func<SvgPathSegment, SvgPathSegmentWrapper> func2 = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local2 = <>c.<>9__2_1;
                func2 = <>c.<>9__2_1 = segment => new SvgPathCloseSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathCloseSegment), func2);
            Func<SvgPathSegment, SvgPathSegmentWrapper> func3 = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local3 = <>c.<>9__2_2;
                func3 = <>c.<>9__2_2 = segment => new SvgPathLineToSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathLineToSegment), func3);
            Func<SvgPathSegment, SvgPathSegmentWrapper> func4 = <>c.<>9__2_3;
            if (<>c.<>9__2_3 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local4 = <>c.<>9__2_3;
                func4 = <>c.<>9__2_3 = segment => new SvgPathCurveToCubicSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathCurveToCubicSegment), func4);
            Func<SvgPathSegment, SvgPathSegmentWrapper> func5 = <>c.<>9__2_4;
            if (<>c.<>9__2_4 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local5 = <>c.<>9__2_4;
                func5 = <>c.<>9__2_4 = segment => new SvgPathCurveToQuadraticSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathCurveToQuadraticSegment), func5);
            Func<SvgPathSegment, SvgPathSegmentWrapper> func6 = <>c.<>9__2_5;
            if (<>c.<>9__2_5 == null)
            {
                Func<SvgPathSegment, SvgPathSegmentWrapper> local6 = <>c.<>9__2_5;
                func6 = <>c.<>9__2_5 = segment => new SvgPathArcSegmentWrapper(segment);
            }
            this.providers.Add(typeof(SvgPathArcSegment), func6);
        }

        public SvgPathSegmentWrapper Wrap(SvgPathSegment element) => 
            this.providers[element.GetType()](element);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgPathSegmentWrapperFactory.<>c <>9 = new SvgPathSegmentWrapperFactory.<>c();
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_0;
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_1;
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_2;
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_3;
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_4;
            public static Func<SvgPathSegment, SvgPathSegmentWrapper> <>9__2_5;

            internal SvgPathSegmentWrapper <.ctor>b__2_0(SvgPathSegment segment) => 
                new SvgPathMoveToSegmentWrapper(segment);

            internal SvgPathSegmentWrapper <.ctor>b__2_1(SvgPathSegment segment) => 
                new SvgPathCloseSegmentWrapper(segment);

            internal SvgPathSegmentWrapper <.ctor>b__2_2(SvgPathSegment segment) => 
                new SvgPathLineToSegmentWrapper(segment);

            internal SvgPathSegmentWrapper <.ctor>b__2_3(SvgPathSegment segment) => 
                new SvgPathCurveToCubicSegmentWrapper(segment);

            internal SvgPathSegmentWrapper <.ctor>b__2_4(SvgPathSegment segment) => 
                new SvgPathCurveToQuadraticSegmentWrapper(segment);

            internal SvgPathSegmentWrapper <.ctor>b__2_5(SvgPathSegment segment) => 
                new SvgPathArcSegmentWrapper(segment);
        }
    }
}

