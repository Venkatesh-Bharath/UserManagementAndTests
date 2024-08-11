using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Repository;

namespace UserManagementTest.Repository
{
    public class UserRepositoryTest
    {
        private readonly UserRepository userRepository;
        public UserRepositoryTest() {
          var userDbContext=GetDatabaseContext().Result;
          userRepository=new UserRepository(userDbContext);
        }
        private async Task<UserDBContext> GetDatabaseContext()
        {
            // Load the configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the connection string from the configuration
            var connectionString = configuration.GetConnectionString("UserCS");

            var options = new DbContextOptionsBuilder<UserDBContext>()
                .UseSqlServer(connectionString).Options;
            var userDbContest=new UserDBContext(options);
            userDbContest.Database.EnsureCreated();
            if(!await userDbContest.Users.AnyAsync())
            {
                userDbContest.Users.Add(new User() {  email = "mahi@gmail.com", name = "mahi" });
                await userDbContest.SaveChangesAsync();
            }
            return userDbContest;
        }

      
        [Fact]
        public async void UserRepository_Create_Returntrue()
        {
            // Arrange
            var user = new User
            {
                email = "bsk@example.com",
                name = "bsk"
            };

            // Act
            var result = await userRepository.CreateAsync(user);

            //Assert
            result.Should().BeTrue();

        }
        [Fact]
        public async void UserRepository_GetUsers_ReturnUsers()
        {
            // Arrange


            // Act
            var result = await userRepository.GetAllAsync();

            //Assert
            result.Should().AllBeOfType<User>();

        }

        [Theory]
        [InlineData(2)]
        public async void UserRepository_GetUser_ReturnUser(int id)
        {
            // Arrange


            // Act
            var result = await userRepository.GetByIdAsync(id);

            //Assert
            result.Should().BeOfType<User>();

        }
        [Theory]
        [InlineData(2)]
        public async void UserRepository_UpdateUser_ReturnTrue(int id)
        {
            // Arrange


            // Act
            var user = await userRepository.GetByIdAsync(id);
            user.name = "Maheswari";
            var result= await userRepository.UpdateAsync(user);

            //Assert
            result.Should().BeTrue();

        }
        /*[Fact]
        public async void UserRepository_DeleteUser_ReturnTrue()
        {
            // Arrange     
            int id = 1;

            // Act
            var result = await userRepository.DeleteAsync(id);

            //Assert
            result.Should().BeTrue();

        }*/
    }
}
