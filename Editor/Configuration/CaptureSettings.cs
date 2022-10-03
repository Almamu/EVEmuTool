using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVEmuTool.Configuration
{
    static internal class CaptureSettings
    {
        public static string ServerAddress = "127.0.0.1";
        public static string ServerPort = "25999";
        public static string ServerAutoStart = "False";

        public static void LoadSettingsFromRegistry()
        {
            RegistryKey key = null;

            try
            {
                key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true)?.CreateSubKey("EVEmu")?.CreateSubKey("LivePacketEditor");
            }
            catch(Exception)
            {

            }

            if (key is null)
            {
                MessageBox.Show("There was an error accessing the registry to store/retrieve settings. Your settings won't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ServerAddress = (string) key.GetValue("serverAddress", "127.0.0.1");
            ServerPort = (string) key.GetValue("serverPort", "25999");
            ServerAutoStart = (string)key.GetValue("serverAutoStart", "False");

            // check that the server port is valid, otherwise reset it and store everything back
            if (ushort.TryParse(ServerPort, out _) == false)
                ServerPort = "25999";

            key.SetValue("serverAddress", ServerAddress);
            key.SetValue("serverPort", ServerPort);
            key.SetValue("serverAutoStart", ServerAutoStart);
            key.Close();
        }

        public static bool SaveSettingsToRegistry()
        {
            RegistryKey key = null;

            try
            {
                key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true)?.CreateSubKey("EVEmu")?.CreateSubKey("LivePacketEditor");
            }
            catch (Exception)
            {

            }

            if (key is null)
            {
                MessageBox.Show("There was an error accessing the registry to store/retriev settings. Your settings won't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            key.SetValue("serverAddress", ServerAddress);
            key.SetValue("serverPort", ServerPort);
            key.SetValue("serverAutoStart", ServerAutoStart);
            key.Close();

            return true;
        }
    }
}
