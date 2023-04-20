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
 * 1. Dynamically add buttons representing LOG inside 'logPanel'.
 * 2. Dynamically add 'nutritional info' screen for 'ingredientDataPanel'  | ingredient name/weight/calories.
 * 3. Dynamically add 'nutritional info' screen for 'ingredientMacroPanel' | meal macros. 
 */

namespace RecipeCalCalcV3.ChildForms
{
    public partial class LogsForm : Form
    {
        // MainFrom object
        MainForm main = null;

        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/

        public LogsForm(MainForm m)
        {
            InitializeComponent();

            // Setting 'main' to passed in MainForm 'm'.
            main = m;
        }
    }
}
