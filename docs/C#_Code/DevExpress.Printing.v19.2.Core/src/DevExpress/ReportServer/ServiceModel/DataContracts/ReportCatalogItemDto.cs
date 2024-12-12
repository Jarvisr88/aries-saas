namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReportCatalogItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public string LockedBy { get; set; }

        public int? OptimisticLock { get; set; }
    }
}

