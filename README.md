![7digital](http://i.imgur.com/StUnvCy.png?1)

What is this code?
========
Schema in C# for public 7digital Api endpoints. See [The Api Wrapper](https://github.com/7digital/SevenDigital.Api.Wrapper).


Notes
=====

* The schema is compiled as a portable class library. Therefore certain attributes such as `[Serializable]` and `[DataContract]` are not available. They should not be needed.
* To avoid name clashes, the folder name is plural, the classes inside it are singular. e.g. class `Release` in namespace  `Releases`.
* Use tabs not spaces.

### Attributes ###

* The `[XmlElement()]` or `[XmlAttribute()]` attributes gives the name when generating or reading from xml, the property name (lowercased) is used for json. Both should be meaningful.
* The `[XmlRoot()]` element is only needed on the root of the DTO, i.e. where the `[ApiEndpoint]` attribute is also found.

