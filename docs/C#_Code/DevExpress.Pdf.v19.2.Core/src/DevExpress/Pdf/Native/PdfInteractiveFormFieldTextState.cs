namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfInteractiveFormFieldTextState
    {
        public const double DefaultFontSize = 12.0;
        private readonly PdfSetTextFontCommand fontCommand;
        private readonly double characterSpacing;
        private readonly double wordSpacing;
        private readonly double horizontalScaling = 100.0;
        private byte[] commandsToFill;

        public PdfInteractiveFormFieldTextState(PdfInteractiveFormField formField)
        {
            IEnumerable<PdfCommand> appearanceCommandsInheritable = this.GetAppearanceCommandsInheritable(formField);
            PdfCommandList list = new PdfCommandList();
            if (appearanceCommandsInheritable != null)
            {
                foreach (PdfCommand command in appearanceCommandsInheritable)
                {
                    PdfSetWordSpacingCommand command2 = command as PdfSetWordSpacingCommand;
                    if (command2 != null)
                    {
                        this.wordSpacing = command2.WordSpacing;
                    }
                    PdfSetCharacterSpacingCommand command3 = command as PdfSetCharacterSpacingCommand;
                    if (command3 != null)
                    {
                        this.characterSpacing = command3.CharacterSpacing;
                    }
                    PdfSetTextHorizontalScalingCommand command4 = command as PdfSetTextHorizontalScalingCommand;
                    if (command4 != null)
                    {
                        this.horizontalScaling = command4.HorizontalScaling;
                    }
                    PdfSetTextFontCommand command5 = command as PdfSetTextFontCommand;
                    if (command5 == null)
                    {
                        list.Add(command);
                    }
                    else
                    {
                        this.fontCommand = command5;
                    }
                }
                this.commandsToFill = list.ToByteArray(new PdfResources(null, false));
            }
        }

        public void FillCommands(PdfCommandConstructor constructor)
        {
            constructor.AddCommands(this.commandsToFill);
        }

        private IEnumerable<PdfCommand> GetAppearanceCommandsInheritable(PdfInteractiveFormField formField)
        {
            if (formField == null)
            {
                return null;
            }
            IEnumerable<PdfCommand> appearanceCommands = formField.AppearanceCommands;
            if (appearanceCommands != null)
            {
                return appearanceCommands;
            }
            PdfInteractiveFormField parent = formField.Parent;
            return (((parent != null) || (formField.Form == null)) ? this.GetAppearanceCommandsInheritable(parent) : formField.Form.DefaultAppearanceCommands);
        }

        public PdfSetTextFontCommand FontCommand =>
            this.fontCommand;

        public double CharacterSpacing =>
            this.characterSpacing;

        public double WordSpacing =>
            this.wordSpacing;

        public double HorizontalScaling =>
            this.horizontalScaling;

        public double FontSize =>
            (this.fontCommand == null) ? 12.0 : this.fontCommand.FontSize;
    }
}

