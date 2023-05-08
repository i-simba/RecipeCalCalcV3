namespace RecipeCalCalcV3.ChildForms
{
    partial class LogsForm
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
            this.logPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ingredientDataPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ingredientMacroPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.loadButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logPanel
            // 
            this.logPanel.AutoScroll = true;
            this.logPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(130)))), ((int)(((byte)(126)))));
            this.logPanel.Location = new System.Drawing.Point(10, 60);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(310, 725);
            this.logPanel.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(10, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 50);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "SELECT LOG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ingredientDataPanel
            // 
            this.ingredientDataPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ingredientDataPanel.Location = new System.Drawing.Point(330, 5);
            this.ingredientDataPanel.Name = "ingredientDataPanel";
            this.ingredientDataPanel.Size = new System.Drawing.Size(400, 835);
            this.ingredientDataPanel.TabIndex = 5;
            // 
            // ingredientMacroPanel
            // 
            this.ingredientMacroPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ingredientMacroPanel.Location = new System.Drawing.Point(740, 5);
            this.ingredientMacroPanel.Name = "ingredientMacroPanel";
            this.ingredientMacroPanel.Size = new System.Drawing.Size(400, 835);
            this.ingredientMacroPanel.TabIndex = 6;
            // 
            // loadButton
            // 
            this.loadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(92)))), ((int)(((byte)(69)))));
            this.loadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loadButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.ForeColor = System.Drawing.SystemColors.Control;
            this.loadButton.Location = new System.Drawing.Point(10, 790);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(310, 50);
            this.loadButton.TabIndex = 7;
            this.loadButton.Text = "LOAD IN BUILDER";
            this.loadButton.UseVisualStyleBackColor = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1150, 850);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.ingredientMacroPanel);
            this.Controls.Add(this.ingredientDataPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.logPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogsForm";
            this.Text = "LogsForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel logPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel ingredientDataPanel;
        private System.Windows.Forms.FlowLayoutPanel ingredientMacroPanel;
        private System.Windows.Forms.Button loadButton;
    }
}