namespace MavenThought.MediaLibrary.Domain
{
    public interface IMoviePosterService
    {
        IMoviePoster GetMoviePoster(string movieTitle);
    }
}