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
            this.addIngredientButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
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
            // addIngredientButton
            // 
            this.addIngredientButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(92)))), ((int)(((byte)(69)))));
            this.addIngredientButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addIngredientButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addIngredientButton.ForeColor = System.Drawing.SystemColors.Control;
            this.addIngredientButton.Location = new System.Drawing.Point(1005, 50);
            this.addIngredientButton.Name = "addIngredientButton";
            this.addIngredientButton.Size = new System.Drawing.Size(100, 74);
            this.addIngredientButton.TabIndex = 1;
            this.addIngredientButton.Text = "ADD INGREDIENT";
            this.addIngredientButton.UseVisualStyleBackColor = false;
            this.addIngredientButton.Click += new System.EventHandler(this.addIngredientButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(121)))), ((int)(((byte)(102)))));
            this.panel1.Location = new System.Drawing.Point(35, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 94);
            this.panel1.TabIndex = 2;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1150, 850);
            this.Controls.Add(this.addIngredientButton);
            this.Controls.Add(this.ingDisplayPanel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ingDisplayPanel;
        private System.Windows.Forms.Timer ingPicsAnim;
        private System.Windows.Forms.Button addIngredientButton;
        private System.Windows.Forms.Panel panel1;
    }
}