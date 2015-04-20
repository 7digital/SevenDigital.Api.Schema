![7digital](http://i.imgur.com/StUnvCy.png?1)

What is this code?
========
Schema in C# for public 7digital Api endpoints. These definitions are used by and installed with [The Api Wrapper](https://github.com/7digital/SevenDigital.Api.Wrapper). If you're new, start there.

These definitions can be useful on their own if you want to roll your own http communication to the 7digital Api, but want objects to de-serialise output into. It may also useful as a reference for other languages, when creating objects to deserialise 7digital Api responses into.

Where to get it.
====
This code is [packaged here on nuget](https://www.nuget.org/packages/SevenDigital.Api.Schema/), and is a dependency for [the Api Wrapper](https://www.nuget.org/packages/SevenDigital.Api.Wrapper/)

Notes for coders
=====

* The code is compiled as a portable class library for use from many different .Net versions. Therefore certain attributes such as `[Serializable]` and `[DataContract]` are not available. They should not be needed.
* To avoid name clashes, the folder name and corresponding namespace is plural, the classes inside it are singular. e.g. class `Release` in namespace  `Releases`. i.e. The fully qualified name is `SevenDigital.Api.Schema.Releases.Release`.
* Use tabs not spaces.

### Output generation as XML and json ###

* The `[XmlElement()]` or `[XmlAttribute()]` attributes gives the name when generating or reading from xml, the property name (lowercased) is used for json. Both should be meaningful.
* The `[XmlRoot()]` element is only needed on the root object, i.e. where the `[ApiEndpoint]` attribute is also found.

