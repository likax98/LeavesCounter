using System;
using static System.Console;
using System.Reflection;
using System.Linq;

namespace Tree_Homework
{
    class Program
    {
        public static int NodesCounter { get; set; }
        public static int LeavesCounter { get; set; }

        static void Main()
        {

            Node node1 = new();
            Node node2 = new();
            Node node3 = new();
            Node node4 = new();
            Node node5 = new();
            Node node6 = new();

            node1.Left = node2;
            node1.Right = node3;
            node2.Left = node4;
            node2.Right = node5;
            node3.Left = node6;

            Print(GetNodesCount(node1), GetLeavesCount(node1));
        }

        static void Print(int nodesCount, int leavesCount)
        {
            if (IsValueNull<int>(nodesCount))
            {
                return;
            }

            ForegroundColor = ConsoleColor.Magenta;
            WriteLine($"Nodes = {nodesCount}");
            ResetColor();

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"Leaves = {leavesCount}");
            ResetColor();
        }

        static int GetNodesCount(Node node)
        {
            CheckOnNullException(node);

            ++NodesCounter;

            foreach (var prop in node.GetNodeProperties())
            {
                var nodeSideValue = CreateDynamicNode(prop, node);

                if (!IsValueNull<Node>(nodeSideValue))
                {
                    GetNodesCount(nodeSideValue);
                }
            }

            return NodesCounter;
        }

        static int GetLeavesCount(Node node)
        {
            CheckOnNullException(node);

            int i = 0;
            var properties = node.GetNodeProperties();
            var leftNode = CreateDynamicNode(properties[i], node);
            var rightNode = CreateDynamicNode(properties[i + 1], node);

            if (!IsValueNull<Node>(leftNode) && !IsValueNull<Node>(rightNode))
            {
                ++LeavesCounter;
                GetLeavesCount(leftNode);
            }

            return LeavesCounter;
        }

        static Node CreateDynamicNode(PropertyInfo prop, object node)
        {
            return prop.GetValue(node) as Node;
        }

        static bool IsValueNull<T>(T value)
        {
            return value == null;
        }

        static void CheckOnNullException(Node node)
        {
            if (IsValueNull(node))
            {
                throw new ArgumentNullException("Value cannot be null!");
            }
        }
    }

    class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public PropertyInfo[] GetNodeProperties()
        {
            return this.GetType().GetProperties().ToArray();
        }
    }
}