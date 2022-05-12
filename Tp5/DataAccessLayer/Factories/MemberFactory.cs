using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models;

namespace Tp5.DataAccessLayer.Factories
{
    public class MemberFactory
    {
        private Member CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nom = mySqlDataReader["Nom"].ToString();
            string courriel = mySqlDataReader["Courriel"].ToString();
            string user = mySqlDataReader["NomUtilisateur"].ToString();
            string mdp = mySqlDataReader["MotPasse"].ToString();
            string role = mySqlDataReader["Role"].ToString();

            return new Member(id, nom,courriel,user,mdp,role);
        }

        public Member CreateEmpty()
        {
            return new Member(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public Member[] GetAll()
        {
            List<Member> members = new List<Member>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_members ORDER BY Nom";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    members.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }
            return members.ToArray();
        }

        public Member Get(string sid)
        {
            if (int.TryParse(sid, out int id))
            {
                return Get(id);
            }

            return null;
        }

        public Member Get(int id)
        {
            Member member = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_members WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    member = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return member;
        }

        public Member GetByEmail(string courriel)
        {
            Member member = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_members WHERE Courriel = @Courriel";
                mySqlCmd.Parameters.AddWithValue("@Courriel", courriel);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    member = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return member;
        }

        public Member GetByUsername(string nomUtilisateur)
        {
            Member member = null;
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_members WHERE NomUtilisateur = @NomUtilisateur";
                mySqlCmd.Parameters.AddWithValue("@NomUtilisateur", nomUtilisateur);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    member = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return member;
        }

        public void Save(Member member)
        {
            MySqlConnection mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (member.Id == 0)
                {
                    // On sait que c'est un nouveau produit avec Id == 0,
                    // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                    mySqlCmd.CommandText = "INSERT INTO tp5_members(Nom, Courriel, NomUtilisateur, MotPasse, Role) " +
                                           "VALUES (@Name, @Email, @Username, @password, @Role)";
                }
                else
                {
                    mySqlCmd.CommandText = "UPDATE tp5_members " +
                                           "Nom = @Name, Courriel = @Email, NomUtilisateur = @Username, MotPasse = @password, Role = @Role," +
                                           "WHERE Id=@Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", member.Id);

                }
                mySqlCmd.Parameters.AddWithValue("@Name", member.Name.Trim());
                mySqlCmd.Parameters.AddWithValue("@Email", member.Email.Trim());
                mySqlCmd.Parameters.AddWithValue("@Username", member.Username.Trim());
                mySqlCmd.Parameters.AddWithValue("@password", member.Password.Trim());
                mySqlCmd.Parameters.AddWithValue("@Role", member.Role.Trim());

                mySqlCmd.ExecuteNonQuery();
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
                mySqlCmd.CommandText = "DELETE FROM tp5_members WHERE Id=@Id";
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
