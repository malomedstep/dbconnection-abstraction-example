using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ConsoleApplication1 {
    public static class DbConnectionExtensions {
        public static int ExecuteNonQuery(this DbConnection conn, string sql, object param = null,
            DbTransaction trans = null) {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = trans;

            if (param != null) {
                foreach (var propInfo in param.GetType().GetProperties()) {
                    var dbParam = cmd.CreateParameter();
                    dbParam.Value = propInfo.GetValue(param);
                    dbParam.ParameterName = propInfo.Name;
                    cmd.Parameters.Add(dbParam);
                }
            }

            return cmd.ExecuteNonQuery();
        }

        public static IEnumerable<T> ExecuteReader<T>(this DbConnection conn, string sql, object param = null) where T : class, new() {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            if (param != null) {
                foreach (var propInfo in param.GetType().GetProperties()) {
                    var dbParam = cmd.CreateParameter();
                    dbParam.Value = propInfo.GetValue(param);
                    dbParam.ParameterName = propInfo.Name;
                    cmd.Parameters.Add(dbParam);
                }
            }

            var items = Activator.CreateInstance<List<T>>();
            
            var reader = cmd.ExecuteReader();
            var props = typeof(T).GetProperties();
            while (reader.Read()) {
                var item = Activator.CreateInstance<T>();
                foreach (var prop in props) {
                    prop.SetValue(item, reader[prop.Name]);
                }
                items.Add(item);
            }
            return items;
        }

        public static T ExecuteScalar<T>(this DbConnection conn, string sql, object param = null) {
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            if (param != null) {
                foreach (var propInfo in param.GetType().GetProperties()) {
                    var dbParam = cmd.CreateParameter();
                    dbParam.Value = propInfo.GetValue(param);
                    dbParam.ParameterName = propInfo.Name;
                    cmd.Parameters.Add(dbParam);
                }
            }

            return (T)cmd.ExecuteScalar();
        }
    }
}