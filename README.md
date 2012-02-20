Description
===========

``goatfish.net`` key-value store for .Net inspired by https://github.com/stochastic-technologies/goatfish




Installation
------------

To install ``goatfish.net`` you need:

* Visual Studio 2008/2010 (May work with Mono, has not been tested).

To use in your projects

* Add a reference to ``GoatFish.net.dll``.
* You may also need to install System.Data.SQLite from ``http://sqlite.phxsoftware.com``.


Usage
-----
To use from command prompt, run ``GoatFish.net-cli/bin/Debug/GoatFish.net-cli.exe``. Type ``help`` for a list of commands.
To use ``goatfish.net``, in your projects you first need to create a new instance of the Models class. 

Use
``Models model = new Models();``

# Add a new key/value pair to GoatFish
 	Models.Save("key", "value");

# Find entity with key 'key'
	Console.WriteLine(Models.Find("key");
	<key,value>

# Find all entities, iterate through and print
	foreach(var v in Models.Find())
	{
		Console.WriteLine(v);
	}
	<key, value>
# Delete entity
	Models.Delete("key");
# Try and find deleted key
	Console.WriteLine(Models.Find("key"));
	<empty, empty>




License
-------

``goatfish.net`` is distributed under the BSD license.
