using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EODModelViewer.Models;

namespace EODModelViewer
{
    internal class ModelDataService
    {
        public async Task<Dictionary<string, List<IModelObject>>> ParseData(Dictionary<string, string> data)
        {
            if (!data.ContainsKey("items.json") || !data.ContainsKey("mobs.json"))
            {
                return null;
            }

            var parsedData = new Dictionary<string, List<IModelObject>>();

            var itemsTask = Task.Run(() =>
                Item.ParseItems(data["items.json"]).Select(x => x as IModelObject).ToList());

            var mobsTask = Task.Run(() =>
                Mob.ParseMobs(data["mobs.json"]).Select(x => x as IModelObject).ToList());

            var items = await itemsTask;
            var mobs = await mobsTask;

            parsedData.Add("items", items);
            parsedData.Add("mobs", mobs);

            return parsedData;
        }

        public async Task<Dictionary<string, string>> GetData()
        {
            if (!File.Exists("./Data/items.json") || !File.Exists("./Data/items.json"))
            {
                return await DownloadDataAsync();
            }

            var data = new Dictionary<string, string>
            {
                {"items.json", File.ReadAllText("./Data/items.json")},
                {"mobs.json", File.ReadAllText("./Data/mobs.json")}
            };

            return data;
        }

        public async Task<Dictionary<string, string>> DownloadDataAsync()
        {
            var data = new Dictionary<string, string>();
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("https://github.com/Eve-of-Darkness/DolModels/raw/master/src/data.zip"))
            using (HttpContent content = response.Content)
            {
                var stream = await content.ReadAsStreamAsync();
                using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    foreach (var entry in zip.Entries)
                    {
                        using (var zipFileStream = entry.Open())
                        {
                            var jsonData = StreamToString(zipFileStream);
                            data.Add(entry.Name, jsonData);
                        }
                    }
                }
            }

            PersistData(data);

            return data;
        }

        public void PersistData(Dictionary<string, string> data)
        {
            if (!Directory.Exists("./Data"))
            {
                Directory.CreateDirectory("./Data");
            }

            foreach (var dataKey in data.Keys)
            {
                File.WriteAllText($"./Data/{dataKey}", data[dataKey]);
            }
        }

        private string StreamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
