using PhonebookWebApplication.Dtos;

namespace PhonebookWebApplication.Service
{
    public interface IUserRepository
    {
        UserResponse SaveUser(UserRequest request);
        GetUserDetailsResponse GetAllUsers();
        UpdateUserResponse UpdateUser(UpdateUserRequest request);
        DeleteResponse DeleteUser(int id);
        GetUserDetailsResponse GetUserByName(string name);
        GetUserInfo GetUserId(int id);
    }
}
