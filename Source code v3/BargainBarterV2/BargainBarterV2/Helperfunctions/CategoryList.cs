using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BargainBarterV2.Helperfunctions
{
    public class CategoryList : List<SelectListItem>
    {
        public CategoryList()
        {
            //Add(new SelectListItem
            //{
            //    Text = "---Vælg kategori---",
            //    Value = "---Vælg kategori---",
            //    Selected = true,
            //    Disabled = true
            //});

            Add(new SelectListItem
            {
                Text = "Diverse",
                Value = "Diverse"
            });
            Add(new SelectListItem
            {
                Text = "Interiør",
                Value = "Interiør"
            });
            Add(new SelectListItem
            {
                Text = "Elektronik",
                Value = "Elektronik"
            });
            Add(new SelectListItem
            {
                Text = "Sport",
                Value = "Sport"
            });
            Add(new SelectListItem
            {
                Text = "Tøj",
                Value = "Tøj"
            });
        }
    }
}