namespace DevExpress.Office
{
    using System;
    using System.Collections.Generic;

    public class OpenXmlRelationCollection : List<OpenXmlRelation>
    {
        public string GenerateId() => 
            $"rId{base.Count + 1}";

        public OpenXmlRelation LookupRelationById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int count = base.Count;
                for (int i = 0; i < count; i++)
                {
                    if (base[i].Id == id)
                    {
                        return base[i];
                    }
                }
            }
            return null;
        }

        internal OpenXmlRelation LookupRelationByTargetAndType(string target, string type)
        {
            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(type))
            {
                int count = base.Count;
                for (int i = 0; i < count; i++)
                {
                    if ((base[i].Target == target) && (base[i].Type == type))
                    {
                        return base[i];
                    }
                }
            }
            return null;
        }

        public OpenXmlRelation LookupRelationByType(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                int count = base.Count;
                for (int i = 0; i < count; i++)
                {
                    if (base[i].Type == type)
                    {
                        return base[i];
                    }
                }
            }
            return null;
        }
    }
}

