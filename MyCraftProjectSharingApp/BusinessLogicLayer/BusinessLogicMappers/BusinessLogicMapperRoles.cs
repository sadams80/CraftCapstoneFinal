using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperRoles
    {
        #region Mappers
        public DataAccessRoles MapRole(BusinessLogicRoles bRoles)
        {
            if (bRoles == null)
            {
                return null;
            }
            DataAccessRoles dRoles = new DataAccessRoles
            {
                Role_ID = bRoles.Role_ID,
                RoleName = bRoles.RoleName
            };
            return dRoles;
        }

        public BusinessLogicRoles MapRole(DataAccessRoles dRoles)
        {
            if (dRoles == null)
            {
                return null;
            }
            BusinessLogicRoles bRoles = new BusinessLogicRoles
            {
                Role_ID = dRoles.Role_ID,
                RoleName = dRoles.RoleName
            };
            return bRoles;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicRoles> MapRoles(List<DataAccessRoles> dRoles)
        {
            List<BusinessLogicRoles> bRoles = new List<BusinessLogicRoles>();
            foreach (DataAccessRoles dRole in dRoles)
            {
                bRoles.Add(MapRole(dRole));
            }
            return bRoles;
        }

        public List<DataAccessRoles> MapRoles(List<BusinessLogicRoles> bRoles)
        {
            List<DataAccessRoles> dRoles = new List<DataAccessRoles>();
            foreach (BusinessLogicRoles bRole in bRoles)
            {
                dRoles.Add(MapRole(bRole));
            }
            return dRoles;
        }
        #endregion
    }
}
