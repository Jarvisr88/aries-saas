namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class SyncHelper
    {
        private static int warningThrown;

        public static bool CanCaptureContext();
        public static SynchronizationContext CaptureSynchronizationContextOrFail();
        private static void DoLowContentionPostCoreDelays(ref int stage, int maxMsOnStage0, int maxMsOnStage1);
        public static SynchronizationContext FailIfContextWasNotCaptured(SynchronizationContext capturedContext);
        public static void LowContentionPost(IEnumerable<Tuple<SynchronizationContext, Action, Action>> toDo, int postThreshold = 100, int executionThreshold = 0xd05);
        private static void LowContentionPostCore(IList<Tuple<SynchronizationContext, Action, Action>> toDo, int postThreshold, int executionThreshold);
        public static SynchronizationContext TryCaptureSynchronizationContext();
        private static SynchronizationContext TryCaptureSynchronizationContextCore();

        public class ZombieContextsDetector : SynchronizationContext
        {
            public static int DetectionDelaySend;
            public static int DetectionDelayPost;
            public static int DetectionLevel;
            public static bool? CaptureCapturingStack;
            public static bool? CaptureCapturingThreadState;
            [ThreadStatic]
            private static bool WarningBkgndThreadCaptureThrownInThread;
            private static int WarningBkgndThreadCaptureThrownOnce;
            private readonly SynchronizationContext DestinationContext;
            private readonly Thread DestinationThread;
            private readonly string CapturingThreadState;
            private readonly StackTrace CapturingStack;
            private static int __issued_ReachingPostContext;
            private static int __issued_ReachingSendContext;
            private static int __issued_ExecutingCallback;
            private static int __issued_Other;

            public static event EventHandler<SyncHelper.ZombieContextsDetector.CustomActionEventArgs> CustomAction;

            static ZombieContextsDetector();
            public ZombieContextsDetector(SynchronizationContext _DestinationContext);
            private static void BkgndCaptureWarning(SynchronizationContext suspect);
            private static string GetThreadString(Thread th);
            private static bool NeedWarning(SyncHelper.ZombieContextsDetector.Stage currStage);
            public override void Post(SendOrPostCallback d, object state);
            public override void Send(SendOrPostCallback d, object state);
            private static void Suspects(SyncHelper.ZombieContextsDetector.Stage currStage, SynchronizationContext capturedContext, Thread capturedThread, Thread invocationThread, string capturingThreadState, StackTrace capturingStackTrace);
            public static SynchronizationContext Wrap(SynchronizationContext suspect);

            public class CustomActionEventArgs : EventArgs
            {
                public readonly string Message;
                public readonly DevExpress.Data.Helpers.SyncHelper.ZombieContextsDetector.Stage Stage;
                public readonly SynchronizationContext CapturedContext;
                public readonly Thread CapturedThread;
                public readonly Thread InvocationThread;

                protected internal CustomActionEventArgs(string msg, DevExpress.Data.Helpers.SyncHelper.ZombieContextsDetector.Stage stage, SynchronizationContext capturedContext, Thread capturedThread, Thread invocationThread);
            }

            public enum Stage
            {
                public const SyncHelper.ZombieContextsDetector.Stage None = SyncHelper.ZombieContextsDetector.Stage.None;,
                public const SyncHelper.ZombieContextsDetector.Stage ReachingPostContext = SyncHelper.ZombieContextsDetector.Stage.ReachingPostContext;,
                public const SyncHelper.ZombieContextsDetector.Stage ReachingSendContext = SyncHelper.ZombieContextsDetector.Stage.ReachingSendContext;,
                public const SyncHelper.ZombieContextsDetector.Stage ExecutingCallback = SyncHelper.ZombieContextsDetector.Stage.ExecutingCallback;,
                public const SyncHelper.ZombieContextsDetector.Stage Returning = SyncHelper.ZombieContextsDetector.Stage.Returning;,
                public const SyncHelper.ZombieContextsDetector.Stage Clear = SyncHelper.ZombieContextsDetector.Stage.Clear;
            }
        }
    }
}

