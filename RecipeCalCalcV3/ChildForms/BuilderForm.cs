using RecipeCalCalcV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

/*
 * TODO::
 * 1. Add portion and cooked weight.
 * 2. Save recipe functionality.
 * 3. Pot/Pan object with weight(?) used to deduct from cooked weight.
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

            // Reset the text displayed for each totals TextBox.
            foreach (Panel panel in totalsPanel.Controls)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Text = string.Empty;
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
                    panel.Dispose();
                }
            }
        }

        /**
         * TODO make form pretty
         */
        public static DialogResult inputBox(String promptText, ref String value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            label.Text = promptText;

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
        private void button_Click(object sender, EventArgs e)
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
                            entreIngredientPanel.Controls.Add(container); Console.WriteLine("\nENTRE\n");
                            entrePanels.Add(container);
                            break;
                        case 2: 
                            baseIngredientPanel.Controls.Add(container); Console.WriteLine("\nBASE\n");
                            basePanels.Add(container);
                            break;
                        case 3: 
                            snacksIngredientPanel.Controls.Add(container); Console.WriteLine("\nSNACK\n");
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
                        entreRW += Convert.ToInt32(con.Text);

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            // Match panel that corresponds to the ingredient.
                            if (panel.Name.Equals(ing.getName()))
                            {
                                ing.setEnteredWeight(Convert.ToInt32(con.Text));                                  // Add entered weight to ingredient.
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
                        baseRW += Convert.ToInt32(con.Text);

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            // Match panel that corresponds to the ingredient.
                            if (panel.Name.Equals(ing.getName()))
                            {
                                ing.setEnteredWeight(Convert.ToInt32(con.Text));                                  // Add entered weight to ingredient.
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
                        snackRW += Convert.ToInt32(con.Text);

                        // Iterate through each Ingredient object within the Ingredient List in Program.
                        foreach (Ingredient ing in Program.ingredients)
                        {
                            if (panel.Name.Equals(ing.getName()))
                            {
                                ing.setEnteredWeight(Convert.ToInt32(con.Text));                                  // Add entered weight to ingredient.
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
         * 
         */
        private void saveButton_Click(object sender, EventArgs e)
        {
            // TODO
            String value = "";
            if (inputBox("Recipe Name: ", ref value) == DialogResult.OK)
            {
                // <Save Name Var> = value;
            }
        }
    }
}
