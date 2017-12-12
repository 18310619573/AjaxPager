using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxPager
{
    /// <summary>
    /// GetDataList 的摘要说明
    /// </summary>
    public class GetDataList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //这两个参数为固定参数
            var pageindex = Convert.ToInt32(context.Request["pageindex"]);
            var pageSize = Convert.ToInt32(context.Request["pagesize"]);

            string response = GetDataByIndex(pageindex, pageSize);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 数据源分页获取
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private string GetDataByIndex(int pageIndex, int pageSize)
        {
            var curPagedDataList = new List<DataModel>();

            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = (pageIndex - 1) * pageSize + 1 + pageSize;

            for (int i = startIndex; i < endIndex; i++)
            {
                curPagedDataList.Add(new DataModel() { Id = i, Name = $"张三_{i}", Age = 2 });
            }

            /*{"total":0,"items":[]}*/
            var obj = new { total = 10000, items = curPagedDataList };
            string response = JsonConvert.SerializeObject(obj);

            return response;
        }
    }

    internal class DataModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
    }
}