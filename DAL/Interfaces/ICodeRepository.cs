using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public partial interface ICodeRepository
    {
        List<CodeModel> GetMyCode(string id);
        CodeModel Create(string ID_User, int ID_Event);  

    }
}
