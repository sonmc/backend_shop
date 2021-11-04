using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial class EventBusiness: IEventBusiness
    {
        private IEventRepository _res;
        public EventBusiness(IEventRepository even)
        {
            _res = even;
        }
        public List<EventModel> GetEventTime()
        {
            return _res.GetEventTime();
        }
        public List<EventModel> GetEventNew()
        {
            return _res.GetEventNew();
        }
        public EventModel GetEventID(int id)
        {
            return _res.GetEventID(id);
        }
        public bool Create(EventModel model)
        {
            return _res.Create(model);
        }
        public bool Update(EventModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public List<EventModel> Search(int pageIndex, int pageSize, out long total, string name_event, string code)
        {
            return _res.Search(pageIndex, pageSize, out total, name_event, code);
        }
    }
}
