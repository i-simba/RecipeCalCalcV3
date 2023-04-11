namespace RecipeCalCalcV3.ChildForms
{
    partial class BuilderForm
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
            this.miscPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.liquidPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.veggiePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.proteinPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // miscPanel
            // 
            this.miscPanel.AutoScroll = true;
            this.miscPanel.BackColor = System.Drawing.Color.Silver;
            this.miscPanel.Location = new System.Drawing.Point(830, 640);
            this.miscPanel.Name = "miscPanel";
            this.miscPanel.Size = new System.Drawing.Size(310, 200);
            this.miscPanel.TabIndex = 7;
            // 
            // liquidPanel
            // 
            this.liquidPanel.AutoScroll = true;
            this.liquidPanel.BackColor = System.Drawing.Color.Silver;
            this.liquidPanel.Location = new System.Drawing.Point(830, 430);
            this.liquidPanel.Name = "liquidPanel";
            this.liquidPanel.Size = new System.Drawing.Size(310, 200);
            this.liquidPanel.TabIndex = 6;
            // 
            // veggiePanel
            // 
            this.veggiePanel.AutoScroll = true;
            this.veggiePanel.BackColor = System.Drawing.Color.Silver;
            this.veggiePanel.Location = new System.Drawing.Point(830, 220);
            this.veggiePanel.Name = "veggiePanel";
            this.veggiePanel.Size = new System.Drawing.Size(310, 200);
            this.veggiePanel.TabIndex = 5;
            // 
            // proteinPanel
            // 
            this.proteinPanel.AutoScroll = true;
            this.proteinPanel.BackColor = System.Drawing.Color.Silver;
            this.proteinPanel.Location = new System.Drawing.Point(830, 10);
            this.proteinPanel.Name = "proteinPanel";
            this.proteinPanel.Size = new System.Drawing.Size(310, 200);
            this.proteinPanel.TabIndex = 4;
            // 
            // BuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(221)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(1150, 850);
            this.Controls.Add(this.miscPanel);
            this.Controls.Add(this.liquidPanel);
            this.Controls.Add(this.veggiePanel);
            this.Controls.Add(this.proteinPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BuilderForm";
            this.Text = "BuilderForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel miscPanel;
        private System.Windows.Forms.FlowLayoutPanel liquidPanel;
        private System.Windows.Forms.FlowLayoutPanel veggiePanel;
        private System.Windows.Forms.FlowLayoutPanel proteinPanel;
    }
}