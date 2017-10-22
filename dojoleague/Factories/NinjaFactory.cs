using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using dojoleague.Models;

namespace dojoleague.Factory {
    public class NinjaFactory : IFactory<Ninja> {
        private string connectionString;
        public NinjaFactory () {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=dojoleague;SslMode=None";
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void AddNinja(Ninja item) {
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO ninjas (name, level, description) VALUES (@Name, @Level, @Description)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Ninja> FindAll() {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id");
            }
        }

        public Ninja FindByID(int id) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id WHERE ninja.id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Ninja> NinjasForDojoById(int id) {
            using (IDbConnection dbConnection = Connection) {
                var query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id AND dojo.id = {id}";
                dbConnection.Open();

                var myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return myNinjas;
            }
        }

        public IEnumerable<Dojo> AllDojos() {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM dojos");
            }
        }
    }
}