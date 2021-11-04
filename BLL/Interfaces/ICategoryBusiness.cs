using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BLL
{
    public partial interface ICategoryBusiness
    {
        List<CategoryModel> GetCatAll();
        CategoryModel GetCatID(int id);
        bool Create(CategoryModel model);
        bool Update(CategoryModel model);
        bool Delete(string id);
        List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string category_name);
    }
}
