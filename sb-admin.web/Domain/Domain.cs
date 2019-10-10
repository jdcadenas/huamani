using sb_admin.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sb_admin.web.Models
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar {id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true });
            menu.Add(new Navbar { id = 2, nameOption = "Charts", controller = "Home", action = "Charts", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true });
            menu.Add(new Navbar { id = 3, nameOption = "Tables", controller = "Home", action = "Tables", imageClass = "fa fa-fw fa-table", estatus = true });
            menu.Add(new Navbar { id = 4, nameOption = "Forms", controller = "Home", action = "Forms", imageClass = "fa fa-fw fa-edit", estatus = true });
            menu.Add(new Navbar { id = 5, nameOption = "Bootstrap Elements", controller = "Home", action = "BootstrapElements", imageClass = "fa fa-fw fa-desktop", estatus = true });
            menu.Add(new Navbar { id = 6, nameOption = "Bootstrap Grid", controller = "Home", action = "BootstrapGrid", imageClass = "fa fa-fw fa-wrench", estatus = true });
            menu.Add(new Navbar { id = 7, nameOption = "Blank Page", controller = "Home", action = "BlankPage", imageClass = "fa fa-fw fa-file", estatus = true });

            return menu.ToList();
        }

        public IEnumerable<User> users()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, user = "admin", password = "12345", estatus = true, RememberMe = true });
            users.Add(new User { Id = 2, user = "lvasquez", password = "lvasquez", estatus = true, RememberMe = false });
            users.Add(new User { Id = 3, user = "invite", password = "12345", estatus = false, RememberMe = false });

            return users.ToList();
        }

        public IEnumerable<Roles> roles()
        {
            var roles = new List<Roles>();
            roles.Add(new Roles { id = 1, userId = 1, MenuId = 1, status = true });
            roles.Add(new Roles { id = 2, userId = 1, MenuId = 2, status = true });
            roles.Add(new Roles { id = 3, userId = 1, MenuId = 3, status = true });
            roles.Add(new Roles { id = 4, userId = 1, MenuId = 4, status = true });
            roles.Add(new Roles { id = 5, userId = 1, MenuId = 5, status = true });
            roles.Add(new Roles { id = 6, userId = 1, MenuId = 6, status = true });
            roles.Add(new Roles { id = 7, userId = 1, MenuId = 7, status = true });
            roles.Add(new Roles { id = 8, userId = 2, MenuId = 1, status = true });
            roles.Add(new Roles { id = 9, userId = 2, MenuId = 2, status = true });
            roles.Add(new Roles { id = 10, userId = 2, MenuId = 3, status = true });
            roles.Add(new Roles { id = 11, userId = 2, MenuId = 4, status = true });
            roles.Add(new Roles { id = 12, userId = 2, MenuId = 5, status = true });
            roles.Add(new Roles { id = 13, userId = 3, MenuId = 1, status = true });
            roles.Add(new Roles { id = 14, userId = 3, MenuId = 2, status = true });

            return roles.ToList();
        }

        public IEnumerable<Navbar> itemsPerUser(string controller, string action, string userName)
        {
            
            IEnumerable<Navbar> items = navbarItems();
            IEnumerable<Roles> rolesNav = roles();
            IEnumerable<User> usersNav = users();

            var navbar =  items.Where(p => p.controller == controller && p.action == action).Select(c => { c.activeli = true; return c; }).ToList();

            navbar = (from nav in items
                      join rol in rolesNav on nav.id equals rol.MenuId
                      join user in usersNav on rol.userId equals user.Id
                      where user.user == userName
                      select new Navbar
                      {
                          id = nav.id,
                          nameOption = nav.nameOption,
                          controller = nav.controller,
                          action = nav.action,
                          imageClass = nav.imageClass,
                          estatus = nav.estatus,
                          activeli = nav.activeli
                      }).ToList();

            return navbar.ToList();
        }

    }
}