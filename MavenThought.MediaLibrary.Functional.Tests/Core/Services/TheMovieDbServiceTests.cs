using MavenThought.MediaLibrary.Core.Services;
using MbUnit.Framework;

namespace MavenThought.MediaLibrary.Functional.Tests.Core.Gateways
{
    [TestFixture]
    public class TheMovieDbServiceTests
    {
        [Test]
        public void GetMoviePoster_ShouldReturnMoviePoster_WhenMovieTitleIsFound()
        {
            var service = new TheMovieDbService();
            var poster = service.GetMoviePoster("Army of Darkness");
            Assert.IsNotNull(poster);
            Assert.AreEqual("Amry of Darkness", poster.MovieTitle);
        }

        [Test]
        public void GetMoviePoster_ShouldReturnDefaultPoster_WhenMovieTitleIsNotFound()
        {
            var service = new TheMovieDbService();
            var poster = service.GetMoviePoster("dsfasdfadsfads");
            Assert.IsNotNull(poster);
            Assert.AreEqual("N/A", poster.MovieTitle);
        }


    }
}