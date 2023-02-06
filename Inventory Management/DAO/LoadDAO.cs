using Inventory_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.DAO
{
    public class LoadDAO
    {
        private static LoadDAO instance;

        public static LoadDAO Instance
        {
            get { if (instance == null) instance = new LoadDAO(); return LoadDAO.instance; }
            private set { LoadDAO.instance = value; }
        }

        
        private LoadDAO() { }

        public List<Load> LoadMenu()
        {
            List<Load> Load = new List<Load>();

            string query = "USP_Load";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Load load = new Load(item);
                Load.Add(load);
            }

            return Load;
        }
    }

    
}
