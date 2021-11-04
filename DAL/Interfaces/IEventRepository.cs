using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public partial interface IEventRepository
    {
        List<EventModel> GetEventTime();
        List<EventModel> GetEventNew();
        EventModel GetEventID(int id); 
        bool Create(EventModel model);
        bool Update(EventModel model);
        bool Delete(string id);
        List<EventModel> Search(int pageIndex, int pageSize, out long total, string name_event, string code);
    }
}
