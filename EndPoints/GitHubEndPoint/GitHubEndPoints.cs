using CvHantering.Services;

namespace CvHantering.EndPoints.GitHubEndPoint
{
    public class GitHubEndPoints
    {

        public static async void RegisterGitHubEndPoints(WebApplication app)
        {
            app.MapGet("/github/{username}", async (GithubService githubService, string username) =>
            {
                try
                {
                    var gitHubUser = await githubService.GetGitHubRepos(username);
                    return Results.Ok(gitHubUser);
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound("User not found");
                }
            });
        }

    }
}

