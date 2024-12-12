namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ServerModeServerAndChannelModelBuilder
    {
        public static bool TraceWriteLines;
        public const int MaxSamples = 0x400;
        protected readonly IList<ServerModeServerAndChannelModelBuilder.Sample> Samples;
        private static readonly string[] Combinations;

        static ServerModeServerAndChannelModelBuilder();
        public ServerModeServerAndChannelModelBuilder();
        [IteratorStateMachine(typeof(ServerModeServerAndChannelModelBuilder.<Get1And3OfEvery3>d__16<>))]
        private static IEnumerable<T> Get1And3OfEvery3<T>(IEnumerable<T> src);
        [IteratorStateMachine(typeof(ServerModeServerAndChannelModelBuilder.<Get2OfEvery3>d__15<>))]
        private static IEnumerable<T> Get2OfEvery3<T>(IEnumerable<T> src);
        public int? GetMaxObservableTake();
        private static bool IsInvalid(ServerModeServerAndChannelModel s);
        [IteratorStateMachine(typeof(ServerModeServerAndChannelModelBuilder.<Join>d__14<>))]
        private static IEnumerable<T> Join<T>(IEnumerable<T> first, IEnumerable<T> second);
        public static double[] Linear(int unknowns, double[,] data);
        public static double[] LinearLeastSquares(ServerModeServerAndChannelModelBuilder.LinearLeastSquaresArgs args);
        private static double[] PackSample(ServerModeServerAndChannelModelBuilder.Sample sample, string combination);
        [IteratorStateMachine(typeof(ServerModeServerAndChannelModelBuilder.<PackSamples>d__11))]
        private static IEnumerable<double[]> PackSamples(IEnumerable<ServerModeServerAndChannelModelBuilder.Sample> samples, string combination);
        public void RegisterSample(int take, int scan, double time);
        public ServerModeServerAndChannelModel Resolve();
        private static ServerModeServerAndChannelModel Resolve(IEnumerable<ServerModeServerAndChannelModelBuilder.Sample> samples, string combination);
        private static ServerModeServerAndChannelModel UnpackSolution(double[] llsResults, string combination);
        private static double Weight(IEnumerable<ServerModeServerAndChannelModelBuilder.Sample> samples, ServerModeServerAndChannelModel solution);

        [CompilerGenerated]
        private sealed class <Get1And3OfEvery3>d__16<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> src;
            public IEnumerable<T> <>3__src;
            private int <i>5__1;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <Get1And3OfEvery3>d__16(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <Get2OfEvery3>d__15<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> src;
            public IEnumerable<T> <>3__src;
            private int <i>5__1;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <Get2OfEvery3>d__15(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <Join>d__14<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<T> first;
            public IEnumerable<T> <>3__first;
            private IEnumerable<T> second;
            public IEnumerable<T> <>3__second;
            private IEnumerator<T> <>7__wrap1;

            [DebuggerHidden]
            public <Join>d__14(int <>1__state);
            private void <>m__Finally1();
            private void <>m__Finally2();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        [CompilerGenerated]
        private sealed class <PackSamples>d__11 : IEnumerable<double[]>, IEnumerable, IEnumerator<double[]>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private double[] <>2__current;
            private int <>l__initialThreadId;
            private IEnumerable<ServerModeServerAndChannelModelBuilder.Sample> samples;
            public IEnumerable<ServerModeServerAndChannelModelBuilder.Sample> <>3__samples;
            private string combination;
            public string <>3__combination;
            private IEnumerator<ServerModeServerAndChannelModelBuilder.Sample> <>7__wrap1;

            [DebuggerHidden]
            public <PackSamples>d__11(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<double[]> IEnumerable<double[]>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            double[] IEnumerator<double[]>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }

        public class LinearLeastSquaresArgs
        {
            public readonly int Unknowns;
            public readonly IEnumerable<double[]> Data;

            public LinearLeastSquaresArgs(int unknowns, IEnumerable<double[]> data);
        }

        protected class Sample
        {
            public readonly int Take;
            public readonly int Scan;
            public readonly double Time;

            public Sample(int take, int scan, double time);
        }
    }
}

