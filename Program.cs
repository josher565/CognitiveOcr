using System;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace CognitiveOcr
{
    class Program
    {
        static void Main(string[] args)
        {
            string imgUrl = "https://photos.app.goo.gl/aMacQbh9SeYnn6UF8";

            Console.WriteLine("Please enter your api key");
            var key = Console.ReadLine();

            var client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(key),
                new System.Net.Http.DelegatingHandler[]{}
            );

            client.Endpoint = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0";

            Console.WriteLine("Analyzing...");
            GetImageText(client, imgUrl).Wait();
            Console.ReadKey();
        }

        static async Task GetImageText(ComputerVisionClient client, string imageUrl){

            // Finally some AI calls
            var ocrResult = await client.RecognizeTextAsync(imageUrl, TextRecognitionMode.Printed);
            WriteResults(ocrResult);
        }

        private static void WriteResults(RecognizeTextHeaders result)
        {
            if (result != null){
                foreach(var line in result.OperationLocation)
            }
        }
    }
}
