using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperUsers
    {
        static PaswordHashLogic _businessLogic = new PaswordHashLogic();

        #region Mappers
        public DataAccessUsers MapUser(BusinessLogicUsers bUser)
        {
            if (bUser == null)
            {
                return null;
            }
            DataAccessUsers dUser = new DataAccessUsers
            {
                User_ID = bUser.User_ID,
                FirstName = bUser.FirstName,
                LastName = bUser.LastName,
                Age = bUser.Age,
                Gender = bUser.Gender,
                Email = bUser.Email,
                Username = bUser.Username,
                Password = _businessLogic.HashPassword(bUser.Password),
                House_ID = bUser.House_ID,
                Role_ID = bUser.Role_ID
            };
            return dUser;
        }

        public BusinessLogicUsers MapUser(DataAccessUsers dUser)
        {
            if (dUser == null)
            {
                return null;
            }
            BusinessLogicUsers bUser = new BusinessLogicUsers
            {
                User_ID = dUser.User_ID,
                FirstName = dUser.FirstName,
                LastName = dUser.LastName,
                Age = dUser.Age,
                Gender = dUser.Gender,
                Email = dUser.Email,
                Username = dUser.Username,
                Password = dUser.Password,
                House_ID = dUser.House_ID,
                Role_ID = dUser.Role_ID,
            };
            return bUser;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicUsers> MapUsers(List<DataAccessUsers> dUsers)
        {
            List<BusinessLogicUsers> bUsers = new List<BusinessLogicUsers>();
            foreach (DataAccessUsers dUser in dUsers)
            {
                bUsers.Add(MapUser(dUser));
            }
            return bUsers;
        }
        public List<DataAccessUsers> MapUsers(List<BusinessLogicUsers> bUsers)
        {
            List<DataAccessUsers> dUsers = new List<DataAccessUsers>();
            foreach (BusinessLogicUsers bUser in bUsers)
            {
                dUsers.Add(MapUser(bUser));
            }
            return dUsers;
        }
        #endregion
    }
}
