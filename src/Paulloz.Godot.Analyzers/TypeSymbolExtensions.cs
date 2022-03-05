using Godot;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GObject = Godot.Object;

namespace Paulloz.Godot.Analyzers
{
    internal static class TypeSymbolExtensions
    {
        public static bool Extends(this ITypeSymbol? symbol, Type type)
        {
            if (symbol is null || type is null) return false;

            while (symbol is not null)
            {
                if (symbol.Matches(type)) return true;

                symbol = symbol.BaseType;
            }

            return false;
        }

        public static bool IsMarshallable(this ITypeSymbol symbol)
        {
            return symbol.SpecialType == SpecialType.System_Boolean
                || symbol.SpecialType == SpecialType.System_String
                || symbol.SpecialType == SpecialType.System_Single
                || symbol.SpecialType == SpecialType.System_Int16
                || symbol.SpecialType == SpecialType.System_Int32
                || symbol.SpecialType == SpecialType.System_Int64
                || symbol.SpecialType == SpecialType.System_UInt16
                || symbol.SpecialType == SpecialType.System_UInt32
                || symbol.SpecialType == SpecialType.System_UInt64
                || symbol.Extends(typeof(AABB))
                || symbol.Extends(typeof(Basis))
                || symbol.Extends(typeof(Color))
                || symbol.Extends(typeof(Plane))
                || symbol.Extends(typeof(Quat))
                || symbol.Extends(typeof(Rect2))
                || symbol.Extends(typeof(Transform))
                || symbol.Extends(typeof(Transform2D))
                || symbol.Extends(typeof(Vector2))
                || symbol.Extends(typeof(Vector3))
                || symbol.Extends(typeof(NodePath))
                || symbol.Extends(typeof(GObject))
                || symbol.Extends(typeof(RID))
                || IsMarshallableArray(symbol)
                || IsMarshallableICollectionT(symbol);

            static bool IsMarshallableArray(ITypeSymbol symbol) =>
                symbol is IArrayTypeSymbol arraySymbol && arraySymbol.ElementType.IsMarshallable();

            static bool IsMarshallableICollectionT(ITypeSymbol symbol) =>
                symbol.AllInterfaces.Any(i => i.ConstructedFrom.Matches(typeof(ICollection<>).GetGenericTypeDefinition()))
                && symbol is not IArrayTypeSymbol
                && ((INamedTypeSymbol)symbol).TypeArguments.All(t => t.IsMarshallable());
        }

        public static bool Matches(this ITypeSymbol symbol, Type type)
        {
            switch (symbol.SpecialType)
            {
                case SpecialType.System_Void:
                    return type == typeof(void);
                case SpecialType.System_Boolean:
                    return type == typeof(bool);
                case SpecialType.System_Int32:
                    return type == typeof(int);
                case SpecialType.System_Single:
                    return type == typeof(float);
            }

            if (type.IsArray)
                return symbol is IArrayTypeSymbol array && Matches(array.ElementType, type.GetElementType()!);

            if (symbol is not INamedTypeSymbol named)
                return false;

            if (type.IsConstructedGenericType)
            {
                var args = type.GetTypeInfo().GenericTypeArguments;
                if (args.Length != named.TypeArguments.Length)
                    return false;

                for (var i = 0; i < args.Length; i++)
                    if (!Matches(named.TypeArguments[i], args[i]))
                        return false;

                return Matches(named.ConstructedFrom, type.GetGenericTypeDefinition());
            }

            if (type.IsGenericType && type.GetTypeInfo().GenericTypeParameters.Length != named.TypeParameters.Length)
                return false;

            return named.Name == type.Name.Split('`')[0]
                   && named.ContainingNamespace?.ToDisplayString() == type.Namespace;
        }
    }
}
