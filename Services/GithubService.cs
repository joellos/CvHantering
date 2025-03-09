using CvHantering.DTOs.GitHubDTO;
using System.Text.Json;

namespace CvHantering.Services
{
    public class GithubService
    {
        private readonly HttpClient HttpClient;

        public GithubService(HttpClient _httpClient)
        {
            HttpClient = _httpClient;
        }

        public async Task<List<GitHubDTOs>> GetGitHubRepos(string username)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/users/{username}/repos");
            request.Headers.Add("User-Agent", "CvHantering");
            var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new KeyNotFoundException("User not found or request failed.");
            }
            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<List<GitHubDTOs>>(json, options);

            if (data == null)
            {
                throw new KeyNotFoundException("No repositories found.");
            }

            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item.Description == null)
                    {
                        item.Description = "No description";
                    }
                    if (item.Language == null)
                    {
                        item.Language = "No language";
                    }

                   
                }
            }
            return data;
        }
    }
}
