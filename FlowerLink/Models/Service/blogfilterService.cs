using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Vitamito.Models.Service
{
    public class blogfilterService : baseService
    {
        blogfilterBLL _service;
        public blogfilterService()
        {
            _service = new blogfilterBLL();
        }
        public List<blogfilterBLL> GetAll(blogfilterBLL filter)
        {
            try
            {
                return _service.GetAll(filter);
            }
            catch (Exception ex)
            {
                return new List<blogfilterBLL>();
            }
            }

    }
}