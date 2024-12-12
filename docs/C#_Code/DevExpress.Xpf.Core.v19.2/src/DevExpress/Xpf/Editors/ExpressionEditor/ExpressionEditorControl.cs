namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.ExpressionEditor.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public class ExpressionEditorControl : Control, IExpressionEditor, IDialogContent, ISupportExpressionString
    {
        internal TextEdit expressionTextEdit;
        internal Button buttonPlus;
        internal Button buttonMinus;
        internal Button buttonMultiply;
        internal Button buttonDivide;
        internal Button buttonModulo;
        internal Button wrapSelectionButton;
        internal Button buttonEqual;
        internal Button buttonNotEqual;
        internal Button buttonLess;
        internal Button buttonLessOrEqual;
        internal Button buttonLargerOrEqual;
        internal Button buttonLarger;
        internal Button buttonAnd;
        internal Button buttonOr;
        internal Button buttonNot;
        internal ListBox listOfInputTypes;
        internal ComboBoxEdit functionsTypes;
        internal DXListBox listOfInputParameters;
        internal TextEdit descriptionEdit;
        protected ExpressionEditorLogic fEditorLogic;
        protected IMemoEdit ExpressionMemoEdit;
        private ISelector ListOfInputTypes;
        private ISelector ListOfInputParameters;
        private ISelector FunctionsTypes;

        public ExpressionEditorControl()
        {
            base.DefaultStyleKey = typeof(ExpressionEditorControl);
            base.Loaded += new RoutedEventHandler(this.ExpressionEditorControl_Loaded);
        }

        public ExpressionEditorControl(IDataColumnInfo columnInfo) : this()
        {
            this.ColumnInfo = columnInfo;
        }

        string IExpressionEditor.GetFunctionTypeStringID(string functionType) => 
            "FunctionType_" + functionType;

        string IExpressionEditor.GetResourceString(string stringId) => 
            this.GetResourceStringCore(stringId);

        void IExpressionEditor.HideFunctionsTypes()
        {
            this.functionsTypes.Visibility = Visibility.Collapsed;
        }

        void IExpressionEditor.SetDescription(string description)
        {
            this.descriptionEdit.Text = description;
        }

        void IExpressionEditor.ShowError(string error)
        {
            DXMessageBox.Show(error, EditorLocalizer.GetString(EditorStringId.CaptionError), MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        void IExpressionEditor.ShowFunctionsTypes()
        {
            this.functionsTypes.Visibility = Visibility.Visible;
        }

        bool IDialogContent.CanCloseWithOKResult() => 
            this.fEditorLogic.CanCloseWithOKResult();

        void IDialogContent.OnApply()
        {
        }

        void IDialogContent.OnOk()
        {
        }

        string ISupportExpressionString.GetExpressionString(IDataColumnInfo columnInfo) => 
            this.Expression;

        void ISupportExpressionString.SetExpressionString(IDataColumnInfo columnInfo, string value)
        {
            if (this.expressionTextEdit != null)
            {
                this.expressionTextEdit.Text = value;
            }
        }

        private void ExpressionEditorControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.fEditorLogic != null)
            {
                this.fEditorLogic.OnLoad();
            }
        }

        private void functionsTypes_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            this.fEditorLogic.OnFunctionTypeChanged();
        }

        protected virtual ExpressionEditorLogic GetExpressionEditorLogic() => 
            new ExpressionEditorLogicEx(this, this.ColumnInfo);

        private string GetResourceStringCore(string stringId) => 
            EditorLocalizer.GetString("ExpressionEditor_" + stringId.Replace(".", "_").Replace(" ", "_"));

        private void InitializeStandardOperationButton(Button button, string operation)
        {
            if (button != null)
            {
                button.Click += new RoutedEventHandler(this.insertOperationButton_Click);
                button.Tag = operation;
            }
        }

        private void InitializeStandardOperationButtons()
        {
            this.InitializeStandardOperationButton(this.buttonPlus, " + ");
            this.InitializeStandardOperationButton(this.buttonMinus, " - ");
            this.InitializeStandardOperationButton(this.buttonMultiply, " * ");
            this.InitializeStandardOperationButton(this.buttonDivide, " / ");
            this.InitializeStandardOperationButton(this.buttonModulo, " % ");
            this.InitializeStandardOperationButton(this.buttonEqual, " == ");
            this.InitializeStandardOperationButton(this.buttonNotEqual, " != ");
            this.InitializeStandardOperationButton(this.buttonLess, " < ");
            this.InitializeStandardOperationButton(this.buttonLessOrEqual, " <= ");
            this.InitializeStandardOperationButton(this.buttonLargerOrEqual, " >= ");
            this.InitializeStandardOperationButton(this.buttonLarger, " > ");
            this.InitializeStandardOperationButton(this.buttonAnd, " And ");
            this.InitializeStandardOperationButton(this.buttonOr, " Or ");
            this.InitializeStandardOperationButton(this.buttonNot, " Not ");
        }

        private void insertOperationButton_Click(object sender, RoutedEventArgs e)
        {
            this.fEditorLogic.OnInsertOperation((string) ((Button) sender).Tag);
        }

        private void listOfInputParameters_MouseDoubleClick(object sender, EventArgs e)
        {
            this.fEditorLogic.OnInsertInputParameter();
        }

        private void listOfInputParameters_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.fEditorLogic.OnInputParametersChanged();
        }

        private void listOfInputTypes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.fEditorLogic.OnInputTypeChanged();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.listOfInputParameters != null)
            {
                this.listOfInputParameters.ItemLeftButtonDoubleClick -= new EventHandler(this.listOfInputParameters_MouseDoubleClick);
                this.listOfInputParameters.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(this.listOfInputParameters_SelectionChanged);
            }
            if (this.wrapSelectionButton != null)
            {
                this.wrapSelectionButton.Click -= new RoutedEventHandler(this.wrapSelectionButton_Click);
            }
            if (this.listOfInputTypes != null)
            {
                this.listOfInputTypes.SelectionChanged -= new System.Windows.Controls.SelectionChangedEventHandler(this.listOfInputTypes_SelectionChanged);
            }
            if (this.functionsTypes != null)
            {
                this.functionsTypes.SelectedIndexChanged -= new RoutedEventHandler(this.functionsTypes_SelectedIndexChanged);
            }
            this.UnsubscribeStandardOperationButtons();
            this.expressionTextEdit = (TextEdit) base.GetTemplateChild("expressionTextEdit");
            this.buttonPlus = (Button) base.GetTemplateChild("buttonPlus");
            this.buttonMinus = (Button) base.GetTemplateChild("buttonMinus");
            this.buttonMultiply = (Button) base.GetTemplateChild("buttonMultiply");
            this.buttonDivide = (Button) base.GetTemplateChild("buttonDivide");
            this.buttonModulo = (Button) base.GetTemplateChild("buttonModulo");
            this.wrapSelectionButton = (Button) base.GetTemplateChild("wrapSelectionButton");
            this.buttonEqual = (Button) base.GetTemplateChild("buttonEqual");
            this.buttonNotEqual = (Button) base.GetTemplateChild("buttonNotEqual");
            this.buttonLess = (Button) base.GetTemplateChild("buttonLess");
            this.buttonLessOrEqual = (Button) base.GetTemplateChild("buttonLessOrEqual");
            this.buttonLargerOrEqual = (Button) base.GetTemplateChild("buttonLargerOrEqual");
            this.buttonLarger = (Button) base.GetTemplateChild("buttonLarger");
            this.buttonAnd = (Button) base.GetTemplateChild("buttonAnd");
            this.buttonOr = (Button) base.GetTemplateChild("buttonOr");
            this.buttonNot = (Button) base.GetTemplateChild("buttonNot");
            this.listOfInputTypes = (ListBox) base.GetTemplateChild("listOfInputTypes");
            this.functionsTypes = (ComboBoxEdit) base.GetTemplateChild("functionsTypes");
            this.listOfInputParameters = (DXListBox) base.GetTemplateChild("listOfInputParameters");
            this.descriptionEdit = (TextEdit) base.GetTemplateChild("descriptionEdit");
            if (this.listOfInputParameters != null)
            {
                this.listOfInputParameters.ItemLeftButtonDoubleClick += new EventHandler(this.listOfInputParameters_MouseDoubleClick);
                this.listOfInputParameters.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listOfInputParameters_SelectionChanged);
            }
            if (this.wrapSelectionButton != null)
            {
                this.wrapSelectionButton.Click += new RoutedEventHandler(this.wrapSelectionButton_Click);
            }
            if (this.listOfInputTypes != null)
            {
                this.listOfInputTypes.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listOfInputTypes_SelectionChanged);
            }
            if (this.functionsTypes != null)
            {
                this.functionsTypes.SelectedIndexChanged += new RoutedEventHandler(this.functionsTypes_SelectedIndexChanged);
            }
            this.expressionTextEdit.SelectAllOnGotFocus = false;
            this.InitializeStandardOperationButtons();
            this.ExpressionMemoEdit = new MemoEditWrapper(this.expressionTextEdit);
            this.ListOfInputTypes = new ListBoxControlWrappper(this.listOfInputTypes);
            this.ListOfInputParameters = new ListBoxControlWrappper(this.listOfInputParameters);
            this.FunctionsTypes = new ComboBoxEditWrappper(this.functionsTypes);
            this.fEditorLogic = this.GetExpressionEditorLogic();
            this.fEditorLogic.Initialize();
            this.fEditorLogic.OnLoad();
        }

        private static void SetButtonImage(Button button, string imageName)
        {
            if (imageName != "Plus")
            {
                Image image1 = new Image();
                image1.Source = new BitmapImage(DevExpress.Xpf.Core.UriHelper.GetUri("DevExpress.Xpf.Core", "/Editors/Images/ExpressionEditor/" + imageName + ".png", null));
                button.Content = image1;
            }
        }

        private void UnsubscribeClickEvent(Button button)
        {
            if (button != null)
            {
                button.Click -= new RoutedEventHandler(this.insertOperationButton_Click);
            }
        }

        private void UnsubscribeStandardOperationButtons()
        {
            this.UnsubscribeClickEvent(this.buttonPlus);
            this.UnsubscribeClickEvent(this.buttonMinus);
            this.UnsubscribeClickEvent(this.buttonMultiply);
            this.UnsubscribeClickEvent(this.buttonDivide);
            this.UnsubscribeClickEvent(this.buttonModulo);
            this.UnsubscribeClickEvent(this.buttonEqual);
            this.UnsubscribeClickEvent(this.buttonNotEqual);
            this.UnsubscribeClickEvent(this.buttonLess);
            this.UnsubscribeClickEvent(this.buttonLessOrEqual);
            this.UnsubscribeClickEvent(this.buttonLargerOrEqual);
            this.UnsubscribeClickEvent(this.buttonLarger);
            this.UnsubscribeClickEvent(this.buttonAnd);
            this.UnsubscribeClickEvent(this.buttonOr);
            this.UnsubscribeClickEvent(this.buttonNot);
        }

        private void wrapSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            this.fEditorLogic.OnWrapExpression();
        }

        public string Expression =>
            this.fEditorLogic.GetExpression();

        protected IDataColumnInfo ColumnInfo { get; set; }

        ExpressionEditorLogic IExpressionEditor.EditorLogic =>
            this.fEditorLogic;

        IMemoEdit IExpressionEditor.ExpressionMemoEdit =>
            this.ExpressionMemoEdit;

        ISelector IExpressionEditor.ListOfInputParameters =>
            this.ListOfInputParameters;

        ISelector IExpressionEditor.ListOfInputTypes =>
            this.ListOfInputTypes;

        ISelector IExpressionEditor.FunctionsTypes =>
            this.FunctionsTypes;

        string IExpressionEditor.FilterCriteriaInvalidExpressionExMessage =>
            EditorLocalizer.GetString(EditorStringId.FilterCriteriaInvalidExpressionEx);

        string IExpressionEditor.FilterCriteriaInvalidExpressionMessage =>
            EditorLocalizer.GetString(EditorStringId.FilterCriteriaInvalidExpression);
    }
}

