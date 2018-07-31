using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperCrafts
    {
        #region Mappers
        public DataAccessCrafts MapCraft(BusinessLogicCrafts bCraft)
        {
            if (bCraft == null)
            {
                return null;
            }
            DataAccessCrafts dCraft = new DataAccessCrafts
            {
                Craft_ID = bCraft.Craft_ID,
                CraftName = bCraft.CraftName,
                Description = bCraft.Description,
                User_ID = bCraft.User_ID
            };
            return dCraft;
        }

        public BusinessLogicCrafts MapCraft(DataAccessCrafts dCraft)
        {
            if (dCraft == null)
            {
                return null;
            }
            BusinessLogicCrafts bCraft = new BusinessLogicCrafts
            {
                Craft_ID = dCraft.Craft_ID,
                CraftName = dCraft.CraftName,
                Description = dCraft.Description,
                User_ID = dCraft.User_ID,
                Username = dCraft.Username
            };
            return bCraft;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicCrafts> MapCrafts(List<DataAccessCrafts> dCrafts)
        {
            List<BusinessLogicCrafts> bCrafts = new List<BusinessLogicCrafts>();
            foreach (DataAccessCrafts dCraft in dCrafts)
            {
                bCrafts.Add(MapCraft(dCraft));
            }
            return bCrafts;
        }
        public List<DataAccessCrafts> MapCrafts(List<BusinessLogicCrafts> bCrafts)
        {
            List<DataAccessCrafts> dCrafts = new List<DataAccessCrafts>();
            foreach (BusinessLogicCrafts bCraft in bCrafts)
            {
                dCrafts.Add(MapCraft(bCraft));
            }
            return dCrafts;
        }
        #endregion
    }
}
