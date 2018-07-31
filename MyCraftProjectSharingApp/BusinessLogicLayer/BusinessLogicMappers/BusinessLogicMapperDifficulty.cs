using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperDifficulty
    {
        #region Mappers
        public DataAccessDifficulty MapDifficulty(BusinessLogicDifficulty bDifficulty)
        {
            if (bDifficulty == null)
            {
                return null;
            }
            DataAccessDifficulty dDifficulty = new DataAccessDifficulty
            {
                Difficulty_ID = bDifficulty.Difficulty_ID,
                DifficultyLevel = bDifficulty.DifficultyLevel
            };
            return dDifficulty;
        }

        public BusinessLogicDifficulty MapDifficulty(DataAccessDifficulty dDifficulty)
        {
            if (dDifficulty == null)
            {
                return null;
            }
            BusinessLogicDifficulty bDifficulty = new BusinessLogicDifficulty
            {
                Difficulty_ID = dDifficulty.Difficulty_ID,
                DifficultyLevel = dDifficulty.DifficultyLevel
            };
            return bDifficulty;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicDifficulty> MapDifficulties(List<DataAccessDifficulty> dDifficulties)
        {
            List<BusinessLogicDifficulty> bDifficulties = new List<BusinessLogicDifficulty>();
            foreach (DataAccessDifficulty dDifficulty in dDifficulties)
            {
                bDifficulties.Add(MapDifficulty(dDifficulty));
            }
            return bDifficulties;
        }

        public List<DataAccessDifficulty> MapDifficulties(List<BusinessLogicDifficulty> bDifficulties)
        {
            List<DataAccessDifficulty> dDifficulties = new List<DataAccessDifficulty>();
            foreach (BusinessLogicDifficulty bDifficulty in bDifficulties)
            {
                dDifficulties.Add(MapDifficulty(bDifficulty));
            }
            return dDifficulties;
        }
        #endregion
    }
}
