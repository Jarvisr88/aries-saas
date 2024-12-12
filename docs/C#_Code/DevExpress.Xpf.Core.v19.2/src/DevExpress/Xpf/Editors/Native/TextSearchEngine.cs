namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class TextSearchEngine : TextSearchEngineBase
    {
        private readonly Func<string, int, bool, int> searchCallback;

        public TextSearchEngine(Func<string, int, bool, int> searchCallback, TimeSpan timeSpan) : base(timeSpan)
        {
            this.searchCallback = searchCallback;
        }

        public void CancelSearch()
        {
            this.ResetState();
        }

        internal bool DeleteLastCharacter() => 
            this.DoSearch(base.BackString, -1);

        internal bool DoSearch(string nextChar, int currentItemIndex)
        {
            bool lookForFallbackMatchToo = false;
            int startItemIndex = -1;
            if (base.IsActive)
            {
                startItemIndex = currentItemIndex;
            }
            if ((base.charsEntered.Count > 0) && (string.Compare(base.charsEntered[base.charsEntered.Count - 1], nextChar, true, CultureInfo.CurrentCulture) == 0))
            {
                lookForFallbackMatchToo = true;
            }
            bool wasNewCharUsed = false;
            int num2 = this.FindMatchingPrefix(base.Prefix, nextChar, startItemIndex, lookForFallbackMatchToo, ref wasNewCharUsed);
            if (num2 == -1)
            {
                if (base.IsBackSpace(nextChar) && (base.Prefix.Length > 0))
                {
                    base.Prefix = base.Prefix.Substring(0, base.Prefix.Length - 1);
                }
            }
            else
            {
                if (!base.IsActive || (num2 != startItemIndex))
                {
                    this.MatchedItemIndex = num2;
                }
                if (wasNewCharUsed)
                {
                    base.AddCharToPrefix(nextChar);
                }
                if (!base.IsActive)
                {
                    base.IsActive = true;
                    base.SeachText = base.Prefix;
                }
            }
            if (base.IsActive)
            {
                base.ResetTimeout();
            }
            return (num2 != -1);
        }

        private int FindMatchingPrefix(string prefix, string newChar, int startItemIndex, bool lookForFallbackMatchToo, ref bool wasNewCharUsed)
        {
            int num = -1;
            string str = base.StartFindMatchingPrefix(prefix, newChar);
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            wasNewCharUsed = false;
            int num3 = this.searchCallback(str, (base.IsBackSpace(newChar) || string.IsNullOrEmpty(base.Prefix)) ? -1 : startItemIndex, false);
            if (num3 > -1)
            {
                wasNewCharUsed = true;
                num = num3;
            }
            else
            {
                if (lookForFallbackMatchToo)
                {
                    num3 = this.searchCallback(prefix, startItemIndex, true);
                }
                if (num3 > -1)
                {
                    num = num3;
                }
                else
                {
                    num3 = this.searchCallback(prefix, -1, false);
                    if (num3 > -1)
                    {
                        num = num3;
                    }
                }
            }
            return num;
        }

        protected override void ResetState()
        {
            this.MatchedItemIndex = -1;
            base.ResetState();
        }

        public int MatchedItemIndex { get; private set; }
    }
}

