using System.Collections.Generic;
using System.Linq;
using SnakeBattle.Api;

namespace Client
{
	class GraphCreator
	{
        public static Graph Create(GameBoard gameBoard)
        {
            Graph graph = new Graph();
            var size = gameBoard.Size;

            for(int i=0;i<size;i++)
                for(int j=0;j<size;j++)
                {
                    BoardElement element = gameBoard.Board[i, j];
                    if (ElementHelper.IsWall(element))
                        continue;
                    BoardPoint point = new BoardPoint(i, j);

                    graph.AddNode(point, element);
                }



            //for (var i = 0; i < objects.Count; ++i)
            //{
            //    if (objects[i].IsOutOfBoard(gameBoard.Size)) continue;
            //    graph.AddNode(objects[i], gameBoard.Board[]);
            //    ++t;
            //}
            //AddNeighborhood(graph, gameBoard);
            return graph;
        }

        //private static void AddNeighborhood(Graph graph, GameBoard gameBoard)
        //{
        //    var objects = gameBoard.Objects;
        //    foreach (var gameBoardObject in objects)
        //    {
        //        if (gameBoardObject.Wall != null) continue;
        //        foreach (var location in gameBoardObject.Location.ToLocation().Neighborhood)
        //        {
        //            if (graph.IsLocationInGraph(location))
        //                graph.Connect(gameBoardObject.Location.ToLocation(), location);
        //            else if (location.X >= 0 && location.X < gameBoard.Width && location.Y >= 0 && location.Y < gameBoard.Height &&
        //                    !graph.BorderLocations.Contains(location))
        //            {
        //                graph.BorderLocations.Add(location);
        //                graph.AddNode(location);
        //            }
        //        }
        //    }
        //}

        //public static void CheckBorder(Graph graph, GameBoard gameBoard)
        //{
        //    var borderList = new List<Location>(graph.BorderLocations.Count);
        //    foreach (var borderLocation in graph.BorderLocations)
        //    {
        //        var objectData = Program.GetObjectAt(gameBoard, borderLocation);
        //        if (objectData == null)
        //        {
        //            borderList.Add(borderLocation);
        //            continue;
        //        }
        //        if (objectData.Wall == null)
        //            graph.AddData(objectData);
        //        foreach (var location in borderLocation.Neighborhood)
        //        {
        //            if (!graph.IsLocationInGraph(location) &&
        //                location.X >= 0 && location.X < gameBoard.Width && location.Y >= 0 && location.Y < gameBoard.Height &&
        //                !borderList.Contains(location) && !graph.BorderLocations.Contains(location))
        //            {
        //                borderList.Add(location);
        //                graph.AddNode(location);
        //            }
        //        }
        //    }
        //    graph.BorderLocations = borderList;
        //}

        //public static void UpdateGraph(HommSensorData sensorData, Graph graph)
        //{
        //    CheckBorder(graph, sensorData.Map);
        //    UpdateNodes(sensorData, graph);
        //    DeleteUselessWalls(graph);
        //}

        //private static void UpdateNodes(HommSensorData sensorData, Graph graph)
        //{
        //    foreach (var gameBoardObject in sensorData.Map.Objects)
        //    {
        //        graph.UpdateData(gameBoardObject, sensorData);
        //    }
        //}


        ////TODO: Тестить
        //private static void DeleteUselessWalls(Graph graph)
        //{
        //    Stack<Graph.Node> stack;
        //    var dict = new Dictionary<Location, bool>();
        //    foreach (var node in graph.BorderLocations)
        //        dict.Add(node, true);

        //    var wall = graph.BorderLocations.Select(x => graph[x]).Where(x => x.IsCreated && x.SavedData.Wall != null && dict[x.Location]).FirstOrDefault();
        //    while (wall != null)
        //    {
        //        var walls = new List<Graph.Node>();
        //        bool t = true;
        //        stack = new Stack<Graph.Node>();
        //        stack.Push(wall);
        //        while (stack.Count > 0)
        //        {
        //            var tWall = stack.Pop();
        //            if (!tWall.IsCreated)
        //            {
        //                t = false;
        //                break;
        //            }
        //            walls.Add(wall);
        //            foreach (var neighbor in tWall.Location.Neighborhood.Where(x => graph.BorderLocations.Contains(x) && dict[x])) //TODO: обработать соседние с горой клетки на существование
        //            {
        //                dict[wall.Location] = false;
        //                stack.Push(graph[neighbor]);
        //            }
        //        }
        //        if (t)
        //            foreach (var e in walls)
        //            {
        //                graph.BorderLocations.Remove(e.Location);
        //                graph.DeleteNode(e);
        //            }
        //        wall = graph.BorderLocations.Select(x => graph[x]).Where(x => x.IsCreated && x.SavedData.Wall != null && dict[x.Location]).FirstOrDefault();
        //    }
        //}
    }
}
