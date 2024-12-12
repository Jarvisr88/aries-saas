namespace DevExpress.Office.Forms
{
    using DevExpress.Office.PInvoke;
    using System;
    using System.Windows.Forms;

    public class EditSecurityForm
    {
        public void ShowDialog(IWin32Window parent, ISecurityInformation securityInformation)
        {
            Win32.EditSecurity(parent, securityInformation);
        }
    }
}

