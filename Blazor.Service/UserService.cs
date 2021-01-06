using Blazor.Data;
using Blazor.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Service
{
   public class UserService:IUserService
    {
        private IRepository<User> userRepository;
        private IRepository<UserProfile> userProfileRepository;
        public UserService(IRepository<User> userRepository, IRepository<UserProfile> userProfileRepository)
        {
            this.userRepository = userRepository;
            this.userProfileRepository = userProfileRepository;
        }

        public void DeleteUser(long id)
        {
            //UserProfile userProfile = userProfileRepository.Get(id);
            //if (userProfile != null)
            //    userProfileRepository.Remove(userProfile);
            User user = GetUser(id);
            //userRepository.Remove(user);
            //userRepository.SaveChanges();
            userRepository.Delete(user);
        }

        public User GetUser(long id)
        {
            return userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers()
        {
           return userRepository.GetAll();
        }

        public void InsertUser(User user)
        {
             userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            var updatedUser = GetUser(user.Id);
            updatedUser.UserName = user.UserName;
            updatedUser.Password = user.Password;
            updatedUser.Email = user.Email;
            updatedUser.ModifiedDate = user.ModifiedDate;
            userRepository.Update(updatedUser);
        }
    }
}
