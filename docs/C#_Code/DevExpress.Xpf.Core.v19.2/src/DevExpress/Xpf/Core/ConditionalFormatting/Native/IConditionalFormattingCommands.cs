namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System.Windows.Input;

    public interface IConditionalFormattingCommands
    {
        ICommand ShowLessThanFormatConditionDialog { get; }

        ICommand ShowGreaterThanFormatConditionDialog { get; }

        ICommand ShowEqualToFormatConditionDialog { get; }

        ICommand ShowBetweenFormatConditionDialog { get; }

        ICommand ShowTextThatContainsFormatConditionDialog { get; }

        ICommand ShowADateOccurringFormatConditionDialog { get; }

        ICommand ShowUniqueDuplicateRuleFormatConditionDialog { get; }

        ICommand ShowCustomConditionFormatConditionDialog { get; }

        ICommand ShowTop10ItemsFormatConditionDialog { get; }

        ICommand ShowBottom10ItemsFormatConditionDialog { get; }

        ICommand ShowTop10PercentFormatConditionDialog { get; }

        ICommand ShowBottom10PercentFormatConditionDialog { get; }

        ICommand ShowAboveAverageFormatConditionDialog { get; }

        ICommand ShowBelowAverageFormatConditionDialog { get; }

        ICommand ClearFormatConditionsFromAllColumns { get; }

        ICommand ClearFormatConditionsFromColumn { get; }

        ICommand ShowConditionalFormattingManager { get; }

        ICommand ShowDataUpdateFormatConditionDialog { get; }

        ICommand AddFormatCondition { get; }
    }
}

