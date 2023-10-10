using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using XGENO.Mobile.Framework.Extensions;
using XGENO.Wordly.Interfaces;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.Helpers
{
    public static class WordlyHelpers
    {
        public static async Task PrepareWordsAndLevels(IDatabaseService _appDBService, IApiService _appApiService)
        {
            //Check if Levels list has been downloaded
            var levelsCount = await _appDBService.GetAllLevelsCount();

            if (levelsCount == 0)
            {
                //Download the words from API
                var levelList = await _appApiService.GetWords(0);

                //Randomize the words
                levelList = levelList.RandomizeList();

                int currLevel = 1;

                //Prepare the Levels List
                foreach (var level in levelList)
                {
                    level.Level_No = currLevel++;
                    level.Status = (level.Level_No == 1 ? "Not Started" : "Locked");
                    level.Hint_Used = false;
                    level.Success_Row = 0;
                }

                //Save the words to database
                await _appDBService.SaveLevels(levelList);
            }

            //Check if Valid Words list has been downloaded
            var validWordsCount = await _appDBService.GetAllValidWordsCount();

            if (validWordsCount == 0)
            {
                //Download the words from API
                var wordList = await _appApiService.GetValidWords();

                //Save the words to database
                await _appDBService.SaveValidWords(wordList);
            }
        }

        public static string CheckWordle(this string originalWord, string wordToCheck)
        {
            var outWord = "";

            var sbOrginal = new StringBuilder(originalWord);

            //Round 1 Check
            for (int i = 0; i < originalWord.Length; i++)
            {
                if (char.ToUpper(originalWord[i]) == char.ToUpper(wordToCheck[i]))
                {
                    outWord += "C";
                    sbOrginal[i] = '#';
                }
                else
                    outWord += "_";
            }

            originalWord = sbOrginal.ToString();

            //Round 2 Check
            for (int i = 0; i < originalWord.Length; i++)
            {
                if (outWord[i] == 'C')
                    continue;

                var theChar = char.ToUpper(wordToCheck[i]);

                for (int j = 0; j < originalWord.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (char.ToUpper(originalWord[j]) == char.ToUpper(wordToCheck[i]))
                    {
                        var sb = new StringBuilder(outWord);
                        sb[i] = 'W';
                        sbOrginal[j] = '#';
                        outWord = sb.ToString();
                        originalWord = sbOrginal.ToString();
                    }
                }
            }

            return outWord;
        }

        public static Display_Letter[] BreakWordleLetters(string guessWord, string resultPhrase, Color correctWordColor, Color notPresentWordcolor, Color wrongPlaceColor, Color emptyBoxColor)
        {
            var brokenLetters = new Display_Letter[5];

            for (int i = 0; i < 5; i++)
            {
                var displayLetter = new Display_Letter();
                displayLetter.Alphabet = guessWord[i];

                switch (resultPhrase[i])
                {
                    case 'C':
                        //Correct word
                        displayLetter.Background_Color = correctWordColor;
                        displayLetter.Text_Color = Color.White;
                        break;
                    case 'W':
                        //Wrong Place word
                        displayLetter.Background_Color = wrongPlaceColor;
                        displayLetter.Text_Color = Color.Black;
                        break;
                    case '_':
                        //Wrong word
                        displayLetter.Background_Color = notPresentWordcolor;
                        displayLetter.Text_Color = Color.White;
                        break;
                    case ' ':
                        //Wrong word
                        displayLetter.Background_Color = emptyBoxColor;
                        displayLetter.Text_Color = Color.White;
                        break;
                }

                brokenLetters[i] = displayLetter;
            }

            return brokenLetters;
        }
    }
}

