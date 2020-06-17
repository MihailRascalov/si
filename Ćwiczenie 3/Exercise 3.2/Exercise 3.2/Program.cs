using System;
using System.Collections.Generic;

namespace Exercise_3._2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Exercise 3.2\n");

            int size = 3;
            int[][] startMatrix = new int[size][];
            int[][] endMatrix = new int[size][];

            for (int i = 0; i < size; i++)
            {
                startMatrix[i] = new int[size];
                endMatrix[i] = new int[size];
            }

            startMatrix[0][0] = 8;
            startMatrix[0][1] = -1;
            startMatrix[0][2] = 7;
            startMatrix[1][0] = 6;
            startMatrix[1][1] = 5;
            startMatrix[1][2] = 4;
            startMatrix[2][0] = 3;
            startMatrix[2][1] = 2;
            startMatrix[2][2] = 1;

            endMatrix[0][0] = 1;
            endMatrix[0][1] = 2;
            endMatrix[0][2] = 3;
            endMatrix[1][0] = 4;
            endMatrix[1][1] = 5;
            endMatrix[1][2] = 6;
            endMatrix[2][0] = 7;
            endMatrix[2][1] = 8;
            endMatrix[2][2] = -1;

            RiddleNode lastState = FindPath(startMatrix, endMatrix);
            List<RiddleNode.RiddleActions> Exercise_3_2 = RiddleNode.ReadResult(lastState);

            if (Exercise_3_2 != null)
            {
                foreach (RiddleNode.RiddleActions action in Exercise_3_2)
                {
                    if (action == RiddleNode.RiddleActions.MoveRight)
                        Console.WriteLine("Right");
                    else if (action == RiddleNode.RiddleActions.MoveLeft)
                        Console.WriteLine("Left");
                    else if (action == RiddleNode.RiddleActions.MoveUp)
                        Console.WriteLine("Up");
                    else if (action == RiddleNode.RiddleActions.MoveDown)
                        Console.WriteLine("Down");
                }
                Console.WriteLine("\nRiddle solved.\n");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nRiddle not solved.\n");
                Console.ReadKey();
            }
        }

        private static RiddleNode FindPath(int[][] startMatrix, int[][] endMatrix)
        {
            RiddleNode state = new RiddleNode(3, startMatrix, RiddleNode.RiddleActions.NaN, null);
            List<RiddleNode> openSet = new List<RiddleNode>();
            List<RiddleNode> closeSet = new List<RiddleNode>();
            openSet.Add(state);
            while (openSet.Count > 0)
            {
                int minimalCost = int.MaxValue;
                foreach (RiddleNode temporaryState in openSet)
                {
                    if (temporaryState.FNode(endMatrix) < minimalCost)
                    {
                        minimalCost = temporaryState.FNode(endMatrix);
                        state = temporaryState;
                    }
                }
                openSet.Remove(state);
                closeSet.Add(state);
                if (state.HNode(endMatrix) == 0)
                    return state;
                List<RiddleNode.RiddleActions> actions = GreedyFirstSearch.Action(state);
                actions.Remove(state.ParentAction);
                foreach (RiddleNode.RiddleActions action in actions)
                {
                    RiddleNode temporaryState = GreedyFirstSearch.Result(state, action);
                    bool isNew = true;
                    foreach (RiddleNode close in closeSet)
                    {
                        if (close.AreSame(temporaryState))
                        {
                            isNew = false;
                            break;
                        }
                    }
                    if (isNew)
                        openSet.Add(temporaryState);
                }
            }
            return null;
        }
    }

    public class RiddleNode
    {
        public enum RiddleActions
        {
            NaN,
            MoveLeft,
            MoveUp,
            MoveRight,
            MoveDown
        };

        public RiddleActions ParentAction { get; set; }
        public RiddleNode ParentNode { get; set; }
        public int Size { get; set; }
        public int[][] State { get; set; }

        public RiddleNode(int _size, int[][] _state, RiddleActions _action, RiddleNode _node)
        {
            Size = _size;
            State = new int[Size][];
            for (int i = 0; i < Size; i++)
                State[i] = new int[Size];
            for (int row = 0; row < Size; row++)
                for (int column = 0; column < Size; column++)
                    State[row][column] = _state[row][column];
            ParentAction = _action;
            ParentNode = _node;
        }

        public int[] FindBlank()
        {
            int[] blank = new int[2];
            for (int row = 0; row < Size; row++)
                for (int column = 0; column < Size; column++)
                    if (State[row][column] == -1)
                    {
                        blank[0] = row;
                        blank[1] = column;
                    }
            return blank;
        }

        public int HNode(int[][] final)
        {
            int hNode = 0;
            for (int row = 0; row < Size; row++)
                for (int column = 0; column < Size; column++)
                    if (State[row][column] != final[row][column])
                        hNode++;
            return hNode;
        }

        public int FNode(int[][] final)
        {
            return HNode(final);
        }

        public bool AreSame(RiddleNode otherState)
        {
            if (HNode(otherState.State) == 0)
                return true;
            else
                return false;
        }

        public static List<RiddleActions> ReadResult(RiddleNode final)
        {
            if (final != null)
            {
                List<RiddleActions> actions = new List<RiddleActions>();
                while (final != null)
                {
                    if (final.ParentNode != null)
                        actions.Add(final.ParentAction);
                    final = final.ParentNode;
                }
                actions.Reverse();
                return actions;
            }
            return null;
        }
    }

    class GreedyFirstSearch
    {
        public static List<RiddleNode.RiddleActions> Action(RiddleNode state)
        {
            List<RiddleNode.RiddleActions> actions = new List<RiddleNode.RiddleActions>();
            int[] temporary = state.FindBlank();
            if (temporary[0] > 0)
                actions.Add(RiddleNode.RiddleActions.MoveUp);
            if (temporary[0] < state.Size - 1)
                actions.Add(RiddleNode.RiddleActions.MoveDown);
            if (temporary[1] > 0)
                actions.Add(RiddleNode.RiddleActions.MoveLeft);
            if (temporary[1] < state.Size - 1)
                actions.Add(RiddleNode.RiddleActions.MoveRight);
            return actions;
        }

        public static RiddleNode Result(RiddleNode state, RiddleNode.RiddleActions action)
        {
            RiddleNode temporary = new RiddleNode(state.Size, state.State, RiddleNode.RiddleActions.NaN, state);
            int[] empty = state.FindBlank();

            if (action == RiddleNode.RiddleActions.MoveLeft)
            {
                temporary.State[empty[0]][empty[1]] = temporary.State[empty[0]][empty[1] - 1];
                temporary.State[empty[0]][empty[1] - 1] = -1;
                temporary.ParentAction = RiddleNode.RiddleActions.MoveLeft;
            }
            else if (action == RiddleNode.RiddleActions.MoveRight)
            {
                temporary.State[empty[0]][empty[1]] = temporary.State[empty[0]][empty[1] + 1];
                temporary.State[empty[0]][empty[1] + 1] = -1;
                temporary.ParentAction = RiddleNode.RiddleActions.MoveRight;
            }
            else if (action == RiddleNode.RiddleActions.MoveDown)
            {
                temporary.State[empty[0]][empty[1]] = temporary.State[empty[0] + 1][empty[1]];
                temporary.State[empty[0] + 1][empty[1]] = -1;
                temporary.ParentAction = RiddleNode.RiddleActions.MoveDown;
            }
            else if (action == RiddleNode.RiddleActions.MoveUp)
            {
                temporary.State[empty[0]][empty[1]] = temporary.State[empty[0] - 1][empty[1]];
                temporary.State[empty[0] - 1][empty[1]] = -1;
                temporary.ParentAction = RiddleNode.RiddleActions.MoveUp;
            }
            return temporary;
        }
    }
}