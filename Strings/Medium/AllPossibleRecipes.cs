/**
LeetCode #2115
Strings
You have information about n different recipes. You are given a string array recipes and a 2D string array ingredients. The ith recipe has the name recipes[i], and you can create it if you have all the needed ingredients from ingredients[i]. Ingredients to a recipe may need to be created from other recipes, i.e., ingredients[i] may contain a string that is in recipes.
You are also given a string array supplies containing all the ingredients that you initially have, and you have an infinite supply of all of them.
Return a list of all the recipes that you can create. You may return the answer in any order.
Note that two recipes may contain each other in their ingredients.
Example 1:
Input: recipes = ["bread"], ingredients = [["yeast","flour"]], supplies = ["yeast","flour","corn"]
Output: ["bread"]
Explanation:
We can create "bread" since we have the ingredients "yeast" and "flour".
**/



public class Solution {
   
    public IList<string> FindAllRecipes(string[] recipes, IList<IList<string>> ingredients, string[] supplies) 
    {
        List<String> possibleRecipes = new List<String>();
        Dictionary<string, Recipe> recipeMatrix = BuildRecipeMatrix(recipes, ingredients);
        
        foreach(var recipe in recipeMatrix)
        {            
            if(recipe.Value.CheckIfRecipeIsPossible(recipeMatrix, supplies))
            {
                possibleRecipes.Add(recipe.Key);
            }
        }
        return possibleRecipes;
    }
    
    
    
    public Dictionary<string, Recipe> BuildRecipeMatrix(string[] recipes, IList<IList<string>> ingredients)
    {
        Dictionary<string, Recipe> recipeMatrix = new Dictionary<string, Recipe>();
        
        for(int i = 0; i < recipes.Length; i ++)
        {
            string recipeName = recipes[i];
            Recipe r = new Recipe(recipeName, ingredients[i].ToList<String>());
            if(!recipeMatrix.TryGetValue(recipeName, out Recipe res))
            {
                recipeMatrix.Add(recipeName, r);
            }
        }
        return recipeMatrix;
    }   
}



public class Recipe
    {
        private string name;
        private List<string> ingredients;
        private bool visited = false;
        private bool possible = false;
        
        
        public Recipe(string name, List<string> ingredients)
        {
            this.name = name;
            this.ingredients = ingredients;
        }
        
        public List<string> GetIngredients()
        {
            return this.ingredients;
        }
    
        public string GetName()
        {
            return this.name;
        }
        
        public void MarkVisited()
        {
            visited = true;
        }
        
        public void MarkPossible()
        {
            possible = true;
        }
        
        public bool IsVisited()
        {
            return visited;
        }
    
        public bool IsPossible()
        {
            return possible;
        }
    
        public bool CheckIfRecipeIsPossible(Dictionary<string, Recipe> recipeMatrix, string[] supplies)
        {
           if( !visited )
           {
               visited = true;
               int foundIngredientCount = 0;
               
               foreach(string ingredient in this.ingredients)
               {
                  if(supplies.Contains(ingredient))
                  {
                     foundIngredientCount ++;
                      continue;
                  }
                 //Check if a recipe can be built.
                 if(recipeMatrix.TryGetValue(ingredient, out Recipe ingredientRecipe))
                 {
                    if(ingredientRecipe.CheckIfRecipeIsPossible(recipeMatrix, supplies))
                    {
                            foundIngredientCount++;
                    }
                    else
                    {
                       //No need to check for rest of the ingredients.
                       break; 
                    }
                 }
               }
               if(foundIngredientCount == ingredients.Count())
               {
                    possible = true;
               }
           }          
           return possible;      
        }        
    }
