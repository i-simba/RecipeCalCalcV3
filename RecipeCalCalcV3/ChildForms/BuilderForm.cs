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

namespace RecipeCalCalcV3.ChildForms
{
    public partial class BuilderForm : Form
    {
        // MainFrom object
        MainForm main = null;

        // Button List & Boolean List
        private List<Button> ingredientButtons = null;
        private List<Boolean> isAdded = null;

        public BuilderForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;

            // Initialize lists.
            ingredientButtons = new List<Button>();
            isAdded = new List<Boolean>();
            initButtons();
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
                Button temp = new Button();
                temp.Name = ing.getName();
                temp.Image = ing.getImage();
                temp.Width = 64;
                temp.Height = 64;
                temp.Click += new EventHandler(button_Click);

                Boolean b = false;

                ToolTip tip = new ToolTip();
                tip.SetToolTip(temp, ing.getTipName());

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
         * 
         */
        private void button_Click(object sender, EventArgs e)
        {
            Panel container = new Panel();
            PictureBox pic = new PictureBox();

            container.BackColor = Color.FromArgb(168, 163, 157);
            container.Padding = new Padding(5, 5, 0, 0);
            container.Width = 385;
            container.Height = 70;

            pic.BackColor = Color.FromArgb(134, 130, 126);
            pic.SetBounds(3, 3, 64, 64);
            pic.Width = 64;
            pic.Height = 64;

            int i = 0;
            foreach (Button button in ingredientButtons)
            {
                if (sender == button && isAdded[i] == false)
                {
                    pic.Image = button.Image;
                    isAdded[i] = true;

                    container.Controls.Add(pic);
                    mainIngredientPanel.Controls.Add(container);

                    i++;
                }
                else i++;
            }
        }

    }
}
