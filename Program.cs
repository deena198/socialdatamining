﻿using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CVSPredictionSample
{
    public static class Program
    {
        public static void Main()
        {
            Console.Write("Enter image file path: ");
            string imageFilePath = Console.ReadLine();

            MakePredictionRequest(imageFilePath).Wait();

            Console.WriteLine("\n\nHit ENTER to exit...");
            Console.ReadLine();
        }

        public static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid Prediction-Key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "e2c6302e96b24c28b3a27edcfdb85457");

            // Prediction URL - replace this example URL with your valid Prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/c405a16c-c509-4ed1-852b-3033f49f2bb9/classify/iterations/Iteration1/image";

            HttpResponseMessage response;

            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        private static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
}