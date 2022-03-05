using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Paulloz.Godot.Analyzers.Resources;
using System.Collections.Immutable;
using System.Linq;

namespace Paulloz.Godot.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ExportSuppressor : DiagnosticSuppressor
    {
        internal static readonly SuppressionDescriptor ReadonlyRule = new(
            id: "GSP0004",
            suppressedDiagnosticId: "IDE0044",
            justification: Strings.ReadonlySerializeFieldSuppressorJustification);

        internal static readonly SuppressionDescriptor UnusedRule = new(
            id: "GSP0006",
            suppressedDiagnosticId: "IDE0051",
            justification: Strings.UnusedSerializeFieldSuppressorJustification);

        internal static readonly SuppressionDescriptor UnusedFxCopRule = new(
            id: "GSP0013",
            suppressedDiagnosticId: "CA1823",
            justification: Strings.UnusedSerializeFieldSuppressorJustification);

        internal static readonly SuppressionDescriptor NeverAssignedRule = new(
            id: "GSP0007",
            suppressedDiagnosticId: "CS0649",
            justification: Strings.NeverAssignedSerializeFieldSuppressorJustification);

        public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions
            => ImmutableArray.Create(ReadonlyRule, UnusedRule, UnusedFxCopRule, NeverAssignedRule);

        public override void ReportSuppressions(SuppressionAnalysisContext context)
        {
            foreach (var diagnostic in context.ReportedDiagnostics)
                AnalyzeDiagnostic(diagnostic, context);
        }

        private static bool IsSuppressable(IFieldSymbol fieldSymbol)
        {
            if (fieldSymbol.GetAttributes().Any(a => a.AttributeClass.Matches(typeof(ExportAttribute))))
                return true;

            return false;
        }

        private void AnalyzeDiagnostic(Diagnostic diagnostic, SuppressionAnalysisContext context)
        {
            if (context.GetSuppressibleNode<VariableDeclaratorSyntax>(diagnostic) is not VariableDeclaratorSyntax fieldDeclarationSyntax)
                return;

            SemanticModel model = context.GetSemanticModel(diagnostic.Location.SourceTree);
            if (model.GetDeclaredSymbol(fieldDeclarationSyntax) is not IFieldSymbol fieldSymbol)
                return;

            if (!IsSuppressable(fieldSymbol))
                return;

            foreach (var descriptor in SupportedSuppressions.Where(d => d.SuppressedDiagnosticId == diagnostic.Id))
                context.ReportSuppression(Suppression.Create(descriptor, diagnostic));
        }
    }
}
