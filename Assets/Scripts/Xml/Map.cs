/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp {
	[XmlRoot(ElementName = "tileset")]
	public class Tileset {
		[XmlAttribute(AttributeName = "firstgid")]
		public string Firstgid { get; set; }
		[XmlAttribute(AttributeName = "source")]
		public string Source { get; set; }
	}

	[XmlRoot(ElementName = "data")]
	public class Data {
		[XmlAttribute(AttributeName = "encoding")]
		public string Encoding { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "layer")]
	public class Layer {
		[XmlElement(ElementName = "data")]
		public Data Data { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public string Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
	}

	[XmlRoot(ElementName = "object")]
	public class Object {
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "gid")]
		public string Gid { get; set; }
		[XmlAttribute(AttributeName = "x")]
		public string X { get; set; }
		[XmlAttribute(AttributeName = "y")]
		public string Y { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public string Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
	}

	[XmlRoot(ElementName = "objectgroup")]
	public class Objectgroup {
		[XmlElement(ElementName = "object")]
		public List<Object> Object { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "map")]
	public class Map {
		[XmlElement(ElementName = "tileset")]
		public List<Tileset> Tileset { get; set; }
		[XmlElement(ElementName = "layer")]
		public Layer Layer { get; set; }
		[XmlElement(ElementName = "objectgroup")]
		public Objectgroup Objectgroup { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName = "tiledversion")]
		public string Tiledversion { get; set; }
		[XmlAttribute(AttributeName = "orientation")]
		public string Orientation { get; set; }
		[XmlAttribute(AttributeName = "renderorder")]
		public string Renderorder { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public string Width { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
		[XmlAttribute(AttributeName = "tilewidth")]
		public string Tilewidth { get; set; }
		[XmlAttribute(AttributeName = "tileheight")]
		public string Tileheight { get; set; }
		[XmlAttribute(AttributeName = "infinite")]
		public string Infinite { get; set; }
		[XmlAttribute(AttributeName = "nextlayerid")]
		public string Nextlayerid { get; set; }
		[XmlAttribute(AttributeName = "nextobjectid")]
		public string Nextobjectid { get; set; }
	}

}
