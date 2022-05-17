using Tp5.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Tp5.DataAccessLayer.Factories
{
    public class MenuFactory
    {
        private Menu CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nom = mySqlDataReader["Description"].ToString();
            string imagePath = mySqlDataReader["ImagePath"].ToString();

            return new Menu(id,nom,imagePath);
        }

        public Menu CreateEmpty()
        {
            return new Menu(0, string.Empty, string.Empty);
        }

        public Menu[] GetAll()
        {
            List<Menu> menus = new List<Menu>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuChoices ORDER BY Description";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    menus.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }
            return menus.ToArray();
        }

        public Menu Get(int id)
        {
            Menu menu = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuChoices WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    menu = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return menu;
        }

        public void Save(Menu menu)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (menu.id == 0)
                {
                    // On sait que c'est un nouveau produit avec Id == 0,
                    // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                    mySqlCmd.CommandText = "INSERT INTO tp5_menuchoices(Description,ImagePath) " +
                                           "VALUES (@Description,@ImagePath)";
                }
                else
                {
                    mySqlCmd.CommandText = "UPDATE tp5_menuchoices " +
                                           "SET Id=@Id, Description=@Description, ImagePath=@ImagePath " +
                                           "WHERE Id=@Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", menu.id);
                    
                }
                if(menu.nom != null)
                {
                    mySqlCmd.Parameters.AddWithValue("@Description", menu.nom.Trim());
                    mySqlCmd.Parameters.AddWithValue("@ImagePath", menu.ImagePath);
                    mySqlCmd.ExecuteNonQuery();
                }
               
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }

        public void Delete(int id)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "DELETE FROM tp5_menuchoices WHERE Id=@Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);
                mySqlCmd.ExecuteNonQuery();
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }
    }
}
