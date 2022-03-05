using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Paulloz.Godot.Analyzers.Resources;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Paulloz.Godot.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NonGenericGetNodeAnalyzer : DiagnosticAnalyzer
    {
        internal static readonly DiagnosticDescriptor Rule = new(
            id: "GD0001",
            title: Strings.NonGenericGetNodeDiagnosticTitle,
            messageFormat: Strings.NonGenericGetComponentDiagnosticMessageFormat,
            category: DiagnosticCategory.TypeSafety,
            defaultSeverity: DiagnosticSeverity.Info,
            isEnabledByDefault: true,
            description: Strings.NonGenericGetComponentDiagnosticDescription);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
        {
            var invocation = (InvocationExpressionSyntax)context.Node;
            var name = invocation.GetMethodNameSyntax();
            if (name is null) return;
            if (!IsGetNodeName(name)) return;

            var symbol = context.SemanticModel.GetSymbolInfo(invocation);
            if (symbol.Symbol is null) return;
            if (!IsNonGenericGetNode(symbol.Symbol, out var methodName)) return;
            if (invocation.Expression is not IdentifierNameSyntax) return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, invocation.GetLocation(), methodName));
        }

        private static bool IsGetNodeName(string str)
            => GetNodeNames.Contains(str);

        private static bool IsGetNodeName(SimpleNameSyntax sns)
            => IsGetNodeName(sns.Identifier.Text);

        private static bool IsGetNode(IMethodSymbol method)
            => IsGetNodeName(method.Name) && method.ContainingType.Matches(typeof(Node));

        private static bool IsNonGenericGetNode(ISymbol symbol, out string? methodName)
        {
            methodName = null;

            if (symbol is not IMethodSymbol method) return false;
            if (!IsGetNode(method)) return false;
            if (method.TypeParameters.Length > 0) return false;

            methodName = method.Name;
            return true;
        }

        private static readonly HashSet<string> GetNodeNames = new(new[]
        {
            nameof(Node.GetNode),
            nameof(Node.GetNodeOrNull),
            nameof(Node.GetChild),
            nameof(Node.GetParent),
            nameof(Node.GetOwner),
        });
    }
}
