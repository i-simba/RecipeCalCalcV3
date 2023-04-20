namespace RecipeCalCalcV3
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainButtonPanel = new System.Windows.Forms.Panel();
            this.savedPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.savedButton = new System.Windows.Forms.Button();
            this.builderButton = new System.Windows.Forms.Button();
            this.viewLogsButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.titleTextBox = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.savedPanelTimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.mainButtonPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.panel1.Controls.Add(this.mainButtonPanel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 1000);
            this.panel1.TabIndex = 0;
            // 
            // mainButtonPanel
            // 
            this.mainButtonPanel.Controls.Add(this.savedPanel);
            this.mainButtonPanel.Controls.Add(this.savedButton);
            this.mainButtonPanel.Controls.Add(this.builderButton);
            this.mainButtonPanel.Controls.Add(this.viewLogsButton);
            this.mainButtonPanel.Controls.Add(this.homeButton);
            this.mainButtonPanel.Location = new System.Drawing.Point(5, 155);
            this.mainButtonPanel.Name = "mainButtonPanel";
            this.mainButtonPanel.Size = new System.Drawing.Size(140, 635);
            this.mainButtonPanel.TabIndex = 1;
            // 
            // savedPanel
            // 
            this.savedPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(171)))), ((int)(((byte)(167)))));
            this.savedPanel.Location = new System.Drawing.Point(0, 205);
            this.savedPanel.MaximumSize = new System.Drawing.Size(140, 400);
            this.savedPanel.MinimumSize = new System.Drawing.Size(140, 0);
            this.savedPanel.Name = "savedPanel";
            this.savedPanel.Size = new System.Drawing.Size(140, 400);
            this.savedPanel.TabIndex = 4;
            // 
            // savedButton
            // 
            this.savedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.savedButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.savedButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.savedButton.FlatAppearance.BorderSize = 3;
            this.savedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savedButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savedButton.ForeColor = System.Drawing.SystemColors.Control;
            this.savedButton.Location = new System.Drawing.Point(0, 150);
            this.savedButton.Name = "savedButton";
            this.savedButton.Size = new System.Drawing.Size(140, 50);
            this.savedButton.TabIndex = 3;
            this.savedButton.Text = "SAVED";
            this.savedButton.UseVisualStyleBackColor = false;
            this.savedButton.Click += new System.EventHandler(this.savedButton_Click);
            // 
            // builderButton
            // 
            this.builderButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.builderButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.builderButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.builderButton.FlatAppearance.BorderSize = 3;
            this.builderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.builderButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.builderButton.ForeColor = System.Drawing.SystemColors.Control;
            this.builderButton.Location = new System.Drawing.Point(0, 100);
            this.builderButton.Name = "builderButton";
            this.builderButton.Size = new System.Drawing.Size(140, 50);
            this.builderButton.TabIndex = 2;
            this.builderButton.Text = "BUILDER";
            this.builderButton.UseVisualStyleBackColor = false;
            this.builderButton.Click += new System.EventHandler(this.builderButton_Click);
            // 
            // viewLogsButton
            // 
            this.viewLogsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.viewLogsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.viewLogsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.viewLogsButton.FlatAppearance.BorderSize = 3;
            this.viewLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewLogsButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewLogsButton.ForeColor = System.Drawing.SystemColors.Control;
            this.viewLogsButton.Location = new System.Drawing.Point(0, 50);
            this.viewLogsButton.Name = "viewLogsButton";
            this.viewLogsButton.Size = new System.Drawing.Size(140, 50);
            this.viewLogsButton.TabIndex = 1;
            this.viewLogsButton.Text = "VIEW LOGS";
            this.viewLogsButton.UseVisualStyleBackColor = false;
            this.viewLogsButton.Click += new System.EventHandler(this.viewLogsButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.homeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.homeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.homeButton.FlatAppearance.BorderSize = 3;
            this.homeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeButton.ForeColor = System.Drawing.SystemColors.Control;
            this.homeButton.Location = new System.Drawing.Point(0, 0);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(140, 50);
            this.homeButton.TabIndex = 0;
            this.homeButton.Text = "HOME";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(140, 140);
            this.panel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(85)))), ((int)(((byte)(71)))));
            this.panel2.Controls.Add(this.titleTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(150, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1150, 150);
            this.panel2.TabIndex = 1;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleTextBox.Font = new System.Drawing.Font("Segoe UI", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTextBox.ForeColor = System.Drawing.SystemColors.Control;
            this.titleTextBox.Location = new System.Drawing.Point(0, 0);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(1150, 150);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.Text = "TITLE";
            this.titleTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(150, 150);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1150, 850);
            this.mainPanel.TabIndex = 2;
            // 
            // savedPanelTimer
            // 
            this.savedPanelTimer.Interval = 10;
            this.savedPanelTimer.Tick += new System.EventHandler(this.savedPanelTimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RecipeCalCalcV3.Properties.Resources.calories;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1300, 1000);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.mainButtonPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel mainButtonPanel;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button savedButton;
        private System.Windows.Forms.Button builderButton;
        private System.Windows.Forms.Button viewLogsButton;
        private System.Windows.Forms.Label titleTextBox;
        private System.Windows.Forms.FlowLayoutPanel savedPanel;
        private System.Windows.Forms.Timer savedPanelTimer;
    }
}

