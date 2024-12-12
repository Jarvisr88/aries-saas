namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;

    internal static class DefaultSpanHelper
    {
        private static Dictionary<Type, int> defaultColumnSpans = CreateDefaultColumnSpans();
        private static Dictionary<Type, int> defaultRowSpans = CreateDefaultRowSpans();
        private const int AllColumnsSpan = -2147483648;

        public static int CalcDefaultColumnSpan(EditFormColumnSource source, int columnCount)
        {
            int defaultSpan = GetDefaultSpan(source, defaultColumnSpans);
            if (defaultSpan == -2147483648)
            {
                defaultSpan = columnCount;
            }
            return Math.Min(columnCount, defaultSpan);
        }

        public static int CalcDefaultRowSpan(EditFormColumnSource source) => 
            GetDefaultSpan(source, defaultRowSpans);

        private static Dictionary<Type, int> CreateDefaultColumnSpans() => 
            new Dictionary<Type, int> { 
                { 
                    typeof(MemoEditSettings),
                    -2147483648
                },
                { 
                    typeof(ImageEditSettings),
                    2
                }
            };

        private static Dictionary<Type, int> CreateDefaultRowSpans() => 
            new Dictionary<Type, int> { 
                { 
                    typeof(MemoEditSettings),
                    3
                },
                { 
                    typeof(ImageEditSettings),
                    3
                }
            };

        private static int GetDefaultSpan(EditFormColumnSource source, Dictionary<Type, int> registeredSpans)
        {
            Type key = null;
            if ((source != null) && (source.EditSettings != null))
            {
                key = source.EditSettings.GetType();
            }
            int num = 1;
            if (key != null)
            {
                registeredSpans.TryGetValue(key, out num);
            }
            return num;
        }
    }
}

