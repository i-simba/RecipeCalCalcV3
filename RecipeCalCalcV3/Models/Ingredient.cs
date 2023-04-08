/**
 * Ingredient is a class that represents a food ingredient.
 * This class' objects are used for handling data relating to an ingredient
 * and is responsible for containing the name, type, calories, and weight
 * of a given ingredient.
 * Types are currently handled as Strings, but could easily be refactored as enumerations.
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
    internal class Ingredient
    {
        private String name;     // Name of ingredient.
        private String type;     // Type of ingredient - Protein, Vegetable, etc.
        private int calories;    // Calories contained within the set weight.
        private int weight;      // Weight of ingredient related to calories.

        private double calculatedCal;    // The total calculated calories for an ingredient based on its entered weight.
        private double enteredWeight;    // The entered weight of a given ingredient and will be used to calculate 'calculatedCal'.

        /**
         * Constructor - String, String, Int, Int.
         * 
         * @param n assigned to 'name'.
         * @param t assigned to 'type'.
         * @param c assigned to 'calories'.
         * @param w assigned to 'weight'.
         */
        public Ingredient(String n, String t, int c, int w)
        {
            this.name = n;
            this.type = t;
            this.calories = c;
            this.weight = w;

            this.calculatedCal = 0;
            this.enteredWeight = 0;
        }

        /**
         * toString() function returns the string representation of ingredient object.
         * 
         * @return ingredient object's details.
         */
        public String toString()
        {
            return "Name: " + this.name + 
                " Type: " + this.type + 
                " Calories: " + this.calories + 
                " Weight: " + this.weight;
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
         * Getter for 'type'.
         * 
         * @return 'type'.
         */
        public String getType()
        {
            return this.type;
        }

        /**
         * Setter for 'type'.
         * 
         * @param t assigned to 'type'.
         */
        public void setType(String t)
        {
            this.type = t;
        }

        /**
         * Getter for 'calories'.
         * 
         * @return 'calories'.
         */
        public int getCalories()
        {
            return this.calories;
        }

        /**
         * Setter for 'calories'.
         * 
         * @param c assigned to 'calories'.
         */
        public void setCalories(int c)
        {
            this.calories = c;
        }

        /**
         * Getter for 'weight'.
         * 
         * @return 'weight'.
         */
        public int getWeight()
        {
            return this.weight;
        }

        /**
         * Setter for 'weight'.
         * 
         * @param w assigned to 'weight'.
         */
        public void setWeight(int w)
        {
            this.weight = w;
        }

        /**
         * Getter for 'calculatedCal'.
         * 
         * @return 'calculatedCal'.
         */
        public double getCalculatedCal()
        {
            return this.calculatedCal;
        }

        /**
         * Setter for 'calculatedCal'.
         * 
         * @param c assigned to 'calculatedCal'.
         */
        public void setCalculatedCal(double c)
        {
            this.calculatedCal = c;
        }

        /**
         * Getter for 'enteredWeight'.
         * 
         * @return 'enteredWeight'.
         */
        public double getEnteredWeight()
        {
            return this.enteredWeight;
        }

        /**
         * Setter for 'enteredWeight'.
         * 
         * @param e assigned to 'enteredWeight'.
         */
        public void setEnteredWeight(double e)
        {
            this.enteredWeight = e;
        }
    }
}
