using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace HG
{
    public partial class Form1 : Form
    {
        private Graph graph;                // ���� ��� �����
        private bool isAddingMode = true;   // ����� ���������� (�� ���������)
        private int? selectedVertex = null; // ��������� ������� ��� ���������� ����
        private int? draggingVertex = null; // ��������������� �������
        private Point draggingOffset;       // �������� ������� ������������ ������ �������
        private bool isAlgorithmRunning = false;
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

        // ������� ��������� �����
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            if (graph.Vertices.Count == 0) return;

            var positions = graph.GetVertexPositions(drawPanel.Size);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var pen = new Pen(Color.Black, 2);
            var font = new Font("Arial", 10);
            var brush = Brushes.LightBlue;
            var textBrush = Brushes.Black;

            // ������ ����
            foreach (var edge in graph.Edges)
            {
                var sourcePos = positions[edge.Source];
                var targetPos = positions[edge.Target];

                // ������ ����� �����
                pen.Color = edge.Color;
                g.DrawLine(pen, sourcePos, targetPos);

                // ���� ���� ���������������, ������ �������
                if (graph.IsDirected)
                {
                    // ������ ����������� ��� �������
                    var direction = new PointF(
                        targetPos.X - sourcePos.X,
                        targetPos.Y - sourcePos.Y
                    );
                    var length = Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
                    var unitDirection = new PointF(
                        (float)(direction.X / length),
                        (float)(direction.Y / length)
                    );

                    // ��������� �������
                    // ����� ������� (�� 10 �������� �����)
                    var offset = 20;

                    // ��������� �������
                    var arrowSize = 15; // ������ �������
                    var arrowBase = new PointF(
                        targetPos.X - unitDirection.X * (arrowSize + offset),  // ����� � ������ offset
                        targetPos.Y - unitDirection.Y * (arrowSize + offset)   // ����� � ������ offset
                    );
                    var arrowLeft = new PointF(
                        arrowBase.X + unitDirection.Y * arrowSize / 2,
                        arrowBase.Y - unitDirection.X * arrowSize / 2
                    );
                    var arrowRight = new PointF(
                        arrowBase.X - unitDirection.Y * arrowSize / 2,
                        arrowBase.Y + unitDirection.X * arrowSize / 2
                    );

                    // ������ ������� ��� �����������
                    g.FillPolygon(Brushes.Black, new[] { targetPos, arrowLeft, arrowRight });
                }
            }


            // ������ �������
            foreach (var vertex in graph.Vertices)
            {
                var pos = positions[vertex];
                var radius = 15;

                // ������ ���� �������
                g.FillEllipse(brush, pos.X - radius, pos.Y - radius, radius * 2, radius * 2);
                g.DrawEllipse(pen, pos.X - radius, pos.Y - radius, radius * 2, radius * 2);

                // ������ ����� � ������ �������
                var stringSize = g.MeasureString(vertex.ToString(), font);
                g.DrawString(vertex.ToString(), font, textBrush,
                    pos.X - stringSize.Width / 2, pos.Y - stringSize.Height / 2);
            }
        }




        // ������� �������� ����� ���� (���������� �������)
        private void DrawPanel_DoubleClick(object sender, MouseEventArgs e)
        {
            if (!isAddingMode || e.Button != MouseButtons.Left) return;

            int newVertex = graph.Vertices.Count > 0 ? graph.Vertices.Max() + 1 : 1;
            graph.AddVertex(newVertex, e.Location); // ������� ���������� �����
            drawPanel.Invalidate(); // �������������� ������
            UpdateComboBoxVertices();
            ResetGraph();
        }


        // ������� ����� ���� (����������/�������� ���� ��� ������)
        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            var positions = graph.GetVertexPositions(drawPanel.Size);

            if (ModifierKeys == Keys.Shift && e.Button == MouseButtons.Left)
            {
                // �������� �������
                foreach (var vertex in graph.Vertices)
                {
                    var pos = positions[vertex];
                    if (IsPointInCircle(e.Location, pos, 15))
                    {
                        graph.RemoveVertex(vertex);
                        drawPanel.Invalidate();
                        UpdateComboBoxVertices();
                        ResetGraph();
                        return;
                    }
                }

                // �������� �����
                foreach (var edge in graph.Edges.ToList())
                {
                    var sourcePos = positions[edge.Source];
                    var targetPos = positions[edge.Target];
                    if (IsPointNearLine(e.Location, sourcePos, targetPos, 5))
                    {
                        graph.RemoveEdge(edge);
                        drawPanel.Invalidate();
                        if (edge.Color == Color.Red)
                        { 
                            ResetGraph();
                        }
                        return;
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                // ���������� �����
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
                            ResetGraph();
                        }
                        return;
                    }
                }

                selectedVertex = null;
            }
        }

        // ��������, ��������� �� ����� ������ ����������
        private bool IsPointInCircle(Point point, Point center, int radius)
        {
            int dx = point.X - center.X;
            int dy = point.Y - center.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        // ��������, ��������� �� ����� ������ �����
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

            lblActions.Text = $"��������: {solver.ActionsCount}";
            lblExecutionTime.Text = $"�����: {solver.ExecutionTime.TotalMilliseconds} ��";

            if (result)
                MessageBox.Show("����������� ���� ������!");
            else
                MessageBox.Show("����������� ���� �� ������.");
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
            public bool IsCancelled { get; private set; } = false;

            public void Cancel()
            {
                IsCancelled = true;
            }

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
                if (result)
                {
                    graph.AddRange(visited);
                }
                return result;
            }

            private async Task<bool> Backtracking(Form1 form, int currentVertex)
            {
                if (IsCancelled) return false;

                visited.Add(currentVertex);

                if (visited.Count == graph.Vertices.Count &&
                    graph.Edges.Any(edge =>
                        (settings.IsDirected && edge.Source == currentVertex && edge.Target == settings.StartVertex) ||
                        (!settings.IsDirected && ((edge.Source == currentVertex && edge.Target == settings.StartVertex) ||
                                                   (edge.Target == currentVertex && edge.Source == settings.StartVertex)))))
                {
                    form.PaintItRed(new Edge(currentVertex, settings.StartVertex));
                    return true;
                }

                foreach (var edge in graph.Edges)
                {
                    if (IsCancelled) return false;

                    bool canVisit = settings.IsDirected
                        ? edge.Source == currentVertex && !visited.Contains(edge.Target)
                        : (edge.Source == currentVertex && !visited.Contains(edge.Target)) ||
                          (edge.Target == currentVertex && !visited.Contains(edge.Source));

                    if (canVisit)
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
        private void UpdateComboBoxVertices()
        {
            cmbStartVertex.Items.Clear(); // �������� ���������� ��������
            foreach (var vertex in graph.Vertices)
            {
                cmbStartVertex.Items.Add(vertex); // �������� ��� �������
            }

            if (cmbStartVertex.Items.Count > 0)
                cmbStartVertex.SelectedIndex = 0; // ������� ������ ������� �� ���������
        }
        private void cmbStartVertex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStartVertex.SelectedItem is int selectedVertex)
            {
                algorithmSettings.StartVertex = selectedVertex; // ���������� ��������� �������
            }
        }
        private void UpdateAlgorithmSettings()
        {
            int delay = Math.Max(0, (int)nudDelay.Value); // �����������, ��� �������� >= 0
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
            if (isAlgorithmRunning)
            {
                StopAlgorithm(); // ���������� ��������
            }
            else
            {
                StartAlgorithmWithUIUpdate(); // ��������� ��������
            }
        }

        private async void StartAlgorithmWithUIUpdate()
        {
            ResetGraph();
            UpdateAlgorithmSettings();
            isAlgorithmRunning = true;
            btnStartAlgorithm.Text = "����";
            await StartAlgorithm();
            if (!solver.IsCancelled)
            {
                isAlgorithmRunning = false;
                btnStartAlgorithm.Text = "����";
            }
        }

        private void StopAlgorithm()
        {
            if (solver != null)
            {
                solver.Cancel();
            }
            isAlgorithmRunning = false;
            btnStartAlgorithm.Text = "����";
        }

        private void ButtonIsDirect_Click(object sender, EventArgs e)
        {
            // ����������� ����� �����
            graph.IsDirected = !graph.IsDirected;

            // ��������� ����������� �����
            drawPanel.Invalidate();

            // �������� ������ ������
            algorithmSettings.IsDirected = graph.IsDirected;

            // ��������� � ������� ������
            string mode = graph.IsDirected ? "���������������" : "�����������������";
            MessageBox.Show($"���� ���������� � {mode} �����.");
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            graph.Clear();
            drawPanel.Invalidate();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // ������ ������ ��� ������ ���� ����������
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // �������� ���� ���������� �����
                    FileManager.SaveGraphToFile(graph, filePath); // ��������� ���� � ����
                }
            }
        }
        public void LoadFromFile(string filePath)
        {
            graph.Clear();
            graph = FileManager.LoadGraphFromFile(filePath); // ��������� ���� �� �����

            // ���������� ���������� ����� ��������
            drawPanel.Invalidate(); // ��������� ����������� �����
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // ������ ������ ��� ������ �����
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.DefaultExt = "json";
                openFileDialog.AddExtension = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName; // �������� ���� ���������� �����
                    LoadFromFile(filePath);

                }
            }
        }
    }
}
