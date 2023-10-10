namespace XGENO.Vocably.Services;

public interface IDatabaseService
{
    Task<List<Level>> GetAllLevels();
    Task<int> GetAllLevelsCount();
    Task<Level> GetLevelDetails(int levelNo);
    Task SaveLevel(Level word);
    Task SaveLevels(List<Level> words);
    Task<int> GetAllValidWordsCount();
    Task SaveValidWords(List<Valid_Word> words);
    Task<bool> IsValidEnglishWord(string word);
    Task SaveLeveProgress(Level_Progress progress);
    Task<List<Level_Progress>> GetLevelProgress(int levelNo);
    Task<List<Level_Progress>> GetAllLevelProgress();
}