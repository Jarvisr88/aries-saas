System.InvalidOperationException: Unresolved type
   at Reflector.Disassembler.NewTranslator.CustomTransformer.TransformUnresolvedType(IUnresolvedType value)
   at Reflector.CodeModel.Visitor.Transformer.TransformType(IType value)
   at Reflector.CodeModel.Visitor.Transformer.TransformVariableDeclaration(IVariableDeclaration value)
   at Reflector.CodeModel.Visitor.Transformer.TransformVariableDeclarationExpression(IVariableDeclarationExpression value)
   at Reflector.CodeModel.Visitor.Transformer.TransformExpression(IExpression value)
   at Reflector.CodeModel.Visitor.Transformer.TransformAssignExpressionTarget(IExpression value)
   at Reflector.CodeModel.Visitor.Transformer.TransformAssignExpression(IAssignExpression value)
   at Reflector.CodeModel.Visitor.Transformer.TransformExpression(IExpression value)
   at Reflector.CodeModel.Visitor.Transformer.TransformExpressionStatement(IExpressionStatement value)
   at Reflector.CodeModel.Visitor.Transformer.TransformStatement(IStatement value)
   at Reflector.CodeModel.Visitor.Transformer.InsituTransformStatementCollection(StatementCollection value)
   at Reflector.CodeModel.Visitor.Transformer.TransformBlockStatement(IBlockStatement value)
   at Reflector.Disassembler.NewTranslator.TranslateMethodDeclaration(IMethodDeclaration mD, IMethodBody mB)
   at Reflector.Disassembler.Disassembler.TransformMethodDeclaration(IMethodDeclaration value)
   at Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   at Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace ActiproSoftware.Drawing
{
}

