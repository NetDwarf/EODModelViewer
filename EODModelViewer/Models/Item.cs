using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EODModelViewer.Models
{
    public class Item : IModelObject
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsArmor { get; set; }
        public bool IsWeapon { get; set; }
        public int WeaponHand { get; set; }
        public WeaponHands WeaponHandName { get; set; }
        public bool IsSiegeWeapon { get; set; }
        public bool IsHousingItem { get; set; }
        public bool IsWorldObject { get; set; }
        public bool IsInventory { get; set; }
        public bool IsOther { get; set; }

        public enum WeaponHands
        {
            LeftHanded = 0,
            TwoHanded = 1,
            RightHand = 2
        }

        public static List<Item> ParseItems(string itemsJson)
        {
            var reader = new JsonTextReader(new StringReader(itemsJson));
            var items = new List<Item>();
            Item item = null;

            while (reader.Read())
            {
                switch (reader.Value)
                {
                    case var _ when reader.TokenType == JsonToken.StartObject:
                        item = new Item();
                        break;
                    case var _ when reader.TokenType == JsonToken.EndObject:
                        if (item != null)
                        {
                            items.Add(item);
                        }

                        item = new Item();
                        break;
                    case var _ when reader.TokenType == JsonToken.PropertyName:
                        ParseProperty(reader.Value.ToString(), item, reader);
                        break;
                }
            }

            return items;
        }

        private static void ParseProperty(string propName, Item item, JsonTextReader reader)
        {
            switch (propName)
            {
                case "ModelId":
                    reader.Read();
                    if (int.TryParse(reader.Value.ToString(), out int modelId))
                    {
                        item.ModelId = modelId;
                    }
                    break;
                case "Name":
                    reader.Read();
                    item.Name = reader.Value.ToString();
                    break;
                case "Category":
                    reader.Read();
                    item.Category = reader.Value.ToString();
                    break;
                case "IsArmor":
                    reader.Read();
                    item.IsArmor = (bool)reader.Value;
                    break;
                case "IsWeapon":
                    reader.Read();
                    item.IsWeapon = (bool)reader.Value;
                    break;
                case "IsSiegeWeapon":
                    reader.Read();
                    item.IsSiegeWeapon = (bool)reader.Value;
                    break;
                case "IsHousingItem":
                    reader.Read();
                    item.IsHousingItem = (bool)reader.Value;
                    break;
                case "IsWorldObject":
                    reader.Read();
                    item.IsWorldObject = (bool)reader.Value;
                    break;
                case "IsInventory":
                    reader.Read();
                    item.IsInventory = (bool)reader.Value;
                    break;
                case "IsOther":
                    reader.Read();
                    item.IsOther = (bool)reader.Value;
                    break;
                case "WeaponHandName":
                    reader.Read();
                    if (int.TryParse(reader.Value.ToString(), out int weaponHandName))
                    {
                        item.WeaponHandName = (WeaponHands) weaponHandName;
                    }
                    break;
            }
        }
    }
}