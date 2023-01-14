namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Autenticacao
    {
        Usuarios usuarios = new Usuarios();

        private static RestClient? restClient;
        public Autenticacao() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }

        public string getToken()
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
            JsonNode jsn = JsonNode.Parse(response.Content!)!;    

            return (string)jsn!["authorization"]!; 
        }

        public void autenticaJW(RestClient client)
        {
            string tokenCompleto = getToken();
            string tokenSemBearer = string.Empty;
            string bearer = "Bearer ";
            int i = tokenCompleto.IndexOf(bearer);
            if (i >= 0)
            {
                tokenSemBearer = tokenCompleto.Remove(i, bearer.Length);
            }
            
            client.Authenticator = new JwtAuthenticator(tokenSemBearer);
        }

        public void autenticaHeader(RestClient client)
        {
            client.AddDefaultHeader("Authorization", getToken());
        }
    }
}
