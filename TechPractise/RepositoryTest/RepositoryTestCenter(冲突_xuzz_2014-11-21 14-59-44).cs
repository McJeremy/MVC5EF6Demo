using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JX.Domain.IRepositories;
using JX.Infrastructure;
using JX.Domain.Model.Entities;
using System.Transactions;
using System.Linq.Expressions;
using JX.Domain.Specifications;

namespace TechPractise.RepositoryTest
{
    public class RepositoryTestCenter
    {
        private RepositoryTestCenter()
        {
        }

        private static readonly Lazy<RepositoryTestCenter> center = new Lazy<RepositoryTestCenter>(() =>
        {
            return new RepositoryTestCenter();
        });

        public static RepositoryTestCenter Center
        {
            get
            {
                return center.Value;
            }
        }

        #region User
        public void InitUser()
        {
            ShowCount();

            //IRepositoryContext context = ServiceLocator.Instance.GetService<IRepositoryContext>();
            //context.Commit();

                IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            IRoleRepository rr = ServiceLocator.Instance.GetService<IRoleRepository>();
            IUserRoleRepository urr = ServiceLocator.Instance.GetService<IUserRoleRepository>();
            using (TransactionScope scope = new TransactionScope())
            {
                //初始化5条用户
                for (int i = 1;
                i <= 5;
                i++)
                {
                    ur.Add(new User
                    {
                        UserName = "U" + i.ToString(),
                        UserPWD = "PWD" + i.ToString()
                    });
                }
                ur.Context.Commit();

                //初始化3条规则
                for (int j = 1;
                j <= 3;
                j++)
                {
                    rr.Add(new Role
                    {
                        RoleName = "RoleName" + j.ToString(),
                        RoleDESC = ""
                    });
                }
                rr.Context.Commit();

                scope.Complete();
            }

            Console.WriteLine("初始化完成");
            ShowCount();
        }

        public void ShowCount()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            IRoleRepository rr = ServiceLocator.Instance.GetService<IRoleRepository>();
            IUserRoleRepository urr = ServiceLocator.Instance.GetService<IUserRoleRepository>();
            Console.WriteLine("User记录数：{0},Role记录数:{1},UserRole记录数:{2}", ur.Count, rr.Count, urr.Count);
        }

        public void GetUserByID()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            
            User u = ur.GetByID(new Guid("8865A8C8-7F69-E411-828C-A3DF8914565B"));
            if (null != u)
            {
                Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
            }
        }

        public void GetUserAll()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            IQueryable<User> users = ur.GetALL();
            users.ToList().ForEach(u =>
            {
                Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
            });
        }

        public void GetUserAllWithWhere()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            IQueryable<User> users = ur.GetALL(u => u.UserName == "U3");
            users.ToList().ForEach(u =>
            {
                Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
            });
        }

        public void GetUserWithSpecification()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            UserNameSpecification uns = new UserNameSpecification("U4");
            User u = ur.Get(uns);
            if (null != u)
            {
                Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
            }
        }

        public void GetUserAllWithSpecification()
        {
            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            ISpecification<User> uns = (new UserNameSpecification("U2")).Or(new UserNameSpecification("U5"));

            IQueryable<User> users = ur.GetALL(uns);
            users.ToList().ForEach(u =>
            {
                Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
            });
        }

        public void GetUserAllWithPaging()
        {

            IUserRepository ur = ServiceLocator.Instance.GetService<IUserRepository>();
            var iPageSize = 2;
            var iRecordCount = ur.Count;
            var iPageCount = iRecordCount % iPageSize == 0 ? iRecordCount / iPageSize : iRecordCount / iPageSize + 1;

            for (int i = 1;
            i <= iPageCount;
            i++)
            {
                Console.WriteLine("-----");
                Console.WriteLine("第{0}页，每页显示{1}条记录,共{2}条记录.", i, iPageSize, iRecordCount);
                PagedResult<User> users = ur.GetALL(u => true, u => u.UserName, SortOrder.DESC, iPageSize, i);               
                users.Data.ForEach(u =>
               {
                   Console.WriteLine("用户名：{0},用户密码:{1}", u.UserName, u.UserPWD);
               });
            }
        }
#endregion

        #region Blog

        public void InitBlog()
        {
            IBlogRepository blog = ServiceLocator.Instance.GetService<IBlogRepository>();
            IBlogUserRepository blogU = ServiceLocator.Instance.GetService<IBlogUserRepository>();

            Blog b = new Blog()
            {
                Title = "Xuzzs Blog",
                BlogPosts = new List<BlogPost>()
                {
                    new BlogPost {
                        Title = "BLogPost1",
                        CreateDate = DateTime.Now,
                        BlogPostAuthors =new List<BlogPostAuthor>()
                        {
                            new BlogPostAuthor {Name="XZZ AUhor1" },
                            new BlogPostAuthor {Name="XZZ AUhor2" }
                        }
                    },
                    new BlogPost {
                        Title = "BLogPost2",
                        CreateDate = DateTime.Now,
                        BlogPostAuthors =new List<BlogPostAuthor>()
                        {
                            new BlogPostAuthor {Name="XRH AUhor1" },
                            new BlogPostAuthor {Name="XRH AUhor2" }
                        }
                    }
                }
            };

            BlogUser u = new BlogUser
            {
                Name = "XUZZ"
            };
            u.Blog = b;

            blog.Add(b);
            blogU.Add(u);

            blog.Context.Commit();
        }

        public void GetPostsOfBlog()
        {
            IBlogRepository blog = ServiceLocator.Instance.GetService<IBlogRepository>();
            IBlogPostRepository blogP = ServiceLocator.Instance.GetService<IBlogPostRepository>();
            IBlogPostAuthorRepository blogPA = ServiceLocator.Instance.GetService<IBlogPostAuthorRepository>();

            Blog b = blog.Get((x => x.Title == "Xuzzs Blog"));

            Console.WriteLine("BlogID:{0},BlogName:{1},有以下博客：",b.ID,b.Title);

            //blogP.GetALL(bp => bp.Blog.ID == b.ID).ToList().ForEach(ps =>
            //    {
            //        Console.WriteLine("      PostID:{0},PostTitle:{1},作者有：", ps.ID, ps.Title);                    
            //    });

            b.BlogPosts.ForEach(ps =>
            {
                Console.WriteLine("      PostID:{0},PostTitle:{1},作者有：", ps.ID, ps.Title);
                ps.BlogPostAuthors.ForEach(psa =>
                {
                    Console.WriteLine("          AuthorID:{0},Name:{1}", psa.ID, psa.Name);
                });
            });
        }

#endregion
    }

    public class UserNameSpecification : Specification<User>
    {
        private string name;
        public UserNameSpecification(string strName)
        {
            name = strName;
        }

        public override Expression<Func<User, bool>> GetExpression()
        {
            return u => u.UserName == name;
        }
    }

}
