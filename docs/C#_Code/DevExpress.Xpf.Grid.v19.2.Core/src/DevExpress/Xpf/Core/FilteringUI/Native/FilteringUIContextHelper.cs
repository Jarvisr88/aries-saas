namespace DevExpress.Xpf.Core.FilteringUI.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class FilteringUIContextHelper
    {
        private static readonly char[] Separators = new char[] { ';', ',', ' ' };
        private static readonly Tuple<Type, DependencyProperty[]>[] TrackingEditSettings = CreateTrackingEditSettings();

        private static Tuple<Type, DependencyProperty[]>[] CreateTrackingEditSettings()
        {
            DependencyProperty[] propertyArray1 = new DependencyProperty[] { BaseEditSettings.DisplayFormatProperty };
            Tuple<Type, DependencyProperty[]>[] tupleArray1 = new Tuple<Type, DependencyProperty[]>[4];
            tupleArray1[0] = new Tuple<Type, DependencyProperty[]>(typeof(BaseEditSettings), propertyArray1);
            DependencyProperty[] propertyArray2 = new DependencyProperty[] { TextEditSettings.MaskProperty, TextEditSettings.MaskUseAsDisplayFormatProperty };
            tupleArray1[1] = new Tuple<Type, DependencyProperty[]>(typeof(TextEditSettings), propertyArray2);
            DependencyProperty[] propertyArray3 = new DependencyProperty[] { LookUpEditSettingsBase.ItemTemplateProperty, LookUpEditSettingsBase.ItemTemplateSelectorProperty, LookUpEditSettingsBase.ItemsSourceProperty, LookUpEditSettingsBase.ValueMemberProperty, LookUpEditSettingsBase.DisplayMemberProperty, LookUpEditSettingsBase.ApplyItemTemplateToSelectedItemProperty };
            tupleArray1[2] = new Tuple<Type, DependencyProperty[]>(typeof(LookUpEditSettingsBase), propertyArray3);
            DependencyProperty[] propertyArray4 = new DependencyProperty[] { ListBoxEditSettings.ItemTemplateProperty, ListBoxEditSettings.ItemTemplateSelectorProperty, ListBoxEditSettings.ItemsSourceProperty, ListBoxEditSettings.ValueMemberProperty, ListBoxEditSettings.DisplayMemberProperty };
            tupleArray1[3] = new Tuple<Type, DependencyProperty[]>(typeof(ListBoxEditSettings), propertyArray4);
            return tupleArray1;
        }

        public static string[] SplitGroupFields(string fieldName, string groupFields, bool skipRootIfInverted)
        {
            string[] localArray1;
            if (groupFields == null)
            {
                localArray1 = null;
            }
            else
            {
                Func<string, bool> predicate = <>c.<>9__1_0;
                if (<>c.<>9__1_0 == null)
                {
                    Func<string, bool> local1 = <>c.<>9__1_0;
                    predicate = <>c.<>9__1_0 = x => x != string.Empty;
                }
                localArray1 = groupFields.Split(Separators).Where<string>(predicate).ToArray<string>();
            }
            string[] local2 = localArray1;
            string[] instance = local2;
            if (local2 == null)
            {
                string[] local3 = local2;
                instance = EmptyArray<string>.Instance;
            }
            string[] array = instance;
            if (Array.IndexOf<string>(array, fieldName) != -1)
            {
                return (skipRootIfInverted ? array.Skip<string>(1).ToArray<string>() : array);
            }
            if (skipRootIfInverted)
            {
                return array;
            }
            string[] first = new string[] { fieldName };
            return first.Concat<string>(array).ToArray<string>();
        }

        public static UnsubscribeAction SubscribeSettings(BaseEditSettings editSettings, EventHandler action)
        {
            if (editSettings == null)
            {
                return null;
            }
            Func<Tuple<Type, DependencyProperty[]>, IEnumerable<DependencyProperty>> selector = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<Tuple<Type, DependencyProperty[]>, IEnumerable<DependencyProperty>> local1 = <>c.<>9__5_1;
                selector = <>c.<>9__5_1 = (Func<Tuple<Type, DependencyProperty[]>, IEnumerable<DependencyProperty>>) (info => info.Item2);
            }
            List<PropertyChangeTracker> trackingProperties = (from prop in (from info in TrackingEditSettings
                where info.Item1.IsInstanceOfType(editSettings)
                select info).SelectMany<Tuple<Type, DependencyProperty[]>, DependencyProperty>(selector) select new PropertyChangeTracker(editSettings, prop)).ToList<PropertyChangeTracker>();
            trackingProperties.ForEach(delegate (PropertyChangeTracker x) {
                x.Changed += action;
            });
            return delegate {
                Action<PropertyChangeTracker> <>9__5;
                Action<PropertyChangeTracker> action2 = <>9__5;
                if (<>9__5 == null)
                {
                    Action<PropertyChangeTracker> local1 = <>9__5;
                    action2 = <>9__5 = delegate (PropertyChangeTracker x) {
                        x.Changed -= action;
                    };
                }
                trackingProperties.ForEach(action2);
            };
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteringUIContextHelper.<>c <>9 = new FilteringUIContextHelper.<>c();
            public static Func<string, bool> <>9__1_0;
            public static Func<Tuple<Type, DependencyProperty[]>, IEnumerable<DependencyProperty>> <>9__5_1;

            internal bool <SplitGroupFields>b__1_0(string x) => 
                x != string.Empty;

            internal IEnumerable<DependencyProperty> <SubscribeSettings>b__5_1(Tuple<Type, DependencyProperty[]> info) => 
                info.Item2;
        }
    }
}

