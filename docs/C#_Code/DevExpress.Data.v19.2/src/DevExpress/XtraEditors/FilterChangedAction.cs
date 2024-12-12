namespace DevExpress.XtraEditors
{
    using System;

    public enum FilterChangedAction
    {
        public const FilterChangedAction RebuildWholeTree = FilterChangedAction.RebuildWholeTree;,
        public const FilterChangedAction FilterStringChanged = FilterChangedAction.FilterStringChanged;,
        public const FilterChangedAction ValueChanged = FilterChangedAction.ValueChanged;,
        public const FilterChangedAction FieldNameChange = FilterChangedAction.FieldNameChange;,
        public const FilterChangedAction OperationChanged = FilterChangedAction.OperationChanged;,
        public const FilterChangedAction GroupTypeChanged = FilterChangedAction.GroupTypeChanged;,
        public const FilterChangedAction RemoveNode = FilterChangedAction.RemoveNode;,
        public const FilterChangedAction AddNode = FilterChangedAction.AddNode;,
        public const FilterChangedAction ClearAll = FilterChangedAction.ClearAll;,
        public const FilterChangedAction AggregateOperationChanged = FilterChangedAction.AggregateOperationChanged;,
        public const FilterChangedAction AggregatePropertyChanged = FilterChangedAction.AggregatePropertyChanged;
    }
}

