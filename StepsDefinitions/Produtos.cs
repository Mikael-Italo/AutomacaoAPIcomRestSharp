using System.Xml.XPath;

namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Produtos
    {
        private static RestClient? restClient;
        public Produtos() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }
        Autenticacao at = new Autenticacao();
        Evidencia evidencia = new Evidencia();
        Carrinho carrinho = new Carrinho();
        public static string idProduto, nome, descricao = string.Empty;
        public static int preco, quantidade;

        public void cadastroDeProdutosComSucesso(StreamWriter sw, string bdd, string nome)
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
            //Validações
            Assert.AreEqual(201, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"));
            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void cadastroDeProdutosComMesmoNome(StreamWriter sw, string bdd, string nome)
        {
            #region Cadastro duplicado
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();

            //Autentica usando o JW
            at.autenticaJW(restClient!);
            string nomeProduto = $"PC of the Xuxa {rdm}";

            RestRequest requestDuplicacao = new RestRequest("/produtos", Method.Post);
            requestDuplicacao.AddJsonBody(new
            {
                nome = nomeProduto,
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });
            RestResponse responseDuplicacao = restClient!.Execute(requestDuplicacao);
            #endregion Cadastro duplicado

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = nomeProduto,
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });
            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(400, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Já existe produto com esse nome"));
            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void cadastroDeProdutosSemAutenticar(StreamWriter sw, string bdd, string nome)
        {
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = $"PC of the Xuxa {rdm}",
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(401, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Token de acesso ausente, inválido, expirado ou usuário do token não existe mais"));
            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void cadastroDeProdutosSemContaAdministrador(StreamWriter sw, string bdd, string nome)
        {
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();

            //Autentica usando o JW
            at.autenticaHeaderSemAdm(restClient!);

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {   
                nome = $"PC of the Xuxa {rdm}",
                preco = 100,
                descricao = "Roda briga de vizinho",
                quantidade = 12
            });

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(403, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Rota exclusiva para administradores"));
            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }
        
        public void cadastroParaTeste()
        {
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();
            string nomePost = $"PC of the Xuxa {rdm}";
            int precoPost = 100;
            string descricaoPost = "Roda briga de vizinho";
            int quantidadePost = 12;

            //Autentica usando o JW
            at.autenticaJW(restClient!);

            RestRequest request = new RestRequest("/produtos", Method.Post);
            request.AddJsonBody(new
            {
                nome = nomePost,
                preco = precoPost,
                descricao = descricaoPost,
                quantidade = quantidadePost
            });

            response = restClient!.Execute(request);
            JsonNode jsn = JsonNode.Parse(response.Content!)!;
            idProduto = (string)jsn!["_id"]!;
            nome = nomePost;
            preco = precoPost;
            descricao = descricaoPost;
            quantidade = quantidadePost;
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
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);           
        }

        public void buscarDadosProdutos(StreamWriter sw, string bdd, string nomeTxt)
        {
            cadastroParaTeste();

            RestRequest request = new RestRequest($"/produtos", Method.Get);
            request.AddParameter("_id", idProduto);
            request.AddParameter("nome", nome);
            request.AddParameter("preco", preco);
            request.AddParameter("descricao", descricao);
            request.AddParameter("quantidade", quantidade);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains((string)idProduto));
            Assert.IsTrue(response.Content!.Contains("\"produtos\":"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
        }

        public void buscarProdutoPorIdInexistente(StreamWriter sw, string bdd, string nomeTxt)
        {
            RestRequest request = new RestRequest($"/produtos/{"IdInexistente"}", Method.Get);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(400, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Produto não encontrado"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
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
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
        }

        public void excluirProdutoIdInexistente(StreamWriter sw, string bdd, string nomeTxt)
        {
            at.autenticaHeader(restClient!);

            RestRequest request = new RestRequest($"/produtos/{"IdInexistente"}", Method.Delete);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Nenhum registro excluído"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
        }

        public void excluirProdutoSemAutenticar(StreamWriter sw, string bdd, string nomeTxt)
        {
            RestRequest request = new RestRequest($"/produtos/{"IDAQUI"}", Method.Delete);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(401, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Token de acesso ausente, inválido, expirado ou usuário do token não existe mais"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
        }

        public void excluirProdutoSemContaADM(StreamWriter sw, string bdd, string nomeTxt)
        {
            at.autenticaHeaderSemAdm(restClient!);
            RestRequest request = new RestRequest($"/produtos/{"IDAQUI"}", Method.Delete);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(403, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Rota exclusiva para administradores"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
        }

        public void excluirProdutoAdicionadoCarrinho(StreamWriter sw, string bdd, string nomeTxt)
        {
            cadastroParaTeste();
            #region Add produto a um carrinho
            carrinho.cadastraCarrinho1ProdutoSemRetorno(restClient!, idProduto!);
            #endregion

            RestRequest request = new RestRequest($"/produtos/{idProduto}", Method.Delete);

            response = restClient!.Execute(request);

            //Validações
            Assert.AreEqual(400, (int)response.StatusCode);
            Assert.IsTrue(response.Content!.Contains("Não é permitido excluir produto que faz parte de carrinho"));

            //Evidencia
            evidencia.geraEvidenciaHtml(sw, response, bdd, nomeTxt);
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
        }

        #region Cadastro2ProdutosParaCarrinhoDinamico
        public string cadastro2ProdutosCarrinho()
        {
            Random rand = new Random();
            string rdm = rand.Next(1, 200).ToString();
            string? idProduto1 = null;
            string? idProduto2 = null;

            //Autentica usando o JW
            at.autenticaJW(restClient!);

            for (int i = 0; i < 2; i++)
            {
                RestRequest request = new RestRequest("/produtos", Method.Post);
                request.AddJsonBody(new
                {
                    nome = $"PC of the Xuxa {rdm+i}",
                    preco = 100,
                    descricao = "Roda briga de vizinho",
                    quantidade = 12
                });

                response = restClient!.Execute(request);
                JsonNode jsn = JsonNode.Parse(response.Content!)!;

                if (i == 0)
                {
                    idProduto1 = (string)jsn!["_id"]!;
                }
                else
                {
                    idProduto2 = (string)jsn!["_id"]!;
                }
            }
            string retorno = "{\r\n  \"idProduto1\": \"" + idProduto1 + "\"," +
                "\r\n  \"idProduto2\": \""+ idProduto2 +"\"" +
                "\n}";

            return retorno;
        }
        #endregion
    }
}
