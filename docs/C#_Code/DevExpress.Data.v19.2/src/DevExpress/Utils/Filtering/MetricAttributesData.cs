namespace DevExpress.Utils.Filtering
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;

    public abstract class MetricAttributesData
    {
        protected internal const string DataControllerMember = "DataController";
        protected internal const string UniqueValuesMember = "UniqueValues";
        protected internal const string DisplayTextsMember = "DisplayTexts";
        protected internal const string FilterByDisplayTextMember = "FilterByDisplayText";
        protected internal const string DisplayBlanksMember = "DisplayBlanks";
        protected internal const string DisplayRadioMember = "DisplayRadio";
        protected internal const string DataItemsLookupMember = "DataItemsLookup";
        protected internal const string MemberBindings = "#MemberBindings";
        protected internal const string GroupValuesMember = "GroupValues";
        protected internal const string GroupTextsMember = "GroupTexts";
        protected internal const string GroupPathMember = "#GroupPath";
        protected internal const string GroupParentValuesMember = "#GroupParentValues";
        protected internal const string GroupParentCriteriaMember = "#GroupParentCriteria";
        private readonly IDictionary<string, object> memberValues;

        protected MetricAttributesData(IDictionary<string, object> memberValues)
        {
            this.memberValues = memberValues;
        }

        protected TValue GetValue<TValue>(Expression<Func<TValue>> expression) => 
            this.GetValue<TValue>(ExpressionHelper.GetPropertyName<TValue>(expression));

        protected TValue GetValue<TValue>(string memberName)
        {
            object obj2;
            if (this.memberValues.TryGetValue(memberName, out obj2) && (obj2 is TValue))
            {
                return (TValue) obj2;
            }
            return default(TValue);
        }

        protected bool HasValue<TValue>(string memberName, Predicate<TValue> nonDefault)
        {
            object obj2;
            return (this.memberValues.TryGetValue(memberName, out obj2) && ((obj2 is TValue) && nonDefault((TValue) obj2)));
        }

        protected internal static bool IsSpecialMember(string propertyName) => 
            (propertyName == "DataController") || ((propertyName == "UniqueValues") || ((propertyName == "GroupValues") || ((propertyName == "DisplayTexts") || ((propertyName == "GroupTexts") || ((propertyName == "DataItemsLookup") || ((propertyName == "FilterByDisplayText") || ((propertyName == "DisplayBlanks") || ((propertyName == "DisplayRadio") || (propertyName == "#MemberBindings")))))))));

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetDisplayBlanks()
        {
            bool? nullable = null;
            this.SetValue<bool?>("DisplayBlanks", nullable);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetDisplayRadio()
        {
            bool? nullable = null;
            this.SetValue<bool?>("DisplayRadio", nullable);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDataController(object controller)
        {
            this.SetValue<object>("DataController", controller);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void SetDataItemsLookup(object lookup)
        {
            this.SetValue<object>("DataItemsLookup", lookup);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDisplayBlanks(bool value)
        {
            this.SetValue<bool?>("DisplayBlanks", new bool?(value));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDisplayRadio(bool value)
        {
            this.SetValue<bool?>("DisplayRadio", new bool?(value));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDisplayTexts(string[] displayTexts)
        {
            this.SetValue<string[]>("DisplayTexts", displayTexts);
        }

        protected void SetValue<TValue>(Expression<Func<TValue>> expression, object value)
        {
            this.SetValue<object>(ExpressionHelper.GetPropertyName<TValue>(expression), value);
        }

        protected void SetValue<TValue>(string memberName, TValue value)
        {
            if (!this.memberValues.ContainsKey(memberName))
            {
                this.memberValues.Add(memberName, value);
            }
            else
            {
                this.memberValues[memberName] = value;
            }
        }

        internal bool TryGetValue<TValue>(string memberName, out TValue memberValue)
        {
            object obj2;
            bool flag = false;
            if (!this.memberValues.TryGetValue(memberName, out obj2) || !(obj2 is TValue))
            {
                memberValue = default(TValue);
            }
            else
            {
                memberValue = (TValue) obj2;
                flag = true;
            }
            return flag;
        }
    }
}

