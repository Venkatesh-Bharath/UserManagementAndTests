using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using System.Text;
using FluentAssertions;
using System.Threading.Tasks;
using UserManagement.Repository;
using UserManagement.Controllers;
using UserManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UserManagementTest.Controller
{
    public class UserControllerTest
    {
        private readonly IUserInterface userInterface;
        private readonly UserController userController;
        public UserControllerTest()
        {
            //set up dependencies
            this.userInterface = A.Fake<IUserInterface>();

            //sut ->System under test
            this.userController =new UserController(userInterface);
        }
        private static User CreateFakeUser() => A.Fake<User>();

        [Fact]
        public async void UserController_Create_ReturnCreated()
        {   
            //Arrange
            var user = CreateFakeUser();

            //Act
            A.CallTo(() => userInterface.CreateAsync(user)).Returns(true);
            var result = (CreatedAtActionResult)await userController.Create(user);

            //Assert
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();

            
        }

        [Fact]

        public async void UserController_GetUsers_ReaturnOK()
        {
            var users =A.Fake<List<User>>();
            users.Add(new User(){ name="Mahi",email="mahi@gmail.com"});

            A.CallTo(()=>userInterface.GetAllAsync()).Returns(users);
            var result = (OkObjectResult)await userController.GetUsers();

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Should().NotBeNull();
        }
        [Theory]
        [InlineData(1)]
        public async void UserController_GetUser_ReturnOk(int id)
        {
            var user = CreateFakeUser();
            user.name = "maheswari";
            user.email = "mahi@gmail.com";
            user.id = id;

            A.CallTo(()=>userInterface.GetByIdAsync(id)).Returns(user);
            var result=(OkObjectResult)await userController.GetUser(id);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Should().NotBeNull();
        }
        [Fact]
        public async void UserController_Delete_ReturnNoContent()
        {
            int userid = 1;

            A.CallTo(()=>userInterface.DeleteAsync(userid)).Returns(true);
            var result=(NoContentResult)await userController.Delete(userid);

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            result.Should().NotBeNull();
        }
    }
}
