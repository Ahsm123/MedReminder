using Dapper;
using MedReminder.Dal.Dao;
using MedReminder.Dal.Interfaces;
using MedReminder.DAL.Models;
using Moq;
using Moq.Dapper;
using System.Data;

namespace MedReminder.Test.UnitTests.Dal
{
    [TestFixture]
    public class UserDaoUnitTests
    {
        private Mock<IDbConnection> _mockConnection;
        private Mock<IConnectionFactory> _mockConnectionFactory;
        private UserDao _userDao;

        [SetUp]
        public void SetUp()
        {
            _mockConnection = new Mock<IDbConnection>();
            _mockConnectionFactory = new Mock<IConnectionFactory>();

            _mockConnectionFactory.Setup(factory => factory.CreateConnection()).Returns(_mockConnection.Object);

            _userDao = new UserDao(_mockConnectionFactory.Object);
        }

        [Test]
        public async Task GetByIdAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var expectedUser = new User
            {
                Id = 1,
                FirstName = "Bruce",
                LastName = "Wayne",
                Email = "bruce.wayne@example.com",
                PasswordHash = "password",
                CreatedAt = DateTime.Now
            };

            _mockConnection
                .SetupDapperAsync(c =>
                    c.QueryFirstOrDefaultAsync<User>(
                        It.IsAny<string>(),
                        It.IsAny<object>(),
                        null,
                        null,
                        null))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userDao.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Email, result.Email);
        }

        [Test]
        public async Task GetUserByIdAsync_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var expectedUser = new User
            {
                Id = 1,
                FirstName = "Bruce",
                LastName = "Wayne",
                Email = "bruce.wayne@example.com",
                PasswordHash = "password",
                CreatedAt = DateTime.Now
            };

            _mockConnection
                .SetupDapperAsync(c =>
                    c.QueryFirstOrDefaultAsync<User>(
                        It.IsAny<string>(),
                        It.IsAny<object>(),
                        null,
                        null,
                        null))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userDao.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Email, result.Email);
        }

        [Test]
        public async Task CreateUserAsync_ShouldReturnGeneratedId()
        {
            throw new NotImplementedException();
            //TODO: Moq.Dapper does not support ExecuteScalarAsync
        }

    }
}
