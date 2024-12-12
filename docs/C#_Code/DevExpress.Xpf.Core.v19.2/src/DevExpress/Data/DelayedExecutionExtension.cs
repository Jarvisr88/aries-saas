namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public static class DelayedExecutionExtension
    {
        private static readonly Dictionary<object, DispatcherTimer> ElementDictionary = new Dictionary<object, DispatcherTimer>();
        private static readonly Dictionary<object, List<DispatcherTimer>> ElementDictionaryList = new Dictionary<object, List<DispatcherTimer>>();

        private static void AddNewTimer(object scroll, int milliseconds, Action method)
        {
            DispatcherTimer item = CreateNewTimer(method, scroll, 0);
            item.Interval = TimeSpan.FromMilliseconds((double) milliseconds);
            Dictionary<object, List<DispatcherTimer>> elementDictionaryList = ElementDictionaryList;
            lock (elementDictionaryList)
            {
                ElementDictionaryList[scroll].Add(item);
            }
        }

        private static DispatcherTimer CreateNewTimer(Action method, object elem, int milliseconds)
        {
            DispatcherTimer timer = new DispatcherTimer();
            if (milliseconds > 0)
            {
                timer.Interval = TimeSpan.FromMilliseconds((double) milliseconds);
            }
            timer.Tick += delegate (object d, EventArgs e) {
                method();
                timer.Stop();
                Dictionary<object, DispatcherTimer> elementDictionary = ElementDictionary;
                lock (elementDictionary)
                {
                    ElementDictionary.Remove(elem);
                }
                RemoveTimerFromElementDictionaryList(elem, timer);
            };
            timer.Start();
            return timer;
        }

        public static void DelayedExecute(this object scroll, Action method)
        {
            scroll.DelayedExecute(0, method);
        }

        public static void DelayedExecute(this object scroll, int milliseconds, Action method)
        {
            Dictionary<object, DispatcherTimer> elementDictionary = ElementDictionary;
            lock (elementDictionary)
            {
                if (!ElementDictionary.ContainsKey(scroll))
                {
                    ElementDictionary.Add(scroll, CreateNewTimer(method, scroll, milliseconds));
                }
                else
                {
                    DispatcherTimer timer = ElementDictionary[scroll];
                    timer.Stop();
                    RemoveTimerFromElementDictionaryList(scroll, timer);
                    ElementDictionary[scroll] = CreateNewTimer(method, scroll, milliseconds);
                }
            }
        }

        public static void DelayedExecuteEnqueue(this object scroll, Action method)
        {
            scroll.DelayedExecuteEnqueue(0, method);
        }

        public static void DelayedExecuteEnqueue(this object scroll, int milliseconds, Action method)
        {
            Dictionary<object, DispatcherTimer> elementDictionary = ElementDictionary;
            lock (elementDictionary)
            {
                Dictionary<object, List<DispatcherTimer>> elementDictionaryList = ElementDictionaryList;
                lock (elementDictionaryList)
                {
                    if (ElementDictionary.ContainsKey(scroll))
                    {
                        if (ElementDictionaryList.ContainsKey(scroll))
                        {
                            AddNewTimer(scroll, milliseconds, method);
                        }
                        else
                        {
                            Dictionary<object, DispatcherTimer> dictionary3 = ElementDictionary;
                            lock (dictionary3)
                            {
                                List<DispatcherTimer> list1 = new List<DispatcherTimer>();
                                list1.Add(ElementDictionary[scroll]);
                                ElementDictionaryList.Add(scroll, list1);
                            }
                            AddNewTimer(scroll, milliseconds, method);
                        }
                        return;
                    }
                }
            }
            scroll.DelayedExecute(milliseconds, method);
        }

        public static void RemoveDelayedExecute(this object scroll)
        {
            Dictionary<object, DispatcherTimer> elementDictionary = ElementDictionary;
            lock (elementDictionary)
            {
                DispatcherTimer timer;
                if (ElementDictionary.TryGetValue(scroll, out timer))
                {
                    timer.Stop();
                    ElementDictionary.Remove(scroll);
                }
            }
            RemoveDelayedExecuteQueue(scroll);
        }

        private static void RemoveDelayedExecuteQueue(object scroll)
        {
            Dictionary<object, List<DispatcherTimer>> elementDictionaryList = ElementDictionaryList;
            lock (elementDictionaryList)
            {
                List<DispatcherTimer> list;
                if (ElementDictionaryList.TryGetValue(scroll, out list))
                {
                    foreach (DispatcherTimer timer in list)
                    {
                        timer.Stop();
                    }
                    ElementDictionaryList.Remove(scroll);
                }
            }
        }

        private static void RemoveTimerFromElementDictionaryList(object elem, DispatcherTimer timer)
        {
            Dictionary<object, List<DispatcherTimer>> elementDictionaryList = ElementDictionaryList;
            lock (elementDictionaryList)
            {
                if (ElementDictionaryList.ContainsKey(elem))
                {
                    ElementDictionaryList[elem].Remove(timer);
                    if (ElementDictionaryList[elem].Count == 0)
                    {
                        ElementDictionaryList.Remove(elem);
                    }
                }
            }
        }
    }
}

