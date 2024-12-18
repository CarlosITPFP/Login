using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login___GitHub.Data; // Asegúrate de tener la referencia a tu clase de conexión a la base de datos

namespace Login
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        // Este método puede ser llamado en el evento "Load" o cuando el formulario se inicializa.
        private void Form2_Load(object sender, EventArgs e)
        {
            // Aquí puedes inicializar cualquier cosa que necesites al cargar el formulario
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;  // Asumimos que textBox1 es para el nombre de usuario
            string password = textBox2.Text;  // Asumimos que textBox2 es para la contraseña

            // Validamos si ambos campos no están vacíos
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese tanto el nombre de usuario como la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hacemos el hash de la contraseña antes de guardarla
            string hashedPassword = HashPassword(password);

            // Consulta SQL para insertar los datos en la tabla de usuarios
            string query = "INSERT INTO users (username, password_hash) VALUES (@username, @password)";
            var parameters = new Dictionary<string, object>
    {
        { "@username", username },
        { "@password", hashedPassword }  // Usamos la contraseña en formato de hash
    };

            try
            {
                // Ejecutamos la consulta
                Connection.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cerramos el formulario actual (el formulario de registro) y mostramos Form1
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();  // Opcional: Oculta el formulario actual (registro)
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

