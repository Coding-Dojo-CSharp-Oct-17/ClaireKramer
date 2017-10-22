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

        public void AddNinja(Ninja item, int dojo_id) {
            using (IDbConnection dbConnection = Connection) {
                string query = $"INSERT INTO ninjas (name, level, description, dojo_id) VALUES (@Name, @Level, @Description, {dojo_id})";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Ninja> FindAll() {
            using (IDbConnection dbConnection = Connection) {
                var query = "SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id = dojos.id";
                dbConnection.Open();
                var ninjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return ninjas;

            }
        }

        public Ninja FindByID(int id) {
            using (IDbConnection dbConnection = Connection) {
                var query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id = dojos.id WHERE ninjas.id = {id}";
                dbConnection.Open();
                var oneninja = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; }).FirstOrDefault();
                return oneninja;
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

        public void Banish(int id) {
            using (IDbConnection dbConnection = Connection) {
                string query = $"UPDATE ninjas SET dojo_id = 1 WHERE ninjas.id = {id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public void Recruit(int dojo_id, int ninja_id) {
            using (IDbConnection dbConnection = Connection) {
                string query = $"UPDATE ninjas SET dojo_id = {dojo_id} WHERE ninjas.id = {ninja_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
    }
}