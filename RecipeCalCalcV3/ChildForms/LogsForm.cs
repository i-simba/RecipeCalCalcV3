using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * TODO ::
 * 1. Dynamically add buttons representing LOG inside 'logPanel'.
 * 2. C# Dictionary to store loaded logs instead of structs(?).
 * 2. Dynamically add 'nutritional info' screen for 'ingredientDataPanel'  | ingredient name/weight/calories.
 * 3. Dynamically add 'nutritional info' screen for 'ingredientMacroPanel' | meal macros.
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class LogsForm : Form
    {
        MainForm main = null;                      // MainFrom object

        private List<Button> logButtons = null;    // List of buttons dynamically created containing logs.

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public LogsForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;

            // Initialize lists.
            logButtons = new List<Button>();

            // Initialize logs
            initLogs();
        }

        /**********************************************************************************/
        /*                                 INTERNAL USE                                   */
        /**********************************************************************************/

        /**
         * TODO:
         */
        private void initLogs()
        {
            StreamReader reader = null;

            String[] fNames = Directory.GetFiles(Program.logsPath);
            foreach (String name in fNames)
            {
                List<String> temp = new List<string>();
                reader = new StreamReader(name);

                //while (!reader.EndOfStream)
                //{
                    // TODO: Parse log data.
                //}
                addLogButton(Path.GetFileNameWithoutExtension(name).ToUpper());
                reader.Close();
            }
        }

        /**
         * TODO:
         */
        private void addLogButton(String n)
        {
            Button temp = new Button();

            temp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(171)))), ((int)(((byte)(167)))));
            temp.FlatAppearance.BorderSize = 1;
            temp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            temp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(143)))), ((int)(((byte)(138)))));
            temp.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            temp.Cursor = System.Windows.Forms.Cursors.Hand;
            temp.ForeColor = System.Drawing.Color.White;
            temp.Location = new System.Drawing.Point(0);
            temp.Size = new System.Drawing.Size(304, 50);
            temp.Dock = DockStyle.Top;
            temp.Text = n;
            //temp.Tag = i;
            //temp.Click += new EventHandler(button_Click);

            logPanel.Controls.Add(temp);
            logButtons.Add(temp);
        }
    }
}
