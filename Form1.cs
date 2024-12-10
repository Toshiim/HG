using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HG
{
    public partial class Form1 : Form
    {
        private Graph graph;                // Поле для графа
        private bool isAddingMode = true;   // Режим добавления (по умолчанию)
        private int? selectedVertex = null; // Выбранная вершина для добавления рёбер
        private int? draggingVertex = null; // Перетаскиваемая вершина
        private Point draggingOffset;       // Смещение курсора относительно центра вершины

        public Form1()
        {
            InitializeComponent();
            InitializeGraph();
            InitializeAlgorithmSettings();
        }

        private void InitializeGraph()
        {
            graph = new Graph();
        }


        private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            var positions = graph.GetVertexPositions(drawPanel.Size);

            foreach (var vertex in graph.Vertices)
            {
                var pos = positions[vertex];
                if (IsPointInCircle(e.Location, pos, 15))
                {
                    draggingVertex = vertex;
                    draggingOffset = new Point(e.Location.X - pos.X, e.Location.Y - pos.Y);
                    return;
                }
            }
        }
        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingVertex.HasValue && e.Button == MouseButtons.Left)
            {
                graph.UpdateVertexPosition(draggingVertex.Value, new Point(e.Location.X - draggingOffset.X, e.Location.Y - draggingOffset.Y));
                drawPanel.Invalidate();
            }
        }

        private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            draggingVertex = null;
        }

        // Событие отрисовки графа
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            if (graph.Vertices.Count == 0) return;

            var positions = graph.GetVertexPositions(drawPanel.Size);
            var g = e.Graphics;

            var pen = new Pen(Color.Black, 2);
            var font = new Font("Arial", 10);
            var brush = Brushes.LightBlue;
            var textBrush = Brushes.Black;

            // Рисуем рёбра
            foreach (var edge in graph.Edges)
            {
                var sourcePos = positions[edge.Source];
                var targetPos = positions[edge.Target];
                pen.Color = edge.Color; // Используем цвет ребра
                g.DrawLine(pen, sourcePos, targetPos);
            }


            // Рисуем вершины
            foreach (var vertex in graph.Vertices)
            {
                var pos = positions[vertex];
                g.FillEllipse(brush, pos.X - 15, pos.Y - 15, 30, 30);
                g.DrawEllipse(pen, pos.X - 15, pos.Y - 15, 30, 30);

                var stringSize = g.MeasureString(vertex.ToString(), font);
                g.DrawString(vertex.ToString(), font, textBrush,
                    pos.X - stringSize.Width / 2, pos.Y - stringSize.Height / 2);
            }
        }

        // Событие двойного клика мыши (добавление вершины)
        private void DrawPanel_DoubleClick(object sender, MouseEventArgs e)
        {
            if (!isAddingMode || e.Button != MouseButtons.Left) return;

            int newVertex = graph.Vertices.Count > 0 ? graph.Vertices.Max() + 1 : 1;
            graph.AddVertex(newVertex, e.Location); // Передаём координаты клика
            drawPanel.Invalidate(); // Перерисовываем панель
        }


        // Событие клика мыши (добавление/удаление рёбер или вершин)
        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            var positions = graph.GetVertexPositions(drawPanel.Size);

            if (ModifierKeys == Keys.Shift && e.Button == MouseButtons.Left)
            {
                // Удаление вершины
                foreach (var vertex in graph.Vertices)
                {
                    var pos = positions[vertex];
                    if (IsPointInCircle(e.Location, pos, 15))
                    {
                        graph.RemoveVertex(vertex);
                        drawPanel.Invalidate();
                        return;
                    }
                }

                // Удаление ребра
                foreach (var edge in graph.Edges.ToList())
                {
                    var sourcePos = positions[edge.Source];
                    var targetPos = positions[edge.Target];
                    if (IsPointNearLine(e.Location, sourcePos, targetPos, 5))
                    {
                        graph.RemoveEdge(edge);
                        drawPanel.Invalidate();
                        return;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Добавление ребра
                foreach (var vertex in graph.Vertices)
                {
                    var pos = positions[vertex];
                    if (IsPointInCircle(e.Location, pos, 15))
                    {
                        if (selectedVertex == null)
                        {
                            selectedVertex = vertex;
                        }
                        else
                        {
                            graph.AddEdge(selectedVertex.Value, vertex);
                            selectedVertex = null;
                            drawPanel.Invalidate();
                        }
                        return;
                    }
                }

                selectedVertex = null;
            }
        }

        // Проверка, находится ли точка внутри окружности
        private bool IsPointInCircle(Point point, Point center, int radius)
        {
            int dx = point.X - center.X;
            int dy = point.Y - center.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        // Проверка, находится ли точка вблизи линии
        private bool IsPointNearLine(Point point, Point lineStart, Point lineEnd, int tolerance)
        {
            var dx = lineEnd.X - lineStart.X;
            var dy = lineEnd.Y - lineStart.Y;
            var lengthSquared = dx * dx + dy * dy;

            if (lengthSquared == 0) return false;

            var t = ((point.X - lineStart.X) * dx + (point.Y - lineStart.Y) * dy) / (float)lengthSquared;
            t = Math.Max(0, Math.Min(1, t));

            var nearestX = lineStart.X + t * dx;
            var nearestY = lineStart.Y + t * dy;
            var distanceSquared = (point.X - nearestX) * (point.X - nearestX) + (point.Y - nearestY) * (point.Y - nearestY);

            return distanceSquared <= tolerance * tolerance;
        }

        // Переключение на режим добавления
        private void addModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            isAddingMode = true;
            selectedVertex = null;
        }

        // Переключение на режим удаления
        private void removeModeRadio_CheckedChanged(object sender, EventArgs e)
        {
            isAddingMode = false;
            selectedVertex = null;
        }
        private AlgorithmSettings algorithmSettings;
        private HamiltonianCycleSolver solver;

        private void InitializeAlgorithmSettings()
        {
            algorithmSettings = new AlgorithmSettings(1, 0, false);
        }

        private async Task StartAlgorithm()
        {
            solver = new HamiltonianCycleSolver(graph, algorithmSettings);
            var result = await solver.FindCycleAsync(this);

            lblActions.Text = $"Действий: {solver.ActionsCount}";
            lblExecutionTime.Text = $"Время: {solver.ExecutionTime.TotalMilliseconds} мс";

            if (result)
                MessageBox.Show("Гамильтонов цикл найден!");
            else
                MessageBox.Show("Гамильтонов цикл не найден.");
        }

        public void PaintItRed(Edge edge)
        {
            graph.UpdateEdgeColor(edge, Color.Red);
            drawPanel.Invalidate();
        }

        public void PaintItBlack(Edge edge)
        {
            graph.UpdateEdgeColor(edge, Color.Black);
            drawPanel.Invalidate();
        }
        public class AlgorithmSettings
        {
            public int StartVertex { get; set; }
            public int Delay { get; set; }
            public bool IsDirected { get; set; }

            public AlgorithmSettings(int startVertex, int delay, bool isDirected)
            {
                StartVertex = startVertex;
                Delay = delay;
                IsDirected = isDirected;
            }
        }
        public class HamiltonianCycleSolver
        {
            private Graph graph;
            private AlgorithmSettings settings;
            private List<Edge> currentPath;
            private List<int> visited;
            private DateTime startTime;
            public int ActionsCount { get; private set; }
            public TimeSpan ExecutionTime { get; private set; }

            public HamiltonianCycleSolver(Graph graph, AlgorithmSettings settings)
            {
                this.graph = graph;
                this.settings = settings;
                currentPath = new List<Edge>();
                visited = new List<int>();
            }

            public async Task<bool> FindCycleAsync(Form1 form)
            {
                ActionsCount = 0;
                startTime = DateTime.Now;
                visited.Clear();
                currentPath.Clear();

                bool result = await Backtracking(form, settings.StartVertex);
                ExecutionTime = DateTime.Now - startTime;
                return result;
            }

            private async Task<bool> Backtracking(Form1 form, int currentVertex)
            {
                visited.Add(currentVertex);

                if (visited.Count == graph.Vertices.Count &&
                    graph.Edges.Any(edge => edge.Source == currentVertex && edge.Target == settings.StartVertex))
                {
                    form.PaintItRed(new Edge(currentVertex, settings.StartVertex));
                    return true;
                }


                foreach (var edge in graph.Edges)
                {
                    if ((edge.Source == currentVertex && !visited.Contains(edge.Target)) ||
                        (edge.Target == currentVertex && !visited.Contains(edge.Source)))
                    {
                        ActionsCount++;
                        form.PaintItRed(edge);
                        currentPath.Add(edge);
                        await Task.Delay(settings.Delay);

                        int nextVertex = edge.Source == currentVertex ? edge.Target : edge.Source;
                        if (await Backtracking(form, nextVertex))
                            return true;

                        form.PaintItBlack(edge);
                        currentPath.Remove(edge);
                    }
                }


                visited.Remove(currentVertex);
                return false;
            }
        }
        private void UpdateAlgorithmSettings()
        {
            int delay = Math.Max(0, (int)nudDelay.Value); // Гарантируем, что задержка >= 0
            algorithmSettings.Delay = delay;
        }
        public void ResetGraph()
        {
            foreach (var edge in graph.Edges)
            {
                graph.UpdateEdgeColor(edge, Color.Black);
            }
            drawPanel.Invalidate();
        }
        private void btnStartAlgorithm_Click(object sender, EventArgs e)
        {
            ResetGraph();
            UpdateAlgorithmSettings();
            _ = StartAlgorithm();
        }
    }
}
