using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace IOC.Framework
{
    public static class ObjectCloneExtentions
    {
        public static T ShallowCloneCopy<T>(this T obj) where T : IShallowCopy<T>
        {
            return obj.ShallowCopy();
        }
        public static T DeepClone<T>(this Object obj)
        {
            BinaryFormatter BF = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            BF.Serialize(memStream, obj);
            memStream.Flush();
            memStream.Position = 0;
            return (T)BF.Deserialize(memStream);
        }
        public static Object DeepClone(this Object obj, Type type)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj), type);
        }
    }
}
