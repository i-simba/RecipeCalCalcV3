﻿/**
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
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeCalCalcV3.Models
{
    internal class Ingredient
    {
        public const int ENTRE = 1;      // Denotes ingredient as an entre.
        public const int BASE = 2;       // Denotes ingredient as a base.
        public const int SNACK = 3;      // Denotes ingredient as a snack.

        private String name;             // Name of ingredient - Used to match with corresponding image file's name.
        private String tipName;          // Name displayed on the tooltip as the mouse hovers over an ingredient button.
        private String type;             // Type of ingredient - Protein, Vegetable, etc.
        private int calories;            // Calories contained within the set weight.
        private int weight;              // Weight of ingredient related to calories.
        private int course;              // Category of food, i.e., Entre/Base/Snack.

        private double calculatedCal;    // The total calculated calories for an ingredient based on its entered weight.
        private int enteredWeight;       // The entered weight of a given ingredient and will be used to calculate 'calculatedCal'.

        private Image img;               // Associated image file for ingredient.


        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/


        /**
         * Constructor - Default.
         */
        public Ingredient()
        {
            this.name = string.Empty;
            this.tipName = string.Empty;
            this.type = string.Empty;
            this.calories = 0;
            this.weight = 0;
            this.course = 0;

            this.calculatedCal = 0.0;
            this.enteredWeight = 0;
        }

        /**
         * Constructor - String, String, Int, Int.
         * 
         * @param n assigned to 'name'.
         * @param tn assigned to 'tipName'.
         * @param t assigned to 'type'.
         * @param c assigned to 'calories'.
         * @param w assigned to 'weight'.
         */
        public Ingredient(String n, String tn, String t, int c, int w, int cr)
        {
            this.name = n;
            this.tipName = tn;
            this.type = t;
            this.calories = c;
            this.weight = w;
            this.course = cr;

            this.calculatedCal = 0.0;
            this.enteredWeight = 0;
        }


        /**********************************************************************************/
        /*                                 EXTERNAL USE                                   */
        /**********************************************************************************/


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
         * resetValues() function resets the variables that change with each use, as these variables are based
         * on user input. These variables are the entered weight of a given ingredient, (enteredWeight)
         * and the calculated calories of the ingredient based on the entered weight. (calculatedCal)
         */
        public void resetValues()
        {
            this.calculatedCal = 0.0;
            this.enteredWeight = 0;
        }

        /**
         * cleanUp() function sets all String and Image variables to null.
         * Usually followed up by a list clearing containing Ingredient objects.
         */
        public void cleanUp()
        {
            this.name = null;
            this.tipName = null;
            this.type = null;
            this.img = null;
        }


        /**********************************************************************************/
        /*                                SETTERS/GETTERS                                 */
        /**********************************************************************************/


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
         * Getter for 'tipName'.
         * 
         * @return 'tipName'.
         */
        public String getTipName()
        {
            return this.tipName;
        }

        /**
         * Setter for 'tipName'.
         * 
         * @param tn assigned to 'tipName'.
         */
        public void setTipName(String tn)
        {
            this.tipName = tn;
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
         * Getter for 'course'.
         * 
         * @return 'course'.
         */
        public int getCourse()
        {
            return this.course;
        }

        /**
         * Setter for 'course'.
         * 
         * @param cr assigned to 'course'.
         */
        public void setCourse(int cr)
        {
            this.course = cr;
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

        public int getEnteredWeight()
        {
            return this.enteredWeight;
        }

        /**
         * Setter for 'enteredWeight'.
         * 
         * @param e assigned to 'enteredWeight'.
         */

        public void setEnteredWeight(int e)
        {
            this.enteredWeight = e;
        }

        /**
         * Getter for 'img'.
         * 
         * @return 'img'.
         */
        public Image getImage()
        {
            return this.img;
        }

        /*
         * Setter for 'img'.
         * 
         * @param i assigned to 'img'.
         */
        public void setImage(Image i)
        {
            this.img = i;
        }
    }
}
