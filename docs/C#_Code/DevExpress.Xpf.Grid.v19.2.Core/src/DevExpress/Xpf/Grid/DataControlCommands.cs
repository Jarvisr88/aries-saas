namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    internal static class DataControlCommands
    {
        internal static readonly RoutedCommand changeColumnSortOrder = new RoutedCommand("ChangeColumnSortOrder", typeof(DataControlCommands));
        internal static readonly RoutedCommand clearColumnFilter = new RoutedCommand("ClearColumnFilter", typeof(DataControlCommands));
        internal static readonly RoutedCommand showFilterEditor = new RoutedCommand("ShowFilterEditor", typeof(DataControlCommands));
        internal static readonly RoutedCommand showColumnChooser = new RoutedCommand("ShowColumnChooser", typeof(DataControlCommands));
        internal static readonly RoutedCommand hideColumnChooser = new RoutedCommand("HideColumnChooser", typeof(DataControlCommands));
        internal static readonly RoutedCommand movePrevCell = new RoutedCommand("MovePrevCell", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveNextCell = new RoutedCommand("MoveNextCell", typeof(DataControlCommands));
        internal static readonly RoutedCommand movePrevRow = new RoutedCommand("MovePrevRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveNextRow = new RoutedCommand("MoveNextRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveFirstRow = new RoutedCommand("MoveFirstRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveLastRow = new RoutedCommand("MoveLastRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand movePrevPage = new RoutedCommand("MovePrevPage", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveNextPage = new RoutedCommand("MoveNextPage", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveFirstCell = new RoutedCommand("MoveFirstCell", typeof(DataControlCommands));
        internal static readonly RoutedCommand moveLastCell = new RoutedCommand("MoveLastCell", typeof(DataControlCommands));
        internal static readonly RoutedCommand clearFilter = new RoutedCommand("ClearFilter", typeof(DataControlCommands));
        internal static readonly RoutedCommand deleteFocusedRow = new RoutedCommand("DeleteFocusedRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand editFocusedRow = new RoutedCommand("EditFocusedRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand cancelEditFocusedRow = new RoutedCommand("CancelEditFocusedRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand endEditFocusedRow = new RoutedCommand("EndEditFocusedRow", typeof(DataControlCommands));
        internal static readonly RoutedCommand showUnboundExpressionEditor = new RoutedCommand("ShowUnboundExpressionEditor", typeof(DataControlCommands));

        [Description("")]
        public static RoutedCommand ChangeColumnSortOrder =>
            changeColumnSortOrder;

        [Description("")]
        public static RoutedCommand ClearColumnFilter =>
            clearColumnFilter;

        [Description("")]
        public static RoutedCommand ShowFilterEditor =>
            showFilterEditor;

        [Description("")]
        public static RoutedCommand ShowColumnChooser =>
            showColumnChooser;

        [Description("")]
        public static RoutedCommand HideColumnChooser =>
            hideColumnChooser;

        [Description("")]
        public static RoutedCommand MovePrevCell =>
            movePrevCell;

        [Description("")]
        public static RoutedCommand MoveNextCell =>
            moveNextCell;

        [Description("")]
        public static RoutedCommand MovePrevRow =>
            movePrevRow;

        [Description("")]
        public static RoutedCommand MoveNextRow =>
            moveNextRow;

        [Description("")]
        public static RoutedCommand MoveFirstRow =>
            moveFirstRow;

        [Description("")]
        public static RoutedCommand MoveLastRow =>
            moveLastRow;

        [Description("")]
        public static RoutedCommand MovePrevPage =>
            movePrevPage;

        [Description("")]
        public static RoutedCommand MoveNextPage =>
            moveNextPage;

        [Description("")]
        public static RoutedCommand MoveFirstCell =>
            moveFirstCell;

        [Description("")]
        public static RoutedCommand MoveLastCell =>
            moveLastCell;

        [Description("")]
        public static RoutedCommand ClearFilter =>
            clearFilter;

        [Description("")]
        public static RoutedCommand DeleteFocusedRow =>
            deleteFocusedRow;

        [Description("")]
        public static RoutedCommand EditFocusedRow =>
            editFocusedRow;

        [Description("")]
        public static RoutedCommand CancelEditFocusedRow =>
            cancelEditFocusedRow;

        [Description("")]
        public static RoutedCommand EndEditFocusedRow =>
            endEditFocusedRow;

        [Description("")]
        public static RoutedCommand ShowUnboundExpressionEditor =>
            showUnboundExpressionEditor;
    }
}

