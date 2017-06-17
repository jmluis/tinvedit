using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using RollbarDotNet;
using TerrariaInvEdit.Properties;
using TerrariaInvEdit.UI.Forms;

namespace TerrariaInvEdit
{
    static class Program
    {
        public static MainForm MainF;

        [STAThread]
        static void Main()
        {
            RollbarConfig config = new RollbarConfig(Resources.Rollbar_Access);
#if DEBUG
            config.Environment = "development";
#endif
            Rollbar.Init(config);

            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.ThreadException += (sender, args) =>
            {
                Rollbar.Report(args.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                    {
                        Rollbar.Report(args.ExceptionObject as System.Exception);
                    };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Rollbar.Init(new RollbarConfig(Resources.Rollbar_Access));
            Application.Run(MainF = new MainForm());
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(args.Name);

            string path = assemblyName.Name + ".dll";
            using (Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null)
                    return null;

                byte[] assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }
    }
}
