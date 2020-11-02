using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using Xml2CSharp;

public class MapManager : MonoBehaviour {

	public TextAsset xmlMap;

    void Start() {
		var map = Deserialize<Map>(xmlMap.text);
		var mapLayer = new MapLayer(map.Layer.Name, map.Layer.Width, map.Layer.Height, map.Layer.Data.Text);
		var mapObjectGroup = new MapObjectGroup(map.Objectgroup.Name, map.Objectgroup.Object);
		
	}

	static T Deserialize<T>(string xmlString) {
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));
		return (T)serializer.Deserialize(xmlStream);
	}
}
