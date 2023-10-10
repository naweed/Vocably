namespace XGENO.Vocably.ViewControls;

public partial class ShowHintPopupView : Popup
{
    private string meaning, word;

    public ShowHintPopupView(string _meaning, string _word)
    {
        InitializeComponent();

        meaning = _meaning;
        word = _word;

        PrepareView();
    }

    private void PrepareView()
    {
        var firstChar = word.Substring(0, 1).ToUpper();

        var aAn = "a";

        switch (firstChar)
        {
            case "A":
            case "E":
            case "F":
            case "H":
            case "I":
            case "L":
            case "M":
            case "N":
            case "O":
            case "R":
            case "S":
            case "X":
                aAn = "an";
                break;
            default:
                aAn = "a";
                break;
        }

        txtTitle.Text = "This word starts with " + aAn + " \"" + firstChar + "\", and it means:";

        txtMeaning.Text = "\"" + meaning + "\"";
    }

    private void CloseButton_Clicked(object sender, EventArgs e) => this.Close();
}