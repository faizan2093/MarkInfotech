using MarkInfotech.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarkInfotech.Controllers
{
    public class HomeController : Controller
    {
        //private object objUser;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Create()
        //{

        //}

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_user objuser)
        {
            markinfotechEntities db = new markinfotechEntities();
            var user = db.tbl_user.Where(x => x.username == objuser.username && x.password == objuser.password).Count();
            if (user > 0)
            {
                return RedirectToAction("ListUser");
            }
            else
            {
                return View();
            }

        }
        
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(tbl_user user)
        {
            markinfotechEntities db = new markinfotechEntities();
            db.tbl_user.Add(user);
            db.SaveChanges();
            return RedirectToAction("ListUser");
        }

        //searching, sorting
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ListUser(string searchtxt, string SortOrder, string SortBy, int PageNumber = 1)
        {
            markinfotechEntities db = new markinfotechEntities();
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;

            var users = db.tbl_user.ToList();
            if (searchtxt != null)
            {
                users = db.tbl_user.Where(x => x.username.Contains(searchtxt) || x.email.Contains(searchtxt) || x.gender.Contains(searchtxt)).ToList();
                ApplySorting(SortOrder, SortBy, users);
                users = ApplyPagination(users, PageNumber);
            }
            else
            {
                ApplySorting(SortOrder, SortBy, users);
                users = ApplyPagination(users, PageNumber);
            }
            return View(users);
        }

        public void ApplySorting(string SortOrder, string SortBy, List<tbl_user> users)
        {
            switch (SortBy)
            {
                case "username":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.username).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.username).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.username).ToList();
                                    break;
                                }
                        }
                        break;
                    }

                case "email":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.email).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.email).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.email).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "password":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.password).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.password).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.password).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "gender":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    users = users.OrderBy(x => x.gender).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    users = users.OrderByDescending(x => x.gender).ToList();
                                    break;
                                }
                            default:
                                {
                                    users = users.OrderBy(x => x.gender).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        //Pagination
        public List<tbl_user> ApplyPagination(List<tbl_user> users, int PageNumber)
        {
            ViewBag.TotalPages = Math.Ceiling(users.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            users = users.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return users;
        }


        public ActionResult Details(int id)
        {
            markinfotechEntities db = new markinfotechEntities();
            var user = db.tbl_user.Find(id);
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            markinfotechEntities db = new markinfotechEntities();
            var user = db.tbl_user.Find(id);
            db.tbl_user.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ListUser");

            //markinfotechEntities db = new markinfotechEntities();
            //var user = db.tbl_user.Find(id);
            //db.tbl_user.Remove(user);
            //db.SaveChanges();
            ////return View();
            //return RedirectToAction("Delete");
        }

        public ActionResult UserProfile(int id)
        {
            markinfotechEntities db = new markinfotechEntities();
            var user = db.tbl_user.Find(id);
            user.isinterestedinC_ = (user.isinterestedinC_ == null) ? false : user.isinterestedinC_;
            user.isinterestedinjava = (user.isinterestedinjava == null) ? false : user.isinterestedinjava;
            user.isinterestedinpython = (user.isinterestedinpython == null) ? false : user.isinterestedinpython;

            var cityList = db.tbl_city.ToList();
            user.CityList = new SelectList(cityList, "cityid", "cityname");

            if (user.ImagePath == null)
            {
                user.ImagePath = "~/Content/images/images.png";
            }
            return View(user);
        }
        
        [HttpPost]
        public ActionResult UserProfile(tbl_user objuser, string Csharp, string Java, string Python)
        {
            objuser.isinterestedinC_ = (Csharp == "true") ? true : false;

            objuser.isinterestedinjava = (Java == "true") ? true : false;

            objuser.isinterestedinpython = (Python == "true") ? true : false;

            //if (Csharp == "true")
            //{
            //    objuser.isinterestedinC_ = true;
            //}
            //else
            //{
            //    objuser.isinterestedinC_ = false;
            //}

            //if (Java == "true")
            //{
            //    objuser.isinterestedinjava = true;
            //}
            //else
            //{
            //    objuser.isinterestedinjava = false;
            //}

            //if (Python == "true")
            //{
            //    objuser.isinterestedinpython = true;
            //}
            //else
            //{
            //    objuser.isinterestedinpython = false;
            //}

            if (objuser.UserImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objuser.UserImageFile.FileName);

                string extension = Path.GetExtension(objuser.UserImageFile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;

                objuser.ImagePath = "~/Content/images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                objuser.UserImageFile.SaveAs(fileName);
            }

            if (objuser.ImagePath == "/Content/images/images.png")
            {
                objuser.ImagePath = null;
            }

            markinfotechEntities db = new markinfotechEntities();

            db.Entry(objuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UserProfile", new { id = objuser.userid });
        }
    }
}