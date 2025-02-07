using System.Net.Http.Json;


namespace TelegramBotAPI
{
    internal class CatFact
    {  
        const string https_adress = "https://catfact.ninja/fact";
        record CatFactDto(string Fact, int Length);
        public async Task<string> GetCatFactAsync(CancellationToken cancellationToken)
        {
            try
            {
                var client = new HttpClient();
                var catFact = await client.GetFromJsonAsync<CatFactDto>(https_adress, cancellationToken);
                if (catFact != null)
                {
                    return catFact.Fact;
                }
                else
                {
                    return "Не удалось получить факта о кошках";
                }
            }
            catch
            {
                return $"Ресурс {https_adress} сейчас недоступен";
            }
        }
    }
}

