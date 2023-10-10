using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using XGENO.Wordly.Interfaces;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.Services
{
    public class AppDBService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _dbConn;

        public AppDBService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XGENO_Wordly.dbs");

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

        public async Task<List<Level>> GetAllLevels()
        {
            var records = await _dbConn.Table<Level>().ToListAsync();

            return records;
        }

        public async Task<int> GetAllLevelsCount()
        {
            var count = await _dbConn.Table<Level>().CountAsync();

            return count;
        }

        public async Task SaveLevel(Level word)
        {
            await _dbConn.UpdateAsync(word);
        }

        public async Task<Level> GetLevelDetails(int levelNo)
        {
            var level = await _dbConn.Table<Level>().Where(_level => _level.Level_No == levelNo).FirstAsync();

            return level;
        }

        public async Task SaveLevels(List<Level> words)
        {
            await _dbConn.InsertAllAsync(words);
        }

        public async Task<int> GetAllValidWordsCount()
        {
            var count = await _dbConn.Table<Valid_Word>().CountAsync();

            return count;
        }

        public async Task SaveValidWords(List<Valid_Word> words)
        {
            await _dbConn.InsertAllAsync(words);
        }

        public async Task<bool> IsValidEnglishWord(string word)
        {
            var count = await _dbConn.Table<Valid_Word>().Where(_word => _word.Word.ToLower() == word.ToLower()).CountAsync();

            return (count > 0);
        }

        public async Task SaveLeveProgress(Level_Progress progress)
        {
            await _dbConn.InsertAsync(progress);
        }

        public async Task<List<Level_Progress>> GetLevelProgress(int levelNo)
        {
            var levelProgress = await _dbConn.Table<Level_Progress>().Where(_level => _level.Level_No == levelNo).OrderBy(_l => _l.Row_No).ToListAsync();

            return levelProgress;
        }

        public async Task<List<Level_Progress>> GetAllLevelProgress()
        {
            var levelProgress = await _dbConn.Table<Level_Progress>().ToListAsync();

            return levelProgress;
        }



    }
}
