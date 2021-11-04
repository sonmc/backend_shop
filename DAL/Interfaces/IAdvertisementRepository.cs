using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public partial interface IAdvertisementRepository
    {
        List<AdvertisementModel> GetAdvAll();
        AdvertisementModel GetAdvID(int id);
        bool Create(AdvertisementModel model);
        bool Update(AdvertisementModel model);
        bool Delete(string id);
        List<AdvertisementModel> Search(int pageIndex, int pageSize, out long total, string title);
    }
}
