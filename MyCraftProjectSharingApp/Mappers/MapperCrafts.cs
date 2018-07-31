using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessLogicObjects;
using MyCraftProjectSharingApp.Models;

namespace MyCraftProjectSharingApp.Mappers
{
    public class MapperCrafts
    {
        public BusinessLogicCrafts MapCraft(Crafts craft)
        {
            if (craft == null)
            {
                return null;
            }
            BusinessLogicCrafts bCraft = new BusinessLogicCrafts
            {
                Craft_ID = craft.Craft_ID,
                CraftName = craft.CraftName,
                Description = craft.Description,
                User_ID = craft.User_ID
            };
            return bCraft;
        }
        public Crafts MapCraft(BusinessLogicCrafts bCraft)
        {
            if (bCraft == null)
            {
                return null;
            }
            Crafts craft = new Crafts
            {
                Craft_ID = bCraft.Craft_ID,
                CraftName = bCraft.CraftName,
                Description = bCraft.Description,
                User_ID = bCraft.User_ID,
                Username = bCraft.Username
            };
            return craft;
        }
        public List<Crafts> MapCrafts(List<BusinessLogicCrafts> bCrafts)
        {
            List<Crafts> crafts = new List<Crafts>();
            foreach (BusinessLogicCrafts bCraft in bCrafts)
            {
                crafts.Add(MapCraft(bCraft));
            }
            return crafts;
        }
        public List<BusinessLogicCrafts> MapCrafts(List<Crafts> crafts)
        {
            List<BusinessLogicCrafts> bCrafts = new List<BusinessLogicCrafts>();
            foreach (Crafts craft in crafts)
            {
                bCrafts.Add(MapCraft(craft));
            }
            return bCrafts;
        }
    }
}