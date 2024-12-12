namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class DefaultServiceContainer : ServiceContainer
    {
        public DefaultServiceContainer() : base(null)
        {
        }

        [CompilerGenerated, DebuggerHidden]
        private IEnumerable<object> <>n__0(Type type, bool localOnly) => 
            base.GetServicesCore(type, localOnly);

        protected virtual ResourceDictionary GetApplicationResources()
        {
            Func<Application, bool> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<Application, bool> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.Dispatcher.CheckAccess();
            }
            return (Application.Current.Return<Application, bool>(evaluator, (<>c.<>9__1_1 ??= () => false)) ? Application.Current.Resources : null);
        }

        private Dictionary<string, object> GetApplicationResources(Type type)
        {
            ResourceDictionary appResources = this.GetApplicationResources();
            if (appResources == null)
            {
                return new Dictionary<string, object>();
            }
            Func<string, string> keySelector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<string, string> local1 = <>c.<>9__2_0;
                keySelector = <>c.<>9__2_0 = x => x;
            }
            Func<KeyValuePair<string, object>, string> func2 = <>c.<>9__2_3;
            if (<>c.<>9__2_3 == null)
            {
                Func<KeyValuePair<string, object>, string> local2 = <>c.<>9__2_3;
                func2 = <>c.<>9__2_3 = x => x.Key;
            }
            return (from x in appResources.Keys.OfType<string>().ToDictionary<string, string, object>(keySelector, x => appResources[x])
                where (x.Value != null) && type.IsAssignableFrom(x.Value.GetType())
                select x).ToDictionary<KeyValuePair<string, object>, string, object>(func2, (<>c.<>9__2_4 ??= x => x.Value));
        }

        protected override object GetServiceCore(Type type, string key, ServiceSearchMode searchMode, out bool serviceHasKey)
        {
            object obj2 = base.GetServiceCore(type, key, searchMode, out serviceHasKey);
            if (obj2 != null)
            {
                return obj2;
            }
            Dictionary<string, object> applicationResources = this.GetApplicationResources(type);
            if (!string.IsNullOrEmpty(key) && applicationResources.ContainsKey(key))
            {
                return applicationResources[key];
            }
            serviceHasKey = true;
            return applicationResources.FirstOrDefault<KeyValuePair<string, object>>().Value;
        }

        [IteratorStateMachine(typeof(<GetServicesCore>d__4))]
        protected override IEnumerable<object> GetServicesCore(Type type, bool localOnly)
        {
            Dictionary<string, object>.ValueCollection.Enumerator <>7__wrap2;
            IEnumerator<object> enumerator = this.<>n__0(type, localOnly).GetEnumerator();
        Label_PostSwitchInIterator:;
            if (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                yield return current;
                goto Label_PostSwitchInIterator;
            }
            else
            {
                enumerator = null;
                <>7__wrap2 = this.GetApplicationResources(type).Values.GetEnumerator();
            }
            if (!<>7__wrap2.MoveNext())
            {
                <>7__wrap2 = new Dictionary<string, object>.ValueCollection.Enumerator();
                yield break;
            }
            else
            {
                object current = <>7__wrap2.Current;
                yield return current;
                yield break;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DefaultServiceContainer.<>c <>9 = new DefaultServiceContainer.<>c();
            public static Func<Application, bool> <>9__1_0;
            public static Func<bool> <>9__1_1;
            public static Func<string, string> <>9__2_0;
            public static Func<KeyValuePair<string, object>, string> <>9__2_3;
            public static Func<KeyValuePair<string, object>, object> <>9__2_4;

            internal bool <GetApplicationResources>b__1_0(Application x) => 
                x.Dispatcher.CheckAccess();

            internal bool <GetApplicationResources>b__1_1() => 
                false;

            internal string <GetApplicationResources>b__2_0(string x) => 
                x;

            internal string <GetApplicationResources>b__2_3(KeyValuePair<string, object> x) => 
                x.Key;

            internal object <GetApplicationResources>b__2_4(KeyValuePair<string, object> x) => 
                x.Value;
        }

    }
}

