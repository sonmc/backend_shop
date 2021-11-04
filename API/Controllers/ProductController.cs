using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _proBusiness;
        private string _path;
        public ProductController(IProductBusiness iproBusiness)
        {
            _proBusiness = iproBusiness;
        }

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<ProductModel> GetProAll()
        {
            return _proBusiness.GetProAll();
        }

        [Route("get-new/{top}/{pageIndex}/{pageSize}")]
        [HttpGet]
        public ResponseModel GetProNew(int top, int pageIndex, int pageSize)
        {
            var response = new ResponseModel();
            try
            {
                long total = 0;
                var data = _proBusiness.GetProNew(top, pageIndex, pageSize, out total);
                response.TotalItems = total;
                response.Data = data;
                response.Page = pageIndex;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
        [Route("get-views/{top}/{pageIndex}/{pageSize}")]
        [HttpGet]
        public ResponseModel GetProViews(int top, int pageIndex, int pageSize)
        {
            var response = new ResponseModel();
            try
            {
                long total = 0;
                var data = _proBusiness.GetProViews(top, pageIndex, pageSize, out total);
                response.TotalItems = total;
                response.Data = data;
                response.Page = pageIndex;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("get-pro-by-cate/{id}")]
        [HttpGet]
        public IEnumerable<ProductModel> GetCate(string id)
        {
            return _proBusiness.GetCate(id);
        }

        [Route("get-pro-by-cate-hot/{id}")]
        [HttpGet]
        public IEnumerable<ProductModel> GetCateHot(string id)
        {
            return _proBusiness.GetCateHot(id);
        }

        [Route("get-pro-by-id/{id}")]
        [HttpGet]
        public ProductModel GetProID(int id)
        {
            return _proBusiness.GetProID(id);
        }

        [Route("create-pro")]
        [HttpPost]
        public ProductModel CreatePro([FromBody] ProductModel model)
        {
            if (model.Images != null)
            {
                var arrData = model.Images.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/FilesUpload/{arrData[0]}";
                    model.Images = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _proBusiness.Create(model);
            return model;
        }

        [Route("update-pro")]
        [HttpPost]
        public ProductModel UpdatePro([FromBody] ProductModel model)
        {
            if (model.Images != null)
            {
                var arrData = model.Images.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/FilesUpload/{arrData[0]}";
                    model.Images = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _proBusiness.Update(model);
            return model;
        }

        [Route("delete-pro")]
        [HttpPost]
        public IActionResult DeletePro([FromBody] Dictionary<string, object> formData)
        {
            string id = "";
            if (formData.Keys.Contains("id") && !string.IsNullOrEmpty(Convert.ToString(formData["id"]))) { id = Convert.ToString(formData["id"]); }
            _proBusiness.Delete(id);
            return Ok();
        }
        [Route("search/{pageIndex}/{pageSize}")]
        [Route("search/{pageIndex}/{pageSize}/{pro_name}")]
        [Route("search/{pageIndex}/{pageSize}/{price_max}/{price_min}")]
        [Route("search/{pageIndex}/{pageSize}/{price_max}/{price_min}/{pro_name}")]
        [HttpGet]
        public ResponseModel Search(int pageIndex, int pageSize,string pro_name, string price_max, string price_min)
        {
            var response = new ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(pro_name)))
                {
                    pro_name = "";
                }
                if (string.IsNullOrEmpty(Convert.ToString(price_max)))
                {
                    price_max = "";
                }
                if (string.IsNullOrEmpty(Convert.ToString(price_min)))
                {
                    price_min = "";
                }
                long total = 0;
                var data = _proBusiness.Search(pageIndex, pageSize, out total, pro_name, price_max, price_min);
                response.TotalItems = total;
                response.Data = data;
                response.Page = pageIndex;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
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
                string pro_name = "";
                string price_max = "1000000000";
                string price_min = "0";
                if (formData.Keys.Contains("pro_name") && !string.IsNullOrEmpty(Convert.ToString(formData["pro_name"]))) { pro_name = Convert.ToString(formData["pro_name"]); }
                long total = 0;
                var data = _proBusiness.Search(page, pageSize, out total, pro_name, price_max, price_min);
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
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
