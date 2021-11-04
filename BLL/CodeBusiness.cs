using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class CodeBusiness: ICodeBusiness
    {
        private ICodeRepository _res;
        public CodeBusiness(ICodeRepository code)
        {
            _res = code;
        }
        public List<CodeModel> GetMyCode(string id)
        {
            return _res.GetMyCode(id);
        }
        public CodeModel Create(string ID_User, int ID_Event)
        {
            return _res.Create(ID_User, ID_Event);
        }
    }
}
