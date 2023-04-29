/**
 * Log is a class that contains information about a recipe's ingredients.
 * Each ingredient will contain an entered weight and the corresponding calorie count.
 * Totals will also be contianed within this class, totals include:
 * Raw Weight, Entre Calories, Base Calories, etc.
 * 
 * @author Ivan Simbulan
 * Recipe Calculator v3 - April 2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCalCalcV3.Models
{
    internal class Log
    {
        private Boolean isPortioned;                // Denotes if a given logged recipe has been portioned.

        private String name;                        // Name of the log. (Date)
        private String recipeName;                  // Name of the recipe saved in the log.

        private List<Ingredient> ingredientList;    // Ingredients used in making the entre.

        private double entreIngWeight;              // Total weight of uncooked entre ingredients.
        private double baseIngWeight;               // Total weight of base ingredients.
        private double snackIngWeight;              // Total weight of snack ingredients.
        private double totalIngWeight;              // Total weight of all ingredients.

        private double entreCalories;               // Total calories of entre ingredients.
        private double baseCalories;                // Total calories of base ingredients.
        private double snackCalories;               // Total calories of snack ingredients.
        private double totalCalories;               // Total combined calories of all ingredients.

        private double cookedWeight;                // Total cooked weight of ingredients.
        private double portionWeight;               // Total portion weight of cooked ingredients.
        private double portionCalories;             // Total calories of the entered portion weight.
        private double portionAllCalories;          // Total calories of the entered portion weight plus snacks and base.

        /**
         * Constructor.
         * Default constructor without arguments.
         * This will be used for creating logs.
         */
        public Log()
        {
            this.isPortioned = false;

            this.name = string.Empty;
            this.recipeName = string.Empty;

            this.ingredientList = new List<Ingredient>();

            this.entreIngWeight = 0.0;
            this.baseIngWeight = 0.0;

            this.entreCalories = 0.0;
            this.baseCalories = 0.0;
            this.totalCalories = 0.0;

            this.cookedWeight = 0.0;
            this.portionWeight = 0.0;
            this.portionCalories = 0.0;
        }

        /**
         * Constructor Overload.
         * This constructor will be used for recipes that are used only for that day.
         * For example, Chicken Katsu, in which the total isn't portioned.
         * 
         * @param n assigned to 'name'.
         * @param rN assigned to 'recipeName'.
         * @param rEW assigned to 'entreIngWeight'.
         * @param bW assigned to 'baseIngWeight'.
         * @param sW assigned to 'snackIngWeight'.
         * @param tW assigned to 'totalIngWeight'.
         * @param eC assigned to 'entreCalories'.
         * @param bC assigned to 'baseCalories'.
         * @param sC assigned to 'snackCalories'.
         * @param tC assigned to 'totalCalories'.
         */
        public Log(String n, String rN, double rEW, double bW, double sW, double tW, double eC, double bC, double sC, double tC)
        {
            this.isPortioned = false;

            this.name = n;
            this.recipeName = rN;

            this.ingredientList = new List<Ingredient>();

            this.entreIngWeight = rEW;
            this.baseIngWeight = bW;
            this.snackIngWeight = sW;
            this.totalIngWeight = tW;

            this.entreCalories = eC;
            this.baseCalories = bC;
            this.snackCalories = sC;
            this.totalCalories = tC;

            this.cookedWeight = 0.0;
            this.portionWeight = 0.0;
            this.portionCalories = 0.0;
            this.portionAllCalories = 0.0;
        }

        /**
         * Constructor Overload.
         * This constructor will be used for recipes that are split between multiple days.
         * For example, Torta, in which the recipe will yeild enough portions for two or three days.
         * 
         * @param n assigned to 'name'.
         * @param rN assigned to 'recipeName'.
         * @param rEW assigned to 'entreIngWeight'.
         * @param bW assigned to 'baseIngWeight'.
         * @param sW assigned to 'snackIngWeight'.
         * @param tW assigned to 'totalIngWeight'.
         * @param eC assigned to 'entreCalories'.
         * @param bC assigned to 'baseCalories'.
         * @param sC assigned to 'snackCalories'.
         * @param tC assigned to 'totalCalories'.
         * @param cW assigned to 'cookedWeight'.
         * @param pW assigned to 'portionWeight'.
         * @param pC assigned to 'portionCalories'.
         */
        public Log(String n, String rN, double rEW, double bW, double sW, double tW, double eC, double bC, double sC, double tC, double cW, double pW, double pC, double pAC)
        {
            this.isPortioned = false;

            this.name = n;
            this.recipeName = rN;

            this.ingredientList = new List<Ingredient>();

            this.entreIngWeight = rEW;
            this.baseIngWeight = bW;
            this.snackIngWeight = sW;
            this.totalIngWeight = tW;

            this.entreCalories = eC;
            this.baseCalories = bC;
            this.snackCalories = sC;
            this.totalCalories = tC;

            this.cookedWeight = cW;
            this.portionWeight = pW;
            this.portionCalories = pC;
            this.portionAllCalories = pAC;
        }

        /**
         * TODO: toString().
         */
        public String toString()
        {
            String temp = string.Empty;

            temp += recipeName + "\n";
            foreach (Ingredient ing in ingredientList)
            {
                temp += ing.getCourse() + "," + ing.getName() + "," +
                    ing.getEnteredWeight() + "," +
                    ing.getCalculatedCal() + "\n";
            }
            temp += "TOTALS\n";
            temp += isPortioned.ToString() + "\n";
            temp += entreIngWeight.ToString() + "," + entreCalories.ToString() + "\n";
            temp += baseIngWeight.ToString() + "," + baseCalories.ToString() + "\n";
            temp += snackIngWeight.ToString() + "," + snackCalories.ToString() + "\n";
            temp += totalIngWeight.ToString() + "," + totalCalories.ToString() + "\n";

            temp += cookedWeight.ToString() + "," + portionWeight.ToString() + "\n";
            temp += portionCalories.ToString() + "," + portionAllCalories.ToString() + "\n";

            return temp;
        }

        /**
         * Getter for 'isPortioned'.
         * 
         * @return 'isPortioned'.
         */
        public Boolean getIsPortioned()
        {
            return this.isPortioned;
        }

        /**
         * Setter for 'isPortioned'.
         * 
         * @param b assigned to 'isPortioned'.
         */
        public void setIsPortioned(Boolean b)
        {
            this.isPortioned = b;
        }

        /**
         * Getter for 'name'.
         * 
         * @return 'name'.
         */
        public String getName()
        {
            return this.name;
        }

        /**
         * Setter for 'name'.
         * 
         * @param n assigned to 'name'.
         */
        public void setName(String n)
        {
            this.name = n;
        }

        /**
         * Getter for 'recipeName'.
         * 
         * @return 'recipeName'.
         */
        public String getRecipeName()
        {
            return this.recipeName;
        }

        /**
         * Setter for 'recipeName'.
         * 
         * @param rN assigned to 'recipeName'.
         */
        public void setRecipeName(String rN)
        {
            this.recipeName = rN;
        }

        /**
         * Getter for 'entreIngWeight'.
         * 
         * @return 'entreIngWeight'.
         */
        public double getentreIngWeight()
        {
            return this.entreIngWeight;
        }

        /**
         * Setter for 'entreIngWeight'.
         * 
         * @param eW assigned to 'entreIngWeight'.
         */
        public void setentreIngWeight(double eW)
        {
            this.entreIngWeight = eW;
        }

        /**
         * Getter for 'baseIngWeight'.
         * 
         * @return 'baseIngWeight'.
         */
        public double getBaseIngWeight()
        {
            return this.baseIngWeight;
        }

        /**
         * Setter for 'baseIngWeight'.
         * 
         * @param bW assigned to 'baseIngWeight'.
         */
        public void setBaseIngWeight(double bW)
        {
            this.baseIngWeight = bW;
        }

        /**
         * Getter for 'snackIngWeight'.
         * 
         * @return 'snackIngWeight'.
         */
        public double getSnackIngWeight()
        {
            return this.snackIngWeight;
        }

        /**
         * Setter for 'snackIngWeight'.
         * 
         * @param sW assigned to 'snackIngWeight'.
         */
        public void setSnackIngWeight(double sW)
        {
            this.snackIngWeight = sW;
        }

        /**
         * Getter for 'totalIngWeight'.
         * 
         * @return 'totalIngWeight'.
         */
        public double getTotalIngWeight()
        {
            return this.totalIngWeight;
        }

        /**
         * Setter for 'totalIngWeight'.
         * 
         * @param tW assigned to 'totalIngWeight'.
         */
        public void setTotalIngWeight(double tW)
        {
            this.totalIngWeight = tW;
        }

        /**
         * Getter for 'entreCalories'.
         * 
         * @return 'entreCalories'.
         */
        public double getEntreCalories()
        {
            return this.entreCalories;
        }

        /**
         * Setter for 'entreCalories'.
         * 
         * @param eC assigned to 'entreCalories'.
         */
        public void setEntreCalories(double eC)
        {
            this.entreCalories = eC;
        }

        /**
         * Getter for 'baseCalories'.
         * 
         * @return 'baseCalories'.
         */
        public double getBaseCalories()
        {
            return this.baseCalories;
        }

        /**
         * Setter for 'baseCalories'.
         * 
         * @param bC assigned to 'baseCalories'.
         */
        public void setBaseCalories(double bC)
        {
            this.baseCalories = bC;
        }

        /**
         * Getter for 'snackCalories'.
         * 
         * @return 'snackCalories'.
         */
        public double getSnackCalories()
        {
            return this.snackCalories;
        }

        /**
         * Setter for 'snackCalories'.
         * 
         * @param sC assigned to 'snackCalories'.
         */
        public void setSnackCalories(double sC)
        {
            this.snackCalories = sC;
        }

        /**
         * Getter for 'totalCalories'.
         * 
         * @return 'totalCalories'.
         */
        public double getTotalCalories()
        {
            return this.totalCalories;
        }

        /**
         * Setter for 'totalCalories'.
         * 
         * @param tC assigned to 'totalCalories'.
         */
        public void setTotalCalories(double tC)
        {
            this.totalCalories = tC;
        }

        /**
         * Getter for 'cookedWeight'.
         * 
         * @return 'cookedWeight'.
         */
        public double getCookedWeight()
        {
            return this.cookedWeight;
        }

        /**
         * Setter for 'cookedWeight'.
         * 
         * @param cW assigned to 'cookedWeight'.
         */
        public void setCookedWeight(double cW)
        {
            this.cookedWeight = cW;
        }

        /**
         * Getter for 'portionWeight'.
         * 
         * @return 'portionWeight'.
         */
        public double getPortionWeight()
        {
            return this.portionWeight;
        }

        /**
         * Setter for 'portionWeight'.
         * 
         * @param pW assigned to 'portionWeight'.
         */
        public void setPortionWeight(double pW)
        {
            this.portionWeight = pW;
        }

        /**
         * Getter for 'portionCalories'.
         * 
         * @return 'portionCalories'.
         */
        public double getPortionCalories()
        {
            return this.portionCalories;
        }

        /**
         * Setter for 'portionCalories'.
         * 
         * @param pC assigned to 'portionCalories'.
         */
        public void setPortionCalories(double pC)
        {
            this.portionCalories = pC;
        }

        /**
         * Getter for 'portionAllCalories'.
         * 
         * @return 'portionAllCalories'.
         */
        public double getPortionAllCalories()
        {
            return this.portionAllCalories;
        }

        /**
         * Setter for 'portionAllCalories'.
         * 
         * @param pAC assigned to 'portionAllCalories'.
         */
        public void setPortionAllCalories(double pAC)
        {
            this.portionAllCalories = pAC;
        }

        /**
         * Getter for 'entreIng'
         * 
         * @return 'entreIng'.
         */
        public List<Ingredient> getIngredientList()
        {
            return this.ingredientList;
        }

        /**
         * Setter for 'entreIng'.
         * 
         * @param e assigned to 'entreIng'.
         */
        public void setIngredientList(List<Ingredient> e)
        {
            this.ingredientList = e;
        }

        /**
         * addEntre() function adds an ingredient to the entre list.
         */
        public void addIngredient(Ingredient e)
        {
            this.ingredientList.Add(e);
        }

        /**
         * getEntreAt() function returns the ingredient indexed at i.
         * 
         * @param i index.
         */
        public Ingredient getIngredientAt(int i)
        {
            return this.ingredientList[i];
        }
    }
}
