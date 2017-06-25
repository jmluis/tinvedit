/*
       This file is part of Terraria Inventory Editor
                            Copyright © 2017 Jose Luis, Anthony Wolfe

    Terraria Inventory Editor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Terraria Inventory Editor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Terraria Inventory Editor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection;
using System.Windows.Forms;
using RollbarDotNet;
using TerrariaInvEdit.Properties;
using TerrariaInvEdit.UI.Forms;
using Exception = System.Exception;

namespace TerrariaInvEdit
{
    internal static class Program
    {
        public static MainForm MainF;

        [STAThread]
        private static void Main()
        {
            var config = new RollbarConfig(Resources.Rollbar_Access);
#if DEBUG
            config.Environment = "development";
#endif
            Rollbar.Init(config);

            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.ThreadException += (sender, args) => { Rollbar.Report(args.Exception); };
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Rollbar.Report(args.ExceptionObject as Exception);
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Rollbar.Init(new RollbarConfig(Resources.Rollbar_Access));
            Application.Run(MainF = new MainForm());
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";
            using (var stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null)
                    return null;

                var assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }
    }
}