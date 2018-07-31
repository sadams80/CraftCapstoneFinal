using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessLogicObjects;
using MyCraftProjectSharingApp.Models;

namespace MyCraftProjectSharingApp.Mappers
{
    public class MapperUsers
    {
        public BusinessLogicUsers MapUser(Users user)
        {
            if (user == null)
            {
                return null;
            }
            BusinessLogicUsers bUser = new BusinessLogicUsers
            {
                User_ID = user.User_ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Gender = user.Gender,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                House_ID = user.House_ID,
                Role_ID = user.Role_ID
            };
            return bUser;
        }
        public Users MapUser(BusinessLogicUsers bUser)
        {
            if (bUser == null)
            {
                return null;
            }
            Users user = new Users
            {
                User_ID = bUser.User_ID,
                FirstName = bUser.FirstName,
                LastName = bUser.LastName,
                Age = bUser.Age,
                Gender = bUser.Gender,
                Email = bUser.Email,
                Username = bUser.Username,
                Password = bUser.Password,
                House_ID = bUser.House_ID,
                Role_ID = bUser.Role_ID
            };
            return user;
        }
        public List<BusinessLogicUsers> MapUsers(List<Users> users)
        {
            List<BusinessLogicUsers> bUsers = new List<BusinessLogicUsers>();
            foreach (Users user in users)
            {
                bUsers.Add(MapUser(user));
            }
            return bUsers;
        }
        public List<Users> MapUsers(List<BusinessLogicUsers> bUsers)
        {
            List<Users> users = new List<Users>();
            foreach (BusinessLogicUsers bUser in bUsers)
            {
                users.Add(MapUser(bUser));
            }
            return users;
        }
    }
}