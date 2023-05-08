/**
 * Cookware
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
    internal class Cookware
    {
        public String name { get; set; }
        public String tipName { get; set; }
        public int weight { get; set; }


        /**********************************************************************************/
        /*                                  CONSTRUCTOR                                   */
        /**********************************************************************************/


        /**
         * Constructor - String, String, Int.
         * 
         * @param n assigned to 'name'.
         * @param tn assigned to 'tipName'.
         * @param w assigned to 'weight'.
         */
        public Cookware(String n, String tn, int w)
        {
            this.name = n;
            this.tipName = tn;
            this.weight = w;
        }


        /**********************************************************************************/
        /*                                 EXTERNAL USE                                   */
        /**********************************************************************************/


        /**
         * toString()
         */
        public String toString()
        {
            return "\n" +
                "Name     : " + this.name + "\n" +
                "Tip Name : " + this.tipName + "\n" +
                "Weight   : " + this.weight;
        }

        /**
         * cleanUp() function sets all String variables to null.
         * Usually followed by a list clearing containing Cookware objects.
         */
        public void cleanUp()
        {
            this.name = null;
            this.tipName = null;
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
    }
}
