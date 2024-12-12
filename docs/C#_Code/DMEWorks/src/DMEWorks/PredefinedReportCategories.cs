namespace DMEWorks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PredefinedReportCategories
    {
        public const string CmnRx = "CMN/RX";
        public const string Customer = "Customer";
        public const string Doctor = "Doctor";
        public const string ICD9 = "ICD9";
        public const string ICD10 = "ICD10";
        public const string Insurance = "Insurance";
        public const string Invoice = "Invoice";
        public const string LegalRep = "Legal Rep";
        public const string Order = "Order";
        public const string PurchaseOrder = "PO";
        public const string Referral = "Referral";
        public const string SalesRep = "Sales Rep";
        public const string Vendor = "Vendor";

        [IteratorStateMachine(typeof(VB$StateMachine_14_AllCategories))]
        public static IEnumerable<string> AllCategories() => 
            new VB$StateMachine_14_AllCategories(-2);

        [CompilerGenerated]
        private sealed class VB$StateMachine_14_AllCategories : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
        {
            public int $State;
            public string $Current;
            public int $InitialThreadId;

            public VB$StateMachine_14_AllCategories(int $State)
            {
                this.$State = $State;
                this.$InitialThreadId = Environment.CurrentManagedThreadId;
            }

            private void Dispose()
            {
            }

            private IEnumerator<string> GetEnumerator()
            {
                PredefinedReportCategories.VB$StateMachine_14_AllCategories categories;
                if ((this.$State != -2) || (this.$InitialThreadId != Environment.CurrentManagedThreadId))
                {
                    categories = new PredefinedReportCategories.VB$StateMachine_14_AllCategories(0);
                }
                else
                {
                    this.$State = 0;
                    categories = this;
                }
                return categories;
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();

            [CompilerGenerated]
            private bool MoveNext()
            {
                int num;
                switch (this.$State)
                {
                    case 0:
                        this.$State = num = -1;
                        this.$Current = "CMN/RX";
                        this.$State = num = 1;
                        return true;

                    case 1:
                        this.$State = num = -1;
                        this.$Current = "Customer";
                        this.$State = num = 2;
                        return true;

                    case 2:
                        this.$State = num = -1;
                        this.$Current = "Doctor";
                        this.$State = num = 3;
                        return true;

                    case 3:
                        this.$State = num = -1;
                        this.$Current = "ICD9";
                        this.$State = num = 4;
                        return true;

                    case 4:
                        this.$State = num = -1;
                        this.$Current = "ICD10";
                        this.$State = num = 5;
                        return true;

                    case 5:
                        this.$State = num = -1;
                        this.$Current = "Insurance";
                        this.$State = num = 6;
                        return true;

                    case 6:
                        this.$State = num = -1;
                        this.$Current = "Invoice";
                        this.$State = num = 7;
                        return true;

                    case 7:
                        this.$State = num = -1;
                        this.$Current = "Legal Rep";
                        this.$State = num = 8;
                        return true;

                    case 8:
                        this.$State = num = -1;
                        this.$Current = "Order";
                        this.$State = num = 9;
                        return true;

                    case 9:
                        this.$State = num = -1;
                        this.$Current = "PO";
                        this.$State = num = 10;
                        return true;

                    case 10:
                        this.$State = num = -1;
                        this.$Current = "Referral";
                        this.$State = num = 11;
                        return true;

                    case 11:
                        this.$State = num = -1;
                        this.$Current = "Sales Rep";
                        this.$State = num = 12;
                        return true;

                    case 12:
                        this.$State = num = -1;
                        this.$Current = "Vendor";
                        this.$State = num = 13;
                        return true;

                    case 13:
                        this.$State = num = -1;
                        return false;
                }
                return false;
            }

            private void Reset()
            {
                throw new NotSupportedException();
            }

            private string Current =>
                this.$Current;

            object IEnumerator.Current =>
                this.$Current;
        }
    }
}

