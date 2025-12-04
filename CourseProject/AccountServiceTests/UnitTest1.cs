using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.DTO_s;
using ServiceLayer.Models;
using ServiceLayer.Service;

namespace AccountServiceTests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task CreateForestPlotAsync_ValidDto_ReturnsTrue()
        {
            // Arrange
            // Создаём in-memory контекст базы данных
            var options = new DbContextOptionsBuilder<ForestryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            // Вызываем контекст БД и передаем в него параметры
            using var context = new ForestryContext(options);
            // Создаем экземпляр сервиса и передаем в него контекст БД
            var service = new ForestPlotService(context);
            // Создаем объекты DTO для создания записи в БД
            var createDto = new CreateForestPlotDto
            {
                PlotId = 1,
                UserId = 1,
                Compartment = 10,
                Subcompartment = 2,
                TreeComposition = new List<CreateTreesNumberDto>
            {
                new CreateTreesNumberDto { TreeTypeId = 1, Amount = 100 },
                new CreateTreesNumberDto { TreeTypeId = 2, Amount = 50 }
            }
            };
            // Act
            // Выполняем запрос на создание записи
            var result = await service.CreateForestPlotAsync(createDto);
            // Assert
            Assert.True(result);
            // Проверяем добавился ли участок
            var savedPlot = await context.ForestPlots
                .Include(fp => fp.TreesNumbers)
                .FirstOrDefaultAsync(fp => fp.PlotId == createDto.PlotId);
            // Проверяем данные на корректность 
            Assert.NotNull(savedPlot);
            Assert.Equal(createDto.UserId, savedPlot.UserId);
            Assert.Equal((byte)createDto.Compartment, savedPlot.Compartment);
            Assert.Equal((byte)createDto.Subcompartment, savedPlot.Subcompartment);
            Assert.Equal(2, savedPlot.TreesNumbers.Count);
            Assert.Contains(savedPlot.TreesNumbers, tn => tn.TreeTypeId == 1 && tn.Amount == 100);
            Assert.Contains(savedPlot.TreesNumbers, tn => tn.TreeTypeId == 2 && tn.Amount == 50);
        }
    }
}