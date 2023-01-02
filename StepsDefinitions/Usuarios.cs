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

        #region Cadastro
        public void realizarNovoCadastroComSucesso()
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
            //var options = new JsonSerializerOptions { WriteIndented = true };
            //Salva o ID na variavel idUser
            idUser = (string)JsonNode!["_id"]!;

            Console.WriteLine("id: " + idUser);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(201, code);
            Assert.IsTrue(response.Content!.Contains("Cadastro realizado com sucesso"), "Verificado que contém mensagem de sucesso");
        }

        public void realizarCadastroComDadosJaExistentes()
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

            Console.WriteLine("id: " + idUser);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Este email já está sendo usado"), "Verifica se contém mensagem 'Este email já está sendo usado' ");
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
                nome = "Testando",
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
        #endregion Cadastro Simples

        #endregion Cadastro

        #region Consultas

        public void realizarConsultaListaDeUsuariosCadastrados()
        {
            RestRequest request = new RestRequest("/usuarios", Method.Get);
            request.AddParameter("_id", idUser);
            request.AddParameter("nome", nomeL);
            request.AddParameter("email", emailL);
            request.AddParameter("password", passwordL);
            //request.AddParameter("admnistrador", "false");

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + idUser);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains(idUser!), "Verifica se o contém o Id no response");
            Assert.IsTrue(response.Content!.Contains(emailL!), "Verifica se o contém o Email no response");
        }

        public void realizarConsultaUsuarioId()
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Get);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + idUser);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains(id), "Verifica se o contém o Id no response");
            Assert.IsTrue(response.Content!.Contains(emailL!), "Verifica se o contém o email no response");
        }

        public void realizarConsultaUsuarioIdInexistente()
        {
            string id = "SD3ysskAoSL";

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Get);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + idUser);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            //Validações
            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Usuário não encontrado"));
        }
        #endregion Consultas

        #region Editar

        public void realizarEdicaoUsuarioId()
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

            Console.WriteLine("id: " + id);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Registro alterado com sucesso"));
        }

        public void realizarEdicaoUsuarioIdComMesmoEmail()
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Put);
            request.AddJsonBody(new
            {
                nome = "Teste editado",
                email = "beltrano@qa.com.br",
                password = "123teste",
                administrador = "true"
            });

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + id);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            Assert.AreEqual(400, code);
            Assert.IsTrue(response.Content!.Contains("Este email já está sendo usado"));
        }

        #endregion Editar

        #region Exclusão

        public void realizarExclusaoPorId()
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Delete);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + id);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Registro excluído com sucesso"));

        }

        public void tentarRealizarExclusaoComIdInexistente()
        {
            string id = idUser!.ToString();

            RestRequest request = new RestRequest("/usuarios/" + id, Method.Delete);

            response = restClient!.Execute(request);
            var code = (int)response.StatusCode;

            Console.WriteLine("id: " + id);
            Console.WriteLine("Code: " + code + " Response " + response.Content);

            Assert.AreEqual(200, code);
            Assert.IsTrue(response.Content!.Contains("Nenhum registro excluído"));
        }
        #endregion Exclusão
    }
}
