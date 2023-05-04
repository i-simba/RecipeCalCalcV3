using RecipeCalCalcV3.ChildForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * TODO ::
 * #. 
 */

namespace RecipeCalCalcV3
{
    public partial class MainForm : Form
    {
        // Child Forms Objects.
        private Form[] childForms = null;
        private HomeForm hForm = null;
        private LogsForm lForm = null;
        private BuilderForm bForm = null;

        // Constant integers used to index 'childForm'.
        private const int HOME = 0;
        private const int LOGS = 1;
        private const int BUILDER = 2;

        // List of buttons dynamically created containing saved recipes.
        private List<Button> ingButtons = null;

        // List of ingredients assigned from loaded recipe.
        private List<List<String>> ingLists = null;

        // Animation variable.
        private bool isSavedExpanded = false;
        private int openInt = 30;
        private int closeInt = 60;

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public MainForm()
        {
            InitializeComponent();

            // Initialize 'childForms' array.
            childForms = new Form[3];

            // Initializing Child Forms.
            childForms[BUILDER] = new BuilderForm(this);
            childForms[HOME] = new HomeForm(this);
            childForms[LOGS] = new LogsForm(this, childForms[BUILDER]);
            

            // Setting Attributes and adding Child Forms to 'mainPanel'.
            initForm(childForms[HOME]);
            initForm(childForms[LOGS]);
            initForm(childForms[BUILDER]);

            // Assigning childForms elements to child forms.
            hForm = (HomeForm)childForms[HOME];
            lForm = (LogsForm)childForms[LOGS];
            bForm = (BuilderForm)childForms[BUILDER];

            // Initialize buttons list.
            ingButtons = new List<Button>();

            // Initialize ingredient lists.
            ingLists = new List<List<String>>();

            // Initialize saved recipes.
            initSaved();
        }

        /**********************************************************************************/
        /*                                 INTERNAL USE                                   */
        /**********************************************************************************/

        /**
         * MainForm_Load() function sets the default opening screen to be the 'HOME' screen
         * as well as ensuring the 'savedPanel' starts off collapsed.
         */
        private void MainForm_Load(object sender, EventArgs e)
        {
            savedPanel.Size = savedPanel.MinimumSize;
            homeButton_Click(sender, e);
        }

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
         * initSaved() function loads saved recipes from the 'savedIngredientPath' directory.
         * Each recipe is saved within its own text file, and the contents are saved inside 'ingLists'.
         */
        private void initSaved()
        {
            StreamReader reader = null;

            String[] fNames = Directory.GetFiles(Program.savedIngredientPath);
            foreach (String name in fNames)
            {
                List<String> temp = new List<String>();
                reader = new StreamReader(name);

                while (!reader.EndOfStream)
                {
                    temp.Add(reader.ReadLine());
                }
                ingLists.Add(temp);
                addSavedButton(Path.GetFileNameWithoutExtension(name).ToUpper(), ingLists.IndexOf(temp));
                reader.Close();
            }
        }

        /**
         * addSavedButton() function dynamically creates button objects which corresponds to a saved recipe.
         * 
         * @param n assigned to 'temp.Text'.
         * @param i assinged to 'temp.Tag'.
         */
        private void addSavedButton(String n, int i)
        {
            Button temp = new Button();

            temp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(171)))), ((int)(((byte)(167)))));
            temp.FlatAppearance.BorderSize = 1;
            temp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            temp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(143)))), ((int)(((byte)(138)))));
            temp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            temp.Cursor = System.Windows.Forms.Cursors.Hand;
            temp.ForeColor = System.Drawing.Color.White;
            temp.Location = new System.Drawing.Point(0);
            temp.Size = new System.Drawing.Size(134, 50);
            temp.Dock = DockStyle.Top;
            temp.Text = n;
            temp.Tag = i;
            temp.Click += new EventHandler(button_Click);

            savedPanel.Controls.Add(temp);
            ingButtons.Add(temp);
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

        /**
         * button_Click() function listens for click events from buttons inside 'ingButtons'.
         * This function utilizes the button list present inside BuilderForm and compare the ingredient name
         * to that list's button name.
         * If the strings match, the matched button will be clicked.
         */
        private void button_Click(object sender, EventArgs e)
        {
            activateForm(childForms[BUILDER]);
            List<Button> tempBL = bForm.getIngredientButtons();
            bForm.reset();

            foreach (Button button in ingButtons)
            {
                if (sender != button) continue;
                titleTextBox.Text = button.Text;
                for (int i = 0; i  < ingLists.Count; i++)
                {
                    if (i != (int)button.Tag) continue;
                    foreach (String str in ingLists[i])
                        foreach (Button btn in tempBL)
                            if (btn.Name.Equals(str))
                                bForm.button_Click(btn, e);
                }
            }
        }

        /**
         * homeButton_Click() function listens to 'homeButton'.
         * This fucntion sets the titleTextBox Text to "HOME" and
         * calls on activateForm() to activate the home form.
         */
        public void homeButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "HOME";
            activateForm(childForms[HOME]);
        }

        /**
         * viewLogsButton_Click() function listens to 'viewLogsButton'.
         * This function checks the Boolean variable 'logAdded' inside Program.
         * If 'logAdded' is true, this function will call on the reset() function in LogsForm
         * to reset and rebuild the log buttons in 'logPanel'.
         * The function then resets 'logAdded' to false, and calls on activateForm() to
         * activate the LogsForm.
         */
        public void viewLogsButton_Click(object sender, EventArgs e)
        {
            if (Program.logAdded)
            {
                lForm.reset();
                Program.logAdded = false;
            }
            titleTextBox.Text = "LOGS";
            activateForm(childForms[LOGS]);
        }

        /**
         * builderButton_Click() function listens to 'builderButton'.
         * This function will then call on the reset() function in BuilderForm to clear
         * the ingredients present within each category's FlowLayoutPanel.
         * The function then calls on activateForm() to activate the builder form.
         */
        public void builderButton_Click(object sender, EventArgs e)
        {
            titleTextBox.Text = "BUILDER";
            bForm.reset();
            activateForm(childForms[BUILDER]);
        }

        /**
         * savedButton_Click() function listens to 'savedButton'.
         * It then will call on 'savedPanelTimer' to start, which in turn will either
         * expand or collapse 'savedPanel'.
         */
        private void savedButton_Click(object sender, EventArgs e)
        {
            savedPanelTimer.Start();
        }

        /**********************************************************************************/
        /*                              ANIMATION FUNCTIONS                               */
        /**********************************************************************************/

        /**
         * savedPanelTimer_Tick() function animates the expansion/collapse of the 'savedPanel' panel.
         */
        private void savedPanelTimer_Tick(object sender, EventArgs e)
        {
            if (isSavedExpanded)
            {
                savedPanel.Height -= closeInt;
                if (savedPanel.Height == savedPanel.MinimumSize.Height)
                {
                    isSavedExpanded = false;
                    //savedPanel.Size = savedPanel.MinimumSize;
                    savedPanelTimer.Stop();
                }
            }
            else
            {
                savedPanel.Height += openInt;
                if (savedPanel.Height == savedPanel.MaximumSize.Height)
                {
                    isSavedExpanded = true;
                    //savedPanel.Size = savedPanel.MaximumSize;
                    savedPanelTimer.Stop();
                }
            }
        }

        /**********************************************************************************/
        /*                                SETTERS/GETTERS                                 */
        /**********************************************************************************/

        /**
         * Getter for 'titleTextBox' text.
         * 
         * @return 'titleTextBox' text.
         */
        public String getTitle()
        {
            return titleTextBox.Text;
        }

        /**
         * Setter for 'titleTextBox' text.
         * 
         * @param t assigned to 'titleTextBox'.Text.
         */
        public void setTitle(String t)
        {
            titleTextBox.Text = t;
        }
    }
}