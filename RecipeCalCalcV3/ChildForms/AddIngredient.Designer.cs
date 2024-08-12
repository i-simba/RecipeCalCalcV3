namespace RecipeCalCalcV3.ChildForms
{
    partial class AddIngredient
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.ingNameTB = new System.Windows.Forms.TextBox();
            this.ingCalTB = new System.Windows.Forms.TextBox();
            this.ingWeightTB = new System.Windows.Forms.TextBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.courseCombo = new System.Windows.Forms.ComboBox();
            this.addPicButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(102)))), ((int)(((byte)(89)))));
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.SystemColors.Control;
            this.cancelButton.Location = new System.Drawing.Point(15, 205);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "CANCEL";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(142)))), ((int)(((byte)(129)))));
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.SystemColors.Control;
            this.okButton.Location = new System.Drawing.Point(110, 205);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 30);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ingNameTB
            // 
            this.ingNameTB.ForeColor = System.Drawing.Color.Gray;
            this.ingNameTB.Location = new System.Drawing.Point(30, 30);
            this.ingNameTB.Name = "ingNameTB";
            this.ingNameTB.Size = new System.Drawing.Size(140, 20);
            this.ingNameTB.TabIndex = 2;
            this.ingNameTB.Text = "Name";
            this.ingNameTB.Click += new System.EventHandler(this.ingNameTB_Click);
            this.ingNameTB.Enter += new System.EventHandler(this.ingNameTB_Enter);
            this.ingNameTB.Leave += new System.EventHandler(this.ingNameTB_Leave);
            // 
            // ingCalTB
            // 
            this.ingCalTB.ForeColor = System.Drawing.Color.Gray;
            this.ingCalTB.Location = new System.Drawing.Point(30, 60);
            this.ingCalTB.Name = "ingCalTB";
            this.ingCalTB.Size = new System.Drawing.Size(140, 20);
            this.ingCalTB.TabIndex = 3;
            this.ingCalTB.Text = "Calories";
            this.ingCalTB.Click += new System.EventHandler(this.ingCalTB_Click);
            this.ingCalTB.Enter += new System.EventHandler(this.ingCalTB_Enter);
            this.ingCalTB.Leave += new System.EventHandler(this.ingCalTB_Leave);
            // 
            // ingWeightTB
            // 
            this.ingWeightTB.ForeColor = System.Drawing.Color.Gray;
            this.ingWeightTB.Location = new System.Drawing.Point(30, 90);
            this.ingWeightTB.Name = "ingWeightTB";
            this.ingWeightTB.Size = new System.Drawing.Size(140, 20);
            this.ingWeightTB.TabIndex = 4;
            this.ingWeightTB.Text = "Weight";
            this.ingWeightTB.Click += new System.EventHandler(this.ingWeightTB_Click);
            this.ingWeightTB.Enter += new System.EventHandler(this.ingWeightTB_Enter);
            this.ingWeightTB.Leave += new System.EventHandler(this.ingWeightTB_Leave);
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Items.AddRange(new object[] {
            "Protein",
            "Veggie",
            "Liquids",
            "Misc"});
            this.typeCombo.Location = new System.Drawing.Point(30, 125);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(71, 21);
            this.typeCombo.TabIndex = 7;
            // 
            // courseCombo
            // 
            this.courseCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.courseCombo.FormattingEnabled = true;
            this.courseCombo.Items.AddRange(new object[] {
            "Entre",
            "Base",
            "Snacks"});
            this.courseCombo.Location = new System.Drawing.Point(30, 158);
            this.courseCombo.Name = "courseCombo";
            this.courseCombo.Size = new System.Drawing.Size(71, 21);
            this.courseCombo.TabIndex = 8;
            // 
            // addPicButton
            // 
            this.addPicButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.addPicButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addPicButton.FlatAppearance.BorderSize = 0;
            this.addPicButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addPicButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPicButton.Location = new System.Drawing.Point(106, 120);
            this.addPicButton.Name = "addPicButton";
            this.addPicButton.Size = new System.Drawing.Size(64, 64);
            this.addPicButton.TabIndex = 9;
            this.addPicButton.Text = "ADD PIC";
            this.addPicButton.UseVisualStyleBackColor = false;
            this.addPicButton.Click += new System.EventHandler(this.addPicButton_Click);
            // 
            // AddIngredient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(211)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(200, 250);
            this.Controls.Add(this.addPicButton);
            this.Controls.Add(this.courseCombo);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.ingWeightTB);
            this.Controls.Add(this.ingCalTB);
            this.Controls.Add(this.ingNameTB);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddIngredient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Ingredient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox ingNameTB;
        private System.Windows.Forms.TextBox ingCalTB;
        private System.Windows.Forms.TextBox ingWeightTB;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.ComboBox courseCombo;
        private System.Windows.Forms.Button addPicButton;
    }
}