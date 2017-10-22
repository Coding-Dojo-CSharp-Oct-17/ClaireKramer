using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dojoleague.Models;

namespace dojoleague.Factory {
    public class DojoFactory : IFactory<Dojo> {
        private string connectionString;
        public DojoFactory () {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoleague;SslMode=None";
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void AddDojo(Dojo item) {
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO dojos (name, location, info) VALUES (@Name, @Location, @Info)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Dojo> FindAll() {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }

        public Dojo GetById(int id) {
            using (IDbConnection dbConnection = Connection) {
                string query = "SELECT * FROM dojos WHERE (Id=@Id)";
                dbConnection.Open();
                return dbConnection.Query<Dojo>(query, new {Id = id}).FirstOrDefault();
            }
        }

        public Dojo GetNinjasById(int id) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                var query = @"
                SELECT * FROM dojos WHERE id = @Id;
                SELECT * FROM ninjas WHERE dojo_id = @Id;
                ";

                using (var multi = dbConnection.QueryMultiple(query, new {Id = id})) {
                    var dojo = multi.Read<Dojo>().Single();
                    dojo.ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }
            }
        }

        public IEnumerable<Ninja> GetRogues() {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas WHERE dojo_id = null");
            }
        }
    }
}