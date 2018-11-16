using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace CognitiveOcr
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = BuildClient();

            Console.WriteLine("Press any key to begin...");
            Console.ReadKey();

            using(Stream imgStream = File.OpenRead("img\\words_img.jpg")){
                Console.WriteLine("imgStream size " + imgStream.Length);
                GetImageText(client, imgStream).Wait();                
            }

            Console.WriteLine("Press any key to finish execution...");
            Console.ReadKey();
        }

        static async Task GetImageText(ComputerVisionClient client, Stream imgStream){
            
            OcrResult ocrResult = await client.RecognizePrintedTextInStreamAsync(false, imgStream, OcrLanguages.En);
            foreach (var item in ocrResult.Regions)
            {
                foreach (var line in item.Lines)
                {
                    foreach(var word in line.Words)
                        Console.WriteLine(word.Text);
                }
            }
        }
        
        public static ComputerVisionClient BuildClient() {

            var client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(Constants.API_KEY),
                new System.Net.Http.DelegatingHandler[] { }
            );

            client.Endpoint = Constants.COGNITIVE_URL;

            return client;
        }
    }
}
