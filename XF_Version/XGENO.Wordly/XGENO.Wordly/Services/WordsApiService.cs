using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using XGENO.Mobile.Framework.Services;
using XGENO.Wordly.Interfaces;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.Services
{
    public class WordsApiService : RestServiceBase, IApiService
    {
        private string client_ID = "";
        private string client_Secret = "";
        private string client_Token = "";

        public WordsApiService(string apiBaseUrl) : base(apiBaseUrl)
        {
            AddHttpHeader("vocably_client_id", client_ID);
            AddHttpHeader("vocably_client_secret", client_Secret);
            AddHttpHeader("vocably_client_token", client_Token);
        }

        public async Task<List<Level>> GetWords(int fromWordID = 0)
        {
            var result = await GetAsync<List<Level>>($"words/list/{fromWordID}", Connectivity.NetworkAccess, 30 * 24); //Cached for 30 days

            return result;
        }

        public async Task<List<Valid_Word>> GetValidWords()
        {
            var result = await GetAsync<List<Valid_Word>>($"words/valid", Connectivity.NetworkAccess, 30 * 24); //Cached for 30 days

            return result;
        }

    }
}
