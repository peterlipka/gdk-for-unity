using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Improbable.CodeGeneration.FileHandling;
using Improbable.CodeGeneration.Jobs;
using Improbable.CodeGeneration.Model;
using Improbable.CodeGeneration.Utils;

namespace Improbable.Gdk.CodeGenerator
{
    public class SingleGenerationJob : CodegenJob
    {
        private readonly string relativeOutputPath;
        private readonly string package;
        private readonly List<UnityTypeDefinition> typesToGenerate;
        private readonly List<UnityComponentDefinition> componentsToGenerate;
        private readonly List<EnumDefinitionRaw> enumsToGenerate;

        private readonly HashSet<string> enumSet = new HashSet<string>();

        private const string fileExtension = ".cs";

        public SingleGenerationJob(string outputDir, UnitySchemaFile schemaFile, IFileSystem fileSystem,
            HashSet<string> enumSet) : base(
            outputDir, fileSystem)
        {
            InputFiles = new List<string> { schemaFile.CompletePath };
            OutputFiles = new List<string>();

            relativeOutputPath = Formatting.GetNamespacePath(schemaFile.Package);
            package = Formatting.CapitaliseQualifiedNameParts(schemaFile.Package);

            typesToGenerate = SelectTypesToGenerate(schemaFile);

            foreach (var unityTypeDefinition in typesToGenerate)
            {
                var fileName = Path.ChangeExtension(unityTypeDefinition.Name, fileExtension);
                OutputFiles.Add(Path.Combine(relativeOutputPath, fileName));
            }

            componentsToGenerate = schemaFile.ComponentDefinitions;
            foreach (var component in componentsToGenerate)
            {
                OutputFiles.Add(Path.Combine(relativeOutputPath, Path.ChangeExtension(component.Name, fileExtension)));

                if (component.CommandDefinitions.Count > 0)
                {
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}CommandPayloads", fileExtension)));
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}CommandManager", fileExtension)));
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}CommandComponents", fileExtension)));
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}CommandSenderReceiver", fileExtension)));
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}ReactiveCommandComponents", fileExtension)));
                }

                if (component.EventDefinitions.Count > 0)
                {
                    OutputFiles.Add(Path.Combine(relativeOutputPath,
                        Path.ChangeExtension($"{component.Name}Events", fileExtension)));
                }

                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}UpdateSender", fileExtension)));
                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}Translation", fileExtension)));
                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}UpdateManager", fileExtension)));
                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}ReactiveComponents", fileExtension)));
                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}Providers", fileExtension)));
                OutputFiles.Add(Path.Combine(relativeOutputPath,
                    Path.ChangeExtension($"{component.Name}ComponentReaderWriter", fileExtension)));
            }

            enumsToGenerate = new List<EnumDefinitionRaw>();
            enumsToGenerate.AddRange(schemaFile.EnumDefinitions);
            foreach (var unityEnum in enumsToGenerate)
            {
                var fileName = Path.ChangeExtension(unityEnum.name, fileExtension);
                OutputFiles.Add(Path.Combine(relativeOutputPath, fileName));
            }

            this.enumSet = enumSet;
        }

        protected override void RunImpl()
        {
            var typeGenerator = new UnityTypeGenerator();
            var enumGenerator = new UnityEnumGenerator();
            var eventGenerator = new UnityEventGenerator();
            var commandPayloadGenerator = new UnityCommandPayloadGenerator();
            var commandManagerGenerator = new UnityCommandManagerGenerator();
            var commandComponentsGenerator = new UnityCommandComponentsGenerator();
            var blittableComponentGenerator = new UnityComponentDataGenerator();
            var componentConversionGenerator = new UnityComponentConversionGenerator();
            var componentSenderGenerator = new UnityComponentSenderGenerator();
            var componentUpdateManagerGenerator = new UnityComponentUpdateManagerGenerator();
            var referenceTypeProviderGenerator = new UnityReferenceTypeProviderGenerator();
            var componentReaderWriterGenerator = new UnityComponentReaderWriterGenerator();
            var commandSenderReceiverGenerator = new UnityCommandSenderReceiverGenerator();
            var reactiveComponentGenerator = new ReactiveComponentGenerator();
            var reactiveCommandComponentGenerator = new ReactiveCommandComponentGenerator();

            foreach (var enumType in enumsToGenerate)
            {
                var fileName = Path.ChangeExtension(enumType.name, fileExtension);
                var enumCode = enumGenerator.Generate(enumType, package);
                Content.Add(Path.Combine(relativeOutputPath, fileName), enumCode);
            }

            foreach (var type in typesToGenerate)
            {
                var fileName = Path.ChangeExtension(type.Name, fileExtension);
                var typeCode = typeGenerator.Generate(type, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, fileName), typeCode);
            }

            foreach (var component in componentsToGenerate)
            {
                var componentFileName = Path.ChangeExtension(component.Name, fileExtension);
                var componentCode = blittableComponentGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, componentFileName), componentCode);

                if (component.CommandDefinitions.Count > 0)
                {
                    var commandPayloadsFileName =
                        Path.ChangeExtension($"{component.Name}CommandPayloads", fileExtension);
                    var commandPayloadCode =
                        commandPayloadGenerator.Generate(component, package);
                    Content.Add(Path.Combine(relativeOutputPath, commandPayloadsFileName), commandPayloadCode);

                    var commandManagerFileName =
                        Path.ChangeExtension($"{component.Name}CommandManager", fileExtension);
                    var commandManagerCode =
                        commandManagerGenerator.Generate(component, package, enumSet);
                    Content.Add(Path.Combine(relativeOutputPath, commandManagerFileName), commandManagerCode);

                    var commandComponentsFileName =
                        Path.ChangeExtension($"{component.Name}CommandComponents", fileExtension);
                    var commandComponentsCode =
                        commandComponentsGenerator.Generate(component, package);
                    Content.Add(Path.Combine(relativeOutputPath, commandComponentsFileName), commandComponentsCode);

                    var commandSenderReceiverFileName =
                        Path.ChangeExtension($"{component.Name}CommandSenderReceiver", fileExtension);
                    var commandSenderReceiverCode =
                        commandSenderReceiverGenerator.Generate(component, package, enumSet);
                    Content.Add(Path.Combine(relativeOutputPath, commandSenderReceiverFileName), commandSenderReceiverCode);

                    var reactiveCommandComponentsFileName =
                        Path.ChangeExtension($"{component.Name}ReactiveCommandComponents", fileExtension);
                    var reactiveCommandComponentsCode =
                        reactiveCommandComponentGenerator.Generate(component, package, enumSet);
                    Content.Add(Path.Combine(relativeOutputPath, reactiveCommandComponentsFileName),
                        reactiveCommandComponentsCode);
                }

                if (component.EventDefinitions.Count > 0)
                {
                    var eventsFileName = Path.ChangeExtension($"{component.Name}Events", fileExtension);
                    var eventsCode = eventGenerator.Generate(component, package);
                    Content.Add(Path.Combine(relativeOutputPath, eventsFileName), eventsCode);
                }

                var updateSenderFileName = Path.ChangeExtension($"{component.Name}UpdateSender", fileExtension);
                var updateSenderCode = componentSenderGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, updateSenderFileName), updateSenderCode);

                var conversionFileName = Path.ChangeExtension($"{component.Name}Translation", fileExtension);
                var componentTranslationCode = componentConversionGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, conversionFileName), componentTranslationCode);

                var updateManagerFileName = Path.ChangeExtension($"{component.Name}UpdateManager", fileExtension);
                var updateManagerCode = componentUpdateManagerGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, updateManagerFileName), updateManagerCode);

                var reactiveComponentsFileName = Path.ChangeExtension($"{component.Name}ReactiveComponents", fileExtension);
                var reactiveComponentsCode = reactiveComponentGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, reactiveComponentsFileName), reactiveComponentsCode);

                var referenceProviderFileName = Path.ChangeExtension($"{component.Name}Providers", fileExtension);
                var referenceProviderTranslationCode =
                    referenceTypeProviderGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, referenceProviderFileName),
                    referenceProviderTranslationCode);

                var componentReaderWriterFileName =
                    Path.ChangeExtension($"{component.Name}ComponentReaderWriter", fileExtension);
                var componentReaderWriterCode =
                    componentReaderWriterGenerator.Generate(component, package, enumSet);
                Content.Add(Path.Combine(relativeOutputPath, componentReaderWriterFileName), componentReaderWriterCode);
            }
        }

        /// <summary>
        ///     Filters out auto-generated types like PositionData from the JSON AST.
        ///     However, we want to keep types that are used as a "data" field in a component.
        /// </summary>
        private List<UnityTypeDefinition> SelectTypesToGenerate(UnitySchemaFile schemaFile)
        {
            var componentDataTypes =
                schemaFile.ComponentDefinitions.Select(component => component.RawDataDefinition);

            // From inspection of the JSON AST you can observe that a type definition is auto-generated if the following
            // conditions are true:
            //     1. The FQN type names are the same .
            //     2. The source references are the same.
            // Using this information, we can effectively filter out auto-generated types.
            var filteredTypes = schemaFile.TypeDefinitions.Where(type => componentDataTypes.All(componentData =>
                type.QualifiedName != componentData.TypeName ||
                !SourceReferenceEquals(type.SourceReference, componentData.sourceReference)));

            return filteredTypes.ToList();
        }

        private bool SourceReferenceEquals(SourceReferenceRaw sourceRef1, SourceReferenceRaw sourceRef2)
        {
            return sourceRef1.column == sourceRef2.column && sourceRef1.line == sourceRef2.line;
        }
    }
}
