namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class RealTimeEventsQueue
    {
        private int InQueueLock;
        private int BufferLock;
        private int UiLock;
        private RealTimeEventBase First;
        private RealTimeEventBase Last;
        private Queue<RealTimeRowEvent> Buffer;
        private int BufferedProcessing;
        private readonly Action SomethingInTheQueueAction;

        public RealTimeEventsQueue(Action _SomethingInTheQueueAction);
        private void _BufferProcessing();
        private RealTimeRowEvent FindRowInQueue(int from, int to, out bool noMoves);
        public bool IsEmpty();
        private static bool IsEmptyEvent(RealTimeEventBase ev);
        public bool IsSomethingToPull();
        private bool ProcessBufferStep();
        private bool ProcessRowInQueueIfExists(RealTimeRowEvent rowEvent);
        public IEnumerable<RealTimeEventBase> PullAllEvents();
        public RealTimeEventBase PullEvent();
        private RealTimeEventBase PullEventCore();
        public void PushEvent(RealTimePropertyDescriptorsChangedEvent pdcEvent);
        public void PushEvent(RealTimeResetEvent resetEvent);
        public void PushEvent(RealTimeRowEvent rowEvent);
        private void PushEventCore(RealTimeRowEvent rowEvent);
        private void SomethingReadyForPull();
        private void StartBufferProcessingIfNeeded();
        private static void SureLock(ref int location, int lockedValue);
        private static void UnLock(ref int location, int lockedValue);
    }
}

