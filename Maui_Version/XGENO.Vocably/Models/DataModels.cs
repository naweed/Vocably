using SQLite;

namespace XGENO.Vocably.Models;

/// <summary>
/// 2700 Levels
/// </summary>
public class Level
{
    //From API Service
    [PrimaryKey]
    public int Word_ID { get; set; }
    public string Word { get; set; }
    public string Meaning { get; set; }

    //User Related Fields
    public int Level_No { get; set; }
    public string Status { get; set; } //Locked, Not Started, Started, Won, Lost
    public bool Hint_Used { get; set; }
    public int Success_Row { get; set; }

    [Ignore]
    public List<Level_Progress> Level_Details { get; set; }
}

/// <summary>
/// Rows for each level
/// </summary>
public class Level_Progress
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int Level_No { get; set; }
    public int Row_No { get; set; }
    public string Guess_Word { get; set; }
    public string ResultPhrase { get; set; }
    public bool Is_Correct { get; set; }

    [Ignore]
    public Display_Letter[] Display_Chars { get; set; }
}

/// <summary>
/// List of 15000 valid 5-letter words
/// </summary>
public class Valid_Word
{
    //From API Service
    [PrimaryKey]
    public string Word { get; set; }
}

public class Display_Letter
{
    public char Alphabet { get; set; }
    public Color Background_Color { get; set; }
    public Color Text_Color { get; set; }
}

public class Attempt_Data
{
    public int Attempt_No { get; set; }
    public int Attempt_Count { get; set; }
    public float Percentage { get; set; }
    public int Rank { get; set; } = 0;
}

public class GuessRank
{
    public int Attempt_No { get; set; }
    public int Attempt_Count { get; set; }
    public int Rank { get; set; } = 0;
}