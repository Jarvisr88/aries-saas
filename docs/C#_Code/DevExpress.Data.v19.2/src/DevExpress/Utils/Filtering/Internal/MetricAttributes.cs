namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal abstract class MetricAttributes : IMetricAttributes, INotifyPropertyChanged, ISummaryMetricAttributes, IDisplayMetricAttributes
    {
        private static readonly Type[] groupConstructorParameterTypes = new Type[] { typeof(GroupUIEditorType), typeof(string[]), typeof(ValueSelectionMode), typeof(bool?), typeof(string), typeof(string), typeof(string[]) };
        private static readonly IDictionary<Type, GroupMetricCtor> groupInitializers = new Dictionary<Type, GroupMetricCtor>();
        private static IDictionary<Type, Func<bool?, string, string, string, BooleanUIEditorType, string[], IMetricAttributes>> booleanChoiceInitializers = new Dictionary<Type, Func<bool?, string, string, string, BooleanUIEditorType, string[], IMetricAttributes>>();
        private static readonly Type[] enumConstructorParameterTypes;
        private static readonly IDictionary<Type, EnumChoiceMetricCtor> enumChoiceInitializers;
        private static readonly Type[] lookupConstructorParameterTypes;
        private static readonly IDictionary<Type, LookupMetricCtor> lookupInitializers;
        private static readonly IDictionary<Type, Func<ILazyMetricAttributesFactory, Type, Type, IMetricAttributes>> lazyInstantiatorsMapping;
        private readonly string[] members;
        private readonly string[] unboundMembers;
        private readonly MemberValueBox[] valueBoxes;
        private readonly DisplayTextsBox displayTexts;
        private readonly MemberNullableValueBox<bool> filterByDisplayText;
        private readonly MemberNullableValueBox<bool> displayBlanks;
        private readonly MemberNullableValueBox<bool> displayRadio;
        private readonly DataItemsLookupBox dataItemsLookup;
        private static IDictionary<Type, Func<object, object, object, string, string, string, RangeUIEditorType, string[], IMetricAttributes>> rangeInitializers;
        private static IDictionary<Type, Func<object, object, string, string, string, DateTimeRangeUIEditorType, string[], IMetricAttributes>> dateTimeRangeInitializers;

        public event PropertyChangedEventHandler PropertyChanged;

        static MetricAttributes()
        {
            Type[] typeArray2 = new Type[9];
            typeArray2[0] = typeof(Type);
            typeArray2[1] = typeof(LookupUIEditorType);
            typeArray2[2] = typeof(bool?);
            typeArray2[3] = typeof(FlagComparisonRule);
            typeArray2[4] = typeof(ValueSelectionMode);
            typeArray2[5] = typeof(bool?);
            typeArray2[6] = typeof(string);
            typeArray2[7] = typeof(string);
            typeArray2[8] = typeof(string[]);
            enumConstructorParameterTypes = typeArray2;
            enumChoiceInitializers = new Dictionary<Type, EnumChoiceMetricCtor>();
            Type[] typeArray3 = new Type[13];
            typeArray3[0] = typeof(object);
            typeArray3[1] = typeof(string);
            typeArray3[2] = typeof(string);
            typeArray3[3] = typeof(int?);
            typeArray3[4] = typeof(int?);
            typeArray3[5] = typeof(LookupUIEditorType);
            typeArray3[6] = typeof(ValueSelectionMode);
            typeArray3[7] = typeof(bool?);
            typeArray3[8] = typeof(string);
            typeArray3[9] = typeof(string);
            typeArray3[10] = typeof(bool?);
            typeArray3[11] = typeof(string);
            typeArray3[12] = typeof(string[]);
            lookupConstructorParameterTypes = typeArray3;
            lookupInitializers = new Dictionary<Type, LookupMetricCtor>();
            Dictionary<Type, Func<ILazyMetricAttributesFactory, Type, Type, IMetricAttributes>> dictionary = new Dictionary<Type, Func<ILazyMetricAttributesFactory, Type, Type, IMetricAttributes>> {
                { 
                    typeof(IRangeMetricAttributes<>),
                    (factory, type, enumDataType) => factory.CreateRange(type)
                },
                { 
                    typeof(ILookupMetricAttributes<>),
                    (factory, type, enumDataType) => factory.CreateLookup(type)
                },
                { 
                    typeof(IChoiceMetricAttributes<>),
                    (factory, type, enumDataType) => factory.CreateBooleanChoice(type)
                },
                { 
                    typeof(IEnumChoiceMetricAttributes<>),
                    (factory, type, enumDataType) => factory.CreateEnumChoice(type, enumDataType)
                }
            };
            lazyInstantiatorsMapping = dictionary;
            rangeInitializers = new Dictionary<Type, Func<object, object, object, string, string, string, RangeUIEditorType, string[], IMetricAttributes>>();
            dateTimeRangeInitializers = new Dictionary<Type, Func<object, object, string, string, string, DateTimeRangeUIEditorType, string[], IMetricAttributes>>();
        }

        protected MetricAttributes(string[] members, int specialMembersCountMembers = 0)
        {
            this.members = members;
            this.unboundMembers = new string[(members.Length + specialMembersCountMembers) + 5];
            this.valueBoxes = new MemberValueBox[(members.Length + specialMembersCountMembers) + 5];
            this.displayTexts = new DisplayTextsBox(-specialMembersCountMembers - 1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes)), (MethodInfo) methodof(MetricAttributes.get_DisplayTexts)), new ParameterExpression[0]));
            this.filterByDisplayText = new MemberNullableValueBox<bool>(0, -specialMembersCountMembers - 2, this, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes)), (MethodInfo) methodof(MetricAttributes.get_FilterByDisplayText)), new ParameterExpression[0]));
            bool? defaultValue = null;
            this.displayBlanks = new MemberNullableValueBox<bool>(defaultValue, -specialMembersCountMembers - 3, this, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes)), (MethodInfo) methodof(MetricAttributes.get_DisplayBlanks)), new ParameterExpression[0]));
            this.dataItemsLookup = new DataItemsLookupBox(-specialMembersCountMembers - 4, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes)), (MethodInfo) methodof(MetricAttributes.get_DataItemsLookup)), new ParameterExpression[0]));
            this.displayRadio = new MemberNullableValueBox<bool>(0, -specialMembersCountMembers - 5, this, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes)), (MethodInfo) methodof(MetricAttributes.get_DisplayRadio)), new ParameterExpression[0]));
        }

        protected virtual bool AllowDisplayLookup(object owner) => 
            true;

        internal static void CheckDataTimeRange(Type type, ref object min, ref object max)
        {
            if ((min != null) && !IsDateTime(min.GetType()))
            {
                min = null;
            }
            if ((max != null) && !IsDateTime(max.GetType()))
            {
                max = null;
            }
            CheckDataTimeRangeCore(type, ref min, ref max);
        }

        internal static void CheckDataTimeRangeCore(Type type, ref object min, ref object max)
        {
            if (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type))
            {
                ref object objRef1 = min;
                if (min == null)
                {
                    ref object local1 = min;
                    objRef1 = IsTimeSpan(type) ? ((object) TimeSpan.MinValue) : ((object) DateTime.MinValue);
                }
                min = objRef1;
                ref object objRef2 = max;
                if (max == null)
                {
                    ref object local2 = max;
                    objRef2 = IsTimeSpan(type) ? ((object) TimeSpan.MaxValue) : ((object) DateTime.MaxValue);
                }
                max = objRef2;
            }
        }

        private static DateTimeRangeUIEditorType CheckDateTimeRangeUIEditorType(RangeUIEditorType value) => 
            (value == RangeUIEditorType.Range) ? DateTimeRangeUIEditorType.Range : ((value != RangeUIEditorType.Spin) ? DateTimeRangeUIEditorType.Default : DateTimeRangeUIEditorType.Picker);

        protected virtual bool CheckLookupDisplayText(object value, ref string text)
        {
            if (IsNullOrDBNull(value))
            {
                text = this.DefaultNullName;
            }
            return true;
        }

        internal static void CheckNumericRange(Type type, ref object min, ref object max)
        {
            if (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type))
            {
                object obj2 = DefaultValuesCache.Get(type);
                ref object objRef1 = min;
                if (min == null)
                {
                    ref object local1 = min;
                    objRef1 = obj2;
                }
                min = objRef1;
                ref object objRef2 = max;
                if (max == null)
                {
                    ref object local2 = max;
                    objRef2 = obj2;
                }
                max = objRef2;
            }
        }

        internal static IMetricAttributes CreateBooleanChoice(Type type, bool? defaultValue, string trueName, string falseName, string defaultName, BooleanUIEditorType editorType, string[] members)
        {
            Func<bool?, string, string, string, BooleanUIEditorType, string[], IMetricAttributes> func;
            if (!IsBooleanChoice(type))
            {
                return null;
            }
            if (!booleanChoiceInitializers.TryGetValue(type, out func))
            {
                ParameterExpression expression = Expression.Parameter(typeof(bool?), "defaultValue");
                ParameterExpression expression2 = Expression.Parameter(typeof(string), "trueName");
                ParameterExpression expression3 = Expression.Parameter(typeof(string), "falseName");
                ParameterExpression expression4 = Expression.Parameter(typeof(string), "defaultName");
                ParameterExpression expression5 = Expression.Parameter(typeof(BooleanUIEditorType), "editorType");
                ParameterExpression expression6 = Expression.Parameter(typeof(string[]), "members");
                Type[] types = new Type[] { typeof(bool?), typeof(string), typeof(string), typeof(string), typeof(BooleanUIEditorType), typeof(string[]) };
                Expression[] arguments = new Expression[] { expression, expression2, expression3, expression4, expression5, expression6 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2, expression3, expression4, expression5, expression6 };
                func = Expression.Lambda<Func<bool?, string, string, string, BooleanUIEditorType, string[], IMetricAttributes>>(Expression.New(typeof(BooleanChoiceMetricAttributes).GetConstructor(types), arguments), parameters).Compile();
                booleanChoiceInitializers.Add(type, func);
            }
            return func(defaultValue, trueName, falseName, defaultName, editorType, members);
        }

        protected virtual KeyValuePair<object, string> CreateDisplayLookupItem(object value, string text) => 
            new KeyValuePair<object, string>(IsNullOrDBNull(value) ? BaseRowsKeeper.NullObject : value, text);

        internal static IMetricAttributes CreateEnumChoice(Type type, Type enumDataType, LookupUIEditorType editorType, bool? useFlags, FlagComparisonRule comparisonRule, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members)
        {
            EnumChoiceMetricCtor ctor;
            if (!enumChoiceInitializers.TryGetValue(type, out ctor))
            {
                Type boxedType = NullableHelpers.GetBoxedType(type);
                Type[] typeArguments = new Type[] { boxedType };
                ParameterExpression expression = Expression.Parameter(typeof(Type), "enumDataType");
                ParameterExpression expression2 = Expression.Parameter(typeof(LookupUIEditorType), "editorType");
                ParameterExpression expression3 = Expression.Parameter(typeof(bool?), "useFlags");
                ParameterExpression expression4 = Expression.Parameter(typeof(FlagComparisonRule), "comparisonRule");
                ParameterExpression expression5 = Expression.Parameter(typeof(ValueSelectionMode), "selectionMode");
                ParameterExpression expression6 = Expression.Parameter(typeof(bool?), "useSelectAll");
                ParameterExpression expression7 = Expression.Parameter(typeof(string), "selectAllName");
                ParameterExpression expression8 = Expression.Parameter(typeof(string), "nullName");
                ParameterExpression expression9 = Expression.Parameter(typeof(string[]), "members");
                Expression[] arguments = new Expression[9];
                arguments[0] = expression;
                arguments[1] = expression2;
                arguments[2] = expression3;
                arguments[3] = expression4;
                arguments[4] = expression5;
                arguments[5] = expression6;
                arguments[6] = expression7;
                arguments[7] = expression8;
                arguments[8] = expression9;
                ParameterExpression[] parameters = new ParameterExpression[9];
                parameters[0] = expression;
                parameters[1] = expression2;
                parameters[2] = expression3;
                parameters[3] = expression4;
                parameters[4] = expression5;
                parameters[5] = expression6;
                parameters[6] = expression7;
                parameters[7] = expression8;
                parameters[8] = expression9;
                ctor = Expression.Lambda<EnumChoiceMetricCtor>(Expression.New(typeof(EnumChoiceMetricAttributes).MakeGenericType(typeArguments).GetConstructor(enumConstructorParameterTypes), arguments), parameters).Compile();
                enumChoiceInitializers.Add(type, ctor);
            }
            return ctor(enumDataType, editorType, useFlags, comparisonRule, selectionMode, useSelectAll, selectAllName, nullName, members);
        }

        internal static IMetricAttributes CreateGroup(Type type, GroupUIEditorType editorType, string[] grouping, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members)
        {
            GroupMetricCtor ctor;
            if (!groupInitializers.TryGetValue(type, out ctor))
            {
                Type[] typeArguments = new Type[] { type };
                ParameterExpression expression = Expression.Parameter(typeof(GroupUIEditorType), "editorType");
                ParameterExpression expression2 = Expression.Parameter(typeof(string[]), "grouping");
                ParameterExpression expression3 = Expression.Parameter(typeof(ValueSelectionMode), "selectionMode");
                ParameterExpression expression4 = Expression.Parameter(typeof(bool?), "useSelectAll");
                ParameterExpression expression5 = Expression.Parameter(typeof(string), "selectAllName");
                ParameterExpression expression6 = Expression.Parameter(typeof(string), "nullName");
                ParameterExpression expression7 = Expression.Parameter(typeof(string[]), "members");
                Expression[] arguments = new Expression[] { expression, expression2, expression3, expression4, expression5, expression6, expression7 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2, expression3, expression4, expression5, expression6, expression7 };
                ctor = Expression.Lambda<GroupMetricCtor>(Expression.New(typeof(GroupMetricAttributes).MakeGenericType(typeArguments).GetConstructor(groupConstructorParameterTypes), arguments), parameters).Compile();
                groupInitializers.Add(type, ctor);
            }
            return ctor(editorType, grouping, selectionMode, useSelectAll, selectAllName, nullName, members);
        }

        internal static IMetricAttributes CreateLazyMetricAttributes(IServiceProvider serviceProvider, string path, Type type, Type enumDataType)
        {
            IMetricAttributesCache cache = serviceProvider.GetService<IMetricAttributesCache>();
            return cache.GetValueOrCache(path, delegate {
                Func<ILazyMetricAttributesFactory, Type, Type, IMetricAttributes> func;
                Type key = GetMetricAttributesTypeDefinition(cache, path, type, enumDataType);
                if (!lazyInstantiatorsMapping.TryGetValue(key, out func))
                {
                    throw new CreateMetricException(path);
                }
                return func(serviceProvider.GetService<ILazyMetricAttributesFactory>(), type, enumDataType);
            });
        }

        internal static IMetricAttributes CreateLookup(Type type, object dataSource, string valueMember, string displayMember, int? top, int? maxCount, LookupUIEditorType editorType, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, bool? useBlanks, string blanksName, string[] members)
        {
            LookupMetricCtor ctor;
            if (!lookupInitializers.TryGetValue(type, out ctor))
            {
                Type[] typeArguments = new Type[] { type };
                ParameterExpression expression = Expression.Parameter(typeof(object), "dataSource");
                ParameterExpression expression2 = Expression.Parameter(typeof(string), "valueMember");
                ParameterExpression expression3 = Expression.Parameter(typeof(string), "displayMember");
                ParameterExpression expression4 = Expression.Parameter(typeof(int?), "top");
                ParameterExpression expression5 = Expression.Parameter(typeof(int?), "maxCount");
                ParameterExpression expression6 = Expression.Parameter(typeof(string[]), "members");
                ParameterExpression expression7 = Expression.Parameter(typeof(LookupUIEditorType), "editorType");
                ParameterExpression expression8 = Expression.Parameter(typeof(ValueSelectionMode), "selectionMode");
                ParameterExpression expression9 = Expression.Parameter(typeof(bool?), "useSelectAll");
                ParameterExpression expression10 = Expression.Parameter(typeof(string), "selectAllName");
                ParameterExpression expression11 = Expression.Parameter(typeof(string), "nullName");
                ParameterExpression expression12 = Expression.Parameter(typeof(bool?), "useBlanks");
                ParameterExpression expression13 = Expression.Parameter(typeof(string), "blanksName");
                Expression[] arguments = new Expression[13];
                arguments[0] = expression;
                arguments[1] = expression2;
                arguments[2] = expression3;
                arguments[3] = expression4;
                arguments[4] = expression5;
                arguments[5] = expression7;
                arguments[6] = expression8;
                arguments[7] = expression9;
                arguments[8] = expression10;
                arguments[9] = expression11;
                arguments[10] = expression12;
                arguments[11] = expression13;
                arguments[12] = expression6;
                ParameterExpression[] parameters = new ParameterExpression[13];
                parameters[0] = expression;
                parameters[1] = expression2;
                parameters[2] = expression3;
                parameters[3] = expression4;
                parameters[4] = expression5;
                parameters[5] = expression7;
                parameters[6] = expression8;
                parameters[7] = expression9;
                parameters[8] = expression10;
                parameters[9] = expression11;
                parameters[10] = expression12;
                parameters[11] = expression13;
                parameters[12] = expression6;
                ctor = Expression.Lambda<LookupMetricCtor>(Expression.New(typeof(LookupMetricAttributes).MakeGenericType(typeArguments).GetConstructor(lookupConstructorParameterTypes), arguments), parameters).Compile();
                lookupInitializers.Add(type, ctor);
            }
            return ctor(dataSource, valueMember, displayMember, top, maxCount, editorType, selectionMode, useSelectAll, selectAllName, nullName, useBlanks, blanksName, members);
        }

        internal static IMetricAttributes CreateRange(Type type, object min, object max, string fromName, string toName, string nullName, DateTimeRangeUIEditorType editorType, string[] members)
        {
            Func<object, object, string, string, string, DateTimeRangeUIEditorType, string[], IMetricAttributes> func;
            if (!IsRange(type) || !IsDateTimeRange(type))
            {
                return null;
            }
            if (!dateTimeRangeInitializers.TryGetValue(type, out func))
            {
                Type type2;
                Type nullable = DevExpress.Utils.Filtering.Internal.TypeHelper.GetNullable(type, out type2);
                Type[] typeArguments = new Type[] { type2 };
                ParameterExpression p = Expression.Parameter(typeof(object), "min");
                ParameterExpression expression2 = Expression.Parameter(typeof(object), "max");
                ParameterExpression expression3 = Expression.Parameter(typeof(string), "fromName");
                ParameterExpression expression4 = Expression.Parameter(typeof(string), "toName");
                ParameterExpression expression5 = Expression.Parameter(typeof(string), "nullName");
                ParameterExpression expression6 = Expression.Parameter(typeof(DateTimeRangeUIEditorType), "editorType");
                ParameterExpression expression7 = Expression.Parameter(typeof(string[]), "members");
                Type[] types = new Type[] { nullable, nullable, typeof(string), typeof(string), typeof(string), typeof(DateTimeRangeUIEditorType), typeof(string[]) };
                Expression[] arguments = new Expression[] { Converter.GetConvertNullableExpression(type2, p), Converter.GetConvertNullableExpression(type2, expression2), expression3, expression4, expression5, expression6, expression7 };
                ParameterExpression[] parameters = new ParameterExpression[] { p, expression2, expression3, expression4, expression5, expression6, expression7 };
                func = Expression.Lambda<Func<object, object, string, string, string, DateTimeRangeUIEditorType, string[], IMetricAttributes>>(Expression.New(typeof(RangeMetricAttributes).MakeGenericType(typeArguments).GetConstructor(types), arguments), parameters).Compile();
                dateTimeRangeInitializers.Add(type, func);
            }
            return func(min, max, fromName, toName, nullName, editorType, members);
        }

        internal static IMetricAttributes CreateRange(Type type, object min, object max, object avg, string fromName, string toName, string nullName, RangeUIEditorType editorType, string[] members)
        {
            Func<object, object, object, string, string, string, RangeUIEditorType, string[], IMetricAttributes> func;
            if (!IsRange(type))
            {
                return null;
            }
            if (IsDateTimeRange(type))
            {
                CheckDataTimeRange(type, ref min, ref max);
                return CreateRange(type, min, max, fromName, toName, nullName, CheckDateTimeRangeUIEditorType(editorType), members);
            }
            if (!rangeInitializers.TryGetValue(type, out func))
            {
                Type type2;
                Type nullable = DevExpress.Utils.Filtering.Internal.TypeHelper.GetNullable(type, out type2);
                Type[] typeArguments = new Type[] { type2 };
                ParameterExpression p = Expression.Parameter(typeof(object), "min");
                ParameterExpression expression2 = Expression.Parameter(typeof(object), "max");
                ParameterExpression expression3 = Expression.Parameter(typeof(object), "avg");
                ParameterExpression expression4 = Expression.Parameter(typeof(string), "fromName");
                ParameterExpression expression5 = Expression.Parameter(typeof(string), "toName");
                ParameterExpression expression6 = Expression.Parameter(typeof(string), "nullName");
                ParameterExpression expression7 = Expression.Parameter(typeof(RangeUIEditorType), "editorType");
                ParameterExpression expression8 = Expression.Parameter(typeof(string[]), "members");
                Type[] types = new Type[] { nullable, nullable, nullable, typeof(string), typeof(string), typeof(string), typeof(RangeUIEditorType), typeof(string[]) };
                Expression[] arguments = new Expression[] { Converter.GetConvertNullableExpression(type2, p), Converter.GetConvertNullableExpression(type2, expression2), Converter.GetConvertNullableExpression(type2, expression3), expression4, expression5, expression6, expression7, expression8 };
                ParameterExpression[] parameters = new ParameterExpression[] { p, expression2, expression3, expression4, expression5, expression6, expression7, expression8 };
                func = Expression.Lambda<Func<object, object, object, string, string, string, RangeUIEditorType, string[], IMetricAttributes>>(Expression.New(typeof(RangeMetricAttributes).MakeGenericType(typeArguments).GetConstructor(types), arguments), parameters).Compile();
                rangeInitializers.Add(type, func);
            }
            return func(min, max, avg, fromName, toName, nullName, editorType, members);
        }

        bool IDisplayMetricAttributes.TryGetDisplayIndex(string displayText, out int valueIndex)
        {
            valueIndex = (this.DisplayTexts != null) ? Array.IndexOf<string>(this.DisplayTexts, displayText) : -1;
            return (valueIndex != -1);
        }

        bool IDisplayMetricAttributes.TryGetDisplayLookup(object owner, object uniqueValues, bool skipNulls, out object lookup)
        {
            lookup = uniqueValues;
            if (!this.AllowDisplayLookup(owner) || this.FilterByDisplayText.GetValueOrDefault())
            {
                return false;
            }
            object[] objArray = uniqueValues as object[];
            string[] displayTexts = this.DisplayTexts;
            bool flag = ((displayTexts != null) && (objArray != null)) && (displayTexts.Length == objArray.Length);
            if (flag)
            {
                List<KeyValuePair<object, string>> list = new List<KeyValuePair<object, string>>(objArray.Length);
                int index = 0;
                while (true)
                {
                    if (index >= objArray.Length)
                    {
                        lookup = list;
                        break;
                    }
                    if (!skipNulls || !this.IsNullDisplayLookupItem(objArray[index]))
                    {
                        string text = displayTexts[index];
                        if (this.CheckLookupDisplayText(objArray[index], ref text))
                        {
                            list.Add(this.CreateDisplayLookupItem(objArray[index], text));
                        }
                    }
                    index++;
                }
            }
            return flag;
        }

        bool IDisplayMetricAttributes.TryGetDisplayText(int valueIndex, out string text)
        {
            string[] displayTexts = this.DisplayTexts;
            bool flag = (displayTexts != null) && ((valueIndex >= 0) && (valueIndex < displayTexts.Length));
            text = flag ? displayTexts[valueIndex] : null;
            return flag;
        }

        void IMetricAttributes.UpdateMemberBinding(string unboundMemberName, object value)
        {
            if (!string.IsNullOrEmpty(unboundMemberName))
            {
                for (int i = 0; i < this.unboundMembers.Length; i++)
                {
                    if ((this.unboundMembers[i] == unboundMemberName) && this.valueBoxes[i].Update(value))
                    {
                        this.RaisePropertyChanged("#MemberBindings");
                        return;
                    }
                }
            }
        }

        void IMetricAttributes.UpdateMemberBindings(MetricAttributesData data, IMetricAttributesQuery query)
        {
            bool flag = false;
            IDictionary<string, object> dictionary = query.InitializeValues(data);
            for (int i = 0; i < this.unboundMembers.Length; i++)
            {
                object obj2;
                if (!string.IsNullOrEmpty(this.unboundMembers[i]) && dictionary.TryGetValue(this.unboundMembers[i], out obj2))
                {
                    flag |= this.valueBoxes[i].Update(obj2);
                }
            }
            if (flag)
            {
                this.RaisePropertyChanged("#MemberBindings");
            }
        }

        void IMetricAttributes.UpdateMemberBindings(object viewModel, string propertyName, IMetricAttributesQuery queryProvider)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(propertyName))
            {
                object obj4 = MemberReader.Read(viewModel, propertyName);
                for (int i = 0; i < this.members.Length; i++)
                {
                    if (this.members[i] == propertyName)
                    {
                        flag |= this.valueBoxes[i].Update(obj4);
                    }
                }
            }
            else
            {
                Dictionary<string, object> valuesHash = new Dictionary<string, object>(StringComparer.Ordinal);
                Dictionary<string, object> queryMemberHash = new Dictionary<string, object>(StringComparer.Ordinal);
                int index = 0;
                while (true)
                {
                    if (index >= this.members.Length)
                    {
                        this.EnsureQueryProvider(queryProvider).Do<IMetricAttributesQuery>(q => q.QueryValues(queryMemberHash));
                        for (int i = 0; i < this.unboundMembers.Length; i++)
                        {
                            object obj3;
                            if (!string.IsNullOrEmpty(this.unboundMembers[i]) && queryMemberHash.TryGetValue(this.unboundMembers[i], out obj3))
                            {
                                flag |= this.valueBoxes[i].Update(obj3);
                            }
                        }
                        break;
                    }
                    if (!string.IsNullOrEmpty(this.members[index]))
                    {
                        object obj2 = MemberReader.Read(viewModel, this.members[index], valuesHash);
                        MemberValueBox box = this.valueBoxes[index];
                        flag |= box.Update(obj2);
                        queryMemberHash[box.propertyName] = obj2;
                    }
                    index++;
                }
            }
            if (flag)
            {
                this.RaisePropertyChanged("#MemberBindings");
            }
        }

        bool ISummaryMetricAttributes.TryGetDataController(out object controller) => 
            this.TryGetValue("DataController", out controller);

        bool ISummaryMetricAttributes.TryGetSummaryValue(string member, out object value) => 
            this.TryGetValue(member, out value);

        protected virtual IMetricAttributesQuery EnsureQueryProvider(IMetricAttributesQuery queryProvider) => 
            queryProvider;

        internal static bool ForceFilterByText(Type type, IDisplayMetricAttributes displayMetricAttributes) => 
            ((type == typeof(Guid)) || (type == typeof(Guid?))) ? displayMetricAttributes.FilterByDisplayText : (((type == typeof(IntPtr)) || (type == typeof(IntPtr?))) ? displayMetricAttributes.FilterByDisplayText : (!(type == typeof(string)) ? (!(type == typeof(char)) ? (!IsLookup(type) && displayMetricAttributes.FilterByDisplayText) : displayMetricAttributes.FilterByDisplayText) : displayMetricAttributes.FilterByDisplayText));

        private static bool GetIsKey(IMetadataStorage metadataStorage, string path)
        {
            AnnotationAttributes attributes;
            return (metadataStorage.TryGetValue(path, out attributes) && attributes.IsKey);
        }

        internal static Type GetMetricAttributesTypeDefinition(IMetricAttributesCache cache, string path, Type type, Type enumDataType) => 
            cache.GetValueOrCache(path, delegate {
                Type type = null;
                if (IsRange(type))
                {
                    type = typeof(IRangeMetricAttributes<>);
                }
                if (IsLookup(type, path, cache as IMetadataStorage))
                {
                    type = typeof(ILookupMetricAttributes<>);
                }
                if (IsBooleanChoice(type))
                {
                    type = typeof(IChoiceMetricAttributes<>);
                }
                if (IsEnumChoice(type) || IsEnumChoice(enumDataType))
                {
                    type = typeof(IEnumChoiceMetricAttributes<>);
                }
                if (type == null)
                {
                    throw new ResolveMetricTypeDefinitionException(path, type);
                }
                return type;
            });

        private static bool GetUseFlags(bool? useFlags, Type enumType) => 
            (useFlags != null) ? (useFlags.Value && EnumHelper.IsFlags(enumType)) : EnumHelper.IsFlags(enumType);

        protected static bool IsBlank(object value) => 
            IsNullOrDBNull(value) || (value == string.Empty);

        internal static bool IsBooleanChoice(Type type)
        {
            Type type2;
            return (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type, out type2) ? (type == typeof(bool)) : IsBooleanChoice(type2));
        }

        private static bool IsDateTime(Type type) => 
            (type == typeof(DateTime)) || ((type == typeof(DateTime?)) || IsTimeSpan(type));

        internal static bool IsDateTimeRange(Type type) => 
            IsDateTime(type) || IsTimeSpan(type);

        internal static bool IsEnumChoice(Type type)
        {
            Type type2;
            return (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type, out type2) ? type.IsEnum : IsEnumChoice(type2));
        }

        private static bool IsIdProperty(string path) => 
            path.ToLowerInvariant().EndsWith("id");

        private static bool IsKeyMember(IMetadataStorage metadataStorage, string path) => 
            metadataStorage.Get<IMetadataStorage, bool>(storage => GetIsKey(storage, path), false);

        internal static bool IsLookup(Type type)
        {
            Type type2;
            return ((type == typeof(string)) || ((type == typeof(char)) || ((type == typeof(object)) || (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type, out type2) ? ((type == typeof(Guid)) || (type == typeof(IntPtr))) : IsLookup(type2)))));
        }

        internal static bool IsLookup(IEndUserFilteringMetric metric, IMetadataStorage storage) => 
            IsLookup(metric.Type, metric.Path, storage);

        internal static bool IsLookup(Type type, string path, IMetadataStorage storage) => 
            IsLookup(type) || (IsLookupMember(storage, type, path) || DevExpress.Utils.Filtering.Internal.TypeHelper.IsExpandableType(type));

        internal static bool IsLookupMember(IMetadataStorage metadataStorage, Type type, string path)
        {
            Type type2;
            return (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type, out type2) ? (IsKeyMember(metadataStorage, path) || (IsIdProperty(path) && IsTypicalLookupMemberType(type))) : IsLookupMember(metadataStorage, type2, path));
        }

        protected virtual bool IsNullDisplayLookupItem(object value) => 
            (value == null) || (value == DBNull.Value);

        protected static bool IsNullOrDBNull(object value) => 
            (value == null) || (value == DBNull.Value);

        internal static bool IsRange(Type type)
        {
            TypeCode typeCode = DXTypeExtensions.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Boolean:
                case TypeCode.Char:
                    return false;

                case TypeCode.Object:
                    Type type2;
                    return (!DevExpress.Utils.Filtering.Internal.TypeHelper.IsNullable(type, out type2) ? IsTimeSpan(type) : IsRange(type2));
            }
            return (typeCode != TypeCode.String);
        }

        internal static bool IsTimeSpan(Type type) => 
            (type == typeof(TimeSpan)) || (type == typeof(TimeSpan?));

        private static bool IsTypicalLookupMemberType(Type type) => 
            (type == typeof(int)) || ((type == typeof(uint)) || ((type == typeof(long)) || ((type == typeof(ulong)) || ((type == typeof(Guid)) || (type == typeof(IntPtr))))));

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected internal virtual bool ShouldUseBlanksFilterForNullValue(object value) => 
            (value == null) && (this.DefaultNullName == FilteringLocalizer.GetString(FilteringLocalizerStringId.BlanksName));

        internal bool TryGetValue(string member, out object value)
        {
            for (int i = 0; i < this.valueBoxes.Length; i++)
            {
                if (this.valueBoxes[i].TryGetValue(member, out value))
                {
                    return true;
                }
            }
            value = null;
            return false;
        }

        protected virtual string DefaultNullName =>
            FilteringLocalizer.GetString(FilteringLocalizerStringId.NullName);

        protected virtual string[] DisplayTexts =>
            this.displayTexts.Value;

        protected virtual bool? FilterByDisplayText =>
            this.filterByDisplayText.Value;

        protected virtual bool? DisplayBlanks =>
            this.displayBlanks.Value;

        protected virtual bool? DisplayRadio =>
            this.displayRadio.Value;

        protected virtual object DataItemsLookup =>
            this.dataItemsLookup.Value;

        bool IDisplayMetricAttributes.FilterByDisplayText =>
            this.FilterByDisplayText.GetValueOrDefault();

        object IDisplayMetricAttributes.DataItemsLookup =>
            this.dataItemsLookup.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MetricAttributes.<>c <>9 = new MetricAttributes.<>c();

            internal IMetricAttributes <.cctor>b__110_0(ILazyMetricAttributesFactory factory, Type type, Type enumDataType) => 
                factory.CreateRange(type);

            internal IMetricAttributes <.cctor>b__110_1(ILazyMetricAttributesFactory factory, Type type, Type enumDataType) => 
                factory.CreateLookup(type);

            internal IMetricAttributes <.cctor>b__110_2(ILazyMetricAttributesFactory factory, Type type, Type enumDataType) => 
                factory.CreateBooleanChoice(type);

            internal IMetricAttributes <.cctor>b__110_3(ILazyMetricAttributesFactory factory, Type type, Type enumDataType) => 
                factory.CreateEnumChoice(type, enumDataType);
        }

        private abstract class BaseLookupMetricAttributes : MetricAttributes
        {
            private readonly LookupUIEditorType editorType;
            private readonly string selectAllName;
            private readonly string nullName;
            private readonly bool useRadioSelection;
            private readonly bool? useSelectAll;

            protected BaseLookupMetricAttributes(bool? useSelectAll, ValueSelectionMode selectionMode, string selectAllName, string nullName, LookupUIEditorType editorType, string[] members, int specialMembersCountMembers = 0) : base(members, specialMembersCountMembers)
            {
                this.selectAllName = selectAllName;
                this.nullName = nullName;
                this.useRadioSelection = selectionMode == ValueSelectionMode.Single;
                this.useSelectAll = useSelectAll;
                this.editorType = editorType;
            }

            protected abstract bool GetDefaultSelectAll();

            public LookupUIEditorType EditorType =>
                this.editorType;

            public string SelectAllName =>
                this.selectAllName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.SelectAllName);

            public string NullName =>
                this.nullName ?? this.DefaultNullName;

            public bool UseRadioSelection =>
                this.useRadioSelection ? true : this.DisplayRadio.GetValueOrDefault();

            public bool UseSelectAll =>
                this.UseRadioSelection ? false : this.useSelectAll.GetValueOrDefault(this.GetDefaultSelectAll());
        }

        private class BooleanChoiceMetricAttributes : MetricAttributes, IBooleanChoiceMetricAttributes, IChoiceMetricAttributes<bool>, IMetricAttributes<bool>, IMetricAttributes, IUniqueValuesMetricAttributes
        {
            private readonly BooleanUIEditorType editorType;
            private readonly string trueName;
            private readonly string falseName;
            private readonly string defaultName;
            private readonly MetricAttributes.UniqueNullableValuesBox<bool> unique;
            private readonly MetricAttributes.MemberNullableValueBox<bool> defaultValue;

            public BooleanChoiceMetricAttributes(bool? defaultValue, string trueName, string falseName, string defaultName, BooleanUIEditorType editorType, string[] members) : base(members, 1)
            {
                this.unique = new MetricAttributes.UniqueNullableValuesBox<bool>(-1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.BooleanChoiceMetricAttributes)), (MethodInfo) methodof(MetricAttributes.BooleanChoiceMetricAttributes.get_UniqueValues)), new ParameterExpression[0]));
                this.defaultValue = new MetricAttributes.MemberNullableValueBox<bool>(defaultValue, 0, this, Expression.Lambda<Func<bool?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.BooleanChoiceMetricAttributes)), (MethodInfo) methodof(MetricAttributes.BooleanChoiceMetricAttributes.get_DefaultValue)), new ParameterExpression[0]));
                this.trueName = trueName;
                this.falseName = falseName;
                this.defaultName = defaultName;
                this.editorType = editorType;
            }

            public BooleanUIEditorType EditorType =>
                this.editorType;

            public object UniqueValues =>
                this.unique.Values;

            public bool HasUniqueValues =>
                this.unique.HasValues;

            public bool? DefaultValue =>
                this.defaultValue.Value;

            public string TrueName =>
                this.trueName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.TrueName);

            public string FalseName =>
                this.falseName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.FalseName);

            public string DefaultName =>
                this.defaultName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.DefaultName);
        }

        internal static class Converter
        {
            private static MethodInfo selectInfo = (typeof(Enumerable).GetMember("Select", BindingFlags.Public | BindingFlags.Static).First<MemberInfo>() as MethodInfo);
            private static MethodInfo convertAllInfo = (typeof(Array).GetMember("ConvertAll", BindingFlags.Public | BindingFlags.Static).First<MemberInfo>() as MethodInfo);

            internal static T ChangeType<T>(object value)
            {
                if (!ReferenceEquals(string.Empty, value) || !typeof(T).IsValueType)
                {
                    TimeSpan span;
                    if (!(value is string) || !(typeof(TimeSpan) == typeof(T)))
                    {
                        return (T) System.Convert.ChangeType(value, typeof(T));
                    }
                    if (TimeSpan.TryParse((string) value, out span))
                    {
                        return (T) span;
                    }
                }
                return default(T);
            }

            internal static T Convert<T>(object value)
            {
                if (value != null)
                {
                    return (!typeof(T).IsAssignableFrom(value.GetType()) ? ChangeType<T>(value) : ((T) value));
                }
                return default(T);
            }

            internal static T? ConvertNullable<T>(object value) where T: struct
            {
                if (value != null)
                {
                    return new T?(Convert<T>(value));
                }
                return null;
            }

            internal static T[] ConvertToArray<T>(object value)
            {
                T[] localArray = value as T[];
                if (localArray != null)
                {
                    return localArray;
                }
                object[] array = value as object[];
                if (array != null)
                {
                    Converter<object, T> converter = <>c__6<T>.<>9__6_0;
                    if (<>c__6<T>.<>9__6_0 == null)
                    {
                        Converter<object, T> local1 = <>c__6<T>.<>9__6_0;
                        converter = <>c__6<T>.<>9__6_0 = e => Convert<T>(e);
                    }
                    return Array.ConvertAll<object, T>(array, converter);
                }
                IEnumerable<T> source = value as IEnumerable<T>;
                if (source != null)
                {
                    return source.ToArray<T>();
                }
                IEnumerable enumerable = value as IEnumerable;
                return ((enumerable == null) ? new T[0] : Iterate<T>(enumerable).ToArray<T>());
            }

            internal static IEnumerable<T> ConvertToIEnumerable<T>(object value)
            {
                IEnumerable<T> enumerable = value as IEnumerable<T>;
                if (enumerable != null)
                {
                    return enumerable;
                }
                IEnumerable enumerable2 = value as IEnumerable;
                return ((enumerable2 == null) ? ((IEnumerable<T>) ConvertToArray<T>(value)) : Iterate<T>(enumerable2));
            }

            internal static IEnumerable<T> ConvertToIEnumerable<T>(object value, string member)
            {
                if (string.IsNullOrEmpty(member))
                {
                    return ConvertToIEnumerable<T>(value);
                }
                IEnumerable enumerable = value as IEnumerable;
                return ((enumerable == null) ? Enumerable.Empty<T>() : Iterate<T>(enumerable, member));
            }

            private static LambdaExpression GetConvertExpression(Type type)
            {
                ParameterExpression expression = Expression.Parameter(typeof(object), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return Expression.Lambda(Expression.Call(GetConvertMethod(type), expression), parameters);
            }

            internal static MethodCallExpression GetConvertExpression(Type type, ParameterExpression p) => 
                Expression.Call(GetConvertMethod(type), p);

            private static MethodInfo GetConvertMethod(Type type)
            {
                Type[] typeArguments = new Type[] { type };
                return typeof(MetricAttributes.Converter).GetMethod("Convert", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeArguments);
            }

            internal static MethodCallExpression GetConvertNullableExpression(Type type, ParameterExpression p) => 
                Expression.Call(GetConvertNullableMethod(type), p);

            private static MethodInfo GetConvertNullableMethod(Type type)
            {
                Type[] typeArguments = new Type[] { type };
                return typeof(MetricAttributes.Converter).GetMethod("ConvertNullable", BindingFlags.NonPublic | BindingFlags.Static).MakeGenericMethod(typeArguments);
            }

            internal static MethodCallExpression GetConvertToArrayExpression(Type type, ParameterExpression pArray)
            {
                Type[] typeArguments = new Type[] { typeof(object), type };
                Type[] typeArray2 = new Type[] { typeof(object), type };
                Type delegateType = typeof(Converter<,>).MakeGenericType(typeArray2);
                Delegate delegate2 = GetConvertMethod(type).CreateDelegate(delegateType);
                return Expression.Call(convertAllInfo.MakeGenericMethod(typeArguments), pArray, Expression.Constant(delegate2));
            }

            internal static MethodCallExpression GetConvertToIEnumerableExpression(Type type, ParameterExpression pValues)
            {
                LambdaExpression convertExpression = GetConvertExpression(type);
                Type[] typeArguments = new Type[] { typeof(object), type };
                return Expression.Call(selectInfo.MakeGenericMethod(typeArguments), pValues, convertExpression);
            }

            [IteratorStateMachine(typeof(<Iterate>d__5<>))]
            internal static IEnumerable<T> Iterate<T>(IEnumerable enumerable)
            {
                <Iterate>d__5<T> d__1 = new <Iterate>d__5<T>(-2);
                d__1.<>3__enumerable = enumerable;
                return d__1;
            }

            [IteratorStateMachine(typeof(<Iterate>d__4<>))]
            internal static IEnumerable<T> Iterate<T>(IEnumerable enumerable, string member)
            {
                <Iterate>d__4<T> d__1 = new <Iterate>d__4<T>(-2);
                d__1.<>3__enumerable = enumerable;
                d__1.<>3__member = member;
                return d__1;
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__6<T>
            {
                public static readonly MetricAttributes.Converter.<>c__6<T> <>9;
                public static Converter<object, T> <>9__6_0;

                static <>c__6()
                {
                    MetricAttributes.Converter.<>c__6<T>.<>9 = new MetricAttributes.Converter.<>c__6<T>();
                }

                internal T <ConvertToArray>b__6_0(object e) => 
                    MetricAttributes.Converter.Convert<T>(e);
            }

            [CompilerGenerated]
            private sealed class <Iterate>d__4<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                private int <>l__initialThreadId;
                private IEnumerable enumerable;
                public IEnumerable <>3__enumerable;
                private string member;
                public string <>3__member;
                private IEnumerator <>7__wrap1;

                [DebuggerHidden]
                public <Iterate>d__4(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    IDisposable disposable = this.<>7__wrap1 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.enumerable.GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
                        }
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            object current = this.<>7__wrap1.Current;
                            this.<>2__current = MetricAttributes.Converter.Convert<T>(current.GetMemberValue(this.member, null));
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<T> IEnumerable<T>.GetEnumerator()
                {
                    MetricAttributes.Converter.<Iterate>d__4<T> d__;
                    if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                    {
                        d__ = new MetricAttributes.Converter.<Iterate>d__4<T>(0);
                    }
                    else
                    {
                        this.<>1__state = 0;
                        d__ = (MetricAttributes.Converter.<Iterate>d__4<T>) this;
                    }
                    d__.enumerable = this.<>3__enumerable;
                    d__.member = this.<>3__member;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private sealed class <Iterate>d__5<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private T <>2__current;
                private int <>l__initialThreadId;
                private IEnumerable enumerable;
                public IEnumerable <>3__enumerable;
                private IEnumerator <>7__wrap1;

                [DebuggerHidden]
                public <Iterate>d__5(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    IDisposable disposable = this.<>7__wrap1 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.enumerable.GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
                        }
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            object current = this.<>7__wrap1.Current;
                            this.<>2__current = MetricAttributes.Converter.Convert<T>(current);
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<T> IEnumerable<T>.GetEnumerator()
                {
                    MetricAttributes.Converter.<Iterate>d__5<T> d__;
                    if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                    {
                        d__ = new MetricAttributes.Converter.<Iterate>d__5<T>(0);
                    }
                    else
                    {
                        this.<>1__state = 0;
                        d__ = (MetricAttributes.Converter.<Iterate>d__5<T>) this;
                    }
                    d__.enumerable = this.<>3__enumerable;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                T IEnumerator<T>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private sealed class CreateMetricException : MetricAttributes.MetricException
        {
            public CreateMetricException(string path) : base(path, "Unable to create Metric", new object[0])
            {
            }
        }

        private sealed class DataControllerBox : MetricAttributes.MemberValueBox
        {
            internal DataControllerBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal DataController Value =>
                base.HasMemberValue ? (base.GetValue() as DataController) : null;
        }

        private sealed class DataItemsLookupBox : MetricAttributes.MemberValueBox
        {
            internal DataItemsLookupBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal object Value =>
                base.HasMemberValue ? base.GetValue() : null;
        }

        private sealed class DisplayTextsBox : MetricAttributes.MemberValueBox
        {
            internal DisplayTextsBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal string[] Value =>
                base.HasMemberValue ? (base.GetValue() as string[]) : null;
        }

        private sealed class EnumChoiceMetricAttributes<T> : MetricAttributes.BaseLookupMetricAttributes, IEnumChoiceMetricAttributes<T>, IChoiceMetricAttributes<T>, IMetricAttributes<T>, IMetricAttributes, IEnumChoiceMetricAttributes, IBaseLookupMetricAttributes, ICollectionMetricAttributes, IUniqueValuesMetricAttributes where T: struct
        {
            private readonly MetricAttributes.UniqueNullableEnumValuesBox<T> unique;

            public EnumChoiceMetricAttributes(Type enumDataType, LookupUIEditorType editorType, bool? useFlags, FlagComparisonRule comparisonRule, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members) : base(useSelectAll, selectionMode, selectAllName, nullName, editorType, members, 1)
            {
                this.unique = new MetricAttributes.UniqueNullableEnumValuesBox<T>(-1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.EnumChoiceMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.EnumChoiceMetricAttributes<T>.get_UniqueValues, MetricAttributes.EnumChoiceMetricAttributes<T>)), new ParameterExpression[0]), enumDataType);
                this.UseFlags = GetUseFlags(useFlags, this.EnumType);
                this.UseContainsForFlags = this.UseFlags && (comparisonRule != FlagComparisonRule.Equals);
            }

            protected sealed override bool GetDefaultSelectAll() => 
                false;

            public object UniqueValues =>
                this.unique.Values;

            public bool HasUniqueValues =>
                this.unique.HasValues;

            public Type EnumType =>
                this.unique.EnumType;

            public bool UseFlags { get; private set; }

            public bool UseContainsForFlags { get; private set; }
        }

        private delegate IMetricAttributes EnumChoiceMetricCtor(Type enumDataType, LookupUIEditorType editorType, bool? useFlags, FlagComparisonRule comparisonRule, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members);

        private sealed class GroupMetricAttributes<T> : MetricAttributes, IGroupMetricAttributes<T>, IMetricAttributes<T>, IMetricAttributes, IGroupMetricAttributes, ICollectionMetricAttributes, IUniqueValuesMetricAttributes
        {
            private readonly GroupUIEditorType editorType;
            private readonly MetricAttributes.GroupValuesBox groupValues;
            private readonly MetricAttributes.GroupTextsBox groupTexts;
            private readonly string[] grouping;
            private readonly string selectAllName;
            private readonly string nullName;
            private static readonly bool isDateOrTime;
            private static readonly bool isString;
            private readonly bool useRadioSelection;
            private readonly bool? useSelectAll;
            private static readonly bool allowNull;

            static GroupMetricAttributes()
            {
                MetricAttributes.GroupMetricAttributes<T>.isDateOrTime = IsDateTime(typeof(T));
                MetricAttributes.GroupMetricAttributes<T>.isString = typeof(T) == typeof(string);
                MetricAttributes.GroupMetricAttributes<T>.allowNull = DevExpress.Utils.Filtering.Internal.TypeHelper.AllowNull(typeof(T));
            }

            public GroupMetricAttributes(GroupUIEditorType editorType, string[] grouping, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members) : base(members, 2)
            {
                this.editorType = editorType;
                this.groupValues = new MetricAttributes.GroupValuesBox(-1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.GroupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.GroupMetricAttributes<T>.get_GroupValues, MetricAttributes.GroupMetricAttributes<T>)), new ParameterExpression[0]));
                this.groupTexts = new MetricAttributes.GroupTextsBox(-2, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.GroupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.GroupMetricAttributes<T>.get_GroupTexts, MetricAttributes.GroupMetricAttributes<T>)), new ParameterExpression[0]));
                string[] textArray1 = grouping;
                if (grouping == null)
                {
                    string[] local1 = grouping;
                    textArray1 = new string[0];
                }
                this.grouping = textArray1;
                this.selectAllName = selectAllName;
                this.nullName = nullName;
                this.useRadioSelection = selectionMode == ValueSelectionMode.Single;
                this.useSelectAll = useSelectAll;
            }

            protected sealed override bool CheckLookupDisplayText(object value, ref string text)
            {
                if (value != BaseRowsKeeper.NullObject)
                {
                    return true;
                }
                text = this.NullName;
                return MetricAttributes.GroupMetricAttributes<T>.allowNull;
            }

            protected override IMetricAttributesQuery EnsureQueryProvider(IMetricAttributesQuery queryProvider)
            {
                IGroupMetricAttributesQuery @this = queryProvider as IGroupMetricAttributesQuery;
                string[] grouping = FilterGroupAttribute.Ensure(this.Grouping, queryProvider.Path);
                return @this.Get<IGroupMetricAttributesQuery, IMetricAttributesQuery>(x => x.Initialize(null, null, grouping[0]), queryProvider);
            }

            private bool GetDefaultSelectAll()
            {
                object[] objArray;
                return (this.GroupValues.TryGetValue(-2128831035, out objArray) && (objArray.Length > 1));
            }

            protected sealed override bool IsNullDisplayLookupItem(object value) => 
                value == BaseRowsKeeper.NullObject;

            public GroupUIEditorType EditorType =>
                this.editorType;

            public string[] Grouping =>
                this.grouping;

            public IDictionary<int, object[]> GroupValues =>
                this.groupValues.Values;

            public IDictionary<int, string[]> GroupTexts =>
                this.groupTexts.Texts;

            public bool HasUniqueValues =>
                this.GroupValues.ContainsKey(-2128831035);

            public object UniqueValues
            {
                get
                {
                    object[] objArray;
                    return (this.GroupValues.TryGetValue(-2128831035, out objArray) ? objArray : null);
                }
            }

            protected sealed override string[] DisplayTexts
            {
                get
                {
                    string[] strArray;
                    return (this.GroupTexts.TryGetValue(-2128831035, out strArray) ? strArray : null);
                }
            }

            public string SelectAllName =>
                this.selectAllName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.SelectAllName);

            public string NullName =>
                this.nullName ?? this.DefaultNullName;

            protected sealed override string DefaultNullName =>
                FilteringLocalizer.GetString(MetricAttributes.GroupMetricAttributes<T>.isString ? FilteringLocalizerStringId.BlanksName : (MetricAttributes.GroupMetricAttributes<T>.isDateOrTime ? FilteringLocalizerStringId.EmptyName : FilteringLocalizerStringId.NullName));

            public bool UseRadioSelection =>
                this.useRadioSelection ? true : this.DisplayRadio.GetValueOrDefault();

            public bool UseSelectAll =>
                this.UseRadioSelection ? false : this.useSelectAll.GetValueOrDefault(this.GetDefaultSelectAll());
        }

        private delegate IMetricAttributes GroupMetricCtor(GroupUIEditorType editorType, string[] children, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, string[] members);

        private sealed class GroupTextsBox : MetricAttributes.MemberValueBox
        {
            private static readonly IDictionary<int, string[]> Empty = new Dictionary<int, string[]>(0);

            internal GroupTextsBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal IDictionary<int, string[]> Texts =>
                base.HasMemberValue ? ((base.GetValue() as IDictionary<int, string[]>) ?? Empty) : Empty;
        }

        private sealed class GroupValuesBox : MetricAttributes.MemberValueBox
        {
            private static readonly IDictionary<int, object[]> Empty = new Dictionary<int, object[]>(0);

            internal GroupValuesBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal IDictionary<int, object[]> Values =>
                base.HasMemberValue ? ((base.GetValue() as IDictionary<int, object[]>) ?? Empty) : Empty;
        }

        private sealed class LookupMetricAttributes<T> : MetricAttributes.BaseLookupMetricAttributes, IUniqueValuesMetricAttributes, IMetricAttributes, ILookupMetricAttributes<T>, IMetricAttributes<T>, ILookupMetricAttributes, IBaseLookupMetricAttributes, ICollectionMetricAttributes
        {
            private static readonly bool isString;
            private static readonly bool isDateOrTime;
            private static readonly bool allowNull;
            private readonly string blanksName;
            private readonly MetricAttributes.UniqueValuesBox<T> unique;
            private readonly MetricAttributes.MemberLookupValuesBox<T> lookupValues;
            private readonly MetricAttributes.MemberNullableValueBox<int> top;
            private readonly MetricAttributes.MemberNullableValueBox<int> maxCount;
            private readonly bool? useBlanks;

            static LookupMetricAttributes()
            {
                MetricAttributes.LookupMetricAttributes<T>.isString = typeof(T) == typeof(string);
                MetricAttributes.LookupMetricAttributes<T>.isDateOrTime = IsDateTime(typeof(T));
                MetricAttributes.LookupMetricAttributes<T>.allowNull = DevExpress.Utils.Filtering.Internal.TypeHelper.AllowNull(typeof(T));
            }

            public LookupMetricAttributes(object dataSource, string valueMember, string displayMember, int? top, int? maxCount, LookupUIEditorType editorType, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, bool? useBlanks, string blanksName, string[] members) : base(useSelectAll, selectionMode, selectAllName, nullName, editorType, members, 1)
            {
                this.unique = new MetricAttributes.UniqueValuesBox<T>(-1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.LookupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.LookupMetricAttributes<T>.get_UniqueValues, MetricAttributes.LookupMetricAttributes<T>)), new ParameterExpression[0]));
                this.lookupValues = new MetricAttributes.MemberLookupValuesBox<T>(dataSource, valueMember, displayMember, 0, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.LookupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.LookupMetricAttributes<T>.get_DataSource, MetricAttributes.LookupMetricAttributes<T>)), new ParameterExpression[0]));
                this.top = new MetricAttributes.MemberNullableValueBox<int>(top, 1, this, Expression.Lambda<Func<int?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.LookupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.LookupMetricAttributes<T>.get_Top, MetricAttributes.LookupMetricAttributes<T>)), new ParameterExpression[0]));
                this.maxCount = new MetricAttributes.MemberNullableValueBox<int>(maxCount, 2, this, Expression.Lambda<Func<int?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.LookupMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.LookupMetricAttributes<T>.get_MaxCount, MetricAttributes.LookupMetricAttributes<T>)), new ParameterExpression[0]));
                this.useBlanks = useBlanks;
                this.blanksName = blanksName;
            }

            protected sealed override bool AllowDisplayLookup(object owner)
            {
                if (!MetricAttributes.LookupMetricAttributes<T>.isString)
                {
                    return true;
                }
                Func<EndUserFilteringMetric, bool> get = <>c<T>.<>9__11_0;
                if (<>c<T>.<>9__11_0 == null)
                {
                    Func<EndUserFilteringMetric, bool> local1 = <>c<T>.<>9__11_0;
                    get = <>c<T>.<>9__11_0 = m => m.IsExplicitLookup();
                }
                return (owner as EndUserFilteringMetric).Get<EndUserFilteringMetric, bool>(get, false);
            }

            protected sealed override bool CheckLookupDisplayText(object value, ref string text)
            {
                if (IsNullOrDBNull(value))
                {
                    text = base.NullName;
                }
                return (!IsNullOrDBNull(value) || MetricAttributes.LookupMetricAttributes<T>.allowNull);
            }

            private bool GetDefaultDisplayBlanks() => 
                ExcelFilterOptions.Default.ShowBlanks.GetValueOrDefault(true);

            protected sealed override bool GetDefaultSelectAll() => 
                !this.unique.HasValues || (QueryHelper.Count<T>(this.unique.Values) > 1);

            private bool GetDefaultUseBlanks() => 
                this.unique.HasValues && ((QueryHelper.Count<T>(this.unique.Values) > 0) && QueryHelper.Any<T>(this.unique.Values, new Func<object, bool>(MetricAttributes.IsBlank)));

            private static int? GetMaxCount(int? top, int? maxCount)
            {
                if ((maxCount != null) && (maxCount.Value > 0))
                {
                    return ((top == null) ? maxCount : new int?(Math.Max(maxCount.Value, top.Value)));
                }
                return null;
            }

            private static int? GetTop(int? top)
            {
                if ((top == null) || (top.Value > 0))
                {
                    return top;
                }
                return null;
            }

            private bool GetUseBlanks() => 
                this.useBlanks.GetValueOrDefault(this.GetDefaultUseBlanks());

            protected sealed override string DefaultNullName =>
                FilteringLocalizer.GetString(MetricAttributes.LookupMetricAttributes<T>.isDateOrTime ? FilteringLocalizerStringId.EmptyName : FilteringLocalizerStringId.NullName);

            public object UniqueValues =>
                this.unique.Values;

            public bool HasUniqueValues =>
                this.unique.HasValues;

            public object DataSource =>
                this.lookupValues.Value;

            public string ValueMember =>
                this.lookupValues.ValueMember;

            public string DisplayMember =>
                this.lookupValues.DisplayMember;

            public int? Top =>
                MetricAttributes.LookupMetricAttributes<T>.GetTop(this.top.Value);

            public int? MaxCount =>
                MetricAttributes.LookupMetricAttributes<T>.GetMaxCount(this.top.Value, this.maxCount.Value);

            public bool UseBlanks =>
                (!this.AllowBlanks || !this.GetUseBlanks()) ? false : this.DisplayBlanks.GetValueOrDefault(this.GetDefaultDisplayBlanks());

            private bool AllowBlanks =>
                MetricAttributes.LookupMetricAttributes<T>.isString ? true : this.FilterByDisplayText.GetValueOrDefault();

            public string BlanksName =>
                this.blanksName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.BlanksName);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly MetricAttributes.LookupMetricAttributes<T>.<>c <>9;
                public static Func<EndUserFilteringMetric, bool> <>9__11_0;

                static <>c()
                {
                    MetricAttributes.LookupMetricAttributes<T>.<>c.<>9 = new MetricAttributes.LookupMetricAttributes<T>.<>c();
                }

                internal bool <AllowDisplayLookup>b__11_0(EndUserFilteringMetric m) => 
                    m.IsExplicitLookup();
            }
        }

        private delegate IMetricAttributes LookupMetricCtor(object dataSource, string valueMember, string displayMember, int? top, int? maxCount, LookupUIEditorType editorType, ValueSelectionMode selectionMode, bool? useSelectAll, string selectAllName, string nullName, bool? useBlanks, string blanksName, string[] members);

        private sealed class MemberLookupValuesBox<T> : MetricAttributes.MemberValueBox
        {
            private readonly object defaultValue;

            internal MemberLookupValuesBox(object defaultValue, string valueMember, string displayMember, int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
                this.defaultValue = defaultValue;
                this.ValueMember = valueMember;
                this.DisplayMember = displayMember;
            }

            internal string ValueMember { get; private set; }

            internal string DisplayMember { get; private set; }

            internal object Value =>
                base.HasMemberValue ? base.GetValue() : this.defaultValue;

            internal bool HasMemberOrValue =>
                base.hasMember || !Equals(this.defaultValue, null);
        }

        private sealed class MemberNullableValueBox<T> : MetricAttributes.MemberNullableValueBoxBase<T> where T: struct
        {
            internal MemberNullableValueBox(T? defaultValue, int memberIndex, MetricAttributes owner, Expression<Func<T?>> propertyExpression) : base(defaultValue, memberIndex, owner, propertyExpression)
            {
            }
        }

        private abstract class MemberNullableValueBoxBase<T> : MetricAttributes.MemberValueBox where T: struct
        {
            private readonly T? defaultValue;

            internal MemberNullableValueBoxBase(T? defaultValue, int memberIndex, MetricAttributes owner, Expression<Func<T?>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
                this.defaultValue = defaultValue;
            }

            protected virtual T? GetNullable() => 
                new T?(base.ConvertValue<T>());

            internal T? Value =>
                base.HasMemberValue ? this.GetNullable() : this.defaultValue;

            internal bool HasMemberOrValue =>
                base.hasMember || (this.defaultValue != null);
        }

        private sealed class MemberNullableValueBoxCanBeNotLoaded<T> : MetricAttributes.MemberNullableValueBoxBase<T> where T: struct
        {
            internal MemberNullableValueBoxCanBeNotLoaded(T? defaultValue, int memberIndex, MetricAttributes owner, Expression<Func<T?>> propertyExpression) : base(defaultValue, memberIndex, owner, propertyExpression)
            {
            }

            protected sealed override T? GetNullable()
            {
                if (!base.IsNotLoaded)
                {
                    return new T?(base.ConvertValue<T>());
                }
                return null;
            }
        }

        private class MemberValueBox
        {
            private readonly MetricAttributes owner;
            private readonly LambdaExpression propertyExpression;
            internal readonly string propertyName;
            protected readonly bool hasMember;
            private object value;

            public MemberValueBox(int memberIndex, MetricAttributes owner, LambdaExpression propertyExpression)
            {
                this.owner = owner;
                this.propertyExpression = propertyExpression;
                this.propertyName = ExpressionHelper.GetPropertyName(propertyExpression);
                bool flag = memberIndex < 0;
                if (flag)
                {
                    memberIndex = owner.valueBoxes.Length + memberIndex;
                }
                owner.valueBoxes[memberIndex] = this;
                this.hasMember = flag || !string.IsNullOrEmpty(owner.members[memberIndex]);
                if (!this.hasMember | flag)
                {
                    owner.unboundMembers[memberIndex] = this.propertyName;
                }
            }

            protected T ConvertValue<T>() => 
                MetricAttributes.Converter.Convert<T>(this.value);

            protected object GetValue() => 
                this.value;

            protected void NotifyOwner(string property)
            {
                this.owner.RaisePropertyChanged(property);
            }

            protected virtual void OnUpdateValue()
            {
                this.owner.RaisePropertyChanged(this.propertyExpression);
            }

            protected virtual object PrepareValue(object value) => 
                value;

            protected internal bool TryGetValue(string member, out object value)
            {
                value = ((this.propertyName != member) || !this.HasMemberValue) ? null : this.value;
                return (value != null);
            }

            internal bool Update(object value)
            {
                if (Equals(this.value, value))
                {
                    return false;
                }
                this.value = this.PrepareValue(value);
                this.OnUpdateValue();
                return true;
            }

            protected bool HasMemberValue =>
                !Equals(this.value, null);

            protected bool IsNotLoaded =>
                this.value is NotLoadedObject;
        }

        private class MetricException : Exception
        {
            public MetricException(string path, string format, params object[] parameters) : base("[" + path + "] Error: " + string.Format(format, parameters))
            {
            }
        }

        private class RangeMetricAttributes<T> : MetricAttributes, IUniqueValuesMetricAttributes, IMetricAttributes, IRangeMetricAttributes<T>, IMetricAttributes<T>, IRangeMetricAttributes, ISummaryMetricAttributes where T: struct
        {
            private readonly MetricAttributes.DataControllerBox dataController;
            private readonly MetricAttributes.UniqueNullableValuesBox<T> unique;
            private readonly MetricAttributes.MemberNullableValueBoxCanBeNotLoaded<T> minimum;
            private readonly MetricAttributes.MemberNullableValueBoxCanBeNotLoaded<T> maximum;
            private readonly MetricAttributes.MemberNullableValueBox<T> average;
            private readonly Lazy<RangeUIEditorType> numericRangeUIEditorType;
            private readonly Lazy<DevExpress.Utils.Filtering.DateTimeRangeUIEditorType> dateTimeRangeUIEditorType;
            private readonly string fromName;
            private readonly string toName;
            private readonly string nullName;
            private static readonly bool isDateOrTime;
            private static readonly bool isTime;

            static RangeMetricAttributes()
            {
                MetricAttributes.RangeMetricAttributes<T>.isDateOrTime = IsDateTime(typeof(T));
                MetricAttributes.RangeMetricAttributes<T>.isTime = IsTimeSpan(typeof(T));
            }

            private RangeMetricAttributes(T? min, T? max, T? avg, string fromName, string toName, string nullName, string[] members) : base(members, 2)
            {
                this.dataController = new MetricAttributes.DataControllerBox(-2, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.RangeMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.RangeMetricAttributes<T>.get_DataController, MetricAttributes.RangeMetricAttributes<T>)), new ParameterExpression[0]));
                this.unique = new MetricAttributes.UniqueNullableValuesBox<T>(-1, this, Expression.Lambda<Func<object>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.RangeMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.RangeMetricAttributes<T>.get_UniqueValues, MetricAttributes.RangeMetricAttributes<T>)), new ParameterExpression[0]));
                this.minimum = new MetricAttributes.MemberNullableValueBoxCanBeNotLoaded<T>(min, 0, this, Expression.Lambda<Func<T?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.RangeMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.RangeMetricAttributes<T>.get_Minimum, MetricAttributes.RangeMetricAttributes<T>)), new ParameterExpression[0]));
                this.maximum = new MetricAttributes.MemberNullableValueBoxCanBeNotLoaded<T>(max, 1, this, Expression.Lambda<Func<T?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.RangeMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.RangeMetricAttributes<T>.get_Maximum, MetricAttributes.RangeMetricAttributes<T>)), new ParameterExpression[0]));
                this.average = new MetricAttributes.MemberNullableValueBox<T>(avg, 2, this, Expression.Lambda<Func<T?>>(Expression.Property(Expression.Constant(this, typeof(MetricAttributes.RangeMetricAttributes<T>)), (MethodInfo) methodof(MetricAttributes.RangeMetricAttributes<T>.get_Average, MetricAttributes.RangeMetricAttributes<T>)), new ParameterExpression[0]));
                this.fromName = fromName;
                this.toName = toName;
                this.nullName = nullName;
                Func<RangeUIEditorType> valueFactory = <>c<T>.<>9__12_0;
                if (<>c<T>.<>9__12_0 == null)
                {
                    Func<RangeUIEditorType> local1 = <>c<T>.<>9__12_0;
                    valueFactory = <>c<T>.<>9__12_0 = () => RangeUIEditorType.Default;
                }
                this.numericRangeUIEditorType = new Lazy<RangeUIEditorType>(valueFactory);
                Func<DevExpress.Utils.Filtering.DateTimeRangeUIEditorType> func2 = <>c<T>.<>9__12_1;
                if (<>c<T>.<>9__12_1 == null)
                {
                    Func<DevExpress.Utils.Filtering.DateTimeRangeUIEditorType> local2 = <>c<T>.<>9__12_1;
                    func2 = <>c<T>.<>9__12_1 = () => DevExpress.Utils.Filtering.DateTimeRangeUIEditorType.Default;
                }
                this.dateTimeRangeUIEditorType = new Lazy<DevExpress.Utils.Filtering.DateTimeRangeUIEditorType>(func2);
            }

            public RangeMetricAttributes(T? min, T? max, string fromName, string toName, string nullName, DevExpress.Utils.Filtering.DateTimeRangeUIEditorType editorType, string[] members) : this(min, max, nullable, fromName, toName, nullName, members)
            {
                this.dateTimeRangeUIEditorType = new Lazy<DevExpress.Utils.Filtering.DateTimeRangeUIEditorType>(() => editorType);
            }

            public RangeMetricAttributes(T? min, T? max, T? avg, string fromName, string toName, string nullName, RangeUIEditorType editorType, string[] members) : this(min, max, avg, fromName, toName, nullName, members)
            {
                this.numericRangeUIEditorType = new Lazy<RangeUIEditorType>(() => ((editorType != RangeUIEditorType.Default) || (((MetricAttributes.RangeMetricAttributes<T>) this).minimum.HasMemberOrValue && ((MetricAttributes.RangeMetricAttributes<T>) this).maximum.HasMemberOrValue)) ? editorType : RangeUIEditorType.Text);
            }

            public bool IsNumericRange =>
                !MetricAttributes.RangeMetricAttributes<T>.isDateOrTime;

            public bool IsTimeSpanRange =>
                MetricAttributes.RangeMetricAttributes<T>.isTime;

            public DevExpress.Data.DataController DataController =>
                this.dataController.Value;

            public object UniqueValues =>
                this.unique.Values;

            public bool HasUniqueValues =>
                this.unique.HasValues;

            public T? Minimum =>
                this.minimum.Value;

            public T? Maximum =>
                this.maximum.Value;

            public T? Average =>
                this.average.Value;

            public RangeUIEditorType NumericRangeUIEditorType =>
                this.numericRangeUIEditorType.Value;

            public DevExpress.Utils.Filtering.DateTimeRangeUIEditorType DateTimeRangeUIEditorType =>
                this.dateTimeRangeUIEditorType.Value;

            public string FromName =>
                this.fromName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.FromName);

            public string ToName =>
                this.toName ?? FilteringLocalizer.GetString(FilteringLocalizerStringId.ToName);

            public string NullName =>
                this.nullName ?? this.DefaultNullName;

            protected sealed override string DefaultNullName =>
                FilteringLocalizer.GetString(MetricAttributes.RangeMetricAttributes<T>.isDateOrTime ? FilteringLocalizerStringId.EmptyName : FilteringLocalizerStringId.NullName);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly MetricAttributes.RangeMetricAttributes<T>.<>c <>9;
                public static Func<RangeUIEditorType> <>9__12_0;
                public static Func<DateTimeRangeUIEditorType> <>9__12_1;

                static <>c()
                {
                    MetricAttributes.RangeMetricAttributes<T>.<>c.<>9 = new MetricAttributes.RangeMetricAttributes<T>.<>c();
                }

                internal RangeUIEditorType <.ctor>b__12_0() => 
                    RangeUIEditorType.Default;

                internal DateTimeRangeUIEditorType <.ctor>b__12_1() => 
                    DateTimeRangeUIEditorType.Default;
            }
        }

        private sealed class ResolveMetricTypeDefinitionException : MetricAttributes.MetricException
        {
            public ResolveMetricTypeDefinitionException(string path, Type type) : base(path, "Unable to resolve metric type definition for type {0}", objArray1)
            {
                object[] objArray1 = new object[] { type };
            }
        }

        private sealed class UniqueNullableEnumValuesBox<T> : MetricAttributes.MemberValueBox where T: struct
        {
            private readonly Type enumType;

            internal UniqueNullableEnumValuesBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression, Type enumDataType) : base(memberIndex, owner, propertyExpression)
            {
                Type underlyingType = Nullable.GetUnderlyingType(enumDataType);
                Type type2 = underlyingType;
                if (underlyingType == null)
                {
                    Type local1 = underlyingType;
                    type2 = enumDataType;
                }
                this.enumType = type2;
            }

            protected sealed override object PrepareValue(object value)
            {
                object[] objArray = value as object[];
                if (((objArray != null) && (objArray.Length != 0)) && (this.enumType.IsEnum && typeof(T).IsEnum))
                {
                    Type underlyingType = Enum.GetUnderlyingType(this.enumType);
                    for (int i = 0; i < objArray.Length; i++)
                    {
                        if ((objArray[i] != null) && (objArray[i].GetType() == underlyingType))
                        {
                            objArray[i] = Enum.ToObject(this.enumType, objArray[i]);
                        }
                    }
                }
                return value;
            }

            internal Type EnumType =>
                this.enumType;

            internal bool HasValues =>
                base.HasMemberValue;

            internal object Values =>
                base.HasMemberValue ? base.GetValue() : null;
        }

        private sealed class UniqueNullableValuesBox<T> : MetricAttributes.MemberValueBox where T: struct
        {
            internal UniqueNullableValuesBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal bool HasValues =>
                base.HasMemberValue;

            internal object Values =>
                base.HasMemberValue ? base.GetValue() : null;
        }

        private sealed class UniqueValuesBox<T> : MetricAttributes.MemberValueBox
        {
            internal UniqueValuesBox(int memberIndex, MetricAttributes owner, Expression<Func<object>> propertyExpression) : base(memberIndex, owner, propertyExpression)
            {
            }

            internal bool HasValues =>
                base.HasMemberValue;

            internal object Values =>
                base.HasMemberValue ? base.GetValue() : null;
        }
    }
}

