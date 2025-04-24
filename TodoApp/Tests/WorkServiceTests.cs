using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Model;
using TodoApp.Repositories;
using TodoApp.Services;
using Xunit;

namespace TodoApp.Tests
{
    public class WorkServiceTests
    {
        private readonly Mock<IWorkRepository> _mockRepo;
        private readonly WorkService _service;

        public WorkServiceTests()
        {
            _mockRepo = new Mock<IWorkRepository>();
            _service = new WorkService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllWorksAsync_ReturnsWorkList()
        {
            // Arrange
            var fakeWorks = new List<Work>
            {
                new Work { id = 1, name = "Test1", description = "Desc1", status = "not started" },
                new Work { id = 2, name = "Test2", description = "Desc2", status = "in progress" }
            };

            _mockRepo.Setup(r => r.GetAllWorksAsync()).ReturnsAsync(fakeWorks);

            // Act
            var result = await _service.GetAllWorksAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetWorkByIdAsync_ReturnsWork()
        {
            // Arrange
            var work = new Work { id = 1, name = "Test", description = "Testing", status = "completed" };

            _mockRepo.Setup(r => r.GetWorkByIdAsync(1)).ReturnsAsync(work);

            // Act
            var result = await _service.GetWorkByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.name);
        }

        [Fact]
        public async Task AddWorkAsync_CallsRepository()
        {
            // Arrange
            var newWork = new Work { name = "New", description = "Create", status = "not started" };

            // Act
            await _service.AddWorkAsync(newWork);

            // Assert
            _mockRepo.Verify(r => r.AddWorkAsync(newWork), Times.Once);
        }

        [Fact]
        public async Task UpdateWorkAsync_ReturnsTrueIfSuccessful()
        {
            // Arrange
            var updatedWork = new Work { id = 1, name = "Updated", description = "Updated Desc", status = "completed" };

            _mockRepo.Setup(r => r.UpdateWorkAsync(updatedWork)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateWorkAsync(updatedWork);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteWorkAsync_ReturnsTrueIfSuccessful()
        {
            // Arrange
            int id = 1;
            _mockRepo.Setup(r => r.DeleteWorkAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteWorkAsync(id);

            // Assert
            Assert.True(result);
        }
    }
}
