using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Desktop.Events
{
    public interface IMovieSelected : IEvent
    {
        /// <summary>
        /// Gets or sets the movie added
        /// </summary>
        IMovie Movie { get; set; }
    }
}