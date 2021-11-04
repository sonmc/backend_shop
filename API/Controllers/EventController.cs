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
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventBusiness _evenBusiness;
        public EventController(IEventBusiness ievenBusiness)
        {
            _evenBusiness = ievenBusiness;
        }
        [Route("event-get-time")]
        [HttpGet]
        public IEnumerable<EventModel> GetEventTime()
        {
            return _evenBusiness.GetEventTime();
        }
        [Route("event-get-new")]
        [HttpGet]
        public IEnumerable<EventModel> GetEventNew()
        {
            return _evenBusiness.GetEventNew();
        }

        [Route("get-event-by-id/{id}")]
        [HttpGet]
        public EventModel GetEventID(int id)
        {
            return _evenBusiness.GetEventID(id);
        }

        [Route("create-event")]
        [HttpPost]
        public EventModel CreateEvent([FromBody] EventModel model)
        {
            _evenBusiness.Create(model);
            return model;
        }

        [Route("update-event")]
        [HttpPost]
        public EventModel UpdateEvent([FromBody] EventModel model)
        {
            _evenBusiness.Update(model);
            return model;
        }

        [Route("delete-event")]
        [HttpPost]
        public IActionResult DeleteEvent([FromBody] Dictionary<string, object> formData)
        {
            string id = "";
            if (formData.Keys.Contains("id") && !string.IsNullOrEmpty(Convert.ToString(formData["id"]))) { id = Convert.ToString(formData["id"]); }
            _evenBusiness.Delete(id);
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
                string name_event = "";
                string code = "";
                if (formData.Keys.Contains("name_event") && !string.IsNullOrEmpty(Convert.ToString(formData["name_event"])))
                {
                    name_event = Convert.ToString(formData["name_event"]);
                }
                if (formData.Keys.Contains("code") && !string.IsNullOrEmpty(Convert.ToString(formData["code"])))
                {
                    code = Convert.ToString(formData["code"]);
                }
                long total = 0;
                var data = _evenBusiness.Search(page, pageSize, out total, name_event, code);
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
