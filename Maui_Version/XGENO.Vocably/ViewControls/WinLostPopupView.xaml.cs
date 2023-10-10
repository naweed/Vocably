namespace XGENO.Vocably.ViewControls;

public partial class WinLostPopupView : Popup
{
    private string word, meaning, pronunciationURL;
    private bool didWin;
    private int levelNo;
    private List<Level_Progress> levelProgress;

    public WinLostPopupView(string _word, string _meaning, string _pronunciationURL, bool _didWin, int _levelNo, List<Level_Progress> _levelProgress)
    {
        InitializeComponent();

        word = _word;
        meaning = _meaning;
        pronunciationURL = _pronunciationURL;
        didWin = _didWin;
        levelNo = _levelNo;
        levelProgress = _levelProgress;

        PrepareView();
    }

    private void PrepareView()
    {
        if (didWin)
        {
            imgWon.IsVisible = true;

        }
        else
        {
            imgLost.IsVisible = true;
        }

        txtWord.Text = word.ToUpper();
        txtMeaning.Text = "\"" + meaning + "\"";

        imgStar1.Source = ImageSource.FromFile("starempty.png");
        imgStar2.Source = ImageSource.FromFile("starempty.png");
        imgStar3.Source = ImageSource.FromFile("starempty.png");


        if (didWin)
        {
            switch (levelProgress.Count)
            {
                case 6:
                    imgStar1.Source = ImageSource.FromFile("starfilled.png");
                    break;
                case 4:
                case 5:
                    imgStar1.Source = ImageSource.FromFile("starfilled.png");
                    imgStar2.Source = ImageSource.FromFile("starfilled.png");
                    break;
                default:
                    imgStar1.Source = ImageSource.FromFile("starfilled.png");
                    imgStar2.Source = ImageSource.FromFile("starfilled.png");
                    imgStar3.Source = ImageSource.FromFile("starfilled.png");
                    break;
            }
        }
    }

    private void CloseButton_Clicked(object sender, EventArgs e) => this.Close();


    async void ShareButton_Clicked(System.Object sender, System.EventArgs e)
    {
        var textToShare = $"#Vocably \"{word.ToUpper()}\" {(didWin ? "😊" : "😞")}{Environment.NewLine}It means \"{meaning}\"" + Environment.NewLine;

        //Add Pictogram
        foreach (var row in levelProgress.OrderBy(_r => _r.Row_No))
        {
            textToShare += Environment.NewLine;

            for (int i = 0; i < 5; i++)
            {
                if (row.ResultPhrase[i] == 'C')
                    textToShare += "🟩";

                if (row.ResultPhrase[i] == 'W')
                    textToShare += "🟨";

                if (row.ResultPhrase[i] == '_')
                    textToShare += "⬛";
            }
        }

        textToShare += $"{Environment.NewLine}{Environment.NewLine}https://vocably-game.com";

        //Share
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = textToShare,
            Title = "VOCABLY"
        });
    }
}