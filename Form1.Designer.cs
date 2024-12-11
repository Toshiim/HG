namespace HG
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawPanel = new DoubleBufferedPanel();
            this.lblExecutionTime = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStartAlgorithm = new System.Windows.Forms.Button();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.cmbStartVertex = new System.Windows.Forms.ComboBox();
            this.lblActions = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGraph = new System.Windows.Forms.TabPage();
            this.tabFile = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.drawPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.White;
            this.drawPanel.Controls.Add(this.flowLayoutPanel2);
            this.drawPanel.Controls.Add(this.lblExecutionTime);
            this.drawPanel.Controls.Add(this.flowLayoutPanel1);
            this.drawPanel.Controls.Add(this.lblActions);
            this.drawPanel.Controls.Add(this.label1);
            this.drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanel.Location = new System.Drawing.Point(0, 0);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(1239, 908);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawPanel_Paint);
            this.drawPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseClick);
            this.drawPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_DoubleClick);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseDown);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseMove);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawPanel_MouseUp);
            // 
            // lblExecutionTime
            // 
            this.lblExecutionTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblExecutionTime.AutoSize = true;
            this.lblExecutionTime.BackColor = System.Drawing.Color.Transparent;
            this.lblExecutionTime.Enabled = false;
            this.lblExecutionTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblExecutionTime.Location = new System.Drawing.Point(487, 35);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(58, 21);
            this.lblExecutionTime.TabIndex = 5;
            this.lblExecutionTime.Text = "Время:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.flowLayoutPanel1.Controls.Add(this.btnStartAlgorithm);
            this.flowLayoutPanel1.Controls.Add(this.nudDelay);
            this.flowLayoutPanel1.Controls.Add(this.cmbStartVertex);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(88, 101);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnStartAlgorithm
            // 
            this.btnStartAlgorithm.Location = new System.Drawing.Point(3, 3);
            this.btnStartAlgorithm.Name = "btnStartAlgorithm";
            this.btnStartAlgorithm.Size = new System.Drawing.Size(75, 26);
            this.btnStartAlgorithm.TabIndex = 5;
            this.btnStartAlgorithm.Text = "Пуск ";
            this.btnStartAlgorithm.UseVisualStyleBackColor = true;
            this.btnStartAlgorithm.Click += new System.EventHandler(this.btnStartAlgorithm_Click);
            // 
            // nudDelay
            // 
            this.nudDelay.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudDelay.Location = new System.Drawing.Point(3, 35);
            this.nudDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(75, 23);
            this.nudDelay.TabIndex = 7;
            // 
            // cmbStartVertex
            // 
            this.cmbStartVertex.FormattingEnabled = true;
            this.cmbStartVertex.Location = new System.Drawing.Point(3, 64);
            this.cmbStartVertex.Name = "cmbStartVertex";
            this.cmbStartVertex.Size = new System.Drawing.Size(75, 23);
            this.cmbStartVertex.TabIndex = 8;
            this.cmbStartVertex.SelectedIndexChanged += new System.EventHandler(this.cmbStartVertex_SelectedIndexChanged);
            // 
            // lblActions
            // 
            this.lblActions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblActions.AutoSize = true;
            this.lblActions.BackColor = System.Drawing.Color.Transparent;
            this.lblActions.Enabled = false;
            this.lblActions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblActions.Location = new System.Drawing.Point(162, 35);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(85, 21);
            this.lblActions.TabIndex = 4;
            this.lblActions.Text = "Действий: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(0, 824);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 84);
            this.label1.TabIndex = 9;
            this.label1.Text = "    \r\n    Управление:\r\n    Двойной клик ЛКМ для создания вершины\r\n    ЛКМ поочерё" +
    "дно на две вершины для создания ребра\r\n    Shift + ЛКМ для удаления вершины или " +
    "ребра\r\n\r\n";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.flowLayoutPanel2.Controls.Add(this.tabControl1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(855, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(377, 113);
            this.flowLayoutPanel2.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGraph);
            this.tabControl1.Controls.Add(this.tabFile);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(369, 100);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGraph
            // 
            this.tabGraph.Controls.Add(this.button2);
            this.tabGraph.Controls.Add(this.button1);
            this.tabGraph.Location = new System.Drawing.Point(4, 24);
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraph.Size = new System.Drawing.Size(361, 72);
            this.tabGraph.TabIndex = 0;
            this.tabGraph.Text = "Граф";
            this.tabGraph.UseVisualStyleBackColor = true;
            // 
            // tabFile
            // 
            this.tabFile.Location = new System.Drawing.Point(4, 24);
            this.tabFile.Name = "tabFile";
            this.tabFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabFile.Size = new System.Drawing.Size(361, 72);
            this.tabFile.TabIndex = 1;
            this.tabFile.Text = "Файл";
            this.tabFile.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::HG.Properties.Resources.Directed_svg;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(18, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 51);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(104, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 908);
            this.Controls.Add(this.drawPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.drawPanel.ResumeLayout(false);
            this.drawPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferedPanel drawPanel;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblExecutionTime;
        private Button btnStartAlgorithm;
        private NumericUpDown nudDelay;
        private ComboBox cmbStartVertex;
        private Label lblActions;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel2;
        private TabControl tabControl1;
        private TabPage tabGraph;
        private Button button2;
        private Button button1;
        private TabPage tabFile;
    }
}