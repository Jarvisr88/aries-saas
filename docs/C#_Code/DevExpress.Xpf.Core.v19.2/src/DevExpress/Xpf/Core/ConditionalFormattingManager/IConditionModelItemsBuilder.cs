namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;

    public interface IConditionModelItemsBuilder
    {
        IModelItem BuildColorScaleCondition(ColorScaleEditUnit unit, IModelItem source);
        IModelItem BuildCondition(ConditionEditUnit unit, IModelItem source);
        IModelItem BuildDataBarCondition(DataBarEditUnit unit, IModelItem source);
        IModelItem BuildDataUpdateFormatCondition(AnimationEditUnit unit, IModelItem source);
        IModelItem BuildIconSetCondition(IconSetEditUnit unit, IModelItem source);
        IModelItem BuildTopBottomCondition(TopBottomEditUnit unit, IModelItem source);
        IModelItem BuildUniqueDuplicateCondition(UniqueDuplicateEditUnit unit, IModelItem source);
    }
}

