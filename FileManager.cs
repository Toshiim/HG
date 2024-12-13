using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Drawing;

namespace HG
{
    public static class FileManager
    {
        // Сохранение графа и гамильтонова пути в файл
        public static void SaveGraphToFile(Graph graph, string filePath)
        {
            var graphData = new
            {
                Vertices = graph.Vertices,
                Edges = graph.Edges.Select(e => new { e.Source, e.Target, e.Color }).ToList(),
                HamiltonianPath = graph.HamiltonianPath,
                Positions = graph.GetVertexPositions(new Size(0, 0)) // Получаем все позиции вершин
            };

            try
            {
                string json = JsonConvert.SerializeObject(graphData, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Граф успешно сохранён в файл {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении графа: {ex.Message}");
            }
        }

        // Загрузка графа и гамильтонова пути из файла
        public static Graph LoadGraphFromFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var graphData = JsonConvert.DeserializeObject<dynamic>(json);

                Graph graph = new Graph();

                // Восстанавливаем вершины
                foreach (var vertex in graphData.Vertices)
                {
                    graph.AddVertex(vertex, new Point(0, 0)); // Позиции будут сгенерированы при необходимости
                }

                // Восстанавливаем рёбра
                foreach (var edge in graphData.Edges)
                {
                    graph.AddEdge((int)edge.Source, (int)edge.Target);
                }

                // Восстанавливаем гамильтонов путь
                if (graphData.HamiltonianPath != null)
                {
                    graph.HamiltonianPath = graphData.HamiltonianPath.ToObject<List<int>>();
                }

                // Восстанавливаем позиции вершин
                if (graphData.Positions != null)
                {
                    foreach (var position in graphData.Positions)
                    {
                        int vertex = position.Name;
                        var point = new Point((int)position.Value.X, (int)position.Value.Y);
                        graph.UpdateVertexPosition(vertex, point);
                    }
                }

                Console.WriteLine($"Граф успешно загружен из файла {filePath}");
                return graph;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке графа: {ex.Message}");
                return null;
            }
        }
    }
}
