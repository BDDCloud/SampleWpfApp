using System;
using System.ComponentModel;
using System.Windows.Input;
using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Core;
using MavenThought.MediaLibrary.Desktop.Events;
using MavenThought.MediaLibrary.Desktop.netfx.System.Windows.Input;
using MavenThought.MediaLibrary.Domain;

namespace MavenThought.MediaLibrary.Desktop.Views.AddMovie
{
    public class AddMovieViewModel: INotifyPropertyChanged 
    {
        public bool DeleteDialogOpen { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }
        public ICommand DeleteDialog { get; private set; }
        public ICommand CancelDelete { get; private set; }
        public string Title { get; set; }

        public AddMovieViewModel(IMediaLibrary library, IEventAggregator eventAggregator)
        {
            Add = new DelegateCommand(() => AddMovie(library, eventAggregator));
            Delete = new DelegateCommand(() => DeleteMovie(library, eventAggregator));
            DeleteDialog = new DelegateCommand(DeleteDialogBox);
            CancelDelete = new DelegateCommand(CancelDeleteDialog);
            DeleteDialogOpen = false;
        }

        private void DeleteMovie(IMediaLibrary library, IEventAggregator eventAggregator)
        {
            eventAggregator.Raise<ISelectedMovieDeleted>(e => {});
            DeleteDialogOpen = false;
            OnPropertyChanged("DeleteDialogOpen");
        }

        private void DeleteDialogBox()
        {
            DeleteDialogOpen = true;
            OnPropertyChanged("DeleteDialogOpen");
        }

        private void CancelDeleteDialog()
        {
            DeleteDialogOpen = false;
            OnPropertyChanged("DeleteDialogOpen");
        }

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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}