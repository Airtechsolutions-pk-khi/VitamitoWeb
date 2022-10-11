using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vitamito.Models.Service
{
    public class itemService : baseService
    {
        itemBLL _service;
        public itemService()
        {
            _service = new itemBLL();
        }

        public List<itemBLL> GetAll(int LocationID)
        {
            try
            {
                return _service.GetAll(LocationID);
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }
        public List<itemBLL> GetSelecteditems(int ID, int LocationID)
        {
            try
            {
                return _service.GetSelecteditems(ID, LocationID);
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }
        public List<itemBLL> GetAllFeatured()
        {
            try
            {
                return _service.GetAllFeatured();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

        public List<itemBLL> GetAllPopular()
        {
            try
            {
                return _service.GetAllPopular();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

        public List<itemBLL> GetAllValentineDay()
        {
            try
            {
                return _service.GetAllValentineDay();
            }
            catch (Exception ex)
            {
                return new List<itemBLL>();
            }
        }

    }
}