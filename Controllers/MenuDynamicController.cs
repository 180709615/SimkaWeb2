using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APIConsume.DAO;
using APIConsume.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace APIConsume.Controllers
{
    public class MenuDynamicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult getSidebarMenu()
        {
            var menu = "";
            var npp = HttpContext.Session.GetString("NPP");
            DBOutput data = (new SidebarMenuDAO()).getSidebarMenu(npp);
            List<SidebarMenu> listMenu = new List<SidebarMenu>();
            List<SidebarMenu> menuTree = GetMenuTree(data.data, 0);
            menuTree.RemoveAll(a => a.ListMenu.Count == 0);
            var append = "";
            foreach (var menus in menuTree)
            {
                if (menus.parentid == 0)
                {
                    append += "<li class='nav-item'><a href = '#' class='nav-link' style='padding-left:0;'><i  class='nav-icon fas fa-circle left' style='font-size: 13px; '></i><p style:'padding-left:1%'>" + menus.menuname + "<i class='fas fa-angle-left right'></i></p></a><ul class='nav nav-treeview'>";

                    if (menus.ListMenu != null && menus.ListMenu.Count > 0)
                    {
                        foreach (var submenu in menus.ListMenu)
                        {
                            //append += "<li class='nav-item'><a href='https://localhost:44393" + submenu.menulocation + "' class='nav-link' style='padding-left:0;' ><i class='far fa-circle nav-icon left'  style='font-size: 13px; '></i><p style:'padding-left:1%'>" + submenu.menuname + "</p></a></li>";
                            append += "<li class='nav-item'><a href='https://simka2-dev.uajy.ac.id" + submenu.menulocation + "' class='nav-link' style='padding-left:0;' ><i class='far fa-circle nav-icon left'  style='font-size: 13px; '></i><p style:'padding-left:1%'>" + submenu.menuname + "</p></a></li>";
                        }
                    }
                    append += "</ul></li>";
                }
            }
            return Json(append);
        }
        private List<SidebarMenu> GetMenuTree(List<SidebarMenu> list, int? parent)
        {
            return list.Where(x => x.parentid == parent).Select(x => new SidebarMenu
            {
                urut = x.urut,
                NO_URUT = x.NO_URUT,
                menuid = x.menuid,
                menuname = x.menuname,
                menulocation = x.menulocation,
                ListMenu = GetMenuTree(list, x.menuid)

            }).ToList();
        }


    }
}
