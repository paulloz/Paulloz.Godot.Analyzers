#pragma warning disable

namespace Godot
{
    internal class ExportAttribute : System.Attribute { }

    internal class NodePath { }
    internal class Node
    {
        public Node GetNode(NodePath path) { return null; }
        public Node GetNodeOrNull(NodePath path) { return null; }
        public Node GetChild(int idx) { return null; }
        public Node GetParent() { return null; }
        public Node GetOwner() { return null; }
    }
    internal class Object { }
    internal class RID { }

    internal struct AABB { }
    internal struct Basis { }
    internal struct Color { }
    internal struct Plane { }
    internal struct Quat { }
    internal struct Rect2 { }
    internal struct Transform { }
    internal struct Transform2D { }
    internal struct Vector2 { }
    internal struct Vector3 { }
}

#pragma warning restore
