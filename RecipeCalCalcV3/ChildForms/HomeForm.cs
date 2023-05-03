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
 * 1. Add ingredient functionality.
 * 2. Display tips.
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class HomeForm : Form
    {
        private const int picSize = 64;                    // Denotes the max size for a picture box.

        MainForm main = null;                              // MainForm object.

        private List<PictureBox> ingredientPics = null;    // List containing all added ingredient PictureBoxes.

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public HomeForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;

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
         * TODO:
         */
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                //TODO: Do something with selected image. File.Copy(?).
            }
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