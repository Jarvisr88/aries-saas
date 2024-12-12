namespace DevExpress.Security.Resources
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class AccessSettings
    {
        private static readonly ReadOnlyCollection<IAccessRule> defaultRules;
        private ReadOnlyCollection<IAccessRule> rules;

        static AccessSettings()
        {
            IAccessRule[] list = new IAccessRule[] { DirectoryAccessRule.Allow(new string[0]), UrlAccessRule.Allow(new string[0]) };
            defaultRules = new ReadOnlyCollection<IAccessRule>(list);
            <StaticResources>k__BackingField = new AccessSettings();
            <DataResources>k__BackingField = new AccessSettings();
            <ReportingSpecificResources>k__BackingField = new AccessSettings();
        }

        public bool CheckRules<T>(Func<T, bool> check) where T: IAccessRule
        {
            Guard.ArgumentNotNull(check, "check");
            IReadOnlyCollection<T> rules = this.GetRules<T>(AccessPermission.Allow);
            IReadOnlyCollection<T> source = this.GetRules<T>(AccessPermission.Deny);
            return ((rules.Count <= 0) ? !source.Any<T>(check) : (rules.Any<T>(check) && !source.Any<T>(check)));
        }

        private IReadOnlyCollection<T> GetRules<T>(AccessPermission permission) where T: IAccessRule => 
            new ReadOnlyCollection<T>((from x in (this.rules ?? defaultRules).OfType<T>()
                where x.Permission == permission
                select x).ToList<T>());

        public void SetRules(params IAccessRule[] rules)
        {
            if (!this.TrySetRules(rules))
            {
                throw new InvalidOperationException("Rules have been already set.");
            }
        }

        public bool TrySetRules(params IAccessRule[] rules)
        {
            Guard.ArgumentNotNull(rules, "rules");
            Func<IAccessRule, bool> predicate = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<IAccessRule, bool> local1 = <>c.<>9__2_0;
                predicate = <>c.<>9__2_0 = x => x != null;
            }
            IAccessRule[] list = rules.Where<IAccessRule>(predicate).ToArray<IAccessRule>();
            if ((this.rules != null) || (list.Length == 0))
            {
                return false;
            }
            this.rules = new ReadOnlyCollection<IAccessRule>(list);
            return true;
        }

        public static AccessSettings StaticResources { get; private set; }

        public static AccessSettings DataResources { get; private set; }

        public static AccessSettings ReportingSpecificResources { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AccessSettings.<>c <>9 = new AccessSettings.<>c();
            public static Func<IAccessRule, bool> <>9__2_0;

            internal bool <TrySetRules>b__2_0(IAccessRule x) => 
                x != null;
        }
    }
}

