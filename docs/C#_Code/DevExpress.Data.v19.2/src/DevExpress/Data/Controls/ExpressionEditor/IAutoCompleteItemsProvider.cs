namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;
    using System.Collections.Generic;

    public interface IAutoCompleteItemsProvider
    {
        IEnumerable<AutoCompleteItem> GetAutoCompleteItems(string expression, int caretPosition);
    }
}

