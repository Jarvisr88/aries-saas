System.InvalidOperationException: Invalid stack state
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.ConditionalExpressionBuilder.Solution2()
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.ConditionalExpressionBuilder.TryBuild(AnalyzeContext context)
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.TryBuildConditionalExpression(AnalyzeContext context)
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.Analyze(CodeNode node)
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.AnalyzeRange(CodeNode rootNode, OffsetRange range)
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.AnalyzeAll()
   at Reflector.Disassembler.TranslatorBase.ExitAnalyzer.Analyze(NewTranslator translator)
   at Reflector.Disassembler.NewTranslator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB)
   at Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   at Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   at Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Data.Controls.ExpressionEditor.Native
{
}

