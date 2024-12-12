System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars.Internal
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;

    public class LogicalChildrenContainer
