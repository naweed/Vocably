

namespace XGENO.Vocably.Views;

public partial class NewGamePage : ViewBase<NewGamePageViewModel>
{
    private int currentRow = 0;
    private int currentColumn = 0;
    private bool isLocked = false;
    private string theWord;
    private string theMeaning;
    private List<Level_Progress> levelProgress;
    private NewGamePageViewModel viewModel;

    private Color correctWordColor;
    private Color notPresentWordcolor;
    private Color wrongPlaceColor;
    private Color wrongWordColor;
    private Color lightTextColor;
    private Color darkTextColor;
    private Color mediumFrameColor;

    private bool previouslyLoaded = false;

    private WordleBox[,] wordleFrames;


    public NewGamePage() : base()
    {
		InitializeComponent();

        lightTextColor = (Color)App.Current.Resources["LightTextColor"];
        darkTextColor = (Color)App.Current.Resources["DarkTextColor"];
        mediumFrameColor = (Color)App.Current.Resources["MediumFrameColor"];

        correctWordColor = (Color)App.Current.Resources["CorrectWordColor"];
        notPresentWordcolor = (Color)App.Current.Resources["NotPresentWordcolor"];
        wrongPlaceColor = (Color)App.Current.Resources["WrongPlaceColor"];
        wrongWordColor = (Color)App.Current.Resources["WrongWordColor"];

        this.ViewModelInitialized += (s, e) =>
        {
            viewModel = this.BindingContext as NewGamePageViewModel;
            viewModel.WordLoadedEvent += WordLoadedEvent;
        };
    }

    private void WordLoadedEvent(object sender, WordEventArgs e)
    {
        theWord = e.Word;
        theMeaning = e.Meaning;
        levelProgress = e.Progress;

        this.InitializeGameScreen();
    }

    private async void InitializeGameScreen()
    {
        wordleFrames = new WordleBox[6, 5];

        //Set the Current Row and Column to ZERO
        currentColumn = 0;
        currentRow = 0;

        //Setup the Grids
        for (int i = 0; i < 6; i++)
        {
            var wordleRow = WordleGrid.Children[i] as HorizontalStackLayout;

            //Clear all the children
            if (wordleRow.Children.Count > 0)
                wordleRow.Children.Clear();

            //Add the frames
            for (int j = 0; j < 5; j++)
            {
                var wordleFrame = new WordleBox();
                ////TODO: Remove after frame opacity issue is fixed
                //_ = wordleFrame.FadeTo(0, 10, Easing.BounceOut);
                wordleFrames[i, j] = wordleFrame;
                wordleRow.Children.Add(wordleFrame);
            }
        }

        stkKeyboardRow1.Opacity = 0;
        stkKeyboardRow2.Opacity = 0;
        stkKeyboardRow3.Opacity = 0;

        if (previouslyLoaded)
        {
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                var button = GetPressedButton(ch.ToString());
                button.BackgroundColor = mediumFrameColor;
                button.TextColor = lightTextColor;
            }
        }


        //Show Previous Level Progress if applicable
        if (levelProgress.Count > 0)
        {
            foreach (var row in levelProgress)
            {
                for (int i = 0; i < 5; i++)
                {
                    var frame = wordleFrames[currentRow, i];
                    var labelControl = GetLabelControl(frame);
                    labelControl.Text = row.Guess_Word[i].ToString();
                    var button = GetPressedButton(row.Guess_Word[i].ToString());


                    switch (row.ResultPhrase[i])
                    {
                        case 'C':
                            //Correct word
                            frame.BackgroundColor = correctWordColor;
                            button.BackgroundColor = correctWordColor;
                            button.TextColor = lightTextColor;
                            break;
                        case 'W':
                            //Wrong Place word
                            frame.BackgroundColor = wrongPlaceColor;
                            labelControl.TextColor = Colors.Black;
                            if (button.BackgroundColor != correctWordColor)
                            {
                                button.BackgroundColor = wrongPlaceColor;
                                button.TextColor = darkTextColor;
                            }
                            break;
                        case '_':
                            //Wrong word
                            frame.BackgroundColor = notPresentWordcolor;
                            if (button.BackgroundColor != correctWordColor && button.BackgroundColor != wrongPlaceColor)
                            {
                                button.BackgroundColor = notPresentWordcolor;
                                button.TextColor = lightTextColor;
                            }
                            break;
                    }
                }

                currentRow++;
            }
        }


        //Make the grid visible
        for (int i = 0; i < 6; i++)
        {
            //Add the frames
            for (int j = 0; j < 5; j++)
            {
                var wordleFrame = wordleFrames[i, j];
                _ = wordleFrame.FadeTo(1, 500, Easing.BounceOut);
                await Task.Delay(50);
            }
        }

        //Set Highlight
        SetHighlightedBox();

        //Make the keyboards visible
        _ = stkKeyboardRow1.FadeTo(1, 1000, Easing.SinIn);
        _ = stkKeyboardRow2.FadeTo(1, 1000, Easing.SinIn);
        await stkKeyboardRow3.FadeTo(1, 1000, Easing.SinIn);

        if (!previouslyLoaded)
            previouslyLoaded = true;
    }

    private Label GetLabelControl(WordleBox frame) => frame.FindByName<Label>("lblLetter");

    private async Task AnimateFrame(WordleBox frame)
    {
        await frame.RotateYTo(0, 250, Easing.SinIn);
    }

    private void SetHighlightedBox(bool remove = false)
    {
        if (currentRow < 0 || currentRow > 5 || currentColumn < 0 || currentColumn > 4)
            return;

        //Get the control
        var frame = wordleFrames[currentRow, currentColumn];
        var labelControl = GetLabelControl(frame);

        //Set the value in the label
        labelControl.Text = (remove ? "" : "_");
    }

    private void Regular_Keyboard_Button_Clicked(object sender, EventArgs e)
    {
        if (currentRow < 0 || currentRow > 5 || currentColumn < 0 || currentColumn > 4)
            return;

        //Provide Haptic Feedback
        try
        {
            HapticFeedback.Perform(HapticFeedbackType.Click);
        }
        catch (FeatureNotSupportedException ex)
        {
        }
        catch
        {
        }

        //Get the control
        var frame = wordleFrames[currentRow, currentColumn];
        var labelControl = GetLabelControl(frame);

        //Set the value in the label
        labelControl.Text = (sender as Button).Text;

        currentColumn++;

        //Set Highlight
        SetHighlightedBox();
    }

    private Button GetPressedButton(string label)
    {
        var btnControl = new Button();

        foreach (var control in stkKeyboardRow1.Children)
        {
            if (control is Button btn)
            {
                if (btn.Text == label)
                {
                    return btn;
                }
            }
        }

        foreach (var control in stkKeyboardRow2.Children)
        {
            if (control is Button btn)
            {
                if (btn.Text == label)
                {
                    return btn;
                }
            }
        }

        foreach (var control in stkKeyboardRow3.Children)
        {
            if (control is Button btn)
            {
                if (btn.Text == label)
                {
                    return btn;
                }
            }
        }

        return btnControl;
    }

    private async void Back_Keyboard_Button_Clicked(object sender, EventArgs e)
    {
        if (currentRow < 0 || currentRow > 5 || currentColumn == 0)
            return;

        //Remove Highlight
        SetHighlightedBox(true);

        currentColumn--;

        //Set Highlight
        SetHighlightedBox();

        try
        {
            HapticFeedback.Perform(HapticFeedbackType.Click);
        }
        catch (FeatureNotSupportedException ex)
        {
        }
        catch
        {
        }

    }

    private async void UseHint_Clicked(object sender, EventArgs e)
    {
        var showHint = await viewModel.Can_Show_Hint();

        //Show Hint
        if (showHint)
        {
            var hintPopup = new ShowHintPopupView(theMeaning, theWord);
            this.ShowPopup(hintPopup);
        }
    }

    private async void Enter_Keyboard_Button_Clicked(object sender, EventArgs e)
    {
        if (currentRow < 0 || currentRow > 5 || currentColumn != 5 || isLocked)
            return;

        isLocked = true;

        //Get the word
        var guessWord = String.Empty;
        for (int i = 0; i < 5; i++)
        {
            var frame = wordleFrames[currentRow, i];
            var labelControl = GetLabelControl(frame);

            guessWord += labelControl.Text;
        }

        var isValidWord = await viewModel.IsValidWord(guessWord);

        //Invalid word detected
        if (!isValidWord)
        {
            for (int i = 0; i < 5; i++)
            {
                var frame = wordleFrames[currentRow, i];
                frame.RotationY = -90;
            }


            for (int i = 0; i < 5; i++)
            {
                var frame = wordleFrames[currentRow, i];
                frame.BackgroundColor = wrongWordColor;
                await AnimateFrame(frame);
            }

            //Provide Haptic Feedback
            try
            {
                var duration = TimeSpan.FromMilliseconds(500);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }

            //Wait and Clear
            await Task.Delay(500);

            for (int i = 0; i < 5; i++)
            {
                var frame = wordleFrames[currentRow, i];
                var labelControl = GetLabelControl(frame);

                frame.BackgroundColor = Colors.Transparent;
                labelControl.Text = "";
            }

            currentColumn = 0;

            //Set Highlight
            SetHighlightedBox();

            isLocked = false;

            return;
        }

        //Guess the word
        var guessResult = WordlyHelpers.CheckWordle(theWord, guessWord);

        //Display the result in the Grid
        for (int i = 0; i < 5; i++)
        {
            var frame = wordleFrames[currentRow, i];
            frame.RotationY = -90;
        }


        for (int i = 0; i < 5; i++)
        {
            var frame = wordleFrames[currentRow, i];
            var labelControl = GetLabelControl(frame);
            var button = GetPressedButton(labelControl.Text);

            switch (guessResult[i])
            {
                case 'C':
                    //Correct word
                    frame.BackgroundColor = correctWordColor;
                    await AnimateFrame(frame);
                    button.BackgroundColor = correctWordColor;
                    button.TextColor = lightTextColor;
                    break;
                case 'W':
                    //Wrong Place word
                    frame.BackgroundColor = wrongPlaceColor;
                    labelControl.TextColor = Colors.Black;
                    await AnimateFrame(frame);
                    if (button.BackgroundColor != correctWordColor)
                    {
                        button.BackgroundColor = wrongPlaceColor;
                        button.TextColor = darkTextColor;
                    }
                    break;
                case '_':
                    //Wrong word
                    frame.BackgroundColor = notPresentWordcolor;
                    await AnimateFrame(frame);
                    if (button.BackgroundColor != correctWordColor && button.BackgroundColor != wrongPlaceColor)
                    {
                        button.BackgroundColor = notPresentWordcolor;
                        button.TextColor = lightTextColor;
                    }
                    break;
            }
        }


        //Save Level Progress
        await viewModel.SaveLevelProgress(currentRow + 1, guessWord, guessResult, theWord.ToLower() == guessWord.ToLower() ? true : false);

        currentRow++;
        currentColumn = 0;

        //Set Highlight
        SetHighlightedBox();

        isLocked = false;

        //User has WON
        if (theWord.ToLower() == guessWord.ToLower())
        {
            await viewModel.SetUserGameResult(true);

            //Show Won Animation
            var levelProg = await viewModel.ProgressLevels();

            var animPopup = new WinLostPopupView(theWord, theMeaning, "", true, viewModel.TheGame.Level_No, levelProg);
            animPopup.Closed += Popup_Popped;
            this.ShowPopup(animPopup);

            return;
        }

        //User has LOST
        if (currentRow == 6)
        {
            await viewModel.SetUserGameResult(false);

            //Show Lost Animation
            var levelProg = await viewModel.ProgressLevels();

            var animPopup = new WinLostPopupView(theWord, theMeaning, "", false, viewModel.TheGame.Level_No, levelProg);
            animPopup.Closed += Popup_Popped;
            this.ShowPopup(animPopup);

            return;
        }
    }

    private async void Popup_Popped(object sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
    {
        await viewModel.PrepareGame();
    }

}