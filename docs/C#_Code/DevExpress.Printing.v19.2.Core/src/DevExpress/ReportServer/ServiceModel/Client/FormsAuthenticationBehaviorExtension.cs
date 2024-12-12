namespace DevExpress.ReportServer.ServiceModel.Client
{
    using System;
    using System.ServiceModel.Configuration;

    public class FormsAuthenticationBehaviorExtension : BehaviorExtensionElement
    {
        private FormsAuthenticationEndpointBehavior formsAuthenticationEndpointBehavior;

        protected override object CreateBehavior()
        {
            this.formsAuthenticationEndpointBehavior ??= new FormsAuthenticationEndpointBehavior();
            return this.formsAuthenticationEndpointBehavior;
        }

        public override Type BehaviorType =>
            typeof(FormsAuthenticationEndpointBehavior);
    }
}

