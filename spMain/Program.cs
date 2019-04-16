using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ionic.Zip;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
namespace spMain
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ys();
            Application.Run(new mjpeg.Sp());
            //Application.Run(new mjpeg.frmW());
        }
        public static void ys()
        {
            //using Ionic.Zip;
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            string p1 = currentDirectory + @"ibvlc1";
            if (!System.IO.Directory.Exists(p1))
            {
                Task t1 = Task.Factory.StartNew(jy);
            }


        }
        public static void jy()
        {
            try
            {
                var currentAssembly = Assembly.GetEntryAssembly();
                var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
               // string p1 = currentDirectory + @"ibvlc1";
                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                using (ZipFile zip = ZipFile.Read(@"libvlc.zip", options))
                {
                    zip.ExtractAll(currentDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch(Exception ex )
            {
               
            }

        }
    }
}
