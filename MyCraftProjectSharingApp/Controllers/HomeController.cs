﻿using System;
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
    public class HomeController : Controller
    {
        #region Instances
        // GET: Home
        static HousesLogic _housesBusinessLogic = new HousesLogic();
        static MapperHouses _housesMapper = new MapperHouses();
        #endregion

        #region HomePage
        public ActionResult Index()
        {
            BusinessLogicHouses blTopHouse = _housesBusinessLogic.TopHousePoints();
            Houses topHouse = _housesMapper.Maphouse(blTopHouse);

            ViewModel houseVM = new ViewModel
            {
                SingleHouse = topHouse
            };
            return View(houseVM);
        } //main home page
        #endregion
    }
}