using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models;

namespace Tp5.DataAccessLayer.Factories
{
    public class ReservationFactory
    {
        private Reservation CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            int nbPersonne = (int)mySqlDataReader["NbPersonne"];
            int menuChoiceId = (int)mySqlDataReader["MenuChoiceId"];
            string nom = mySqlDataReader["Nom"].ToString();
            string courriel = mySqlDataReader["Courriel"].ToString();
            DateTime date = (DateTime)mySqlDataReader["DateReservation"];

            return new Reservation(id,nbPersonne,menuChoiceId,nom,courriel,date);
        }

        public Reservation CreateEmpty()
        {
            return new Reservation(0,0,0, string.Empty, string.Empty,DateTime.Now);
        }

        public Reservation[] GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations ORDER BY DateReservation Desc";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    reservations.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservations.ToArray();
        }

        public Reservation Get(int id)
        {
            Reservation reservation = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    reservation = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservation;
        }

        public void Save(Reservation reservation)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (reservation.id == 0)
                {
                    // On sait que c'est un nouveau produit avec Id == 0,
                    // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                    mySqlCmd.CommandText = "INSERT INTO tp5_reservations(Nom, Courriel, NbPersonne, DateReservation, MenuChoiceId) " +
                                           "VALUES (@Nom, @Courriel, @NbPersonne, @DateReservation, @MenuChoiceId)";
                }
                else
                {
                    mySqlCmd.CommandText = "UPDATE tp5_reservations " +
                                           "SET Id=@Id, Nom=@Nom, Courriel=@Courriel, NbPersonne=@NbPersonne, DateReservation=@DateReservation, MenuChoiceId=@MenuChoiceId," +
                                           "WHERE Id=@Id";

                    mySqlCmd.Parameters.AddWithValue("@Id", reservation.id);
                }

                mySqlCmd.Parameters.AddWithValue("@Id", reservation.id);
                mySqlCmd.Parameters.AddWithValue("@Nom", reservation.nom.Trim());
                mySqlCmd.Parameters.AddWithValue("@Courriel", reservation.courriel.Trim());
                mySqlCmd.Parameters.AddWithValue("@NbPersonne", reservation.nbPersonne);
                mySqlCmd.Parameters.AddWithValue("@DateReservation", reservation.date);
                mySqlCmd.Parameters.AddWithValue("@MenuChoiceId", reservation.menuChoiceId);

                mySqlCmd.ExecuteNonQuery();

                if (reservation.id == 0)
                {
                    // Si c'était un nouveau produit (requête INSERT),
                    // nous affectons le nouvel Id de l'instance au cas où il serait utilisé dans le code appelant.
                    reservation.id = (int)mySqlCmd.LastInsertedId;
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
                mySqlCmd.CommandText = "DELETE FROM tp5_reservations WHERE Id=@Id";
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

