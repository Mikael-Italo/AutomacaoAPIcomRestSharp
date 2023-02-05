﻿namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Produtos
    {
        private static RestClient? restClient;
        public Produtos() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }
        Autenticacao at = new Autenticacao();
        Evidencia evidencia = new Evidencia();
        public static string idProduto = string.Empty;

        public void cadastroDeProdutosComSucesso(StreamWriter sw, string bdd, string nomeTxt)
        {
            Random rand = new Random();
            string rdm = rand.Next(1,200).ToString();

            //Autentica usando o JW
            at.autenticaJW(restClient!);

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = $"PC of the Xuxa {rdm}",
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });

            response = restClient!.Execute(request);
            Console.WriteLine(response.Content);    
            
            //Validações
            Assert.AreEqual(201, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"));
            //Evidencia
            evidencia.geraEvidencia(sw, response, bdd, nomeTxt);
        }

        public void cadastroParaTeste()
        {
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();

            //Autentica usando o JW
            at.autenticaJW(restClient!);

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = $"PC of the Xuxa {rdm}",
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });

            response = restClient!.Execute(request);
            JsonNode jsn = JsonNode.Parse(response.Content!)!;
            idProduto = (string)jsn!["_id"]!;
        }

        public void buscarProdutoPorId(StreamWriter sw, string bdd, string nomeTxt)
        {
            cadastroParaTeste();

            RestRequest request = new RestRequest($"/produtos/{idProduto}", Method.Get);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains((string)idProduto));

            //Evidencia
            evidencia.geraEvidencia(sw, response, bdd, nomeTxt);
            Console.WriteLine(response.Content);            
        }

        public void excluirProdutoPorId(StreamWriter sw, string bdd, string nomeTxt)
        {
            cadastroParaTeste();

            RestRequest request = new RestRequest($"/produtos/{idProduto}", Method.Delete);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Registro excluído com sucesso"));

            //Evidencia
            evidencia.geraEvidencia(sw, response, bdd, nomeTxt);
            Console.WriteLine(response.Content);
        }
        public void editarProdutoPorId(StreamWriter sw, string bdd, string nomeTxt)
        {
            Random rand = new Random();
            string rdn = rand.Next(100,200).ToString();
            cadastroParaTeste();

            RestRequest request = new RestRequest($"/produtos/{idProduto}", Method.Put);
            request.AddJsonBody(new
            {
                nome = $"PC do batman{rdn}",
                preco =  470,
                descricao = "Roda briga na baixa",
                quantidade = 22
            });

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Registro alterado com sucesso"));

            //Evidencia
            //evidencia.geraEvidencia(sw, response, bdd, nomeTxt);
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
            Console.WriteLine(response.Content);
        }
    }
}
