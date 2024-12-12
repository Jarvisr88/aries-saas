namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PdfEmbeddedFiles
    {
        private List<PdfFilespec> fileNames;
        private bool compressed;

        public PdfEmbeddedFiles(bool compressed)
        {
            this.compressed = compressed;
        }

        private void AssertState()
        {
            if (this.fileNames == null)
            {
                throw new InvalidOperationException();
            }
        }

        public void FillUp()
        {
            this.AssertState();
            foreach (PdfFilespec filespec in this.fileNames)
            {
                filespec.FillUp();
                filespec.EmbeddedFile.FillUp();
            }
        }

        private static byte[] GetData(PdfAttachment attachment)
        {
            if (attachment.Data != null)
            {
                return attachment.Data;
            }
            byte[] buffer = null;
            try
            {
                buffer = File.ReadAllBytes(attachment.FilePath);
                DateTime? modificationDate = attachment.ModificationDate;
                attachment.ModificationDate = new DateTime?((modificationDate != null) ? modificationDate.GetValueOrDefault() : File.GetLastWriteTime(attachment.FilePath));
                modificationDate = attachment.CreationDate;
                attachment.CreationDate = new DateTime?((modificationDate != null) ? modificationDate.GetValueOrDefault() : File.GetCreationTime(attachment.FilePath));
            }
            catch
            {
            }
            return buffer;
        }

        private static string GetName(PdfAttachment attachment) => 
            string.IsNullOrEmpty(attachment.FileName) ? (string.IsNullOrEmpty(attachment.FilePath) ? null : Path.GetFileName(attachment.FilePath)) : attachment.FileName;

        public void Register(PdfXRef xRef)
        {
            this.AssertState();
            foreach (PdfFilespec filespec in this.fileNames)
            {
                filespec.Register(xRef);
                filespec.EmbeddedFile.Register(xRef);
            }
        }

        internal void SetAttachments(ICollection<PdfAttachment> attachments)
        {
            this.fileNames = new List<PdfFilespec>();
            if (attachments.Count > 0)
            {
                foreach (PdfAttachment attachment in attachments)
                {
                    string str;
                    if (string.IsNullOrEmpty(GetName(attachment)))
                    {
                        str = "Attachment";
                    }
                    byte[] data = GetData(attachment);
                    if (data != null)
                    {
                        PdfEmbeddedFile file1 = new PdfEmbeddedFile(this.compressed);
                        PdfEmbeddedFile file2 = new PdfEmbeddedFile(this.compressed);
                        file2.Subtype = string.IsNullOrEmpty(attachment.Type) ? "application/octet-stream" : attachment.Type;
                        PdfEmbeddedFile local1 = file2;
                        local1.CreationDate = attachment.CreationDate;
                        DateTime? modificationDate = attachment.ModificationDate;
                        local1.ModificationDate = new DateTime?((modificationDate != null) ? modificationDate.GetValueOrDefault() : DateTimeHelper.Now);
                        PdfEmbeddedFile local2 = local1;
                        local2.Data = data;
                        PdfEmbeddedFile file = local2;
                        PdfFilespec filespec1 = new PdfFilespec();
                        filespec1.Name = str;
                        filespec1.Description = attachment.Description;
                        filespec1.Relationship = attachment.Relationship;
                        filespec1.EmbeddedFile = file;
                        PdfFilespec item = filespec1;
                        this.fileNames.Add(item);
                    }
                }
            }
        }

        public void Write(StreamWriter writer)
        {
            this.AssertState();
            foreach (PdfFilespec filespec in this.fileNames)
            {
                filespec.Write(writer);
                filespec.EmbeddedFile.Write(writer);
            }
        }

        public PdfArray AFArray
        {
            get
            {
                this.AssertState();
                PdfArray array = new PdfArray();
                foreach (PdfFilespec filespec in this.fileNames)
                {
                    array.Add(filespec.InnerObject);
                }
                return array;
            }
        }

        public PdfDictionary NamesDictionary
        {
            get
            {
                this.AssertState();
                PdfArray array = new PdfArray();
                foreach (PdfFilespec filespec in this.fileNames)
                {
                    array.Add(new PdfLiteralString(filespec.Name));
                    array.Add(filespec.InnerObject);
                }
                PdfDictionary dictionary = new PdfDictionary();
                dictionary.Add("Names", array);
                PdfDictionary dictionary2 = new PdfDictionary();
                dictionary2.Add("EmbeddedFiles", dictionary);
                return dictionary2;
            }
        }

        public bool Active =>
            (this.fileNames != null) && (this.fileNames.Count > 0);
    }
}

