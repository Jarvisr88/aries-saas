namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class BehaviorProvider : IBehaviorProvider
    {
        private IDictionary<string, int> orders;
        private Hashtable disabledElements;

        public BehaviorProvider(IDictionary<string, int> orders, Hashtable disabledElements);
        public bool GetIsEnabled(string name);
        public bool GetIsVisible(string name);
    }
}

