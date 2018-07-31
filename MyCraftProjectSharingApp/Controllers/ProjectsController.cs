using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCraftProjectSharingApp.Models;
using MyCraftProjectSharingApp.Mappers;
using BusinessLogicLayer;
namespace MyCraftProjectSharingApp.Controllers
{
    public class ProjectsController : Controller
    {
        #region Instances
        static ProjectsLogic _projectBusinessLogic = new ProjectsLogic();
        static CraftsLogic _craftBusinessLogic = new CraftsLogic();
        static UsersLogic _userBusinessLogic = new UsersLogic();
        static DifficultyLogic _difficultyBusinessLogic = new DifficultyLogic();
        static MapperProjects _projectMapper = new MapperProjects();
        static MapperCrafts _craftMapper = new MapperCrafts();
        static MapperUsers _userMapper = new MapperUsers();
        static MapperDifficulty _difficultyMapper = new MapperDifficulty();
        static HousesController _houseController = new HousesController();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View("Index", "Home", new { area = "" });
        } //return to main homepage
        #endregion

        #region POST
        [HttpGet]
        public ActionResult CreateProject()
        {
            if (Session["RoleID"] != null)
            {
                ViewModel projectViewModel = new ViewModel
                {
                    Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts()),
                    Difficulties = _difficultyMapper.MapDifficulties(_difficultyBusinessLogic.GetDifficulty())
                };
                projectViewModel.SingleProject.User_ID = (int)Session["UserId"];
                return View(projectViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //create project for all users

        [HttpPost]
        public ActionResult CreateProject(ViewModel projectToAdd)
        {
            if (Session["RoleID"] != null)
            {
                if (ModelState.IsValid)
                {
                    _projectBusinessLogic.AddProject(_projectMapper.MapProject(projectToAdd.SingleProject));
                    ViewModel projects = new ViewModel
                    {
                        Projects = _projectMapper.MapProjects(_projectBusinessLogic.GetProjects())
                    };
                    _houseController.AddPoints(20, (int)Session["House_ID"]);
                    if ((int)Session["House_ID"] == 1)
                    {
                        TempData["ProjectSuccess"] = "20 points to GRYFFINDOR!";
                    }
                    else if ((int)Session["House_ID"] == 2)
                    {
                        TempData["ProjectSuccess"] = "20 points to SLYTHERIN!";
                    }
                    else if ((int)Session["House_ID"] == 3)
                    {
                        TempData["ProjectSuccess"] = "20 points to RAVENCLAW!";
                    }
                    else if ((int)Session["House_ID"] == 4)
                    {
                        TempData["ProjectSuccess"] = "20 points to HUFFLEPUFF!";
                    }
                    return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                }
                else
                {
                    ViewModel projectViewModel = new ViewModel
                    {
                        Difficulties = _difficultyMapper.MapDifficulties(_difficultyBusinessLogic.GetDifficulty()),
                        Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts())
                    };
                    projectViewModel.SingleProject.User_ID = (int)Session["UserId"];
                    return View(projectViewModel);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }

        } //create project for all users
        #endregion

        #region Gets
        public ActionResult ViewProjects()
        {
            if (Session["RoleID"] != null)
            {
                ViewModel projects = new ViewModel
                {
                    Projects = _projectMapper.MapProjects(_projectBusinessLogic.GetProjects())
                };
                foreach (Projects project in projects.Projects)
                {
                    ViewModel users = new ViewModel
                    {
                        SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(project.User_ID))
                    };
                    if (project.User_ID == users.SingleUser.User_ID)
                    {
                        project.Username = users.SingleUser.Username;
                    }
                }
                foreach (Projects project in projects.Projects)
                {
                    ViewModel crafts = new ViewModel
                    {
                        SingleCraft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftId(project.Craft_ID))
                    };
                    if (project.Craft_ID == crafts.SingleCraft.Craft_ID)
                    {
                        project.CraftName = crafts.SingleCraft.CraftName;
                    }
                }
                return View(projects);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //view project for all users
        #endregion

        #region Patch
        [HttpGet]
        public ActionResult UpdateProject(int? id)
        {
            if (id != null)
            {
                if (Session["RoleID"] != null)
                {
                    int projectToUpdate = ConvertToInt(id);
                    Projects project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToUpdate));
                    if ((int)Session["UserId"] == project.User_ID || (int)Session["RoleID"] == 3)
                    {
                        ViewModel projectViewModel = new ViewModel
                        {
                            Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts()),
                            SingleProject = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToUpdate)),
                            Difficulties = _difficultyMapper.MapDifficulties(_difficultyBusinessLogic.GetDifficulty())
                        };
                        projectViewModel.SingleProject.User_ID = project.User_ID;
                        //if (project.DifficultyLevel = 'Beginner')
                        return View(projectViewModel);
                    }
                    else
                    {
                        return RedirectToAction("PageError", "Error", new { area = "" });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Users", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("PageError", "Error", new { area = "" });
            }
        }   //update project for all users

        [HttpPost]
        public ActionResult UpdateProject(ViewModel projectToUpdate)
        {
            if (Session["RoleId"] != null)
            {
                if (ModelState.IsValid)
                {
                    Projects project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToUpdate.SingleProject.Project_ID));
                    if ((int)Session["UserId"] == projectToUpdate.SingleProject.User_ID || (int)Session["RoleID"] == 3)
                    {
                        _projectBusinessLogic.UpdateProject(projectToUpdate.SingleProject.Project_ID, _projectMapper.MapProject(projectToUpdate.SingleProject));
                        TempData["ProjectSuccess"] = "Project successfully updated.";
                        return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                    }
                    else
                    {
                        return RedirectToAction("PageError", "Error", new { area = "" });
                    }
                }
                else
                {
                    ViewModel projectViewModel = new ViewModel
                    {
                        Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts()),
                        Difficulties = _difficultyMapper.MapDifficulties(_difficultyBusinessLogic.GetDifficulty())
                    };
                    projectViewModel.SingleProject.Project_ID = projectToUpdate.SingleProject.Project_ID;
                    projectViewModel.SingleProject.User_ID = projectToUpdate.SingleProject.User_ID;
                    return View(projectViewModel);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });

            }
        }  //update project for all users
        #endregion

        #region Delete
        public ActionResult DeleteProject(int? id)
        {
            if (id != null)
            {
                if (Session["RoleID"] != null)
                {
                    int projectToDelete = ConvertToInt(id);
                    Projects project = _projectMapper.MapProject(_projectBusinessLogic.GetProjectByProjectId(projectToDelete));
                    if ((int)Session["UserId"] == project.User_ID || (int)Session["RoleID"] != 3)
                    {
                        _projectBusinessLogic.DeleteProject(projectToDelete);
                        _houseController.AddPoints(-20, (int)Session["House_ID"]);
                        TempData["ProjectDeleted"] = "Project has been deleted successfully.";
                        return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                    }
                    else
                    {
                        ViewModel user = new ViewModel
                        {
                            SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(project.User_ID))
                        };
                        if (project.User_ID == user.SingleUser.User_ID)
                        {
                            _houseController.AddPoints(-20, user.SingleUser.House_ID);
                        }
                        _projectBusinessLogic.DeleteProject(projectToDelete);
                        TempData["ProjectDeleted"] = "Project has been deleted successfully.";
                        return RedirectToAction("ViewProjects", "Projects", new { area = "" });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Users", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("PageError", "Error", new { area = "" });
            }
        } //delete project for user based on role or userId
        #endregion

        #region NullCheck
        public int ConvertToInt(int? id)
        {
            return Convert.ToInt32(id);
        }
        #endregion
    }
}