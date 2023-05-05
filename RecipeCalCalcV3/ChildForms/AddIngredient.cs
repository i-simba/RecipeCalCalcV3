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

namespace RecipeCalCalcV3.ChildForms
{
    public partial class AddIngredient : Form
    {
        private String destPath = null;      // Destination copied image path.
        private String sourcePath = null;    // Source image path.

        private String type = null;          // User entered ingredient type.
        private String name = null;          // User entered ingredient name.
        private int calories;                // User entered ingredient calories per given weight.
        private int weight;                  // User entered ingredient weight.
        private int course;                  // User entered ingredient course.

        public AddIngredient()
        {
            InitializeComponent();
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
        private String getTipName(String n)
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
         * TODO:
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
         * TODO: okButton_Click()
         */
        private void okButton_Click(object sender, EventArgs e)
        {
            int calParse = 0;    // Calories per entered weight.
            int weightParse = 0;      // Entered weight.
            
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

            destPath = Path.Combine(Program.ingredientImgPath, getTipName(name) + Path.GetExtension(sourcePath));

            // TODO: File.Copy(source, destination);


            /*
            Console.WriteLine("\n" + sourcePath + "\n");
            Console.WriteLine(destPath + "\n");
            /*
            Console.WriteLine("\n" +
                "Name     : " + name + "\n" +
                "Calories : " + calories + "\n" +
                "Weight   : " + weight + "\n" +
                "Type     : " + type + "\n" +
                "Course   : " + course + "\n" +
                "Tip Name : " + getTipName(name) + "\n");*/
        }

        /**
         * TODO: cancelButton_Click() listens for a click on 'cancelButton'.
         * If the button is clicked, this form Closes.
         */
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * TODO: addPicButton()
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
