using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.DAO
{
    public class InoutDAO
    {
        private static InoutDAO instance;

        public static InoutDAO Instance
        {
            get { if (instance == null) instance = new InoutDAO(); return InoutDAO.instance; }
            private set { InoutDAO.instance = value; }
        }
        private InoutDAO() { }

        public int Nhap(int id, int count, int type, string nameUser, DateTime? dateEdit, string note)
        {
            string query = "USP_Nhap @id , @count , @type , @nameUser , @dateEdit , @note";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, count, type, nameUser, dateEdit, note});
        }

        public int Xuat(int id, int count, int type, string nameUser, DateTime? dateEdit, string note)
        {
            string query = "USP_Xuat @id , @count , @type , @nameUser , @dateEdit , @note";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, count, type, nameUser, dateEdit, note });
        }

        public void AddInout(int bo1, int bo2)
        {
            string query = "EXEC AddInout @bo1 , @bo2";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { bo1, bo2 });
        }
    }
}
