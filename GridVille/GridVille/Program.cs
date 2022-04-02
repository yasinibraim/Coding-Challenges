using System;
using System.Collections.Generic;

namespace GridVille
{
    internal class Program
    {
        static int n, m;
        static int start_i = 0, start_j = 0;
        static int finish_i = 0, finish_j = 0;

        public static void Main()
        {
            ReadGridDimensions();
            ReadStartPosition();
            ReadEndPosition();
            var trafficJams = ReadTrafficJams();

            var vertices = new int[n * m];
            for (int i = 0; i < n * m; i++)
                vertices[i] = i;
            
            var edges = new List<Tuple<int,int>>();

            for (int i = 0; i < n * m; i++)
            {
                var line = i / m;
                var column = i % m;
                if (!trafficJams.Contains(new Tuple<int, int>(line,column)))
                    for (int j = -1; j <= 1; j += 2)
                {
                    if (IsPositionInsideField(line + j, column) && !trafficJams.Contains(new Tuple<int, int>(line + j, column)))
                        edges.Add(Tuple.Create(i, (line + j) * m + column));
                    if (IsPositionInsideField(line, column + j) && !trafficJams.Contains(new Tuple<int, int>(line, column + j)))
                        edges.Add(Tuple.Create(i, line * m + column + j));
                }
            }

            var graph = new Graph<int>(vertices, edges);
            var algorithms = new Algorithms();

            try
            {
                var path = algorithms.ShortestPathFunction(graph, start_i * m + start_j);
                var shortestPath = string.Join(", ", path(finish_i * m + finish_j));
                var pairs = new List<Tuple<int, int>>();
                var splitValues = shortestPath.Split(',');
                foreach(var val in splitValues)
                {
                    var parsedVal = Int32.Parse(val);
                    pairs.Add(new Tuple<int, int>(parsedVal % m, parsedVal / m));
                }
                Console.WriteLine("Shortest route length: {0}", pairs.Count-1);
                Console.Write("Shortest route: ");
                foreach(var pair in pairs)
                {
                    Console.Write("({0},{1})",pair.Item1,pair.Item2);
                    if (pair != pairs[pairs.Count-1])
                        Console.Write(",");
                }
                //Console.WriteLine("Shortest path: {0}", string.Join(", ", ));
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                Console.WriteLine("No solution.");
            }
        }
        private static bool IsPositionInsideField(int x, int y)
        {
            if (x < 0 || y < 0 || x >= n || y >= m) return false;
            return true;
        }

        static void ReadGridDimensions()
        {
            Console.Write("Grid height: ");
            n = Int32.Parse(Console.ReadLine())+1;
            Console.Write("Grid width: ");
            m = Int32.Parse(Console.ReadLine())+1;
        }

        static void ReadStartPosition()
        {
            Console.Write("Start position row: ");
            start_i = Int32.Parse(Console.ReadLine());
            Console.Write("Start position column: ");
            start_j = Int32.Parse(Console.ReadLine());
        }
        static void ReadEndPosition()
        {
            Console.Write("End position row: ");
            finish_i = Int32.Parse(Console.ReadLine());
            Console.Write("End position column: ");
            finish_j = Int32.Parse(Console.ReadLine());
        }

        static List<Tuple<int, int>> ReadTrafficJams()
        {
            Console.WriteLine("Insert traffic jams positions formated as: i1, j1; i2 j2");
            var input = Console.ReadLine();
            var pairs = input.Split(';');

            var result = new List<Tuple<int, int>>();
            foreach(var pair in pairs)
            {
                var split_pair = pair.Split(',');
                int result1, result2;
                try
                {
                    Int32.TryParse(split_pair[0], out result1);
                    Int32.TryParse(split_pair[1], out result2);
                    result.Add(new Tuple<int, int>(result2, result1));
                }
                catch (Exception ex) { };
            }
            return result;
        }
    }
}
