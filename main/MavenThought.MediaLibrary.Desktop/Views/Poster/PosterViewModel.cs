using System.IO;
using System.Net;
using System.Threading.Tasks;
using Bootstrap;
using Castle.Windsor;
using MavenThought.Commons.Events;
using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Desktop.Events;
using MavenThought.MediaLibrary.Domain;
using Microsoft.Practices.ServiceLocation;

namespace MavenThought.MediaLibrary.Desktop.Views.Poster
{
    /// <summary>
    /// View model for posters
    /// </summary>
    public class PosterViewModel : AbstractNotifyPropertyChanged, IHandleEventsOfType<IMovieAdded>, IHandleEventsOfType<IMovieSelected>
    {
        private readonly IMoviePosterService _moviePosterService;
        private readonly IFileDownloader _fileDownloader;
        private bool _isBusy;

        private const string BlankPoster = "images/_blank.bmp";


        public PosterViewModel()
            :this(ServiceLocator.Current.GetInstance<IMoviePosterService>(), ServiceLocator.Current.GetInstance<IFileDownloader>())
        {
        }

        public PosterViewModel(IMoviePosterService moviePosterService, IFileDownloader fileDownloader)
        {
            _moviePosterService = moviePosterService;
            _fileDownloader = fileDownloader;
            SetPoster(BlankPoster);
        }

        /// <summary>
        /// Gets or sets the poster of the movie
        /// </summary>
        public string Poster { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { 
                _isBusy = value;
                OnPropertyChanged(() => IsBusy);
                OnPropertyChanged(() => IsNotBusy);
            }
        }

        public bool IsNotBusy
        {
            get { return !IsBusy; }
        }

        /// <summary>
        /// Find the poster for the movie
        /// </summary>
        /// <param name="event"></param>
        public void Handle(IMovieAdded @event)
        {
            UpdatePoster(@event.Movie);
        }

        public void Handle(IMovieSelected @event)
        {
            UpdatePoster(@event.Movie);
        }

        private void UpdatePoster(IMovie movie)
        {
            IsBusy = true;
            try
            {
                var title = string.Format("images/{0}.jpg", movie
                                                          .Title
                                                          .Trim()
                                                          .ToLower()
                                                          .Replace(" ", string.Empty));

                if (!File.Exists(title))
                {
                    title = RetrievePosterAsync(movie.Title);
                }

                SetPoster(title);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private string RetrievePosterAsync(string movieTitle)
        {
            var moviePoster = _moviePosterService.GetMoviePoster(movieTitle);
            if (moviePoster.MovieTitle.Equals("N/A"))
            {
                return BlankPoster;
            }
            var title = string.Format("images/{0}.jpg", movieTitle.Trim().ToLower().Replace(" ", string.Empty));
            _fileDownloader.DownloadFile(moviePoster.Url, title);
            return title;
        }

        private void SetPoster(string title)
        {
            this.Poster = new FileInfo(title).FullName;
            this.OnPropertyChanged(() => Poster);
        }
    }

   
}