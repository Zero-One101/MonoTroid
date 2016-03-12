using System;

namespace Editor
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new MainForm();
            form.Show();
            using (var game = new Game1(form))
                game.Run();
        }
    }
#endif
}
