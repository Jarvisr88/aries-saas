namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Data.Helpers;
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public static class ViewModelMetadataSource
    {
        internal const string PackPrefix = "pack://application:,,,";
        public const string ImagesImagePrefix = "pack://application:,,,/DevExpress.Images.v19.2;component/";
        private const string STR_Command = "Command";

        public static void ApplyCommandAttributes(this IEdmPropertyInfo propertyInfo, ICommandAttributesApplier applier, ImageType defaultImageType)
        {
            applier.SetPropertyName(propertyInfo.Name);
            string str = propertyInfo.HasDisplayAttribute() ? propertyInfo.DisplayName : GetCaptionFromCommandName(propertyInfo.Name);
            applier.SetCaption(propertyInfo.Attributes.ShortName ?? str);
            if (!string.IsNullOrEmpty(propertyInfo.Attributes.Description))
            {
                applier.SetHint(propertyInfo.Attributes.Description);
            }
            Tuple<string, string> tuple = EnumSourceHelperCore.GetImageInfo(propertyInfo.Attributes.Image(), propertyInfo.Attributes.DXImage(), GetImageNameFromCommandName(propertyInfo.Name), GetKnownImageCallback(defaultImageType));
            if (tuple.Item1 != null)
            {
                applier.SetImageUri(tuple.Item1);
            }
            if (tuple.Item2 != null)
            {
                applier.SetLargeImageUri(tuple.Item2);
            }
            if (!string.IsNullOrEmpty(propertyInfo.Attributes.CommandParameterName()))
            {
                applier.SetParameter(propertyInfo.Attributes.CommandParameterName());
            }
        }

        public static void GenerateMetadata(IEnumerable<IEdmPropertyInfo> properties, ICommandGroupsGenerator generator, ViewModelMetadataOptions options)
        {
            IAttributesProvider provider = options.AttributesProvider;
            if (options.LayoutType == LayoutType.ContextMenu)
            {
                Func<IEdmPropertyInfo, bool> predicate = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<IEdmPropertyInfo, bool> local1 = <>c.<>9__6_0;
                    predicate = <>c.<>9__6_0 = x => x.Attributes.IsContextMenuItem();
                }
                properties = properties.Where<IEdmPropertyInfo>(predicate).ToArray<IEdmPropertyInfo>();
            }
            Func<IEdmPropertyInfo, string> keySelector = <>c.<>9__6_2;
            if (<>c.<>9__6_2 == null)
            {
                Func<IEdmPropertyInfo, string> local2 = <>c.<>9__6_2;
                keySelector = <>c.<>9__6_2 = x => x.Attributes.ToolBarPageName();
            }
            foreach (IGrouping<string, IEdmPropertyInfo> grouping in (from x in EditorsGeneratorBase.GetFilteredAndSortedProperties(properties, options.Scaffolding, false, options.LayoutType) select (provider == null) ? ((IEnumerable<IEdmPropertyInfo>) x) : ((IEnumerable<IEdmPropertyInfo>) x.AddAttributes(provider.GetAttributes(x.Name)))).GroupBy<IEdmPropertyInfo, string>(keySelector))
            {
                Func<IEdmPropertyInfo, string> <>9__3;
                ICommandSubGroupsGenerator generator2 = generator.CreateGroup(grouping.Key);
                Func<IEdmPropertyInfo, string> func2 = <>9__3;
                if (<>9__3 == null)
                {
                    Func<IEdmPropertyInfo, string> local3 = <>9__3;
                    func2 = <>9__3 = x => x.Attributes.GetToolBarPageGroupName(options.LayoutType);
                }
                foreach (IGrouping<string, IEdmPropertyInfo> grouping2 in grouping.GroupBy<IEdmPropertyInfo, string>(func2))
                {
                    ICommandsGenerator generator3 = generator2.CreateSubGroup(grouping2.Key);
                    foreach (IEdmPropertyInfo info in grouping2)
                    {
                        ProcessProperty(info, generator3);
                    }
                }
            }
        }

        public static string GetCaptionFromCommandName(string commandName) => 
            DevExpress.Data.Helpers.SplitStringHelper.SplitPascalCaseString(GetImageNameFromCommandName(commandName));

        private static string GetImageNameFromCommandName(string commandName)
        {
            string str = commandName;
            if (str.EndsWith("Command") && (str != "Command"))
            {
                str = str.Remove(str.Length - "Command".Length, "Command".Length);
            }
            return str;
        }

        public static Func<string, bool, string> GetKnownImageCallback(ImageType defaultImageType) => 
            (image, large) => GetKnownImageUri(image, large ? ImageSize.Size32x32 : ImageSize.Size16x16, defaultImageType);

        public static string GetKnownImageUri(string name, ImageSize imageSize, ImageType imageType) => 
            DXImageHelper.GetFile(name, imageSize, imageType);

        public static IEnumerable<IEdmPropertyInfo> GetProperties(object viewModel) => 
            GetPropertiesCore(TypeDescriptor.GetProperties(viewModel), viewModel.GetType());

        public static IEnumerable<IEdmPropertyInfo> GetPropertiesByType(Type type) => 
            GetPropertiesCore(TypeDescriptor.GetProperties(type), type);

        private static IEnumerable<IEdmPropertyInfo> GetPropertiesCore(PropertyDescriptorCollection propertyDescriptors, Type type) => 
            ((IEntityProperties) new ReflectionEntityProperties(propertyDescriptors.Cast<PropertyDescriptor>(), type, true, null)).AllProperties;

        public static bool IsCommandProperty(Type propertyType) => 
            typeof(ICommand).IsAssignableFrom(propertyType);

        private static void ProcessProperty(IEdmPropertyInfo property, ICommandsGenerator generator)
        {
            if (IsCommandProperty(property.PropertyType))
            {
                generator.GenerateCommand(property);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelMetadataSource.<>c <>9 = new ViewModelMetadataSource.<>c();
            public static Func<IEdmPropertyInfo, bool> <>9__6_0;
            public static Func<IEdmPropertyInfo, string> <>9__6_2;

            internal bool <GenerateMetadata>b__6_0(IEdmPropertyInfo x) => 
                x.Attributes.IsContextMenuItem();

            internal string <GenerateMetadata>b__6_2(IEdmPropertyInfo x) => 
                x.Attributes.ToolBarPageName();
        }
    }
}

