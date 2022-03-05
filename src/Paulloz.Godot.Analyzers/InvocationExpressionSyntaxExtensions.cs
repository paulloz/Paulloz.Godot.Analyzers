using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Paulloz.Godot.Analyzers
{
    internal static class InvocationExpressionSyntaxExtensions
    {
        public static SimpleNameSyntax? GetMethodNameSyntax(this InvocationExpressionSyntax expr)
        {
            return expr.Expression switch
            {
                MemberAccessExpressionSyntax mae => mae.Name,
                SimpleNameSyntax sns => sns,
                _ => null,
            };
        }
    }
}
