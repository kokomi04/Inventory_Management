using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.DTO
{
    public class Load
    {
        public Load(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["Tên hàng"].ToString();
            this.Ngoaichuyen = (int)row["Ngoài chuyền"];
            this.Trongkho = (int)row["Trong kho"];
            this.Mica = (int)row["(Lẻ Mica)"];
            this.Tray = (int)row["(Lẻ Tray)"];
            this.Note = row["Ghi chú"].ToString();
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        private int tray;

        public int Tray
        {
            get { return tray; }
            set { tray = value; }
        }

        private int mica;

        public int Mica
        {
            get { return mica; }
            set { mica = value; }
        }

        private int trongkho;

        public int Trongkho
        {
            get { return trongkho; }
            set { trongkho = value; }
        }

        private int ngoaichuyen;

        public int Ngoaichuyen
        {
            get { return ngoaichuyen; }
            set { ngoaichuyen = value; }
        }

        private string nameProduct;

        public string NameProduct
        {
            get { return nameProduct; }
            set { nameProduct = value; }
        }
    }
}
