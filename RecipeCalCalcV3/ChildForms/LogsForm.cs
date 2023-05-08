using RecipeCalCalcV3.Models;
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
using System.Windows.Forms.VisualStyles;

/**
 * TODO ::
 * #. LOGS are currently sorted by creation date - NEED TO IMPLEMENT SORTING BY MONTH.
 * #. Dynamically add 'nutritional info' screen for 'ingredientMacroPanel' | meal macros.
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class LogsForm : Form
    {
        // String array containing months of the year.
        private readonly String[] MONTHS = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN",
                                            "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

        public const int COURSE = 0;                                  // Denotes ingredient's course type index found in a log file.
        public const int NAME = 1;                                    // Denotes ingredient's name index found in a log file.
        public const int WEIGHT = 2;                                  // Denotes ingredient's entered weight found in a log file. 
        public const int CALORIES = 3;                                // Denotes ingredient's calculated calories found in a log file.

        public const int INGREDIENT = 1;                              // Denotes the type of Panel to be created is an Ingredient.
        public const int PORTIONED = 2;                               // Denotes the type of Panel to be created is a Portion.

        private MainForm main = null;                                 // MainFrom object.
        private BuilderForm bForm = null;                             // BuilderForm object.

        private Dictionary<String, FlowLayoutPanel> logFLP = null;    // Dictionary of FlowLayoutPanels keyed by their log name. (date)
        private Dictionary<String, Log> logList = null;               // Dictionary of logs keyed by their log name. (date)
        private String selected = null;                               // String denoting which log is currently selected.
        private List<Button> logButtons = null;                       // List of buttons dynamically created containing logs.
        private List<Panel> entrePanels = null;                       // List of panels dynamically created containing entre ingredients.
        private List<Panel> basePanels = null;                        // List of panels dynamically created containing base ingredeints.
        private List<Panel> snackPanels = null;                       // List of panels dynamically created containing snack ingredients.
        private Label rName = null;                                   // Label containing recipe name.
        private Label eWeight = null;                                 // Label containing entered weight.
        private Label tCal = null;                                    // Label containing total calories.
        

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/


        public LogsForm(MainForm m, Form b)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm' and 'bForm' to passed in Form 'b'.
            main = m;
            bForm = (BuilderForm)b;

            // Initialize Strings.
            selected = string.Empty;

            // Initialize lists.
            logList = new Dictionary<String, Log>();
            logButtons = new List<Button>();
            entrePanels = new List<Panel>();
            basePanels = new List<Panel>();
            snackPanels = new List<Panel>();
            logFLP = new Dictionary<String, FlowLayoutPanel>();

            // Initialize logs
            initLogs();
        }


        /**********************************************************************************/
        /*                                 INTERNAL USE                                   */
        /**********************************************************************************/


        /**
         * initLogs() function is the main data parsing function that gets the files present within the 'Logs' directory.
         * Each file is a log that contain's that day's food ingredients, their totals, and overall totals to include portion data if applicable.
         * The data present within these logs are read into a temporary Log variable 'entry', which is then added to
         * the dictionary 'logList', with the key being the log's (file) name.
         */
        private void initLogs()
        {
            StreamReader reader = null;
            Log entry = null;
            Ingredient ing = null;
            int course = 0;
            int weight = 0;
            double calories = 0.0;

            String[] ingDetails = null;
            String line = null;

            /* NOTE:
             * Currently, files are being read in order of creation (decending) then reversed to get a proper order of each log.
             * Needs to be refactored later to actually sort the log buttons based on month, then year as this implementation
             * can cause problems, i.e., adding a log that meant for a previous date.
             */
            var oNames = Directory.GetFiles(Program.logsPath).OrderByDescending(d => new FileInfo(d).CreationTime);
            String[] fNames = oNames.Reverse().ToArray();

            List<String> logNames = new List<String>();

            foreach (String name in fNames)
            {
                List<String> temp = new List<string>();
                reader = new StreamReader(name);
                entry = new Log();

                line = reader.ReadLine();
                entry.setRecipeName(line);
                entry.setName(Path.GetFileNameWithoutExtension(name).ToUpper());
                logNames.Add(Path.GetFileNameWithoutExtension(name).ToUpper());

                while (line != "TOTALS")
                {
                    ing = new Ingredient();

                    line = reader.ReadLine();
                    if (line.Equals("TOTALS")) break;
                    ingDetails = line.Split(',');

                    // Setting the ingredient's course type
                    if (int.TryParse(ingDetails[COURSE], out course)) 
                    {
                       ing.setCourse(course);
                    }
                    else ing.setCourse(course);

                    // Setting the ingredient's name.
                    ing.setName(ingDetails[NAME]);

                    // Setting the ingredient's entered weight.
                    if (int.TryParse(ingDetails[WEIGHT], out weight))
                    {
                        ing.setEnteredWeight(weight);
                    }
                    else ing.setEnteredWeight(weight);

                    // Setting the ingredient's calculated calories.
                    if (Double.TryParse(ingDetails[CALORIES], out calories))
                    {
                        ing.setCalculatedCal(calories);
                    }
                    else ing.setCalculatedCal(calories);

                    // Compare 'ing' with ingredients inside 'Program.ingredients'.
                    foreach (Ingredient ping in Program.ingredients)
                    {
                        // If ingredients match, assign correct Tip Name.
                        if (ping.getName().Equals(ing.getName()))
                            ing.setTipName(ping.getTipName());
                    }

                    // Adding ingredient to log.
                    entry.addIngredient(ing);
                }

                // Setting logged recipe's portion value.
                entry.setIsPortioned(Boolean.Parse(reader.ReadLine()));

                // Setting logged recipe's total values.
                int i = 0;
                while (!reader.EndOfStream)
                {
                    double tempDoubleA = 0.0;
                    double tempDoubleB = 0.0;

                    line = reader.ReadLine();
                    ingDetails = line.Split(',');
                    Double.TryParse(ingDetails[0], out tempDoubleA);
                    Double.TryParse(ingDetails[1], out tempDoubleB);

                    switch (i)
                    {
                        case 0: // Setting logged recipe's entre weight and calories.
                            entry.setentreIngWeight(tempDoubleA);
                            entry.setEntreCalories(tempDoubleB);
                            break;
                        case 1: // Setting logged recipe's base weight and calories.
                            entry.setBaseIngWeight(tempDoubleA);
                            entry.setBaseCalories(tempDoubleB);
                            break;
                        case 2: // Setting logged recipe's snack weight and calories.
                            entry.setSnackIngWeight(tempDoubleA);
                            entry.setSnackCalories(tempDoubleB);
                            break;
                        case 3: // Setting logged recipe's total weight and calories
                            entry.setTotalIngWeight(tempDoubleA);
                            entry.setTotalCalories(tempDoubleB);
                            break;
                        case 4: // Setting logged recipe's cooked weight and portion weight.
                            entry.setCookedWeight(tempDoubleA);
                            entry.setPortionWeight(tempDoubleB);
                            break;
                        case 5: // Setting logged recipe's portion calories and portioned plus all calories.
                            entry.setPortionCalories(tempDoubleA);
                            entry.setPortionAllCalories(tempDoubleB);
                            break;
                        default:
                            break;
                    }
                    i++;
                }

                logList.Add(Path.GetFileNameWithoutExtension(name).ToUpper(), entry);    // Add entry to logList dictionary with the logged recipe name (date) being the key.
                addLogButton(Path.GetFileNameWithoutExtension(name).ToUpper());          // Add button representing entry for the user to interact with.
                reader.Close();
            }
            
            foreach (Button button in logButtons)
            {
                logPanel.Controls.Add(button);
            }

            foreach (String str in logNames)
            {
                logFLP.Add(str, buildFlowPanel(str));
            }
        }

        /**
         * initCalorieBreakdown() function creates the initializes controls displaying log data to include:
         * Separator panels, Calorie Breakdown label, Recipe name, Total calories, etc.
         * This function takes in a single FlowLayoutPanel 'flp' in which created controls are added to.
         * 
         * @param flp FlowLayoutPanel in which created controls are added to.
         */
        private void initCalorieBreakdown(FlowLayoutPanel flp)
        {
            // Label containing "CALORIE BREAKDOWN".
            Label cB = new Label();
            cB.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cB.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            cB.Location = new System.Drawing.Point(3, 0);
            cB.Name = "label2";
            cB.Size = new System.Drawing.Size(397, 50);
            cB.TabIndex = 0;
            cB.Text = "CALORIE BREAKDOWN";
            cB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            flp.Controls.Add(cB);

            // Separator panel.
            Panel sep = new Panel();
            sep = initSeparatorPanel(sep, 5, 3);
            flp.Controls.Add(sep);

            // Panel containing Labels representing recipe name and total calories.
            Panel container = new Panel();
            container.Location = new System.Drawing.Point(20, 64);
            container.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            container.Name = "container";
            container.Size = new System.Drawing.Size(355, 25);
            container.TabIndex = 3;

            // Label containing recipe name.
            Label recipeName = new Label();
            recipeName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            recipeName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            recipeName.Location = new System.Drawing.Point(0, 0);
            recipeName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            recipeName.Name = "recipeName";
            recipeName.Size = new System.Drawing.Size(155, 25);
            recipeName.TabIndex = 2;
            recipeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            container.Controls.Add(recipeName);

            // Label containing "TOTAL CAL:".
            Label totalEnteredWeight = new Label();
            totalEnteredWeight.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalEnteredWeight.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            totalEnteredWeight.Location = new System.Drawing.Point(155, 0);
            totalEnteredWeight.Margin = new System.Windows.Forms.Padding(0);
            totalEnteredWeight.Size = new System.Drawing.Size(100, 25);
            totalEnteredWeight.TabIndex = 4;
            totalEnteredWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            totalEnteredWeight.Name = "totalCalPrompt";
            container.Controls.Add(totalEnteredWeight);

            // Label containing total calories.
            Label totalCalTB = new Label();
            totalCalTB.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalCalTB.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            totalCalTB.Location = new System.Drawing.Point(255, 0);
            totalCalTB.Margin = new System.Windows.Forms.Padding(0);
            totalCalTB.Name = "totalCalTB";
            totalCalTB.Size = new System.Drawing.Size(100, 25);
            totalCalTB.TabIndex = 3;
            totalCalTB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            container.Controls.Add(totalCalTB);
            flp.Controls.Add(container);

            // Assigning created recipe name and total calories label to global variables.
            rName = recipeName;
            eWeight = totalEnteredWeight;
            tCal = totalCalTB;
        }

        /**
         * addLogButton() function builds a button representing a logged recipe and is interactable to select that given log.
         * This function takes in a single String variable 'n' representing the log's name with this format "##AAA##", i.e., "01JAN23".
         * The function then creates a temporary button variable and setting its properties to match the given aesthetic, and assigning
         * the button's Text property to 'n'.
         * The temporary button is then added to the list 'logButtons'.
         * 
         * @param n String variable representing log's name and assigned to button's Text.
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
            temp.Click += new EventHandler(button_Click);

            logButtons.Add(temp);
        }

        /**
         * initSeparatorPanel() function takes in a Panel and two integer arguments.
         * The Panel is initialized and built by this function, which also uses the integer 'height' to set panel's height
         * and the integer 'topPad' is used to set panel's top padding.
         * 
         * @param panel Panel variable to be built.
         * @param height integer used to set panel's height.
         * @param topPad integer used to set panel's top padding.
         * @return panel initialized and built panel.
         */
        private Panel initSeparatorPanel(Panel panel, int height, int topPad)
        {
            panel.BackColor = System.Drawing.SystemColors.ScrollBar;
            panel.Location = new System.Drawing.Point(3, 0);
            panel.Margin = new System.Windows.Forms.Padding(20, topPad, 3, 3);
            panel.Size = new System.Drawing.Size(355, height);

            return panel;
        }

        /**
         * initIngredientTotals() function takes in four data arguments which are used to build a panel containing those data.
         * This function also takes in an integer 'code' which is used to chose between different build properties, i.e., Text, Size, etc.
         * Despite being named 'initIngredientTotals', it doesn't only build Ingredient totals, but also other totals such as Portion totals.
         * 
         * @param container Panel variable to be built.
         * @param total String representing type of total to be built.
         * @param weight String representing the entered/total weight of a given Ingredient/Total.
         * @param idx integer denoting which type of Panel to build.
         * @return container Panel initialized and built panel.
         */
        private Panel initIngredientTotals(Panel container, String total, String weight, String calories, int idx)
        {
            container.Location = new System.Drawing.Point(0, 0);

            Label totalName = new Label();
            totalName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            totalName.Location = new System.Drawing.Point(0, 0);
            totalName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            totalName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            totalName.Text = total;

            Label eWeight = new Label();
            eWeight.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            eWeight.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            eWeight.Location = new System.Drawing.Point(205, 0);
            eWeight.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            eWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            Label calculatedCal = new Label();
            calculatedCal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            calculatedCal.Location = new System.Drawing.Point(255, 0);
            calculatedCal.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            calculatedCal.Name = "ingCalories";
            calculatedCal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            calculatedCal.Text = calories + " cal";

            if (idx % 2 != 0)
            {
                container.Size = new System.Drawing.Size(355, 20);
                container.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);

                totalName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                totalName.Size = new System.Drawing.Size(205, 20);

                eWeight.Size = new System.Drawing.Size(50, 20);
                eWeight.Text = weight + "g";

                calculatedCal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                calculatedCal.Size = new System.Drawing.Size(90, 20);
            }
            else if (idx % 2 == 0)
            {
                container.Size = new System.Drawing.Size(355, 35);
                container.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
                container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(233)))), ((int)(((byte)(228)))));

                totalName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                totalName.Size = new System.Drawing.Size(205, 35);

                eWeight.Size = new System.Drawing.Size(50, 35);
                eWeight.Text = string.Empty;

                calculatedCal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                calculatedCal.Size = new System.Drawing.Size(90, 35);
            }

            container.Controls.Add(totalName);
            container.Controls.Add(eWeight);
            container.Controls.Add(calculatedCal);

            return container;
        }


        /**********************************************************************************/
        /*                                 EXTERNAL USE                                   */
        /**********************************************************************************/


        /**
         * reset() function resets 'logPanel' by clearing it's Controls.
         * Panels containing ingredient data, i.e., entre/base/snack, are also cleared and 'initLogs' is called again to rebuild logs.
         * This function is mainly used within MainForm, to reset and rebuild 'logPanel' if a new log has just been created.
         */
        public void reset()
        {
            logPanel.Controls.Clear();
            logList.Clear();
            logButtons.Clear();
            entrePanels.Clear();
            basePanels.Clear();
            snackPanels.Clear();
            logFLP.Clear();
            initLogs();
        }

        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/

        /**
         * button_Click() function listens for a button click event that happens to a given log present within the 'logPanel'.
         * The name of the log button is then used to retrieve the correct FlowLayoutPanel to be displayed.
         * The selected log button will be highlighted by changing the button color to a darker shade,
         * while the other buttons are reset back to the original color.
         */
        public void button_Click(object sender, EventArgs e)
        {
            ingredientDataPanel.Controls.Clear();
            foreach (Button button in  logButtons)
            {
                if (sender == button)
                {
                    // Set selected button's color to a darker shade to denote it is selected.
                    button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(113)))), ((int)(((byte)(108)))));
                    FlowLayoutPanel temp = null;
                    logFLP.TryGetValue(button.Text, out temp);
                    ingredientDataPanel.Controls.Add(temp);

                    selected = button.Text;
                }
                else
                {
                    // Reset buttons BackColor back to original.
                    button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(143)))), ((int)(((byte)(138)))));
                }
            }
        }

        /**
         * buildFlowPanel() function is used to create a FlowLayoutPanel that contains the calorie breakdown for a given log.
         * 'logName' is the key used to find the Log within the dictionary 'logList' and the data present within that log is pased and added to 
         * Controls within a created FlowLayoutPanel which will be used to display inside the ingredientDataPanel.
         * 
         * @param logName is used to determine which log is to be built.
         * @return FlowLayoutPanel.
         */
        private FlowLayoutPanel buildFlowPanel(String logName)
        {
            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Size = ingredientDataPanel.Size;

            // Reset.
            ingredientDataPanel.Controls.Clear();
            ingredientMacroPanel.Controls.Clear();
            initCalorieBreakdown(flp);
            entrePanels.Clear();
            basePanels.Clear();
            snackPanels.Clear();

            // Set 'isClicked' values of all logs to 'false'.
            foreach (KeyValuePair<String, Log> log in logList)
            {
                Log foo = log.Value;
                foo.setIsClicked(false);
            }

            // Set the selected log's 'isClicked' value to 'true'.
            Log temp = null;
            if (logList.TryGetValue(logName, out temp))
            {
                temp.setIsClicked(true);
            }

            rName.Text = temp.getRecipeName();
            eWeight.Text = temp.getTotalIngWeight().ToString() + "g";
            tCal.Text = temp.getTotalCalories().ToString("F2") + " cal";

            // Building ingredient panels and adding them to corresponding list.
            foreach (Ingredient ing in temp.getIngredientList())
            {
                Panel container = new Panel();
                container.Margin = new System.Windows.Forms.Padding(40, 3, 3, 3);
                container.Name = "container";
                container.Size = new System.Drawing.Size(335, 20);

                Label ingName = new Label();
                ingName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ingName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                ingName.Location = new System.Drawing.Point(0, 0);
                ingName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                ingName.Name = "ingName";
                ingName.Size = new System.Drawing.Size(185, 20);
                ingName.Text = ing.getTipName();
                ingName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                Label ingWeight = new Label();
                ingWeight.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ingWeight.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                ingWeight.Location = new System.Drawing.Point(185, 0);
                ingWeight.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                ingWeight.Name = "ingWeight";
                ingWeight.Size = new System.Drawing.Size(50, 20);
                ingWeight.Text = ing.getEnteredWeight().ToString() + "g";
                ingWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                Label ingCalories = new Label();
                ingCalories.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ingCalories.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                ingCalories.Location = new System.Drawing.Point(235, 0);
                ingCalories.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
                ingCalories.Name = "ingCalories";
                ingCalories.Size = new System.Drawing.Size(90, 20);
                ingCalories.Text = ing.getCalculatedCal().ToString("F2") + " cal";
                ingCalories.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                container.Controls.Add(ingName);
                container.Controls.Add(ingWeight);
                container.Controls.Add(ingCalories);

                switch (ing.getCourse())
                {
                    case 1:
                        entrePanels.Add(container);
                        break;
                    case 2:
                        basePanels.Add(container);
                        break;
                    case 3:
                        snackPanels.Add(container);
                        break;
                    default:
                        break;
                }
            }

            // Initialize labels for entre/base/snack.
            Label[] label = new Label[5];
            for (int i = 0; i < label.Count(); i++)
            {
                label[i] = new Label();
                label[i].Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label[i].ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                label[i].BackColor = System.Drawing.SystemColors.ControlLight;
                label[i].Location = new System.Drawing.Point(0, 0);
                label[i].Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
                label[i].Size = new System.Drawing.Size(355, 25);
                label[i].TextAlign = System.Drawing.ContentAlignment.BottomLeft;

                switch (i)
                {
                    case 0:
                        label[i].Text = "Entre";
                        break;
                    case 1:
                        label[i].Text = "Base";
                        break;
                    case 2:
                        label[i].Text = "Snack";
                        break;
                    case 3:
                        label[i].Text = "Totals";
                        break;
                    case 4:
                        label[i].Text = "Portion";
                        break;
                    default:
                        break;
                }
            }

            Panel miniSep = new Panel();
            miniSep = initSeparatorPanel(miniSep, 2, 3);

            Panel miniSep2 = new Panel();
            miniSep2 = initSeparatorPanel(miniSep2, 2, 3);

            Panel miniSep3 = new Panel();
            miniSep3 = initSeparatorPanel(miniSep3, 2, 3);

            // Add entre ingredients to be displayed.
            flp.Controls.Add(label[0]);
            flp.Controls.Add(miniSep);
            foreach (Panel panel in entrePanels)
            {
                flp.Controls.Add(panel);
            }

            // Add base ingredients to be displayed.
            flp.Controls.Add(label[1]);
            flp.Controls.Add(miniSep2);
            foreach (Panel panel in basePanels)
            {
                flp.Controls.Add(panel);
            }

            // Add snack ingredients to be displayed.
            flp.Controls.Add(label[2]);
            flp.Controls.Add(miniSep3);
            foreach (Panel panel in snackPanels)
            {
                flp.Controls.Add(panel);
            }

            // Panel separating Totals.
            Panel totSep = new Panel();
            totSep = initSeparatorPanel(totSep, 5, 20);
            flp.Controls.Add(totSep);

            // Initializing and displaying totals.
            flp.Controls.Add(label[3]);
            Panel totalEntre = new Panel();
            totalEntre = initIngredientTotals(totalEntre, "Entre Totals", temp.getentreIngWeight().ToString(), temp.getEntreCalories().ToString("F2"), INGREDIENT);
            flp.Controls.Add(totalEntre);

            Panel totalBase = new Panel();
            totalBase = initIngredientTotals(totalBase, "Base Totals", temp.getBaseIngWeight().ToString(), temp.getBaseCalories().ToString("F2"), INGREDIENT);
            flp.Controls.Add(totalBase);

            Panel totalSnack = new Panel();
            totalSnack = initIngredientTotals(totalSnack, "Snack Totals", temp.getSnackIngWeight().ToString(), temp.getSnackCalories().ToString("F2"), INGREDIENT);
            flp.Controls.Add(totalSnack);

            // Display logged recipe portion if applicable.
            if (temp.getIsPortioned())
            {
                Panel porSep = new Panel();
                porSep = initSeparatorPanel(porSep, 5, 20);

                Panel cookedWeight = new Panel();
                cookedWeight = initIngredientTotals(cookedWeight, "Cooked Weight", temp.getCookedWeight().ToString(), temp.getTotalCalories().ToString("F2"), INGREDIENT);
                Panel portionWeight = new Panel();
                portionWeight = initIngredientTotals(portionWeight, "Portion Weight", temp.getPortionWeight().ToString(), temp.getPortionCalories().ToString("F2"), INGREDIENT);
                Panel portionedPanel = new Panel();
                portionedPanel = initIngredientTotals(portionedPanel, "Portioned Calories", "", temp.getPortionAllCalories().ToString("F2"), PORTIONED);

                flp.Controls.Add(porSep);
                flp.Controls.Add(label[4]);
                flp.Controls.Add(cookedWeight);
                flp.Controls.Add(portionWeight);
                flp.Controls.Add(portionedPanel);
            }
            else
            {
                Panel singlePortionPanel = new Panel();
                singlePortionPanel = initIngredientTotals(singlePortionPanel, "Single Portion Calories", "", temp.getTotalCalories().ToString("F2"), PORTIONED);
                flp.Controls.Add(singlePortionPanel);
            }

            return flp;
        }

        /**
         * loadButton_Click() function listens to the loadButton.
         * Upon a click, the selected log will be parsed for ingredient data, and if applicable, cooked weight data.
         * Each present ingredient will then be used to 'push' buttons within the BuilderForm that corresponds to that ingredient.
         * The entered weight of each ingredient will then be displayed on the BuilderForm's ingredient panel's TextBox.
         * If the selected log is portioned, the cooked weight will also be displayed.
         * This function also 'pushes' the builderButton in MainForm to hide LogsForm and display BuilderForm with the mentioned data above.
         */
        private void loadButton_Click(object sender, EventArgs e)
        {
            // Error trap - No log selected.
            if (selected.Equals(string.Empty))
            {
                MessageBox.Show("No selected log!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Retrieve the log from the dictionary and assign it to 'log'.
            List<Button> tempBL = bForm.getIngredientButtons();
            Log log = new Log();
            logList.TryGetValue(selected, out log);

            // Click 'builderButton' in main.
            main.builderButton_Click(sender, e);

            // Click ingredient buttons in BuilderForm.
            foreach (Ingredient ing in log.getIngredientList())
            {
                foreach (Button btn in tempBL)
                {
                    if (btn.Name.Equals(ing.getName()))
                    {
                        btn.Tag = ing.getEnteredWeight();
                        bForm.button_Click(btn, e);
                    }
                }
            }

            // Set cooked weight in BuilderForm if log is portioned.
            if (log.getIsPortioned())
            {
                bForm.setCookedWeight((int)log.getCookedWeight());
            }

            // Set main title's Text to recipe name.
            main.setTitle(log.getRecipeName());
        }


        /**********************************************************************************/
        /*                                SETTERS/GETTERS                                 */
        /**********************************************************************************/


        /**
         * Getter for 'logFLP'.
         * 
         * @return 'logFLP'.
         */
        public Dictionary<String, FlowLayoutPanel> getFlowPanelDict()
        {
            return this.logFLP;
        }

        /**
         * Setter for 'logFLP'.
         * 
         * @param flp assigned to 'logFLP'.
         */
        public void setFlowPanelDict(Dictionary<String, FlowLayoutPanel> flp)
        {
            this.logFLP = flp;
        }

        /**
         * Get FlowLayoutPanel at a given String key 'key'.
         * 
         * @param key String used as a key to get a given value within 'logFLP'.
         * @return FlowLayoutPanel with matched key.
         */
        public FlowLayoutPanel getFlowPanelAt(String key)
        {
            FlowLayoutPanel temp = null;
            logFLP.TryGetValue(key, out temp);
            return temp;
        }

        /**
         * Getter for 'selected'.
         * 
         * @return 'selected'.
         */
        public String getSelectedLog()
        {
            return this.selected;
        }

        /**
         * Setter for 'selected'.
         * 
         * @param log assigned to 'selected'.
         */
        public void setSelectedLog(String log)
        {
            this.selected = log;
        }

        /**
         * Getter for 'logButtons'.
         * 
         * @return 'logButtons'.
         */
        public List<Button> getLogButtons()
        {
            return this.logButtons;
        }

        /**
         * Setter for 'logButtons'.
         * 
         * @param logB assigned to 'logButtons'.
         */
        public void setLogButtons(List<Button> logB)
        {
            this.logButtons = logB;
        }

        /**
         * Get Button indexed at 'i'.
         * 
         * @param i used as an index for an element in 'logButtons'.
         * @return Button indexed at 'i'.
         */
        public Button getLogButtonAt(int i)
        {
            return logButtons[i];
        }

        /**
         * Getter for 'entrePanels'.
         * 
         * @return 'entrePanels'.
         */
        public List<Panel> getEntrePanels()
        {
            return this.entrePanels;
        }

        /**
         * Setter for 'entrePanels'.
         * 
         * @param entreP assigned to 'entrePanels'.
         */
        public void setEntrePanels(List<Panel> entreP)
        {
            this.entrePanels = entreP;
        }

        /**
         * Get Entre Panel indexed at 'i'.
         * 
         * @param i used as an index for an element in 'entrePanels'.
         * @return Panel indexed at 'i'.
         */
        public Panel getEntrePanelAt(int i)
        {
            return entrePanels[i];
        }

        /**
         * Getter for 'basePanels'.
         * 
         * @return 'basePanels'.
         */
        public List<Panel> getBasePanels()
        {
            return this.basePanels;
        }

        /**
         * Setter for 'basePanels'.
         * 
         * @param baseP assigned to 'basePanels'.
         */
        public void setBasePanels(List<Panel> baseP)
        {
            this.basePanels = baseP;
        }

        /**
         * Get Base Panel indexed at 'i'.
         * 
         * @param i used as an index for an element in 'basePanels'.
         * @return Panel indexed at 'i'.
         */
        public Panel getBasePanelAt(int i)
        {
            return basePanels[i];
        }

        /**
         * Getter for 'snackPanels'.
         * 
         * @return 'snackPanels'.
         */
        public List<Panel> getSnackPanels()
        {
            return this.snackPanels;
        }

        /**
         * Setter for 'snackPanels'.
         * 
         * @param snackP assigned to 'snackPanels'.
         */
        public void setSnackPanels(List<Panel> snackP)
        {
            this.snackPanels = snackP;
        }

        /**
         * Get Snack Panel indexed at 'i'.
         * 
         * @param i used as an index for an element in 'snackPanels'.
         * @return Panel indexed at 'i'.
         */
        public Panel getSnackPanelAt(int i)
        {
            return snackPanels[i];
        }

        /**
         * Getter for 'rName'.
         * 
         * @return 'rName'.
         */
        public Label getRecipeName()
        {
            return rName;
        }

        /**
         * Setter for 'rName'.
         * 
         * @param recipeN assigned to 'rName'.
         */
        public void setRecipeName(Label recipeN)
        {
            this.rName = recipeN;
        }

        /**
         * Getter for 'eWeight'.
         * 
         * @return 'eWeight'.
         */
        public Label getEnteredWeight()
        {
            return eWeight;
        }

        /**
         * Setter for 'eWeight'.
         * 
         * @param enteredW assigned to 'eWeight'.
         */
        public void setEnteredWeight(Label enteredW)
        {
            this.eWeight = enteredW;
        }

        /**
         * Getter for 'tCal'.
         * 
         * @return 'tCal'.
         */
        public Label getTotalCal()
        {
            return tCal;
        }

        /**
         * Setter for 'tCal'.
         * 
         * @param totalC assigned to 'tCal'.
         */
        public void setTotalCal(Label totalC)
        {
            this.tCal = totalC;
        }
    }
}