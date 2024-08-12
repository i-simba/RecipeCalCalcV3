# Recipe Calorie Calculator

Recipe Calorie Calculator, as the name suggests, calculates the total calories of the combined ingredients based on their weight in grams.
The user also has the ability to save the entered ingredients as a recipe, as well as logging the current calculated recipe.
Saved recipes will be accessible via the "SAVED" button.
Logs will be accessible via the "LOGS" button.
Logs are saved locally as text files, although this program could benefit by using a database.
It has three main views the user can navigate, which include Home, Logs, and Builder.

## Home
Home is the default view the user will be greeted with on launch.\
It contains a panel of all currently added ingredients scrolling by from right to left.\
The user has the ability to add a new ingredient by clicking the "ADD INGREDIENT" button within the same panel,
which asks the user for data relating to the ingredient being added.\
These include:\
&emsp; - _Ingredient name\
&emsp; - Portion calories\
&emsp; - Portion weight\
&emsp; - Type (Protein, Veggie, Liquid, Snack)\
&emsp; - Course (Entre, Base, Snack)\
&emsp; - Ingredient picture_

## Logs
View all added logs saved locally, and view the recipe's calorie breakdown.
If the user enters a portion weight, the log will also display a different breakdown of calories based on the portion size.

## Builder
This is where the user is able to add ingredients, enter their weight in grams, and calculate the calories.
Totals will also be calculated and displayed.
The user also has the option to save the entered recipe, or log the calculated values.

## Saved
Clicking the "SAVED" button will display all saved recipes.
Clicking on any recipe will open that recipe in builder, where the recipe name is displayed and all ingredients entered.
