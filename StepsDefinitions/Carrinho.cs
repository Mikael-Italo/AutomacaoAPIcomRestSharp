using RestSharp;
using System.Runtime.Intrinsics.Arm;

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


        public void cadastrarCarrinho(StreamWriter sw, string bdd, string nome)
        {
            JsonNode jsn = JsonNode.Parse(produtos.cadastro2ProdutosCarrinho())!;
            string idProduto1 = (string)jsn!["idProduto1"]!;
            string idProduto2 = (string)jsn!["idProduto2"]!;

            string jsonPost = "{\r\n  \"produtos\": [\r\n    {\r\n      " +
                "\"idProduto\": \""+idProduto1+"\",\r\n      " +
                "\"quantidade\": 1\r\n    },\r\n    {\r\n      " +
                "\"idProduto\": \""+idProduto2+"\",\r\n      " +
                "\"quantidade\": 3\r\n    " +
                "}\r\n  ]\r\n}";

            at.autenticaHeader(restClient!);

            RestRequest request = new RestRequest("/carrinhos", Method.Post);
            request.AddJsonBody(jsonPost);
       

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(201, code);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
        }

        public void cadastraCarrinho1ProdutoSemRetorno(RestClient restClient, string idProduto)
        {
            string jsonPost = "{\r\n  \"produtos\": [\r\n    {\r\n     " +
                " \"idProduto\": \""+idProduto+"\",\r\n     " +
                " \"quantidade\": 1\r\n    }\r\n  ]\r\n}";

            at.autenticaHeader(restClient!);

            RestRequest request = new RestRequest("/carrinhos", Method.Post);
            request.AddJsonBody(jsonPost);


            response = restClient!.Execute(request);
        }


    }
}
