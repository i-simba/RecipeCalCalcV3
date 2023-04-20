using RecipeCalCalcV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

/*
 * TODO ::
 * 0. Pot/Pan object with weight(?) used to deduct from cooked weight.
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class BuilderForm : Form
    {
        MainForm main = null;                             // MainForm object. 
        
        private List<Button> ingredientButtons = null;    // List of buttons for each ingredient.
        private List<Panel> ingPanels = null;             // List of Panels which displays all added ingredients.
        private List<Panel> entrePanels = null;           // List of Panels which displays added entre ingredients.
        private List<Panel> basePanels = null;            // List of Panels which displays added base ingredients.
        private List<Panel> snackPanels = null;           // List of Panels which displays added snack ingredients.

        private int entreRW = 0;                          // Total of all entered weight for entre ingredients.
        private int baseRW = 0;                           // Total of all entered weight for base ingredients.
        private int snackRW = 0;                          // Total of all entered weight for snack ingredients.
        private int totalRW = 0;                          // Total of all combined entered weight.

        private double entreCalculatedCal = 0.0;          // Total of all calculated calories for entre ingredients.
        private double baseCalculatedCal = 0.0;           // Total of all calculated calories for base ingredients.
        private double snackCalculatedCal = 0.0;          // Total of all calculated calories for snack ingredients.
        private double totalCalculatedCal = 0.0;          // Total of all combined calculated calories.

        private int cookedWeight = 0;                     // The entered cooked weight of entre ingredients.
        private int portionWeight = 0;                    // The portion weight as it relates to cooked weight.
        private double portionCalculatedCal = 0.0;        // The calculated calories of the portion.

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public BuilderForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;

            // Initialize lists.
            ingredientButtons = new List<Button>();
            ingPanels = new List<Panel>();
            entrePanels = new List<Panel>();
            basePanels = new List<Panel>();
            snackPanels = new List<Panel>();
            initButtons();
        }

        /**
         * reset() function disposes of all added ingredient panels, and all totals values will be reset to zero and their
         * corresponding TextBox set to be empty strings.
         */
        public void reset()
        {
            // Dispose of any entered ingredient panel.
            foreach (Panel panel in ingPanels)
            {
                panel.Dispose();
            }

            // Clear lists of all items.
            ingPanels.Clear();
            entrePanels.Clear();
            basePanels.Clear();
            snackPanels.Clear();

            // Reset the text displayed for each totals TextBox.
            foreach (Control control in totalsPanel.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in  control.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ctrl.Text = string.Empty;
                        }
                    }
                }
            }
            

            // Reset tags on each ingredient button.
            foreach (Button button in ingredientButtons)
            {
                button.Tag = "Not";
            }

            cookedWeightTB.Text = string.Empty;
            portionWeightTB.Text = string.Empty;

            // Resets the values of each totals.
            resetValues();
            
            // Resets the entered weight and calculated calorie variables for each ingredient.
            Program.resetListVals();
        }

        /**
         * resetValues() function resets only the total values for each calculation.
         * Mainly used within the calculateButton_Click() function to accurately display totals.
         * Pulled from reset() function.
         */
        private void resetValues()
        {
            // Reset all totals variable, weight and calories, to zero.
            entreRW = 0;
            baseRW = 0;
            snackRW = 0;
            totalRW = 0;
            entreCalculatedCal = 0.0;
            baseCalculatedCal = 0.0;
            snackCalculatedCal = 0.0;
            totalCalculatedCal = 0.0;
            cookedWeight = 0;
            portionWeight = 0;
            portionCalculatedCal = 0.0;
        }

        /**
         * initButtons() function initializes ingredient buttons by dynamically creating
         * buttons and adding each to a panel based on the ingredient type.
         * Ingredient images will also be displayed on the button.
         */
        private void initButtons()
        {
            foreach (Ingredient ing in Program.ingredients)
            {
                // Temporary Button object used to add to list.
                Button temp = new Button();
                temp.Name = ing.getName();
                temp.Tag = "Not";
                temp.Image = ing.getImage();
                temp.Width = 64;
                temp.Height = 64;
                temp.Click += new EventHandler(button_Click);

                // ToolTip used to show user the name of the ingredient.
                ToolTip tip = new ToolTip();
                tip.SetToolTip(temp, ing.getTipName());

                // Adding ingredient to the correct ingredient panel.
                if (ing.getType().Equals("protein"))
                {
                    proteinPanel.Controls.Add(temp);
                }
                else if (ing.getType().Equals("veggie"))
                {
                    veggiePanel.Controls.Add(temp);
                }
                else if (ing.getType().Equals("liquid"))
                {
                    liquidPanel.Controls.Add(temp);
                }
                else if (ing.getType().Equals("misc"))
                {
                    miscPanel.Controls.Add(temp);
                }
                ingredientButtons.Add(temp);
            }
        }

        /**
         * panelClick() function listens to a click even that happend for any added ingredient panel
         * for any course. The clicked panel will be disposed, denoting the removal of the ingredient.
         */
        private void panelClick(object sender, EventArgs e)
        {
            resetValues();
            Program.resetListVals();
            foreach (Panel panel in ingPanels)
            {
                if (sender == panel)
                {
                    foreach (Button button in ingredientButtons)
                    {
                        if (panel.Name.Equals(button.Name))
                        {
                            button.Tag = "Not";
                        }
                    }
                    foreach (Control ctrl in panel.Controls)
                    {
                        panel.Controls.Remove(ctrl);
                    }
                    if (entrePanels.Contains(panel))
                        entrePanels.Remove(panel);
                    if (basePanels.Contains(panel))
                        basePanels.Remove(panel);
                    if (snackPanels.Contains(panel))
                        snackPanels.Remove(panel);
                    ingPanels.Remove(panel);
                    panel.Dispose();
                    return;
                }
            }
        }

        /**
         * inputBox() function dynamically creates a new Form that prompts the user to enter a
         * name for the recipe.
         * If the string entered is either empty or only contains white spaces, this function will
         * display an error and recursively prompt the user again until a valid input is entered.
         */
        public static DialogResult inputBox(String promptText, String text, ref String value, int i)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            label.Text = promptText;
            // Conditional statements based on the caller 'i'.
            if (i == 2)
            {
                textBox.Text = text;
            }

            buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            buttonOk.BackColor = Color.FromArgb(226, 221, 217);
            buttonCancel.BackColor = Color.FromArgb(216, 200, 185);
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(15, 20, 200, 20);
            textBox.SetBounds(110, 20, 170, 20);
            buttonOk.SetBounds(205, 60, 75, 30);
            buttonCancel.SetBounds(110, 60, 75, 30);

            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            form.ClientSize = new Size(300, 100);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.BackColor = Color.FromArgb(168, 163, 157);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel});
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult result = form.ShowDialog();

            // Validate recipe name input - Recursively show Dialog if empty.
            if (result == DialogResult.OK && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (i == 1)
                {
                    MessageBox.Show("Recipe name cannot be empty!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return inputBox(promptText, text, ref value, i);
                }
                if (i == 2)
                {
                    MessageBox.Show("Log name cannot be empty!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return inputBox(promptText, text, ref value, i);
                }
            }

            value = textBox.Text;
            return result;
        }

        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/

        /**
         * button_Click() function is the defacto button listener for the dynamically created ingredient buttons.
         * This function dynamically builds the Panel that will contain the following:
         * Panel 'container' that will contain all other child controls listed below.
         * PictureBox 'pic' that will contain the ingredient's picture.
         * TextBox 'text' that will contain a field in which the user can enter the ingredient's weight in grams.
         * Panel 'calPanel' that will contain the child form 'calLabel' below.
         * Label 'calLabel' that will contain the numeric calorie value of the ingredient based on its entered weight.
         */
        public void button_Click(object sender, EventArgs e)
        {
            // Controls used to build the 'container' for the ingredient.
            Panel container = new Panel();
            PictureBox pic = new PictureBox();
            TextBox text = new TextBox();
            Panel calPanel = new Panel();
            Label calLabel = new Label();

            // ToolTip object bound to the panel 'container' to display "Delete".
            ToolTip tip = new ToolTip();
            tip.SetToolTip(container, "Delete");

            // Set Panel 'container' properties.
            container.BackColor = Color.FromArgb(168, 163, 157);
            Padding mainPadding = new Padding(15, 15, 0, 0);
            Padding subPadding = new Padding(5, 5, 0, 0);
            container.Click += new EventHandler(panelClick);
            container.Width = 375;
            container.Height = 70;

            // Set PictureBox 'pic' properties.
            pic.BackColor = Color.FromArgb(134, 130, 126);
            pic.SetBounds(3, 3, 64, 64);
            pic.Width = 64;
            pic.Height = 64;

            // Set TextBox 'text' properties.
            text.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            text.Location = new System.Drawing.Point(73, 13);
            text.Size = new System.Drawing.Size(150, 44);

            // Set Panel 'calPanel' properties.
            calPanel.Width = 115;
            calPanel.Height = 44;
            calPanel.BackColor = Color.FromArgb(134, 130, 126);
            calPanel.Location = new System.Drawing.Point(230, 13);

            // Set Label 'calLabel' properties.
            calLabel.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            calLabel.ForeColor = System.Drawing.SystemColors.Control;
            calLabel.Location = new System.Drawing.Point(3, 9);
            calLabel.Size = new System.Drawing.Size(85, 27);

            int i = 0;
            foreach (Button button in ingredientButtons)
            {
                if (sender == button && button.Tag.Equals("Not"))
                {
                    // Set ingredient's name and image.
                    container.Name = button.Name;
                    button.Tag = "Added";
                    pic.Image = button.Image;
                    text.Name = button.Name + "TB";

                    // Build container to add to display by adding child controls.
                    calPanel.Controls.Add(calLabel);
                    container.Controls.Add(pic);
                    container.Controls.Add(text);
                    container.Controls.Add(calPanel);

                    // Add container to correct panel and list to be displayed.
                    switch (Program.ingredients[i].getCourse())
                    {
                        case 1:
                            container.Padding = new Padding(15, 15, 0, 0);
                            entreIngredientPanel.Controls.Add(container);
                            entrePanels.Add(container);
                            break;
                        case 2: 
                            baseIngredientPanel.Controls.Add(container);
                            basePanels.Add(container);
                            break;
                        case 3: 
                            snacksIngredientPanel.Controls.Add(container);
                            snackPanels.Add(container);
                            break;
                        default: Console.WriteLine("\nERROR! BuilderForm.cs -> button_Click() -> switch!");
                            break;
                    }
                    ingPanels.Add(container);

                    i++;
                }
                else i++;
            }
        }        

        /**
         * resetButton_Click() function listens to the resetButton and executes reset().
         */
        private void resetButton_Click(object sender, EventArgs e)
        {
            reset();
        }
        
        /**
         * calculateButton_Click function listens to the calculateButton.
         * This function takes in user entered values for each ingredient weight (grams)
         * The total weight is calculated, as well as the total for each course.
         * Calories are calculated for each ingredient, totaled up for each course, and 
         * all courses are summed up to get the total calories for the meal.
         * All information mentioned above will also be displayed.
         */
        private void calculateButton_Click(object sender, EventArgs e)
        {
            // Initially reset all total values to zero.
            resetValues();

            // Calculate and add each entre weight to find the sum.
            foreach (Panel panel in entrePanels)
            {
                double temp = 0.0;
                foreach (Control con in panel.Controls)
                {
                    if (con is TextBox)
                    {
                        // Add entered weight to the sum of total entre weight.
                        if (con.Text != string.Empty)
                        {
                            entreRW += Convert.ToInt32(con.Text);
                        }
                        else entreRW += 0;

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            // Match panel that corresponds to the ingredient.
                            if (panel.Name.Equals(ing.getName()))
                            {
                                if (con.Text != string.Empty)
                                {
                                    ing.setEnteredWeight(Convert.ToInt32(con.Text));                              // Add entered weight to ingredient.
                                }
                                else ing.setEnteredWeight(0);
                                temp = (ing.getEnteredWeight() * ing.getCalories()) / (double)ing.getWeight();    // Calorie calculation.
                                ing.setCalculatedCal(temp);                                                       // Add calculated calorie to ingredient.
                            }
                        }
                    }
                    if (con is Panel)
                    {
                        foreach (Control lab in con.Controls)
                        {
                            if (lab is Label)
                            {
                                // Iterate through each Ingredient object within the Ingredient List in Program.
                                foreach (Ingredient ing in Program.ingredients)
                                {
                                    if (panel.Name.Equals(ing.getName()))
                                    {
                                        lab.Text = ing.getCalculatedCal().ToString();    // Display calculated calories.
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Calculate and add each base weight to find the sum.
            foreach (Panel panel in basePanels)
            {
                double temp = 0.0;
                foreach (Control con in panel.Controls)
                {
                    if (con is TextBox)
                    {
                        // Add entered weight to the sum of total entre weight.
                        if (con.Text != string.Empty)
                        {
                            baseRW += Convert.ToInt32(con.Text);
                        }
                        else baseRW += 0;

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            // Match panel that corresponds to the ingredient.
                            if (panel.Name.Equals(ing.getName()))
                            {
                                if (con.Text != string.Empty)
                                {
                                    ing.setEnteredWeight(Convert.ToInt32(con.Text));                              // Add entered weight to ingredient.
                                }
                                else ing.setEnteredWeight(0);
                                temp = (ing.getEnteredWeight() * ing.getCalories()) / (double)ing.getWeight();    // Calorie calculation.
                                ing.setCalculatedCal(temp);                                                       // Add calculated calorie to ingredient.
                            }
                        }
                    }
                    if (con is Panel)
                    {
                        foreach (Control lab in con.Controls)
                        {
                            if (lab is Label)
                            {
                                // Iterate through each Ingredient object within the Ingredient List in Program.
                                foreach (Ingredient ing in Program.ingredients)
                                {
                                    if (panel.Name.Equals(ing.getName()))
                                    {
                                        lab.Text = ing.getCalculatedCal().ToString();    // Display calculated calories.
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Calculate and add each snack weight to find the sum.
            foreach (Panel panel in snackPanels)
            {
                double temp = 0.0;
                foreach (Control con in panel.Controls)
                {
                    if (con is TextBox)
                    {
                        // Add entered weight to the sum of total entre weight.
                        if (con.Text != string.Empty)
                        {
                            snackRW += Convert.ToInt32(con.Text);
                        }
                        else snackRW += 0;

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            if (panel.Name.Equals(ing.getName()))
                            {
                                if (con.Text != string.Empty)
                                {
                                    ing.setEnteredWeight(Convert.ToInt32(con.Text));                              // Add entered weight to ingredient.
                                }
                                else ing.setEnteredWeight(0);
                                temp = (ing.getEnteredWeight() * ing.getCalories()) / (double)ing.getWeight();    // Calorie calculation.
                                ing.setCalculatedCal(temp);                                                       // Add calculated calorie to ingredient.
                            }
                        }
                    }
                    if (con is Panel)
                    {
                        foreach (Control lab in con.Controls)
                        {
                            if (lab is Label)
                            {
                                // Iterate through each Ingredient object within the Ingredient List in Program.
                                foreach (Ingredient ing in Program.ingredients)
                                {
                                    if (panel.Name.Equals(ing.getName()))
                                    {
                                        lab.Text = ing.getCalculatedCal().ToString();    // Display calculated calories.
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Iterate through each Ingredient object within the Ingredient List in Program.
            foreach (Ingredient ing in Program.ingredients)
            {
                // Add total calculated calories for each ingredient type. (course)
                switch (ing.getCourse())
                {
                    case 1:
                        entreCalculatedCal += ing.getCalculatedCal();
                        break;
                    case 2:
                        baseCalculatedCal += ing.getCalculatedCal();
                        break;
                    case 3:
                        snackCalculatedCal += ing.getCalculatedCal();
                        break;
                    default:
                        break;
                }
            }

            // Display each sum to the corresponding TextBox.
            entreRWTotalTB.Text = entreRW.ToString();
            baseRWTotalTB.Text = baseRW.ToString();
            snackRWTotalTB.Text = snackRW.ToString();
            totalRW += entreRW + baseRW + snackRW;
            totalRWTB.Text = totalRW.ToString();

            // Display each total calorie to the corresponding TextBox.
            entreCalTotalTB.Text = entreCalculatedCal.ToString("0.00");
            baseCalTotalTB.Text = baseCalculatedCal.ToString("0.00");
            snackCalTotalTB.Text = snackCalculatedCal.ToString("0.00");
            totalCalculatedCal += entreCalculatedCal + baseCalculatedCal + snackCalculatedCal;
            totalCalTB.Text = totalCalculatedCal.ToString("0.00");

            // Calculating and displaying portion calories if applicable.
            if (cookedWeightTB.Text != string.Empty)
            {
                if (!int.TryParse(cookedWeightTB.Text, out cookedWeight))
                    cookedWeight = 0;
            }
            else cookedWeight = 0;
            if (portionWeightTB.Text != string.Empty)
            {
                if (!int.TryParse(portionWeightTB.Text, out portionWeight))
                    portionWeight = 0;
            }
            else portionWeight = 0;
            if (cookedWeight == 0 || portionWeight == 0)
            {
                portionCalTB.Text = "0";
                portionlAllCalTB.Text = "0";
            }
            else
            {
                portionCalculatedCal = (entreCalculatedCal * portionWeight) / cookedWeight;
                portionCalTB.Text = portionCalculatedCal.ToString("0.00");
                portionlAllCalTB.Text = (portionCalculatedCal + baseCalculatedCal + snackCalculatedCal).ToString("0.00");
            }
        }

        /**
         * saveButton_Click() function listens to the saveButton.
         * Upon a click, a new generated Form will appear prompting the user to enter a recipe name.
         * The user is then given the choice to either press 'cancel' or 'ok'.
         * If the user entered a name and pressed 'ok', the recipe will be saved.
         */
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Error trap - Cannot save if no ingredient is present.
            if (ingPanels.Count <= 0)
            {
                MessageBox.Show("No ingredients to save!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            String value = "";
            if (inputBox("Recipe Name: ", "", ref value, 1) == DialogResult.OK)
            {
                // Assigning file's name to user entered recipe name and creating the file within the 'SavedRecipes' directory.
                String fName = value;
                StreamWriter writer = new StreamWriter(Program.savedIngredientPath + fName + ".txt");

                // Writing all added ingredients to created file.
                foreach (Panel panel in ingPanels)
                {
                    writer.WriteLine(panel.Name);
                }
                writer.Close();

                // Display Success message upon recipe save.
                MessageBox.Show("SUCCESS! Recipe saved!", "", MessageBoxButtons.OK);
            }
        }

        /**
         * logButton_Click() function listents to the logButton.
         * Upon a click, a new generated Form will appear prompting the user to enter a log name.
         * The generated TextBox within the Form will already contain the intended log name. (Todays date)
         * The user is then given a chance to change it, or keep it by pressing 'ok'.
         * Additionally, the user can cancel the log by pressing 'cancel'.
         * If the user entered a name and pressed 'ok', the log will be saved.
         */
        private void logButton_Click(object sender, EventArgs e)
        {
            // Error trap - Cannot log before calculating.
            if (totalCalTB.Text.Equals(string.Empty) || totalCalTB.Text.Equals("0"))
            {
                MessageBox.Show("No data to log! Make sure to calculate first!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            // Name of the log file to be saved will be log day's date.
            String lName = DateTime.Today.ToString("ddMMMyy").ToUpper();

            // Dialog Box asking user to confirm log name. (Correct Date)
            String value = "";
            if (inputBox("Log Name: ", lName, ref value, 2) == DialogResult.OK)
            {
                lName = value;

                // Display Success message upon saving log.
                MessageBox.Show("SUCCESS! Log saved!", "", MessageBoxButtons.OK);
            }
            else return;
            
            String fName = Program.logsPath + lName + ".txt";
            Log temp = null;
            StreamWriter writer = new StreamWriter(fName);

            // Recipe is not portioned.
            if (portionCalTB.Text.Equals(string.Empty) || portionCalTB.Text.Equals("0"))
            {
                temp = new Log(fName, main.getTitle(), 
                    entreRW, baseRW, snackRW, totalRW, 
                    entreCalculatedCal, baseCalculatedCal, snackCalculatedCal, totalCalculatedCal);
            }

            // Recipe is portioned.
            else
            {
                temp = new Log(fName, main.getTitle(),
                    entreRW, baseRW, snackRW, totalRW,
                    entreCalculatedCal, baseCalculatedCal, snackCalculatedCal, totalCalculatedCal,
                    cookedWeight, portionWeight, portionCalculatedCal);
            }
            // Loop through each ingredient panels.
            foreach (Panel panel in ingPanels)
            {
                foreach (Ingredient ing in Program.ingredients)
                {
                    if (panel.Name.Equals(ing.getName()))
                    {
                        temp.addIngredient(ing);
                    }
                }
            }

            // Write log to file.
            writer.Write(temp.toString());
            writer.Close();
            reset();
        }

        /**********************************************************************************/
        /*                                SETTERS/GETTERS                                 */
        /**********************************************************************************/

        /**
         * Getter for 'ingredientButtons'.
         * 
         * @return 'ingredientButtons'.
         */
        public List<Button> getIngredientButtons()
        {
            return this.ingredientButtons;
        }

        /**
         * Setter for 'ingredientButtons'.
         * 
         * @param iB assigned to 'ingredientButtons'.
         */
        public void setIngredientButtons(List<Button> iB)
        {
            this.ingredientButtons = iB;
        }

        /**
         * Get ingredient button at index 'i' from 'ingredientButtons' list.
         * 
         * @param i index.
         */
        public Button getIngredientButtonAt(int i)
        {
            return this.ingredientButtons[i];
        }

        /**
         * Getter for 'ingPanels'.
         * 
         * @return 'ingPanels'.
         */
        public List<Panel> getIngredientPanels()
        {
            return this.ingPanels;
        }

        /**
         * Setter for 'ingPanels'.
         * 
         * @param iP assigned to 'ingPanels'.
         */
        public void setIngredientPanels(List<Panel> iB)
        {
            this.ingPanels = iB;
        }

        /**
         * Get ingredient panel at index 'i' from 'ingPanels' list.
         * 
         * @param i index.
         */
        public Panel getIngredientPanelAt(int i)
        {
            return this.ingPanels[i];
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
         * @param eP assigned to 'entrePanels'.
         */
        public void setEntrePanels(List<Panel> eP)
        {
            this.entrePanels = eP;
        }

        /**
         * Get entre panel at index 'i' from 'entrePanels' list.
         * 
         * @param i index.
         */
        public Panel getEntrePanelAt(int i)
        {
            return this.entrePanels[i];
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
         * @param bP assigned to 'basePanels'.
         */
        public void setBasePanels(List<Panel> bP)
        {
            this.basePanels = bP;
        }

        /**
         * Get base panel at index 'i' from 'basePanels' list.
         * 
         * @param i index.
         */
        public Panel getBasePanelAt(int i)
        {
            return this.basePanels[i];
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
         * @param sP assigned to 'snackPanels'.
         */
        public void setSnackPanels(List<Panel> sP)
        {
            this.snackPanels = sP;
        }

        /**
         * Get base panel at index 'i' from 'snackPanels' list.
         * 
         * @param i index.
         */
        public Panel getSnackPanelAt(int i)
        {
            return this.snackPanels[i];
        }

        /**
         * Getter for 'entreRW'.
         * 
         * @return 'entreRW'.
         */
        public int getEntreWeight()
        {
            return entreRW;
        }

        /**
         * Setter for 'entreRW'.
         * 
         * @param eW assigned to 'entreRW'.
         */
        public void setEntreWeight(int eW)
        {
            this.entreRW = eW;
        }

        /**
         * Getter for 'baseRW'.
         * 
         * @return 'baseRW'.
         */
        public int getBaseWeight()
        {
            return this.baseRW;
        }

        /**
         * Setter for 'baseRW'.
         * 
         * @param bW assigned to 'baseRW'.
         */
        public void setBaseWeight(int bW)
        {
            this.baseRW = bW;
        }

        /**
         * Getter for 'snackRW'.
         * 
         * @return 'snackRW'.
         */
        public int getSnackWeight()
        {
            return this.snackRW;
        }

        /**
         * Setter for 'snackRW'.
         * 
         * @param sW assigned to 'snackRW'.
         */
        public void setSnackWeight(int sW)
        {
            this.snackRW = sW;
        }

        /**
         * Getter for 'totalRW'.
         * 
         * @return 'totalRW'.
         */
        public int getTotalWeight()
        {
            return this.totalRW;
        }

        /**
         * Setter for 'totalRW'.
         * 
         * @param tW assigned to 'totalRW'.
         */
        public void setTotalWeight(int tW)
        {
            this.totalRW = tW;
        }

        /**
         * Getter for 'entreCalculatedCal'.
         * 
         * @return 'entreCalculatedCal'.
         */
        public double getEntreCal()
        {
            return this.entreCalculatedCal;
        }

        /**
         * Setter for 'entreCalculatedCal'.
         * 
         * @param eC assigned to 'entreCalculatedCal'.
         */
        public void setEntreCal(double eC)
        {
            this.entreCalculatedCal = eC;
        }

        /**
         * Getter for 'baseCalculatedCal'.
         * 
         * @return 'baseCalculatedCal'.
         */
        public double getBaseCal()
        {
            return this.baseCalculatedCal;
        }

        /**
         * Setter for 'baseCalculatedCal'.
         * 
         * @param bC assigned to 'baseCalculatedCal'.
         */
        public void setBaseCal(double bC)
        {
            this.baseCalculatedCal = bC;
        }

        /**
         * Getter for 'snackCalculatedCal'.
         * 
         * @return 'snackCalculatedCal'.
         */
        public double getSnackCal()
        {
            return this.snackCalculatedCal;
        }

        /**
         * Setter for 'snackCalculatedCal'.
         * 
         * @param sC assigned to 'snackCalculatedCal'.
         */
        public void setSnackCal(double sC)
        {
            this.snackCalculatedCal = sC;
        }

        /**
         * Getter for 'totalCalculatedCal'.
         * 
         * @return 'totalCalculatedCal'.
         */
        public double getTotalCal()
        {
            return this.totalCalculatedCal;
        }

        /**
         * Setter for 'totalCalculatedCal'.
         * 
         * @param tC assigned to 'totalCalculatedCal'.
         */
        public void setTotalCal(double tC)
        {
            this.totalCalculatedCal = tC;
        }

        /**
         * Getter for 'cookedWeight'.
         * 
         * @return 'cookedWeight'.
         */
        public int getCookedWeight()
        {
            return this.cookedWeight;
        }

        /**
         * Setter for 'cookedWeight'.
         * 
         * @param cW assigned to 'cookedWeight'.
         */
        public void setCookedWeight(int cW)
        {
            this.cookedWeight = cW;
        }

        /**
         * Getter for 'portionWeight'.
         * 
         * @return 'portionWeight'.
         */
        public int getPortionWeight()
        {
            return this.portionWeight;
        }

        /**
         * Setter for 'portionWeight'.
         * 
         * @param pW assigned to 'portionWeight'.
         */
        public void setPortionWeight(int pW)
        {
            this.portionWeight = pW;
        }

        /**
         * Getter for 'portionCalculatedCal'.
         * 
         * @return 'portionCalculatedCal'.
         */
        public double getPortionCal()
        {
            return this.portionCalculatedCal;
        }

        /**
         * Setter for 'portionCalculatedCal'.
         * 
         * @param pC assigned to 'portionCalculatedCal'.
         */
        public void setPortionCal(double pC)
        {
            this.portionCalculatedCal = pC;
        }        
    }
}
