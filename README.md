ELFinder Connector for .NET with support for NancyFX and ASP.NET MVC
========

elFinder is an open-source file manager for web, written in JavaScript using
jQuery UI. Creation is inspired by simplicity and convenience of Finder program
used in Mac OS X operating system.

This connector supports version 2.0 of the [ELFinder API](https://github.com/Studio-42/elFinder/wiki/Client-Server-API-2.0).

It works with ASP.NET MVC (version 4) and NancyFX (1.4.3) as self-host. Other versions might work as well.

Contents
--------
* [Supported features](#features)
* [Missing features](#missing)
* [Requirements](#requirements)
* [Getting started](#getting started)
* [Deployment](#deployment)
* [FAQs](#faqs)
* [TODO](#todo)
* [Support](#support)
* [Author](#author)
* [Acknowledgments](#acknowledgments)
* [License](#license)

Features
--------
 * Support [ELFinder API v2.0](https://github.com/Studio-42/elFinder/wiki/Client-Server-API-2.0)
 * All operations with files and folders on a remote server
 * Multi-root support
 * Local file system volume storage driver
 * Lots of comments in the source code

Missing
--------

 * Archives create/extract (zip, rar, 7z, tar, gzip, bzip2)
 * Full error handling conforming to [API specs](https://github.com/Studio-42/elFinder/wiki/Client-Server-API-2.0#errors). Some errors are properly handled, but parameters validation is incomplete.

Requirements
------------

Solution `ELFinder.sln` was made with Visual Studio 2015. It's Nuget-enabled, the required packages should restore automatically when building.

### ASP.NET MVC
 * Framework version 4.0.30506 (nuget dependency)
 * .NET Framework 4

### NancyFX
 * NancyFX 1.4.3 (nuget dependency)
 * .NET Framework 4

Getting started
------------

### From source

##### Core

 1. Clone this repository

      ```
      $ git clone https://github.com/linguanostra/ELFinder.Connector.NET.git
      ```

 2. Open solution `ELFinder.sln`

 3. Follow instructions for [ASP.NET MVC](#ASP.NET MVC) or [NancyFX](#NancyFX)

 4. Restore Nuget packages (should be automatic when building)

 4. Compile / Run

##### ASP.NET MVC

 1. Set `ELFinder.WebServer.ASPNet` as your startup project

 2. Edit method `InitELFinderConfiguration` in `Global.asax.cs` to customize configuration
 
##### ASP.NET MVC Core

 1. Set `ELFinder.WebServer.ASPNet` as your startup project

 2. Edit method `InitELFinderConfiguration` in `Startup.cs` to customize configuration

##### NancyFX

1. Set `ELFinder.WebServer.Nancy` as your startup project

2. Edit method `InitELFinderConfiguration` in `Program.cs` to customize configuration

### From Nuget

##### ASP.NET MVC

 1. Install connector Nuget package for ASP.NET MVC using this command:

 ```
 $ Install-Package ELFinder.Connector.ASPNet
 ```

 2. Create a controller that inherits from ELFinderBaseConnectorController .

 3. Refer to the ELFinder.WebServer.ASPNet project for help with usage.
 

##### ASP.NET MVC Core

 1. Install connector Nuget package for ASP.NET MVC using this command:

 ```
 $ Install-Package ELFinder.Connector.ASPNetCore
 ```

 2. Create a controller that inherits from ELFinderBaseConnectorController .

 3. Refer to the ELFinder.WebServer.ASPNetCore project for help with usage.
 

##### NancyFX

  1. Install connector Nuget package for NancyFX using this command:

  ```
  $ Install-Package ELFinder.Connector.Nancy
  ```
  2. Create a module that inherits from ELFinderBaseConnectorModule .

  3. Refer to the ELFinder.WebServer.Nancy project for help with usage.

Deployment
------------

Feel free to adapt the code in any way you see fit for your needs. For this solution, the deployment requirements are as follows:

##### Core

The project/library `ELFinder.Connector`

##### ASP.NET MVC

The project/library `ELFinder.Connector.ASPNet`

##### ASP.NET MVC Core

The project/library `ELFinder.Connector.ASPNetCore`

##### NancyFX

The project/library `ELFinder.Connector.Nancy`

FAQs
------------
### Is this production ready?
Depends on your requirements, tests don't cover much of the code.

Security of hashes for files/directories is far from being strong. For hosting this connector to access sensitive files, I'd recommend you change the hashes encryption routines.

Plenty of testing should be done. Feel free to contribute any changes you make.

### Will this connector support the v2.1 elFinder API?
Eventually

### Who's the cat in sample files?
That Wasabi, my 2 years old adorable [Exotic Shorthair](https://en.wikipedia.org/wiki/Exotic_Shorthair)

TODO
-------

* More tests
* Complete errors handling (including parameters validation) conforming to [API specs](https://github.com/Studio-42/elFinder/wiki/Client-Server-API-2.0#errors)
* Encryption of files/directories hashes
* Archive support
* elFinder v2.1 API support

Support
-------

### Connector

* [Wiki](https://github.com/linguanostra/ELFinder.Connector.NET/wiki)
* [Issues](https://github.com/linguanostra/ELFinder.Connector.NET/issues)

Contact author: <linguanostra@gmail.com>

### ElFinder

Refer to [ELFinder Homepage](http://elfinder.org) or [Github repository](https://github.com/Studio-42/elFinder) .

Author
-------

 * Patrick Cloutier (<linguanostra@gmail.com>)

Acknowledgments
-------

This project uses sources/libraries from the following:

1. [ELFinder](https://github.com/Studio-42/elFinder)
2. [ELFinder.NET](https://elfinder.codeplex.com/)
3. [ImageProcessor](http://imageprocessor.org/)
4. [NancyFX](http://nancyfx.org/)

License
-------

ELFinder Connector for .NET is issued under a 3-clauses BSD license.

<pre>
Copyright (c) 2016, Patrick Cloutier
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice,
  this list of conditions and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

* Neither the name of ELFinder.Connector.NET nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
</pre>
