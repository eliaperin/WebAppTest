using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebAppTest.Pages.TestModel;
using System.Data.SqlClient;

namespace WebAppTest.Pages
{
    public class modificaModel : PageModel
    {
        String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=G:\\Il mio Drive\\Classroom\\22-2IOT-Sviluppo Applicazioni\\visual studio\\webapp\\WebAppTest\\WebAppTest\\db.mdf;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30";

        public anagrafica dato = new anagrafica();

        public string errorMessage = "";
        public void OnGet() // viene eseguita al caricamento della pagina
        {
            String id = "" + Request.Query["id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT *  FROM Anagrafica WHERE id like '" + id + "'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dato.id = reader.GetInt32(0);
                                dato.Nome = reader.GetString(1);
                                dato.Cognome = reader.GetString(2);
                                dato.Telefono = reader.GetString(3);
                                dato.DataNascita = reader.GetDateTime(4).ToString("dd/MM/yyyy");
                                
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



        public void OnPost() // viene eseguita alla pressione del pulsante salva
        {
            String id = "" + Request.Query["id"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE *  FROM Anagrafica WHERE id like '" + id + "'";



                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }


            Response.Redirect("test");
        }
    }
}
