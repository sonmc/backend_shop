using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BLL
{
    public partial interface ICodeBusiness
    {
        List<CodeModel> GetMyCode(string id);
        CodeModel Create(string ID_User, int ID_Event);
    }
}
