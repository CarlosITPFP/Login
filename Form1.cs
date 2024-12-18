using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Login___GitHub.Data;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Connection.OpenConnection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Aquí deberías agregar la lógica para verificar las credenciales en la base de datos.
            // Por ejemplo, se puede usar una consulta SQL para verificar el usuario y la contraseña.

            string query = "SELECT * FROM users WHERE username = @username AND password_hash = @password";
            var parameters = new Dictionary<string, object>
            {
                { "@username", username },
                { "@password", HashPassword(password) } // Asegúrate de utilizar un método de hashing para la contraseña.
            };

            // Verificamos si el usuario existe
            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                MessageBox.Show("Inició sesión correctamente");
                Form2 form2 = new Form2(); // Suponiendo que tienes otro formulario llamado Form2
                form2.Show();
                this.Hide(); // Opcional: Oculta el formulario actual.
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            } // Aquí ahora devuelve un DataTable

            if (result.Rows.Count > 0)  // Ahora puedes acceder a Rows sin error
            {
                // Si el usuario es válido, redirigimos a otro formulario (por ejemplo, un formulario principal).
                MessageBox.Show("Inició sesión correctamente");
                Form2 form2 = new Form2(); // Suponiendo que tienes otro formulario llamado Form2
                form2.Show();
                this.Hide(); // Opcional: Oculta el formulario actual.
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private string HashPassword(string password)
        {
            // Método de ejemplo para obtener un hash de la contraseña usando SHA256.
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(); // Asegúrate de tener un formulario para crear cuentas
            form.ShowDialog();
            this.Hide(); // Opcional: Oculta el formulario actual.
        }
    }
}
