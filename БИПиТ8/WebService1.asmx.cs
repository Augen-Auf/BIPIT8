using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Data;

namespace БИПиТ8
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebService1 : WebService
    {
        [WebMethod]
        public AllData[] GetRecords()
        {
            List<AllData> data = new List<AllData>();
            using (adverstEntities db = new adverstEntities())
            {
                var order = db.OrderView;
                foreach (var item in order)
                {
                    AllData info = new AllData
                    {
                        IdO = item.IdO.ToString(),
                        Client = item.Client.ToString(),
                        Service = item.Service.ToString(),
                        Time = item.Time.ToString(),
                        Price = item.Price.ToString(),
                        Total = item.Total.ToString(),
                        Date = item.Date.ToString(),
                    };
                    data.Add(info);
                }
            }
            return data.ToArray();
        }

        public Dictionary<int, string> Clients()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            using (adverstEntities db = new adverstEntities())
            {
                var client = db.Client;
                foreach (var item in client)
                    dict.Add(item.IdC, item.Client1);
            }
            return dict;
        }
        public Dictionary<int,string> Services()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            using (adverstEntities db = new adverstEntities())
            {
                var client = db.Service;
                foreach (var item in client)
                    dict.Add(item.IdS, item.Service1);
            }
            return dict;
        }

        public void NewRec(int IdC,int IdS, int Time, DateTime Date)
        {
            using (adverstEntities db = new adverstEntities())
            {
                var order = db.Order;
                Order OrderToAdd = new Order
                {
                    IdC_FK = IdC,
                    IdS_FK = IdS,
                    Time = Time,
                    Date = Date
                };
                order.Add(OrderToAdd);
                db.SaveChanges();
            }
        }
        public void DeleteRec(int orderID)
        {
            using (adverstEntities db = new adverstEntities())
            {
                var order = db.Order;
                Order OrderToDelete = db.Order.Where(x => x.IdO == orderID).FirstOrDefault();
                if (OrderToDelete != null)
                {
                    db.Order.Remove(OrderToDelete);
                    db.SaveChanges();
                }
            }
        }
        public List<string> GetData(string dateBegin, string dateEnd)
        {
            List<string> rowsID = new List<string>();
            using (adverstEntities db = new adverstEntities())
            {
                var order = db.OrderView;
                foreach (var item in order)
                {
                    if (dateBegin != "" && dateEnd != "")
                    {
                        if (item.Date >= Convert.ToDateTime(dateBegin) & item.Date <= Convert.ToDateTime(dateEnd))
                            rowsID.Add(item.IdO.ToString());
                    }
                    else
                        rowsID.Add(item.IdO.ToString());
                }
            }
            return rowsID;
        }
        public class AllData
        {
            public string IdO { get; set; } 
            public string Client { get; set; }
            public string Service { get; set; }
            public string Time { get; set; }
            public string Price { get; set; }
            public string Total { get; set; }
            public string Date { get; set; }
        }

    }
}
