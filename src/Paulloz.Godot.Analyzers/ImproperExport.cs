using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Paulloz.Godot.Analyzers.Resources;
using System.Collections.Immutable;
using System.Linq;

namespace Paulloz.Godot.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ImproperExportAnalyzer : DiagnosticAnalyzer
    {
        internal static readonly DiagnosticDescriptor Rule = new(
            id: "GD0002",
            title: Strings.ExportIsMarshallableDiagnosticTitle,
            messageFormat: Strings.ExportIsMarshallableDiagnosticMessageFormat,
            category: DiagnosticCategory.Usage,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Strings.ExportIsMarshallableDiagnosticDescription);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeMemberDeclaration, SyntaxKind.PropertyDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeMemberDeclaration, SyntaxKind.FieldDeclaration);
        }

        private static void AnalyzeMemberDeclaration(SyntaxNodeAnalysisContext context)
        {
            ISymbol? symbol = context.Node switch
            {
                PropertyDeclarationSyntax propertySyntax
                    => context.SemanticModel.GetDeclaredSymbol(propertySyntax),
                FieldDeclarationSyntax fieldSyntax when fieldSyntax.Declaration.Variables.Count > 0
                    => context.SemanticModel.GetDeclaredSymbol(fieldSyntax.Declaration.Variables[0]),
                _ => null
            };

            if (symbol is null) return;
            if (!symbol.ContainingType.Extends(typeof(Object))) return;
            if (!symbol.GetAttributes().Any(a => a.AttributeClass.Matches(typeof(ExportAttribute)))) return;

            // if (symbol is IFieldSymbol field && field.Type is IArrayTypeSymbol arr)
            // context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), $"{arr.ElementType.ToDisplayString()}--{arr.ElementType.IsMarshallable()}"));

            if (!IsMarshallable(symbol, out string? typeName))
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), typeName));
        }

        private static bool IsMarshallable(ISymbol? symbol, out string? typeName)
        {
            typeName = null;

            ITypeSymbol? typeSymbol = symbol switch
            {
                IPropertySymbol propertySymbol => propertySymbol.Type,
                IFieldSymbol fieldSymbol => fieldSymbol.Type,
                _ => null,
            };

            if (typeSymbol is null) return false;

            typeName = typeSymbol.ToDisplayString();
            return typeSymbol.IsMarshallable();
        }
    }
}
