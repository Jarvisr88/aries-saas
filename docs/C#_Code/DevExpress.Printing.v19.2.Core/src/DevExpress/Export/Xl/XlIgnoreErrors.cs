namespace DevExpress.Export.Xl
{
    using System;

    [Flags]
    public enum XlIgnoreErrors
    {
        None = 0,
        CalculatedColumn = 1,
        EmptyCellReference = 2,
        EvaluationError = 4,
        Formula = 8,
        FormulaRange = 0x10,
        ListDataValidation = 0x20,
        NumberStoredAsText = 0x40,
        TwoDigitTextYear = 0x80,
        UnlockedFormula = 0x100,
        Any = 0x1ff
    }
}

