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
    public class CraftsController : Controller
    {
        #region Instances
        // GET: Crafts
        static CraftsLogic _craftBusinessLogic = new CraftsLogic();
        static UsersLogic _userBusinessLogic = new UsersLogic();
        static MapperCrafts _craftMapper = new MapperCrafts();
        static MapperUsers _userMapper = new MapperUsers();
        static HousesController _houseController = new HousesController();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View("Index", "Home", new { are = "" });
        } //returns to home controller homepage
        #endregion

        #region Post
        [HttpGet]
        public ActionResult CreateCraft()
        {
            if (Session["RoleID"] != null)
            {
                if (Session["UserId"] != null && (int)Session["RoleID"] != 1)
                {
                    ViewModel craftViewModel = new ViewModel();
                    craftViewModel.SingleCraft.User_ID = (int)Session["UserId"];
                    return View(craftViewModel);
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
        } //create craft for power user and admin

        [HttpPost]
        public ActionResult CreateCraft(ViewModel craftToAdd)
        {
            if (Session["RoleID"] != null)
            {
                if ((int)Session["RoleID"] != 1)
                {
                    if (ModelState.IsValid)
                    {
                        Crafts craft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftName(craftToAdd.SingleCraft.CraftName));
                        if (craft == null)
                        {
                            _craftBusinessLogic.AddCraft(_craftMapper.MapCraft(craftToAdd.SingleCraft));
                            _houseController.AddPoints(50, (int)Session["House_ID"]);
                            if ((int)Session["House_ID"] == 1)
                            {
                                TempData["CraftSuccess"] = "50 points to GRYFFINDOR!";
                            }
                            else if ((int)Session["House_ID"] == 2)
                            {
                                TempData["CraftSuccess"] = "50 points to SLYTHERIN!";
                            }
                            else if ((int)Session["House_ID"] == 3)
                            {
                                TempData["CraftSuccess"] = "50 points to RAVENCLAW!";
                            }
                            else if ((int)Session["House_ID"] == 4)
                            {
                                TempData["CraftSuccess"] = "50 points to HUFFLEPUFF!";
                            }
                            return RedirectToAction("ViewAllCrafts", "Crafts", new { area = "" });
                        }
                        else
                        {
                            TempData["CraftError"] = "Craft name already exists";
                            return View();
                        }
                    }
                    else
                    {
                        return View(craftToAdd);
                    }
                }
                else
                {
                    return RedirectToAction("PageError", "Errors", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //create craft for power user and admin
        #endregion

        #region Gets
        public ActionResult ViewAllCrafts()
        {
            if (Session["RoleID"] != null)
            {
                ViewModel crafts = new ViewModel
                {
                    Crafts = _craftMapper.MapCrafts(_craftBusinessLogic.GetCrafts())
                };
                foreach (Crafts craft in crafts.Crafts)
                {
                    ViewModel users = new ViewModel
                    {
                        SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(craft.User_ID))
                    };
                    if (craft.User_ID == users.SingleUser.User_ID)
                    {
                        craft.Username = users.SingleUser.Username;
                    }
                }
                return View(crafts);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        } //View all crafts for individual and admin
        #endregion

        #region Patch
        [HttpGet]
        public ActionResult UpdateCraft(int? id)
        {
            if (Session["RoleID"] != null)
            {
                int craftToUpdate = ConvertToInt(id);
                Crafts craft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftId(craftToUpdate));
                if ((int)Session["UserId"] == craft.User_ID && (int)Session["RoleID"] != 1 || (int)Session["RoleID"] == 3)
                {
                    ViewModel craftViewModel = new ViewModel
                    {
                        SingleCraft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftId(craftToUpdate))
                    };
                    craftViewModel.SingleCraft.User_ID = craft.User_ID;
                    return View(craftViewModel);
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
        }  //update craft for power user and admin
        [HttpPost]
        public ActionResult UpdateCraft(ViewModel craftToUpdate)
        {
            if (Session["RoleID"] != null)
            {
                if (ModelState.IsValid)
                {
                    if ((int)Session["UserId"] == craftToUpdate.SingleCraft.User_ID || (int)Session["RoleID"] == 3)
                    {
                        Crafts craft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftName(craftToUpdate.SingleCraft.CraftName));
                        if (craft == null || craft.CraftName == craftToUpdate.SingleCraft.CraftName)
                        {
                            if (craft.User_ID == (int)Session["UserId"] || (int)Session["RoleId"] == 3)
                            {
                                _craftBusinessLogic.UpdateCraft(craftToUpdate.SingleCraft.Craft_ID, _craftMapper.MapCraft(craftToUpdate.SingleCraft));
                                TempData["CraftSuccess"] = "Craft successfully updated.";
                                return RedirectToAction("ViewAllCrafts", "Crafts", new { area = "" });
                            }
                            else
                            {
                                return RedirectToAction("PageError", "Error", new { area = "" });
                            }
                        }
                        else
                        {
                            TempData["CraftError"] = "Craft name already exists";
                            return View(craftToUpdate);
                        }
                    }
                    else
                    {
                        return RedirectToAction("PageError", "Error", new { area = "" });
                    }
                }
                else
                {
                    return View(craftToUpdate);
                }
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        }  //update craft for power user and admin
        #endregion

        #region Delete
        public ActionResult DeleteCraft(int? id)
        {
            if (Session["RoleID"] != null)
            {
                if ((int)Session["RoleID"] != 1)
                {
                    int craftToDelete = ConvertToInt(id);
                    Crafts craft = _craftMapper.MapCraft(_craftBusinessLogic.GetCraftByCraftId(craftToDelete));
                    if ((int)Session["UserId"] == craft.User_ID && (int)Session["RoleID"] != 3)
                    {
                        _craftBusinessLogic.DeleteCraft(craftToDelete);
                        _houseController.AddPoints(-50, (int)Session["House_ID"]);
                        TempData["DeleteSuccess"] = "Craft successfully deleted.";
                        return RedirectToAction("ViewAllCrafts", "Crafts", new { area = "" });
                    }
                    else
                    {
                        ViewModel user = new ViewModel
                        {
                            SingleUser = _userMapper.MapUser(_userBusinessLogic.GetUserByUserId(craft.User_ID))
                        };
                        if (craft.User_ID == user.SingleUser.User_ID)
                        {
                            _houseController.AddPoints(-50, user.SingleUser.House_ID);
                        }
                        _craftBusinessLogic.DeleteCraft(craftToDelete);
                        TempData["DeleteSuccess"] = "Craft successfully deleted.";
                        return RedirectToAction("ViewAllCrafts", "Crafts", new { area = "" });
                    }
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
        } //delete craft for power user and admin
        #endregion

        #region NullCheck
        public int ConvertToInt(int? id)
        {
            return Convert.ToInt32(id);
        }
        #endregion
    }
}