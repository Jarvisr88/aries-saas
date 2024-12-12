namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.Localization;
    using DevExpress.Utils.Localization.Internal;
    using System;

    public class FilterUIElementLocalizer : XtraLocalizer<FilterUIElementLocalizerStringId>
    {
        static FilterUIElementLocalizer()
        {
            SetActiveLocalizerProvider(new DefaultActiveLocalizerProvider<FilterUIElementLocalizerStringId>(CreateDefaultLocalizer()));
        }

        public static XtraLocalizer<FilterUIElementLocalizerStringId> CreateDefaultLocalizer() => 
            new FilterUIElementResXLocalizer();

        public override XtraLocalizer<FilterUIElementLocalizerStringId> CreateResXLocalizer() => 
            new FilterUIElementResXLocalizer();

        public static string GetString(FilterUIElementLocalizerStringId id) => 
            Active.GetLocalizedString(id);

        protected override void PopulateStringTable()
        {
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersNumericName, "Numeric Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersNumericDescription, "Numeric Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersDateTimeName, "Date Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersDateTimeDescription, "Date Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersDurationName, "Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersDurationDescription, "Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersTextName, "Text Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersTextDescription, "Text Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersBooleanName, "Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersBooleanDescription, "Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersEnumName, "Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFiltersEnumDescription, "Filters Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterEqualsName, "Equals");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterEqualsDescription, "Equals a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEqualName, "Does Not Equal");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEqualDescription, "Does not equal a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBetweenName, "Between");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBetweenDescription, "Values within a range");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNullName, "Is Null");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNullDescription, "Is empty");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNotNullName, "Is Not Null");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNotNullDescription, "Not empty");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanName, "Greater Than");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanDescription, "Greater than a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanOrEqualToName, "Greater Than Or Equal To");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterGreaterThanOrEqualToDescription, "Greater than or equal to a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLessThanName, "Less Than");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLessThanDescription, "Less than a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLessThanOrEqualToName, "Less Than Or Equal To");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLessThanOrEqualToDescription, "Less than or equal to a value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTopNName, "Top N");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTopNDescription, "The highest values");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBottomNName, "Bottom N");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBottomNDescription, "The lowest values");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierItemsName, "Items");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierItemsDescription, "Items Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierPercentsName, "Percents");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSequenceQualifierPercentsDescription, "Percents Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAboveAverageName, "Above Average");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAboveAverageDescription, "Values above the average");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBelowAverageName, "Below Average");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBelowAverageDescription, "Values below the average");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBeginsWithName, "Begins With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBeginsWithDescription, "Starts with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterEndsWithName, "Ends With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterEndsWithDescription, "Ends with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginWithName, "Does Not Begin With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginWithDescription, "Does not start with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndWithName, "Does Not End With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndWithDescription, "Does not end with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginsWithName, "Does Not Begin With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotBeginsWithDescription, "Does not start with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndsWithName, "Does Not End With");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotEndsWithDescription, "Does not end with a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterContainsName, "Contains");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterContainsDescription, "Contains a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotContainName, "Does Not Contain");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDoesNotContainDescription, "Does not contain a specific text");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsBlankName, "Is Blank");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsBlankDescription, "Empty or not specified");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNotBlankName, "Is Not Blank");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsNotBlankDescription, "Not empty");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLikeName, "Is Like");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLikeDescription, "Match a specific pattern");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNotLikeName, "Is Not Like");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNotLikeDescription, "Does not match a specific pattern");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsSameDayName, "Is Same Day");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterIsSameDayDescription, "The same date");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBeforeName, "Before");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterBeforeDescription, "Prior to a date");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAfterName, "After");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAfterDescription, "After a date");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTomorrowName, "Tomorrow");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTomorrowDescription, "Tomorrow");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTodayName, "Today");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterTodayDescription, "Today");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterYesterdayName, "Yesterday");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterYesterdayDescription, "Yesterday");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextWeekName, "Next Week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextWeekDescription, "Next week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisWeekName, "This Week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisWeekDescription, "This week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastWeekName, "Last Week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastWeekDescription, "Last week");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextMonthName, "Next Month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextMonthDescription, "Next month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisMonthName, "This Month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisMonthDescription, "This month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastMonthName, "Last Month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastMonthDescription, "Last month");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextQuarterName, "Next Quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextQuarterDescription, "Next quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisQuarterName, "This Quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisQuarterDescription, "This quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastQuarterName, "Last Quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastQuarterDescription, "Last quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextYearName, "Next Year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNextYearDescription, "Next year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisYearName, "This Year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterThisYearDescription, "This year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastYearName, "Last Year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterLastYearDescription, "Last year");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterYearToDateName, "Year To Date");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterYearToDateDescription, "From the beginning of the year to the present");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAllDatesInThePeriodName, "All Dates In The Period");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAllDatesInThePeriodDescription, "Dates within a range");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter1Name, "Quarter 1");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter1Description, "First quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter2Name, "Quarter 2");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter2Description, "Second quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter3Name, "Quarter 3");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter3Description, "Third quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter4Name, "Quarter 4");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterQuarter4Description, "Fourth quarter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJanuaryName, "January");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJanuaryDescription, "January");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterFebruaryName, "February");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterFebruaryDescription, "February");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterMarchName, "March");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterMarchDescription, "March");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAprilName, "April");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAprilDescription, "April");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterMayName, "May");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterMayDescription, "May");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJuneName, "June");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJuneDescription, "June");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJulyName, "July");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterJulyDescription, "July");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAugustName, "August");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterAugustDescription, "August");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSeptemberName, "September");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterSeptemberDescription, "September");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterOctoberName, "October");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterOctoberDescription, "October");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNovemberName, "November");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNovemberDescription, "November");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDecemberName, "December");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDecemberDescription, "December");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDatePeriodsName, "Specific Date Periods");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterDatePeriodsDescription, "Common date ranges");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNoneName, "Choose One");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterNoneDescription, "Choose One Description");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterCustomName, "Custom Filter");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterCustomDescription, "Two conditions combined by the AND or OR operator");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterUserName, "Predefined Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFilterUserDescription, "Predefined Filters");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptChooseOne, "Choose one...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterADate, "Enter a date...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterADuration, "Enter a duration...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectAValue, "Select a value...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptEnterAValue, "Enter a value...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectADate, "Select a date...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptSelectADuration, "Select a duration...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUINullValuePromptSearchControl, "Enter text to search...");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIFirstLabel, "First");
            this.AddString(FilterUIElementLocalizerStringId.CustomUISecondLabel, "Second");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUITabValues, "Values");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUITabGroups, "Groups");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUIClearFilter, "Clear Filter");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUIClose, "Close");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUISearchByYearCaption, "Year");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUISearchByMonthCaption, "Month");
            this.AddString(FilterUIElementLocalizerStringId.FilteringUISearchByDayCaption, "Day");
            this.AddString(FilterUIElementLocalizerStringId.CustomUIValueLabel, "Value");
            this.AddString(FilterUIElementLocalizerStringId.CustomUITypeLabel, "Type");
        }

        public static XtraLocalizer<FilterUIElementLocalizerStringId> Active
        {
            get => 
                XtraLocalizer<FilterUIElementLocalizerStringId>.Active;
            set => 
                XtraLocalizer<FilterUIElementLocalizerStringId>.Active = value;
        }
    }
}

