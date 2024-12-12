namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class GS1Helper
    {
        internal static List<GS1Helper.AIElement> knownAI;

        static GS1Helper();
        private static void AddKnownAI(string prefix, int length, bool predefined);
        private static void AddKnownAI_Y(string prefix, int length, bool predefined);
        private static GS1Helper.ElementResult ConvertAIElement(string text, ref int from, char fnc1Char);
        private static GS1Helper.ElementResult ExtractAIElementValue(string text, ref int from, GS1Helper.AIElement ai, char fnc1Char);
        [IteratorStateMachine(typeof(GS1Helper.<GetAIElements>d__5))]
        internal static IEnumerable<GS1Helper.ElementResult> GetAIElements(string text, char fnc1Char, string fnc1Subst);
        public static string MakeDisplayText(string text, char fnc1Char, string fnc1Subst, bool decodeText);

        [CompilerGenerated]
        private sealed class <GetAIElements>d__5 : IEnumerable<GS1Helper.ElementResult>, IEnumerable, IEnumerator<GS1Helper.ElementResult>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private GS1Helper.ElementResult <>2__current;
            private int <>l__initialThreadId;
            private string fnc1Subst;
            public string <>3__fnc1Subst;
            private string text;
            public string <>3__text;
            private char fnc1Char;
            public char <>3__fnc1Char;
            private int <from>5__1;
            private int <count>5__2;

            [DebuggerHidden]
            public <GetAIElements>d__5(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<GS1Helper.ElementResult> IEnumerable<GS1Helper.ElementResult>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            GS1Helper.ElementResult IEnumerator<GS1Helper.ElementResult>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        internal class AIElement
        {
            public readonly string id;
            public readonly int length;
            public readonly bool predefined;

            public AIElement(string ai, int length, bool predefined);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ElementResult
        {
            public string AI;
            public string Value;
        }
    }
}

