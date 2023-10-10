using SQLite;

namespace XGENO.Vocably.Services;

public class AppDBService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _dbConn;

    public AppDBService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XGENO_Wordly.dbs");

#if ANDROID
        var path2 = dbPath.Replace("/files/XGENO_Wordly.dbs", "/files/.local/share/XGENO_Wordly.dbs");

        if (File.Exists(path2))
            dbPath = path2;
#endif

        //Initiate Database Connection
        _dbConn = new SQLiteAsyncConnection(dbPath);

        //Create Tables
        CreateTables();
    }

    private async void CreateTables()
    {
        await _dbConn.CreateTableAsync<Level>();
        await _dbConn.CreateTableAsync<Level_Progress>();
        await _dbConn.CreateTableAsync<Valid_Word>();
    }

    public async Task<List<Level>> GetAllLevels() =>
        await _dbConn.Table<Level>().ToListAsync();

    public async Task<int> GetAllLevelsCount() =>
        await _dbConn.Table<Level>().CountAsync();

    public async Task<Level> GetLevelDetails(int levelNo) =>
        await _dbConn.Table<Level>().Where(_level => _level.Level_No == levelNo).FirstAsync();

    public async Task<int> GetAllValidWordsCount() =>
        await _dbConn.Table<Valid_Word>().CountAsync();

    public async Task<bool> IsValidEnglishWord(string word) =>
        (await _dbConn.Table<Valid_Word>().Where(_word => _word.Word.ToLower() == word.ToLower()).CountAsync()) > 0;

    public async Task<List<Level_Progress>> GetLevelProgress(int levelNo) =>
        await _dbConn.Table<Level_Progress>().Where(_level => _level.Level_No == levelNo).OrderBy(_l => _l.Row_No).ToListAsync();

    public async Task<List<Level_Progress>> GetAllLevelProgress() =>
        await _dbConn.Table<Level_Progress>().ToListAsync();

    public async Task SaveLevel(Level word) =>
        await _dbConn.UpdateAsync(word);

    public async Task SaveLevels(List<Level> words) =>
        await _dbConn.InsertAllAsync(words);

    public async Task SaveValidWords(List<Valid_Word> words) =>
        await _dbConn.InsertAllAsync(words);

    public async Task SaveLeveProgress(Level_Progress progress) =>
        await _dbConn.InsertAsync(progress);

}