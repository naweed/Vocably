namespace XGENO.Vocably.Models;

public class WordEventArgs : EventArgs
{
    public string Word { get; set; }
    public string Meaning { get; set; }
    public List<Level_Progress> Progress { get; set; } = new List<Level_Progress>();
}