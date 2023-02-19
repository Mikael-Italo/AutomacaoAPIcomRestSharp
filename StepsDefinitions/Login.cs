namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Login
    {
        Usuarios usuarios = new Usuarios();
        Evidencia evidencia = new Evidencia();

        private static RestClient? restClient;
        public Login() => restClient = new RestClient("https://serverest.dev/#/");

        public static RestResponse? response { get; set; }

        public void realizarLoginComSucesso(StreamWriter sw, string bdd, string nome)
        {
            JsonNode JsonNode = JsonNode.Parse(usuarios.cadastroAdm())!;
            string email = (string)JsonNode!["email"]!;
            string senha = (string)JsonNode!["senha"]!;

            RestRequest request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new
            {
                email,
                password = senha
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Login realizado com sucesso"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarLoginSemSucesso(StreamWriter sw, string bdd, string nome)
        {
            RestRequest request = new RestRequest("/login", Method.Post);
            request.AddJsonBody(new
            {
                email = "saosa@assa.com",
                password = "asdasdijas"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(401, code);
            Assert.IsTrue(response.Content!.Contains("Email e/ou senha inválidos"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

    }
}
