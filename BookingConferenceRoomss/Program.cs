using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookingConferenceRooms;

namespace BookingConferenceRoomss
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Указываем, что DataDirectory — это папка с программой
            AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath);

            // Автоматически создаём базу данных при первом запуске
            DatabaseInitializer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
