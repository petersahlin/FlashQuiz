using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
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

            try
            {
                var url = "https://opentdb.com/api.php?amount=10&type=multiple";

                var response = await _httpClient.GetFromJsonAsync<TriviaResponse>(url);

                if (response?.Results == null || response.Results.Count == 0)
                {
                    throw new Exception("No trivia questions found.");
                }

                _questionList = response.Results;
                return _questionList;


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }

            return _questionList;


        }
    }
}
