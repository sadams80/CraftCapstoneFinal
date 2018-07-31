using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataAccessObjects;
using BusinessLogicLayer.BusinessLogicObjects;

namespace BusinessLogicLayer.BusinessLogicMappers
{
    public class BusinessLogicMapperProjects
    {
        #region Mappers
        public DataAccessProjects MapProject(BusinessLogicProjects bProject)
        {
            if (bProject == null)
            {
                return null;
            }
            DataAccessProjects dProject = new DataAccessProjects()
            {
                Project_ID = bProject.Project_ID,
                User_ID = bProject.User_ID,
                Craft_ID = bProject.Craft_ID,
                ProjectName = bProject.ProjectName,
                ProjectBody = bProject.ProjectBody,
                Difficulty_ID = bProject.Difficulty_ID,
                DifficultyLevel = bProject.DifficultyLevel
            };
            return dProject;
        }

        public BusinessLogicProjects MapProject(DataAccessProjects dProject)
        {
            if (dProject == null)
            {
                return null;
            }
            BusinessLogicProjects bProject = new BusinessLogicProjects
            {
                Project_ID = dProject.Project_ID,
                User_ID = dProject.User_ID,
                Craft_ID = dProject.Craft_ID,
                ProjectName = dProject.ProjectName,
                ProjectBody = dProject.ProjectBody,
                Difficulty_ID = dProject.Difficulty_ID,
                DifficultyLevel = dProject.DifficultyLevel
            };
            return bProject;
        }
        #endregion

        #region Table Mappers
        public List<BusinessLogicProjects> MapProjects(List<DataAccessProjects> dProjects)
        {
            List<BusinessLogicProjects> bProjects = new List<BusinessLogicProjects>();
            foreach (DataAccessProjects dProject in dProjects)
            {
                bProjects.Add(MapProject(dProject));
            }
            return bProjects;
        }

        public List<DataAccessProjects> MapProjects(List<BusinessLogicProjects> bProjects)
        {
            List<DataAccessProjects> dProjects = new List<DataAccessProjects>();
            foreach (BusinessLogicProjects bProject in bProjects)
            {
                dProjects.Add(MapProject(bProject));
            }
            return dProjects;
        }
        #endregion
    }
}
