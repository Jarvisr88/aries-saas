namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class FormatEditorIconViewModel : FormatEditorItemViewModel
    {
        private Dictionary<string, FormatEditorIconItemViewModel> iconCache;

        protected FormatEditorIconViewModel(IDialogContext column) : base(column)
        {
            this.iconCache = new Dictionary<string, FormatEditorIconItemViewModel>();
            this.CreateGroups();
        }

        private bool CheckIsNewAndCache(FormatEditorIconItemViewModel item)
        {
            ImageSource icon = item.Icon;
            if (icon == null)
            {
                return false;
            }
            string path = this.GetPath(icon);
            if (path != null)
            {
                if (this.iconCache.ContainsKey(path))
                {
                    return false;
                }
                this.iconCache.Add(path, item);
            }
            return true;
        }

        public override void Clear()
        {
            this.IconItem = null;
        }

        private void CreateGroups()
        {
            this.iconCache.Clear();
            Func<FormatInfo, string> keySelector = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<FormatInfo, string> local1 = <>c.<>9__12_0;
                keySelector = <>c.<>9__12_0 = x => x.GroupName;
            }
            Func<FormatEditorIconGroup, bool> predicate = <>c.<>9__12_2;
            if (<>c.<>9__12_2 == null)
            {
                Func<FormatEditorIconGroup, bool> local2 = <>c.<>9__12_2;
                predicate = <>c.<>9__12_2 = x => x.Icons.Length != 0;
            }
            IEnumerable<FormatEditorIconGroup> collection = (from x in base.Context.PredefinedFormatsOwner.PredefinedIconSetFormats.GroupBy<FormatInfo, string>(keySelector) select new FormatEditorIconGroup(x.Key, this.ExtractIcons(x))).Where<FormatEditorIconGroup>(predicate);
            this.Groups = new ObservableCollection<FormatEditorIconGroup>(collection);
        }

        private FormatEditorIconItemViewModel[] ExtractIcons(IEnumerable<FormatInfo> formatInfo)
        {
            Func<FormatInfo, IconSetFormat> selector = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<FormatInfo, IconSetFormat> local1 = <>c.<>9__13_0;
                selector = <>c.<>9__13_0 = x => x.Format as IconSetFormat;
            }
            return this.ExtractIcons(formatInfo.Select<FormatInfo, IconSetFormat>(selector));
        }

        private FormatEditorIconItemViewModel[] ExtractIcons(IEnumerable<IconSetFormat> format)
        {
            Func<IconSetFormat, bool> predicate = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<IconSetFormat, bool> local1 = <>c.<>9__14_0;
                predicate = <>c.<>9__14_0 = x => x != null;
            }
            Func<IconSetFormat, IEnumerable<IconSetElement>> selector = <>c.<>9__14_1;
            if (<>c.<>9__14_1 == null)
            {
                Func<IconSetFormat, IEnumerable<IconSetElement>> local2 = <>c.<>9__14_1;
                selector = <>c.<>9__14_1 = x => x.Elements;
            }
            Func<IconSetElement, FormatEditorIconItemViewModel> func3 = <>c.<>9__14_2;
            if (<>c.<>9__14_2 == null)
            {
                Func<IconSetElement, FormatEditorIconItemViewModel> local3 = <>c.<>9__14_2;
                func3 = <>c.<>9__14_2 = e => FormatEditorIconItemViewModel.Factory(e.Icon);
            }
            return (from i in format.Where<IconSetFormat>(predicate).SelectMany<IconSetFormat, IconSetElement>(selector).Select<IconSetElement, FormatEditorIconItemViewModel>(func3)
                where this.CheckIsNewAndCache(i)
                select i).ToArray<FormatEditorIconItemViewModel>();
        }

        private string GetPath(ImageSource source)
        {
            BitmapImage image = source as BitmapImage;
            if ((image == null) || (image.UriSource == null))
            {
                return null;
            }
            string absolutePath = image.UriSource.AbsolutePath;
            return (string.IsNullOrEmpty(absolutePath) ? null : absolutePath);
        }

        public override void InitFromFormat(Format format)
        {
            if (format.Icon == null)
            {
                this.Clear();
            }
            else
            {
                string path = this.GetPath(format.Icon);
                FormatEditorIconItemViewModel model = null;
                if (path != null)
                {
                    this.iconCache.TryGetValue(path, out model);
                }
                if (model == null)
                {
                    model = FormatEditorIconItemViewModel.Factory(format.Icon);
                    FormatEditorIconItemViewModel[] icons = new FormatEditorIconItemViewModel[] { model };
                    FormatEditorIconGroup item = new FormatEditorIconGroup(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_CustomIconGroup), icons);
                    this.Groups.Insert(0, item);
                }
                if (this.IconItem != null)
                {
                    this.IconItem.IsChecked = false;
                }
                this.IconItem = model;
                this.IconItem.IsChecked = true;
            }
        }

        public override void SetFormatProperties(Format format)
        {
            Func<FormatEditorIconItemViewModel, ImageSource> evaluator = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<FormatEditorIconItemViewModel, ImageSource> local1 = <>c.<>9__19_0;
                evaluator = <>c.<>9__19_0 = x => x.Icon;
            }
            ManagerHelperBase.SetProperty(format, Format.IconProperty, this.IconItem.With<FormatEditorIconItemViewModel, ImageSource>(evaluator));
        }

        public static Func<IDialogContext, FormatEditorIconViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, FormatEditorIconViewModel>(Expression.Lambda<Func<IDialogContext, FormatEditorIconViewModel>>(Expression.New((ConstructorInfo) methodof(FormatEditorIconViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="OnChanged")]
        public virtual FormatEditorIconItemViewModel IconItem { get; set; }

        public ObservableCollection<FormatEditorIconGroup> Groups { get; private set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Icon);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatEditorIconViewModel.<>c <>9 = new FormatEditorIconViewModel.<>c();
            public static Func<FormatInfo, string> <>9__12_0;
            public static Func<FormatEditorIconGroup, bool> <>9__12_2;
            public static Func<FormatInfo, IconSetFormat> <>9__13_0;
            public static Func<IconSetFormat, bool> <>9__14_0;
            public static Func<IconSetFormat, IEnumerable<IconSetElement>> <>9__14_1;
            public static Func<IconSetElement, FormatEditorIconItemViewModel> <>9__14_2;
            public static Func<FormatEditorIconItemViewModel, ImageSource> <>9__19_0;

            internal string <CreateGroups>b__12_0(FormatInfo x) => 
                x.GroupName;

            internal bool <CreateGroups>b__12_2(FormatEditorIconGroup x) => 
                x.Icons.Length != 0;

            internal IconSetFormat <ExtractIcons>b__13_0(FormatInfo x) => 
                x.Format as IconSetFormat;

            internal bool <ExtractIcons>b__14_0(IconSetFormat x) => 
                x != null;

            internal IEnumerable<IconSetElement> <ExtractIcons>b__14_1(IconSetFormat x) => 
                x.Elements;

            internal FormatEditorIconItemViewModel <ExtractIcons>b__14_2(IconSetElement e) => 
                FormatEditorIconItemViewModel.Factory(e.Icon);

            internal ImageSource <SetFormatProperties>b__19_0(FormatEditorIconItemViewModel x) => 
                x.Icon;
        }
    }
}

