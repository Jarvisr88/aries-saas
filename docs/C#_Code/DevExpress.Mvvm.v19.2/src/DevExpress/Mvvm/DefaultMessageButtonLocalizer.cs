namespace DevExpress.Mvvm
{
    using System;

    public class DefaultMessageButtonLocalizer : IMessageButtonLocalizer
    {
        public string Localize(MessageResult button)
        {
            switch (button)
            {
                case MessageResult.OK:
                    return "OK";

                case MessageResult.Cancel:
                    return "Cancel";

                case MessageResult.Yes:
                    return "Yes";

                case MessageResult.No:
                    return "No";
            }
            return string.Empty;
        }
    }
}

