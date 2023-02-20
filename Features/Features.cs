namespace AutomacaoAPIcomRestSharp.Features
{
    [TestClass]
    public class Features
    {
        Usuarios usuarios = new();
        Login login = new();
        Produtos produtos = new();
        Carrinho carrinho = new();
        StreamWriter? sw;

        #region Usuários

        #region CADASTRO
        [TestMethod]
        public void CT01ValidarCadastroUsuarioComSucesso()
        {
            string br = "<br>";
            string nome = "CadastraUsuario";
            string bdd =
                $"Dado que haja a necessidade de cadastro de usuario {br}" +
                $"E que realize a chamada ao endpoint POST /usuarios {br}" +
                $"E que preencha com dados validos o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Cadastro realizado com sucesso' junto com seu Id {br}";

            usuarios.realizarNovoCadastroComSucesso(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT02ValidarCadastroUsuarioJaExistente()
        {
            string br = "<br>";
            string nome = "CadastraUsuarioJaExistente";
            string bdd =
                $"Dado que haja a necessidade de cadastro de usuario {br}" +
                $"E que realize a chamada ao endpoint POST /usuarios {br}" +
                $"E que preencha com dados ja cadastrados o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400  {br}" +
                $"E a mensagem: 'Este email já está sendo usado {br}";

            usuarios.realizarCadastroComDadosJaExistentes(sw!, bdd, nome);
        }
        #endregion CADASTRO

        #region CONSULTA
        [TestMethod]
        public void CT03ValidarListarUsuariosCadastrados()
        {
            string br = "<br>";
            string nome = "ListarUsuarioCadastrado";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /usuarios {br}" +
                $"E que preencha com dados validos os campos requisitados {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Contendo a lista de dados do usuário {br}";

            usuarios.realizarConsultaListaDeUsuariosCadastrados(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT04ValidarConsultaPorId()
        {
            string br = "<br>";
            string nome = "ConsultaUsuarioPorId";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /usuarios/_id {br}" +
                $"E que preencha com dados validos o campo Id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: contendo os dados do usuario {br}";

            usuarios.realizarConsultaUsuarioId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT05ValidarConsultaIdInexistente()
        {
            string br = "<br>";
            string nome = "ConsultaUsuarioInexistente";
            string bdd =
                $"Dado que não haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /usuarios/_id {br}" +
                $"E que preencha com um dado não existente o campo Id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400  {br}" +
                $"E a mensagem: 'Usuário não encontrado' {br}";

            usuarios.realizarConsultaUsuarioIdInexistente(sw!, bdd, nome);
        }

        #endregion CONSULTA

        #region Edição

        [TestMethod]
        public void CT06ValidarEdicaoPorId()
        {
            string br = "<br>";
            string nome = "EditarUsuarioPorId";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint PUT /usuarios/_id {br}" +
                $"E que preencha o campo _id com dados do usuario {br}" +
                $"E que preencha o JsonBody com os dados a ser alterados" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Registro alterado com sucesso' {br}";

            usuarios.realizarEdicaoUsuarioId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT07ValidarEdicaoPorIdComMesmoEmail()
        {
            string br = "<br>";
            string nome = "EditarUsuarioPorIdComMesmoEmailAtual";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint PUT /usuarios/_id {br}" +
                $"E que preencha o campo _id com dados do usuario {br}" +
                $"E que preencha o JsonBody com os dados a ser alterados" +
                $"E preencha o email com um que ja esta em uso" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400  {br}" +
                $"E a mensagem: 'Este email já está sendo usado' {br}";

            usuarios.realizarEdicaoUsuarioIdComMesmoEmail(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT07_1ValidarEdicaoPorEmailNovo()
        {
            string br = "<br>";
            string nome = "EditarUsuarioPorEmailNovo";
            string bdd =
                $"Dado que não haja um email nem _id cadastrado {br}" +
                $"E que realize a chamada ao endpoint PUT /usuarios/_id {br}" +
                $"E que preencha o campo _id com dados não existente {br}" +
                $"E que preencha o JsonBody com um email não cadastrado {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200  {br}" +
                $"E a mensagem: 'Cadastro realizado com sucesso' junto de seu _id {br}";

            usuarios.realizarEdicaoComNovoEmailEntaoRealizaCadastro(sw!, bdd, nome);
        }

        #endregion Edição

        #region Exclusão
        [TestMethod]
        public void CT08ValidarExclusaoPorId()
        {
            string br = "<br>";
            string nome = "ExcluirUsuarioPorId";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint DELETE /usuarios/_id {br}" +
                $"E que preencha o campo _id com dados do usuario {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Registro excluido com sucesso' {br}";

            usuarios.realizarExclusaoPorId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT09ValidarTentativaExclusaoComIdInexistente()
        {
            string br = "<br>";
            string nome = "ExcluirUsuarioPorIdInexistente";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint DELETE /usuarios/_id {br}" +
                $"E que preencha o campo _id com um dado inexistente {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200  {br}" +
                $"E a mensagem: 'Nenhum registro excluído' {br}";

            usuarios.tentarRealizarExclusaoComIdInexistente(sw!, bdd, nome);
        }

        #endregion Exclusão

        #endregion Usuários

        #region Login

        [TestMethod]
        public void CT10ValidarloginUsuario()
        {
            string br = "<br>";
            string nome = "RealizarLoginSucesso";
            string bdd =
                $"Dado que haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint POST /login {br}" +
                $"E que preencha o JsonBody com dados do usuario {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Login realizado com sucesso' {br}";

            login.realizarLoginComSucesso(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT11ValidarloginUsuarioInvalido()
        {
            string br = "<br>";
            string nome = "RealizarLoginInvalido";
            string bdd =
                $"Dado que não haja um usuario cadastrado {br}" +
                $"E que realize a chamada ao endpoint POST /login {br}" +
                $"E que preencha o JsonBody com dados inexistentes  {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400  {br}" +
                $"E a mensagem: 'Email e/ou senha inválidos' {br}";

            login.realizarLoginSemSucesso(sw!, bdd, nome);
        }

        #endregion Login

        #region Produtos

        #region Cadastro

        [TestMethod]
        public void CT12ValidarCadastroProdutos()
        {
            string br = "<br>";
            string nome = "CadastroProdutos";
            string bdd =
                $"Dado que o usuario/ADM esteja autenticado e autenticado {br}" +
                $"E que realize a chamada ao endpoint POST /produtos {br}" +
                $"E que preencha com dados validos o request JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 201 - OK {br}" +
                $"E exibe a mensagem: Cadastro realizado com sucesso {br}";

            produtos.cadastroDeProdutosComSucesso(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT12_1ValidarCadastroProdutosComMesmoNome()
        {
            string br = "<br>";
            string nome = "CadastroProdutosComMesmoNome";
            string bdd =
                $"Dado que o usuario/ADM esteja autenticado {br}" +
                $"E que realize a chamada ao endpoint POST /produtos {br}" +
                $"E que preencha o JsonBody com o nome de um produto ja cadastrado {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400 {br}" +
                $"E exibe a mensagem: 'Já existe produto com esse nome' {br}";

            produtos.cadastroDeProdutosComMesmoNome(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT12_2ValidarCadastroProdutosSemAutenticar()
        {
            string br = "<br>";
            string nome = "CadastroProdutosSemAutenticar";
            string bdd =
                $"Dado que o usuario/ADM não esteja autenticado {br}" +
                $"E que realize a chamada ao endpoint POST /produtos {br}" +
                $"E que preencha o JsonBody com dados validos {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 401 {br}" +
                $"E exibe a mensagem: 'Token de acesso ausente, inválido, expirado ou usuário do token não existe mais' {br}";

            produtos.cadastroDeProdutosSemAutenticar(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT12_3ValidarCadastroProdutosSemContaAdministrador()
        {
            string br = "<br>";
            string nome = "CadastroProdutosSemContaAdministrador";
            string bdd =
                $"Dado que o usuario comum esteja autenticado {br}" +
                $"E que realize a chamada ao endpoint POST /produtos {br}" +
                $"E que preencha o JsonBody com dados validos {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 403 {br}" +
                $"E exibe a mensagem: 'Rota exclusiva para administradores' {br}";

            produtos.cadastroDeProdutosSemContaAdministrador(sw!, bdd, nome);
        }
        #endregion Cadastro

        #region Consulta

        [TestMethod]
        public void CT13ValidarConsultaProdutosPorId()
        {
            string br = "<br>";
            string nome = "ConsultaProdutosId";
            string bdd =
                $"Dado que haja um usuario/ADM autenticado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /produtos/_id {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK {br}" +
                $"E os dados do produto em JSON {br}";

            produtos.buscarProdutoPorId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT13_1ValidarConsultaDadosProduto()
        {
            string br = "<br>";
            string nome = "ConsultaDadosProdutos";
            string bdd =
                $"Dado que haja um usuario/ADM autenticado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /produtos {br}" +
                $"E que preencha com dados validos os campos @_id, @nome, @preco, @descricao, @quantidade {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK {br}" +
                $"E os dados do produto em JSON {br}";

            produtos.buscarDadosProdutos(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT13_2ValidarConsultaProdutosIdInexistente()
        {
            string br = "<br>";
            string nome = "ConsultaProdutosIdInexistente";
            string bdd =
                $"Dado que não há produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint GET /produtos/_id {br}" +
                $"E que preencha com dado invalido o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400 {br}" +
                $"E retorna a mensagem: 'Produto não encontrado' {br}";

            produtos.buscarProdutoPorIdInexistente(sw!, bdd, nome);
        }

        #endregion Consulta

        #region Exclusão

        [TestMethod]
        public void CT14ValidarExclusaoProdutosPorId()
        {
            string br = "<br>";
            string nome = "ExcluirProdutosId";
            string bdd =
                $"Dado que haja um usuario/ADM autenticado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint DELETE /produtos/_id {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK {br}" +
                $"E a mensagem: 'Registro excluído com sucesso' {br}";

            produtos.excluirProdutoPorId(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT14_1ValidarExclusaoProdutosIdInexistente()
        {
            string br = "<br>";
            string nome = "ExcluirProdutosIdInexistente";
            string bdd =
                $"Dado que há um usuario/ADM autenticado e não há um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint DELETE /produtos/_id {br}" +
                $"E que preencha com dado invalido o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 {br}" +
                $"E a mensagem: 'Nenhum registro excluído' {br}";

            produtos.excluirProdutoIdInexistente(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT14_2ValidarExclusaoProdutosSemAutenticar()
        {
            string br = "<br>";
            string nome = "ExcluirProdutosSemAutenticar";
            string bdd =
                $"Dado que não há um usuario/ADM autenticado {br}" +
                $"E que realize a chamada ao endpoint DELETE /produtos/_id {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 401 {br}" +
                $"E a mensagem: 'Token de acesso ausente, inválido, expirado ou usuário do token não existe mais' {br}";

            produtos.excluirProdutoSemAutenticar(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT14_3ValidarExclusaoProdutosUsuarioNaoAdm()
        {
            string br = "<br>";
            string nome = "ExcluirProdutosSemContaADM";
            string bdd =
                $"Dado que há um usuario comum autenticado {br}" +
                $"E que realize a chamada ao endpoint DELETE /produtos/_id {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 403 {br}" +
                $"E a mensagem: 'Rota exclusiva para administradores' {br}";

            produtos.excluirProdutoSemContaADM(sw!, bdd, nome);
        }

        [TestMethod]
        public void CT14_4ValidarExclusaosProdutosAdicionadoCarrinho()
        {
            string br = "<br>";
            string nome = "ExcluirProdutosAdicionadosNoCarrinho";
            string bdd =
                $"Dado que há um usuario/ADM autenticado e há um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint DELETE /produtos/_id {br}" +
                $"E que preencha o campo @_id com dados de um produto que faz parte de um carrinho{br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 400 {br}" +
                $"E a mensagem: 'Não é permitido excluir produto que faz parte de carrinho' {br}";

            produtos.excluirProdutoAdicionadoCarrinho(sw!, bdd, nome);
        }
        #endregion Exclusão

        #region Edição

        [TestMethod]
        public void CT15ValidarEdicaoProdutosPorId()
        {
            string br = "<br>";
            string nome = "EditaProdutosId";
            string bdd =
                $"Dado que haja um usuario/ADM autenticado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint PUT /produtos {br}" +
                $"E que preencha com dados validos o campo @_id {br}" +
                $"E que preencha com dados validos o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Registro alterado com sucesso' {br}";

            produtos.editarProdutoPorId(sw!, bdd, nome);
        }


        #endregion Edição

        #endregion Produtos

        #region Carrinho

        #region Cadastro
        [TestMethod]
        public void CT16ValidarCadastroCarrinho()
        {
            string br = "<br>";
            string nome = "CadastraCarrinho";
            string bdd =
                $"Dado que haja um usuario/ADM autenticado e que haja um produto cadastrado {br}" +
                $"E que realize a chamada ao endpoint POST /carrinhos {br}" +
                $"E que preencha com dados validos o body/JSON {br}" +
                $"Quando executar o endpoint {br}" +
                $"Entao o endpoint retorna o code 200 - OK  {br}" +
                $"E a mensagem: 'Cadastro realizado com sucesso' {br}";

            carrinho.cadastrarCarrinho(sw!, bdd, nome);
        }
        #endregion Cadastro

        #endregion Carrinho
    }
}
