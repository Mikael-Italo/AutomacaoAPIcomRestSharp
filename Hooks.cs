namespace AutomacaoAPIcomRestSharp
{
    public class Hooks
    {
        public static RestClient? client { get; set; }

        public void initialize()
        {
            client = new RestClient("https://serverest.dev/#/");
        }

        [TestInitialize]
        public void getUp(){
            Console.WriteLine("Inicio de teste");
            initialize();
        }

        [TestCleanup]
        public void tearDown() {
            Console.WriteLine("Fim de teste");
        }
    }
}
