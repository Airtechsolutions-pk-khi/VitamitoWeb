using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vitamito.Models.Service
{
    public class shopService : baseService
    {
        shopBLL _service;
        public shopService()
        {
            _service = new shopBLL();
        }

        public List<shopBLL> GetAll(string Category)
        {
            try
            {
                return _service.GetAll(Category);
            }
            catch (Exception ex)
            {
                return new List<shopBLL>();
            }
        }
        public List<shopBLL> BestProducts(int LocationID)
        {
            try
            {
                return _service.BestProducts(LocationID);
            }
            catch (Exception ex)
            {
                return new List<shopBLL>();
            }
        }
    }
}