namespace DevExpress.XtraPrinting.Export.Web
{
    using System;

    public class ScriptBlockControl : WebScriptControl, IScriptContainer
    {
        private WebStyleControl styleControl;

        public ScriptBlockControl(WebStyleControl styleControl)
        {
            this.styleControl = styleControl;
        }

        public override void ClearContent()
        {
            base.ClearContent();
            this.styleControl.ClearContent();
        }

        public bool IsClientScriptBlockRegistered(string key) => 
            base.scriptHT.ContainsKey(key);

        public void RegisterCommonCssStyle(string style, string tagName)
        {
            this.styleControl.AddTagStyle(style, tagName);
        }

        public virtual string RegisterCssClass(string style) => 
            this.styleControl.RegisterStyle(style);
    }
}

