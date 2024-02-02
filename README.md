# ![](https://realityripple.com/Software/Applications/Please-Stand-By/favicon-32x32.png) Please Stand By...
Monitor Standby with the click of a mouse or the press of a key.

#### Version 1.4
> Author: Andrew Sachen  
> Created: July 9, 2010  
> Updated: February 2, 2024

Language: Visual Basic.NET  
Compiler: Visual Studio 2010  
Framework: Version 4.0+

## Building
This application can be compiled using Visual Studio 2010 or newer, however an Authenticode-based Digital Signature check is integrated into the code to prevent incorrectly-signed or unsigned copies from running. Comment out lines 10-18 of `/Please Stand By/ApplicationEvents.vb` to disable this signature check before compiling if you wish to build your own copy.

This application is *not* designed to support Mono/Xamarin compilation and may not work on Linux or OS X systems. In particular, there are a few API calls used by this application: "GetLastInputInfo", "RegisterHotKey" and "UnregsterHotKey", as well as "PostMessage".

## Download
You can grab the latest release from the [Official Web Site](https://realityripple.com/Software/Applications/Please-Stand-By/).

## License
This is free and unencumbered software released into the public domain, supported by donations, not advertisements. If you find this software useful, [please support it](https://realityripple.com/donate.php?itm=Please+Stand+By...)!
