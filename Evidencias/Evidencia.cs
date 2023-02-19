using System.Text;

namespace AutomacaoAPIcomRestSharp.Evidencias
{
    public class Evidencia
    {
        public void geraEvidencia(StreamWriter sw, RestResponse response, string bdd, string nomeTxt)
        {
            sw = new StreamWriter($"C:\\Users\\minascimento\\source\\repos\\TreinamentoAutomacaoAPI\\Evidencias\\txt\\{nomeTxt}.txt", true, Encoding.UTF8);

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

        public void geraEvidenciaHtml(StreamWriter sw, RestResponse response, string bdd, string nomeHtml)
        {
            sw = new StreamWriter($"C:\\Users\\minascimento\\source\\repos\\TreinamentoAutomacaoAPI\\Evidencias\\html\\{nomeHtml}.html", true, Encoding.UTF8);

            sw.WriteLine
                ("<!DOCTYPE html>" +
                "\n<html>" +
                "\n<head>\n " +
                "<meta charset='utf-8'>\n " +
                "<meta http-equiv='X-UA-Compatible' content='IE=edge'>\n" +
                "<title>Evidência</title>\n  " +
                "<meta name='viewport' content='width=device-width, initial-scale=1'>\n" +
                "</head>\n" +
                "<body>\n  " +
                "<h1 style=\"margin-left: 45%;\">Evidência de teste</h1>\n    <hr>\n   " +
                "<div>\n      " +
                $"<h2 style=\"color: red;\">Data e hora do teste: {DateTime.Now.ToString()}</h2>\n<hr>\n" +
                $"<h4>{nomeHtml}</h4>\n            <hr>\n       " +
                "<p style=\"color: green;\">\n         " +
                $" {bdd} \n       " +
                "</p>\n        <hr>\n   " +
                "</div>\n    <div>\n       " +
                $"<h2 style=\"color: red;\">response.StatusCode: {response.StatusCode} <br>\n" +
                $"response.StatusCode: {(int)response.StatusCode} <br>\n" +
                $"response.content: {response.Content} <br>\n" +
                "</h2>\n        <hr>\n   " +
                "</div>\n" +
                "</body>\n" +
                "</html>");
            sw.Close ();    
        }
    }
}
