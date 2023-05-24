using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace APPJPG.Pages.Client
{
    public class CreatedModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String erroMessage = "";
        public String succesoMenssagen = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            //validacao usuario//

            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.dataNacimento = Request.Form["dataNacimento"];
            clientInfo.renda = Request.Form["renda"];
            clientInfo.cpf = Request.Form["cpf"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.dataNacimento.Length == 0 || clientInfo.renda.Length == 0 ||
                clientInfo.cpf.Length == 0) 
            {
                erroMessage = "falta alguns dados ";
                return;
            }

            // sava cliente dentro do banco de dados//

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=JPGtest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" + 
                        "(name, email, dataNacimento, renda, cpf) VALUES " + 
                        "(@name, @email, @dataNacimento, @renda, @cpf);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);   
                        command.Parameters.AddWithValue ("email", clientInfo.email);
                        command.Parameters.AddWithValue("dataNacimento", clientInfo.dataNacimento);
                        command.Parameters.AddWithValue("renda",clientInfo.renda);
                        command.Parameters.AddWithValue("cpf", clientInfo.cpf);
                        command.ExecuteNonQuery();
                    }
                }   
            }
            catch (Exception ex)
            {

                erroMessage = ex.Message;
                return;
            }

            clientInfo.name = "";clientInfo.email = "";
            clientInfo.dataNacimento = "";clientInfo.renda = "";
            clientInfo.cpf = "";

            Response.Redirect("/Client/Index");
        }
    }

}
