using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class AdvertisementRepository: IAdvertisementRepository
    {
        private IDatabaseHelper _dbHelper;
        public AdvertisementRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<AdvertisementModel> GetAdvAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "adv_get_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AdvertisementModel GetAdvID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "adv_get_id",
                     "@id", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<AdvertisementModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(AdvertisementModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "adv_create",
                "@Title" , model.Title,
                "@Category", model.Category,
                "@Image" , model.Image,
                "@Link" , model.Link,
                "@Note" , model.Note,
                "@Daystart" , model.Daystart,
                "@Dayend" , model.Dayend);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(AdvertisementModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "adv_update",
                "@ID", model.ID,
                "@Title", model.Title,
                "@Category", model.Category,
                "@Image", model.Image,
                "@Link", model.Link,
                "@Note", model.Note,
                "@Daystart", model.Daystart,
                "@Dayend", model.Dayend);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "adv_delete",
                "@ID", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AdvertisementModel> Search(int pageIndex, int pageSize, out long total, string title)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_adv_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@title", title);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}