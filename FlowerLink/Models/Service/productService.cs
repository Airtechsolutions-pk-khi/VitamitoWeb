using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vitamito.Models.Service
{
    public class productService : baseService
    {
        productBLL _service;
        public productService()
        {
            _service = new productBLL();
        }

        public productBLL GetAll(int ID, int LocationID)
        {
            try
            {
                return _service.GetAll(ID, LocationID);
            }
            catch (Exception ex)
            {
                return new productBLL();
            }
        }
        public List<productBLL> GetRelated(int ID)
        {
            try
            {
                return _service.GetAllRelated(ID);
            }
            catch (Exception ex)
            {
                return new List<productBLL>();
            }
        }

    }
}