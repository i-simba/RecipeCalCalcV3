using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * TODO ::
 * #. Add cookware.
 * #. Display tips.
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class HomeForm : Form
    {
        private const int picSize = 64;                    // Denotes the max size for a picture box.

        private MainForm main = null;                      // MainForm object.
        private BuilderForm bForm = null;                  // BuilderForm object.

        private List<PictureBox> ingredientPics = null;    // List containing all added ingredient PictureBoxes.


        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/


        public HomeForm(MainForm m, Form b)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;
            bForm = (BuilderForm)b;

            // Initialize lists.
            ingredientPics = new List<PictureBox>();

            // Initialize pictures.
            initPics();

            // Start animations.
            ingPicsAnim.Start();
        }


        /**********************************************************************************/
        /*                                 INTERNAL USE                                   */
        /**********************************************************************************/


        /**
         * initPic() function dynamically creates PictureBoxes that contains images from files
         * located within the 'ingredientImgPath' located in Program.
         * These dynamically created PictureBoxes are added to 'ingredientPics' and 'ingDisplayPanel'.
         */
        private void initPics()
        {
            String[] fNames = Directory.GetFiles(Program.ingredientImgPath);
            for (int i = 0, j = 1; i < fNames.Length; i++, j++)
            {
                PictureBox temp = new PictureBox();
                temp.Size = new Size(picSize, picSize);
                temp.Image = Image.FromFile(fNames[i]);
                temp.Location = new Point((picSize * j) + (5 * j), 5);
                temp.BorderStyle = BorderStyle.FixedSingle;
                ingredientPics.Add(temp);
                ingDisplayPanel.Controls.Add(temp);
            }
        }


        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/


        /**
         * addIngredientButton_Click() function listens to 'addIngredientButton'.
         * Once clicked, a new AddIngredient form is created and shown.
         */
        private void addIngredientButton_Click(object sender, EventArgs e)
        {
            AddIngredient add = new AddIngredient(bForm);
            add.Show();
        }


        /**********************************************************************************/
        /*                              ANIMATION FUNCTIONS                               */
        /**********************************************************************************/


        /**
         * ingPicsAnim_Tick(() function is a function that controls the animation of all PictureBoxes
         * inside 'ingDisplayPanel'.
         * Each ingredient images will slowly move towards the left, and once the last element passes
         * the width bounds of 'ingDisplayPanel', all images will wrap around to loop all over again.
         */
        private void ingPicsAnim_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ingredientPics.Count; i++)
            {
                if (ingredientPics[i].Location.X < -picSize)
                {
                    ingredientPics[i].Location = new Point(ingredientPics[ingredientPics.Count - 1].Location.X + (picSize + 5) * i, ingredientPics[i].Location.Y);
                }
                ingredientPics[i].Location = new Point(ingredientPics[i].Location.X - 2, ingredientPics[i].Location.Y);
            }
        }

    }
}