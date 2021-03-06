﻿using System;
using Xunit;
using RecipyBotWeb.Service;
using System.Collections.Generic;
using RecipyBotWeb.Constants;

namespace RecipyBot.Test.Service
{
    public class MiscServiceTest
    {
        // CompareTwoStrings()

        [Theory(DisplayName = "CompareTwoStrings_ReturnsTrue_Test")]
        [InlineData("gif", "GIF")]
        [InlineData("Gif ","Gif")]
        [InlineData("GIF","GIF")]
        public void CompareTwoStrings_ReturnsTrue_Test(string a, string b)
        {
            Assert.True(MiscService.CompareTwoStrings(a, b));
        }

        [Theory(DisplayName = "CompareTwoStrings_ReturnsFalse_Test")]
        [InlineData("abc", "xyz")]
        [InlineData("a gh", "agh")]
        [InlineData("125", "1254")]
        public void CompareTwoStrings_ReturnsFalse_Test(string a, string b)
        {
            Assert.False(MiscService.CompareTwoStrings(a, b));
        }


        // IsUserNameDefaultOrBlank()

        [Theory(DisplayName = "IsUserNameDefaultOrBlank_ReturnsTrue_Test")]
        [InlineData("user")]
        [InlineData("User")]
        [InlineData("User ")]
        [InlineData("")]
        [InlineData(null)]
        public void IsUserNameDefaultOrBlank_ReturnsTrue_Test(string username)
        {
            Assert.True(MiscService.IsUserNameDefaultOrBlank(username));
        }

        [Theory(DisplayName = "IsUserNameDefaultOrBlank_ReturnsFalse_Test")]
        [InlineData("john")]
        [InlineData("Karen")]
        [InlineData("james s")]
        public void IsUserNameDefaultOrBlank_ReturnsFalse_Test(string username)
        {
            Assert.False(MiscService.IsUserNameDefaultOrBlank(username));
        }


        // GiveXFromYNumbers()

        [Theory(DisplayName = "GiveXFromYNumbers_ReturnsTrue_Test")]
        [InlineData(1, 5)]
        [InlineData(2, 7)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        [InlineData(2, 2)]
        [InlineData(1, 1)]
        [InlineData(BotConstants.OtherConstants.DefaultTopN-1, BotConstants.OtherConstants.DefaultTopN)]
        public void GiveXFromYNumbers_ReturnsTrue_Test(int x, int y)
        {
            int count = 0;
            bool isInRange = false;

            IEnumerable<int> result = MiscService.GiveXFromYNumbers(x, y);
            foreach (var current in result)
            {
                count++;
                isInRange = current >= 0 && current < y;
            }

            Assert.NotNull(result);
            Assert.Equal(x, count);
            Assert.True(isInRange);
        }

        [Theory(DisplayName = "GiveXFromYNumbers_ReturnsNull_Test")]
        [InlineData(2, 0)]
        [InlineData(3, -2)]
        public void GiveXFromYNumbers_ReturnsNull_Test(int x, int y)
        {
            IEnumerable<int> result = MiscService.GiveXFromYNumbers(x, y);
            Assert.Null(result);            
        }

        [Theory(DisplayName = "GiveXFromYNumbers_ReturnY_Test")]
        [InlineData(5, 4)]
        [InlineData(7, 2)]
        [InlineData(2, 1)]
        public void GiveXFromYNumbers_ReturnY_Test(int x, int y)
        {
            int count = 0;
            bool isInRange = false;

            IEnumerable<int> result = MiscService.GiveXFromYNumbers(x, y);
            foreach (var current in result)
            {
                count++;
                isInRange = current >= 0 && current < y;
            }
            
            Assert.NotNull(result);
            Assert.Equal(y, count);
            Assert.True(isInRange);
        }

        [Theory(DisplayName = "GiveXFromYNumbers_ReturnDefaultTopNConstant_Test")]
        [InlineData(5, BotConstants.OtherConstants.DefaultTopN+2)]
        [InlineData(7, BotConstants.OtherConstants.DefaultTopN+1)]
        [InlineData(BotConstants.OtherConstants.DefaultTopN+2, BotConstants.OtherConstants.DefaultTopN)]
        public void GiveXFromYNumbers_ReturnDefaultTopNConstant_Test(int x, int y)
        {
            int count = 0;
            bool isInRange = false;

            IEnumerable<int> result = MiscService.GiveXFromYNumbers(x, y);
            foreach (var current in result)
            {
                count++;
                isInRange = current >= 0 && current < y;
            }

            Assert.NotNull(result);
            Assert.Equal(BotConstants.OtherConstants.DefaultTopN, count);
            Assert.True(isInRange);
        }


        // GetRandomTagline()
                
        [Fact()]
        public void GetRandomTagline_ReturnExpectedStringAndNotNullOrEmpty_Test()
        {
            string result = MiscService.GetRandomTagline();
            bool equalsExpectedString = false;
            foreach (var tagline in BotConstants.OtherConstants.Taglines)
            {
                equalsExpectedString |= result.Equals(tagline, StringComparison.CurrentCultureIgnoreCase);
            }
            
            Assert.NotNull(result);
            Assert.True(equalsExpectedString);
        }


        // GetRandomIngredientsPrefix()

        [Fact()]
        public void GetRandomIngredientsPrefix_ReturnExpectedStringAndNotNullOrEmpty_Test()
        {
            string result = MiscService.GetRandomIngredientsPrefix();
            bool equalsExpectedString = false;
            foreach (var tagline in BotConstants.OtherConstants.Prefixes)
            {
                equalsExpectedString |= result.Equals(string.Format("{0} ", tagline), StringComparison.CurrentCultureIgnoreCase);
            }

            Assert.NotNull(result);
            Assert.True(equalsExpectedString);
        }


        // MakeGif()

        [Theory(DisplayName = "MakeGif_ReturnTrimmedStringAndNotNull_Test")]
        [InlineData("https://i.imgur.com/DpUg0ai.gifv", "https://i.imgur.com/DpUg0ai.gif")]
        [InlineData("https://i.imgur.com/m0CPOml.gifv", "https://i.imgur.com/m0CPOml.gif")]
        [InlineData("", "https://i.imgur.com/DpUg0ai.gif")]
        public void MakeGif_ReturnTrimmedStringAndNotNull_Test(string url, string expectedUrl)
        {
            Assert.NotNull(MiscService.MakeGif(url));
            Assert.Equal(expectedUrl, MiscService.MakeGif(url));
        }
        

        // GetNumericEntity()
        
        [Fact()]
        public void GetNumericEntity_ReturnsNumberFromDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("name", "John");
            testParameters.Add("number", 3);

            Assert.Equal(3, MiscService.GetNumericEntity(testParameters));
        }

        [Fact()]
        public void GetNumericEntity_HandlesEmptyStringInNumberValueFromDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("name", "John");
            testParameters.Add("number", "");

            Assert.Equal(5, MiscService.GetNumericEntity(testParameters));
        }

        [Fact()]
        public void GetNumericEntity_ReturnsWhenNoNumberInDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("name", "John");
            testParameters.Add("age", 23);

            Assert.Equal(BotConstants.OtherConstants.DefaultTopN, MiscService.GetNumericEntity(testParameters));
        }

        [Fact()]
        public void GetNumericEntity_ReturnsHandlesNullInput_Test()
        {
            Assert.Equal(BotConstants.OtherConstants.DefaultTopN, MiscService.GetNumericEntity(null));
        }


        // GetFoodEntities()

        [Fact()]
        public void GetFoodEntities_ReturnsFoodItemFromDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("foodItem", "Basil");
            testParameters.Add("number", 5);

            Assert.Equal("Basil", MiscService.GetFoodEntities(testParameters, BotConstants.FoodEntitiesEnum.FoodItem));
        }

        [Fact()]
        public void GetFoodEntities_ReturnsRecipeItemFromDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("recipe", "Green salad with olives");
            testParameters.Add("number", 5);

            Assert.Equal("Green salad with olives", MiscService.GetFoodEntities(testParameters, BotConstants.FoodEntitiesEnum.Recipe));
        }

        [Theory(DisplayName = "GetFoodEntities_HandlesNull_Test")]
        [InlineData(BotConstants.FoodEntitiesEnum.FoodItem, "Chicken, Carrots")]
        [InlineData(BotConstants.FoodEntitiesEnum.Recipe, "Chicken soup")]
        public void GetFoodEntities_HandlesNull_Test(BotConstants.FoodEntitiesEnum foodEntitiesEnum, string expectedOutput)
        {
            Assert.Equal(expectedOutput, MiscService.GetFoodEntities(null, foodEntitiesEnum));
        }

        [Fact()]
        public void GetFoodEntities_ReturnsWhenNoFoodItemInDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("name", "John");
            testParameters.Add("age", 23);

            Assert.Equal(BotConstants.OtherConstants.DefaultIngredientsSerialized, MiscService.GetFoodEntities(testParameters, BotConstants.FoodEntitiesEnum.FoodItem));
        }

        [Fact()]
        public void GetFoodEntities_ReturnsWhenNoRecipeInDictionary_Test()
        {
            Dictionary<string, object> testParameters = new Dictionary<string, object>();
            testParameters.Add("name", "John");
            testParameters.Add("age", 23);

            Assert.Equal(BotConstants.OtherConstants.DefaultRecipeDish, MiscService.GetFoodEntities(testParameters, BotConstants.FoodEntitiesEnum.Recipe));
        }


        // GetNumberOfItemsForGiveXFromYNumbers()

        [Theory(DisplayName = "GetNumberOfItemsForGiveXFromYNumbers_ReturnsExpectedOutput_Test")]
        [InlineData(1, 3, 1)]
        [InlineData(7, 7, 7)]
        [InlineData(9, 8, BotConstants.OtherConstants.DefaultTopN)]
        [InlineData(9, 5, BotConstants.OtherConstants.DefaultTopN)]
        [InlineData(8, 4, 4)]
        [InlineData(0, 0, 0)]
        public void GetNumberOfItemsForGiveXFromYNumbers_ReturnsExpectedOutput_Test(int x, int y, int expected)
        {
            Assert.Equal(expected, MiscService.GetNumberOfItemsForGiveXFromYNumbers(x, y));
        }


        // GetFoodEntitiesWhenFoodItemIsNull()

        [Theory(DisplayName = "GetFoodEntitiesWhenFoodItemIsNull_ReturnsExpectedString_Test")]
        [InlineData("", BotConstants.FoodEntitiesEnum.FoodItem, BotConstants.OtherConstants.DefaultIngredientsSerialized)]
        [InlineData("", BotConstants.FoodEntitiesEnum.Recipe, BotConstants.OtherConstants.DefaultRecipeDish)]
        [InlineData("jelly", BotConstants.FoodEntitiesEnum.FoodItem, "jelly")]
        [InlineData("beans", BotConstants.FoodEntitiesEnum.Recipe, "beans")]
        public void GetFoodEntitiesWhenFoodItemIsNull_ReturnsExpectedString_Test(string foodItem, BotConstants.FoodEntitiesEnum foodEntitiesEnum, string expected)
        {
            Assert.Equal(expected, MiscService.GetFoodEntitiesWhenFoodItemIsNull(foodItem, foodEntitiesEnum));
        }
    }
}
