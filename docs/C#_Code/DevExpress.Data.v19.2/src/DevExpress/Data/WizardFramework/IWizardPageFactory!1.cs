namespace DevExpress.Data.WizardFramework
{
    using System;

    public interface IWizardPageFactory<TModel> where TModel: IWizardModel
    {
        IWizardPage<TModel> GetPage(Type pageType);
    }
}

