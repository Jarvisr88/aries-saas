namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class SvgElementWrapperFactory : ISvgElementWrapperFactory
    {
        public static readonly ISvgElementWrapperFactory Default = new SvgElementWrapperFactory();
        private readonly IDictionary<Type, Func<SvgElement, SvgElementWrapper>> providers = new Dictionary<Type, Func<SvgElement, SvgElementWrapper>>(14);

        public SvgElementWrapperFactory()
        {
            Func<SvgElement, SvgElementWrapper> func1 = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<SvgElement, SvgElementWrapper> local1 = <>c.<>9__2_0;
                func1 = <>c.<>9__2_0 = element => new SvgCircleWrapper(element);
            }
            this.providers.Add(typeof(SvgCircle), func1);
            Func<SvgElement, SvgElementWrapper> func2 = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<SvgElement, SvgElementWrapper> local2 = <>c.<>9__2_1;
                func2 = <>c.<>9__2_1 = element => new SvgLineWrapper(element);
            }
            this.providers.Add(typeof(SvgLine), func2);
            Func<SvgElement, SvgElementWrapper> func3 = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<SvgElement, SvgElementWrapper> local3 = <>c.<>9__2_2;
                func3 = <>c.<>9__2_2 = element => new SvgPathWrapper(element);
            }
            this.providers.Add(typeof(SvgPath), func3);
            Func<SvgElement, SvgElementWrapper> func4 = <>c.<>9__2_3;
            if (<>c.<>9__2_3 == null)
            {
                Func<SvgElement, SvgElementWrapper> local4 = <>c.<>9__2_3;
                func4 = <>c.<>9__2_3 = element => new SvgPolygonWrapper(element);
            }
            this.providers.Add(typeof(SvgPolygon), func4);
            Func<SvgElement, SvgElementWrapper> func5 = <>c.<>9__2_4;
            if (<>c.<>9__2_4 == null)
            {
                Func<SvgElement, SvgElementWrapper> local5 = <>c.<>9__2_4;
                func5 = <>c.<>9__2_4 = element => new SvgPolylineWrapper(element);
            }
            this.providers.Add(typeof(SvgPolyline), func5);
            Func<SvgElement, SvgElementWrapper> func6 = <>c.<>9__2_5;
            if (<>c.<>9__2_5 == null)
            {
                Func<SvgElement, SvgElementWrapper> local6 = <>c.<>9__2_5;
                func6 = <>c.<>9__2_5 = element => new SvgRectangleWrapper(element);
            }
            this.providers.Add(typeof(SvgRectangle), func6);
            Func<SvgElement, SvgElementWrapper> func7 = <>c.<>9__2_6;
            if (<>c.<>9__2_6 == null)
            {
                Func<SvgElement, SvgElementWrapper> local7 = <>c.<>9__2_6;
                func7 = <>c.<>9__2_6 = element => new SvgEllipseWrapper(element);
            }
            this.providers.Add(typeof(SvgEllipse), func7);
            Func<SvgElement, SvgElementWrapper> func8 = <>c.<>9__2_7;
            if (<>c.<>9__2_7 == null)
            {
                Func<SvgElement, SvgElementWrapper> local8 = <>c.<>9__2_7;
                func8 = <>c.<>9__2_7 = element => new SvgGroupWrapper(element);
            }
            this.providers.Add(typeof(SvgGroup), func8);
            Func<SvgElement, SvgElementWrapper> func9 = <>c.<>9__2_8;
            if (<>c.<>9__2_8 == null)
            {
                Func<SvgElement, SvgElementWrapper> local9 = <>c.<>9__2_8;
                func9 = <>c.<>9__2_8 = element => new SvgRootWrapper(element);
            }
            this.providers.Add(typeof(SvgRoot), func9);
            Func<SvgElement, SvgElementWrapper> func10 = <>c.<>9__2_9;
            if (<>c.<>9__2_9 == null)
            {
                Func<SvgElement, SvgElementWrapper> local10 = <>c.<>9__2_9;
                func10 = <>c.<>9__2_9 = element => new SvgDefinitionsWrapper(element);
            }
            this.providers.Add(typeof(SvgDefinitions), func10);
            Func<SvgElement, SvgElementWrapper> func11 = <>c.<>9__2_10;
            if (<>c.<>9__2_10 == null)
            {
                Func<SvgElement, SvgElementWrapper> local11 = <>c.<>9__2_10;
                func11 = <>c.<>9__2_10 = element => new SvgUseWrapper(element);
            }
            this.providers.Add(typeof(SvgUse), func11);
            Func<SvgElement, SvgElementWrapper> func12 = <>c.<>9__2_11;
            if (<>c.<>9__2_11 == null)
            {
                Func<SvgElement, SvgElementWrapper> local12 = <>c.<>9__2_11;
                func12 = <>c.<>9__2_11 = element => new SvgTransformGroupWrapper(element);
            }
            this.providers.Add(typeof(SvgTransformGroup), func12);
            Func<SvgElement, SvgElementWrapper> func13 = <>c.<>9__2_12;
            if (<>c.<>9__2_12 == null)
            {
                Func<SvgElement, SvgElementWrapper> local13 = <>c.<>9__2_12;
                func13 = <>c.<>9__2_12 = element => new SvgClipPathWrapper(element);
            }
            this.providers.Add(typeof(SvgClipPath), func13);
            Func<SvgElement, SvgElementWrapper> func14 = <>c.<>9__2_13;
            if (<>c.<>9__2_13 == null)
            {
                Func<SvgElement, SvgElementWrapper> local14 = <>c.<>9__2_13;
                func14 = <>c.<>9__2_13 = element => new SvgMaskWrapper(element);
            }
            this.providers.Add(typeof(SvgMask), func14);
            Func<SvgElement, SvgElementWrapper> func15 = <>c.<>9__2_14;
            if (<>c.<>9__2_14 == null)
            {
                Func<SvgElement, SvgElementWrapper> local15 = <>c.<>9__2_14;
                func15 = <>c.<>9__2_14 = element => new SvgLinearGradientWrapper(element);
            }
            this.providers.Add(typeof(SvgLinearGradient), func15);
            Func<SvgElement, SvgElementWrapper> func16 = <>c.<>9__2_15;
            if (<>c.<>9__2_15 == null)
            {
                Func<SvgElement, SvgElementWrapper> local16 = <>c.<>9__2_15;
                func16 = <>c.<>9__2_15 = element => new SvgRadialGradientWrapper(element);
            }
            this.providers.Add(typeof(SvgRadialGradient), func16);
            Func<SvgElement, SvgElementWrapper> func17 = <>c.<>9__2_16;
            if (<>c.<>9__2_16 == null)
            {
                Func<SvgElement, SvgElementWrapper> local17 = <>c.<>9__2_16;
                func17 = <>c.<>9__2_16 = element => new SvgTextWrapper(element);
            }
            this.providers.Add(typeof(SvgText), func17);
            Func<SvgElement, SvgElementWrapper> func18 = <>c.<>9__2_17;
            if (<>c.<>9__2_17 == null)
            {
                Func<SvgElement, SvgElementWrapper> local18 = <>c.<>9__2_17;
                func18 = <>c.<>9__2_17 = element => new SvgContentWrapper(element);
            }
            this.providers.Add(typeof(SvgContent), func18);
            Func<SvgElement, SvgElementWrapper> func19 = <>c.<>9__2_18;
            if (<>c.<>9__2_18 == null)
            {
                Func<SvgElement, SvgElementWrapper> local19 = <>c.<>9__2_18;
                func19 = <>c.<>9__2_18 = element => new SvgTspanWrapper(element);
            }
            this.providers.Add(typeof(SvgTspan), func19);
        }

        SvgElementWrapper ISvgElementWrapperFactory.Wrap(SvgElement element)
        {
            Func<SvgElement, SvgElementWrapper> func;
            return (!this.providers.TryGetValue(element.GetType(), out func) ? null : func(element));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgElementWrapperFactory.<>c <>9 = new SvgElementWrapperFactory.<>c();
            public static Func<SvgElement, SvgElementWrapper> <>9__2_0;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_1;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_2;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_3;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_4;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_5;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_6;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_7;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_8;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_9;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_10;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_11;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_12;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_13;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_14;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_15;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_16;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_17;
            public static Func<SvgElement, SvgElementWrapper> <>9__2_18;

            internal SvgElementWrapper <.ctor>b__2_0(SvgElement element) => 
                new SvgCircleWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_1(SvgElement element) => 
                new SvgLineWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_10(SvgElement element) => 
                new SvgUseWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_11(SvgElement element) => 
                new SvgTransformGroupWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_12(SvgElement element) => 
                new SvgClipPathWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_13(SvgElement element) => 
                new SvgMaskWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_14(SvgElement element) => 
                new SvgLinearGradientWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_15(SvgElement element) => 
                new SvgRadialGradientWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_16(SvgElement element) => 
                new SvgTextWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_17(SvgElement element) => 
                new SvgContentWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_18(SvgElement element) => 
                new SvgTspanWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_2(SvgElement element) => 
                new SvgPathWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_3(SvgElement element) => 
                new SvgPolygonWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_4(SvgElement element) => 
                new SvgPolylineWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_5(SvgElement element) => 
                new SvgRectangleWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_6(SvgElement element) => 
                new SvgEllipseWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_7(SvgElement element) => 
                new SvgGroupWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_8(SvgElement element) => 
                new SvgRootWrapper(element);

            internal SvgElementWrapper <.ctor>b__2_9(SvgElement element) => 
                new SvgDefinitionsWrapper(element);
        }
    }
}

