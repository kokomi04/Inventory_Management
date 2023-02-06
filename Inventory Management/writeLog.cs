using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management
{
    public class writeLog
    {
        public static void Log(string nameProduct, int count, string unit, string nameUser, string status)
        {

            string logPath = string.Format(@"{0}\Log", Environment.CurrentDirectory);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            string log = string.Format(@"{0}\Log\{1}.txt", Environment.CurrentDirectory, DateTime.Now.ToString("dd-MM-yyyy"));

            StreamWriter sw = new StreamWriter(log, true);

            sw.WriteLine(string.Format("{0}\t {1}",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), status));
            sw.WriteLine(string.Format("\t{0}\t{1}\t{2}\t\t{3}\n", "Tên hàng", "Số lượng", "Đơn vị", "Người nhập"));
            sw.WriteLine(string.Format("\t{0}\t\t{1}\t\t{2}\t\t{3}\n", nameProduct, count, unit, nameUser));
            sw.Flush();
            sw.Close();
        }
    }
}
