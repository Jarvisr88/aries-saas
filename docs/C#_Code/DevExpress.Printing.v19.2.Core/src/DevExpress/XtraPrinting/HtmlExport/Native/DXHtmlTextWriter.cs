namespace DevExpress.XtraPrinting.HtmlExport.Native
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DXHtmlTextWriter : TextWriter
    {
        private static Dictionary<string, DXHtmlTextWriterAttribute> attributeLookup;
        private List<RenderAttribute> attributesList;
        private static AttributeInformation[] attributeNameLookup;
        private Layout currentLayout;
        private Layout currentWrittenLayout;
        private List<TagStackEntry> endTags;
        private int inlineCount;
        private bool isDescendant;
        private List<DXWebRenderStyle> styleList;
        private int tagIndex;
        private DXHtmlTextWriterTag tagKey;
        private static Dictionary<string, DXHtmlTextWriterTag> tagsLookup = new Dictionary<string, DXHtmlTextWriterTag>();
        private string tagName;
        private static TagInformation[] tagNameLookup = new TagInformation[0x61];
        public const string DefaultTabString = "\t";
        internal const string DesignerRegionAttributeName = "_designerRegion";
        public const char DoubleQuoteChar = '"';
        public const string EndTagLeftChars = "</";
        public const char EqualsChar = '=';
        public const string EqualsDoubleQuoteString = "=\"";
        private int indentLevel;
        public const string SelfClosingChars = " /";
        public const string SelfClosingTagEnd = " />";
        public const char SemicolonChar = ';';
        public const char SingleQuoteChar = '\'';
        public const char SlashChar = '/';
        public const char SpaceChar = ' ';
        public const char StyleEqualsChar = ':';
        private bool tabsPending;
        private string tabString;
        public const char TagLeftChar = '<';
        public const char TagRightChar = '>';
        private TextWriter writer;

        static DXHtmlTextWriter()
        {
            RegisterTag(string.Empty, DXHtmlTextWriterTag.Unknown, TagType.Other);
            RegisterTag("a", DXHtmlTextWriterTag.A, TagType.Inline);
            RegisterTag("acronym", DXHtmlTextWriterTag.Acronym, TagType.Inline);
            RegisterTag("address", DXHtmlTextWriterTag.Address, TagType.Other);
            RegisterTag("area", DXHtmlTextWriterTag.Area, TagType.NonClosing);
            RegisterTag("b", DXHtmlTextWriterTag.B, TagType.Inline);
            RegisterTag("base", DXHtmlTextWriterTag.Base, TagType.NonClosing);
            RegisterTag("basefont", DXHtmlTextWriterTag.Basefont, TagType.NonClosing);
            RegisterTag("bdo", DXHtmlTextWriterTag.Bdo, TagType.Inline);
            RegisterTag("bgsound", DXHtmlTextWriterTag.Bgsound, TagType.NonClosing);
            RegisterTag("big", DXHtmlTextWriterTag.Big, TagType.Inline);
            RegisterTag("blockquote", DXHtmlTextWriterTag.Blockquote, TagType.Other);
            RegisterTag("body", DXHtmlTextWriterTag.Body, TagType.Other);
            RegisterTag("br", DXHtmlTextWriterTag.Br, TagType.Other);
            RegisterTag("button", DXHtmlTextWriterTag.Button, TagType.Inline);
            RegisterTag("caption", DXHtmlTextWriterTag.Caption, TagType.Other);
            RegisterTag("center", DXHtmlTextWriterTag.Center, TagType.Other);
            RegisterTag("cite", DXHtmlTextWriterTag.Cite, TagType.Inline);
            RegisterTag("code", DXHtmlTextWriterTag.Code, TagType.Inline);
            RegisterTag("col", DXHtmlTextWriterTag.Col, TagType.NonClosing);
            RegisterTag("colgroup", DXHtmlTextWriterTag.Colgroup, TagType.Other);
            RegisterTag("del", DXHtmlTextWriterTag.Del, TagType.Inline);
            RegisterTag("dd", DXHtmlTextWriterTag.Dd, TagType.Inline);
            RegisterTag("dfn", DXHtmlTextWriterTag.Dfn, TagType.Inline);
            RegisterTag("dir", DXHtmlTextWriterTag.Dir, TagType.Other);
            RegisterTag("div", DXHtmlTextWriterTag.Div, TagType.Other);
            RegisterTag("dl", DXHtmlTextWriterTag.Dl, TagType.Other);
            RegisterTag("dt", DXHtmlTextWriterTag.Dt, TagType.Inline);
            RegisterTag("em", DXHtmlTextWriterTag.Em, TagType.Inline);
            RegisterTag("embed", DXHtmlTextWriterTag.Embed, TagType.NonClosing);
            RegisterTag("fieldset", DXHtmlTextWriterTag.Fieldset, TagType.Other);
            RegisterTag("font", DXHtmlTextWriterTag.Font, TagType.Inline);
            RegisterTag("form", DXHtmlTextWriterTag.Form, TagType.Other);
            RegisterTag("frame", DXHtmlTextWriterTag.Frame, TagType.NonClosing);
            RegisterTag("frameset", DXHtmlTextWriterTag.Frameset, TagType.Other);
            RegisterTag("h1", DXHtmlTextWriterTag.H1, TagType.Other);
            RegisterTag("h2", DXHtmlTextWriterTag.H2, TagType.Other);
            RegisterTag("h3", DXHtmlTextWriterTag.H3, TagType.Other);
            RegisterTag("h4", DXHtmlTextWriterTag.H4, TagType.Other);
            RegisterTag("h5", DXHtmlTextWriterTag.H5, TagType.Other);
            RegisterTag("h6", DXHtmlTextWriterTag.H6, TagType.Other);
            RegisterTag("head", DXHtmlTextWriterTag.Head, TagType.Other);
            RegisterTag("hr", DXHtmlTextWriterTag.Hr, TagType.NonClosing);
            RegisterTag("html", DXHtmlTextWriterTag.Html, TagType.Other);
            RegisterTag("i", DXHtmlTextWriterTag.I, TagType.Inline);
            RegisterTag("iframe", DXHtmlTextWriterTag.Iframe, TagType.Other);
            RegisterTag("img", DXHtmlTextWriterTag.Img, TagType.NonClosing);
            RegisterTag("input", DXHtmlTextWriterTag.Input, TagType.NonClosing);
            RegisterTag("ins", DXHtmlTextWriterTag.Ins, TagType.Inline);
            RegisterTag("isindex", DXHtmlTextWriterTag.Isindex, TagType.NonClosing);
            RegisterTag("kbd", DXHtmlTextWriterTag.Kbd, TagType.Inline);
            RegisterTag("label", DXHtmlTextWriterTag.Label, TagType.Inline);
            RegisterTag("legend", DXHtmlTextWriterTag.Legend, TagType.Other);
            RegisterTag("li", DXHtmlTextWriterTag.Li, TagType.Inline);
            RegisterTag("link", DXHtmlTextWriterTag.Link, TagType.NonClosing);
            RegisterTag("map", DXHtmlTextWriterTag.Map, TagType.Other);
            RegisterTag("marquee", DXHtmlTextWriterTag.Marquee, TagType.Other);
            RegisterTag("menu", DXHtmlTextWriterTag.Menu, TagType.Other);
            RegisterTag("meta", DXHtmlTextWriterTag.Meta, TagType.NonClosing);
            RegisterTag("nobr", DXHtmlTextWriterTag.Nobr, TagType.Inline);
            RegisterTag("noframes", DXHtmlTextWriterTag.Noframes, TagType.Other);
            RegisterTag("noscript", DXHtmlTextWriterTag.Noscript, TagType.Other);
            RegisterTag("object", DXHtmlTextWriterTag.Object, TagType.Other);
            RegisterTag("ol", DXHtmlTextWriterTag.Ol, TagType.Other);
            RegisterTag("option", DXHtmlTextWriterTag.Option, TagType.Other);
            RegisterTag("p", DXHtmlTextWriterTag.P, TagType.Inline);
            RegisterTag("param", DXHtmlTextWriterTag.Param, TagType.Other);
            RegisterTag("pre", DXHtmlTextWriterTag.Pre, TagType.Other);
            RegisterTag("ruby", DXHtmlTextWriterTag.Ruby, TagType.Other);
            RegisterTag("rt", DXHtmlTextWriterTag.Rt, TagType.Other);
            RegisterTag("q", DXHtmlTextWriterTag.Q, TagType.Inline);
            RegisterTag("s", DXHtmlTextWriterTag.S, TagType.Inline);
            RegisterTag("samp", DXHtmlTextWriterTag.Samp, TagType.Inline);
            RegisterTag("script", DXHtmlTextWriterTag.Script, TagType.Other);
            RegisterTag("select", DXHtmlTextWriterTag.Select, TagType.Other);
            RegisterTag("small", DXHtmlTextWriterTag.Small, TagType.Other);
            RegisterTag("span", DXHtmlTextWriterTag.Span, TagType.Inline);
            RegisterTag("strike", DXHtmlTextWriterTag.Strike, TagType.Inline);
            RegisterTag("strong", DXHtmlTextWriterTag.Strong, TagType.Inline);
            RegisterTag("style", DXHtmlTextWriterTag.Style, TagType.Other);
            RegisterTag("sub", DXHtmlTextWriterTag.Sub, TagType.Inline);
            RegisterTag("sup", DXHtmlTextWriterTag.Sup, TagType.Inline);
            RegisterTag("table", DXHtmlTextWriterTag.Table, TagType.Other);
            RegisterTag("tbody", DXHtmlTextWriterTag.Tbody, TagType.Other);
            RegisterTag("td", DXHtmlTextWriterTag.Td, TagType.Inline);
            RegisterTag("textarea", DXHtmlTextWriterTag.Textarea, TagType.Inline);
            RegisterTag("tfoot", DXHtmlTextWriterTag.Tfoot, TagType.Other);
            RegisterTag("th", DXHtmlTextWriterTag.Th, TagType.Inline);
            RegisterTag("thead", DXHtmlTextWriterTag.Thead, TagType.Other);
            RegisterTag("title", DXHtmlTextWriterTag.Title, TagType.Other);
            RegisterTag("tr", DXHtmlTextWriterTag.Tr, TagType.Other);
            RegisterTag("tt", DXHtmlTextWriterTag.Tt, TagType.Inline);
            RegisterTag("u", DXHtmlTextWriterTag.U, TagType.Inline);
            RegisterTag("ul", DXHtmlTextWriterTag.Ul, TagType.Other);
            RegisterTag("var", DXHtmlTextWriterTag.Var, TagType.Inline);
            RegisterTag("wbr", DXHtmlTextWriterTag.Wbr, TagType.NonClosing);
            RegisterTag("xml", DXHtmlTextWriterTag.Xml, TagType.Other);
            attributeLookup = new Dictionary<string, DXHtmlTextWriterAttribute>();
            attributeNameLookup = new AttributeInformation[0x36];
            RegisterAttribute("abbr", DXHtmlTextWriterAttribute.Abbr, true);
            RegisterAttribute("accesskey", DXHtmlTextWriterAttribute.Accesskey, true);
            RegisterAttribute("align", DXHtmlTextWriterAttribute.Align, false);
            RegisterAttribute("alt", DXHtmlTextWriterAttribute.Alt, true);
            RegisterAttribute("autocomplete", DXHtmlTextWriterAttribute.AutoComplete, false);
            RegisterAttribute("axis", DXHtmlTextWriterAttribute.Axis, true);
            RegisterAttribute("background", DXHtmlTextWriterAttribute.Background, true, true);
            RegisterAttribute("bgcolor", DXHtmlTextWriterAttribute.Bgcolor, false);
            RegisterAttribute("border", DXHtmlTextWriterAttribute.Border, false);
            RegisterAttribute("bordercolor", DXHtmlTextWriterAttribute.Bordercolor, false);
            RegisterAttribute("cellpadding", DXHtmlTextWriterAttribute.Cellpadding, false);
            RegisterAttribute("cellspacing", DXHtmlTextWriterAttribute.Cellspacing, false);
            RegisterAttribute("checked", DXHtmlTextWriterAttribute.Checked, false);
            RegisterAttribute("class", DXHtmlTextWriterAttribute.Class, true);
            RegisterAttribute("cols", DXHtmlTextWriterAttribute.Cols, false);
            RegisterAttribute("colspan", DXHtmlTextWriterAttribute.Colspan, false);
            RegisterAttribute("content", DXHtmlTextWriterAttribute.Content, true);
            RegisterAttribute("coords", DXHtmlTextWriterAttribute.Coords, false);
            RegisterAttribute("dir", DXHtmlTextWriterAttribute.Dir, false);
            RegisterAttribute("disabled", DXHtmlTextWriterAttribute.Disabled, false);
            RegisterAttribute("for", DXHtmlTextWriterAttribute.For, false);
            RegisterAttribute("headers", DXHtmlTextWriterAttribute.Headers, true);
            RegisterAttribute("height", DXHtmlTextWriterAttribute.Height, false);
            RegisterAttribute("href", DXHtmlTextWriterAttribute.Href, true, true);
            RegisterAttribute("id", DXHtmlTextWriterAttribute.Id, false);
            RegisterAttribute("line", DXHtmlTextWriterAttribute.Line, false);
            RegisterAttribute("longdesc", DXHtmlTextWriterAttribute.Longdesc, true, true);
            RegisterAttribute("maxlength", DXHtmlTextWriterAttribute.MaxLength, false);
            RegisterAttribute("multiple", DXHtmlTextWriterAttribute.Multiple, false);
            RegisterAttribute("name", DXHtmlTextWriterAttribute.Name, false);
            RegisterAttribute("nowrap", DXHtmlTextWriterAttribute.Nowrap, false);
            RegisterAttribute("onclick", DXHtmlTextWriterAttribute.Onclick, true);
            RegisterAttribute("onchange", DXHtmlTextWriterAttribute.Onchange, true);
            RegisterAttribute("readonly", DXHtmlTextWriterAttribute.ReadOnly, false);
            RegisterAttribute("rel", DXHtmlTextWriterAttribute.Rel, false);
            RegisterAttribute("rows", DXHtmlTextWriterAttribute.Rows, false);
            RegisterAttribute("rowspan", DXHtmlTextWriterAttribute.Rowspan, false);
            RegisterAttribute("rules", DXHtmlTextWriterAttribute.Rules, false);
            RegisterAttribute("scope", DXHtmlTextWriterAttribute.Scope, false);
            RegisterAttribute("selected", DXHtmlTextWriterAttribute.Selected, false);
            RegisterAttribute("shape", DXHtmlTextWriterAttribute.Shape, false);
            RegisterAttribute("size", DXHtmlTextWriterAttribute.Size, false);
            RegisterAttribute("src", DXHtmlTextWriterAttribute.Src, true, true);
            RegisterAttribute("style", DXHtmlTextWriterAttribute.Style, false);
            RegisterAttribute("tabindex", DXHtmlTextWriterAttribute.Tabindex, false);
            RegisterAttribute("target", DXHtmlTextWriterAttribute.Target, false);
            RegisterAttribute("title", DXHtmlTextWriterAttribute.Title, true);
            RegisterAttribute("type", DXHtmlTextWriterAttribute.Type, false);
            RegisterAttribute("usemap", DXHtmlTextWriterAttribute.Usemap, false);
            RegisterAttribute("valign", DXHtmlTextWriterAttribute.Valign, false);
            RegisterAttribute("value", DXHtmlTextWriterAttribute.Value, true);
            RegisterAttribute("vcard_name", DXHtmlTextWriterAttribute.VCardName, false);
            RegisterAttribute("width", DXHtmlTextWriterAttribute.Width, false);
            RegisterAttribute("wrap", DXHtmlTextWriterAttribute.Wrap, false);
            RegisterAttribute("_designerRegion", DXHtmlTextWriterAttribute.DesignerRegion, false);
        }

        public DXHtmlTextWriter(TextWriter writer) : this(writer, "\t")
        {
        }

        public DXHtmlTextWriter(TextWriter writer, string tabString) : base(CultureInfo.InvariantCulture)
        {
            this.attributesList = new List<RenderAttribute>();
            this.currentLayout = new Layout(DXWebHorizontalAlign.NotSet, true);
            this.endTags = new List<TagStackEntry>(0x10);
            this.styleList = new List<DXWebRenderStyle>(20);
            this.writer = writer;
            this.tabString = tabString;
            this.indentLevel = 0;
            this.tabsPending = false;
            this.isDescendant = base.GetType() != typeof(DXHtmlTextWriter);
            this.inlineCount = 0;
            this.NewLine = "\r\n";
        }

        public virtual void AddAttribute(DXHtmlTextWriterAttribute key, string value)
        {
            int index = (int) key;
            if ((index >= 0) && (index < attributeNameLookup.Length))
            {
                AttributeInformation information = attributeNameLookup[index];
                this.AddAttribute(information.name, value, key, information.encode, information.isUrl);
            }
        }

        public virtual void AddAttribute(string name, string value)
        {
            DXHtmlTextWriterAttribute attributeKey = this.GetAttributeKey(name);
            value = this.EncodeAttributeValue(attributeKey, value);
            this.AddAttribute(name, value, attributeKey);
        }

        public virtual void AddAttribute(DXHtmlTextWriterAttribute key, string value, bool fEncode)
        {
            int index = (int) key;
            if ((index >= 0) && (index < attributeNameLookup.Length))
            {
                AttributeInformation information = attributeNameLookup[index];
                this.AddAttribute(information.name, value, key, fEncode, information.isUrl);
            }
        }

        protected virtual void AddAttribute(string name, string value, DXHtmlTextWriterAttribute key)
        {
            this.AddAttribute(name, value, key, false, false);
        }

        public virtual void AddAttribute(string name, string value, bool fEndode)
        {
            value = this.EncodeAttributeValue(value, fEndode);
            this.AddAttribute(name, value, this.GetAttributeKey(name));
        }

        private void AddAttribute(string name, string value, DXHtmlTextWriterAttribute key, bool encode, bool isUrl)
        {
            RenderAttribute item = new RenderAttribute {
                name = name,
                value = value,
                key = key,
                encode = encode,
                isUrl = isUrl
            };
            bool flag = false;
            int num = 0;
            while (true)
            {
                if (num < this.attributesList.Count)
                {
                    RenderAttribute attribute2 = this.attributesList[num];
                    if (attribute2.name != name)
                    {
                        num++;
                        continue;
                    }
                    this.attributesList[num] = item;
                    flag = true;
                }
                if (!flag)
                {
                    this.attributesList.Add(item);
                }
                return;
            }
        }

        public virtual void AddStyleAttribute(DXHtmlTextWriterStyle key, string value)
        {
            this.AddStyleAttribute(DXCssTextWriter.GetStyleName(key), value, key);
        }

        public virtual void AddStyleAttribute(string name, string value)
        {
            this.AddStyleAttribute(name, value, DXCssTextWriter.GetStyleKey(name));
        }

        protected virtual void AddStyleAttribute(string name, string value, DXHtmlTextWriterStyle key)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DXWebRenderStyle style;
                style.name = name;
                style.key = key;
                string str = value;
                if (DXCssTextWriter.IsStyleEncoded(key))
                {
                    str = DXHttpUtility.HtmlAttributeEncode(value);
                }
                style.value = str;
                this.styleList.Add(style);
            }
        }

        public virtual void BeginRender()
        {
        }

        public override void Close()
        {
            this.writer.Close();
            base.Close();
        }

        protected virtual string EncodeAttributeValue(DXHtmlTextWriterAttribute attrKey, string value)
        {
            bool fEncode = true;
            if ((DXHtmlTextWriterAttribute.Accesskey <= attrKey) && (attrKey < attributeNameLookup.Length))
            {
                fEncode = attributeNameLookup[(int) attrKey].encode;
            }
            return this.EncodeAttributeValue(value, fEncode);
        }

        protected string EncodeAttributeValue(string value, bool fEncode) => 
            (value != null) ? (fEncode ? DXHttpUtility.HtmlAttributeEncode(value) : value) : null;

        protected string EncodeUrl(string url) => 
            DXWebStringUtil.IsUncSharePath(url) ? url : DXHttpUtility.UrlPathEncode(url);

        public virtual void EndRender()
        {
        }

        public virtual void EnterStyle(DXWebStyle style)
        {
            this.EnterStyle(style, DXHtmlTextWriterTag.Span);
        }

        public virtual void EnterStyle(DXWebStyle style, DXHtmlTextWriterTag tag)
        {
            if (!style.IsEmpty || (tag != DXHtmlTextWriterTag.Span))
            {
                style.AddAttributesToRender(this);
                this.RenderBeginTag(tag);
            }
        }

        public virtual void ExitStyle(DXWebStyle style)
        {
            this.ExitStyle(style, DXHtmlTextWriterTag.Span);
        }

        public virtual void ExitStyle(DXWebStyle style, DXHtmlTextWriterTag tag)
        {
            if (!style.IsEmpty || (tag != DXHtmlTextWriterTag.Span))
            {
                this.RenderEndTag();
            }
        }

        protected virtual void FilterAttributes()
        {
            int index = 0;
            for (int i = 0; i < this.styleList.Count; i++)
            {
                DXWebRenderStyle style = this.styleList[i];
                if (this.OnStyleAttributeRender(style.name, style.value, style.key))
                {
                    this.styleList[index++] = style;
                }
            }
            this.styleList.RemoveRange(index, this.styleList.Count - index);
            int num2 = 0;
            for (int j = 0; j < this.attributesList.Count; j++)
            {
                RenderAttribute attribute = this.attributesList[j];
                if (this.OnAttributeRender(attribute.name, attribute.value, attribute.key))
                {
                    this.attributesList[num2++] = attribute;
                }
            }
        }

        public override void Flush()
        {
            this.writer.Flush();
        }

        protected DXHtmlTextWriterAttribute GetAttributeKey(string attrName)
        {
            DXHtmlTextWriterAttribute attribute;
            return ((string.IsNullOrEmpty(attrName) || !attributeLookup.TryGetValue(attrName.ToLowerInvariant(), out attribute)) ? ~DXHtmlTextWriterAttribute.Accesskey : attribute);
        }

        protected string GetAttributeName(DXHtmlTextWriterAttribute attrKey) => 
            ((attrKey < DXHtmlTextWriterAttribute.Accesskey) || (attrKey >= attributeNameLookup.Length)) ? string.Empty : attributeNameLookup[(int) attrKey].name;

        protected DXHtmlTextWriterStyle GetStyleKey(string styleName) => 
            DXCssTextWriter.GetStyleKey(styleName);

        protected string GetStyleName(DXHtmlTextWriterStyle styleKey) => 
            DXCssTextWriter.GetStyleName(styleKey);

        protected virtual DXHtmlTextWriterTag GetTagKey(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
            {
                object obj2 = tagsLookup[tagName.ToLowerInvariant()];
                if (obj2 != null)
                {
                    return (DXHtmlTextWriterTag) obj2;
                }
            }
            return DXHtmlTextWriterTag.Unknown;
        }

        protected virtual string GetTagName(DXHtmlTextWriterTag tagKey)
        {
            int index = (int) tagKey;
            return (((index < 0) || (index >= tagNameLookup.Length)) ? string.Empty : tagNameLookup[index].name);
        }

        protected bool IsAttributeDefined(DXHtmlTextWriterAttribute key)
        {
            bool flag;
            using (List<RenderAttribute>.Enumerator enumerator = this.attributesList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        RenderAttribute current = enumerator.Current;
                        if (current.key != key)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected bool IsAttributeDefined(DXHtmlTextWriterAttribute key, out string value)
        {
            bool flag;
            value = null;
            using (List<RenderAttribute>.Enumerator enumerator = this.attributesList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        RenderAttribute current = enumerator.Current;
                        if (current.key != key)
                        {
                            continue;
                        }
                        value = current.value;
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected bool IsStyleAttributeDefined(DXHtmlTextWriterStyle key)
        {
            bool flag;
            using (List<DXWebRenderStyle>.Enumerator enumerator = this.styleList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DXWebRenderStyle current = enumerator.Current;
                        if (current.key != key)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected bool IsStyleAttributeDefined(DXHtmlTextWriterStyle key, out string value)
        {
            bool flag;
            value = null;
            using (List<DXWebRenderStyle>.Enumerator enumerator = this.styleList.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DXWebRenderStyle current = enumerator.Current;
                        if (current.key != key)
                        {
                            continue;
                        }
                        value = current.value;
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public virtual bool IsValidFormAttribute(string attribute) => 
            true;

        protected virtual bool OnAttributeRender(string name, string value, DXHtmlTextWriterAttribute key) => 
            true;

        protected virtual bool OnStyleAttributeRender(string name, string value, DXHtmlTextWriterStyle key) => 
            true;

        protected virtual bool OnTagRender(string name, DXHtmlTextWriterTag key) => 
            true;

        internal virtual void OpenDiv()
        {
            this.OpenDiv(this.currentLayout, (this.currentLayout != null) && (this.currentLayout.Align != DXWebHorizontalAlign.NotSet), (this.currentLayout != null) && !this.currentLayout.Wrap);
        }

        private void OpenDiv(Layout layout, bool writeHorizontalAlign, bool writeWrapping)
        {
            this.WriteBeginTag("div");
            if (writeHorizontalAlign)
            {
                DXWebHorizontalAlign align = layout.Align;
                string str = (align == DXWebHorizontalAlign.Center) ? "text-align:center" : ((align == DXWebHorizontalAlign.Right) ? "text-align:right" : "text-align:left");
                this.WriteAttribute("style", str);
            }
            if (writeWrapping)
            {
                this.WriteAttribute("mode", layout.Wrap ? "wrap" : "nowrap");
            }
            this.Write('>');
            this.currentWrittenLayout = layout;
        }

        protected virtual void OutputTabs()
        {
            if (this.tabsPending)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.indentLevel)
                    {
                        this.tabsPending = false;
                        break;
                    }
                    this.writer.Write(this.tabString);
                    num++;
                }
            }
        }

        protected string PopEndTag()
        {
            if (this.endTags.Count <= 0)
            {
                throw new InvalidOperationException("HTMLTextWriterUnbalancedPop");
            }
            TagStackEntry entry = this.endTags[this.endTags.Count - 1];
            this.endTags.RemoveAt(this.endTags.Count - 1);
            this.TagKey = entry.tagKey;
            return entry.endTagText;
        }

        protected void PushEndTag(string endTag)
        {
            TagStackEntry item = new TagStackEntry {
                tagKey = this.tagKey,
                endTagText = endTag
            };
            this.endTags.Add(item);
        }

        protected static void RegisterAttribute(string name, DXHtmlTextWriterAttribute key)
        {
            RegisterAttribute(name, key, false);
        }

        private static void RegisterAttribute(string name, DXHtmlTextWriterAttribute key, bool encode)
        {
            RegisterAttribute(name, key, encode, false);
        }

        private static void RegisterAttribute(string name, DXHtmlTextWriterAttribute key, bool encode, bool isUrl)
        {
            string str = name.ToLowerInvariant();
            attributeLookup.Add(str, key);
            if (key < attributeNameLookup.Length)
            {
                attributeNameLookup[(int) key] = new AttributeInformation(name, encode, isUrl);
            }
        }

        protected static void RegisterStyle(string name, DXHtmlTextWriterStyle key)
        {
            DXCssTextWriter.RegisterAttribute(name, key);
        }

        protected static void RegisterTag(string name, DXHtmlTextWriterTag key)
        {
            RegisterTag(name, key, TagType.Other);
        }

        private static void RegisterTag(string name, DXHtmlTextWriterTag key, TagType type)
        {
            string str = name.ToLowerInvariant();
            tagsLookup.Add(str, key);
            string closingTag = null;
            if ((type != TagType.NonClosing) && (key != DXHtmlTextWriterTag.Unknown))
            {
                closingTag = $"</{str}>";
            }
            if (key < tagNameLookup.Length)
            {
                tagNameLookup[(int) key] = new TagInformation(name, type, closingTag);
            }
        }

        protected virtual string RenderAfterContent() => 
            null;

        protected virtual string RenderAfterTag() => 
            null;

        protected virtual string RenderBeforeContent() => 
            null;

        protected virtual string RenderBeforeTag() => 
            null;

        public virtual void RenderBeginTag(DXHtmlTextWriterTag tagKey)
        {
            this.TagKey = tagKey;
            bool flag = this.TagKey != DXHtmlTextWriterTag.Unknown;
            if (this.isDescendant)
            {
                flag = this.OnTagRender(this.tagName, this.tagKey);
                this.FilterAttributes();
                string str3 = this.RenderBeforeTag();
                if (str3 != null)
                {
                    if (this.tabsPending)
                    {
                        this.OutputTabs();
                    }
                    this.writer.Write(str3);
                }
            }
            TagInformation information = tagNameLookup[this.tagIndex];
            TagType tagType = information.tagType;
            bool flag2 = flag && (tagType != TagType.NonClosing);
            string endTag = flag2 ? information.closingTag : null;
            if (flag)
            {
                if (this.tabsPending)
                {
                    this.OutputTabs();
                }
                this.writer.Write('<');
                this.writer.Write(this.tagName);
                string str4 = null;
                foreach (RenderAttribute attribute in this.attributesList)
                {
                    if (attribute.key == DXHtmlTextWriterAttribute.Style)
                    {
                        str4 = attribute.value;
                        continue;
                    }
                    this.writer.Write(' ');
                    this.writer.Write(attribute.name);
                    if (attribute.value != null)
                    {
                        this.writer.Write("=\"");
                        string url = attribute.value;
                        if (attribute.isUrl && ((attribute.key != DXHtmlTextWriterAttribute.Href) || !url.StartsWith("javascript:", StringComparison.Ordinal)))
                        {
                            url = this.EncodeUrl(url);
                        }
                        if (attribute.encode)
                        {
                            this.WriteHtmlAttributeEncode(url);
                        }
                        else
                        {
                            this.writer.Write(url);
                        }
                        this.writer.Write('"');
                    }
                }
                if ((this.styleList.Count > 0) || (str4 != null))
                {
                    this.writer.Write(' ');
                    this.writer.Write("style");
                    this.writer.Write("=\"");
                    DXCssTextWriter.WriteAttributes(this.writer, this.styleList);
                    if (str4 != null)
                    {
                        this.writer.Write(str4);
                    }
                    this.writer.Write('"');
                }
                if (tagType == TagType.NonClosing)
                {
                    this.writer.Write(" />");
                }
                else
                {
                    this.writer.Write('>');
                }
            }
            string str2 = this.RenderBeforeContent();
            if (str2 != null)
            {
                if (this.tabsPending)
                {
                    this.OutputTabs();
                }
                this.writer.Write(str2);
            }
            if (flag2)
            {
                if (tagType == TagType.Inline)
                {
                    this.inlineCount++;
                }
                else
                {
                    this.WriteLine();
                    int indent = this.Indent;
                    this.Indent = indent + 1;
                }
                endTag ??= $"</{this.tagName}>";
            }
            if (this.isDescendant)
            {
                string str6 = this.RenderAfterTag();
                if (str6 != null)
                {
                    endTag = (endTag == null) ? str6 : (str6 + endTag);
                }
                string str7 = this.RenderAfterContent();
                if (str7 != null)
                {
                    endTag = (endTag == null) ? str7 : (str7 + endTag);
                }
            }
            this.PushEndTag(endTag);
            this.styleList.Clear();
            this.attributesList.Clear();
        }

        public virtual void RenderBeginTag(string tagName)
        {
            this.TagName = tagName;
            this.RenderBeginTag(this.tagKey);
        }

        public virtual void RenderEndTag()
        {
            string str = this.PopEndTag();
            if (!string.IsNullOrEmpty(str))
            {
                if (tagNameLookup[this.tagIndex].tagType == TagType.Inline)
                {
                    this.inlineCount--;
                    this.Write(str);
                }
                else
                {
                    int indent = this.Indent;
                    this.Indent = indent - 1;
                    this.Write(str);
                    this.WriteLine();
                }
            }
        }

        public override void Write(bool value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(char value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(char[] buffer)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(buffer);
        }

        public override void Write(double value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(int value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(long value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(object value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(float value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(value);
        }

        public override void Write(string s)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(s);
        }

        public override void Write(string format, params object[] arg)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(format, arg);
        }

        public override void Write(string format, object arg0)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(format, arg0);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(buffer, index, count);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write(format, arg0, arg1);
        }

        public virtual void WriteAttribute(string name, string value)
        {
            this.WriteAttribute(name, value, false);
        }

        public virtual void WriteAttribute(string name, string value, bool fEncode)
        {
            this.writer.Write(' ');
            this.writer.Write(name);
            if (value != null)
            {
                this.writer.Write("=\"");
                if (fEncode)
                {
                    this.WriteHtmlAttributeEncode(value);
                }
                else
                {
                    this.writer.Write(value);
                }
                this.writer.Write('"');
            }
        }

        public virtual void WriteBeginTag(string tagName)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write('<');
            this.writer.Write(tagName);
        }

        public virtual void WriteBreak()
        {
            this.Write("<br />");
        }

        public virtual void WriteEncodedText(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            int length = text.Length;
            int startIndex = 0;
            while (startIndex < length)
            {
                int index = text.IndexOf('\x00a0', startIndex);
                if (index < 0)
                {
                    WebUtility.HtmlEncode((startIndex == 0) ? text : text.Substring(startIndex, length - startIndex), this);
                    startIndex = length;
                    continue;
                }
                if (index > startIndex)
                {
                    WebUtility.HtmlEncode(text.Substring(startIndex, index - startIndex), this);
                }
                this.Write("&nbsp;");
                startIndex = index + 1;
            }
        }

        public virtual void WriteEncodedUrl(string url)
        {
            int index = url.IndexOf('?');
            if (index == -1)
            {
                this.WriteUrlEncodedString(url, false);
            }
            else
            {
                this.WriteUrlEncodedString(url.Substring(0, index), false);
                this.Write(url.Substring(index));
            }
        }

        public virtual void WriteEncodedUrlParameter(string urlText)
        {
            this.WriteUrlEncodedString(urlText, true);
        }

        public virtual void WriteEndTag(string tagName)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write('<');
            this.writer.Write('/');
            this.writer.Write(tagName);
            this.writer.Write('>');
        }

        public virtual void WriteFullBeginTag(string tagName)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.Write('<');
            this.writer.Write(tagName);
            this.writer.Write('>');
        }

        internal void WriteHtmlAttributeEncode(string s)
        {
            DXHttpUtility.HtmlAttributeEncode(s, this.writer);
        }

        public override void WriteLine()
        {
            this.writer.WriteLine();
            this.tabsPending = true;
        }

        public override void WriteLine(bool value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(char value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(int value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(char[] buffer)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(buffer);
            this.tabsPending = true;
        }

        public override void WriteLine(double value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(long value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(object value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(float value)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(value);
            this.tabsPending = true;
        }

        public override void WriteLine(string s)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(s);
            this.tabsPending = true;
        }

        public override void WriteLine(string format, params object[] arg)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(format, arg);
            this.tabsPending = true;
        }

        public override void WriteLine(string format, object arg0)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(format, arg0);
            this.tabsPending = true;
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(buffer, index, count);
            this.tabsPending = true;
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            if (this.tabsPending)
            {
                this.OutputTabs();
            }
            this.writer.WriteLine(format, arg0, arg1);
            this.tabsPending = true;
        }

        public void WriteLineNoTabs(string s)
        {
            this.writer.WriteLine(s);
            this.tabsPending = true;
        }

        internal void WriteObsoleteBreak()
        {
            this.Write("<br>");
        }

        public virtual void WriteStyleAttribute(string name, string value)
        {
            this.WriteStyleAttribute(name, value, false);
        }

        public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
        {
            this.writer.Write(name);
            this.writer.Write(':');
            if (fEncode)
            {
                this.WriteHtmlAttributeEncode(value);
            }
            else
            {
                this.writer.Write(value);
            }
            this.writer.Write(';');
        }

        protected void WriteUrlEncodedString(string text, bool argument)
        {
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = text[i];
                if (DXHttpUtility.IsSafe(ch))
                {
                    this.Write(ch);
                }
                else if (!argument && ((ch == '/') || ((ch == ':') || ((ch == '#') || (ch == ',')))))
                {
                    this.Write(ch);
                }
                else if ((ch == ' ') & argument)
                {
                    this.Write('+');
                }
                else if ((ch & 0xff80) != 0)
                {
                    this.Write(DXHttpUtility.UrlEncodeNonAscii(char.ToString(ch), System.Text.Encoding.UTF8));
                }
                else
                {
                    this.Write('%');
                    this.Write(DXHttpUtility.IntToHex((ch >> 4) & '\x000f'));
                    this.Write(DXHttpUtility.IntToHex(ch & '\x000f'));
                }
            }
        }

        public override System.Text.Encoding Encoding =>
            this.writer.Encoding;

        public int Indent
        {
            get => 
                this.indentLevel;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                this.indentLevel = value;
            }
        }

        public TextWriter InnerWriter
        {
            get => 
                this.writer;
            set => 
                this.writer = value;
        }

        public override string NewLine
        {
            get => 
                this.writer.NewLine;
            set => 
                this.writer.NewLine = value;
        }

        internal virtual bool RenderDivAroundHiddenInputs =>
            true;

        protected DXHtmlTextWriterTag TagKey
        {
            get => 
                this.tagKey;
            set
            {
                this.tagIndex = (int) value;
                if ((this.tagIndex < 0) || (this.tagIndex >= tagNameLookup.Length))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.tagKey = value;
                if (value != DXHtmlTextWriterTag.Unknown)
                {
                    this.tagName = tagNameLookup[this.tagIndex].name;
                }
            }
        }

        protected string TagName
        {
            get => 
                this.tagName;
            set
            {
                this.tagName = value;
                this.tagKey = this.GetTagKey(this.tagName);
                this.tagIndex = (int) this.tagKey;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AttributeInformation
        {
            public string name;
            public bool isUrl;
            public bool encode;
            public AttributeInformation(string name, bool encode, bool isUrl)
            {
                this.name = name;
                this.encode = encode;
                this.isUrl = isUrl;
            }
        }

        internal class Layout
        {
            private DXWebHorizontalAlign align;
            private bool wrap;

            public Layout(DXWebHorizontalAlign alignment, bool wrapping)
            {
                this.Align = alignment;
                this.Wrap = wrapping;
            }

            public DXWebHorizontalAlign Align
            {
                get => 
                    this.align;
                set => 
                    this.align = value;
            }

            public bool Wrap
            {
                get => 
                    this.wrap;
                set => 
                    this.wrap = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RenderAttribute
        {
            public string name;
            public string value;
            public DXHtmlTextWriterAttribute key;
            public bool encode;
            public bool isUrl;
            public override string ToString() => 
                $"RenderAttribute [{this.name}:{this.value}]";
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TagInformation
        {
            public string name;
            public DXHtmlTextWriter.TagType tagType;
            public string closingTag;
            public TagInformation(string name, DXHtmlTextWriter.TagType tagType, string closingTag)
            {
                this.name = name;
                this.tagType = tagType;
                this.closingTag = closingTag;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TagStackEntry
        {
            public DXHtmlTextWriterTag tagKey;
            public string endTagText;
            public override string ToString() => 
                this.endTagText;
        }

        private enum TagType
        {
            Inline,
            NonClosing,
            Other
        }
    }
}

