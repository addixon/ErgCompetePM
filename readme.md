# ErgCompetePM  
Built in .NET 5.0, this application establishes and maintains communication wtih a Concept2 PerformanceMonitor. Standard workout data is polled at minimum intervals and, if desired, forwarded to a listening SignalR hub.  
  
This application leverages LibUsb (by means of LibUsbDotNet) and is therefore platform-independent. The PM3 libraries provided by CSAFE were dlls compiled for Windows only and for that reason were not compatible with my project.  
  
Additionally, while many CSAFE and PM3 commands are not currently used in this application, they do exist and can be leveraged. Many SET commands include convenience constructors to allow a friendly invocation of them with their relevant data.  
  
## Known issues  
At this time, the Runtime Identifiers do not seem to be respected. These are used in establishing the correct libusb library to use. A workaround is to manually specify the desired library based on OS in LibUsbDotNet.LibUsbDotNet.Generated.NativeMethods.cs  
  
## Recognitions  
In the process of converting this application from the PM3 libraries to a lower-level usb implementation, there were a few projects that helped provide insight into troubleshooting and communication standards. Those are:  
- https://github.com/wemakewaves/PyRow  
- https://github.com/spinglass/Performant  
- https://github.com/MattFiler/Concept2API  
- https://www.c2forum.com  
- LibUsbDotNet library - this is found as a NuGet package, but I needed expanded functionality to detach the USB device from the host in Linux, so a modified version is included in this solution  
  
### Notes  
This is one of my first attempts at device communication, so I am sure that there are many areas for improvement. This code is also in-development and there are many areas of cleanup that need to be performed left over from various iterations of experimentation in addition to some functionality being pulled into additional services. Exception handling throughout the project also needs to be improved and made consistent. That said, I do hope that it proves useful to someone who is wanting to integrate with a Performance Monitor via the CSAFE protocols.