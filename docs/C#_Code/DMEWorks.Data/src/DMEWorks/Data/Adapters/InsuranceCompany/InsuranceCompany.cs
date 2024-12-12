namespace DMEWorks.Data.Adapters.InsuranceCompany
{
    using DMEWorks.Data.Common;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class InsuranceCompany
    {
        protected InsuranceCompany()
        {
        }

        public DMEWorks.Data.Adapters.InsuranceCompany.Basis Basis { get; set; }

        public string Contact { get; set; }

        public Region ECSFormat { get; set; }

        public decimal? ExpectedPercent { get; set; }

        public string Fax { get; set; }

        public int? GroupID { get; set; }

        public int? InvoiceFormID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public int? PriceCodeID { get; set; }

        public bool? PrintHAOOnInvoice { get; set; }

        public bool? PrintInvOnInvoice { get; set; }

        public string Title { get; set; }

        public int? Type { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string AbilityNumber { get; set; }

        public string AvailityNumber { get; set; }

        public string ClaimMdNumber { get; set; }

        public string MedicaidNumber { get; set; }

        public string MedicareNumber { get; set; }

        public string OfficeAllyNumber { get; set; }

        public string ZirmedNumber { get; set; }

        public class Existing : DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany, IKeyed<int>
        {
            public int Id { get; set; }

            public InsuranceCompanyMir Mir { get; set; }

            public DateTime LastUpdateDatetime { get; set; }

            public short LastUpdateUserId { get; set; }

            int IKeyed<int>.Key =>
                this.Id;
        }

        public class New : DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany
        {
        }
    }
}

