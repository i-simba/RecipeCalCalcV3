using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
