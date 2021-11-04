using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IProductBusiness
    {
        List<ProductModel> GetProAll();
        List<ProductModel> GetProNew(int top, int pageIndex, int pageSize, out long total);
        List<ProductModel> GetProViews(int top, int pageIndex, int pageSize, out long total);
        ProductModel GetProID(int id);
        bool Create(ProductModel model);
        bool Update(ProductModel model);
        bool Delete(string id);
        List<ProductModel> GetCate(string id);
        List<ProductModel> GetCateHot(string id);
        List<ProductModel> Search(int pageIndex, int pageSize, out long total, string pro_name, string price_max, string price_min);
    }
}
