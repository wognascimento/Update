using BibliotecasSIG;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Update
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UpdateInfoJson { get; set; }
        public string AplicacaoPrincipal { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var updateInfo = JsonSerializer.Deserialize<UpdateInfo>(UpdateInfoJson);
            var updateChecker = new UpdateChecker();

            try
            {
                // Caminho para salvar o download (pode ser personalizado)
                string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Update", $"application-{updateInfo.updateVersion}.zip");
                // Cria diretório se não existir
                Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));
                // Cria um progress handler que atualiza o ProgressBar
                var progressHandler = new Progress<double>(value =>
                {
                    progressDownload.Value = value;
                });

                // Chama o método de download passando o progressHandler
                bool sucesso = await updateChecker.DownloadUpdateAsync(updateInfo.updateUrl, downloadPath, progressHandler);

                if (sucesso)
                {
                    var progressHandlerExtraction = new Progress<double>(valor =>
                    {
                        progressExtraction.Value = valor;
                    });
                    
                    bool sucessoExtraction = await updateChecker.ExtrairZipComProgressoAsync(downloadPath, progressHandlerExtraction);

                    if (sucessoExtraction)
                    {
                        Process.Start(AplicacaoPrincipal);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao extrair o arquivo.");
                    }
                }
                else
                {
                    MessageBox.Show("Falha ao realizar o download.");
                }

            }
            catch(JsonException ex)
            {
                // Log do erro ou tratamento de exceção
                MessageBox.Show(
                    $"Erro ao converter JSON para MODEL: {ex.Message}",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento de exceção
                MessageBox.Show(
                    $"Erro ao verificar atualizações: {ex.Message}",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }

        }
    }
}