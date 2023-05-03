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
            this.ratPanel = new System.Windows.Forms.Panel();
            this.ratPicBox1 = new System.Windows.Forms.PictureBox();
            this.ratPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ratPicBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ratPanel
            // 
            this.ratPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ratPanel.Controls.Add(this.ratPicBox1);
            this.ratPanel.Location = new System.Drawing.Point(630, 10);
            this.ratPanel.Name = "ratPanel";
            this.ratPanel.Size = new System.Drawing.Size(510, 510);
            this.ratPanel.TabIndex = 0;
            // 
            // ratPicBox1
            // 
            this.ratPicBox1.Location = new System.Drawing.Point(0, 0);
            this.ratPicBox1.Name = "ratPicBox1";
            this.ratPicBox1.Size = new System.Drawing.Size(510, 510);
            this.ratPicBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ratPicBox1.TabIndex = 0;
            this.ratPicBox1.TabStop = false;
            this.ratPicBox1.Click += new System.EventHandler(this.ratPicBox1_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1150, 850);
            this.Controls.Add(this.ratPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.ratPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ratPicBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ratPanel;
        private System.Windows.Forms.PictureBox ratPicBox1;
    }
}