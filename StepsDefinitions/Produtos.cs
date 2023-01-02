namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Produtos
    {
        private static RestClient? restClient;
        public Produtos() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }
        Autenticacao at = new Autenticacao();
        public static string idProduto = string.Empty;

        public void cadastroDeProdutosComSucesso()
        {
            Random rand = new Random();
            string rdm = rand.Next(1,200).ToString();

            //Autentica adicionando o token direto no Header
                //at.autenticaHeader(restClient!);
            //Autentica usando o JW
            at.autenticaJW(restClient!);

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = "PC of the Xuxa"+rdm,
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });

            response = restClient!.Execute(request);
            JsonNode jsn = JsonNode.Parse(response.Content!)!;
            idProduto = (string)jsn!["_id"]!;
            
            Console.WriteLine(response.Content);    
            
            //Validações
            Assert.AreEqual(201, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"));
        }
    }
}
