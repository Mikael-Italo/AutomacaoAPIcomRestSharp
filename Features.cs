namespace AutomacaoAPIcomRestSharp
{
    [TestClass]
    public class Features
    {
        Usuarios usuarios = new Usuarios();

        #region ServRestAPI

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

        #region Login
        public void loginUsuario()
        {

        }
        #endregion Login

        #endregion ServRest

    }
}
