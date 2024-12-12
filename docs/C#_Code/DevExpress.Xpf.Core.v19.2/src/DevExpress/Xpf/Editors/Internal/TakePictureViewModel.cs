namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public class TakePictureViewModel : ViewModelBase
    {
        public TakePictureViewModel(TakePictureControl owner)
        {
            this.Owner = owner;
            this.CaptureCaption = EditorLocalizer.GetString(EditorStringId.CameraCaptureButtonCaption);
            this.SaveCaption = EditorLocalizer.GetString(EditorStringId.Save);
            this.CancelCaption = EditorLocalizer.GetString(EditorStringId.Cancel);
            this.CaptureCommand = new DelegateCommand(() => this.Capture());
            this.SaveCommand = new DelegateCommand(() => this.Owner.Save(this.Image));
            this.CancelCommand = new DelegateCommand(() => this.Owner.Cancel());
        }

        private void Capture()
        {
            if (this.HasCapture)
            {
                this.Image = null;
            }
            else
            {
                this.Image = this.Owner.Capture();
            }
        }

        private TakePictureControl Owner { get; set; }

        public string CaptureCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_CaptureCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_CaptureCaption)), new ParameterExpression[0]), value);
        }

        public string SaveCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_SaveCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_SaveCaption)), new ParameterExpression[0]), value);
        }

        public string CancelCaption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_CancelCaption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_CancelCaption)), new ParameterExpression[0]), value);
        }

        public bool HasCapture
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_HasCapture)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_HasCapture)), new ParameterExpression[0]), value);
        }

        public ImageSource Image
        {
            get => 
                base.GetProperty<ImageSource>(Expression.Lambda<Func<ImageSource>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_Image)), new ParameterExpression[0]));
            set
            {
                base.SetProperty<ImageSource>(Expression.Lambda<Func<ImageSource>>(Expression.Property(Expression.Constant(this, typeof(TakePictureViewModel)), (MethodInfo) methodof(TakePictureViewModel.get_Image)), new ParameterExpression[0]), value);
                this.HasCapture = value != null;
                this.CaptureCaption = this.HasCapture ? EditorLocalizer.GetString(EditorStringId.CameraAgainButtonCaption) : EditorLocalizer.GetString(EditorStringId.CameraCaptureButtonCaption);
            }
        }

        public ICommand CaptureCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }
    }
}

