namespace XGENO.Vocably.Services;

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

    public async Task<List<Level>> GetWords(int fromWordID = 0) =>
        await GetAsync<List<Level>>($"words/list/{fromWordID}", Connectivity.NetworkAccess, 30 * 24); //Cached for 30 days

    public async Task<List<Valid_Word>> GetValidWords() =>
        await GetAsync<List<Valid_Word>>($"words/valid", Connectivity.NetworkAccess, 30 * 24); //Cached for 30 days

}