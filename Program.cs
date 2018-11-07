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
            client.Endpoint = "https://eastus.api.cognitive.microsoft.com/";


         

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

            // Finally some AI calls
            RecognizeTextInStreamHeaders ocrResult = 
                await client.RecognizeTextInStreamAsync(imgStream, TextRecognitionMode.Printed);
            
            await RetrieveAndWriteResults(client, ocrResult.OperationLocation);
        }

        static async Task RetrieveAndWriteResults(ComputerVisionClient client, string operationLocation)
        {
            Console.WriteLine("Returned Location: " + operationLocation);

            //lets get the Operation ID that is on the end of the operationLocation.
            //we need this for our next call
            string operationId = operationLocation.Substring(
                operationLocation.Length - 36);
            
            Console.WriteLine("Operation ID: " + operationId);

            TextOperationResult result =
                await client.GetTextOperationResultAsync(operationId);
            
            while ((result.Status == TextOperationStatusCodes.Running ||
                    result.Status == TextOperationStatusCodes.NotStarted)){
                        Console.WriteLine("Client Status: " + result.Status);
                        await Task.Delay(5000);
                    }

            foreach(Line line in result.RecognitionResult.Lines)
                Console.WriteLine(line.Text);
        }
    }
}
