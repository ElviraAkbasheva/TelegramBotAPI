using System.Net.Http.Json;



namespace TelegramBotAPI
{
    public class CatFactServiceException : Exception
    {
        public CatFactServiceException(string message) : base(message) { }
    }

    internal class CatFactService
    {  
        const string https_adress = "https://catfact.ninja/fact";
        record CatFactDto(string Fact, int Length);
        public async Task<string> GetCatFactAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var catFact = await client.GetFromJsonAsync<CatFactDto>(https_adress, cancellationToken);
                    if (catFact != null)
                    {
                        return catFact.Fact;
                    }
                    else
                    {
                        throw new CatFactServiceException("Не удалось получить факт о кошках");
                    }
                }
            }
            catch (Exception)
            {
                throw new CatFactServiceException($"Ресурс {https_adress} сейчас недоступен");
            }
        }
    }
}

