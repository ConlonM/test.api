using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mkz.api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public class User
        {
            public string Name { get; set; }

            public int ID { get; set; }
        }
       // [Authorize]
        // GET api/values/5
        public User Get(int id)
        {
            return new User() {
                Name = "蒙康正",
                ID = 30
            };
        }

        public class PagesUser
        {
            public int PageIndex;
            public int PageSize;
            public bool HasNextPage;
            public List<User> List;
        }

        public PagesUser GetUsers(int pageIndex,int pageSize)
        {
            PagesUser page = new PagesUser();
            page.List = new List<User>();
            if (pageIndex <=5)
            {
                for (int i = (pageIndex-1)*pageSize; i < pageIndex * pageSize; i++)
                {
                    var item = new User()
                    {
                        Name = "综合受理窗口" + i,
                        ID = i
                    };
                    page.List.Add(item);
                }

                if(pageIndex == 5)
                {
                    page.HasNextPage = false;
                }
                else
                {
                    page.HasNextPage = true;
                }
            }  



            return page;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {

            //dev branch
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        //修改dev01
    }
}
