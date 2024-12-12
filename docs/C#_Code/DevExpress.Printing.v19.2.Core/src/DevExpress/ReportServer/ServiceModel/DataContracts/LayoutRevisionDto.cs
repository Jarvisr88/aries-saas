namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutRevisionDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int RevisionNumber { get; set; }

        public string UserName { get; set; }

        public DateTime RevisionDate { get; set; }
    }
}

