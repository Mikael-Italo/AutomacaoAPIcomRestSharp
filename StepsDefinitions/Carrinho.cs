namespace AutomacaoAPIcomRestSharp.StepsDefinitions
{
    public class Carrinho
    {
        private static RestClient? restClient;
        public Carrinho() => restClient = new RestClient("https://serverest.dev/#/");
        public static RestResponse? response { get; set; }
        Autenticacao at = new Autenticacao();
        Evidencia evidencia = new Evidencia();



        public void cadastrarCarrinho(StreamWriter sw, string bdd, string nomeTxt)
        {

        }






    }
}
