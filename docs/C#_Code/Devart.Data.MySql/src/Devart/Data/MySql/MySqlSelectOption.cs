namespace Devart.Data.MySql
{
    using System;

    [Flags]
    internal enum MySqlSelectOption
    {
        public const MySqlSelectOption None = MySqlSelectOption.None;,
        public const MySqlSelectOption AllRows = MySqlSelectOption.AllRows;,
        public const MySqlSelectOption Distinct = MySqlSelectOption.Distinct;,
        public const MySqlSelectOption DistinctRow = MySqlSelectOption.DistinctRow;,
        public const MySqlSelectOption HighPriority = MySqlSelectOption.HighPriority;,
        public const MySqlSelectOption StraightJoin = MySqlSelectOption.StraightJoin;,
        public const MySqlSelectOption SqlBigResult = MySqlSelectOption.SqlBigResult;,
        public const MySqlSelectOption SqlBufferResult = MySqlSelectOption.SqlBufferResult;,
        public const MySqlSelectOption SqlCache = MySqlSelectOption.SqlCache;,
        public const MySqlSelectOption SqlCalcFoundRows = MySqlSelectOption.SqlCalcFoundRows;,
        public const MySqlSelectOption SqlNoCache = MySqlSelectOption.SqlNoCache;,
        public const MySqlSelectOption SqlSmallResult = MySqlSelectOption.SqlSmallResult;
    }
}

