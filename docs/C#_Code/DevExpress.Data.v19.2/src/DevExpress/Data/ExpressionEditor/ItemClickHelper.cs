namespace DevExpress.Data.ExpressionEditor
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;

    public class ItemClickHelper
    {
        protected IExpressionEditor editor;
        protected Dictionary<string, string> itemsTable;

        public ItemClickHelper(IExpressionEditor editor);
        protected virtual void AddItemTable(string key, string description, int offset);
        public void FillItems();
        protected virtual void FillItemsTable();
        public virtual int GetCursorOffset(string item);
        public string GetDescription(string name);
        public object[] GetItems();
        public virtual string GetSpecificItem(string textItem);
        public static ItemClickHelper Instance(string clickedText, IExpressionEditor editor);

        public virtual ColumnSortOrder ParametersSortOrder { get; }

        public class FunctionInfo
        {
            private string function;
            private string description;
            private int cursorOffset;
            private FunctionEditorCategory category;

            public FunctionInfo(string function, string description, int cursorOffset, FunctionEditorCategory category);

            public string Function { get; }

            public string Description { get; }

            public int CursorOffset { get; }

            public FunctionEditorCategory Category { get; }
        }

        private class FunctionsClickHelper : ItemClickHelper
        {
            private Dictionary<string, int> indexToOffset;
            private Dictionary<string, FunctionEditorCategory> indexToCategory;
            private List<ItemClickHelper.FunctionInfo> functions;
            private readonly FunctionEditorCategory category;

            public FunctionsClickHelper(IExpressionEditor editor, FunctionEditorCategory category);
            protected override void AddItemTable(string key, string Description, int offset);
            private void FillFunctions();
            protected override void FillItemsTable();
            public override int GetCursorOffset(string item);
            public override string GetSpecificItem(string textItem);
            public static ItemClickHelper.FunctionsClickHelper Instance(IExpressionEditor editor);
        }
    }
}

