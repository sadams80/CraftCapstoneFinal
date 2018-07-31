using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperHouses
    {
        #region Mappers
        public DataAccessHouses MapHouse(BusinessLogicHouses bHouse)
        {
            if (bHouse == null)
            {
                return null;
            }
            DataAccessHouses dHouse = new DataAccessHouses
            {
                House_ID = bHouse.House_ID,
                HouseName = bHouse.HouseName,
                Motto = bHouse.Motto,
                Points = bHouse.Points
            };
            return dHouse;
        }

        public BusinessLogicHouses MapHouse(DataAccessHouses dHouse)
        {
            if (dHouse == null)
            {
                return null;
            }
            BusinessLogicHouses bHouse = new BusinessLogicHouses
            {
                House_ID = dHouse.House_ID,
                HouseName = dHouse.HouseName,
                Motto = dHouse.Motto,
                Points = dHouse.Points
            };
            return bHouse;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicHouses> MapHouses(List<DataAccessHouses> dHouses)
        {
            List<BusinessLogicHouses> bHouses = new List<BusinessLogicHouses>();
            foreach (DataAccessHouses dHouse in dHouses)
            {
                bHouses.Add(MapHouse(dHouse));
            }
            return bHouses;
        }

        public List<DataAccessHouses> MapHouses(List<BusinessLogicHouses> bHouses)
        {
            List<DataAccessHouses> dHouses = new List<DataAccessHouses>();
            foreach (BusinessLogicHouses bHouse in bHouses)
            {
                dHouses.Add(MapHouse(bHouse));
            }
            return dHouses;
        }
        #endregion
    }
}
