namespace AutomacaoAPIcomRestSharp.Features
{
    [TestClass]
    public class Features
    {
        Usuarios usuarios = new Usuarios();
        Login login = new Login();
        Produtos produtos = new Produtos();
        Carrinho carrinho = new Carrinho(); 
        StreamWriter? sw;

        #region Usuários

        #region CADASTRO
        [TestMethod]
        public void CT01ValidarCadastroUsuarioComSucesso() => usuarios.realizarNovoCadastroComSucesso();

        [TestMethod]
        public void CT02ValidarCadastroUsuarioJaExistente() => usuarios.realizarCadastroComDadosJaExistentes();
        #endregion CADASTRO

        #region CONSULTA
        [TestMethod]
        public void CT03ValidarListarUsuariosCadastrados() => usuarios.realizarConsultaListaDeUsuariosCadastrados();

        [TestMethod]
        public void CT04ValidarConsultaPorId() => usuarios.realizarConsultaUsuarioId();

        [TestMethod]
        public void CT05ValidarConsultaIdInexistente() => usuarios.realizarConsultaUsuarioIdInexistente();

        #endregion CONSULTA

        #region Edição

        [TestMethod]
        public void CT06ValidarEdicaoPorId() => usuarios.realizarEdicaoUsuarioId();

        [TestMethod]
        public void CT07ValidarEdicaoPorIdComMesmoEmail() => usuarios.realizarEdicaoUsuarioIdComMesmoEmail();

        #endregion Edição

        #region Exclusão
        [TestMethod]
        public void CT08ValidarExclusaoPorId() => usuarios.realizarExclusaoPorId();

        [TestMethod]
        public void CT09ValidarTentativaExclusaoComIdInexistente() => usuarios.tentarRealizarExclusaoComIdInexistente();

        #endregion Exclusão

        #endregion Usuários

        #region Login

        [TestMethod]
        public void CT10ValidarloginUsuario() => login.realizarLoginComSucesso();

        [TestMethod]
        public void CT11ValidarloginUsuarioInvalido() => login.realizarLoginSemSucesso();

        #endregion Login

        #region Produtos

        [TestMethod]
        public void CT12ValidarCadastroProdutos()
        {
            string nomeTxt = "CadastroProdutos";
            string bdd =
                "Dado que o usuario/ADM esteja logado e autenticado\n" +
                "E que realize a chamada ao endpoint POST /produtos\n" +
                "E que preencha com dados validos o request JSON\n" +
                "Quando executar o endpoint\n" +
                "Entao o endpoint retorna o code 201 - OK\n" +
                "E exibe a mensagem: Cadastro realizado com sucesso";

            produtos.cadastroDeProdutosComSucesso(sw!, bdd, nomeTxt);
        }
        [TestMethod]
        public void CT13ValidarConsultaProdutosPorId()
        {
            string nome = "ConsultaProdutosId";
            string bdd =
                "Dado que haja um usuario/ADM logado e que haja um produto cadastrado\n" +
                "E que realize a chamada ao endpoint GET /produtos\n" +
                "E que preencha com dados validos o campo @_id\n" +
                "Quando executar o endpoint\n" +
                "Entao o endpoint retorna o code 200 - OK\n" +
                "E os dados do produto em JSON ";

            produtos.buscarProdutoPorId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT14ValidarExclusaoProdutosPorId()
        {
            string nome = "ExcluirProdutosId";
            string bdd =
                "Dado que haja um usuario/ADM logado e que haja um produto cadastrado\n" +
                "E que realize a chamada ao endpoint DELETE /produtos\n" +
                "E que preencha com dados validos o campo @_id\n" +
                "Quando executar o endpoint\n" +
                "Entao o endpoint retorna o code 200 - OK\n" +
                "E a mensagem: 'Registro excluído com sucesso'";

            produtos.excluirProdutoPorId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT15ValidarEdicaoProdutosPorId()
        {
            string br = "<br>";
            string nome = "EditaProdutosId";
            string bdd =
                $"Dado que haja um usuario/ADM logado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint PUT /produtos {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"E que preencha com dados validos o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Registro alterado com sucesso' {br}";

            produtos.editarProdutoPorId(sw!, bdd, nome);
        }
        #endregion Produtos

        #region Carrinho
        [TestMethod]
        public void CT16ValidarCadastroCarrinho()
        {
            string br = "<br>";
            string nome = "CadastraCarrinho";
            string bdd =
                $"Dado que haja um usuario/ADM logado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint POST /carrinhos {br}" +
                $"E que preencha com dados validos o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Registro alterado com sucesso' {br}";

            carrinho.cadastrarCarrinho(sw!, bdd, nome);
        }

        #endregion Carrinho
    }
}
