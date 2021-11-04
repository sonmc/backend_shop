using Model;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public partial class AdvertisementBusiness: IAdvertisementBusiness
    {
        private IAdvertisementRepository _res;
        public AdvertisementBusiness(IAdvertisementRepository adv)
        {
            _res = adv;
        }
        public List<AdvertisementModel> GetAdvAll()
        {
            return _res.GetAdvAll();
        }
        public AdvertisementModel GetAdvID(int id)
        {
            return _res.GetAdvID(id);
        }
        public bool Create(AdvertisementModel model)
        {
            return _res.Create(model);
        }
        public bool Update(AdvertisementModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public List<AdvertisementModel> Search(int pageIndex, int pageSize, out long total, string title)
        {
            return _res.Search(pageIndex, pageSize, out total, title);
        }
    }
}

