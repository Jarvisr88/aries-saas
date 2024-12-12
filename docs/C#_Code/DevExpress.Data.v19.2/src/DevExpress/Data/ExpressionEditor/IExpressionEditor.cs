namespace DevExpress.Data.ExpressionEditor
{
    using System;

    public interface IExpressionEditor
    {
        string GetFunctionTypeStringID(string functionType);
        string GetResourceString(string stringId);
        void HideFunctionsTypes();
        void SetDescription(string description);
        void ShowError(string error);
        void ShowFunctionsTypes();

        ExpressionEditorLogic EditorLogic { get; }

        IMemoEdit ExpressionMemoEdit { get; }

        ISelector ListOfInputTypes { get; }

        ISelector ListOfInputParameters { get; }

        ISelector FunctionsTypes { get; }

        string FilterCriteriaInvalidExpressionMessage { get; }

        string FilterCriteriaInvalidExpressionExMessage { get; }
    }
}

