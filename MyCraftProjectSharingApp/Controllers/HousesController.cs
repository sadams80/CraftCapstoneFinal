using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCraftProjectSharingApp.Models;
using MyCraftProjectSharingApp.Mappers;
using BusinessLogicLayer;
using BusinessLogicLayer.BusinessLogicObjects;

namespace MyCraftProjectSharingApp.Controllers
{
    public class HousesController : Controller
    {
        #region Instances
        // GET: Houses
        static HousesLogic _housesBusinessLogic = new HousesLogic();
        static MapperHouses _housesMapper = new MapperHouses();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View("Index", "Home", new { are = "" });
        }  //return to main homepage
        #endregion

        #region Gets
        public ActionResult ViewHouses()
        {
            if (Session["RoleID"] != null)
            {
                ViewModel houses = new ViewModel
                {
                    Houses = _housesMapper.MapHouses(_housesBusinessLogic.GetHouses())
                };
                return View(houses);
            }
            else
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
        }  //view houses for all users
        #endregion

        #region Points
        [HttpPost]
        public void AddPoints(long points, int houseId)
        {
            _housesBusinessLogic.UpdateHousePoints(points, houseId);
        }  //add points method

        public ActionResult ResetPoints()
        {
            _housesBusinessLogic.ResetPoints();
            BusinessLogicHouses blTopHouse = _housesBusinessLogic.TopHousePoints();
            if (blTopHouse != null)
            {
                TempData["ResetSuccess"] = "House Points reset successfully.";
            }
            return RedirectToAction("ViewHouses", new { area = "" });
        }
        #endregion
    }
}