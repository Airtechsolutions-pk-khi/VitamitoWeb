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
        blogBLL _blogService;
        public productService()
        {
            _service = new productBLL();
            _blogService = new blogBLL();
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
        public blogBLL GetBlogByID(int BlogID, int LocationID)
        {
            try
            {
                return _blogService.GetByID(BlogID, LocationID);
            }
            catch (Exception ex)
            {
                return new blogBLL();
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
        public List<blogBLL> GetRelatedBlog(int BlogID)
        {
            try
            {
                return _blogService.GetAllRelated(BlogID);
            }
            catch (Exception ex)
            {
                return new List<blogBLL>();
            }
        }

    }
}