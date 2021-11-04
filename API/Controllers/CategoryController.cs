 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusiness _catBusiness;
        public CategoryController(ICategoryBusiness icatBusiness)
        {
            _catBusiness = icatBusiness;
        }

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<CategoryModel> GetCatAll()
        {
            return _catBusiness.GetCatAll();
        }

        [Route("get-cat-by-id/{id}")]
        [HttpGet]
        public CategoryModel GetCatID(int id)
        {
            return _catBusiness.GetCatID(id);
        }

        [Route("create-cat")]
        [HttpPost]
        public CategoryModel CreateCat([FromBody] CategoryModel model)
        {
            _catBusiness.Create(model);
            return model;
        }

        [Route("update-cat")]
        [HttpPost]
        public CategoryModel UpdateCat([FromBody] CategoryModel model)
        {
            _catBusiness.Update(model);
            return model;
        }

        [Route("delete-cat")]
        [HttpPost]
        public IActionResult DeleteCat([FromBody] Dictionary<string, object> formData)
        {
            string id = "";
            if (formData.Keys.Contains("id") && !string.IsNullOrEmpty(Convert.ToString(formData["id"]))) { id = Convert.ToString(formData["id"]); }
            _catBusiness.Delete(id);
            return Ok();
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string category_name = "";
                if (formData.Keys.Contains("category_name") && !string.IsNullOrEmpty(Convert.ToString(formData["category_name"])))
                {
                    category_name = Convert.ToString(formData["category_name"]);
                }
                long total = 0;
                var data = _catBusiness.Search(page, pageSize, out total, category_name);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
