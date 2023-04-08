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
        private String name;                        // Name of the log. (Date)
        private String recipeName;                  // Name of the recipe saved in the log.

        private List<Ingredient> entreIng;          // Ingredients used in making the entre.
        private List<Ingredient> baseIng;           // Ingredients not used in the entre. i.e. rice, pasta, etc.

        private double rawEntreWeight;              // Total weight of uncooked entre ingredients.
        private double baseIngWeight;               // Total weight of base ingredients.

        private double entreCalories;               // Total calories of entre ingredients.
        private double baseCalories;                // Total calories of base ingredients.
        private double totalCalories;               // Total combined calories of entre and base ingredients.

        private double cookedWeight;                // Total cooked weight of ingredients.
        private double portionWeight;               // Total portion weight of cooked ingredients.
        private double portionCalories;             // Total calories of the entered portion weight.

        /**
         * Constructor.
         * Default constructor without arguments.
         * This will be used for creating logs.
         */
        public Log()
        {
            this.name = string.Empty;
            this.recipeName = string.Empty;

            this.entreIng = new List<Ingredient>();
            this.baseIng = new List<Ingredient>();

            this.rawEntreWeight = 0.0;
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
         * @param rEW assigned to 'rawEntreWeight'.
         * @param bW assigned to 'baseIngWeight'.
         * @param eC assigned to 'entreCalories'.
         * @param bC assigned to 'baseCalories'.
         * @param tC assigned to 'totalCalories'.
         */
        public Log(String n, String rN, double rEW, double bW, double eC, double bC, double tC)
        {
            this.name = n;
            this.recipeName = rN;

            this.entreIng = new List<Ingredient>();
            this.baseIng = new List<Ingredient>();

            this.rawEntreWeight = rEW;
            this.baseIngWeight = bW;

            this.entreCalories = eC;
            this.baseCalories = bC;
            this.totalCalories = tC;

            this.cookedWeight = 0.0;
            this.portionWeight = 0.0;
            this.portionCalories = 0.0;
        }

        /**
         * Constructor Overload.
         * This constructor will be used for recipes that are split between multiple days.
         * For example, Torta, in which the recipe will yeild enough portions for two or three days.
         * 
         * @param n assigned to 'name'.
         * @param rN assigned to 'recipeName'.
         * @param rEW assigned to 'rawEntreWeight'.
         * @param bW assigned to 'baseIngWeight'.
         * @param eC assigned to 'entreCalories'.
         * @param bC assigned to 'baseCalories'.
         * @param tC assigned to 'totalCalories'.
         * @param cW assigned to 'cookedWeight'.
         * @param pW assigned to 'portionWeight'.
         * @param pC assigned to 'portionCalories'.
         */
        public Log(String n, String rN, double rEW, double bW, double eC, double bC, double tC, double cW, double pW, double pC)
        {
            this.name = n;
            this.recipeName = rN;

            this.entreIng = new List<Ingredient>();
            this.baseIng = new List<Ingredient>();

            this.rawEntreWeight = rEW;
            this.baseIngWeight = bW;

            this.entreCalories = eC;
            this.baseCalories = bC;
            this.totalCalories = tC;

            this.cookedWeight = cW;
            this.portionWeight = pW;
            this.portionCalories = pC;
        }

        /**
         * TODO: toString().
         */

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
         * Getter for 'rawEntreWeight'.
         * 
         * @return 'rawEntreWeight'.
         */
        public double getRawEntreWeight()
        {
            return this.rawEntreWeight;
        }

        /**
         * Setter for 'rawEntreWeight'.
         * 
         * @param eW assigned to 'rawEntreWeight'.
         */
        public void setRawEntreWeight(double eW)
        {
            this.rawEntreWeight = eW;
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
    }
}
