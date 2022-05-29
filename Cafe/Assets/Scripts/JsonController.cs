using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;
public class JsonController
{
    public static void Save(string fileName, object savingObject)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);

        using var file = File.Open(filePath, FileMode.OpenOrCreate);
        var bf = new BinaryFormatter();
        bf.Serialize(file, savingObject);
    }

    public static T Load<T>(string fileName, T defaultObject)
    {
        object loadingObject;
        var filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            using var file = File.Open(filePath, FileMode.Open);
            var bf = new BinaryFormatter();
            loadingObject = (T)bf.Deserialize(file);
        }
        else
        {
            loadingObject = defaultObject;
        }

        return (T)loadingObject;
    }

    public static void InternalSaveJSON(string fileName, object savingObject)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        var json = JsonConvert.SerializeObject(savingObject);
        File.WriteAllText(filePath, json);
    }

    public static T InternalLoadJSON<T>(string fileName, T defaultObject)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath))
        {
            return defaultObject;
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static T PopulateJSON<T>(string fileName, T obj)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath))
        {

            return obj;
        }

        var json = File.ReadAllText(filePath);
        var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
        JsonConvert.PopulateObject(json, obj, serializerSettings);
        return obj;
    }

    public static void InternalDelete(string fileName)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.Delete(filePath);
    }

    public static void SaveJSON(string filePath, object savingObject)
    {
        var json = JsonConvert.SerializeObject(savingObject, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static T LoadJSON<T>(string filePath, T defaultObject)
    {
        if (!File.Exists(filePath))
        {
            return defaultObject;
        }

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static void Delete(string filePath)
    {
        File.Delete(filePath);
    }
}