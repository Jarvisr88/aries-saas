namespace DevExpress.Office
{
    using System;

    public class OpenXmlRelation
    {
        private string id;
        private string target;
        private string type;
        private string targetMode;

        public OpenXmlRelation()
        {
        }

        public OpenXmlRelation(string id, string target, string type) : this(id, target, type, null)
        {
        }

        public OpenXmlRelation(string id, string target, string type, string targetMode)
        {
            this.id = id;
            this.target = target;
            this.type = type;
            this.targetMode = targetMode;
        }

        public string Id
        {
            get => 
                this.id;
            set => 
                this.id = value;
        }

        public string Target
        {
            get => 
                this.target;
            set => 
                this.target = value;
        }

        public string Type
        {
            get => 
                this.type;
            set => 
                this.type = value;
        }

        public string TargetMode
        {
            get => 
                this.targetMode;
            set => 
                this.targetMode = value;
        }
    }
}

