System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars.Automation
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class BarCheckItemLinkControlAutomationPeer
