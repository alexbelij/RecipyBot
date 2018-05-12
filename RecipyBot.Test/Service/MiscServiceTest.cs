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

    }
}
