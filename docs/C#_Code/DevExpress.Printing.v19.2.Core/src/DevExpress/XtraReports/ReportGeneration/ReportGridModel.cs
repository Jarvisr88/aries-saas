namespace DevExpress.XtraReports.ReportGeneration
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    public class ReportGridModel : IWizardModel, ICloneable
    {
        public ReportGridModel()
        {
            this.Options = new ReportGenerationOptions();
        }

        protected ReportGridModel(ReportGridModel model)
        {
            if (model != null)
            {
                ReportGenerationOptions options = model.Options;
                ReportGenerationOptions options2 = options;
                if (options == null)
                {
                    ReportGenerationOptions local1 = options;
                    options2 = new ReportGenerationOptions();
                }
                this.Options = options2;
                this.View = model.View;
            }
        }

        public virtual object Clone() => 
            new ReportGridModel(this);

        public override bool Equals(object obj)
        {
            ReportGridModel model = obj as ReportGridModel;
            return ((model != null) ? (Equals(this.Options, model.Options) ? Equals(this.View, model.View) : false) : false);
        }

        public override int GetHashCode() => 
            0;

        public ReportGenerationOptions Options { get; set; }

        public IGridViewFactoryBase View { get; set; }
    }
}

