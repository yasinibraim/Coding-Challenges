using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static int x, y;
        private static int[,] arr;
        static readonly List<(int, int)> deflection = new()
          {
              (-1, 0),
              (0, -1),
              (0, 1),
              (1, 0)
          };

        private static void ReadFile()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string inputFilePath = Path.Combine(basePath, @"..\..\..\input.txt");

            using (TextReader reader = File.OpenText(inputFilePath))
            {
                x = int.Parse(reader.ReadLine());
                y = int.Parse(reader.ReadLine());
            }
 
            var inputFile = File.ReadLines(inputFilePath).Skip(2);

            int i = 0;
            int j = 0;

            arr = new int[x, y];

            foreach (var row in inputFile)
            {
                j = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    arr[i, j] = int.Parse(col.Trim());
                    j++;
                }
                i++;
            }
        }

        private static void WriteResultToFile(int islandCount, int biggestIslandSize, int biggestLagoonSize)
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string outputFilePath = Path.Combine(basePath, @"..\..\..\output.txt");
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("Numarul de insule = " + (islandCount - 2).ToString());
                writer.WriteLine("Suprafata celei mai mari insule =  " + biggestIslandSize.ToString());
                writer.WriteLine("Suprafata celei mai mari lagune =  " + biggestLagoonSize.ToString());
            }
        }


        private static void ShowArr()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (arr[i, j] < 0) Console.Write(arr[i, j].ToString() + " "); 
                    else Console.Write(" " + arr[i, j].ToString() + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private static int IslandDFS(int n, int m, int currIsland, int size = 1)
        { 
            
            arr[n, m] = currIsland;
            ShowArr();
            foreach (var d in deflection)
            {
                var newX = n + d.Item1;
                var newY = m + d.Item2;
                if (newX >= 0 && newX < x && newY >= 0 && newY < y && arr[newX, newY] == 1)
                {
                    size = IslandDFS(newX, newY, currIsland, size + 1);
                }
            }
            return size;
        }

        private static int LagoonDFS(int n, int m, int size = 1)
        {
            arr[n, m] = -1;
            foreach (var d in deflection)
            {
                var newX = n + d.Item1;
                var newY = m + d.Item2;
                if (!(newX >= 1 && newX < x - 1 && newY >= 1 && newY < y - 1) && arr[newX, newY] == 0)
                    return -1;
                else if ((newX >= 1 && newX < x - 1 && newY >= 1 && newY < y - 1) && arr[newX, newY] == 0)
                {
                    size = LagoonDFS(newX, newY, size + 1);
                }
            }
            return size;
        }
        static void Main(string[] args)
        {
            ReadFile();

            ShowArr();
            int islandCount = 2;
            int biggestIslandSize = 0;
            int biggestLagoonSize = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        int size = IslandDFS(i, j, islandCount);

                        if (size > biggestIslandSize) 
                            biggestIslandSize = size;
                        islandCount++;
                    }
                }
            }

            for (int i = 1; i < x - 1; i++)
            {
                for (int j = 1; j < y - 1; j++)
                {
                    if (arr[i, j] == 0)
                    {
                        int size = LagoonDFS(i, j);
                        if (size > biggestLagoonSize) biggestLagoonSize = size;
                    }
                }
            }
            ShowArr();
            WriteResultToFile(islandCount, biggestIslandSize, biggestLagoonSize);

        }
    }
}