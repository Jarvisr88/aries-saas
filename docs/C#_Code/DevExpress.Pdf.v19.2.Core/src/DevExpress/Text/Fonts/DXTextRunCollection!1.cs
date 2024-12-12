namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;

    public class DXTextRunCollection<T> where T: IDXTextRun<T>
    {
        private readonly LinkedList<T> runs;

        public DXTextRunCollection(T sourceRun)
        {
            this.runs = new LinkedList<T>();
            this.runs.AddFirst(sourceRun);
        }

        public void UpdateRunProperties(int textStart, int textLength, Action<T> runUpdateAction)
        {
            int num = (textStart + textLength) - 1;
            for (LinkedListNode<T> node = this.runs.First; node != null; node = node.Next)
            {
                T local = node.Value;
                int offset = local.Offset;
                int num3 = (local.Offset + local.Length) - 1;
                if ((offset == textStart) && (num3 == num))
                {
                    runUpdateAction(local);
                    return;
                }
                if ((offset <= textStart) && (num3 >= num))
                {
                    if (offset < textStart)
                    {
                        local = local.Split(textStart);
                        LinkedListNode<T> newNode = new LinkedListNode<T>(local);
                        this.runs.AddAfter(node, newNode);
                        node = newNode;
                    }
                    if (num3 != num)
                    {
                        this.runs.AddAfter(node, new LinkedListNode<T>(local.Split(num + 1)));
                    }
                    runUpdateAction(local);
                    return;
                }
                if ((textStart <= offset) && (num >= num3))
                {
                    runUpdateAction(local);
                }
                else if ((textStart >= offset) && ((textStart <= num3) && (num > num3)))
                {
                    LinkedListNode<T> newNode = new LinkedListNode<T>(local.Split(textStart));
                    runUpdateAction(newNode.Value);
                    this.runs.AddAfter(node, newNode);
                    node = newNode;
                }
                else if ((num >= offset) && ((num <= num3) && (textStart < offset)))
                {
                    LinkedListNode<T> newNode = new LinkedListNode<T>(local.Split(num + 1));
                    runUpdateAction(local);
                    this.runs.AddAfter(node, newNode);
                    node = newNode;
                    return;
                }
            }
        }

        public LinkedList<T> Runs =>
            this.runs;
    }
}

