using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public partial class CategoryBusiness: ICategoryBusiness
    {
        private ICategoryRepository _res;
        public CategoryBusiness(ICategoryRepository product)
        {
            _res = product;
        }
        public List<CategoryModel> GetCatAll()
        {
            return _res.GetCatAll();
        }
        public CategoryModel GetCatID(int id)
        {
            return _res.GetCatID(id);
        }
        public bool Create(CategoryModel model)
        {
            return _res.Create(model);
        }
        public bool Update(CategoryModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string category_name)
        {
            return _res.Search(pageIndex, pageSize, out total, category_name);
        }
    }
}
