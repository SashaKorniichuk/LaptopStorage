using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class AccountService : IAccountService
    {
        public ApplicationUser GetNewApplicationUser(ApplicationUserDTO userDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };

            return user;
        }
    }
}
