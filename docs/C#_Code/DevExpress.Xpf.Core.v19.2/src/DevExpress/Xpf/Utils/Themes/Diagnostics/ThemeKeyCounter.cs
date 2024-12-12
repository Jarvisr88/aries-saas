namespace DevExpress.Xpf.Utils.Themes.Diagnostics
{
    using System;
    using System.Collections.Generic;

    public class ThemeKeyCounter
    {
        protected static Dictionary<Type, int> instancesStorage;

        static ThemeKeyCounter()
        {
            CreateInstancesStorage();
        }

        public static int ChangeRefCounter(object obj, RefCounterAction action)
        {
            Type key = obj.GetType();
            int num = 0;
            if (!Info.ContainsKey(key))
            {
                if (action != RefCounterAction.GetValue)
                {
                    Info.Add(key, 1);
                }
            }
            else
            {
                int num2 = 0;
                if (action == RefCounterAction.Increase)
                {
                    num2 = 1;
                }
                else if (action == RefCounterAction.Decrease)
                {
                    num2 = -1;
                }
                num = Info[key] + num2;
                if (num < 0)
                {
                    num = 0;
                }
                Info[key] = num;
            }
            return num;
        }

        private static void CreateInstancesStorage()
        {
            instancesStorage = new Dictionary<Type, int>();
        }

        public static void Reset()
        {
            CreateInstancesStorage();
        }

        public static Dictionary<Type, int> Info =>
            instancesStorage;
    }
}

