using PhonebookWebApplication.Dtos;
using PhonebookWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XunitTestUtility
{
    internal class PhonebookService
    {
        private readonly IUserRepository _userRepository;

        public PhonebookService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetUserDetailsResponse GetUserByName(string name)
        {
            var res=_userRepository.GetUserByName(name);
            if (res.Users.Count == 0)
            {
                res.Message = $"User not found with name {name}";
                return res;
            }
                
            return res;
        }
        public DeleteResponse DeleteUser(int id)
        {
            var res= _userRepository.DeleteUser(id);
            if (res.Id == 0)
            {
                res.Message = $"user not found with userId {id}";
                return res;
            }
            return res;
            
        }



    }
}
