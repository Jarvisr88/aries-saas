namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    public class ScaleWindowViewModel : INotifyPropertyChanged
    {
        private bool isInputValid = true;
        private readonly int currentScaleFactor;
        private int pagesToFit;
        private int scaleFactor;
        private DevExpress.Xpf.Printing.ScaleMode scaleMode;
        private readonly DelegateCommand<object> applyCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ScaleWindowViewModelEventArgs> ScaleApplied;

        public ScaleWindowViewModel(float scaleFactor, int pagesToFit)
        {
            this.applyCommand = DelegateCommandFactory.Create<object>(new Action<object>(this.Apply), new Func<object, bool>(this.CanApply), false);
            int[] collection = new int[] { 10, 0x19, 50, 100, 200, 300, 500, 700, 0x3e8 };
            this.ScaleFactorValues = new List<int>(collection);
            int[] numArray2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.PagesToFitValues = new List<int>(numArray2);
            bool flag = pagesToFit == 0;
            this.pagesToFit = flag ? 1 : pagesToFit;
            this.scaleFactor = (int) Math.Round((double) (scaleFactor * 100f));
            this.currentScaleFactor = this.scaleFactor;
        }

        private void Apply(object parameter)
        {
            if (this.ScaleApplied != null)
            {
                this.ScaleApplied(this, new ScaleWindowViewModelEventArgs(this.ScaleMode, ToFloatFactor(this.ScaleFactor), this.PagesToFit));
            }
        }

        private bool CanApply(object parameter)
        {
            if (!this.IsInputValid)
            {
                return false;
            }
            if (this.ScaleMode == DevExpress.Xpf.Printing.ScaleMode.AdjustToPercent)
            {
                return (this.currentScaleFactor != this.ScaleFactor);
            }
            if (this.ScaleMode != DevExpress.Xpf.Printing.ScaleMode.FitToPageWidth)
            {
                throw new NotSupportedException();
            }
            return true;
        }

        private void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            this.RaisePropertyChanged<T>(this.PropertyChanged, property);
        }

        private static float ToFloatFactor(int value) => 
            Convert.ToSingle(value) / 100f;

        private ValidationResult Validate(object validatingValue, int minValue, int maxValue)
        {
            if (validatingValue == null)
            {
                this.IsInputValid = false;
                return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error));
            }
            int result = 0;
            string str = validatingValue as string;
            if (!string.IsNullOrEmpty(str))
            {
                if (!int.TryParse(str, out result))
                {
                    this.IsInputValid = false;
                    return new ValidationResult(false, PrintingLocalizer.GetString(PrintingStringId.Scaling_ComboBoxEdit_Validation_Error));
                }
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(validatingValue);
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
            this.Validate(value, MinPagesToFit, MaxPagesToFit);

        public ValidationResult ValidateScaleFactor(object value) => 
            this.Validate(value, MinScaleFactor, MaxScaleFactor);

        private static int MinPagesToFit =>
            1;

        private static int MaxPagesToFit =>
            10;

        private static int MinScaleFactor =>
            1;

        private static int MaxScaleFactor =>
            0x3e8;

        public List<int> ScaleFactorValues { get; private set; }

        public List<int> PagesToFitValues { get; private set; }

        public DevExpress.Xpf.Printing.ScaleMode ScaleMode
        {
            get => 
                this.scaleMode;
            set
            {
                this.scaleMode = value;
                this.RaisePropertyChanged<DevExpress.Xpf.Printing.ScaleMode>(Expression.Lambda<Func<DevExpress.Xpf.Printing.ScaleMode>>(Expression.Property(Expression.Constant(this, typeof(ScaleWindowViewModel)), (MethodInfo) methodof(ScaleWindowViewModel.get_ScaleMode)), new ParameterExpression[0]));
                this.applyCommand.RaiseCanExecuteChanged();
            }
        }

        public int ScaleFactor
        {
            get => 
                this.scaleFactor;
            set
            {
                if ((value >= MinScaleFactor) && (value <= MaxScaleFactor))
                {
                    this.scaleFactor = value;
                }
                this.RaisePropertyChanged<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(ScaleWindowViewModel)), (MethodInfo) methodof(ScaleWindowViewModel.get_ScaleFactor)), new ParameterExpression[0]));
                this.applyCommand.RaiseCanExecuteChanged();
            }
        }

        public int PagesToFit
        {
            get => 
                this.pagesToFit;
            set
            {
                if ((value >= MinPagesToFit) && (value <= MaxPagesToFit))
                {
                    this.pagesToFit = value;
                }
                this.RaisePropertyChanged<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(ScaleWindowViewModel)), (MethodInfo) methodof(ScaleWindowViewModel.get_PagesToFit)), new ParameterExpression[0]));
                this.applyCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand ApplyCommand =>
            this.applyCommand;

        private bool IsInputValid
        {
            get => 
                this.isInputValid;
            set
            {
                if (this.isInputValid != value)
                {
                    this.isInputValid = value;
                }
                this.applyCommand.RaiseCanExecuteChanged();
            }
        }
    }
}

