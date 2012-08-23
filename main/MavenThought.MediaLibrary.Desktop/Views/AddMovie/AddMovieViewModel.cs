using System;
using System.Windows.Input;
using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Core;
using MavenThought.MediaLibrary.Desktop.Events;
using MavenThought.MediaLibrary.Desktop.netfx.System.Windows.Input;
using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Desktop.Views.AddMovie
{
    /// <summary>
    /// View model to add movies
    /// </summary>
    public class AddMovieViewModel
    {
        public AddMovieViewModel(IMediaLibrary library, IEventAggregator eventAggregator)
        {
            this.Add = new DelegateCommand(() => AddMovie(library, eventAggregator));
            this.Delete = new DelegateCommand(() => DeleteMovie(library, eventAggregator));
            
        }

        void DeleteMovie(IMediaLibrary library, IEventAggregator eventAggregator)
        {
            eventAggregator.Raise<ISelectedMovieDeleted>(e => {});
        }

        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }
        public string Title { get; set; }

        /// <summary>
        /// Adds a movie and raises an event
        /// </summary>
        /// <param name="library"></param>
        /// <param name="eventAggregator"></param>
        private void AddMovie(IMediaLibrary library, IEventAggregator eventAggregator)
        {
            var movie = new Movie
                            {
                                Title = this.Title,
                                ReleaseDate = DateTime.Now
                            };

            library.Add(movie);

            eventAggregator.Raise<IMovieAdded>(evt => evt.Movie = movie);
        }

    }
}