using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/**
 * TODO:
 * #. 
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class AddIngredient : Form
    {
        private BuilderForm bForm = null;     // BuilderForm object.

        private String destPath = null;       // Destination copied image path.
        private String sourcePath = null;     // Source image path.
        private String tempPath = null;       // String containing ingredient 'type' file.

        private String type = null;           // User entered ingredient type.
        private String name = null;           // User entered ingredient name.
        private String tipNameConv = null;    // 
        private int calories;                 // User entered ingredient calories per given weight.
        private int weight;                   // User entered ingredient weight.
        private int course;                   // User entered ingredient course.


        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/


        public AddIngredient(BuilderForm b)
        {
            InitializeComponent();
            this.bForm = b;
        }


        /**********************************************************************************/
        /*                                 Internal Use                                   */
        /**********************************************************************************/


        /**
         * getTipName() function converts a given string, in this context, a user entered ingredient name
         * into camel-case. i.e., if the user entered name is "Ice Cream Sandwich", the return string
         * will be "iceCreamSandwich".
         * This function also accounts for error in user input such that there are accidental upper case
         * characters present within the body of any given word.
         * 
         * @param n Ingredient name to be converted.
         * @return String conversion of 'n'.
         */
        private String getTipNameConv(String n)
        {
            // Split 'n' and convert all characters to lower-case.
            String[] token = n.ToLower().Split(' ');

            // Convert 'n' to camel-case.
            for (int i = 0; i < token.Length; i++)
            {
                if (i == 0)
                    token[i] = char.ToLower(token[i][0]) + token[i].Substring(1);
                else
                    token[i] = char.ToUpper(token[i][0]) + token[i].Substring(1);
            }

            return string.Join("", token);
        }

        /**
         * resetTextBox() function resets a given's TextBox's properties to its original form.
         * The string 'n' is used to denote which TextBox is passed.
         * 
         * @param TextBox has its properties reset.
         * @param n String denoting which TextBox to manipulate.
         */
        private void resetTextBox(TextBox tb, String n)
        {
            if (tb.Text.Equals(n))
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }


        /**********************************************************************************/
        /*                                 BUTTON EVENTS                                  */
        /**********************************************************************************/


        /**
         * ingNameTB_Click() function listens for a click on 'ingNameTB'.
         * If the Text is "Name", clear the Text.
         */
        private void ingNameTB_Click(object sender, EventArgs e)
        {
            resetTextBox(ingNameTB, "Name");
        }

        /**
         * ingCalTB_Click() function listens for a click on 'ingCalTB'.
         * If the text is "Calories", clear the Text.
         */
        private void ingCalTB_Click(object sender, EventArgs e)
        {
            resetTextBox(ingCalTB, "Calories");
        }

        /**
         * ingWeightTB_Click() function listens for a click on 'ingWeightTB'.
         * If the text "Weight", clear the Text.
         */
        private void ingWeightTB_Click(object sender, EventArgs e)
        {
            resetTextBox(ingWeightTB, "Weight");
        }

        /**
         * okButton_Click() function listens for a click on 'okButton'.
         * Once clicked, it first validates input from the user to ensure only valid input is entered.
         * If all inputs are valid, user entered ingredient data are assigned to local variables.
         * Those variables are then written to a file that corresponds to the ingredient's type.
         * i.e., protein/veggie/liquids/misc.
         */
        private void okButton_Click(object sender, EventArgs e)
        {
            int calParse = 0;       // Calories per entered weight.
            int weightParse = 0;    // Entered weight.
            
            // Error trap - If Name have not been changed or are empty.
            if (ingNameTB.Text.Equals("Name") || ingNameTB.Text.Equals(string.Empty))
            {
                MessageBox.Show("Enter ingredient NAME!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Error trap - If Calories have not been changed or is not an integer value.
            if (ingCalTB.Text.Equals("Calories") || ingCalTB.Text.Equals (string.Empty))
            {
                MessageBox.Show("Enter ingredient CALORIES!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(ingCalTB.Text, out calParse))
            {
                MessageBox.Show("Enter NUMBERS only for CALORIES!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Error trap - If Weight have not been changed or is not an integer value.
            if (ingWeightTB.Text.Equals("Weight") || ingWeightTB.Text.Equals(string.Empty))
            {
                MessageBox.Show("Enter ingredient WEIGHT!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse (ingWeightTB.Text, out weightParse))
            {
                MessageBox.Show("Enter NUMBERS only for WEIGHT!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Error trap - If typeCombo or courseCombo don't have selected items.
            if (typeCombo.SelectedItem == null)
            {
                MessageBox.Show("Select ingredient TYPE!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (courseCombo.SelectedItem == null)
            {
                MessageBox.Show("Select ingredient COURSE!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Error trap - If no image is selected.
            if (addPicButton.Image == null)
            {
                MessageBox.Show("Select ingredient IMAGE!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Assigning user entered ingredient data to local variables to be written to a file.
            name = ingNameTB.Text;
            calories = calParse;
            weight = weightParse;
            course = courseCombo.SelectedIndex + 1;
            type = typeCombo.SelectedItem.ToString();
            tipNameConv = getTipNameConv(name);

            // Building 'destPath' and 'tempPath'.
            destPath = Path.Combine(Program.ingredientImgPath, tipNameConv + Path.GetExtension(sourcePath));
            tempPath = Path.Combine(Program.ingredientPath, typeCombo.SelectedItem.ToString().ToLower() + ".csv");

            // Error trap - If ingredient being added is already present within ingredient type file.
            String[] lines = File.ReadAllLines(tempPath);
            foreach (String str in lines)
            {
                String[] temp = str.Split(',');
                if (temp[0].Equals(tipNameConv))
                {
                    MessageBox.Show("Ingredient is already ADDED!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Writing ingredient data to the right file.
            using (StreamWriter writer = File.AppendText(tempPath))
            {
                writer.WriteLine(
                    tipNameConv + "," +
                    name + "," +
                    calories + "," +
                    weight + "," +
                    course);
            }

            // Copy source image to destination path.
            try
            {
                File.Copy(sourcePath, destPath);
            }
            catch (IOException ex)
            {
                if (ex.Message.Contains("already exists"))
                {
                    MessageBox.Show("Ingredient image already EXISTS!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "IO EXCEPTION OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Program.resetRebuildIngredients();
            bForm.resetRebuildButtons();
            MessageBox.Show("SUCCESS! Ingredient saved!", "", MessageBoxButtons.OK);
            this.Close();
        }

        /**
         * cancelButton_Click() listens for a click on 'cancelButton'.
         * If the button is clicked, this form Closes.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * addPicButton() function listens for a click on 'addPicButton'.
         * Once clicked, an OpenFileDialog is opened in which the user has the option to select
         * and image from their computer.
         * After clicking 'OK', the image's path is assigned to 'sourcePath' and the 'addPicButton'
         * image is set to the selected image.
         */
        private void addPicButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif;*.png";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                addPicButton.Image = Image.FromFile(opnfd.FileName);
                addPicButton.Text = string.Empty;
                sourcePath = opnfd.FileName;
            }
        }


        /**********************************************************************************/
        /*                                 FOCUS EVENTS                                   */
        /**********************************************************************************/


        /**
         * ingNameTB_Leave() function listens when focus leaves 'ingNameTB'.
         * If the Text is empty, set Text back to "Name".
         */
        private void ingNameTB_Leave(object sender, EventArgs e)
        {
            if (ingNameTB.Text.Equals(string.Empty))
            {
                ingNameTB.Text = "Name";
                ingNameTB.ForeColor = Color.Gray;
            }
        }

        /**
         * ingCalTB_Leave() function listens when focus leaves 'ingCalTB'.
         * If the Text is empty, set Text back to "Calories".
         */
        private void ingCalTB_Leave(object sender, EventArgs e)
        {
            if (ingCalTB.Text.Equals(string.Empty))
            {
                ingCalTB.Text = "Calories";
                ingCalTB.ForeColor = Color.Gray;
            }
        }

        /**
         * ingWeightTB_Leave() function listens when focus leaves 'ingWeightTB'.
         * If the Text is empty, set Text back to "Weight".
         */
        private void ingWeightTB_Leave(object sender, EventArgs e)
        {
            if (ingWeightTB.Text.Equals(string.Empty))
            {
                ingWeightTB.Text = "Weight";
                ingWeightTB.ForeColor = Color.Gray;
            }
        }

        /**
         * ingNameTB_Enter() function listens when focus enters 'ingNameTB'.
         * This function then calls 'resetTextBox', which resets 'ingNameTB'
         * to its original state.
         */
        private void ingNameTB_Enter(object sender, EventArgs e)
        {
            resetTextBox(ingNameTB, "Name");
        }

        /**
         * ingCalTB_Enter() function listens when focus enters 'ingCalTB'.
         * This function then calls 'resetTextBox', which resets 'ingCalTB'
         * to its original state.
         */
        private void ingCalTB_Enter(object sender, EventArgs e)
        {
            resetTextBox(ingCalTB, "Calories");
        }

        /**
         * ingWeightTB_Enter() function listens when focus enters 'ingWeightTB'.
         * This function then calls 'resetTextBox', which resets 'ingWeightTB'
         * to its original state.
         */
        private void ingWeightTB_Enter(object sender, EventArgs e)
        {
            resetTextBox(ingWeightTB, "Weight");
        }
    }
}
