using System;
using System.Collections.Generic;
using System.Linq;
using works.Models;

namespace works.Features.App.Services
{
    public class SideMenu
    {
        Menu[] allMenus = new[] {
            new Menu()
            {
                Name = "Dashboard",
                Path = "/",
                Icon = "dashboard"
            },
            new Menu
            {
                Name = "구성원",
                Path = "employees",
                Icon = "person_search"
            },
            new Menu
            {
                Name = "근태",
                Path = "worktime",
                Icon = "&#xe616"
            }
        };

        public IEnumerable<Menu> Menus
        {
            get { return allMenus; }
        }

        public IEnumerable<Menu> Filter(string term)
        {
            Func<string, bool> contains = value => value.Contains(term, StringComparison.OrdinalIgnoreCase);

            Func<Menu, bool> filter = (example) => contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));

            return Menus.Where(category => category.Children != null && category.Children.Any(filter))
                           .Select(category => new Menu()
                           {
                               Name = category.Name,
                               Expanded = true,
                               Children = category.Children.Where(filter).ToArray()
                           }).ToList();
        }

        public Menu FindCurrent(Uri uri)
        {
            return Menus.SelectMany(example => example.Children ?? new[] { example })
                           .FirstOrDefault(example => example.Path == uri.AbsolutePath || $"/{example.Path}" == uri.AbsolutePath);
        }

        public string TitleFor(Menu param)
        {
            if (param != null && param.Name != "First Look")
            {
                return param.Title ?? $"Blazor {param.Name} | a free UI component by Radzen";
            }

            return "Free Blazor Components | 50+ controls by Radzen";
        }

        public string DescriptionFor(Menu param)
        {
            return param?.Description ?? "The Radzen Blazor component library provides more than 50 UI controls for building rich ASP.NET Core web applications.";
        }
    }
}