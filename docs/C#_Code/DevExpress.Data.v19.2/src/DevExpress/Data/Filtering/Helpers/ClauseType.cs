namespace DevExpress.Data.Filtering.Helpers
{
    using System;

    [Serializable]
    public enum ClauseType
    {
        public const ClauseType Equals = ClauseType.Equals;,
        public const ClauseType DoesNotEqual = ClauseType.DoesNotEqual;,
        public const ClauseType Greater = ClauseType.Greater;,
        public const ClauseType GreaterOrEqual = ClauseType.GreaterOrEqual;,
        public const ClauseType Less = ClauseType.Less;,
        public const ClauseType LessOrEqual = ClauseType.LessOrEqual;,
        public const ClauseType Between = ClauseType.Between;,
        public const ClauseType NotBetween = ClauseType.NotBetween;,
        public const ClauseType Contains = ClauseType.Contains;,
        public const ClauseType DoesNotContain = ClauseType.DoesNotContain;,
        public const ClauseType BeginsWith = ClauseType.BeginsWith;,
        public const ClauseType EndsWith = ClauseType.EndsWith;,
        public const ClauseType Like = ClauseType.Like;,
        public const ClauseType NotLike = ClauseType.NotLike;,
        public const ClauseType IsNull = ClauseType.IsNull;,
        public const ClauseType IsNotNull = ClauseType.IsNotNull;,
        public const ClauseType AnyOf = ClauseType.AnyOf;,
        public const ClauseType NoneOf = ClauseType.NoneOf;,
        public const ClauseType IsNullOrEmpty = ClauseType.IsNullOrEmpty;,
        public const ClauseType IsNotNullOrEmpty = ClauseType.IsNotNullOrEmpty;,
        public const ClauseType IsBeyondThisYear = ClauseType.IsBeyondThisYear;,
        public const ClauseType IsLaterThisYear = ClauseType.IsLaterThisYear;,
        public const ClauseType IsLaterThisMonth = ClauseType.IsLaterThisMonth;,
        public const ClauseType IsNextWeek = ClauseType.IsNextWeek;,
        public const ClauseType IsLaterThisWeek = ClauseType.IsLaterThisWeek;,
        public const ClauseType IsTomorrow = ClauseType.IsTomorrow;,
        public const ClauseType IsToday = ClauseType.IsToday;,
        public const ClauseType IsYesterday = ClauseType.IsYesterday;,
        public const ClauseType IsEarlierThisWeek = ClauseType.IsEarlierThisWeek;,
        public const ClauseType IsLastWeek = ClauseType.IsLastWeek;,
        public const ClauseType IsEarlierThisMonth = ClauseType.IsEarlierThisMonth;,
        public const ClauseType IsEarlierThisYear = ClauseType.IsEarlierThisYear;,
        public const ClauseType IsPriorThisYear = ClauseType.IsPriorThisYear;,
        public const ClauseType Function = ClauseType.Function;
    }
}

