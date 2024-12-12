namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using System;

    public static class OpenXmlImportRelationHelper
    {
        public static string CalculateRelationTarget(OpenXmlRelation relation, string rootFolder, string defaultFileName) => 
            (relation != null) ? (!relation.Target.StartsWith("..", StringComparison.Ordinal) ? (!relation.Target.StartsWith("/", StringComparison.Ordinal) ? (!string.IsNullOrEmpty(rootFolder) ? (rootFolder + "/" + relation.Target) : relation.Target) : relation.Target) : (rootFolder + relation.Target.Substring(2))) : (!string.IsNullOrEmpty(defaultFileName) ? (!string.IsNullOrEmpty(rootFolder) ? (rootFolder + "/" + defaultFileName) : defaultFileName) : string.Empty);

        public static string LookupRelationTargetById(OpenXmlRelationCollection relations, string id, string rootFolder, string defaultFileName) => 
            CalculateRelationTarget(relations.LookupRelationById(id), rootFolder, defaultFileName);

        public static string LookupRelationTargetByType(OpenXmlRelationCollection relations, string type, string rootFolder, string defaultFileName) => 
            CalculateRelationTarget(relations.LookupRelationByType(type), rootFolder, defaultFileName);
    }
}

