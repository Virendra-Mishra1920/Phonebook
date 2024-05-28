using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PhonebookWebApplication.Data;
using PhonebookWebApplication.Dtos;
using PhonebookWebApplication.Models;
using System.Xml.Linq;

namespace PhonebookWebApplication.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;  
        }
        public UserResponse SaveUser(UserRequest request)
        {
            var response = new UserResponse();
            try
            {
                var firstname = new Npgsql.NpgsqlParameter("FirstName", NpgsqlTypes.NpgsqlDbType.Varchar);
                firstname.Value = request.FirstName;

                var lastname = new Npgsql.NpgsqlParameter("LastName", NpgsqlTypes.NpgsqlDbType.Varchar);
                lastname.Value = request.LastName;

                var email = new Npgsql.NpgsqlParameter("Email", NpgsqlTypes.NpgsqlDbType.Varchar);
                email.Value = request.Email;

                var phone = new Npgsql.NpgsqlParameter("phone", NpgsqlTypes.NpgsqlDbType.Varchar);
                phone.Value = request.Phone;

                var sql = "CALL \"Insert_User\"(@FirstName,@LastName,@Email,@Phone)";
                _userDbContext.Database.ExecuteSqlRaw(sql,firstname,lastname,email,phone);
                response.Message = "Record inserted successfully";
                
            }
            catch (Exception ex)
            {

                response.Message = "Record not inserted!";
            }
           
            return response; 
        }

        public List<UserResponse> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public DeleteResponse DeleteUser(int id)
        {
            var response = new DeleteResponse();
            try
            {
                var _user = _userDbContext.Users.FirstOrDefault(x => x.Id == id);
                if (_user == null)
                {
                    response.Message = $"user not found with userId {id}";
                    return response;
                }
                using (var connection=_userDbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "delete_user";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        var userIdParam = new Npgsql.NpgsqlParameter("@p_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
                        userIdParam.Value = id;
                        var deletedUserIdParam = new Npgsql.NpgsqlParameter("@p_deleted_user_id", NpgsqlTypes.NpgsqlDbType.Integer);
                        deletedUserIdParam.Direction = System.Data.ParameterDirection.Output;
                        command.Parameters.Add(userIdParam);
                        command.Parameters.Add(deletedUserIdParam);
                        command.ExecuteNonQuery();
                        response.Id = (int)deletedUserIdParam.Value;
                        response.Message = $"user deleted successfully with userId {id}";

                    }

                }
            }
            catch (Exception ex)
            {
                response.Message = $"user not found with userId {id}";
            }
            return response;

        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest  user)
        {
            var response = new UpdateUserResponse();
            try
            {
                var _user = _userDbContext.Users.FirstOrDefault(x => x.Id == user.Id);
                if (_user == null)
                {
                    response.Message = $"user not found with userId {user.Id}";
                    return response;
                }
                using (var connection = _userDbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "update_user";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        var userIdParam = new Npgsql.NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Integer);
                        userIdParam.Value = user.Id;

                        var firstNameParam = new Npgsql.NpgsqlParameter("@first_name", NpgsqlTypes.NpgsqlDbType.Varchar);
                        firstNameParam.Value = user.FirstName;

                        var lastNameParam = new Npgsql.NpgsqlParameter("@last_name", NpgsqlTypes.NpgsqlDbType.Varchar);
                        lastNameParam.Value = user.LastName;

                        var emailParam = new Npgsql.NpgsqlParameter("@email", NpgsqlTypes.NpgsqlDbType.Varchar);
                        emailParam.Value = user.Email;

                        var phoneParam = new Npgsql.NpgsqlParameter("@phone", NpgsqlTypes.NpgsqlDbType.Varchar);
                        phoneParam.Value = user.Phone;


                        command.Parameters.Add(userIdParam);
                        command.Parameters.Add(firstNameParam);

                        command.Parameters.Add(lastNameParam);
                        command.Parameters.Add(emailParam);
                        command.Parameters.Add(phoneParam);
                        command.ExecuteNonQuery();
                        response.Id = user.Id;
                        response.Message = $"user updated successfully with userId {user.Id}";

                    }

                }
            }
            catch (Exception ex)
            {

                response.Message = $"user not found with userId {user.Id}";
            }
            return response;

        }

        public GetUserDetailsResponse GetUserByName(string name)
        {
            var response = new GetUserDetailsResponse();
            List<User> users = new List<User>();
            try
            {
                using (var connection = _userDbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "search_user_by_name";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        var nameParam = new Npgsql.NpgsqlParameter("@p_name", NpgsqlTypes.NpgsqlDbType.Varchar)
                        {
                            Value = name
                        };

                        command.Parameters.Add(nameParam);
                        command.ExecuteNonQuery();
                        // Fetch the results from the temporary table

                        command.CommandText = "SELECT \"Id\" ,\"FirstName\", \"LastName\", \"Email\", \"Phone\" FROM temp_users";
                        command.CommandType = System.Data.CommandType.Text;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                };

                                users.Add(user); 
                            }
                        }
                    }
                }

                if (users.Count == 0)
                {
                    response.Message = $"User not found with username {name}";
                    
                }

                response.Users = users;
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
             
        }

        public GetUserInfo GetUserId(int id)
        {
            var response = new GetUserInfo();
            User _user = new User();
            try
            {
                using (var connection = _userDbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "get_user_by_id";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        var nameParam = new Npgsql.NpgsqlParameter("@p_id", NpgsqlTypes.NpgsqlDbType.Integer)
                        {
                            Value = id
                        };

                        command.Parameters.Add(nameParam);
                        command.ExecuteNonQuery();
                        // Fetch the results from the temporary table

                        command.CommandText = "SELECT \"Id\" ,\"FirstName\", \"LastName\", \"Email\", \"Phone\" FROM temp_users";
                        command.CommandType = System.Data.CommandType.Text;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                };
                                response.User = user;

                            }
                        }
                    }
                }

                if (response.User == null)
                {
                    response.Message = $"User not found with userId {id}";
                }

                
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

        GetUserDetailsResponse IUserRepository.GetAllUsers()
        {
            var response = new GetUserDetailsResponse();
            List<User> users = new List<User>();
            try
            {
                using (var connection = _userDbContext.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "get_all_users";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        command.ExecuteNonQuery();
                        // Fetch the results from the temporary table

                        command.CommandText = "SELECT \"Id\" ,\"FirstName\", \"LastName\", \"Email\", \"Phone\" FROM temp_users";
                        command.CommandType = System.Data.CommandType.Text;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Phone = reader.GetString(reader.GetOrdinal("Phone"))
                                };

                                users.Add(user);
                            }
                        }
                    }
                }

                if (users.Count == 0)
                {
                    response.Message = $"There is no user available at this moment!!";

                }

                response.Users = users;
            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }
    }
}
