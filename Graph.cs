using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HG
{
    public class Graph
    {
        public List<int> Vertices { get; private set; }
        public List<Edge> Edges { get; private set; }
        public bool IsDirected { get; set; } = false;
        public List<int> HamiltonianPath { get; set; }

        private Dictionary<int, Point> positions;

        public Graph()
        {
            Vertices = new List<int>();
            Edges = new List<Edge>();
            positions = new Dictionary<int, Point>();
        }
        public void ParsePositions(int vertex, Point point)
        {
            positions.Clear();
            positions.Add(vertex, point);

        }


            
        public void AddVertex(int vertex, Point position)
        {
            if (!Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);
                positions[vertex] = position; // Сохраняем переданную позицию
            }
        }


        public void AddEdge(int source, int target)
        {
            if (IsDirected)
            {
                // Для ориентированного графа добавляем ребро строго Source -> Target
                if (!Edges.Any(edge => edge.Source == source && edge.Target == target))
                {
                    Edges.Add(new Edge(source, target));
                }
            }
            else
            {
                // Для неориентированного графа проверяем оба направления
                if (!Edges.Any(edge => (edge.Source == source && edge.Target == target) ||
                                       (edge.Source == target && edge.Target == source)))
                {
                    Edges.Add(new Edge(source, target));
                }
            }
        }

        public void UpdateVertexPosition(int vertex, Point newPosition)
        {
            if (positions.ContainsKey(vertex))
            {
                positions[vertex] = newPosition;
            }
        }


        public void RemoveVertex(int vertex)
        {
            if (Vertices.Contains(vertex))
            {
                Vertices.Remove(vertex);
                Edges.RemoveAll(edge => edge.Source == vertex || edge.Target == vertex);
                positions.Remove(vertex);
            }
        }

        public void RemoveEdge(Edge edge)
        {
            Edges.Remove(edge);
        }

        public Dictionary<int, Point> GetVertexPositions(Size panelSize)
        {
            if (positions.Count != Vertices.Count)
            {
                var random = new Random();
                foreach (var vertex in Vertices)
                {
                    if (!positions.ContainsKey(vertex))
                    {
                        int x = random.Next(50, panelSize.Width - 50);
                        int y = random.Next(50, panelSize.Height - 50);
                        positions[vertex] = new Point(x, y);
                    }
                }
            }
            return positions;
        }
        public void AddRange(List<int> path)
        {
            HamiltonianPath = path;
        }

        public void UpdateEdgeColor(Edge edge, Color color)
        {
            // Добавляем свойство Color в Edge и обновляем его здесь
            var targetEdge = Edges.FirstOrDefault(e => e.Equals(edge));
            if (targetEdge != null)
            {
                targetEdge.Color = color;
            }
        }

        public void Clear()
        {
            Vertices.Clear();
            Edges.Clear();
            positions.Clear();
        }
    }

    public class Edge
    {
        public int Source { get; }
        public int Target { get; }
        public Color Color { get; set; } = Color.Black;
        public Edge(int source, int target)
        {
            Source = source;
            Target = target;
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge edge)
            {
                return (Source == edge.Source && Target == edge.Target) ||
                       (Source == edge.Target && Target == edge.Source);
            }
            return false;
        }

    }
}
