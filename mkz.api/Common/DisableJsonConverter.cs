using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace mkz.api
{
    public interface IDisable
    {
        bool Disable { get; set; }
    }
    public class DisableJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
          
            if (typeof(IDisable).IsAssignableFrom(objectType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject jsonObject = JObject.Load(reader);

            return JsonConvert.DeserializeObject(jsonObject.ToString(), objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                if (value != null && value is IDisable && ((IDisable)value).Disable != true)
                {
                    JObject jo = new JObject();
                    Type type = value.GetType();

                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        if (prop.CanRead)
                        {
                            object propVal = prop.GetValue(value, null);
                            if (propVal != null)
                            {
                                if (propVal is IDisable)
                                {
                                    ((IDisable)propVal).Disable = false;
                                }
                                jo.Add(prop.Name, JToken.FromObject(propVal, serializer));
                            }
                        }
                    }
                    jo.WriteTo(writer);
                }
            }
            catch (Exception e)
            {
               // LogScope.LogException("序列化失败", e);
            }

        }
    }
}