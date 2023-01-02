using System.Text;

namespace AutomacaoAPIcomRestSharp.Evidencias
{
    public class Evidencia
    {
        public void geraEvidencia(StreamWriter sw, RestResponse response, string bdd, string nomeTxt)
        {
            sw = new StreamWriter($"C:\\Users\\minascimento\\source\\repos\\TreinamentoAutomacaoAPI\\Evidencias\\{nomeTxt}.txt", true, Encoding.ASCII);

            sw.WriteLine();
            sw.WriteLine("================================================");
            sw.WriteLine($"Data e hora do teste: {DateTime.Now.ToString()}");
            sw.WriteLine();
            sw.WriteLine("---------BDD-----------------");
            sw.WriteLine(bdd);
            sw.WriteLine("-----------------------------");
            sw.WriteLine($"response.StatusCode: {response.StatusCode}");
            sw.WriteLine($"response.StatusCode: {(int)response.StatusCode}");
            sw.WriteLine($"response.content: {response.Content}");
            sw.WriteLine("================================================");

            sw.Close();
        }
    }
}
