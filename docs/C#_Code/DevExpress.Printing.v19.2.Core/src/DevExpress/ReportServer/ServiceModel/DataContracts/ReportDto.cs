namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReportDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Layout { get; set; }

        public int CategoryId { get; set; }

        public int RevisionId { get; set; }

        public string Comment { get; set; }

        public int? OptimisticLock { get; set; }
    }
}

