namespace RecipeCalCalcV3.ChildForms
{
    partial class HomeForm
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
            this.ingDisplayPanel = new System.Windows.Forms.Panel();
            this.ingPicsAnim = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ingDisplayPanel
            // 
            this.ingDisplayPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ingDisplayPanel.Location = new System.Drawing.Point(45, 50);
            this.ingDisplayPanel.Name = "ingDisplayPanel";
            this.ingDisplayPanel.Size = new System.Drawing.Size(950, 74);
            this.ingDisplayPanel.TabIndex = 0;
            // 
            // ingPicsAnim
            // 
            this.ingPicsAnim.Enabled = true;
            this.ingPicsAnim.Interval = 30;
            this.ingPicsAnim.Tick += new System.EventHandler(this.ingPicsAnim_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(92)))), ((int)(((byte)(69)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(1005, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 74);
            this.button1.TabIndex = 1;
            this.button1.Text = "ADD INGREDIENT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1150, 850);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ingDisplayPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ingDisplayPanel;
        private System.Windows.Forms.Timer ingPicsAnim;
        private System.Windows.Forms.Button button1;
    }
}