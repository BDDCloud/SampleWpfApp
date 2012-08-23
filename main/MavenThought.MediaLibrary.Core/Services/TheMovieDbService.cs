using System.Linq;
using MavenThought.MediaLibrary.Domain;
using WatTmdb.V3;

namespace MavenThought.MediaLibrary.Core.Services
{
    public class TheMovieDbService : IMoviePosterService
    {
        private readonly Tmdb _tmdbService;
        private string _baseUrl;

        public TheMovieDbService()
        {
            _tmdbService = new Tmdb("b2f505a89784ca773dae669293fa024f", "en");
        }

        public IMoviePoster GetMoviePoster(string movieTitle)
        {
            if (string.IsNullOrEmpty(_baseUrl))
            {
                _baseUrl = _tmdbService.GetConfiguration().images.base_url;
            }

            var searchResults = _tmdbService.SearchMovie(movieTitle, 1);
            var movie = searchResults.results.FirstOrDefault();
            if (movie == null) return null;
            var images = _tmdbService.GetMovieImages(movie.id);
            var poster = images.posters.FirstOrDefault();
            if (poster == null)
            {
                return new TheMovieDbPoster() {MovieTitle = "N/A"};
            }
            return new TheMovieDbPoster()
                {
                    MovieTitle = movieTitle,
                    Url = string.Format("{0}w{1}{2}", _baseUrl, poster.width, poster.file_path)
                };
        }
    }

    public class TheMovieDbPoster : IMoviePoster
    {
        public string MovieTitle { get; set; }
        public string Url { get; set; }
    }
}