namespace DevExpress.Xpo.Logger
{
    using DevExpress.Xpo.Logger.Transport;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class LoggerBase : ILogger, ILogSource
    {
        private int capacity;
        private LogMessage[] messageArray;
        private long headtail;
        private int lostMessageCount;
        private bool enabled = true;

        public LoggerBase(int capacity)
        {
            this.capacity = capacity;
            this.messageArray = new LogMessage[capacity];
        }

        public void ClearLog()
        {
            if (this.enabled && (this.capacity != 0))
            {
                while (true)
                {
                    int num3;
                    int num4;
                    long headtail = this.headtail;
                    GetHeadAndTail(headtail, out num3, out num4);
                    int head = 0;
                    int tail = 0;
                    long headTail = GetHeadTail(head, tail);
                    if (headtail == Interlocked.CompareExchange(ref this.headtail, headTail, headtail))
                    {
                        return;
                    }
                }
            }
        }

        public LogMessage[] GetCompleteLog()
        {
            LogMessage message;
            if (!this.enabled)
            {
                return null;
            }
            List<LogMessage> list = new List<LogMessage>(this.Count);
            while ((message = this.GetMessage()) != null)
            {
                list.Add(message);
            }
            return list.ToArray();
        }

        private static void GetHeadAndTail(long headTail, out int head, out int tail)
        {
            head = (int) (headTail & 0x7fffffffL);
            tail = (int) ((headTail & 0x7fffffff00000000L) >> 0x20);
        }

        private static long GetHeadTail(int head, int tail) => 
            ((tail & 0x7fffffffL) << 0x20) | (head & 0x7fffffffL);

        public LogMessage GetMessage()
        {
            if (!this.enabled)
            {
                return null;
            }
            while (true)
            {
                int num3;
                int num4;
                long headtail = this.headtail;
                GetHeadAndTail(headtail, out num4, out num3);
                int tail = num3 + 1;
                if (tail >= this.capacity)
                {
                    tail = 0;
                }
                if (num4 == num3)
                {
                    return null;
                }
                long headTail = GetHeadTail(num4, tail);
                LogMessage objA = this.messageArray[num3];
                if ((headtail == Interlocked.CompareExchange(ref this.headtail, headTail, headtail)) || ReferenceEquals(objA, this.messageArray[num3]))
                {
                    return objA;
                }
            }
        }

        public LogMessage[] GetMessages(int messagesAmount)
        {
            if (!this.enabled)
            {
                return null;
            }
            List<LogMessage> list = new List<LogMessage>(messagesAmount);
            int num = 0;
            while (true)
            {
                if (num < messagesAmount)
                {
                    LogMessage item = this.GetMessage();
                    if (item != null)
                    {
                        list.Add(item);
                        num++;
                        continue;
                    }
                }
                return list.ToArray();
            }
        }

        public void Log(LogMessage message)
        {
            if (this.enabled && (this.capacity != 0))
            {
                bool flag = false;
                while (true)
                {
                    int num3;
                    int num4;
                    long headtail = this.headtail;
                    GetHeadAndTail(headtail, out num4, out num3);
                    int head = num4 + 1;
                    int tail = num3;
                    if (head >= this.capacity)
                    {
                        head = 0;
                    }
                    if (((head >= tail) && (num4 < num3)) || (head == tail))
                    {
                        tail = head + 1;
                        flag = true;
                    }
                    if (tail >= this.capacity)
                    {
                        tail = 0;
                    }
                    long headTail = GetHeadTail(head, tail);
                    LogMessage comparand = this.messageArray[num4];
                    if (headtail == Interlocked.CompareExchange(ref this.headtail, headTail, headtail))
                    {
                        if (Interlocked.CompareExchange<LogMessage>(ref this.messageArray[num4], message, comparand) != comparand)
                        {
                            Interlocked.Increment(ref this.lostMessageCount);
                        }
                        if (flag)
                        {
                            Interlocked.Increment(ref this.lostMessageCount);
                        }
                        return;
                    }
                }
            }
        }

        public void Log(LogMessage[] messages)
        {
            if (this.enabled && (this.capacity != 0))
            {
                foreach (LogMessage message in messages)
                {
                    this.Log(message);
                }
            }
        }

        public int Count
        {
            get
            {
                int num2;
                int num3;
                GetHeadAndTail(Interlocked.Read(ref this.headtail), out num2, out num3);
                return ((num3 > num2) ? (num3 - num2) : ((this.capacity - num2) + num3));
            }
        }

        public int Capacity =>
            this.capacity;

        public int LostMessageCount =>
            this.lostMessageCount;

        public bool Enabled
        {
            get => 
                this.enabled;
            set => 
                this.enabled = value;
        }

        public bool IsServerActive =>
            true;
    }
}

