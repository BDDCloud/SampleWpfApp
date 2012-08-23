namespace MavenThought.MediaLibrary.Domain
{
    public interface IMoviePoster
    {
        string MovieTitle { get; set; }
        string Url { get; set; }
    }
}