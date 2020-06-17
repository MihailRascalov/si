using System;
using System.Collections.Generic;

namespace Exercise_3._1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Exercise 3.1\n");
            CleanerNode state = CleanerFindPath();
            List<CleanerNode.CleanerActions> Exercise_3_1 = CleanerNode.ReadResult(state);
            if (Exercise_3_1 != null)
            {
                foreach (CleanerActions action in Exercise_3_1)
                {
                    if (action == CleanerActions.GoRight)
                        Console.WriteLine("GoRight");
                    else if (action == CleanerActions.GoLeft)
                        Console.WriteLine("GoLeft");
                    else if (action == CleanerActions.Clean)
                        Console.WriteLine("Clean");
                    else if (action == CleanerActions.DoNothing)
                        Console.WriteLine("DoNothing");
                }
                Console.WriteLine("\nRoom is clean now.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nRoom cannot be clean.");
                Console.ReadKey();
            }
        }

        public enum CleanerActions
        {
            GoRight,
            GoLeft,
            Clean,
            DoNothing
        };

        private static CleanerNode CleanerFindPath()
        {
            CleanerNode startNode = new CleanerNode();
            Queue<CleanerNode> openNodes = new Queue<CleanerNode>();
            openNodes.Enqueue(startNode);
            while (openNodes.Count > 0)
            {
                CleanerNode node = openNodes.Dequeue();
                if (node.IsClear())
                    return node;
                foreach (CleanerActions action in BreadthFirstSearch.Action())
                    openNodes.Enqueue(BreadthFirstSearch.Result(node, (BreadthFirstSearch.CleanerActions)action));
            }
            return null;
        }
    }

    public class CleanerNode
    {
        public enum CleanerActions
        {
            GoRight,
            GoLeft,
            Clean,
            DoNothing
        };

        public enum RoadStatus
        {
            Clean,
            Dirty
        };

        public CleanerNode Parent { get; set; }
        public RoadStatus[] Road { get; set; }
        public CleanerActions ParentAction { get; set; }
        public int CleanerPosition { get; set; }

        public CleanerNode(RoadStatus[] _state = null, CleanerNode _parent = null, CleanerActions _parentAction = CleanerActions.DoNothing, int _CleanerPosition = 2, int length = 5)
        {
            if (_state != null)
            {
                Road = new RoadStatus[_state.Length];
                for (int i = 0; i < _state.Length; i++)
                    Road[i] = _state[i];
            }
            else
            {
                Road = new RoadStatus[length];
                for (int i = 0; i < length; i++)
                    Road[i] = RoadStatus.Dirty;
            }
            CleanerPosition = _CleanerPosition;
            ParentAction = _parentAction;
            Parent = _parent;
        }

        public bool IsClear()
        {
            foreach (RoadStatus RoadTile in Road)
                if (RoadTile == RoadStatus.Dirty)
                    return false;
            return true;
        }

        public static List<CleanerActions> ReadResult(CleanerNode finalState)
        {
            if (finalState != null)
            {
                List<CleanerActions> actionsStepsReversed = new List<CleanerActions>();
                while (finalState != null)
                {
                    if (finalState.Parent != null)
                        actionsStepsReversed.Add(finalState.ParentAction);
                    finalState = finalState.Parent;
                }
                actionsStepsReversed.Reverse();
                return actionsStepsReversed;
            }
            return null;
        }
    }

    public class BreadthFirstSearch
    {
        public enum CleanerActions
        {
            GoRight,
            GoLeft,
            Clean,
            DoNothing
        };

        public enum RoadStatus
        {
            Clean,
            Dirty
        };

        public static List<CleanerActions> Action()
        {
            List<CleanerActions> actions = new List<CleanerActions>();
            actions.Add(CleanerActions.GoRight);
            actions.Add(CleanerActions.GoLeft);
            actions.Add(CleanerActions.Clean);
            actions.Add(CleanerActions.DoNothing);
            return actions;
        }

        public static CleanerNode Result(CleanerNode node, CleanerActions action)
        {
            CleanerNode temporary = new CleanerNode(node.Road, node, (CleanerNode.CleanerActions)CleanerActions.DoNothing, node.CleanerPosition);
            if (action == CleanerActions.GoRight && node.CleanerPosition < node.Road.Length - 1)
            {
                temporary.CleanerPosition++;
                temporary.ParentAction = (CleanerNode.CleanerActions)CleanerActions.GoRight;
            }
            else if (action == CleanerActions.GoLeft && node.CleanerPosition > 0)
            {
                temporary.CleanerPosition--;
                temporary.ParentAction = (CleanerNode.CleanerActions)CleanerActions.GoLeft;
            }
            else if (action == CleanerActions.Clean)
            {
                temporary.Road[node.CleanerPosition] = (CleanerNode.RoadStatus)RoadStatus.Clean;
                temporary.ParentAction = (CleanerNode.CleanerActions)CleanerActions.Clean;
            }
            return temporary;
        }
    }
}