namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Pdf;
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfDocumentProperties : BindableBase, IPdfDocumentProperties
    {
        private string fileName;
        private string title;
        private string author;
        private string subject;
        private string keywords;
        private DateTimeOffset? created;
        private DateTimeOffset? modified;
        private string application;
        private string producer;
        private string version;
        private string location;
        private long fileSize;
        private int numberOfPages;
        private string pageSize;

        public PdfDocumentProperties(IPdfDocument document)
        {
            PdfDocumentViewModel model = (PdfDocumentViewModel) document;
            PdfDocument pdfDocument = model.PdfDocument;
            string filePath = model.FilePath;
            if (!string.IsNullOrEmpty(model.FilePath))
            {
                this.FileName = Path.GetFileName(filePath);
                this.Location = Path.GetDirectoryName(filePath);
            }
            this.FileSize = model.FileSize;
            switch (pdfDocument.Version)
            {
                case PdfFileVersion.Pdf_1_0:
                    this.Version = "1.0";
                    break;

                case PdfFileVersion.Pdf_1_1:
                    this.Version = "1.1";
                    break;

                case PdfFileVersion.Pdf_1_2:
                    this.Version = "1.2";
                    break;

                case PdfFileVersion.Pdf_1_3:
                    this.Version = "1.3";
                    break;

                case PdfFileVersion.Pdf_1_4:
                    this.Version = "1.4";
                    break;

                case PdfFileVersion.Pdf_1_5:
                    this.Version = "1.5";
                    break;

                case PdfFileVersion.Pdf_1_6:
                    this.Version = "1.6";
                    break;

                case PdfFileVersion.Pdf_1_7:
                    this.Version = "1.7";
                    break;

                default:
                    break;
            }
            this.Title = pdfDocument.Title;
            this.Author = pdfDocument.Author;
            this.Subject = pdfDocument.Subject;
            this.Keywords = pdfDocument.Keywords;
            this.Application = pdfDocument.Creator;
            this.Producer = pdfDocument.Producer;
            if (pdfDocument.CreationDate != null)
            {
                this.Created = new DateTimeOffset?(pdfDocument.CreationDate.Value);
            }
            if (pdfDocument.ModDate != null)
            {
                this.Modified = new DateTimeOffset?(pdfDocument.ModDate.Value);
            }
            this.NumberOfPages = model.Pages.Count;
        }

        public string FileName
        {
            get => 
                this.fileName;
            internal set => 
                base.SetProperty<string>(ref this.fileName, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_FileName)), new ParameterExpression[0]));
        }

        public string Title
        {
            get => 
                this.title;
            internal set => 
                base.SetProperty<string>(ref this.title, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Title)), new ParameterExpression[0]));
        }

        public string Author
        {
            get => 
                this.author;
            internal set => 
                base.SetProperty<string>(ref this.author, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Author)), new ParameterExpression[0]));
        }

        public string Subject
        {
            get => 
                this.subject;
            internal set => 
                base.SetProperty<string>(ref this.subject, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Subject)), new ParameterExpression[0]));
        }

        public string Keywords
        {
            get => 
                this.keywords;
            internal set => 
                base.SetProperty<string>(ref this.keywords, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Keywords)), new ParameterExpression[0]));
        }

        public DateTimeOffset? Created
        {
            get => 
                this.created;
            internal set => 
                base.SetProperty<DateTimeOffset?>(ref this.created, value, Expression.Lambda<Func<DateTimeOffset?>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Created)), new ParameterExpression[0]));
        }

        public DateTimeOffset? Modified
        {
            get => 
                this.modified;
            internal set => 
                base.SetProperty<DateTimeOffset?>(ref this.modified, value, Expression.Lambda<Func<DateTimeOffset?>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Modified)), new ParameterExpression[0]));
        }

        public string Application
        {
            get => 
                this.application;
            internal set => 
                base.SetProperty<string>(ref this.application, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Application)), new ParameterExpression[0]));
        }

        public string Producer
        {
            get => 
                this.producer;
            internal set => 
                base.SetProperty<string>(ref this.producer, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Producer)), new ParameterExpression[0]));
        }

        public string Version
        {
            get => 
                this.version;
            internal set => 
                base.SetProperty<string>(ref this.version, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Version)), new ParameterExpression[0]));
        }

        public string Location
        {
            get => 
                this.location;
            internal set => 
                base.SetProperty<string>(ref this.location, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_Location)), new ParameterExpression[0]));
        }

        public long FileSize
        {
            get => 
                this.fileSize;
            internal set => 
                base.SetProperty<long>(ref this.fileSize, value, Expression.Lambda<Func<long>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_FileSize)), new ParameterExpression[0]));
        }

        public int NumberOfPages
        {
            get => 
                this.numberOfPages;
            internal set => 
                base.SetProperty<int>(ref this.numberOfPages, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_NumberOfPages)), new ParameterExpression[0]));
        }

        public string PageSize
        {
            get => 
                this.pageSize;
            internal set => 
                base.SetProperty<string>(ref this.pageSize, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(PdfDocumentProperties)), (MethodInfo) methodof(PdfDocumentProperties.get_PageSize)), new ParameterExpression[0]));
        }
    }
}

