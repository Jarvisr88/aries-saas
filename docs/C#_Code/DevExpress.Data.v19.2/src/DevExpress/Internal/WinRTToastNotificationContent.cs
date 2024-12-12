namespace DevExpress.Internal
{
    using DevExpress.Internal.WinApi;
    using DevExpress.Internal.WinApi.Window.Data.Xml.Dom;
    using DevExpress.Internal.WinApi.Windows.UI.Notifications;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class WinRTToastNotificationContent : IPredefinedToastNotificationContent, IDisposable, IPredefinedToastNotificationContentGeneric
    {
        private string[] lines = new string[3];
        private ToastTemplateType type;
        private string imagePath;
        internal string tempImagePath;
        private string appLogoImagePath;
        private string tempAppLogoImagePath;
        private string heroImagePath;
        private string tempHeroImagePath;
        private ImageCropType appLogoImageCrop;
        private PredefinedSound sound;
        private NotificationDuration duration;
        private string attributionText = string.Empty;
        private DateTimeOffset? displayTimestamp;
        private Action<XmlDocument> updateToastContent;

        private WinRTToastNotificationContent()
        {
        }

        private static IXmlNode AppendNode(IXmlNode parentNode, IXmlNode childNode)
        {
            IXmlNode node;
            ComFunctions.CheckHRESULT(parentNode.AppendChild(childNode, out node));
            return node;
        }

        private void CheckImagePath(string imagePath)
        {
            FileInfo info = new FileInfo(imagePath);
            if (info.Exists)
            {
                if (!this.IsSupportedExtension(info.Extension))
                {
                    throw new ArgumentException("Unsupported file type");
                }
                if ((info.Length > 0x32000L) && !WindowsVersionProvider.IsWin10FallCreatorsUpdateOrHigher)
                {
                    throw new ArgumentException("File must have size less or equal to 200 KB");
                }
            }
        }

        public static WinRTToastNotificationContent Create(string bodyText)
        {
            string[] lines = new string[] { bodyText };
            return Create(ToastTemplateType.ToastText01, lines);
        }

        private static WinRTToastNotificationContent Create(ToastTemplateType type, params string[] lines)
        {
            WinRTToastNotificationContent content1 = new WinRTToastNotificationContent();
            content1.type = type;
            content1.lines = lines;
            return content1;
        }

        private static IXmlElement CreateElement(IXmlDocument xmldoc, string elementName)
        {
            IXmlElement element;
            ComFunctions.CheckHRESULT(xmldoc.CreateElement(elementName, out element));
            return element;
        }

        private static IXmlNode CreateInlineImageNode(IXmlDocument content, string imagePath)
        {
            IXmlNode node = AppendNode(GetNode(content, "binding"), (IXmlNode) CreateElement(content, "image"));
            SetAttribute(node, "src", imagePath);
            return node;
        }

        public static WinRTToastNotificationContent CreateOneLineHeader(string headlineText, string bodyText)
        {
            string[] lines = new string[] { headlineText, bodyText };
            return Create(ToastTemplateType.ToastText02, lines);
        }

        public static WinRTToastNotificationContent CreateOneLineHeader(string headlineText, string bodyText1, string bodyText2)
        {
            string[] lines = new string[] { headlineText, bodyText1, bodyText2 };
            return Create(ToastTemplateType.ToastText04, lines);
        }

        public static WinRTToastNotificationContent CreateToastGeneric(string headlineText, string bodyText1, string bodyText2)
        {
            string[] lines = new string[] { headlineText, bodyText1, bodyText2 };
            return Create(ToastTemplateType.ToastGeneric, lines);
        }

        public static WinRTToastNotificationContent CreateTwoLineHeader(string headlineText, string bodyText)
        {
            string[] lines = new string[] { headlineText, bodyText };
            return Create(ToastTemplateType.ToastText03, lines);
        }

        public void Dispose()
        {
            this.RemoveTempFile(this.tempImagePath);
            this.tempImagePath = null;
            this.RemoveTempFile(this.tempAppLogoImagePath);
            this.tempAppLogoImagePath = null;
            this.RemoveTempFile(this.tempHeroImagePath);
            this.tempHeroImagePath = null;
            this.updateToastContent = null;
        }

        internal static IXmlDocument GetDocument(IToastNotificationManager manager, IPredefinedToastNotificationInfo info)
        {
            IXmlDocument templateContent = manager.GetTemplateContent(GetToastTemplateType(info));
            if (ToastNotificationManager.IsGenericTemplateSupported)
            {
                UpdateTemplate(templateContent, info);
                UpdateAttributionText(templateContent, info);
                UpdateDisplayTimestamp(templateContent, info);
                UpdateAppLogoImage(info, templateContent);
                UpdateHeroImage(info, templateContent);
            }
            UpdateText(templateContent, info);
            UpdateInlineImage(info, templateContent);
            UpdateSound(templateContent, info);
            UpdateDuration(templateContent, info);
            UpdateContent(templateContent, info);
            return templateContent;
        }

        private static IXmlNode GetNode(IXmlDocument xmldoc, string tagName) => 
            GetNode(GetNodes(xmldoc, tagName), 0);

        private static IXmlNode GetNode(IXmlNodeList nodes, uint index)
        {
            IXmlNode node;
            ComFunctions.CheckHRESULT(nodes.Item(index, out node));
            return node;
        }

        private static IXmlNodeList GetNodes(IXmlDocument xmldoc, string tagName)
        {
            IXmlNodeList list;
            ComFunctions.CheckHRESULT(xmldoc.GetElementsByTagName(tagName, out list));
            return list;
        }

        private static string GetTempPath()
        {
            string tempPath = Path.GetTempPath();
            string path = string.Empty;
            while (true)
            {
                path = $"{tempPath}{Guid.NewGuid()}.png";
                if (!File.Exists(path))
                {
                    return path;
                }
            }
        }

        private static ToastTemplateType GetToastTemplateType(IPredefinedToastNotificationInfo info) => 
            (info.ToastTemplateType == ToastTemplateType.ToastGeneric) ? ToastTemplateType.ToastText04 : info.ToastTemplateType;

        private static bool IsLoopingSound(PredefinedSound sound) => 
            sound >= PredefinedSound.Notification_Looping_Alarm;

        private bool IsSupportedExtension(string imagePath)
        {
            string[] textArray1 = new string[] { ".png", ".jpg", ".jpeg", ".gif" };
            string extension = Path.GetExtension(imagePath);
            foreach (string str2 in textArray1)
            {
                if (extension.Equals(str2, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveTempFile(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
            }
        }

        private void SaveImageToFile(Image image, string path)
        {
            image.Save(path, ImageFormat.Png);
        }

        public void SetAppLogoImageCrop(ImageCropType appLogoImageCrop)
        {
            this.appLogoImageCrop = appLogoImageCrop;
        }

        private static void SetAttribute(IXmlElement node, string attributeName, string attributeValue)
        {
            ComFunctions.CheckHRESULT(node.SetAttribute(attributeName, attributeValue));
        }

        private static void SetAttribute(IXmlNode node, string attributeName, string attributeValue)
        {
            SetAttribute((IXmlElement) node, attributeName, attributeValue);
        }

        private static void SetAttribute(IXmlDocument xmldoc, string tagName, string attributeName, string attributeValue)
        {
            SetAttribute(GetNode(xmldoc, tagName), attributeName, attributeValue);
        }

        public void SetAttributionText(string attributionText)
        {
            this.attributionText = attributionText;
        }

        public void SetDisplayTimestamp(DateTimeOffset? displayTimestamp)
        {
            this.displayTimestamp = displayTimestamp;
        }

        public void SetDuration(NotificationDuration duration)
        {
            this.duration = duration;
        }

        public void SetImage(byte[] image)
        {
            this.SetImage(new MemoryStream(image));
        }

        public void SetImage(Image image)
        {
            this.SetImage(image, ImagePlacement.Inline);
        }

        public void SetImage(Stream stream)
        {
            this.SetImage(Image.FromStream(stream));
        }

        public void SetImage(string imagePath)
        {
            this.SetImage(imagePath, ImagePlacement.Inline);
        }

        public void SetImage(Image image, ImagePlacement placement)
        {
            string tempPath = GetTempPath();
            switch (placement)
            {
                case ImagePlacement.Inline:
                    if (this.tempImagePath != null)
                    {
                        this.RemoveTempFile(this.tempImagePath);
                        this.tempImagePath = null;
                    }
                    this.tempImagePath = tempPath;
                    break;

                case ImagePlacement.Hero:
                    if (this.tempHeroImagePath != null)
                    {
                        this.RemoveTempFile(this.tempHeroImagePath);
                        this.tempHeroImagePath = null;
                    }
                    this.tempHeroImagePath = tempPath;
                    break;

                case ImagePlacement.AppLogo:
                    if (this.tempAppLogoImagePath != null)
                    {
                        this.RemoveTempFile(this.tempAppLogoImagePath);
                        this.tempAppLogoImagePath = null;
                    }
                    this.tempAppLogoImagePath = tempPath;
                    break;

                default:
                    break;
            }
            this.SaveImageToFile(image, tempPath);
            this.SetImage(tempPath, placement);
        }

        public void SetImage(string imagePath, ImagePlacement placement)
        {
            this.CheckImagePath(imagePath);
            switch (placement)
            {
                case ImagePlacement.Inline:
                    this.UpdateTemplateType();
                    this.imagePath = imagePath;
                    return;

                case ImagePlacement.Hero:
                    this.heroImagePath = imagePath;
                    return;

                case ImagePlacement.AppLogo:
                    this.appLogoImagePath = imagePath;
                    return;
            }
        }

        private static void SetImageSrc(IXmlDocument xmldoc, string imagePath)
        {
            IXmlNode node2;
            ComFunctions.CheckHRESULT(GetNode(xmldoc, "image").Attributes.GetNamedItem("src", out node2));
            SetNodeValueString(imagePath, xmldoc, node2);
        }

        private static void SetNodeValueString(string str, IXmlDocument xmldoc, IXmlNode node)
        {
            IXmlText text;
            ComFunctions.CheckHRESULT(xmldoc.CreateTextNode(str, out text));
            AppendNode(node, (IXmlNode) text);
        }

        public void SetSound(PredefinedSound sound)
        {
            this.sound = sound;
        }

        private static void SetSound(IXmlDocument xmldoc, PredefinedSound sound)
        {
            string attributeValue = "ms-winsoundevent:" + sound.ToString().Replace("_", ".");
            IXmlElement node = CreateElement(xmldoc, "audio");
            if (sound == PredefinedSound.NoSound)
            {
                SetAttribute(node, "silent", "true");
            }
            else
            {
                SetAttribute(node, "src", attributeValue);
                SetAttribute(node, "loop", IsLoopingSound(sound).ToString().ToLower());
            }
            AppendNode(GetNode(xmldoc, "toast"), (IXmlNode) node);
        }

        public void SetUpdateToastContentAction(Action<XmlDocument> updateToastContentAction)
        {
            this.updateToastContent = updateToastContentAction;
        }

        private static void UpdateAppLogoImage(IPredefinedToastNotificationInfo info, IXmlDocument content)
        {
            IPredefinedToastNotificationInfoGeneric generic = info as IPredefinedToastNotificationInfoGeneric;
            if (!string.IsNullOrEmpty(generic.AppLogoImagePath) && (info.ToastTemplateType == ToastTemplateType.ToastGeneric))
            {
                string absoluteUri = new Uri(generic.AppLogoImagePath).AbsoluteUri;
                IXmlNode node = CreateInlineImageNode(content, absoluteUri);
                SetAttribute(node, "placement", "appLogoOverride");
                if (generic.AppLogoImageCrop == ImageCropType.Circle)
                {
                    SetAttribute(node, "hint-crop", "circle");
                }
            }
        }

        private static void UpdateAttributionText(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            IPredefinedToastNotificationInfoGeneric generic = info as IPredefinedToastNotificationInfoGeneric;
            if (!string.IsNullOrWhiteSpace(generic.AttributionText))
            {
                IXmlNode node = AppendNode(GetNode(xmldoc, "binding"), (IXmlNode) CreateElement(xmldoc, "text"));
                SetAttribute(node, "placement", "attribution");
                SetNodeValueString(generic.AttributionText, xmldoc, node);
            }
        }

        private static void UpdateContent(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(ToastNotificationManager.GetXml((IXmlNodeSerializer) xmldoc));
            IPredefinedToastNotificationInfoGeneric generic = info as IPredefinedToastNotificationInfoGeneric;
            if (generic.UpdateToastContent != null)
            {
                generic.UpdateToastContent(document);
            }
            generic.UpdateToastContent = null;
            ToastNotificationManager.LoadXml((IXmlDocumentIO) xmldoc, document.OuterXml);
        }

        private static void UpdateDisplayTimestamp(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            IPredefinedToastNotificationInfoGeneric generic = info as IPredefinedToastNotificationInfoGeneric;
            if (generic.DisplayTimestamp != null)
            {
                string attributeValue = generic.DisplayTimestamp.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
                SetAttribute(GetNode(xmldoc, "toast"), "displayTimestamp", attributeValue);
            }
        }

        private static void UpdateDuration(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            NotificationDuration @long = info.Duration;
            if (IsLoopingSound(info.Sound))
            {
                @long = NotificationDuration.Long;
            }
            if (@long != NotificationDuration.Default)
            {
                SetAttribute(xmldoc, "toast", "duration", "long");
            }
        }

        private static void UpdateHeroImage(IPredefinedToastNotificationInfo info, IXmlDocument content)
        {
            IPredefinedToastNotificationInfoGeneric generic = info as IPredefinedToastNotificationInfoGeneric;
            if (!string.IsNullOrEmpty(generic.HeroImagePath) && (info.ToastTemplateType == ToastTemplateType.ToastGeneric))
            {
                string absoluteUri = new Uri(generic.HeroImagePath).AbsoluteUri;
                SetAttribute(CreateInlineImageNode(content, absoluteUri), "placement", "hero");
            }
        }

        private static void UpdateInlineImage(IPredefinedToastNotificationInfo info, IXmlDocument content)
        {
            if (!string.IsNullOrEmpty(info.ImagePath))
            {
                string absoluteUri = new Uri(info.ImagePath).AbsoluteUri;
                if (info.ToastTemplateType == ToastTemplateType.ToastGeneric)
                {
                    CreateInlineImageNode(content, absoluteUri);
                }
                else
                {
                    SetImageSrc(content, absoluteUri);
                }
            }
        }

        private static void UpdateSound(IXmlDocument content, IPredefinedToastNotificationInfo info)
        {
            if (info.Sound != PredefinedSound.Notification_Default)
            {
                SetSound(content, info.Sound);
            }
        }

        private static void UpdateTemplate(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            if (info.ToastTemplateType == ToastTemplateType.ToastGeneric)
            {
                SetAttribute(xmldoc, "binding", "template", "ToastGeneric");
            }
        }

        private void UpdateTemplateType()
        {
            if (this.type == ToastTemplateType.ToastGeneric)
            {
                if (!ToastNotificationManager.IsGenericTemplateSupported)
                {
                    this.type = ToastTemplateType.ToastImageAndText04;
                }
            }
            else
            {
                if (this.type == ToastTemplateType.ToastText01)
                {
                    this.type = ToastTemplateType.ToastImageAndText01;
                }
                if (this.type == ToastTemplateType.ToastText02)
                {
                    this.type = ToastTemplateType.ToastImageAndText02;
                }
                if (this.type == ToastTemplateType.ToastText03)
                {
                    this.type = ToastTemplateType.ToastImageAndText03;
                }
                if (this.type == ToastTemplateType.ToastText04)
                {
                    this.type = ToastTemplateType.ToastImageAndText04;
                }
            }
        }

        private static void UpdateText(IXmlDocument xmldoc, IPredefinedToastNotificationInfo info)
        {
            IXmlNodeList nodes = GetNodes(xmldoc, "text");
            for (uint i = 0; i < info.Lines.Length; i++)
            {
                SetNodeValueString(info.Lines[i], xmldoc, GetNode(nodes, i));
            }
        }

        public IPredefinedToastNotificationInfo Info
        {
            get
            {
                WinRTToastNotificationInfo info1 = new WinRTToastNotificationInfo();
                info1.ToastTemplateType = this.type;
                info1.Lines = this.lines;
                info1.ImagePath = this.imagePath;
                info1.AppLogoImagePath = this.appLogoImagePath;
                info1.HeroImagePath = this.heroImagePath;
                info1.AppLogoImageCrop = this.appLogoImageCrop;
                info1.Duration = this.duration;
                info1.Sound = this.sound;
                info1.AttributionText = this.attributionText;
                info1.DisplayTimestamp = this.displayTimestamp;
                info1.UpdateToastContent = this.updateToastContent;
                return info1;
            }
        }

        public bool IsAssigned { get; set; }

        private class WinRTToastNotificationInfo : IPredefinedToastNotificationInfo, IPredefinedToastNotificationInfoGeneric
        {
            public DevExpress.Internal.WinApi.Windows.UI.Notifications.ToastTemplateType ToastTemplateType { get; set; }

            public string[] Lines { get; set; }

            public string ImagePath { get; set; }

            public string AppLogoImagePath { get; set; }

            public string HeroImagePath { get; set; }

            public ImageCropType AppLogoImageCrop { get; set; }

            public NotificationDuration Duration { get; set; }

            public PredefinedSound Sound { get; set; }

            public string AttributionText { get; set; }

            public DateTimeOffset? DisplayTimestamp { get; set; }

            public Action<XmlDocument> UpdateToastContent { get; set; }
        }
    }
}

