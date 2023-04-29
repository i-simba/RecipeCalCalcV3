using RecipeCalCalcV3.Models;
using RecipeCalCalcV3.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeCalCalcV3
{
    internal static class Program
    {
        /************************************************/
        /*    Data Handling Variables - Ingredients     */
        /************************************************/
        public static List<Ingredient> ingredients = null;                   // Ingredient List that holds added ingredients.
        public const String ingredientPath = ".\\Ingredients\\";             // Path to the directory 'Ingredients'.
        public const String ingredientImgPath = ".\\Images\\";               // Path to the directory 'Images'.
        public const String savedIngredientPath = ".\\SavedRecipes\\";       // Path to the directory 'SavedRecipes'.
        public const String logsPath = ".\\Logs\\";                          // Path to the directory 'Logs'.
        public const String proteinPath = ingredientPath + "protein.csv";    // Path to the csv file 'protein.csv'.
        public const String veggiePath = ingredientPath + "veggie.csv";      // Path to the csv file 'veggie.csv'.
        public const String liquidPath = ingredientPath + "liquids.csv";     // Path to the csv file 'liquids.csv'.
        public const String miscPath = ingredientPath + "misc.csv";          // Path to the csv file 'misc.csv'.

        /************************************************/
        /*    Global Variables used by all Forms        */
        /************************************************/
        public static Boolean logAdded = false;                              // Denotes whether a log was added at runtime.


        /************************************************/
        /*    The main entry point for the application. */
        /************************************************/
        [STAThread]
        static void Main()
        {
            ingredients = new List<Ingredient>();       // Initialize 'ingredients' List.
            initFiles();                                // Create directories/files if they don't exist.
            initIngredients(proteinPath, "protein");    // Add 'protein' type ingredients to List.
            initIngredients(veggiePath, "veggie");      // Add 'veggie' type ingredients to List.
            initIngredients(liquidPath, "liquid");      // Add 'liquid' type ingredients to List.
            initIngredients(miscPath, "misc");          // Add 'misc' type ingredients to List.

            // Provided code to start forms.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /**
         * initIngredients(String, String) function that reads in data from a given csv file denoted by 'path'
         * Each row contains data of a given ingredient (Name, Calories, Weight).
         * This information is then passed into an Ingredient object, which is then added to the List 'ingredients'.
         */
        private static void initIngredients(String path, String type)
        {
            // Check if file within given path is empty - exit if empty.
            if (new FileInfo(path).Length == 0)
            {
                return;
            }

            // File Reading variables
            StreamReader reader = null;    // StreamReader object used to parse csv files.
            String[] ingDetails = null;    // String array containing tokenized Strings from 'line'
            String line = null;            // String containing read in current line from csv.

            // Reading ingredients from 'path'.
            reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                // CSV file format is as follows - Name, Tool Tip Name, Calories, Weight.
                line = reader.ReadLine();        // Read line from csv.
                ingDetails = line.Split(',');    // Tokenize line.
                
                // Create Ingredient object and add to appropriate List.
                Ingredient temp = new Ingredient(
                    ingDetails[0],                      // Name.
                    ingDetails[1],                      // Tool Tip Name.
                    type,                               // Type.
                    Convert.ToInt32(ingDetails[2]),     // Calories.
                    Convert.ToInt32(ingDetails[3]),     // Weight.
                    Convert.ToInt32(ingDetails[4]));    // Course.
                ingredients.Add(temp);
                Image pic = Image.FromFile(ingredientImgPath + temp.getName() + ".png");
                temp.setImage(pic);
            }
            reader.Close();
        }

        /**
         * initFiles() function that initializes directories and files.
         * It first checks if a given directory or file exists, and if it
         * doesn't, it then creates the dirctories/files.
         */
        private static void initFiles()
        {
            if (!Directory.Exists(ingredientPath))
            {
                Directory.CreateDirectory(ingredientPath);
            }
            if (!Directory.Exists(ingredientImgPath))
            {
                Directory.CreateDirectory (ingredientImgPath);
            }
            if (!Directory.Exists(savedIngredientPath))
            {
                Directory.CreateDirectory(savedIngredientPath);
            }
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
            }
            if (!File.Exists(proteinPath))
            {
                File.Create(proteinPath).Close();
            }
            if (!File.Exists(veggiePath))
            {
                File.Create(veggiePath).Close();
            }
            if (!File.Exists(liquidPath))
            {
                File.Create(liquidPath).Close();
            }
            if (!File.Exists(miscPath))
            {
                File.Create(miscPath).Close();
            }
        }

        /**
         * resetListVals() function resets the calculated calorie and entered weight variable
         * for each Ingredient inside the list.
         */
        public static void resetListVals()
        {
            foreach (Ingredient ing in ingredients)
            {
                ing.resetValues();
            }
        }
    }
}
