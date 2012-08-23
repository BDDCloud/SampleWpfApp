using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Desktop.Views.Contents
{
    public class MovieViewModel
    {
        public MovieViewModel(IMovie movie)
        {
            Title = movie.Title;
        }

        public string Title { get; private set; }
    }
}