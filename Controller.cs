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

        public static List<MenuListFull> GetCatItems(string CatName) {

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

            return CatMenu;
        }

        public static List<MenuList> CatItems(string Catname) {
            var CatMenu = GetCatItems(Catname);

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

        public static List<DrinkInfo> GetDrinkInfo(string DrinkName, string DrinkListChoice) {
            var CatMenu = GetCatItems(DrinkListChoice);

            int ItemID = 0;

            foreach (MenuListFull item in CatMenu)
            {
                if(item.MenuItem == DrinkName) {
                    ItemID = item.MenuItemID;
                }
            }

            string CategoryID = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={ItemID}";

            var client = new RestClient(CategoryID);

            var request = new RestRequest();

            var response = client.GetAsync(request).Result.Content;

            JObject Json = JObject.Parse(response);
            
            List<DrinkInfo> DrinkInfo = new();

            foreach (var value in Json["drinks"])
            {
                string Name = value["strDrink"].ToString();
                bool HasAlcohol = value["strAlcoholic"].ToString() == "Alcoholic" ? true : false;
                string Glass = value["strGlass"].ToString();
                string HowTo = value["strInstructions"].ToString();

                

                DrinkInfo.Add(
                    new DrinkInfo {
                        DrinkName = Name,
                        isAlcoholic = HasAlcohol,
                        GlassType = Glass,
                        Instructions = HowTo,
                        Ingredients = "No"
                    }
                );  
            }
            
            return DrinkInfo;
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
    public string DrinkName {get; set;}
    public bool isAlcoholic {get; set;}
    public string GlassType {get; set;}
    public string Instructions {get; set;}
    public string Ingredients {get; set;}
}