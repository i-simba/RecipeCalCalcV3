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

namespace RecipeCalCalcV3.ChildForms
{
    public partial class BuilderForm : Form
    {
        MainForm main = null;                             // MainForm object.

        private List<Boolean> isAdded = null;             // List Booleans corresponding to each button.
        
        private List<Button> ingredientButtons = null;    // List of buttons for each ingredient.
        private List<Panel> ingPanels = null;             // List of Panels which displays all added ingredients.
        private List<Panel> entrePanels = null;           // List of Panels which displays added entre ingredients.
        private List<Panel> basePanels = null;            // List of Panels which displays added base ingredients.
        private List<Panel> snackPanels = null;           // List of Panels which displays added snack ingredients.

        private int entreRW = 0;                          // 
        private int baseRW = 0;                           // 
        private int snackRW = 0;                          // 
        private int totalRW = 0;                          // 

        private double entreCalculatedCal = 0.0;          // 
        private double baseCalculatedCal = 0.0;           // 
        private double snackCalculatedCal = 0.0;          // 
        private double totalCalculatedCal = 0.0;          // 

        public BuilderForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;

            // Initialize lists.
            ingredientButtons = new List<Button>();
            isAdded = new List<Boolean>();
            ingPanels = new List<Panel>();
            entrePanels = new List<Panel>();
            basePanels = new List<Panel>();
            snackPanels = new List<Panel>();
            initButtons();
        }

        /**
         * 
         */
        public void reset()
        {
            foreach (Panel panel in ingPanels)
            {
                panel.Dispose();
            }
            for (int i = 0; i < isAdded.Count; i++)
            {
                isAdded[i] = false;
            }
            foreach (Panel panel in totalsPanel.Controls)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Text = "";
                    }
                }
            }
            entreRW = 0;
            baseRW = 0;
            snackRW = 0;
            totalRW = 0;

            entreCalculatedCal = 0.0;
            baseCalculatedCal = 0.0;
            snackCalculatedCal = 0.0;
            totalCalculatedCal = 0.0;

            // TODO reset ingredient values in program.
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
                temp.Image = ing.getImage();
                temp.Width = 64;
                temp.Height = 64;
                temp.Click += new EventHandler(button_Click);

                // Denotes if ingredient has already been added to panel.
                Boolean b = false;

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
                isAdded.Add(b);
            }
        }

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

            // Set Panel 'container' properties.
            container.BackColor = Color.FromArgb(168, 163, 157);
            Padding mainPadding = new Padding(15, 15, 0, 0);
            Padding subPadding = new Padding(5, 5, 0, 0);
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
                if (sender == button && isAdded[i] == false)
                {
                    // Set ingredient's name and image.
                    container.Name = button.Name;
                    pic.Image = button.Image;
                    text.Name = button.Name + "TB";
                    isAdded[i] = true;

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

        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/

        /**
         * resetButton_Click() function listens to the resetButton and executes reset().
         */
        private void resetButton_Click(object sender, EventArgs e)
        {
            reset();
        }
        
        /**
         * calculateButton_Click function listens to the calculateButton.
         */
        private void calculateButton_Click(object sender, EventArgs e)
        {
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
            entreCalTotalTB.Text = entreCalculatedCal.ToString();
            baseCalTotalTB.Text = baseCalculatedCal.ToString();
            snackCalTotalTB.Text = snackCalculatedCal.ToString();
            totalCalculatedCal += entreCalculatedCal + baseCalculatedCal + snackCalculatedCal;
            totalCalTB.Text = totalCalculatedCal.ToString();
        }
    }
}
