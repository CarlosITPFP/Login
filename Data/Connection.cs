using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Login___GitHub.Data
{
    internal static class Connection
    {
        // Variable estática para la conexión
        private static MySqlConnection connMaster;

        // Configuración de conexión
        private static readonly string server = "localhost";
        private static readonly string database = "LoginSystem";
        private static readonly string user = "root";
        private static readonly string password = "";

        // Propiedad para verificar el estado de la conexión
        public static ConnectionState State => connMaster?.State ?? ConnectionState.Closed;

        // Método para abrir la conexión
        public static void OpenConnection()
        {
            try
            {
                // Verificar si la conexión ya está abierta
                if (connMaster == null || connMaster.State == ConnectionState.Closed)
                {
                    string connectionString = $"server={server}; database={database}; user={user}; password={password};";

                    connMaster = new MySqlConnection(connectionString);
                    connMaster.Open();

                    MessageBox.Show("Conexión Establecida", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La conexión ya está abierta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para cerrar la conexión
        public static void CloseConnection()
        {
            try
            {
                if (connMaster != null && connMaster.State == ConnectionState.Open)
                {
                    connMaster.Close();
                    MessageBox.Show("Conexión Cerrada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La conexión ya está cerrada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar la conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para ejecutar una consulta SQL que no retorna datos (ej. INSERT, UPDATE, DELETE)
        internal static void ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connMaster))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    cmd.ExecuteNonQuery(); // Ejecuta la consulta sin retorno de datos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para ejecutar una consulta SQL que retorna un solo valor (ej. COUNT, MAX)
        internal static object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connMaster))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    return cmd.ExecuteScalar(); // Retorna un solo valor de la consulta
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Método para ejecutar una consulta SQL que retorna un DataTable
        internal static DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connMaster))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt); // Llena el DataTable con los resultados de la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt; // Retorna el DataTable con los resultados
        }
    }
}
