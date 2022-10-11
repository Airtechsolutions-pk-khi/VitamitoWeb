﻿using Vitamito.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Vitamito.Models.Service
{
    public class categoryService : baseService
    {
        categoryBLL _service;
        public categoryService()
        {
            _service = new categoryBLL();
        }

        public List<categoryBLL> GetAll(int LocationID)
        {
            try
            {
                return _service.GetAll(LocationID);
            }
            catch (Exception ex)
            {
                return new List<categoryBLL>();
            }
        }

    }
}