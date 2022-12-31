using RestSharp;

namespace AutomacaoAPIcomRestSharp
{
    public class Login
    {
        Usuarios usuarios = new Usuarios();

        private static RestClient? restClient;
        public Login() => restClient = new RestClient("https://serverest.dev/#/");

        public static RestResponse? response { get; set; }

        public void realizarLoginComSucesso()
        {
            JsonNode JsonNode = JsonNode.Parse(usuarios.cadastroAdm())!;
            string email = (string)JsonNode!["email"]!;
            string senha = (string)JsonNode!["senha"]!;

            RestRequest request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new
            {
                email = email,
                password = senha
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Login realizado com sucesso"));
        }

        public void realizarLoginSemSucesso()
        {
            RestRequest request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new
            {
                email = "saosa@assa.com",
                password = "asdasdijas"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(401, code);
            Assert.IsTrue(response.Content!.Contains("Email e/ou senha inválidos"));
        }

    }
}
