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
            this.drawPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.White;
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
            this.lblExecutionTime.Location = new System.Drawing.Point(774, 41);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(45, 15);
            this.lblExecutionTime.TabIndex = 5;
            this.lblExecutionTime.Text = "Время:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.flowLayoutPanel1.Controls.Add(this.btnStartAlgorithm);
            this.flowLayoutPanel1.Controls.Add(this.nudDelay);
            this.flowLayoutPanel1.Controls.Add(this.cmbStartVertex);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1239, 38);
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
            this.nudDelay.Location = new System.Drawing.Point(84, 3);
            this.nudDelay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(120, 23);
            this.nudDelay.TabIndex = 7;
            // 
            // cmbStartVertex
            // 
            this.cmbStartVertex.FormattingEnabled = true;
            this.cmbStartVertex.Location = new System.Drawing.Point(210, 3);
            this.cmbStartVertex.Name = "cmbStartVertex";
            this.cmbStartVertex.Size = new System.Drawing.Size(43, 23);
            this.cmbStartVertex.TabIndex = 8;
            this.cmbStartVertex.SelectedIndexChanged += new System.EventHandler(this.cmbStartVertex_SelectedIndexChanged);
            // 
            // lblActions
            // 
            this.lblActions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblActions.AutoSize = true;
            this.lblActions.BackColor = System.Drawing.Color.Transparent;
            this.lblActions.Enabled = false;
            this.lblActions.Location = new System.Drawing.Point(346, 41);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(65, 15);
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
    }
}