using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XGENO.Wordly.Models;

namespace XGENO.Wordly.Interfaces
{
    public interface IApiService
    {
        Task<List<Level>> GetWords(int fromWordID = 0);
        Task<List<Valid_Word>> GetValidWords();

    }
}
