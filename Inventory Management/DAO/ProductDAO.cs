using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get { if (instance == null) instance = new ProductDAO(); return ProductDAO.instance; }
            private set { ProductDAO.instance = value; }
        }
        private ProductDAO() { }

        public void AddProduct(string nameProduct)
        {
            string query = "AddProduct @nameProduct";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] {nameProduct});
        }
        public void DelProduct(int id)
        {
            string query = "USP_DelInout @id";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
        }
        public void EditProduct(int id, int bo1, int bo2)
        {
            string query = "USP_Edit @id , @bo1 , @bo2";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, bo1, bo2});
        }
    }
}
