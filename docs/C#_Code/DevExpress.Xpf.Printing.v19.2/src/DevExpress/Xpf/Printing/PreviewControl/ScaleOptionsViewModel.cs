namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class ScaleOptionsViewModel : PreviewSettingsViewModelBase, IDataErrorInfo
    {
        private const string scaleFactorPropertyName = "ScaleFactor";
        private const string pagesToFitPropertyName = "PagesToFit";
        private bool isInputValid = true;
        private readonly double currentScaleFactor;
        private int pagesToFit;
        private float scaleFactor;
        private DevExpress.Xpf.Printing.ScaleMode scaleMode;

        private ScaleOptionsViewModel(PrintingSystemBase printingSystem)
        {
            int[] collection = new int[] { 10, 0x19, 50, 100, 200, 300, 500, 700, 0x3e8 };
            this.ScaleFactorValues = new List<int>(collection);
            int[] numArray2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.PagesToFitValues = new List<int>(numArray2);
            bool flag = printingSystem.Document.AutoFitToPagesWidth == 0;
            this.pagesToFit = flag ? 1 : printingSystem.Document.AutoFitToPagesWidth;
            this.scaleFactor = printingSystem.Document.ScaleFactor;
            this.currentScaleFactor = this.scaleFactor;
        }

        public static ScaleOptionsViewModel Create(PrintingSystemBase printingSystem) => 
            new ScaleOptionsViewModel(printingSystem);

        private bool Validate() => 
            (this.ScaleMode != DevExpress.Xpf.Printing.ScaleMode.AdjustToPercent) ? this.ValidatePagesToFit(this.PagesToFit).IsValid : (this.ValidateScaleFactor(this.ScaleFactor).IsValid && (this.currentScaleFactor != this.ScaleFactor));

        private string Validate(string columnName)
        {
            ValidationResult result = null;
            if (columnName == "ScaleFactor")
            {
                result = this.ValidateScaleFactor(this.ScaleFactor);
            }
            else if (columnName == "PagesToFit")
            {
                result = this.ValidatePagesToFit(this.PagesToFit);
            }
            return result?.ErrorMessage;
        }

        private ValidationResult Validate(object validatingValue, double minValue, double maxValue)
        {
            if (validatingValue == null)
            {
                this.IsInputValid = false;
                return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error));
            }
            double result = 0.0;
            string str = validatingValue as string;
            if (!string.IsNullOrEmpty(str))
            {
                if (!double.TryParse(str, out result))
                {
                    this.IsInputValid = false;
                    return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error));
                }
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(validatingValue);
                }
                catch
                {
                    this.IsInputValid = false;
                    return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error));
                }
            }
            if ((result >= minValue) && (result <= maxValue))
            {
                this.IsInputValid = true;
                return new ValidationResult(true);
            }
            this.IsInputValid = false;
            return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_OutOfRange_Error));
        }

        public ValidationResult ValidatePagesToFit(object value) => 
            this.Validate(value, (double) MinPagesToFit, (double) MaxPagesToFit);

        public ValidationResult ValidateScaleFactor(object value) => 
            this.Validate(value, (double) MinScaleFactor, (double) MaxScaleFactor);

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public override DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType SettingsType =>
            DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType.Scale;

        private static int MinPagesToFit =>
            1;

        private static int MaxPagesToFit =>
            10;

        private static float MinScaleFactor =>
            0.01f;

        private static float MaxScaleFactor =>
            10f;

        public List<int> ScaleFactorValues { get; private set; }

        public List<int> PagesToFitValues { get; private set; }

        public DevExpress.Xpf.Printing.ScaleMode ScaleMode
        {
            get => 
                this.scaleMode;
            set
            {
                base.SetProperty<DevExpress.Xpf.Printing.ScaleMode>(ref this.scaleMode, value, Expression.Lambda<Func<DevExpress.Xpf.Printing.ScaleMode>>(Expression.Property(Expression.Constant(this, typeof(ScaleOptionsViewModel)), (MethodInfo) methodof(ScaleOptionsViewModel.get_ScaleMode)), new ParameterExpression[0]));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public float ScaleFactor
        {
            get => 
                this.scaleFactor;
            set
            {
                base.SetProperty<float>(ref this.scaleFactor, value, Expression.Lambda<Func<float>>(Expression.Property(Expression.Constant(this, typeof(ScaleOptionsViewModel)), (MethodInfo) methodof(ScaleOptionsViewModel.get_ScaleFactor)), new ParameterExpression[0]));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int PagesToFit
        {
            get => 
                this.pagesToFit;
            set
            {
                base.SetProperty<int>(ref this.pagesToFit, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(ScaleOptionsViewModel)), (MethodInfo) methodof(ScaleOptionsViewModel.get_PagesToFit)), new ParameterExpression[0]));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private bool IsInputValid
        {
            get => 
                this.isInputValid;
            set => 
                base.SetProperty<bool>(ref this.isInputValid, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(ScaleOptionsViewModel)), (MethodInfo) methodof(ScaleOptionsViewModel.get_IsInputValid)), new ParameterExpression[0]));
        }

        public bool CanApply =>
            this.Validate();

        public string Error =>
            null;

        string IDataErrorInfo.this[string columnName] =>
            this.Validate(columnName);
    }
}

