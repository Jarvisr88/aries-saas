namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Reflection;

    public class GlobalComponentsCache
    {
        private static Hashtable a = new Hashtable();
        private static ComponentAddedEventHandler b;
        private static ComponentRemovedEventHandler c;

        public static event ComponentAddedEventHandler ComponentAdded
        {
            add
            {
                b += value;
            }
            remove
            {
                b -= value;
            }
        }

        public static event ComponentRemovedEventHandler ComponentRemoved
        {
            add
            {
                c += value;
            }
            remove
            {
                c -= value;
            }
        }

        public static bool AddToGlobalList(IComponent component) => 
            AddToGlobalList(component, null);

        public static bool AddToGlobalList(IComponent component, string groupName)
        {
            bool flag2;
            lock (typeof(GlobalComponentsCache))
            {
                ArrayList objA = a[component.GetType()] as ArrayList;
                bool flag = ReferenceEquals(objA, null);
                if (flag)
                {
                    objA = new ArrayList();
                }
                string keyString = GetKeyString(component);
                int num = 0;
                while (true)
                {
                    if (num < objA.Count)
                    {
                        e weakReference = (e) objA[num];
                        if (!Utils.GetWeakIsAlive(weakReference) || ((((IComponent) weakReference.Target).Site == null) && Utils.DesignMode))
                        {
                            objA.Remove(weakReference);
                            num--;
                        }
                        else
                        {
                            IComponent target = (IComponent) weakReference.Target;
                            if (ReferenceEquals(target, component))
                            {
                                flag2 = false;
                                break;
                            }
                            if ((GetKeyString(target) == keyString) && (weakReference.a == groupName))
                            {
                                objA[num] = new e(component, groupName);
                                flag2 = true;
                                break;
                            }
                        }
                        num++;
                        continue;
                    }
                    objA.Add(new e(component, groupName));
                    if (flag)
                    {
                        a[component.GetType()] = objA;
                    }
                    if (b != null)
                    {
                        b(component);
                    }
                    return true;
                }
            }
            return flag2;
        }

        public static string GetKeyString(IComponent component)
        {
            if (component == null)
            {
                return (((component == null) || (component.Site == null)) ? "" : component.Site.Name);
            }
            PropertyInfo property = component.GetType().GetProperty("Name");
            if (property == null)
            {
                return "";
            }
            string name = (string) property.GetValue(component, null);
            object rootComponent = null;
            if (((component.Site != null) && component.Site.DesignMode) || Utils.DesignMode)
            {
                IContainer container;
                if (component.Site == null)
                {
                    container = null;
                }
                else
                {
                    container = component.Site.Container;
                    if ((component.Site.Container is INestedContainer) && (((INestedContainer) container).Owner.Site != null))
                    {
                        name = ((INestedContainer) container).Owner.Site.Name + "." + name;
                        container = ((INestedContainer) container).Owner.Site.Container;
                    }
                }
                if (container != null)
                {
                    IDesignerHost host = container as IDesignerHost;
                    if (host != null)
                    {
                        rootComponent = host.RootComponent;
                    }
                }
                if (rootComponent == null)
                {
                    PropertyInfo info2 = component.GetType().GetProperty("Owner");
                    if (info2 != null)
                    {
                        rootComponent = info2.GetValue(component, null);
                    }
                }
                IComponent component2 = rootComponent as IComponent;
                if ((component2 != null) && (component2.Site != null))
                {
                    name = component2.Site.Name + "." + name;
                }
            }
            else
            {
                PropertyInfo info3 = component.GetType().GetProperty("Owner");
                if (info3 == null)
                {
                    return "";
                }
                rootComponent = info3.GetValue(component, null);
                DbDataTable table = component as DbDataTable;
                if (table != null)
                {
                    name = table.FullName;
                }
                string str2 = null;
                Type objA = rootComponent.GetType();
                while (true)
                {
                    if (str2 == null)
                    {
                        FieldInfo field = objA.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
                        field ??= objA.GetField(name, BindingFlags.Public | BindingFlags.Instance);
                        if (field != null)
                        {
                            str2 = objA.ToString();
                        }
                        if (str2 == null)
                        {
                            objA = objA.BaseType;
                        }
                        if (!ReferenceEquals(objA, typeof(Component)) && (objA.FullName != "System.Web.UI.Control"))
                        {
                            continue;
                        }
                    }
                    str2 ??= rootComponent.GetType().ToString();
                    name = str2.Substring(str2.LastIndexOf(".") + 1) + "." + name;
                    break;
                }
            }
            return name;
        }

        public static IComponent GetObjectByName(string name)
        {
            IComponent component;
            lock (typeof(GlobalComponentsCache))
            {
                ArrayList list = null;
                using (IDictionaryEnumerator enumerator = a.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                            ArrayList list2 = current.Value as ArrayList;
                            int num = 0;
                            while (true)
                            {
                                if (num < list2.Count)
                                {
                                    e weakReference = (e) list2[num];
                                    if (!Utils.GetWeakIsAlive(weakReference) || ((((IComponent) weakReference.Target).Site == null) && Utils.DesignMode))
                                    {
                                        list2.Remove(weakReference);
                                        num--;
                                    }
                                    else if (GetKeyString((IComponent) weakReference.Target) == name)
                                    {
                                        return (IComponent) weakReference.Target;
                                    }
                                    num++;
                                    continue;
                                }
                                else if (list2.Count == 0)
                                {
                                    list = new ArrayList {
                                        current.Key
                                    };
                                }
                                break;
                            }
                            continue;
                        }
                        else
                        {
                            goto TR_000C;
                        }
                        break;
                    }
                }
                return component;
            TR_000C:
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        a.Remove(list[i]);
                    }
                }
                if (!Utils.DesignMode)
                {
                    throw new Exception("Cannot find component by name " + name + " in global components cache");
                }
                return null;
            }
            return component;
        }

        public static ArrayList GetObjects(Type objectType) => 
            GetObjects(objectType, null);

        public static ArrayList GetObjects(Type objectType, string groupName)
        {
            lock (typeof(GlobalComponentsCache))
            {
                ArrayList list = new ArrayList();
                ArrayList list2 = null;
                foreach (DictionaryEntry entry in a)
                {
                    if (objectType.IsAssignableFrom((Type) entry.Key))
                    {
                        ArrayList list3 = (ArrayList) entry.Value;
                        if (list3 != null)
                        {
                            int num = 0;
                            while (true)
                            {
                                if (num >= list3.Count)
                                {
                                    if (list3.Count == 0)
                                    {
                                        list2 = new ArrayList {
                                            entry.Key
                                        };
                                    }
                                    break;
                                }
                                e weakReference = (e) list3[num];
                                if (!Utils.GetWeakIsAlive(weakReference) || ((((IComponent) weakReference.Target).Site == null) && Utils.DesignMode))
                                {
                                    list3.Remove(weakReference);
                                    num--;
                                }
                                num++;
                            }
                        }
                    }
                }
                if (list2 != null)
                {
                    for (int i = 0; i < list2.Count; i++)
                    {
                        a.Remove(list2[i]);
                    }
                }
                foreach (DictionaryEntry entry2 in a)
                {
                    if (objectType.IsAssignableFrom((Type) entry2.Key))
                    {
                        ArrayList list4 = (ArrayList) entry2.Value;
                        if (list4 != null)
                        {
                            for (int i = 0; i < list4.Count; i++)
                            {
                                e weakReference = (e) list4[i];
                                if ((Utils.GetWeakIsAlive(weakReference) && ((((IComponent) weakReference.Target).Site != null) || Utils.DesignMode)) && ((groupName == null) || ((groupName == "") || (weakReference.a == groupName))))
                                {
                                    list.Add(weakReference.Target);
                                }
                            }
                        }
                    }
                }
                return list;
            }
        }

        public static void RemoveFromGlobalList(IComponent component)
        {
            lock (typeof(GlobalComponentsCache))
            {
                IComponent component2;
                ArrayList list = a[component.GetType()] as ArrayList;
                if (list != null)
                {
                    component2 = null;
                    string keyString = GetKeyString(component);
                    for (int i = 0; i < list.Count; i++)
                    {
                        e weakReference = (e) list[i];
                        if (!Utils.GetWeakIsAlive(weakReference) || ((((IComponent) weakReference.Target).Site == null) && Utils.DesignMode))
                        {
                            list.Remove(weakReference);
                            i--;
                        }
                        else
                        {
                            IComponent target = (IComponent) weakReference.Target;
                            if (ReferenceEquals(target, component) || (GetKeyString(target) == keyString))
                            {
                                list.Remove(weakReference);
                                component2 = target;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    goto TR_0001;
                }
                if (list.Count == 0)
                {
                    a.Remove(component.GetType());
                }
                if ((component2 != null) && (c != null))
                {
                    c(component2);
                }
            TR_0001:;
            }
        }
    }
}

