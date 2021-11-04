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
    public class CodeController : ControllerBase
    {
        private ICodeBusiness _codeBusiness;
        private string _path;
        public CodeController(ICodeBusiness icodeBusiness)
        {
            _codeBusiness = icodeBusiness;
        }

        [Route("get-my-code/{id}")]
        [HttpGet]
        public IEnumerable<CodeModel> GetMyCode(string id)
        {
            return _codeBusiness.GetMyCode(id);
        }

        [Route("create-code/{ID_User}/{ID_Event}")]
        [HttpGet]
        public CodeModel CreateCode(string ID_User, int ID_Event)
        {
            var response = new CodeModel();
            try
            {
                var data = _codeBusiness.Create(ID_User, ID_Event);
                response.Data = data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
