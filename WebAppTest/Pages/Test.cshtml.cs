using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WebAppTest.Pages
{
    public class TestModel : PageModel
    {
        public string errorMessage = "";
        public class anagrafica
        {
            public int id;
            public string Nome;
            public string Cognome;
            public string DataNascita;
            public string Telefono;

        }

        public List<anagrafica> lista = new List<anagrafica>();
        public void OnGet()
        {
            String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=G:\\Il mio Drive\\Classroom\\22-2IOT-Sviluppo Applicazioni\\visual studio\\webapp\\WebAppTest\\WebAppTest\\db.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30";

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT *  FROM Anagrafica";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                anagrafica dato = new anagrafica();
                                dato.id = reader.GetInt32(0);
                                dato.Nome= reader.GetString(1);
                                dato.Cognome= reader.GetString(2);
                                dato.Telefono= reader.GetString(3);
                                dato.DataNascita= reader.GetDateTime(4).ToString("dd/MM/yyyy");
                                lista.Add(dato);
                            }
                        }
                    }


                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
