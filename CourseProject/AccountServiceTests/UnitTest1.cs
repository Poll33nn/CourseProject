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
        public async Task LoginUser_Test()
        {
            // === Arrange: Создаём in-memory контекст прямо в тесте ===

            // Уникальное имя для изоляции (важно при параллельных тестах)
            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ForestryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            await using var context = new ForestryContext(options);
            context.Database.EnsureCreated(); // Инициализируем структуру БД

            // Подготавливаем тестовые данные
            var role = new Role { RoleId = 1, Name = "Лесничий" };
            var user = new User
            {
                UserId = 1,
                Login = "ivanov",
                PasswordHash = "AQIDBA==",
                Name = "Иван",
                LastName = "Иванов",
                Patronymic = "Иванович",
                Role = role,
                RoleId = role.RoleId
            };

            context.Roles.Add(role);
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Создаём тестируемый сервис
            var accountService = new AccountService(context);

            // === Act ===
            var loginDto = new LoginDto
            {
                Login = "ivanov",
                PasswordHash = "AQIDBA=="
            };

            var result = await accountService.LoginUser(loginDto);

            // === Assert ===
            Assert.NotNull(result);
            Assert.Equal("Иванов Иван Иванович", result.FullName);
            Assert.Equal("Лесничий", result.RoleName);
        }
    }
}