using System.Text.Json;
using BooleanCircuits.Models;
using Xunit;

namespace Tests.BooleanCircuits.Models
{
    public class NodeModelTest
    {
        [Fact]
        public void Validate_NoName_ThrowsException()
        {
            NodeModel model = new NodeModel();
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of node cannot be empty!", e.Message);
        }
    }
}
