using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DrinksInfo
{
    public class Controller
    {
        public static List<MenuList> GetCatList() {
            
            string Listurl = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";

            var client = new RestClient(Listurl);

            var request = new RestRequest();

            var response = client.GetAsync(request).Result.Content;

            JObject Json = JObject.Parse(response);

            List<MenuListFull> MenuList = new();

            int ItemIndex = 1;

            foreach (var value in Json["drinks"])
            {
                string ItemValue = value["strCategory"].ToString();

                MenuList.Add(
                    new MenuListFull {
                        MenuItemIndex = ItemIndex,
                        MenuItem = ItemValue
                    }
                );

                ItemIndex++;     
            }

            List<MenuList> List = new();

            foreach (MenuListFull item in MenuList)
            {
                List.Add(
                    new MenuList {
                        MenuItemIndex = item.MenuItemIndex,
                        MenuItem = item.MenuItem
                    }
                );
            }

            return List;
        }

        public static List<MenuList> GetCatItems(string CatName) {

            string CategoryUrl = $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={CatName}";

            var client = new RestClient(CategoryUrl);

            var request = new RestRequest();

            var response = client.GetAsync(request).Result.Content;

            JObject Json = JObject.Parse(response);

            List<MenuListFull> CatMenu = new();

            int ItemIndex = 1;

            foreach (var value in Json["drinks"])
            {
                string ItemValue = value["strDrink"].ToString();
                int ItemID = Int32.Parse(value["idDrink"].ToString());

                CatMenu.Add(
                    new MenuListFull {
                        MenuItemIndex = ItemIndex,
                        MenuItemID = ItemID,
                        MenuItem = ItemValue
                    }
                );

                ItemIndex++;     
            }

            List<MenuList> List = new();

            foreach (MenuListFull item in CatMenu)
            {
                List.Add(
                    new MenuList {
                        MenuItemIndex = item.MenuItemIndex,
                        MenuItem = item.MenuItem
                    }
                );
            }

            return List;
        }
    }
}

public class MenuListFull {
    public int MenuItemIndex {get; set;}
    public int MenuItemID {get; set;}
    public string MenuItem {get; set;}
}

public class MenuList {
    public int MenuItemIndex {get; set;}
    public string MenuItem {get; set;}
}


public class DrinkInfo {

}