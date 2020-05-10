using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesktopKanajoPatcher;
using dnlib.DotNet;
using Microsoft.Win32;

namespace DesktopKanojoPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 200;
            Console.WriteLine(@" ______   _______  _______  ___   _  _______  _______  _______    ___   _  _______  __    _  _______      ___  _______    _______  _______  _______  _______  __   __  _______  ______   ");
            Console.WriteLine(@"|      | |       ||       ||   | | ||       ||       ||       |  |   | | ||   _   ||  |  | ||       |    |   ||       |  |       ||   _   ||       ||       ||  | |  ||       ||    _ |  ");
            Console.WriteLine(@"|  _    ||    ___||  _____||   |_| ||_     _||   _   ||    _  |  |   |_| ||  |_|  ||   |_| ||   _   |    |   ||   _   |  |    _  ||  |_|  ||_     _||       ||  |_|  ||    ___||   | ||  ");
            Console.WriteLine(@"| | |   ||   |___ | |_____ |      _|  |   |  |  | |  ||   |_| |  |      _||       ||       ||  | |  |    |   ||  | |  |  |   |_| ||       |  |   |  |       ||       ||   |___ |   |_||_ ");
            Console.WriteLine(@"| |_|   ||    ___||_____  ||     |_   |   |  |  |_|  ||    ___|  |     |_ |       ||  _    ||  |_|  | ___|   ||  |_|  |  |    ___||       |  |   |  |      _||       ||    ___||    __  |");
            Console.WriteLine(@"|       ||   |___  _____| ||    _  |  |   |  |       ||   |      |    _  ||   _   || | |   ||       ||       ||       |  |   |    |   _   |  |   |  |     |_ |   _   ||   |___ |   |  | |");
            Console.WriteLine(@"|______| |_______||_______||___| |_|  |___|  |_______||___|      |___| |_||__| |__||_|  |__||_______||_______||_______|  |___|    |__| |__|  |___|  |_______||__| |__||_______||___|  |_|");
            Console.WriteLine();
            Console.WriteLine("==========================================================================================================================================================================================");
            const int AppID = 1284820;
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    if (proc.MainModule.FileName.Contains("DesktopKanojo.exe"))
                        proc.Kill();
                }
                catch
                {

                }

            }
            do
            {
                bool PatchMove = false;
                Console.WriteLine("Input the FPS you want:");
                var input = Console.ReadLine();
                Console.WriteLine("Do you want increase character next move time? (Y or any key to abort)");
                var yn = Console.ReadLine();
                if(yn == "y" || yn == "Y")
                {
                    PatchMove = true;
                }
                if (int.TryParse(input, out int result))
                {
                    var sw = Stopwatch.StartNew();
                    Console.WriteLine(" Searching for Steam installation...");
                    var steamPath = default(string);
                    try { steamPath = Steam.Path; }
                    catch (DirectoryNotFoundException)
                    {
                        Console.Error.WriteLine(" Unable to find Steam installation.");
                        Console.ReadLine();
                        return;
                    }
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Path -> " + steamPath);
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Searching for Desktop Kanojo installation...");
                    var Kanojo = default(SteamApp);
                    if (!Steam.Apps.TryGetValue(AppID, out Kanojo))
                    {
                        Console.WriteLine(" Unable to find Desktop Kanojo manifest.");
                        Console.ReadLine();
                        return;
                    }
                    var kanojoPath = default(string);
                    try
                    {
                        kanojoPath = Kanojo.Path;
                    }
                    catch
                    {
                        Console.WriteLine(" Unable to parse Desktop Kanojo manifest.");
                        Console.ReadLine();
                        return;
                    }
                    if (!Directory.Exists(kanojoPath))
                    {
                        Console.WriteLine(" Unable to find Desktop Kanojo installation directory.");
                        Console.ReadLine();
                        return;
                    }
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Path -> " + kanojoPath);
                    var DesktopKanojo = new Kanojo(kanojoPath);
                    if (!Directory.Exists(Path.Combine(DesktopKanojo.ManagedPath, "backup")))
                    {
                        Console.WriteLine(" Backups -> ");
                        var dlls = Directory.GetFiles(DesktopKanojo.ManagedPath, "*.dll");
                        Directory.CreateDirectory(Path.Combine(DesktopKanojo.ManagedPath, "backup"));
                        foreach (var dll in dlls)
                        {
                            var fileName = Path.GetFileName(dll);
                            var dst = Path.Combine(DesktopKanojo.ManagedPath, "backup", fileName);
                            File.Copy(dll, dst, true);
                            Console.WriteLine(new string(' ', " Games -> ".Length) + fileName + " -> " + "backup/" + fileName);
                        }
                    }
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Patches -> ");
                    PatchFPS patch = new PatchFPS();
                    patch.FPS = result;
                    patch.Apply(DesktopKanojo);
                    if (PatchMove)
                    {
                        PatchMovement patchMove = new PatchMovement();
                        patchMove.Apply(DesktopKanojo);
                    }
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Writing new assemblies...");
                    try { DesktopKanojo.Save("patched"); }
                    catch(Exception ex) { Console.Error.WriteLine("Failed to write." + ex.ToString()); }
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Removing Temp files...");
                    DesktopKanojo.Dispose();
                    foreach (var file in Directory.GetFiles("patched"))
                    {
                        var destination = Path.Combine(kanojoPath, "DesktopKanojo_Data", "Managed") + file.Substring(file.LastIndexOf('\\'));
                        File.Delete(destination);
                        File.Copy(file, destination);
                        File.Delete(file);
                    }
                    Directory.Delete("patched");
                    Console.WriteLine(" Done patched!");
                    Process.Start(Path.Combine(kanojoPath , "DesktopKanojo.exe"));
                    Console.WriteLine(" -----------------------------------");
                    Console.WriteLine(" Brought to you by PoH98!");
                    break;
                }
                else
                {
                    Console.WriteLine("Are you kidding me? Is this a number for FPS?");
                    Console.ReadLine();
                    return;
                }
            }
            while (true);
            Console.ReadLine();
        }
    }

    public abstract class Patch
    {
        public abstract void Apply(Kanojo Kanojo);
    }

    public class Kanojo:IDisposable
    {
        private readonly string _managedPath;
        private readonly ModuleContext _moduleCtx;
        private readonly ModuleDefMD _assemblyCSharp;
        private readonly ModuleDefMD _assemblyCSharpFirstpass;
        private readonly ModuleDefMD _unityEngine;
        private readonly Dictionary<string, ModuleDefMD> _modules;
        public Kanojo(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var resolver = new AssemblyResolver();
            var moduleCtx = new ModuleContext(resolver);
            resolver.DefaultModuleContext = moduleCtx;
            resolver.EnableTypeDefCache = true;

            _moduleCtx = moduleCtx;

            _modules = new Dictionary<string, ModuleDefMD>();
            _managedPath = Path.Combine(path, "DesktopKanojo_Data", "Managed");

            _assemblyCSharp = GetModule("Assembly-CSharp.dll");
            _assemblyCSharpFirstpass = GetModule("Assembly-CSharp-firstpass.dll");
            _unityEngine = GetModule("UnityEngine.dll");
        }

        public ModuleDefMD AssemblyCSharp => _assemblyCSharp;
        public ModuleDefMD AssemblyCSharpFirstpass => _assemblyCSharpFirstpass;
        public ModuleDefMD UnityEngine => _unityEngine;

        public string ManagedPath => _managedPath;

        public void Dispose()
        {
            _modules.Clear();
            _unityEngine.Dispose();
            _assemblyCSharpFirstpass.Dispose();
            _assemblyCSharp.Dispose();
            _assemblyCSharp.Dispose();
        }

        public ModuleDefMD GetModule(string name)
        {
            var modulePath = Path.Combine(_managedPath, name);

            var module = default(ModuleDefMD);
            if (!_modules.TryGetValue(modulePath, out module))
            {
                module = ModuleDefMD.Load(modulePath, _moduleCtx);
                module.EnableTypeDefFindCache = true;

                ((AssemblyResolver)_moduleCtx.AssemblyResolver).AddToCache(module);
                _modules.Add(modulePath, module);
            }

            return module;
        }

        public void Save(string outDir)
        {
            Directory.CreateDirectory(outDir);
            foreach (var keyValue in _modules)
            {
                var src = keyValue.Key;
                var dst = Path.Combine(outDir, Path.GetFileName(src));

                keyValue.Value.Write(dst);
            }
        }
    }

    public static class Steam
    {
        private delegate string PathFinder();

        private readonly static Dictionary<int, SteamApp> _apps;
        private readonly static string _path;

        public static string Path => _path;
        public static Dictionary<int, SteamApp> Apps => _apps;

        static Steam()
        {
            _path = GetSteamPath();
            _apps = GetSteamApps();
        }

        private static Dictionary<int, SteamApp> GetSteamApps()
        {
            var apps = new Dictionary<int, SteamApp>();
            var steamAppsPath = System.IO.Path.Combine(Path, "SteamApps");
            if (steamAppsPath == null)
                throw new DirectoryNotFoundException("Unable to find Steam apps directory. " + steamAppsPath);

            var manifests = Directory.GetFiles(steamAppsPath, "appmanifest_*.acf");
            foreach (var manifest in manifests)
            {
                var idStart = manifest.IndexOf("_") + 1;
                var idEnd = manifest.IndexOf(".acf");

                var idStr = manifest.Substring(idStart, idEnd - idStart);
                var id = -1;
                if (!int.TryParse(idStr, out id))
                    continue;

                var steamApp = new SteamApp(manifest);
                apps.Add(id, steamApp);
            }

            return apps;
        }

        private static string GetSteamPath()
        {
            var finders = new PathFinder[]
            {
                () => {
                    /*
                        Find the steam install path by using the HKEY_CURRENT_USER\Software\Valve\Steam\SteamPath value.
                        Which does not require admin access.
                     */
                    using(var softwareReg = Registry.CurrentUser.OpenSubKey("software"))
                    using(var valveReg = softwareReg.OpenSubKey("valve"))
                    using(var steamReg = valveReg.OpenSubKey("steam"))
                    {
                       if (steamReg == null)
                            return null;

                        var steamPath = steamReg.GetValue("SteamPath") as string;
                        return steamPath;
                    }
                },
                () => {
                    /*
                        Find the steam install path by using the HKEY_LOCAL_MACHINE\Software\WOW6432Node\Valve\Steam\InstallPath value.
                        Which does **requires** admin access. I think.
                        This is for 64-bit.
                     */
                    using(var softwareReg = Registry.LocalMachine.OpenSubKey("software"))
                    using(var wow6432nodeReg = softwareReg.OpenSubKey("wow6432node"))
                    using(var valveReg = wow6432nodeReg.OpenSubKey("valve"))
                    using(var steamReg = valveReg.OpenSubKey("steam"))
                    {
                        if (steamReg == null)
                            return null;

                        var installPath = steamReg.GetValue("InstallPath") as string;
                        return installPath;
                    }
                },
                () => {
                    /*
                        Find the steam install path by using the HKEY_LOCAL_MACHINE\Software\WOW6432Node\Valve\Steam\InstallPath value.
                        Which does **requires** admin access. I think.
                        This is for 32-bit.
                     */
                    using(var softwareReg = Registry.LocalMachine.OpenSubKey("software"))
                    using(var valveReg = softwareReg.OpenSubKey("valve"))
                    using(var steamReg = valveReg.OpenSubKey("steam"))
                    {
                        if (steamReg == null)
                            return null;

                        var installPath = steamReg.GetValue("InstallPath") as string;
                        return installPath;
                    }
                },
                () => {
                    /*
                        Find the steam install path by manually looking for it in the program files
                        This is for 32-bit.
                     */
                    var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                    var steamPath = System.IO.Path.Combine(programFilesX86, "Steam");
                    if (!Directory.Exists(steamPath))
                        return null;

                    return steamPath;
                },
                () => {
                    /*
                        Find the steam install path by manually looking for it in the program files
                        This is for 64-bit.
                     */
                    var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    var steamPath = System.IO.Path.Combine(programFilesX86, "Steam");
                    if (!Directory.Exists(steamPath))
                        return null;

                    return steamPath;
                }
            };

            var path = default(string);
            foreach (var finder in finders)
            {
                try { path = finder(); }
                catch { continue; }

                if (path != null)
                    break;
            }

            if (path == null)
                throw new DirectoryNotFoundException("Unable to find steam installation directory path.");

            return System.IO.Path.GetFullPath(path);
        }
    }

    public class SteamApp
    {
        private bool _loaded;

        private int _id;
        private string _path;
        private string _name;
        private readonly string _manifestPath;

        public SteamApp(string manifestPath)
        {
            if (manifestPath == null)
                throw new ArgumentNullException(nameof(manifestPath));

            _loaded = false;
            _manifestPath = manifestPath;
        }

        public int Id
        {
            get
            {
                if (!_loaded)
                    Load();

                return _id;
            }
        }

        public string Path
        {
            get
            {
                if (!_loaded)
                    Load();

                return _path;
            }
        }

        public string Name
        {
            get
            {
                if (!_loaded)
                    Load();

                return _name;
            }
        }

        private void Load()
        {
            /*var keyValues = KeyValues.Parse(_manifestPath);*/

            var data = File.ReadAllText(_manifestPath);

            var appid = int.Parse(GetValue(data, "appid"));
            var path = GetValue(data, "installdir");
            var name = GetValue(data, "name");

            _id = appid;
            _path = System.IO.Path.Combine(Steam.Path, "SteamApps", "common", path);
            _name = name;
            _loaded = true;
        }

        private string GetValue(string keyValue, string key)
        {
            var actualKey = "\"" + key + "\"";
            var keyStart = keyValue.IndexOf(actualKey);
            var index = keyStart + actualKey.Length;
            var token = string.Empty;

            while (true)
            {
                if (index >= keyValue.Length)
                    break;

                var c = keyValue[index];
                if (char.IsWhiteSpace(c))
                {
                    index++;
                    continue;
                }

                if (c == '"')
                {
                    while (true)
                    {
                        c = keyValue[++index];
                        if (c == '"')
                            return token;

                        token += c;
                    }
                }
            }

            return null;
        }
    }
}
