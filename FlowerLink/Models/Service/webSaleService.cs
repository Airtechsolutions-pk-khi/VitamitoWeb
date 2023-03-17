using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vitamito.Models.Service
{
    public class webSaleService : baseService
    {
        webSaleBLL _service;
        public webSaleService()
        {
            _service = new webSaleBLL();
        }
         
        public List<webSaleBLL> GetSelectedSaleitems(int LocationID)
        {
            try
            {
                return _service.GetSelectedFlashItems( LocationID);
            }
            catch (Exception ex)
            {
                return new List<webSaleBLL>();
            }
        }
    
    }
}