namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Carrinho
    {
        private static RestClient? restClient;
        public Carrinho() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }
        Autenticacao at = new Autenticacao();
        Evidencia evidencia = new Evidencia();
        Produtos produtos = new Produtos();


        public void cadastrarCarrinho(StreamWriter sw, string bdd, string nomeTxt)
        {
            JsonNode jsn = JsonNode.Parse(produtos.cadastro2ProdutosCarrinho())!;
            string idProduto1 = (string)jsn!["idProduto1"]!;
            string idProduto2 = (string)jsn!["idProduto2"]!;

            Console.WriteLine(produtos.cadastro2ProdutosCarrinho());
            Console.WriteLine("id1: "+idProduto1);
            Console.WriteLine("id2: "+idProduto2);

            //response = restClient!.Execute(request);
            //var code = (int)response.StatusCode;

            //Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            //Assert.AreEqual(401, code);
            //Assert.IsTrue(response.Content!.Contains("Email e/ou senha inválidos"));

        }

    }
}
