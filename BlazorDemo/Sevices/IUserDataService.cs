using Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Sevices
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> AddUser(User user);
        Task<User> GetUser(long id);
        Task DeleteUser(long id);
    }
}
