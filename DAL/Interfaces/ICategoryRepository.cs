using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public partial interface ICategoryRepository
    {
        List<CategoryModel> GetCatAll();
        CategoryModel GetCatID(int id);
        bool Create(CategoryModel model);
        bool Update(CategoryModel model);
        bool Delete(string id);
        List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string category_name);
    }
}
