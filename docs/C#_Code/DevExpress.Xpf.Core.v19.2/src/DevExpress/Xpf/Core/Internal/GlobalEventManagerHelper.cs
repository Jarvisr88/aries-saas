namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class GlobalEventManagerHelper
    {
        static GlobalEventManagerHelper()
        {
            WDependencyObjectType = typeof(DependencyObjectType).Wrap<IDependencyObjectTypeStatic>();
            WGlobalEventManager = typeof(EventManager).Assembly.GetType("System.Windows.GlobalEventManager").Wrap<IGlobalEventManagerStatic>();
        }

        public static void RegisterClassHandler(Type classType, RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
        {
            IClassHandlersStore store;
            int num;
            DependencyObjectType dType = WDependencyObjectType.FromSystemTypeInternal(classType);
            WGlobalEventManager.GetDTypedClassListeners(dType, routedEvent, out store, out num);
            object synchronized = WGlobalEventManager.Synchronized;
            lock (synchronized)
            {
                IRoutedEventHandlerInfoList existingHandlers = store.AddToExistingHandlers(num, handler, handledEventsToo);
                IItemStructList<DependencyObjectType> activeDTypes = WGlobalEventManager.DTypedClassListeners.ActiveDTypes;
                for (int i = 0; i < activeDTypes.Count; i++)
                {
                    if (activeDTypes.List[i].IsSubclassOf(dType))
                    {
                        WGlobalEventManager.DTypedClassListeners[activeDTypes.List[i]].Wrap<IClassHandlersStore>().UpdateSubClassHandlers(routedEvent, existingHandlers);
                    }
                }
            }
        }

        public static void RegisterPriorityClassHandler(Type classType, RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
        {
            IClassHandlersStore store;
            int num;
            DependencyObjectType dType = WDependencyObjectType.FromSystemTypeInternal(classType);
            WGlobalEventManager.GetDTypedClassListeners(dType, routedEvent, out store, out num);
            object synchronized = WGlobalEventManager.Synchronized;
            lock (synchronized)
            {
                IRoutedEventHandlerInfoList existingHandlers = store.AddToExistingHandlers(num, handler, handledEventsToo);
                RoutedEventHandlerInfo[] handlers = existingHandlers.Handlers;
                RoutedEventHandlerInfo[] destinationArray = new RoutedEventHandlerInfo[] { handlers[handlers.Length - 1] };
                Array.Copy(handlers, 0, destinationArray, 1, handlers.Length - 1);
                existingHandlers.Handlers = destinationArray;
                IItemStructList<DependencyObjectType> activeDTypes = WGlobalEventManager.DTypedClassListeners.ActiveDTypes;
                for (int i = 0; i < activeDTypes.Count; i++)
                {
                    if (activeDTypes.List[i].IsSubclassOf(dType))
                    {
                        WGlobalEventManager.DTypedClassListeners[activeDTypes.List[i]].Wrap<IClassHandlersStore>().UpdateSubClassHandlers(routedEvent, existingHandlers);
                    }
                }
            }
        }

        private static IDependencyObjectTypeStatic WDependencyObjectType { get; set; }

        private static IGlobalEventManagerStatic WGlobalEventManager { get; set; }

        [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance), AssignableFrom("System.Windows.ClassHandlersStore")]
        public interface IClassHandlersStore
        {
            GlobalEventManagerHelper.IRoutedEventHandlerInfoList AddToExistingHandlers(int index, Delegate handler, bool handledEventsToo);
            void UpdateSubClassHandlers(RoutedEvent routedEvent, GlobalEventManagerHelper.IRoutedEventHandlerInfoList existingHandlers);
        }

        [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Static)]
        public interface IDependencyObjectTypeStatic
        {
            DependencyObjectType FromSystemTypeInternal(Type classType);
        }

        [Wrapper, AssignableFrom("MS.Utility.DTypeMap")]
        public interface IDTypeMap
        {
            GlobalEventManagerHelper.IItemStructList<DependencyObjectType> ActiveDTypes { get; }

            object this[DependencyObjectType dType] { get; set; }
        }

        [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)]
        public interface IGlobalEventManagerStatic
        {
            GlobalEventManagerHelper.IRoutedEventHandlerInfoList GetDTypedClassListeners(DependencyObjectType dType, RoutedEvent routedEvent, out GlobalEventManagerHelper.IClassHandlersStore classListenersLists, out int index);

            [FieldAccessor]
            object Synchronized { get; }

            [Name("_dTypedClassListeners"), FieldAccessor]
            GlobalEventManagerHelper.IDTypeMap DTypedClassListeners { get; }
        }

        [Wrapper]
        public interface IItemStructList<T>
        {
            [FieldAccessor]
            int Count { get; }

            [FieldAccessor]
            T[] List { get; }
        }

        [AssignableFrom("System.Windows.RoutedEventHandlerInfoList"), Wrapper]
        public interface IRoutedEventHandlerInfoList
        {
            [BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance), FieldAccessor]
            RoutedEventHandlerInfo[] Handlers { get; set; }
        }
    }
}

