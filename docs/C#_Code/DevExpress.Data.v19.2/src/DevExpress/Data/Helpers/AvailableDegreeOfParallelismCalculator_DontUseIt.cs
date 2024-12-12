namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Threading;

    public class AvailableDegreeOfParallelismCalculator_DontUseIt
    {
        private static Lazy<int> maxDoP;
        public static int? CoresToUseHint;
        private static Lazy<int> realCores;
        public readonly int MaxDegreeToProbe;
        public int Result;
        private int Waiting;
        private int Alive;
        private readonly ManualResetEvent Event;

        static AvailableDegreeOfParallelismCalculator_DontUseIt();
        private AvailableDegreeOfParallelismCalculator_DontUseIt(int maxDegreeToProbe);
        private static int CalculateMaxDoP();
        public void Finish();
        public static int GetMaxDoP();
        public static int GetRealCoresToUse();
        [SecuritySafeCritical]
        private static int GuessRealCores();
        private static int GuessRealCoresStaSafe();
        private void Probe(object useless);
        private void Spawner(object useless);
        public static AvailableDegreeOfParallelismCalculator_DontUseIt Start(int maxDegreeToProbe);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AvailableDegreeOfParallelismCalculator_DontUseIt.<>c <>9;
            public static Func<int> <>9__5_0;

            static <>c();
            internal int <.cctor>b__18_0();
            internal int <.cctor>b__18_1();
            internal int <GuessRealCoresStaSafe>b__5_0();
        }
    }
}

