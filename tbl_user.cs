//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarkInfotech.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    public partial class tbl_user
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public Nullable<bool> isinterestedinC_ { get; set; }
        public Nullable<bool> isinterestedinjava { get; set; }
        public Nullable<bool> isinterestedinpython { get; set; }
        public Nullable<int> cityid { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string ImagePath { get; set; }

        public HttpPostedFileBase UserImageFile { get; set; } 

        public virtual tbl_city tbl_city { get; set; }
        public SelectList CityList { get; internal set; }
    }
}
