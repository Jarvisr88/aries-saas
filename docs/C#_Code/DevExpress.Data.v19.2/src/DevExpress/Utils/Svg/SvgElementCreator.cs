namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class SvgElementCreator
    {
        private static readonly Dictionary<string, Func<SvgElement>> ElementTypes;
        private static readonly Dictionary<string, Func<double[], SvgTransform>> TransformTypes;
        private static readonly System.ComponentModel.DoubleConverter DoubleConverter;
        private static readonly NullableConverter NullableDoubleConverter;
        private static readonly Dictionary<Type, Dictionary<string, Tuple<Action<object, object>, TypeConverter>>> propertyMutators;

        static SvgElementCreator()
        {
            Dictionary<string, Func<SvgElement>> dictionary1 = new Dictionary<string, Func<SvgElement>>(StringComparer.InvariantCultureIgnoreCase);
            dictionary1.Add("path", () => new SvgPath());
            dictionary1.Add("line", () => new SvgLine());
            dictionary1.Add("rect", () => new SvgRectangle());
            dictionary1.Add("circle", () => new SvgCircle());
            dictionary1.Add("ellipse", () => new SvgEllipse());
            dictionary1.Add("polygon", () => new SvgPolygon());
            dictionary1.Add("polyline", () => new SvgPolyline());
            dictionary1.Add("g", () => new SvgGroup());
            dictionary1.Add("svg", () => new SvgRoot());
            dictionary1.Add("mask", () => new SvgMask());
            dictionary1.Add("clipPath", () => new SvgClipPath());
            dictionary1.Add("defs", () => new SvgDefinitions());
            dictionary1.Add("use", () => new SvgUse());
            dictionary1.Add("stop", () => new SvgGradientStop());
            dictionary1.Add("linearGradient", () => new SvgLinearGradient());
            dictionary1.Add("radialGradient", () => new SvgRadialGradient());
            dictionary1.Add("style", () => new SvgStyleItem());
            dictionary1.Add("text", () => new SvgText());
            dictionary1.Add("tspan", () => new SvgTspan());
            ElementTypes = dictionary1;
            Dictionary<string, Func<double[], SvgTransform>> dictionary2 = new Dictionary<string, Func<double[], SvgTransform>>(StringComparer.InvariantCultureIgnoreCase);
            dictionary2.Add("translate", data => new SvgTranslate(data));
            dictionary2.Add("scale", data => new SvgScale(data));
            dictionary2.Add("rotate", data => new SvgRotate(data));
            dictionary2.Add("skewx", data => new SvgSkewX(data));
            dictionary2.Add("skewy", data => new SvgSkewY(data));
            dictionary2.Add("matrix", data => new SvgMatrix(data));
            TransformTypes = dictionary2;
            DoubleConverter = new System.ComponentModel.DoubleConverter();
            NullableDoubleConverter = new NullableConverter(typeof(double?));
            propertyMutators = new Dictionary<Type, Dictionary<string, Tuple<Action<object, object>, TypeConverter>>>(13);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> dictionary = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(13, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "id",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Id = (string) value, null)
                },
                { 
                    "class",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).StyleName = (string) value, null)
                },
                { 
                    "style",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Style = (SvgStyle) value, SvgStyleConverter.Instance)
                },
                { 
                    "display",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Display = (string) value, null)
                },
                { 
                    "fill",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Fill = (string) value, null)
                },
                { 
                    "stroke",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Stroke = (string) value, null)
                },
                { 
                    "opacity",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Opacity = (double?) value, NullableDoubleConverter)
                },
                { 
                    "fill-opacity",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).FillOpacity = (double?) value, NullableDoubleConverter)
                },
                { 
                    "tag",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).Tag = value, SvgTagConverter.Instance)
                }
            };
            propertyMutators.Add(typeof(SvgElement), dictionary);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgRootMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(0x1a, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "version",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).SetAttribute("Version", (string) value), null)
                },
                { 
                    "xmlns",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).SetAttribute("Xmlns", (string) value), null)
                },
                { 
                    "xmlns:xlink",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).SetAttribute("XmlnsXlink", (string) value), null)
                },
                { 
                    "xml:space",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgElement) x).SetAttribute("XmlSpace", (string) value), null)
                },
                { 
                    "viewBox",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).ViewBox = (SvgViewBox) value, SvgViewBoxTypeConverter.Instance)
                },
                { 
                    "x",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).SetUnit("X", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "y",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).SetUnit("Y", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "width",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).SetUnit("Width", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "height",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).SetUnit("Height", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "enable-background",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRoot) x).Background = (SvgViewBox) value, SvgBackgroundTypeConverter.Instance)
                }
            };
            propertyMutators.Add(typeof(SvgRoot), svgRootMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgStyleElementMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(0x11, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "viewBox",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).ViewBox = (SvgViewBox) value, SvgViewBoxTypeConverter.Instance)
                },
                { 
                    "x",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).SetUnit("X", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "y",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).SetUnit("Y", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "width",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).SetUnit("Width", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "height",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).SetUnit("Height", (SvgUnit) value), SvgUnitConverter.Instance)
                },
                { 
                    "enable-background",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgStyleElement) x).Background = (SvgViewBox) value, SvgBackgroundTypeConverter.Instance)
                }
            };
            propertyMutators.Add(typeof(SvgStyleElement), svgStyleElementMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgGroupMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(13, StringComparer.InvariantCultureIgnoreCase);
            propertyMutators.Add(typeof(SvgGroup), svgGroupMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgPathMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(13, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "d",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgPath) x).PathData = (string) value, null)
                }
            };
            propertyMutators.Add(typeof(SvgPath), svgPathMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgPolygonMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(13, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "points",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgPolygon) x).Points = (string) value, null)
                }
            };
            propertyMutators.Add(typeof(SvgPolygon), svgPolygonMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgRectMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(20, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "x",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("X", (double) value), DoubleConverter)
                },
                { 
                    "y",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("Y", (double) value), DoubleConverter)
                },
                { 
                    "width",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("Width", (double) value), DoubleConverter)
                },
                { 
                    "height",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("Height", (double) value), DoubleConverter)
                },
                { 
                    "rx",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("CornerRadiusX", (double) value), DoubleConverter)
                },
                { 
                    "ry",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgRectangle) x).SetDouble("CornerRadiusY", (double) value), DoubleConverter)
                }
            };
            propertyMutators.Add(typeof(SvgRectangle), svgRectMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgCircleMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(20, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "cx",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgCircle) x).SetDouble("CenterX", (double) value), DoubleConverter)
                },
                { 
                    "cy",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgCircle) x).SetDouble("CenterY", (double) value), DoubleConverter)
                },
                { 
                    "r",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgCircle) x).SetDouble("Radius", (double) value), DoubleConverter)
                }
            };
            propertyMutators.Add(typeof(SvgCircle), svgCircleMutators);
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> svgEllipseMutators = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(20, StringComparer.InvariantCultureIgnoreCase) {
                { 
                    "cx",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgEllipse) x).SetDouble("CenterX", (double) value), DoubleConverter)
                },
                { 
                    "cy",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgEllipse) x).SetDouble("CenterY", (double) value), DoubleConverter)
                },
                { 
                    "rx",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgEllipse) x).SetDouble("RadiusX", (double) value), DoubleConverter)
                },
                { 
                    "ry",
                    new Tuple<Action<object, object>, TypeConverter>((x, value) => ((SvgEllipse) x).SetDouble("RadiusY", (double) value), DoubleConverter)
                }
            };
            propertyMutators.Add(typeof(SvgEllipse), svgEllipseMutators);
            dictionary.ToList<KeyValuePair<string, Tuple<Action<object, object>, TypeConverter>>>().ForEach(delegate (KeyValuePair<string, Tuple<Action<object, object>, TypeConverter>> x) {
                svgGroupMutators.Add(x.Key, x.Value);
                svgCircleMutators.Add(x.Key, x.Value);
                svgEllipseMutators.Add(x.Key, x.Value);
                svgRootMutators.Add(x.Key, x.Value);
                svgStyleElementMutators.Add(x.Key, x.Value);
                svgRectMutators.Add(x.Key, x.Value);
                svgPolygonMutators.Add(x.Key, x.Value);
                svgPathMutators.Add(x.Key, x.Value);
            });
        }

        public static SvgElement CreateElement(XmlReader reader, List<string> unknownTags) => 
            CreateElement(reader.LocalName, reader, unknownTags);

        public static SvgElement CreateElement(string elementName, XmlReader reader, List<string> unknownTags)
        {
            Func<SvgElement> func;
            return (ElementTypes.TryGetValue(elementName, out func) ? SetAttributes(func(), reader, unknownTags) : SetUnknownTag(null, elementName, unknownTags));
        }

        public static SvgTransform CreateTransform(string transformName, double[] data)
        {
            Func<double[], SvgTransform> func;
            return (TransformTypes.TryGetValue(transformName.ToLower(), out func) ? func(data) : null);
        }

        private static Dictionary<string, Tuple<Action<object, object>, TypeConverter>> EnsureMutators(Type elementType)
        {
            object syncRoot = ((ICollection) propertyMutators).SyncRoot;
            lock (syncRoot)
            {
                Dictionary<string, Tuple<Action<object, object>, TypeConverter>> dictionary;
                if (!propertyMutators.TryGetValue(elementType, out dictionary))
                {
                    dictionary = new Dictionary<string, Tuple<Action<object, object>, TypeConverter>>(13, StringComparer.InvariantCultureIgnoreCase);
                    propertyMutators.Add(elementType, dictionary);
                }
                return dictionary;
            }
        }

        private static MemberInfo GetMemberInfo(Type type, string memberName) => 
            type.GetProperty(memberName) ?? type.GetField(memberName);

        private static Tuple<Action<object, object>, TypeConverter> GetPropertyTuple(string attributeName, Type elementType)
        {
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> dictionary = EnsureMutators(elementType);
            object syncRoot = ((ICollection) dictionary).SyncRoot;
            lock (syncRoot)
            {
                Tuple<Action<object, object>, TypeConverter> tuple;
                if (!dictionary.TryGetValue(attributeName, out tuple))
                {
                    SvgPropertyNameAliasAttribute[] attributes = new SvgPropertyNameAliasAttribute[] { new SvgPropertyNameAliasAttribute(attributeName) };
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(elementType, attributes);
                    if (properties.Count > 0)
                    {
                        PropertyDescriptor descriptor = properties[0];
                        Type componentType = descriptor.ComponentType;
                        tuple = !(componentType != elementType) ? new Tuple<Action<object, object>, TypeConverter>(MakeMutator(componentType, descriptor.PropertyType, descriptor.Name), descriptor.Converter) : GetPropertyTuple(descriptor, attributeName, componentType);
                        dictionary.Add(attributeName, tuple);
                    }
                    else
                    {
                        dictionary.Add(attributeName, null);
                        return null;
                    }
                }
                return tuple;
            }
        }

        private static Tuple<Action<object, object>, TypeConverter> GetPropertyTuple(PropertyDescriptor descriptor, string attributeName, Type elementType)
        {
            Dictionary<string, Tuple<Action<object, object>, TypeConverter>> dictionary = EnsureMutators(elementType);
            object syncRoot = ((ICollection) dictionary).SyncRoot;
            lock (syncRoot)
            {
                Tuple<Action<object, object>, TypeConverter> tuple;
                if (!dictionary.TryGetValue(attributeName, out tuple))
                {
                    tuple = new Tuple<Action<object, object>, TypeConverter>(MakeMutator(elementType, descriptor.PropertyType, descriptor.Name), descriptor.Converter);
                    dictionary.Add(attributeName, tuple);
                }
                return tuple;
            }
        }

        private static Action<object, object> MakeMutator(Type type, Type valueType, string memberName)
        {
            MemberInfo memberInfo = GetMemberInfo(type, memberName);
            ParameterExpression expression = Expression.Parameter(typeof(object), "target");
            ParameterExpression expression2 = Expression.Parameter(typeof(object), "value");
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
            return Expression.Lambda<Action<object, object>>(Expression.Assign(Expression.MakeMemberAccess(Expression.Convert(expression, type), memberInfo), Expression.Convert(expression2, valueType)), parameters).Compile();
        }

        private static SvgElement SetAttributes(SvgElement element, XmlReader reader, List<string> unknownTags)
        {
            while (reader.MoveToNextAttribute())
            {
                SetPropertyValue(element, reader.Name, reader.Value, unknownTags);
            }
            return element;
        }

        public static void SetPropertyValue(SvgElement element, string attributeName, string value, List<string> unknownTags)
        {
            Tuple<Action<object, object>, TypeConverter> propertyTuple = GetPropertyTuple(attributeName, element.GetType());
            if (propertyTuple == null)
            {
                SetUnknownTag(element, attributeName, unknownTags);
            }
            else
            {
                try
                {
                    object obj2 = (propertyTuple.Item2 != null) ? propertyTuple.Item2.ConvertFrom(null, CultureInfo.InvariantCulture, value) : value;
                    propertyTuple.Item1(element, obj2);
                }
                catch
                {
                }
            }
        }

        private static SvgElement SetUnknownTag(SvgElement element, string attributeName, List<string> unknownTags)
        {
            if (unknownTags != null)
            {
                unknownTags.Add(attributeName);
            }
            return element;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgElementCreator.<>c <>9 = new SvgElementCreator.<>c();

            internal void <.cctor>b__4_0(object x, object value)
            {
                ((SvgElement) x).Id = (string) value;
            }

            internal void <.cctor>b__4_1(object x, object value)
            {
                ((SvgElement) x).StyleName = (string) value;
            }

            internal void <.cctor>b__4_10(object x, object value)
            {
                ((SvgElement) x).SetAttribute("Xmlns", (string) value);
            }

            internal void <.cctor>b__4_11(object x, object value)
            {
                ((SvgElement) x).SetAttribute("XmlnsXlink", (string) value);
            }

            internal void <.cctor>b__4_12(object x, object value)
            {
                ((SvgElement) x).SetAttribute("XmlSpace", (string) value);
            }

            internal void <.cctor>b__4_13(object x, object value)
            {
                ((SvgRoot) x).ViewBox = (SvgViewBox) value;
            }

            internal void <.cctor>b__4_14(object x, object value)
            {
                ((SvgRoot) x).SetUnit("X", (SvgUnit) value);
            }

            internal void <.cctor>b__4_15(object x, object value)
            {
                ((SvgRoot) x).SetUnit("Y", (SvgUnit) value);
            }

            internal void <.cctor>b__4_16(object x, object value)
            {
                ((SvgRoot) x).SetUnit("Width", (SvgUnit) value);
            }

            internal void <.cctor>b__4_17(object x, object value)
            {
                ((SvgRoot) x).SetUnit("Height", (SvgUnit) value);
            }

            internal void <.cctor>b__4_18(object x, object value)
            {
                ((SvgRoot) x).Background = (SvgViewBox) value;
            }

            internal void <.cctor>b__4_19(object x, object value)
            {
                ((SvgStyleElement) x).ViewBox = (SvgViewBox) value;
            }

            internal void <.cctor>b__4_2(object x, object value)
            {
                ((SvgElement) x).Style = (SvgStyle) value;
            }

            internal void <.cctor>b__4_20(object x, object value)
            {
                ((SvgStyleElement) x).SetUnit("X", (SvgUnit) value);
            }

            internal void <.cctor>b__4_21(object x, object value)
            {
                ((SvgStyleElement) x).SetUnit("Y", (SvgUnit) value);
            }

            internal void <.cctor>b__4_22(object x, object value)
            {
                ((SvgStyleElement) x).SetUnit("Width", (SvgUnit) value);
            }

            internal void <.cctor>b__4_23(object x, object value)
            {
                ((SvgStyleElement) x).SetUnit("Height", (SvgUnit) value);
            }

            internal void <.cctor>b__4_24(object x, object value)
            {
                ((SvgStyleElement) x).Background = (SvgViewBox) value;
            }

            internal void <.cctor>b__4_25(object x, object value)
            {
                ((SvgPath) x).PathData = (string) value;
            }

            internal void <.cctor>b__4_26(object x, object value)
            {
                ((SvgPolygon) x).Points = (string) value;
            }

            internal void <.cctor>b__4_27(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("X", (double) value);
            }

            internal void <.cctor>b__4_28(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("Y", (double) value);
            }

            internal void <.cctor>b__4_29(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("Width", (double) value);
            }

            internal void <.cctor>b__4_3(object x, object value)
            {
                ((SvgElement) x).Display = (string) value;
            }

            internal void <.cctor>b__4_30(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("Height", (double) value);
            }

            internal void <.cctor>b__4_31(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("CornerRadiusX", (double) value);
            }

            internal void <.cctor>b__4_32(object x, object value)
            {
                ((SvgRectangle) x).SetDouble("CornerRadiusY", (double) value);
            }

            internal void <.cctor>b__4_33(object x, object value)
            {
                ((SvgCircle) x).SetDouble("CenterX", (double) value);
            }

            internal void <.cctor>b__4_34(object x, object value)
            {
                ((SvgCircle) x).SetDouble("CenterY", (double) value);
            }

            internal void <.cctor>b__4_35(object x, object value)
            {
                ((SvgCircle) x).SetDouble("Radius", (double) value);
            }

            internal void <.cctor>b__4_36(object x, object value)
            {
                ((SvgEllipse) x).SetDouble("CenterX", (double) value);
            }

            internal void <.cctor>b__4_37(object x, object value)
            {
                ((SvgEllipse) x).SetDouble("CenterY", (double) value);
            }

            internal void <.cctor>b__4_38(object x, object value)
            {
                ((SvgEllipse) x).SetDouble("RadiusX", (double) value);
            }

            internal void <.cctor>b__4_39(object x, object value)
            {
                ((SvgEllipse) x).SetDouble("RadiusY", (double) value);
            }

            internal void <.cctor>b__4_4(object x, object value)
            {
                ((SvgElement) x).Fill = (string) value;
            }

            internal SvgElement <.cctor>b__4_41() => 
                new SvgPath();

            internal SvgElement <.cctor>b__4_42() => 
                new SvgLine();

            internal SvgElement <.cctor>b__4_43() => 
                new SvgRectangle();

            internal SvgElement <.cctor>b__4_44() => 
                new SvgCircle();

            internal SvgElement <.cctor>b__4_45() => 
                new SvgEllipse();

            internal SvgElement <.cctor>b__4_46() => 
                new SvgPolygon();

            internal SvgElement <.cctor>b__4_47() => 
                new SvgPolyline();

            internal SvgElement <.cctor>b__4_48() => 
                new SvgGroup();

            internal SvgElement <.cctor>b__4_49() => 
                new SvgRoot();

            internal void <.cctor>b__4_5(object x, object value)
            {
                ((SvgElement) x).Stroke = (string) value;
            }

            internal SvgElement <.cctor>b__4_50() => 
                new SvgMask();

            internal SvgElement <.cctor>b__4_51() => 
                new SvgClipPath();

            internal SvgElement <.cctor>b__4_52() => 
                new SvgDefinitions();

            internal SvgElement <.cctor>b__4_53() => 
                new SvgUse();

            internal SvgElement <.cctor>b__4_54() => 
                new SvgGradientStop();

            internal SvgElement <.cctor>b__4_55() => 
                new SvgLinearGradient();

            internal SvgElement <.cctor>b__4_56() => 
                new SvgRadialGradient();

            internal SvgElement <.cctor>b__4_57() => 
                new SvgStyleItem();

            internal SvgElement <.cctor>b__4_58() => 
                new SvgText();

            internal SvgElement <.cctor>b__4_59() => 
                new SvgTspan();

            internal void <.cctor>b__4_6(object x, object value)
            {
                ((SvgElement) x).Opacity = (double?) value;
            }

            internal SvgTransform <.cctor>b__4_60(double[] data) => 
                new SvgTranslate(data);

            internal SvgTransform <.cctor>b__4_61(double[] data) => 
                new SvgScale(data);

            internal SvgTransform <.cctor>b__4_62(double[] data) => 
                new SvgRotate(data);

            internal SvgTransform <.cctor>b__4_63(double[] data) => 
                new SvgSkewX(data);

            internal SvgTransform <.cctor>b__4_64(double[] data) => 
                new SvgSkewY(data);

            internal SvgTransform <.cctor>b__4_65(double[] data) => 
                new SvgMatrix(data);

            internal void <.cctor>b__4_7(object x, object value)
            {
                ((SvgElement) x).FillOpacity = (double?) value;
            }

            internal void <.cctor>b__4_8(object x, object value)
            {
                ((SvgElement) x).Tag = value;
            }

            internal void <.cctor>b__4_9(object x, object value)
            {
                ((SvgElement) x).SetAttribute("Version", (string) value);
            }
        }
    }
}

