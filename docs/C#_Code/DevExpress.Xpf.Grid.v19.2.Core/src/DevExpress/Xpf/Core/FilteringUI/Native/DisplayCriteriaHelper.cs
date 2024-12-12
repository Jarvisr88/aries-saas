namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    internal class DisplayCriteriaHelper : IDisplayCriteriaGeneratorNamesSourceEx, IDisplayCriteriaGeneratorNamesSource
    {
        private readonly DisplayCriteriaHelperClient client;
        private Locker inRangeLocker = new Locker();

        private DisplayCriteriaHelper(DisplayCriteriaHelperClient client)
        {
            this.client = client;
        }

        T IDisplayCriteriaGeneratorNamesSourceEx.WithDateRangeProcessing<T>(Func<T> func)
        {
            T result = default(T);
            this.inRangeLocker.DoLockedAction<T>(delegate {
                T local;
                result = local = func();
                return local;
            });
            return result;
        }

        public static string GetColumnDisplayText(DisplayCriteriaHelperClient client, string propertyName, bool roundDateTimeFilter, object value)
        {
            DisplayCriteriaColumnTraits column = client.GetColumn(propertyName);
            if ((column != null) && (column.ColumnFilterMode == ColumnFilterMode.Value))
            {
                TextEditSettings textSettings = column.ActualEditSettings as TextEditSettings;
                return GetRoundDateDisplayTextOrFallback(roundDateTimeFilter, value, column.RoundDateDisplayFormat, textSettings, () => client.HasCustomColumnDisplayTextSubscription() || (!BetweenDatesHelper.CanSubstitute(textSettings) || (column.RoundDateDisplayFormat != null)), column.GetDisplayText);
            }
            Func<bool> shouldUseRoundDateDisplayFormat = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<bool> local1 = <>c.<>9__9_1;
                shouldUseRoundDateDisplayFormat = <>c.<>9__9_1 = () => true;
            }
            return GetRoundDateDisplayTextOrFallback(roundDateTimeFilter, value, null, null, shouldUseRoundDateDisplayFormat, new Func<object, string>(DisplayCriteriaHelper.GetValueString));
        }

        public string GetDisplayPropertyName(OperandProperty property)
        {
            object obj1;
            DisplayCriteriaColumnTraits traits = this.client.GetColumn(property.PropertyName);
            if (traits == null)
            {
                obj1 = null;
            }
            else if (traits.HeaderCaption != null)
            {
                obj1 = traits.HeaderCaption.ToString();
            }
            else
            {
                object headerCaption = traits.HeaderCaption;
                obj1 = null;
            }
            object local2 = obj1;
            object propertyName = local2;
            if (local2 == null)
            {
                object local3 = local2;
                propertyName = property.PropertyName;
            }
            return this.ReplaceEscapeSequences((string) propertyName);
        }

        internal static object GetFilterCriteria(CriteriaOperator filter, DisplayCriteriaHelperClient client, bool useFriendlyDateRangePresentation, Func<CriteriaOperator, object> raiseCustomDisplayCriteria)
        {
            if (useFriendlyDateRangePresentation)
            {
                filter = BetweenDatesHelper.SubstituteDateInRange(filter);
            }
            return raiseCustomDisplayCriteria(DXDisplayCriteriaGenerator.ProcessEx(new DisplayCriteriaHelper(client), filter));
        }

        public static string GetFilterDisplayText(CriteriaOperator filter, DisplayCriteriaHelperClient client, bool useFriendlyDateRangePresentation, Func<CriteriaOperator, object> raiseCustomDisplayCriteria)
        {
            Lazy<List<FormatConditionFilter>> formatFilters = new Lazy<List<FormatConditionFilter>>(() => client.GetFormatConditionFilters().ToList<FormatConditionFilter>());
            return GetFilterPanelText(GetFilterCriteria(filter, client, useFriendlyDateRangePresentation, raiseCustomDisplayCriteria), f => formatFilters.Value.Any<FormatConditionFilter>(x => x.IsMatchedInfo(f)));
        }

        private static string GetFilterPanelText(object op, Func<AppliedFormatConditionFilterInfo, bool> isValidFormatConditionFilter) => 
            (op != null) ? (!(op is CriteriaOperator) ? op.ToString() : LocalaizableCriteriaToStringProcessor.Process(op as CriteriaOperator, isValidFormatConditionFilter)) : string.Empty;

        private static string GetRoundDateDisplayTextOrFallback(bool roundDateTimeFilter, object value, string roundDateDisplayFormat, TextEditSettings settings, Func<bool> shouldUseRoundDateDisplayFormat, Func<object, string> fallback)
        {
            if (!roundDateTimeFilter || (!(value is DateTime) || !shouldUseRoundDateDisplayFormat()))
            {
                return fallback(value);
            }
            string format = roundDateDisplayFormat;
            if (roundDateDisplayFormat == null)
            {
                string local1 = roundDateDisplayFormat;
                format = "d";
            }
            return ((DateTime) value).ToString(format, ((settings == null) || !settings.MaskUseAsDisplayFormat) ? CultureInfo.CurrentCulture : settings.MaskCulture);
        }

        public string GetValueScreenText(OperandProperty property, object value) => 
            (property != null) ? GetColumnDisplayText(this.client, property.PropertyName, this.inRangeLocker.IsLocked, value) : GetValueString(value);

        private static string GetValueString(object value) => 
            (value != null) ? value.ToString() : string.Empty;

        private string ReplaceEscapeSequences(string input)
        {
            input = input.Replace("\n", " ");
            input = input.Replace("\t", " ");
            input = input.Replace("\r", " ");
            input = Regex.Replace(input, @"\s{2,}", " ");
            return input;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DisplayCriteriaHelper.<>c <>9 = new DisplayCriteriaHelper.<>c();
            public static Func<bool> <>9__9_1;

            internal bool <GetColumnDisplayText>b__9_1() => 
                true;
        }
    }
}

