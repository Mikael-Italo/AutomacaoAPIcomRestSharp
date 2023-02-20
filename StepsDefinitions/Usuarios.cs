using AutomacaoAPIcomRestSharp.Evidencias;
using System.Collections.Specialized;

namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Usuarios : Hooks
    {
        private static RestClient? restClient;
        public Usuarios() => restClient = new RestClient("https://serverest.dev/#/");

        public static RestResponse? response { get; set; }
        public static string? idUser { get; set; }

        #region Dados
        //Dados para ServRestAPI
        public static string nomeL = "Teste Silva";
        public static string? emailL;
        public static string passwordL = "teste123";
        public static string admnistradorL = "false";
        #endregion Dados

        Evidencia evidencia = new Evidencia();

        #region Cadastro
        public void realizarNovoCadastroComSucesso(StreamWriter sw, string bdd, string nome)
        {
            Random random = new Random();
            string rdn = random.Next(1, 100).ToString();
            emailL = "teste.stronks" + rdn + "@qa.com";

            RestRequest request = new RestRequest("/usuarios", Method.Post);
            request.AddJsonBody(new
            {
                nome = nomeL,
                email = emailL,
                password = passwordL,
                administrador = admnistradorL
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Converte a 'String' do response para o C# identificar isso como JSON
            JsonNode JsonNode = JsonNode.Parse(response.Content!)!;
            idUser = (string)JsonNode!["_id"]!;

            //Validações
            Assert.AreEqual(201, code);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"), "Verificado que contém mensagem de sucesso");

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarCadastroComDadosJaExistentes(StreamWriter sw, string bdd, string nome)
        {
            RestRequest request = new RestRequest("/usuarios", Method.Post);
            request.AddJsonBody(new
            {
                nome = nomeL,
                email = emailL,
                password = passwordL,
                administrador = admnistradorL
            }
            );

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Este email já está sendo usado"), "Verifica se contém mensagem 'Este email já está sendo usado' ");

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }
        #region Cadastro Simples

        public string cadastroAdm()
        {
            Random random = new Random();
            string rdn = random.Next(1, 100).ToString();

            string email = "teste.qa" + rdn + "@qa.com";
            string idS, retorno;

            RestRequest request = new RestRequest("/usuarios", Method.Post);
            request.AddJsonBody(new
            {
                nome = "Testando usuario Adm",
                email,
                password = "teste",
                administrador = "true"
            });

            response = restClient!.Execute(request);
            JsonNode JsonNode = JsonNode.Parse(response.Content!)!;
            idS = (string)JsonNode!["_id"]!;

            retorno = "{\r\n  \"email\": \"" + email + "\"," +
                "\r\n  \"senha\": \"teste\"," +
                "\r\n  \"idUsuario\": \"" + idS + "\"" + "" +
                "\n}";

            return retorno;
        }

        public string cadastroSemAdm()
        {
            Random random = new Random();
            string rdn = random.Next(1, 100).ToString();

            string email = "teste.qa" + rdn + "@qa.com";
            string idS, retorno;

            RestRequest request = new RestRequest("/usuarios", Method.Post);
            request.AddJsonBody(new
            {
                nome = "Testando usuario comum",
                email,
                password = "teste",
                administrador = "false"
            });

            response = restClient!.Execute(request);
            JsonNode JsonNode = JsonNode.Parse(response.Content!)!;
            idS = (string)JsonNode!["_id"]!;

            retorno = "{\r\n  \"email\": \"" + email + "\"," +
                "\r\n  \"senha\": \"teste\"," +
                "\r\n  \"idUsuario\": \"" + idS + "\"" + "" +
                "\n}";

            return retorno;
        }
        #endregion Cadastro Simples

        #endregion Cadastro

        #region Consultas

        public void realizarConsultaListaDeUsuariosCadastrados(StreamWriter sw, string bdd, string nome)
        {
            RestRequest request = new RestRequest("/usuarios", Method.Get);
            request.AddParameter("_id", idUser);
            request.AddParameter("nome", nomeL);
            request.AddParameter("email", emailL);
            request.AddParameter("password", passwordL);
            //request.AddParameter("admnistrador", "false");

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains(idUser!), "Verifica se o contém o Id no response");
            Assert.IsTrue(response.Content!.Contains(emailL!), "Verifica se o contém o Email no response");

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarConsultaUsuarioId(StreamWriter sw, string bdd, string nome)
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Get);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains(id), "Verifica se o contém o Id no response");
            Assert.IsTrue(response.Content!.Contains(emailL!), "Verifica se o contém o email no response");

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarConsultaUsuarioIdInexistente(StreamWriter sw, string bdd, string nome)
        {
            string id = "SD3ysskAoSL";

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Get);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            //Validações
            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Usuário não encontrado"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }
        #endregion Consultas

        #region Editar

        public void realizarEdicaoUsuarioId(StreamWriter sw, string bdd, string nome)
        {
            string id = idUser!.ToString();
            Random random = new Random();
            string rdn = random.Next(100, 200).ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Put);
            request.AddJsonBody(new
            {
                nome = "Teste editado",
                email = "teste.edicao" + rdn + "@qa.com",
                password = "123teste",
                administrador = "true"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Registro alterado com sucesso"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarEdicaoUsuarioIdComMesmoEmail(StreamWriter sw, string bdd, string nome)
        {
            string JsonCadastro1 = cadastroAdm();
            JsonNode jsn1 = JsonNode.Parse(JsonCadastro1)!;
            string email = (string)jsn1["email"]!;

            string JsonCadastro = cadastroAdm();
            JsonNode jsn = JsonNode.Parse(JsonCadastro)!;
            string id = (string)jsn["idUsuario"]!;

            RestRequest request = new RestRequest($"/usuarios/{id}", Method.Put);
            request.AddJsonBody(new
            {
                nome = "Teste editado",
                email = email,
                password = "Teste@1",
                administrador = "true"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Este email já está sendo usado"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }

        public void realizarEdicaoComNovoEmailEntaoRealizaCadastro(StreamWriter sw, string bdd, string nome)
        {
            Random random = new Random();
            string rdn = random.Next(1, 500).ToString();

            RestRequest request = new RestRequest($"/usuarios/{"IdInexistente"}", Method.Put);
            request.AddJsonBody(new
            {
                nome = "Teste editado",
                email = "cadastroPorPUT"+rdn+"@qa.com.br",
                password = "Teste@1",
                administrador = "true"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Assert.AreEqual(201, code);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);

        }

        #endregion Editar

        #region Exclusão

        public void realizarExclusaoPorId(StreamWriter sw, string bdd, string nome)
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Delete);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Registro excluído com sucesso"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);

        }

        public void tentarRealizarExclusaoComIdInexistente(StreamWriter sw, string bdd, string nome)
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Delete);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Nenhum registro excluído"));

            evidencia.geraEvidenciaHtml(sw, response, bdd, nome);
            evidencia.geraEvidencia(sw, response, bdd, nome);
        }
        #endregion Exclusão
    }
}
