using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.IO;

namespace API.Controllers
{
    [Route("api/advertises")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private IAdvertisementBusiness _advBusiness;
        private string _path;
        public AdvertisementController(IAdvertisementBusiness iadvBusiness) 
        {
            _advBusiness = iadvBusiness;
        }

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<AdvertisementModel> GetAdvAll()
        {
            return _advBusiness.GetAdvAll();
        }

        [Route("get-adv-by-id/{id}")]
        [HttpGet]
        public AdvertisementModel GetAdvID(int id)
        {
            return _advBusiness.GetAdvID(id);
        }

        [Route("create-adv")]
        [HttpPost]
        public AdvertisementModel CreateAdv([FromBody] AdvertisementModel model)
        {
            if (model.Image != null)
            {
                var arrData = model.Image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/images/{arrData[0]}";
                    model.Image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _advBusiness.Create(model);
            return model;
        }

        [Route("update-adv")]
        [HttpPost]
        public AdvertisementModel UpdateAdv([FromBody] AdvertisementModel model)
        {
            if (model.Image != null)
            {
                var arrData = model.Image.Split(';');
                if (arrData.Length == 3)
                {
                    var savePath = $@"assets/FilesUpload/{arrData[0]}";
                    model.Image = $"{savePath}";
                    SaveFileFromBase64String(savePath, arrData[2]);
                }
            }
            _advBusiness.Update(model);
            return model;
        }

        [Route("delete-adv")]
        [HttpGet]
        public AdvertisementModel DeleteAdv(int id)
        {
            AdvertisementModel advertiseModel = _advBusiness.GetAdvID(id);
            _advBusiness.Delete(id.ToString()); 
            return advertiseModel;
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
                string title = "";
                if (formData.Keys.Contains("title") && !string.IsNullOrEmpty(Convert.ToString(formData["title"])))
                {
                    title = Convert.ToString(formData["title"]);
                }
                long total = 0;
                var data = _advBusiness.Search(page, pageSize, out total, title);
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
