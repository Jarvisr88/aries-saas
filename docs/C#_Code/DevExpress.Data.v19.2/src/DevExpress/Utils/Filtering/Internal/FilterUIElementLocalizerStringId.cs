﻿namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel;

    public enum FilterUIElementLocalizerStringId
    {
        CustomUIFiltersNumericName = 0,
        CustomUIFiltersNumericDescription = 1,
        CustomUIFiltersDateTimeName = 2,
        CustomUIFiltersDateTimeDescription = 3,
        CustomUIFiltersDurationName = 4,
        CustomUIFiltersDurationDescription = 5,
        CustomUIFiltersTextName = 6,
        CustomUIFiltersTextDescription = 7,
        CustomUIFiltersBooleanName = 8,
        CustomUIFiltersBooleanDescription = 9,
        CustomUIFiltersEnumName = 10,
        CustomUIFiltersEnumDescription = 11,
        CustomUIFilterEqualsName = 12,
        CustomUIFilterEqualsDescription = 13,
        CustomUIFilterDoesNotEqualName = 14,
        CustomUIFilterDoesNotEqualDescription = 15,
        CustomUIFilterBetweenName = 0x10,
        CustomUIFilterBetweenDescription = 0x11,
        CustomUIFilterIsNullName = 0x12,
        CustomUIFilterIsNullDescription = 0x13,
        CustomUIFilterIsNotNullName = 20,
        CustomUIFilterIsNotNullDescription = 0x15,
        CustomUIFilterGreaterThanName = 0x16,
        CustomUIFilterGreaterThanDescription = 0x17,
        CustomUIFilterGreaterThanOrEqualToName = 0x18,
        CustomUIFilterGreaterThanOrEqualToDescription = 0x19,
        CustomUIFilterLessThanName = 0x1a,
        CustomUIFilterLessThanDescription = 0x1b,
        CustomUIFilterLessThanOrEqualToName = 0x1c,
        CustomUIFilterLessThanOrEqualToDescription = 0x1d,
        CustomUIFilterTopNName = 30,
        CustomUIFilterTopNDescription = 0x1f,
        CustomUIFilterBottomNName = 0x20,
        CustomUIFilterBottomNDescription = 0x21,
        CustomUIFilterSequenceQualifierItemsName = 0x22,
        CustomUIFilterSequenceQualifierItemsDescription = 0x23,
        CustomUIFilterSequenceQualifierPercentsName = 0x24,
        CustomUIFilterSequenceQualifierPercentsDescription = 0x25,
        CustomUIFilterAboveAverageName = 0x26,
        CustomUIFilterAboveAverageDescription = 0x27,
        CustomUIFilterBelowAverageName = 40,
        CustomUIFilterBelowAverageDescription = 0x29,
        CustomUIFilterBeginsWithName = 0x2a,
        CustomUIFilterBeginsWithDescription = 0x2b,
        CustomUIFilterEndsWithName = 0x2c,
        CustomUIFilterEndsWithDescription = 0x2d,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterDoesNotBeginsWithName = 0x2e,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterDoesNotBeginsWithDescription = 0x2f,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterDoesNotEndsWithName = 0x30,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterDoesNotEndsWithDescription = 0x31,
        CustomUIFilterContainsName = 50,
        CustomUIFilterContainsDescription = 0x33,
        CustomUIFilterDoesNotContainName = 0x34,
        CustomUIFilterDoesNotContainDescription = 0x35,
        CustomUIFilterIsBlankName = 0x36,
        CustomUIFilterIsBlankDescription = 0x37,
        CustomUIFilterIsNotBlankName = 0x38,
        CustomUIFilterIsNotBlankDescription = 0x39,
        CustomUIFilterLikeName = 0x3a,
        CustomUIFilterLikeDescription = 0x3b,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterNotLikeName = 60,
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        CustomUIFilterNotLikeDescription = 0x3d,
        CustomUIFilterIsSameDayName = 0x3e,
        CustomUIFilterIsSameDayDescription = 0x3f,
        CustomUIFilterBeforeName = 0x40,
        CustomUIFilterBeforeDescription = 0x41,
        CustomUIFilterAfterName = 0x42,
        CustomUIFilterAfterDescription = 0x43,
        CustomUIFilterTomorrowName = 0x44,
        CustomUIFilterTomorrowDescription = 0x45,
        CustomUIFilterTodayName = 70,
        CustomUIFilterTodayDescription = 0x47,
        CustomUIFilterYesterdayName = 0x48,
        CustomUIFilterYesterdayDescription = 0x49,
        CustomUIFilterNextWeekName = 0x4a,
        CustomUIFilterNextWeekDescription = 0x4b,
        CustomUIFilterThisWeekName = 0x4c,
        CustomUIFilterThisWeekDescription = 0x4d,
        CustomUIFilterLastWeekName = 0x4e,
        CustomUIFilterLastWeekDescription = 0x4f,
        CustomUIFilterNextMonthName = 80,
        CustomUIFilterNextMonthDescription = 0x51,
        CustomUIFilterThisMonthName = 0x52,
        CustomUIFilterThisMonthDescription = 0x53,
        CustomUIFilterLastMonthName = 0x54,
        CustomUIFilterLastMonthDescription = 0x55,
        CustomUIFilterNextQuarterName = 0x56,
        CustomUIFilterNextQuarterDescription = 0x57,
        CustomUIFilterThisQuarterName = 0x58,
        CustomUIFilterThisQuarterDescription = 0x59,
        CustomUIFilterLastQuarterName = 90,
        CustomUIFilterLastQuarterDescription = 0x5b,
        CustomUIFilterNextYearName = 0x5c,
        CustomUIFilterNextYearDescription = 0x5d,
        CustomUIFilterThisYearName = 0x5e,
        CustomUIFilterThisYearDescription = 0x5f,
        CustomUIFilterLastYearName = 0x60,
        CustomUIFilterLastYearDescription = 0x61,
        CustomUIFilterYearToDateName = 0x62,
        CustomUIFilterYearToDateDescription = 0x63,
        CustomUIFilterDatePeriodsName = 100,
        CustomUIFilterDatePeriodsDescription = 0x65,
        CustomUIFilterAllDatesInThePeriodName = 0x66,
        CustomUIFilterAllDatesInThePeriodDescription = 0x67,
        CustomUIFilterQuarter1Name = 0x68,
        CustomUIFilterQuarter1Description = 0x69,
        CustomUIFilterQuarter2Name = 0x6a,
        CustomUIFilterQuarter2Description = 0x6b,
        CustomUIFilterQuarter3Name = 0x6c,
        CustomUIFilterQuarter3Description = 0x6d,
        CustomUIFilterQuarter4Name = 110,
        CustomUIFilterQuarter4Description = 0x6f,
        CustomUIFilterJanuaryName = 0x70,
        CustomUIFilterJanuaryDescription = 0x71,
        CustomUIFilterFebruaryName = 0x72,
        CustomUIFilterFebruaryDescription = 0x73,
        CustomUIFilterMarchName = 0x74,
        CustomUIFilterMarchDescription = 0x75,
        CustomUIFilterAprilName = 0x76,
        CustomUIFilterAprilDescription = 0x77,
        CustomUIFilterMayName = 120,
        CustomUIFilterMayDescription = 0x79,
        CustomUIFilterJuneName = 0x7a,
        CustomUIFilterJuneDescription = 0x7b,
        CustomUIFilterJulyName = 0x7c,
        CustomUIFilterJulyDescription = 0x7d,
        CustomUIFilterAugustName = 0x7e,
        CustomUIFilterAugustDescription = 0x7f,
        CustomUIFilterSeptemberName = 0x80,
        CustomUIFilterSeptemberDescription = 0x81,
        CustomUIFilterOctoberName = 130,
        CustomUIFilterOctoberDescription = 0x83,
        CustomUIFilterNovemberName = 0x84,
        CustomUIFilterNovemberDescription = 0x85,
        CustomUIFilterDecemberName = 0x86,
        CustomUIFilterDecemberDescription = 0x87,
        CustomUIFilterNoneName = 0x88,
        CustomUIFilterNoneDescription = 0x89,
        CustomUIFilterCustomName = 0x8a,
        CustomUIFilterCustomDescription = 0x8b,
        CustomUIFilterUserName = 140,
        CustomUIFilterUserDescription = 0x8d,
        CustomUINullValuePromptChooseOne = 0x8e,
        CustomUINullValuePromptEnterADate = 0x8f,
        CustomUINullValuePromptEnterADuration = 0x90,
        CustomUINullValuePromptSelectAValue = 0x91,
        CustomUINullValuePromptEnterAValue = 0x92,
        CustomUINullValuePromptSelectADate = 0x93,
        CustomUINullValuePromptSelectADuration = 0x94,
        CustomUINullValuePromptSearchControl = 0x95,
        CustomUIFirstLabel = 150,
        CustomUISecondLabel = 0x97,
        FilteringUITabValues = 0x98,
        FilteringUITabGroups = 0x99,
        FilteringUIClearFilter = 0x9a,
        FilteringUIClose = 0x9b,
        FilteringUISearchByYearCaption = 0x9c,
        FilteringUISearchByMonthCaption = 0x9d,
        FilteringUISearchByDayCaption = 0x9e,
        CustomUIValueLabel = 0x9f,
        CustomUITypeLabel = 160,
        CustomUIFilterDoesNotBeginWithName = 0xa1,
        CustomUIFilterDoesNotBeginWithDescription = 0xa2,
        CustomUIFilterDoesNotEndWithName = 0xa3,
        CustomUIFilterDoesNotEndWithDescription = 0xa4
    }
}

