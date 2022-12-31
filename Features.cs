namespace AutomacaoAPIcomRestSharp
{
    [TestClass]
    public class Features
    {
        StepDefinitions step = new StepDefinitions();

        #region ServRestAPI

        #region CADASTRO
        [TestMethod]
        public void CT01ValidarVadastroUsuarioComSucesso() => step.realizarNovoCadastroComSucesso();
        
        [TestMethod]
        public void CT02ValidarCadastroUsuarioJaExistente() => step.realizarCadastroComDadosJaExistentes();
        #endregion CADASTRO

        #region CONSULTA
        [TestMethod]
        public void CT03ValidarListarUsuariosCadastrados() => step.realizarConsultaListaDeUsuariosCadastrados();

        [TestMethod]
        public void CT04ValidarConsultaPorId() => step.realizarConsultaUsuarioId();

        #endregion CONSULTA

        #region Edição

        [TestMethod]
        public void CT05ValidarEdicaoPorId() => step.realizarEdicaoUsuarioId();

        [TestMethod]
        public void CT06ValidarEdicaoPorIdComMesmoEmail() => step.realizarEdicaoUsuarioIdComMesmoEmail();

        #endregion Edição

        #region Exclusão
        [TestMethod]
        public void CT07ValidarExclusaoPorId() => step.realizarExclusaoPorId();

        #endregion Exclusão

        #region Login
        public void loginUsuario()
        {

        }
        #endregion Login

        #endregion ServRest

    }
}
