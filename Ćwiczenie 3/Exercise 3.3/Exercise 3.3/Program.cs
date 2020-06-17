using System;
using System.Collections.Generic;

namespace Exercise_3._3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Exercise 3.3\n");

            Node node1 = new Node(0);
            Node node2 = new Node(0);
            Node node3 = new Node(0);
            Node node4 = new Node(1);
            Node node5 = new Node(4);
            Node node6 = new Node(2);
            Node node7 = new Node(6);
            Node node8 = new Node(12);
            Node node9 = new Node(7);
            Node node10 = new Node(1);
            Node node11 = new Node(0);
            Node node12 = new Node(-6);
            Node node13 = new Node(36);
            Node node14 = new Node(-12);
            Node node15 = new Node(4);

            node1.AddChild(node2);
            node1.AddChild(node3);
            node2.AddChild(node4);
            node2.AddChild(node5);
            node3.AddChild(node6);
            node3.AddChild(node7);
            node4.AddChild(node8);
            node4.AddChild(node9);
            node5.AddChild(node10);
            node5.AddChild(node11);
            node6.AddChild(node12);
            node6.AddChild(node13);
            node7.AddChild(node14);
            node7.AddChild(node15);

            Console.WriteLine("The best strategy: " + Maximin.MaxMin(node1, 3, int.MinValue, int.MaxValue, true) + "\n");
            Console.ReadKey();
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<Node> Childrens { get; set; }
        public Node(int _value)
        {
            Value = _value;
            Childrens = new List<Node>();
        }

        public void AddChild(Node child)
        {
            if (Childrens == null)
                Childrens = new List<Node>();
            Childrens.Add(child);
        }
    }

    class Maximin
    {
        public static int MaxMin(Node node, int deepness, int alpha, int beta, bool isMaxPlayer)
        {
            if (deepness == 0)
                return node.Value;
            if (isMaxPlayer)
            {
                int maximum = int.MinValue;
                foreach (Node child in node.Childrens)
                {
                    int temporary = MaxMin(child, deepness - 1, alpha, beta, false);
                    maximum = Math.Max(maximum, temporary);
                    alpha = Math.Max(alpha, temporary);
                    if (beta <= alpha)
                        break;
                }
                node.Value = maximum;
                return maximum;
            }
            else
            {
                int minimum = int.MaxValue;
                foreach (Node child in node.Childrens)
                {
                    int temporary = MaxMin(child, deepness - 1, alpha, beta, true);
                    minimum = Math.Min(minimum, temporary);
                    alpha = Math.Min(beta, temporary);
                    if (beta <= alpha)
                        break;
                }
                node.Value = minimum;
                return minimum;
            }
        }
    }
}