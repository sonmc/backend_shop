using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public partial class ProductBusiness : IProductBusiness
    {
        private IProductRepository _res;
        public ProductBusiness(IProductRepository product)
        {
            _res = product;
        }
        public List<ProductModel> GetProAll()
        {
            return _res.GetProAll();
        }
        public List<ProductModel> GetProNew(int top, int pageIndex, int pageSize, out long total)
        {
            return _res.GetProNew(top, pageIndex, pageSize, out total);
        }
        public List<ProductModel> GetProViews(int top, int pageIndex, int pageSize, out long total)
        {
            return _res.GetProViews(top, pageIndex, pageSize, out total);
        }
        public ProductModel GetProID(int id)
        {
            return _res.GetProID(id);
        }
        public bool Create(ProductModel model)
        {
            return _res.Create(model);
        }
        public bool Update(ProductModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public List<ProductModel> GetCate(string id)
        {
            return _res.GetCate(id);
        }
        public List<ProductModel> GetCateHot(string id)
        {
            return _res.GetCateHot(id);
        }
        public List<ProductModel> Search(int pageIndex, int pageSize, out long total, string pro_name, string price_max, string price_min)
        {
            return _res.Search(pageIndex, pageSize, out total, pro_name, price_max, price_min);
        }

    }
}
