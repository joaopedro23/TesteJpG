using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace APPJPG.Pages.Client
{
    public class EditModel : PageModel
    {
        public ClientInfo editInfo = new ClientInfo();
        public String erroMessagen = "";
        public String sucessuMenssage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=JPGtest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                editInfo.id = "" + reader.GetInt32(0);
                                editInfo.name = reader.GetString(1);
                                editInfo.email = reader.GetString(2);
                                editInfo.dataNacimento = reader.GetString(3);
                                editInfo.renda = reader.GetString(4);
                                editInfo.cpf = reader.GetString(5);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                erroMessagen = ex.Message;
            }
        }
        public void OnPost() 
        {
            editInfo.id = Request.Form["id"]; 
            editInfo.name = Request.Form["name"];
            editInfo.email = Request.Form["email"];
            editInfo.dataNacimento = Request.Form["dataNacimento"];
            editInfo.renda = Request.Form["renda"];
            editInfo.cpf = Request.Form["cpf"];

            if (editInfo.id.Length == 0 || editInfo.name.Length == 0 || editInfo.email.Length == 0 ||
              editInfo.dataNacimento.Length == 0 || editInfo.renda.Length == 0 ||
              editInfo.cpf.Length == 0)
            {
                erroMessagen = "falta alguns dados ";
                return;
            }
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=JPGtest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
              "SET name=@name, email=@email, dataNacimento=@dataNacimento, renda=@renda, cpf=@cpf " +
              "WHERE id=@id";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", editInfo.name);
                        command.Parameters.AddWithValue("@email", editInfo.email);
                        command.Parameters.AddWithValue("@dataNacimento", editInfo.dataNacimento);
                        command.Parameters.AddWithValue("@renda", editInfo.renda);
                        command.Parameters.AddWithValue("@cpf", editInfo.cpf);
                        command.Parameters.AddWithValue("@id",editInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                erroMessagen = ex.Message;
                return;
            }
            Response.Redirect("/Client/Index");
        }
    }
}
