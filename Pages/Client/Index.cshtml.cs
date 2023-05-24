using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace APPJPG.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String conectionString = "Data Source=.\\sqlexpress;Initial Catalog=JPGtest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id =  "" + reader.GetInt32(0);
                                clientInfo.name =  reader.GetString(1);
                                clientInfo.email =  reader.GetString(2);
                                clientInfo.dataNacimento =  reader.GetString(3);
                                clientInfo.renda =  reader.GetString(4);
                                clientInfo.cpf =  reader.GetString(5);
                                clientInfo.create_at = reader.GetDateTime(6).ToString();

                                listClients.Add(clientInfo);


                            }
                        }
                    }
                }   
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception" + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String? id;
        public String? name;
        public String? email;
        public String? dataNacimento;
        public String? renda;
        public String? cpf;
        public String? create_at;   

    }
}
