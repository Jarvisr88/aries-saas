namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? OptimisticLock { get; set; }
    }
}

