namespace DevExpress.Office
{
    using System;
    using System.Collections.Generic;

    public interface IDestinationImporter
    {
        Destination PeekDestination();
        Destination PopDestination();
        void PushDestination(Destination destination);

        Stack<Destination> DestinationStack { get; set; }
    }
}

