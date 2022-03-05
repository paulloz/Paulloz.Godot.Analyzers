using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Linq;

namespace Paulloz.Godot.Analyzers
{
    internal static class SuppressionAnalysisContextExtensions
    {
        public static T? GetSuppressibleNode<T>(this SuppressionAnalysisContext context, Diagnostic diagnostic)
        where T : SyntaxNode
        {
            return GetSuppressibleNode<T>(context, diagnostic, _ => true);
        }

        public static T? GetSuppressibleNode<T>(this SuppressionAnalysisContext context, Diagnostic diagnostic,
                                                Func<T, bool> predicate)
        where T : SyntaxNode
        {
            var location = diagnostic.Location;
            var sourceTree = location.SourceTree;
            var root = sourceTree.GetRoot(context.CancellationToken);

            return root?
                .FindNode(location.SourceSpan)
                .DescendantNodesAndSelf()
                .OfType<T>()
                .FirstOrDefault(predicate);
        }
    }
}
