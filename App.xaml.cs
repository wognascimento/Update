using System.Text.Json;
using System.Windows;

namespace Update
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Em ambiente de desenvolvimento, simule os parâmetros caso não haja
            string[] args = e.Args;

            //{"currentVersion":"1.1.0.0","updateVersion":"1.1.0.0","updateUrl":"http://192.168.0.49/downloads/central-sig/application-1.1.0.0.zip","changelog":["Corre\u00C3\u00A7\u00C3\u00A3o de bugs","Melhorias de desempenho"],"releaseDate":"2025-03-26","minimumCompatibleVersion":"1.0.0"}
#if DEBUG
            if (args == null || args.Length == 0)
            {
                //args = ["{\"currentVersion\":\"1.1.0.0\",\"updateVersion\":\"1.1.0.0\",\"updateUrl\":\"http://192.168.0.49/downloads/central-sig/application-1.1.0.0.zip\",\"changelog\":[\"Corre\\u00C3\\u00A7\\u00C3\\u00A3o de bugs\",\"Melhorias de desempenho\"],\"releaseDate\":\"2025-03-26\",\"minimumCompatibleVersion\":\"1.0.0\"}"];

                args = new string[] { "{\"currentVersion\":\"1.1.0.0\",\"updateVersion\":\"1.1.0.0\",\"updateUrl\":\"http://192.168.0.49/downloads/central-sig/application-1.1.0.0.zip\",\"changelog\":[\"Corre\\u00C3\\u00A7\\u00C3\\u00A3o de bugs\",\"Melhorias de desempenho\"],\"releaseDate\":\"2025-03-26\",\"minimumCompatibleVersion\":\"1.0.0\"}", "CentralSIG.exe" };
            }
#endif
            

            if (args.Length > 0)
            {
                string jsonString = args[0];
                try
                {

                    //JObject jsonObj = JsonConvert.DeserializeObject<JObject>(jsonString);
                    // Acesse os valores do JSON, por exemplo:
                    //string nome = jsonObj["nome"]?.ToString();
                    //string modo = jsonObj["modo"]?.ToString();
                    //Console.WriteLine($"Nome: {nome}, Modo: {modo}");

                    //MessageBox.Show(@$"ARGUMENTO: {jsonString}");

                    // Cria a instância da MainWindow
                    MainWindow mainWindow = new()
                    {
                        // Define o valor que será passado
                        UpdateInfoJson = jsonString,
                        AplicacaoPrincipal = args[1]
                    };

                    // Mostra a janela
                    mainWindow.Show();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao processar o JSON: " + ex.Message);
                }
            }
            

            // Prossegue com a inicialização da aplicação
            base.OnStartup(e);
        }
    }


}
