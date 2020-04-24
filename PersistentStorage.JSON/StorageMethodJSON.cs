using Newtonsoft.Json;
using System;
using System.IO;

namespace PersistentStorage.JSON
{
    public class StorageMethodJSON<T> : ICachedStorageMethod<T>
    {
        public JSONProperties Properties { get; set; }

        public string Name
        {
            get
            {
                return "StorageMethod.Newtonsoft.JSON";
            }
        }

        public T CurrentCache { get; set; }

        public T GetCache()
        {
            return CurrentCache;
        }

        public void Initialize(IProperties Properties)
        {
            this.Properties = (JSONProperties)Properties;
            if (File.Exists(Path.Combine(this.Properties.DataFilePath, this.Properties.DataFile)))
            {
                return;
            }
            Directory.CreateDirectory(this.Properties.DataFilePath);
            File.Create(this.Properties.DataFile).Dispose();
            CurrentCache = Activator.CreateInstance<T>();
            SaveState();
            UpdateCache();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void SaveState()
        {
            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), JsonConvert.SerializeObject(CurrentCache));
        }

        public void SetCache(T CacheObject)
        {
            CurrentCache = CacheObject;
        }

        public void UpdateCache()
        {
            CurrentCache = JsonConvert.DeserializeObject<T>(File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile)));
        }
    }
}
