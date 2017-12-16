using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EODModelViewer.Models
{
    public class Mob : IModelObject
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public bool IsBiped { get; set; }
        public bool IsFemale { get; set; }
        public bool IsVampiir { get; set; }
        public bool IsDemon { get; set; }
        public bool IsAnimal { get; set; }
        public int MountId { get; set; }
        public bool IsOther { get; set; }

        public static List<Mob> ParseMobs(string itemsJson)
        {
            var reader = new JsonTextReader(new StringReader(itemsJson));
            var items = new List<Mob>();
            Mob mob = null;

            while (reader.Read())
            {
                switch (reader.Value)
                {
                    case var _ when reader.TokenType == JsonToken.StartObject:
                        mob = new Mob();
                        break;
                    case var _ when reader.TokenType == JsonToken.EndObject:
                        if (mob != null)
                        {
                            items.Add(mob);
                        }

                        mob = new Mob();
                        break;
                    case var _ when reader.TokenType == JsonToken.PropertyName:
                        ParseProperty(reader.Value.ToString(), mob, reader);
                        break;
                }
            }

            return items;
        }

        private static void ParseProperty(string propName, Mob mob, JsonTextReader reader)
        {
            switch (propName)
            {
                case "ModelId":
                    reader.Read();
                    if (int.TryParse(reader.Value.ToString(), out int modelId))
                    {
                        mob.ModelId = modelId;
                    }
                    break;
                case "Name":
                    reader.Read();
                    mob.Name = reader.Value.ToString();
                    break;
                case "IsBiped":
                    reader.Read();
                    mob.IsBiped = (bool) reader.Value;
                    break;
                case "IsFemale":
                    reader.Read();
                    mob.IsFemale = (bool)reader.Value;
                    break;
                case "IsVampiir":
                    reader.Read();
                    mob.IsVampiir = (bool)reader.Value;
                    break;
                case "IsDemon":
                    reader.Read();
                    mob.IsDemon = (bool)reader.Value;
                    break;
                case "IsAnimal":
                    reader.Read();
                    mob.IsAnimal = (bool)reader.Value;
                    break;
                case "MountId":
                    reader.Read();
                    if (int.TryParse(reader.Value.ToString(), out int mountId))
                    {
                        mob.MountId = mountId;
                    }
                    break;
                case "IsOther":
                    reader.Read();
                    mob.IsOther = (bool)reader.Value;
                    break;
            }
        }
    }
}