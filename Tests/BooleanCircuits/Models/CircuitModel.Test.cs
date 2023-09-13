using System.Text.Json;
using BooleanCircuits.Models;
using Xunit;

namespace Tests.BooleanCircuits.Models
{
    public class CircuitModelTest
    {
        [Fact]
        public void Validate_EmptyModel_ThrowsException()
        {
            CircuitModel model = new CircuitModel();
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of circuit cannot be empty!", e.Message);
        }
        
        [Fact]
        public void Validate_NoName_ThrowsException()
        {
            CircuitModel model = new CircuitModel
            {
                Id = "Id"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Name of circuit cannot be empty!", e.Message);
        }
        
        [Fact]
        public void Validate_NoId_ThrowsException()
        {
            CircuitModel model = new CircuitModel
            {
                Name = "Name"
            };
            JsonException e = Assert.Throws<JsonException>(() => model.Validate());
            Assert.Equal("Id of circuit cannot be empty!", e.Message);
        }
    }
}
