using RecipeCalCalcV3.ChildForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeCalCalcV3
{
    public partial class MainForm : Form
    {
        // Child Forms Objects.
        private Form[] childForms = null;

        // Constant integers used to index 'childForm'.
        private const int HOME = 0;
        private const int LOGS = 1;
        private const int BUILDER = 2;

        public MainForm()
        {
            InitializeComponent();

            // Initializing Child Forms.
            childForms = new Form[3];
            childForms[HOME] = new HomeForm(this);
            childForms[LOGS] = new LogsForm(this);
            childForms[BUILDER] = new BuilderForm(this);

            // Setting Attributes and adding Child Forms to 'mainPanel'.
            initForm(childForms[HOME]);
            initForm(childForms[LOGS]);
            initForm(childForms[BUILDER]);
        }

        /**********************************************************************************/
        /*                                 INTERNAL USE                                   */
        /**********************************************************************************/

        /**
         * initForm() function takes in a Form and initializes it by setting attributes
         * and adding it to the 'mainPanel' inside MainForm.
         * 
         * @param form Form object that is added to 'mainPanel'.
         */
        private void initForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(form);
            this.mainPanel.Tag = form;
        }

        /**
         * activateForm() function takes in a Form object and compares it to the forms
         * inside the 'childForms' array.
         * If it's a match, show the matched childForm, otherwise hide.
         * 
         * @param form Form object that is compared to the forms in 'childForms' array.
         */
        private void activateForm(Form form)
        {
            foreach (Form childForm in childForms)
            {
                if (childForm != form)
                    childForm.Hide();
                else childForm.Show();
            }
        }

        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/
        private void homeButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "HOME";
            activateForm(childForms[HOME]);
        }
        private void viewLogsButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "LOGS";
            activateForm(childForms[LOGS]);
        }
        private void builderButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "BUILDER";
            activateForm(childForms[BUILDER]);
        }
        private void savedButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}


/*
 * OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
 * 
 */