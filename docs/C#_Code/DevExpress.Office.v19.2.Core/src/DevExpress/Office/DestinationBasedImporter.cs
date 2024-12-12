namespace DevExpress.Office
{
    using System;
    using System.Collections.Generic;

    public abstract class DestinationBasedImporter : DocumentModelImporter, IDestinationImporter
    {
        private Stack<Destination> destinationStack;

        protected DestinationBasedImporter(IDocumentModel documentModel) : base(documentModel)
        {
            this.destinationStack = new Stack<Destination>();
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

