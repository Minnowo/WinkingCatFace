using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinkingCat.HelperLibs;

namespace RegistryHelper
{
    public class Program
    {
        public const string ApplicationEntryName = ";3";
        static void Main(string[] args)
        {
            Console.ReadLine();
            //AddContextMenuItem(".zip", "ZipStrip", "Open with &ZipStrip", Application.ExecutablePath + " %1");
        }
        private bool AddContextMenuItem(string Extension, string MenuName, string MenuDescription, string MenuCommand)
            {
                bool ret = false;
                RegistryKey rkey =
                    Registry.ClassesRoot.OpenSubKey(Extension);
                if (rkey != null)
                {
                    string extstring = rkey.GetValue("").ToString();
                    rkey.Close();
                    if (extstring != null)
                    {
                        if (extstring.Length > 0)
                        {
                            rkey = Registry.ClassesRoot.OpenSubKey(
                                extstring, true);
                            if (rkey != null)
                            {
                                string strkey = "shell\\" + MenuName + "\\command";
                                RegistryKey subky = rkey.CreateSubKey(strkey);
                                if (subky != null)
                                {
                                    subky.SetValue("", MenuCommand);
                                    subky.Close();
                                    subky = rkey.OpenSubKey("shell\\" +
                                        MenuName, true);
                                    if (subky != null)
                                    {
                                        subky.SetValue("", MenuDescription);
                                        subky.Close();
                                    }
                                    ret = true;
                                }
                                rkey.Close();
                            }
                        }
                    }
                }
                return ret;
        }
        public static void AddOption_ContextMenu()
        {
            RegistryKey _key = Registry.ClassesRoot.OpenSubKey("Directory\\Background\\Shell", true);
            RegistryKey newkey = _key.CreateSubKey(ApplicationEntryName);
            RegistryKey subNewkey = newkey.CreateSubKey("Command");
            subNewkey.SetValue("", "C:\\yourApplication.exe");
            subNewkey.Close();
            newkey.Close();
            _key.Close();
        }

        private void RemoveOption_ContextMenu()
        {
            RegistryKey _key = Registry.ClassesRoot.OpenSubKey("Directory\\Background\\Shell\\", true);
            _key.DeleteSubKey(ApplicationEntryName);
            _key.Close();
        }
    }
}
