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
        // MainFrom object
        MainForm main = null;

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public HomeForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;
        }

        // TEST TERRIBLE MEMORY - INIT ALL FNAMES AT ONCE RATHER THAN EACH CLICK!
        private void ratPicBox1_Click(object sender, EventArgs e)
        {
            //String[] fNames = Directory.GetFiles(Program.ratPicsPath);
            //Random random = new Random();
            //int test = random.Next(0, fNames.Length - 1);

            //ratPicBox1.Image = Image.FromFile(fNames[test]);
        }
    }
}


/*
 * 
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
            }
 * 
 */