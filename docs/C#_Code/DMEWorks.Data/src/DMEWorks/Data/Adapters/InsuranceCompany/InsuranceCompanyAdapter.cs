namespace DMEWorks.Data.Adapters.InsuranceCompany
{
    using Dapper;
    using Devart.Data.MySql;
    using DMEWorks.Data;
    using DMEWorks.Data.Common;
    using System;

    public class InsuranceCompanyAdapter : AdapterBase<DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany, int, DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New, DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing>
    {
        private readonly string m_connectionString;

        public InsuranceCompanyAdapter(string connectionString)
        {
            if (connectionString == null)
            {
                string local1 = connectionString;
                throw new ArgumentNullException("connectionString");
            }
            this.m_connectionString = connectionString;
        }

        protected override DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New Clone(DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing entity)
        {
            DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New new1 = new DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New();
            new1.Basis = entity.Basis;
            new1.Contact = entity.Contact;
            new1.ECSFormat = entity.ECSFormat;
            new1.ExpectedPercent = entity.ExpectedPercent;
            new1.Fax = entity.Fax;
            new1.GroupID = entity.GroupID;
            new1.InvoiceFormID = entity.InvoiceFormID;
            new1.Name = entity.Name;
            new1.Phone = entity.Phone;
            new1.Phone2 = entity.Phone2;
            new1.PriceCodeID = entity.PriceCodeID;
            new1.PrintHAOOnInvoice = entity.PrintHAOOnInvoice;
            new1.PrintInvOnInvoice = entity.PrintInvOnInvoice;
            new1.Title = entity.Title;
            new1.Type = entity.Type;
            new1.Address1 = entity.Address1;
            new1.Address2 = entity.Address2;
            new1.City = entity.City;
            new1.State = entity.State;
            new1.Zip = entity.Zip;
            new1.AbilityNumber = entity.AbilityNumber;
            new1.AvailityNumber = entity.AvailityNumber;
            new1.ClaimMdNumber = entity.ClaimMdNumber;
            new1.MedicaidNumber = entity.MedicaidNumber;
            new1.MedicareNumber = entity.MedicareNumber;
            new1.OfficeAllyNumber = entity.OfficeAllyNumber;
            new1.ZirmedNumber = entity.ZirmedNumber;
            return new1;
        }

        protected override DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New Create() => 
            new DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New();

        private MySqlConnection CreateConnection() => 
            new MySqlConnection(this.m_connectionString);

        protected override void Delete(int key)
        {
            using (MySqlConnection connection = this.CreateConnection())
            {
                int num;
                try
                {
                    int? commandTimeout = null;
                    CommandType? commandType = null;
                    num = connection.Execute(Queries.InsuranceCompanyDelete, new { Id = key }, null, commandTimeout, commandType);
                }
                catch (MySqlException exception1) when ((() => // NOTE: To create compilable code, filter at IL offset 002E was represented using lambda expression.
                {
                    return (exception1.ErrorCode == 0x4bd);
                })())
                {
                    MySqlException exception;
                    throw new DeadlockException("", exception);
                }
                if (num == 0)
                {
                    throw new ObjectIsNotFoundException();
                }
            }
        }

        protected override DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing Insert(DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.New entity)
        {
            DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing existing;
            bool flag1;
            MySqlConnection cnn = this.CreateConnection();
            try
            {
                int? commandTimeout = null;
                CommandType? commandType = null;
                existing = cnn.QueryFirst<DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing>(Queries.InsuranceCompanyInsert, entity, null, commandTimeout, commandType);
            }
            catch (MySqlException exception1) when ((() => // NOTE: To create compilable code, filter at IL offset 0029 was represented using lambda expression.
            {
                flag1 = exception1.ErrorCode == 0x4bd;
                return (DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing) flag1;
            })())
            {
                MySqlException exception;
                throw new DeadlockException("", exception);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Dispose();
                }
            }
            return existing;
        }

        protected override DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing Select(int key)
        {
            using (MySqlConnection connection = this.CreateConnection())
            {
                int? commandTimeout = null;
                CommandType? commandType = null;
                return connection.QueryFirst<DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing>(Queries.InsuranceCompanySelect, new { Id = key }, null, commandTimeout, commandType);
            }
        }

        protected override DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing Update(DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing entity)
        {
            DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing existing;
            bool flag1;
            MySqlConnection cnn = this.CreateConnection();
            try
            {
                int? commandTimeout = null;
                CommandType? commandType = null;
                existing = cnn.QueryFirst<DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing>(Queries.InsuranceCompanyUpdate, entity, null, commandTimeout, commandType);
            }
            catch (MySqlException exception1) when ((() => // NOTE: To create compilable code, filter at IL offset 0029 was represented using lambda expression.
            {
                flag1 = exception1.ErrorCode == 0x4bd;
                return (DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany.Existing) flag1;
            })())
            {
                MySqlException exception;
                throw new DeadlockException("", exception);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Dispose();
                }
            }
            return existing;
        }

        protected override IValidationResult Validate(DMEWorks.Data.Adapters.InsuranceCompany.InsuranceCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}

