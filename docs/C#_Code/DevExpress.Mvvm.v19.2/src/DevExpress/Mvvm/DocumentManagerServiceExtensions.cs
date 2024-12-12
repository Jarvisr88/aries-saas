namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class DocumentManagerServiceExtensions
    {
        public static IDocument CreateDocument(this IDocumentManagerService service, object viewModel)
        {
            VerifyService(service);
            return service.CreateDocument(null, viewModel, null, null);
        }

        public static IDocument CreateDocument(this IDocumentManagerService service, string documentType, object viewModel)
        {
            VerifyService(service);
            return service.CreateDocument(documentType, viewModel, null, null);
        }

        public static IDocument CreateDocument(this IDocumentManagerService service, string documentType, object parameter, object parentViewModel)
        {
            VerifyService(service);
            return service.CreateDocument(documentType, null, parameter, parentViewModel);
        }

        [Obsolete("Use other extension methods."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static IDocument CreateDocument(this IDocumentManagerService service, string documentType, object parameter, object parentViewModel, bool useParameterAsViewModel)
        {
            VerifyService(service);
            return (!useParameterAsViewModel ? service.CreateDocument(documentType, null, parameter, parameter) : service.CreateDocument(documentType, parameter, null, parentViewModel));
        }

        public static void CreateDocumentIfNotExistsAndShow(this IDocumentManagerService service, ref IDocument documentStorage, string documentType, object parameter, object parentViewModel, object title)
        {
            VerifyService(service);
            if (documentStorage == null)
            {
                documentStorage = service.CreateDocument(documentType, parameter, parentViewModel);
                documentStorage.Title = title;
            }
            documentStorage.Show();
        }

        public static IDocument FindDocument(this IDocumentManagerService service, object viewModel)
        {
            VerifyService(service);
            return service.Documents.FirstOrDefault<IDocument>(d => (d.Content == viewModel));
        }

        public static IDocument FindDocument(this IDocumentManagerService service, object parameter, object parentViewModel)
        {
            VerifyService(service);
            return service.GetDocumentsByParentViewModel(parentViewModel).FirstOrDefault<IDocument>(delegate (IDocument d) {
                ISupportParameter content = d.Content as ISupportParameter;
                return ((content != null) && Equals(content.Parameter, parameter));
            });
        }

        public static IDocument FindDocumentById(this IDocumentManagerService service, object id)
        {
            VerifyService(service);
            return service.Documents.FirstOrDefault<IDocument>(x => Equals(x.Id, id));
        }

        public static IDocument FindDocumentByIdOrCreate(this IDocumentManagerService service, object id, Func<IDocumentManagerService, IDocument> createDocumentCallback)
        {
            VerifyService(service);
            IDocument document = service.FindDocumentById(id);
            if (document == null)
            {
                document = createDocumentCallback(service);
                document.Id = id;
            }
            return document;
        }

        public static IEnumerable<IDocument> GetDocumentsByParentViewModel(this IDocumentManagerService service, object parentViewModel)
        {
            VerifyService(service);
            return service.Documents.Where<IDocument>(delegate (IDocument d) {
                ISupportParentViewModel content = d.Content as ISupportParentViewModel;
                return ((content != null) && Equals(content.ParentViewModel, parentViewModel));
            });
        }

        private static void VerifyService(IDocumentManagerService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
        }
    }
}

