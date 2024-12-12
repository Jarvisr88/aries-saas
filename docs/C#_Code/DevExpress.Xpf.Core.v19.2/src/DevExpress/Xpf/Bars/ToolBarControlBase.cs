System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public abstract class ToolBarControlBase
