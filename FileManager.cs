using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

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
                Positions = graph.GetVertexPositions(new Size(0, 0)), // Получаем все позиции вершин
                IsDirected = graph.IsDirected
            };

            try
            {
                string json = JsonConvert.SerializeObject(graphData, Formatting.Indented);
                File.WriteAllText(filePath, json);
                MessageBox.Show($"Граф успешно сохранён в файл {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.Log($"Граф сохранён в файл: {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении графа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log($"Ошибка при сохранении графа: {ex.Message}");
            }
        }

        // Загрузка графа и гамильтонова пути из файла
        public static Graph LoadGraphFromFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var graphData = JsonConvert.DeserializeObject<GraphData>(json);

                Graph graph = new Graph();
                graph.IsDirected = graphData.IsDirected;

                // Восстанавливаем вершины и их позиции
                foreach (var vertex in graphData.Vertices)
                {
                    Point position = new Point(0, 0); // По умолчанию, если позиция не найдена
                    if (graphData.Positions != null && graphData.Positions.ContainsKey(vertex))
                    {
                        var coordinates = graphData.Positions[vertex].Split(',');
                        position = new Point(int.Parse(coordinates[0].Trim()), int.Parse(coordinates[1].Trim()));
                    }

                    graph.AddVertex(vertex, position); // Добавляем вершину с восстановленной позицией
                }

                // Восстанавливаем рёбра и их цвета
                foreach (var edgeData in graphData.Edges)
                {
                    Color edgeColor = ColorTranslator.FromHtml(edgeData.Color);
                    graph.AddEdge(edgeData.Source, edgeData.Target);
                    var edge = graph.Edges.LastOrDefault(e => e.Source == edgeData.Source && e.Target == edgeData.Target);
                    if (edge != null)
                    {
                        graph.UpdateEdgeColor(edge, edgeColor);
                    }
                }

                // Восстанавливаем гамильтонов путь
                if (graphData.HamiltonianPath != null)
                {
                    graph.HamiltonianPath = graphData.HamiltonianPath;
                }

                // Используем метод GetVertexPositions для получения всех позиций
                var positions = graph.GetVertexPositions(new Size(800, 600)); // Задаём размер панели (по необходимости)

                // Обновляем позиции вершин в графе
                foreach (var position in positions)
                {
                    graph.UpdateVertexPosition(position.Key, position.Value);
                }

                MessageBox.Show($"Граф успешно загружен из файла {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.Log($"Граф загружен из файла: {filePath}");
                return graph;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке графа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log($"Ошибка при загрузке графа: {ex.Message}");
                return null;
            }
        }

        // Класс для структуры данных, которую мы ожидаем из JSON
        public class GraphData
        {
            public List<int> Vertices { get; set; }
            public List<EdgeData> Edges { get; set; }
            public List<int> HamiltonianPath { get; set; }
            public Dictionary<int, string> Positions { get; set; }
            public bool IsDirected { get; set; }
        }

        public class EdgeData
        {
            public int Source { get; set; }
            public int Target { get; set; }
            public string Color { get; set; }
        }
    }
}
