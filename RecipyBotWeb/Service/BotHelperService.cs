﻿using Microsoft.Bot.Connector;
using RecipyBotWeb.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Service
{
    public class BotHelperService
    {
        #region CHECKS ON THE USER MESSAGE
        public static bool IsGifRecipyRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeGif) || MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.GifRecipe);
        }

        public static bool IsRandomRecipeRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRandomRecipe);
        }

        public static bool IsNewRecipeForRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeFor);
        }

        public static bool IsNewRecipeWithRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.NewRecipeWith);
        }

        public static bool IsTopNRecipesRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.TopNRecipes);
        }

        public static bool IsAboutRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.About);
        }

        public static bool IsHelpOrGetStartedRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.GetStarted) || MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Help);
        }

        public static bool IsVersionRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Version);
        }

        public static bool IsFeedbackRequest(string userMessage)
        {
            return MiscService.CompareTwoStrings(userMessage, BotConstants.PreDefinedActions.Feedback);
        }
        #endregion

        #region PRE DEFINED RESPONSES
        /// <summary>
        /// Returns version information back to the user.
        /// </summary>
        public static Activity VersionResponse(Activity message)
        {
            Activity replyToConversation = message.CreateReply("The Recipy Bot is currently running at version " + BotConstants.OtherConstants.Version);
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined get started response.
        /// </summary>
        public static Activity GetStartedResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            Debug.WriteLine("Get Started Debug Writeline");

            string userGreeting = MiscService.IsUserNameDefaultOrBlank(message.From.Name) ? "Hi " + message.From.Name.Split(' ')[0] + "! " : string.Empty;
            Activity replyToConversation = message.CreateReply(userGreeting + "To get started, simple type something like the following:\n\n * Show me the top 5 recipes\n\n * Show me a recipe with chicken and basil\n\n * Show me a recipe for risotto\n\n * Show me todays special.\n\n Recipy Bot is always online so feel free to send me a message anytime.");
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined about us response.
        /// </summary>
        public static Activity AboutResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string response = "The Recipy Bot is your virtual assistant that helps you in your mundane task of deciding what to cook. Just ask The Recipy Bot what you would like a recipe for or get ideas for recipes that include the ingredients you have on you.";
            BotService.SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("This is developed by [Clyde D'Souza](https://clydedsouza.net).\n\nRecipes provided by [Recipe Puppy](http://www.recipepuppy.com/).");
            return replyToConversation;
        }

        /// <summary>
        /// Returns the pre-defined feedback response.
        /// </summary>
        public static Activity FeedbackResponse(Activity message, string defaultResponse)
        {
            if (!string.IsNullOrEmpty(defaultResponse))
            {
                BotService.SendATextResponse(message, defaultResponse);
            }

            string response = "We would love to hear your feedback. Please visit [our contact page](https://recipybot.azurewebsites.net/contact) to send us your feedback.";
            BotService.SendATextResponse(message, response);

            Activity replyToConversation = message.CreateReply("If you spot any issues that you would like to raise, please visit [this page](https://github.com/ClydeDz/RecipyBot/issues/new).");
            return replyToConversation;
        }
        #endregion 

    }
}