using RecipeCalCalcV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public BuilderForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;
            
            initButts();
        }

        // test
        private void initButts()
        {
            foreach (Ingredient ing in Program.ingredients)
            {
                Button temp = new Button();
                temp.Width = 64;
                temp.Height = 64;
                temp.Image = ing.getImage();

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
            }
        }
    }
}
