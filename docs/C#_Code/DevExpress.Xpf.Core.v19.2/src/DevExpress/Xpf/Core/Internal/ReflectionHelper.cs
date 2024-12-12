System.InvalidOperationException: Operation is not valid due to the current state of the object.
   at Reflector.Disassembler.TranslatorBase.VariablesAnalyzer.ConvertCodeNodes(Boolean& anyChange)
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.AnalyzeAll()
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.Analyze(NewTranslator translator)
   at Reflector.Disassembler.NewTranslator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB)
   at Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   at Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   at Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Core.Internal
{
}

