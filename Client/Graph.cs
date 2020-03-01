using System;
using System.Collections.Generic;
using System.Linq;
using SnakeBattle.Api;

namespace Client
{
    class Graph
    {
        private readonly List<Node> nodes;
        public List<BoardPoint> BorderBoardPoints;
        public Graph()
        {
            BorderBoardPoints = new List<BoardPoint>();
            pointToNodeDict = new Dictionary<BoardPoint, Node>();
            nodes = new List<Node>();
        }

        public int Length => nodes.Count;

        public Node this[int index] => nodes[index];
        public Node this[BoardPoint point] => nodes[GetIndexOfBoardPoint(point)];
        //public Node this[BoardElement index] => nodes[GetIndexOfData(index)];

        public void AddNode(BoardPoint point, BoardElement element)
        {
            var num = Length;
            var node = new Node(num);
            pointToNodeDict.Add(point, node);
            node.BoardPoint = point;
            node.Element = element;
            nodes.Add(node);
            //node.AddExtraWeight(0);
            foreach (var l in point.Neighborhood)
            {
                if (IsBoardPointInDict(l))
                    Connect(node, l);
            }
        }

        //public void DeleteNode(Node node)
        //{
        //    pointToIntDict.Remove(node.BoardPoint);
        //    node.Delete();
        //    nodes.Remove(node);
        //}

        //public void DeleteNode(BoardPoint point, BoardElement element)
        //{
        //    DeleteNode(nodes[pointToIntDict[point]]);
        //}

        public int GetIndexOfBoardPoint(BoardPoint point)
        {
            return pointToNodeDict[point].NodeNumber;
        }

        public bool IsBoardPointInDict(BoardPoint point)
        {
            return pointToNodeDict.ContainsKey(point);
        }

        public bool IsBoardPointInGraph(BoardPoint point)
        {
            return (pointToNodeDict.ContainsKey(point) && pointToNodeDict[point].IsCreated);
        }

        public IEnumerable<Node> Nodes => nodes.Where(x => x.IsCreated);

        //public void AddData(BoardElement element, BoardPoint boardPoint)
        //{
        //}

        //public void AddData(BoardElement objectData)
        //{
        //	var index = pointToIntDict[objectData.BoardPoint.ToBoardPoint()];
        //	nodes[index].SavedData = objectData;
        //	nodes[index].SetWeight(objectData.Terrain);
        //}

        //private void UpdateWeight(Node node, GameBoard gameBoard)
        //{
        //	node.DeleteExtraWeight();
        //	if (node.SavedData.Wall != null)
        //	{
        //		node.AddExtraWeight(MyConstants.Wall);
        //		return;
        //	}
        //	if (node.SavedData.ResourcePile != null)
        //	{
        //		node.AddExtraWeight(-0.5);
        //		return;
        //	}
        //	if (Army.GetObjectArmy(node.SavedData, gameBoard) != null)
        //	{
        //		node.AddExtraWeight(
        //			Combat.Resolve(new ArmiesPair(gameBoard.MyArmy, Army.GetObjectArmy(node.SavedData, gameBoard))).IsDefenderWin
        //				? MyConstants.NoChanceToWinWeight
        //				: (MineFounder.isGoldCaptured ? MyConstants.AllChanceToWinWeight : MyConstants.AllChanceToWinWeight * 4));
        //	}
        //	if (node.SavedData.Dwelling != null && node.SavedData.Dwelling.AvailableToBuyCount > 5)
        //	{
        //		node.AddExtraWeight(-0.3);
        //		return;
        //	}
        //	if (node.SavedData.Mine != null && node.SavedData.Mine.Owner != gameBoard.MyRespawnSide &&
        //		Army.GetObjectArmy(node.SavedData, gameBoard) == null)
        //	{
        //		node.AddExtraWeight(-0.5);
        //	}
        //}

        /*public void UpdateData(BoardElement objectData, GameBoard gameBoard)
		{
			var node = nodes[pointToIntDict[objectData.BoardPoint.ToBoardPoint()]];
			node.SavedData = objectData;
			UpdateWeight(node, gameBoard);
		}*/

        private readonly Dictionary<BoardPoint, Node> pointToNodeDict;

        public void Connect(BoardPoint point1, BoardPoint point2)
        {
            Node.Connect(nodes[GetIndexOfBoardPoint(point1)], nodes[GetIndexOfBoardPoint(point2)]);
        }

        public void Connect(Node node, BoardPoint point2)
        {
            Node.Connect(node, nodes[GetIndexOfBoardPoint(point2)]);
        }

        /// <summary>
        /// Возвращает точку, в которую следует двигаться для достижения оптимального маршрута
        /// </summary>
        /// <param name="from">Начальная Точка</param>
        /// <param name="to">Конечная точка</param>
        /// <returns></returns>
        public Node Dijkstra(Node from, Node to)
        {
            var distances = new double[Length];
            var parents = new int[Length];
            var marks = new bool[Length];
            for (var i = 0; i < Length; ++i)
            {
                distances[i] = double.MaxValue;
                marks[i] = false;
            }
            distances[from.NodeNumber] = 0;
            for (var i = 0; i < Length; ++i)
            {
                var v = -1;
                for (var j = 0; j < Length; ++j)
                    if (!marks[j] && (v == -1 || distances[j] < distances[v]))
                        v = j;
                if (Math.Abs(distances[v] - double.MaxValue) < 0.0001)
                    break;
                if (v == to.NodeNumber)
                {
                    while (parents[v] != from.NodeNumber)
                    {
                        v = parents[v];
                    }
                    return nodes[v];
                }
                marks[v] = true;
                foreach (var node in nodes[v].IncidentNodes)
                {
                    var len = nodes[v].Weight + distances[v];
                    if (len >= distances[node.NodeNumber]) continue;
                    distances[node.NodeNumber] = len;
                    parents[node.NodeNumber] = v;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(to));
        }

        /// <summary>
        /// Генерирует массив минимальных расстояний до каждой точки
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <returns>Массив минимальных расстояний</returns>
        public double[] Dijkstra(BoardPoint from)
        {
            return Dijkstra(pointToNodeDict[from]);
        }

        /// <summary>
        /// Генерирует массив минимальных расстояний до каждой точки
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <returns>Массив минимальных расстояний</returns>
        public double[] Dijkstra(Node from)
        {
            var distances = new double[Length];
            var parents = new int[Length];
            var marks = new bool[Length];
            for (var i = 0; i < Length; ++i)
            {
                distances[i] = double.MaxValue;
                marks[i] = false;
            }
            distances[from.NodeNumber] = 0;
            for (var i = 0; i < Length; ++i)
            {
                var v = -1;
                for (var j = 0; j < Length; ++j)
                    if (!marks[j] && (v == -1 || distances[j] < distances[v]))
                        v = j;
                if (Math.Abs(distances[v] - double.MaxValue) < 0.0001)
                    break;
                marks[v] = true;
                foreach (var node in nodes[v].IncidentNodes)
                {
                    var len = nodes[v].Weight + distances[v];
                    if (len >= distances[node.NodeNumber]) continue;
                    distances[node.NodeNumber] = len;
                    parents[node.NodeNumber] = v;
                }
            }
            distances[from.NodeNumber] = Double.MaxValue;
            return distances;
        }

        public class Node
        {
            private readonly List<Node> nodes = new List<Node>();
            public readonly int NodeNumber;
            public BoardPoint BoardPoint;
            public BoardElement element;
            public bool IsCreated;
            public BoardElement Element
            {
                get
                {
                    if (!IsCreated) throw new InvalidOperationException();
                    return element;
                }
                set
                {
                    element = value;
                    SetWeight();
                    IsCreated = true;
                }
            }

            public Node(int number)
            {
                IsCreated = false;
                NodeNumber = number;
                extraWeight = 0;
            }

            public IEnumerable<Node> IncidentNodes => nodes;

            private double weight;

            public double Weight
            {
                get { return weight + extraWeight; }
                private set { weight = value; }
            }

            private double extraWeight;

            public void SetWeight()
            {
                Weight = GetTerraingWeight(element);
            }

            public override string ToString()
            {
                return $"{NodeNumber} {{{Weight}}} in {BoardPoint} with {element}";
            }

            private static double GetTerraingWeight(BoardElement element)
            {
                switch (element)
                {
                    case BoardElement.Apple:
                        return 5;
                    case BoardElement.None:
                        return 15;
                    default:
                        return 1000;
                }
            }

            public void AddExtraWeight(double value)
            {
                extraWeight += value;
            }

            public void DeleteExtraWeight()
            {
                extraWeight = 0;
            }

            public static void Connect(Node node1, Node node2)
            {
                if (node1.nodes.Contains(node2)) return;
                node1.nodes.Add(node2);
                node2.nodes.Add(node1);
            }

            public void Connect(Node anotherNode)
            {
                if (nodes.Contains(anotherNode)) return;
                nodes.Add(anotherNode);
                anotherNode.nodes.Add(this);
            }

            public void Disconnect(Node anotherNode)
            {
                anotherNode.nodes.Remove(this);
                nodes.Remove(anotherNode);
            }

            public void Delete()
            {
                foreach (var node in nodes)
                {
                    Disconnect(node);
                }
            }
        }
    }
}