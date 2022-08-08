using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(CVRAvatarHider.BuildInfo.Name)]
[assembly: AssemblyDescription("CVRAvatarHider.BuildInfo.Description")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct(CVRAvatarHider.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + CVRAvatarHider.BuildInfo.Author + " with code snippets from Dusks, replaceable, and the VRC AvatarHider mod from ImTiara & loukylor")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(CVRAvatarHider.AvatarHider), 
    CVRAvatarHider.BuildInfo.Name, 
    CVRAvatarHider.BuildInfo.Version, 
    CVRAvatarHider.BuildInfo.Author)]
[assembly: MelonColor(System.ConsoleColor.DarkMagenta)]
[assembly: MelonGame("Alpha Blend Interactive", "ChilloutVR")]
// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("da25c190-0798-445b-8114-c2d1a7a89b26")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(CVRAvatarHider.BuildInfo.Version)]
[assembly: AssemblyFileVersion(CVRAvatarHider.BuildInfo.Version)]