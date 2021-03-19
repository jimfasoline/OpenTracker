using NSubstitute;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo
{
    public class AddConnectionTests
    {
        private readonly IConnection _connection = Substitute.For<IConnection>();
        private readonly IConnectionCollection _connections = new ConnectionCollection(
            Substitute.For<ILocationDictionary>(),
            (location1, location2) => new Connection(location1, location2));

        private readonly AddConnection _sut;

        public AddConnectionTests()
        {
            _sut = new AddConnection(_connections, _connection);
        }
        
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenConnectionCollectionDoesNotContainConnection()
        {
            Assert.True(_sut.CanExecute());
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenConnectionCollectionContainsConnection()
        {
            _connections.Add(_connection);
            
            Assert.False(_sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldAddConnectionToConnectionCollection()
        {
            _sut.ExecuteDo();

            Assert.Contains(_connection, _connections);
        }

        [Fact]
        public void ExecuteUndo_ShouldRemoveConnectionFromConnectionCollection()
        {
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.DoesNotContain(_connection, _connections);
        }
    }
}