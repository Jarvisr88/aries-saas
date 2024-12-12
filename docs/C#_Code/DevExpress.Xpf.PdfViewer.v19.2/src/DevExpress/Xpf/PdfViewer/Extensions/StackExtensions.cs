namespace DevExpress.Xpf.PdfViewer.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static class StackExtensions
    {
        public static T Remove<T>(this Stack<T> stack, T element)
        {
            T item = stack.Pop();
            if (item.Equals(element))
            {
                return item;
            }
            T local2 = stack.Remove<T>(element);
            stack.Push(item);
            return local2;
        }
    }
}

