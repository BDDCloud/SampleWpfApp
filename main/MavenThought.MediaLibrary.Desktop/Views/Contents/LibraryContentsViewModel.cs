using System.Collections.ObjectModel;
using System.Linq;
using MavenThought.Commons.Events;
using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Desktop.Events;
using MavenThought.MediaLibrary.Domain;
using Microsoft.Practices.ServiceLocation;

namespace MavenThought.MediaLibrary.Desktop.Views.Contents
{
    /// <summary>
    /// View model to show the contents
    /// </summary>
    public class LibraryContentsViewModel : AbstractNotifyPropertyChanged, IHandleEventsOfType<IMovieAdded>, IHandleEventsOfType<ISelectedMovieDeleted>
    {
        private readonly IMediaLibrary _mediaLibrary;
        private readonly IEventAggregator _eventAggregator;
        private string _contents;
        private ObservableCollection<MovieViewModel> _movies;
        private MovieViewModel _selectedMovie;

        public LibraryContentsViewModel()
            : this(ServiceLocator.Current.GetInstance<IMediaLibrary>(), ServiceLocator.Current.GetInstance<IEventAggregator>())
        {
        }

        public LibraryContentsViewModel(IMediaLibrary mediaLibrary, IEventAggregator eventAggregator)
        {
            _mediaLibrary = mediaLibrary;
            _eventAggregator = eventAggregator;
            _movies = new ObservableCollection<MovieViewModel>();
            this.Contents = "This are the contents of the library";
        }

        /// <summary>
        /// Gets or sets the contents
        /// </summary>
        public string Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                _contents = value;
                this.OnPropertyChanged(() => Contents);
            }
        }

        public ObservableCollection<MovieViewModel> Movies
        {
            get
            {
                return _movies;
            }
        }

        public MovieViewModel SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                if (value != null)
                {
                    _selectedMovie = value;
                    _eventAggregator.Raise<IMovieSelected>(
                        e => e.Movie = _mediaLibrary.Contents.First(m => m.Title == value.Title));
                }
            }
        }

        /// <summary>
        /// The movie was added
        /// </summary>
        /// <param name="event">Event to get the contents from</param>
        public void Handle(IMovieAdded @event)
        {
            this.Contents = string.Format("The movie {0} was added to the collection", @event.Movie.Title);
            _movies.Add(new MovieViewModel(@event.Movie));
        }

        public void Handle(ISelectedMovieDeleted @event)
        {
            _movies.Remove(SelectedMovie);
        }
    }
}