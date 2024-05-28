using PhonebookWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using PhonebookWebApplication.Dtos;

namespace XunitTestUtility
{
    public class PhonebookServiceTests
    {
        private readonly PhonebookService _phonebookService;
        private readonly Mock<IUserRepository> _mockRepo;

        public PhonebookServiceTests()
        {
            _mockRepo=new Mock<IUserRepository>();
            _phonebookService=new PhonebookService( _mockRepo.Object );   
        }

        [Fact]
        public void GetUserByName_When_Data_Exist()
        {
            string name = "david";
            GetUserDetailsResponse response = new GetUserDetailsResponse();
            _mockRepo.Setup(repo => repo.GetUserByName(name)).Returns(response);

            //Act
            var result= _phonebookService.GetUserByName(name);

            Assert.Equal(result, response);
        }

        [Fact]
        public void GetUserByName_When_Data_Not_Exist()
        {
            string name = "david";
            GetUserDetailsResponse response = null;
            _mockRepo.Setup(repo => repo.GetUserByName(name)).Returns(response);

            //Act
            var result = _phonebookService.GetUserByName(name);

            Assert.Equal(result, response);
        }

        [Fact]
        public void DeleteUser_When_Data_Exist()
        {
            int id = 1;
            DeleteResponse response = new DeleteResponse()
            {
                Id = id,
                Message=$"User deleted successfully with userId {id}"
            };
            _mockRepo.Setup(repo => repo.DeleteUser(id)).Returns(response);

            //Act
            var result = _phonebookService.DeleteUser(id);

            Assert.Equal(result, response);
        }

        [Fact]
        public void DeleteUser_When_Data_Not_Exist()
        {
            int id = 1;
            DeleteResponse response = new DeleteResponse()
            {
                Id = 0,
                Message = $"User not found with userId {id}"
            };
            _mockRepo.Setup(repo => repo.DeleteUser(id)).Returns(response);

            //Act
            var result = _phonebookService.DeleteUser(id);

            Assert.Equal(result, response);
        }

    }
}
