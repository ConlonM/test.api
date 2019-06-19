using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace mkz.api
{
    public class PropertyToIgnore
    {
        [XmlAttribute]
        public string Class { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }
    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public new static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        private List<PropertyToIgnore> propertyToIgnores = new List<PropertyToIgnore>();

        public ShouldSerializeContractResolver()
        {
            StringReader rdr = new StringReader(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataIgnore.xml")));
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PropertyToIgnore>));
                propertyToIgnores = (List<PropertyToIgnore>)serializer.Deserialize(rdr);
            }
        }


        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            //需要先判断是否是管理门户登陆的


            return property;
        }
    }
}