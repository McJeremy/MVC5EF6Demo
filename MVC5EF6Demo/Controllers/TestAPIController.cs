using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC5EF6Demo.Controllers
{
    public class Users
    {
        public int UserID { get;set; }
        public string UserName { get;set; }
        public string UserEmail { get;set; }
    }

    public class TestAPIController : ApiController
    {
       private readonly List<Users> _userList = new List<Users>
       {
           new Users {UserID = 1, UserName = "zzl", UserEmail = "bfyxzls@sina.com"},
           new Users {UserID = 2, UserName = "Spiderman", UserEmail = "Spiderman@cnblogs.com"},
           new Users {UserID = 3, UserName = "Batman", UserEmail = "Batman@cnblogs.com"}
       };

        public IEnumerable<Users> GetAll()
        {
            return _userList;
        }

        public Users Get(int id)
        {
            return _userList.FirstOrDefault<Users>(i => i.UserID == id);

        }

        public Users Post([FromBody] Users entity)
        {
            _userList.Add(entity);
            return entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="form">表单对象，它是唯一的</param>
        /// <returns></returns>
        public Users Put(int id, [FromBody]Users entity)
        {
            var user = _userList.FirstOrDefault(i => i.UserID == id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.UserEmail = entity.UserEmail;
            }
            return user;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            _userList.Remove(_userList.FirstOrDefault(i => i.UserID == id));
        }
    }
}
