namespace XGENO.Vocably.Services;

public interface IApiService
{
    Task<List<Level>> GetWords(int fromWordID = 0);
    Task<List<Valid_Word>> GetValidWords();
}