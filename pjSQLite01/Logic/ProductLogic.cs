using pjSQLite01.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjSQLite01.Logic
{
    public class ProductLogic
    {
        private static string connection = ConfigurationManager.ConnectionStrings["AtunConnection"].ConnectionString;

        private static ProductLogic _Instance = null;

        public static ProductLogic Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ProductLogic();
                }
                return _Instance;
            }
        }

        public bool Save(product obj)
        {
            bool ask = true;
            
            using (SQLiteConnection connec = new SQLiteConnection(connection))
            {
                connec.Open();
                string query = "INSERT INTO Product(ProductName,ProductType) VALUES (@productName,@productType)";

                SQLiteCommand cmd = new SQLiteCommand(query, connec);
                cmd.Parameters.Add(new SQLiteParameter("@productName", obj.ProductName));
                cmd.Parameters.Add(new SQLiteParameter("@productType", obj.ProductType));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    ask = false;
                }

            }

            return ask;
        }

        public List<product> lst()
        { 
            List<product> olst = new List<product>();
            using (SQLiteConnection connec = new SQLiteConnection(connection))
            {
                connec.Open();
                string query = "SELECT ProductID,ProductName,ProductType FROM product";
                SQLiteCommand cmd = new SQLiteCommand(query, connec);
                cmd.CommandType = System.Data.CommandType.Text;

                using (SQLiteDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olst.Add(new product()
                        {
                            ProductID = int.Parse(dr["ProductID"].ToString()),
                            ProductName = dr["ProductName"].ToString(),
                            ProductType = dr["ProductType"].ToString()
                        });

                    }
                }
            }

            return olst;
        }

        public bool Edit(product obj)
        {
            bool ask = true;

            using (SQLiteConnection connec = new SQLiteConnection(connection))
            {
                connec.Open();
                string query = "UPDATE Product set ProductName = @productName, ProductType = @productType WHERE ProductID = @productID";

                SQLiteCommand cmd = new SQLiteCommand(query, connec);
                cmd.Parameters.Add(new SQLiteParameter("@productID", obj.ProductID));
                cmd.Parameters.Add(new SQLiteParameter("@productName", obj.ProductName));
                cmd.Parameters.Add(new SQLiteParameter("@productType", obj.ProductType));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    ask = false;
                }

            }

            return ask;
        }

        public bool Delete(product obj)
        {
            bool ask = true;

            using (SQLiteConnection connec = new SQLiteConnection(connection))
            {
                connec.Open();
                string query = "DELETE FROM Product WHERE ProductID = @productID";

                SQLiteCommand cmd = new SQLiteCommand(query, connec);
                cmd.Parameters.Add(new SQLiteParameter("@productID", obj.ProductID));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    ask = false;
                }

            }

            return ask;
        }
    }
}
