namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;

    public abstract class DestinationImporter : IDestinationImporter
    {
        private Stack<Destination> destinationStack = new Stack<Destination>();

        protected DestinationImporter()
        {
        }

        public Destination PeekDestination()
        {
            Destination destination = this.DestinationStack.Peek();
            return ((destination == null) ? destination : destination.Peek());
        }

        public Destination PopDestination() => 
            this.DestinationStack.Pop();

        public void PushDestination(Destination destination)
        {
            this.DestinationStack.Push(destination);
        }

        protected virtual bool IgnoreParseErrors =>
            false;

        public Stack<Destination> DestinationStack
        {
            get => 
                this.destinationStack;
            set => 
                this.destinationStack = value;
        }
    }
}

