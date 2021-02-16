using System;
using System.Data.Common;

namespace ConsoleApplication1 {
    public class SqlDatabase {
        private readonly string _connStr;
        private readonly DbProviderFactory _factory;

        public SqlDatabase(string connStr, string providerName) {
            _connStr = connStr;
            _factory = DbProviderFactories.GetFactory(providerName);
        }

        public DbConnection GetConnection() {
            var conn = _factory.CreateConnection();
            conn.ConnectionString = _connStr;
            conn.Open();
            return conn;
        }

        public void ExecuteInTransaction(Action<DbConnection, DbTransaction> func) {
            using (var conn = GetConnection()) {
                var trans = conn.BeginTransaction();

                try {
                    func.Invoke(conn, trans);
                    trans.Commit();
                }
                catch (Exception ex) {
                    trans.Rollback();
                }
            }
        }
    }
}