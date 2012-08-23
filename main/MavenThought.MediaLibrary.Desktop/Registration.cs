using Bootstrap.WindsorExtension;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MavenThought.Commons.WPF.Events;
using MavenThought.MediaLibrary.Core;
using MavenThought.MediaLibrary.Core.Services;

using MavenThought.MediaLibrary.Desktop.Views.AddMovie;
using MavenThought.MediaLibrary.Desktop.Views.Contents;
using MavenThought.MediaLibrary.Desktop.Views.Poster;
using MavenThought.MediaLibrary.Desktop.Views.Reviews;
using MavenThought.MediaLibrary.Domain;
using Microsoft.Practices.ServiceLocation;
using AddMovieView = MavenThought.MediaLibrary.Desktop.Views.AddMovie.AddMovieView;
using LibraryContentsView = MavenThought.MediaLibrary.Desktop.Views.Contents.LibraryContentsView;
using PosterView = MavenThought.MediaLibrary.Desktop.Views.Poster.PosterView;
using ReviewsView = MavenThought.MediaLibrary.Desktop.Views.Reviews.ReviewsView;

namespace MavenThought.MediaLibrary.Desktop
{
    public class Registration : IWindsorRegistration
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(
                Component
                    .For<IFileDownloader>()
                    .ImplementedBy<FileDownloader>(),
                Component
                    .For<IMoviePosterService>()
                    .ImplementedBy<TheMovieDbService>(),
                Component
                    .For<IMediaLibrary>()
                    .ImplementedBy<MovieLibrary>()
                    .LifeStyle.Is(LifestyleType.Singleton),
                Component
                    .For<IEventAggregator>()
                    .ImplementedBy<EventAggregator>(),
                Component
                    .For<IWindsorContainer>()
                    .Instance(container));

            RegisterViews(container);

            RegisterCritics(container);
        }

        private static void RegisterCritics(IWindsorContainer container)
        {
            var descriptor = AllTypes.FromThisAssembly().BasedOn<IMovieCritic>();

            container.Register(descriptor);
        }

        private static void RegisterViews(IWindsorContainer container)
        {
            container.Register(
                // Add movie
                Component
                    .For<AddMovieViewModel>()
                    .Named("AddViewVM"),
                Component
                    .For<ILibraryView>()
                    .ImplementedBy<AddMovieView>()
                    .Parameters(Parameter
                                    .ForKey("DataContext")
                                    .Eq("${AddViewVM}")),
                // Reviews
                Component
                    .For<ReviewsViewModel>()
                    .Named("ReviewsVM"),
                Component
                    .For<ILibraryView>()
                    .ImplementedBy<ReviewsView>()
                    .Parameters(Parameter
                                    .ForKey("DataContext")
                                    .Eq("${ReviewsVM}")),
                // Poster
                Component
                    .For<PosterViewModel>()
                    .Named("PosterVM"),
                Component
                    .For<ILibraryView>()
                    .ImplementedBy<PosterView>()
                    .Parameters(Parameter
                                    .ForKey("DataContext")
                                    .Eq("${PosterVM}")),
                // Library contents
                Component
                    .For<LibraryContentsViewModel>()
                    .Named("ContentsVM"),
                Component
                    .For<ILibraryView>()
                    .ImplementedBy<LibraryContentsView>()
                    .Parameters(Parameter
                                    .ForKey("DataContext")
                                    .Eq("${ContentsVM}"))


                );
        }
    }
}