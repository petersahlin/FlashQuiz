using System.Net.Http.Json;
using TriviaApp.Models;

namespace TriviaApp.Services
{
    public interface ITriviaService
    {
        public Task<List<TriviaQuestion>> GetTriviaQuestions();
    }


    public class TriviaService : ITriviaService
    {
        HttpClient _httpClient;

        List<TriviaQuestion> _questionList = new();

        public TriviaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<TriviaQuestion>> GetTriviaQuestions()
        {
            if (_questionList?.Count > 0) 
            { 
                return _questionList;
            }
            
            var url = "https://opentdb.com/api.php?amount=10";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var questionsList = await response.Content.ReadFromJsonAsync<List<TriviaQuestion>>();


            }

            return _questionList;



        }
    }
}
