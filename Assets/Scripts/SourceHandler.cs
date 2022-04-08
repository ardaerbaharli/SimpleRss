using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SimpleRss
{
    public class SourceHandler
    {
        private string path;

        public SourceHandler()
        {
            path = Application.persistentDataPath + "/sources.json";
        }

        public void Save(RssSourceProperty sourceProperty)
        {
            if (DoesExists(sourceProperty)) return;
            
            var objects = LoadAll();
            objects.Add(sourceProperty);
            var data = JsonConvert.SerializeObject(objects, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public List<RssSourceProperty> LoadAll()
        {
            if (!File.Exists(path))
            {
                return new List<RssSourceProperty>();
            }

            var data = File.ReadAllText(path);
            var b = JsonConvert.DeserializeObject<List<RssSourceProperty>>(data);
            return b;
        }

        private bool DoesExists(RssSourceProperty sourceProperty)
        {
            var sources = LoadAll();
            return sources.Exists(x => x.URL == sourceProperty.URL);
        }
    }
}