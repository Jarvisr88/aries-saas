System.InvalidOperationException: Operation is not valid due to the current state of the object.
   at Reflector.CodeModel.Assembly.Module.ReadTypeFullyQualifiedName(String typeName, String assemblyReference)
   at Reflector.CodeModel.Assembly.Module.ReadTypeFullyQualifiedName(String fullyQualifiedName)
   at Reflector.CodeModel.Assembly.Module.ReadTypeFullyQualifiedName(ByteArrayReader reader)
   at Reflector.CodeModel.Assembly.CustomAttribute.ReadValue(ByteArrayReader reader, String namespaceName, String name)
   at Reflector.CodeModel.Assembly.CustomAttribute.ReadValue(ByteArrayReader reader, IType type)
   at Reflector.CodeModel.Assembly.CustomAttribute.get_Arguments()
   at Reflector.CodeModel.Visitor.Cloner.TransformCustomAttribute(ICustomAttribute value)
   at Reflector.CodeModel.Visitor.Transformer.TransformCustomAttributeCollection(ICustomAttributeCollection attributes)
   at Reflector.CodeModel.Visitor.Cloner.TransformMethodDeclaration(IMethodDeclaration value)
   at Reflector.CodeModel.Visitor.Transformer.TransformMethodDeclarationCollection(IMethodDeclarationCollection methods)
   at Reflector.CodeModel.Visitor.Cloner.TransformTypeDeclaration(ITypeDeclaration value)
   at Reflector.Disassembler.Disassembler.TransformTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Translator.TranslateTypeDeclaration(ITypeDeclaration value, Boolean memberDeclarationList, Boolean methodDeclarationBody)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Mvvm.Internal
{
}

