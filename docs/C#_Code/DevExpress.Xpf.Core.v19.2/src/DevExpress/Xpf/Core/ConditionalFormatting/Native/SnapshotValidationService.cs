namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;

    public class SnapshotValidationService : IValidationService
    {
        private bool isValid;
        private IValidationServiceClient client;

        public bool Execute(Func<bool> action)
        {
            this.isValid = true;
            this.Subscribe();
            bool flag = action();
            this.Unsubscribe();
            return (this.isValid & flag);
        }

        private void OnErrorDetected(object sender, EventArgs e)
        {
            this.isValid = false;
        }

        public void RegisterClient(IValidationServiceClient client)
        {
            if (client != null)
            {
                this.client = client;
            }
        }

        private void Subscribe()
        {
            if (this.client != null)
            {
                this.client.ErrorDetected += new EventHandler(this.OnErrorDetected);
            }
        }

        private void Unsubscribe()
        {
            if (this.client != null)
            {
                this.client.ErrorDetected -= new EventHandler(this.OnErrorDetected);
            }
        }
    }
}

