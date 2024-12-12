namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public interface IParametersRenamer
    {
        void RenameParameter(string oldName, string newName);
        void RenameParameters(IDictionary<string, string> renamingMap);
    }
}

