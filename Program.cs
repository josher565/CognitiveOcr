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
            const string key = "000000"; //put a valid key here.

            var client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(key),
                new System.Net.Http.DelegatingHandler[] { }
            );
            client.Endpoint = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0";


         

            Console.WriteLine("Analyzing...");
            
            Console.ReadKey();

            using(Stream imgStream = File.OpenRead("img\\words_img.jpg")){
                Console.WriteLine("imgStream size " + imgStream.Length);
                GetImageText(client, imgStream).Wait();
            }

            Console.ReadKey();
        }

        static async Task GetImageText(ComputerVisionClient client, Stream imageUrl){

            // Finally some AI calls
            RecognizeTextInStreamHeaders ocrResult = 
                await client.RecognizeTextInStreamAsync(imageUrl, TextRecognitionMode.Printed);
            Console.WriteLine(ocrResult.OperationLocation);
            await RetrieveAndWriteResults(client, ocrResult.OperationLocation);
        }

        static async Task RetrieveAndWriteResults(ComputerVisionClient client, string operationLocation)
        {
            Console.WriteLine(operationLocation);
        }
    }
}
