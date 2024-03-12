using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace discordTokenGrabber
{
    internal class Program
        /* Program Author : c-nnor : github.com/c-nnor
         *         * Program Name: Discord Token Grabber
         *                 * Program Description: This program is a simple discord token grabber that will grab the user's discord token and print it to the console.
         *                         * Program Version: 1.0
         *                                 * Program Date: 10/10/2021
         */
    {
        static async Task Main(string[] args)
        {
            Console.WindowWidth = 130; // Set the desired width
            Console.WindowHeight = 40; // Set the desired height
            _ = new AssemblyTitleAttribute("Discord Token Grabber");
            String promt = @"{
▓█████▄ ██▓ ██████ ▄████▄  ▒█████  ██▀███ ▓█████▄    ▄▄▄█████▓▒█████  ██ ▄█▓█████ ███▄    █      ▄████ ██▀███  ▄▄▄      ▄▄▄▄   
▒██▀ ██▓██▒██    ▒▒██▀ ▀█ ▒██▒  ██▓██ ▒ ██▒██▀ ██▌   ▓  ██▒ ▓▒██▒  ██▒██▄█▒▓█   ▀ ██ ▀█   █     ██▒ ▀█▓██ ▒ ██▒████▄   ▓█████▄ 
░██   █▒██░ ▓██▄  ▒▓█    ▄▒██░  ██▓██ ░▄█ ░██   █▌   ▒ ▓██░ ▒▒██░  ██▓███▄░▒███  ▓██  ▀█ ██▒   ▒██░▄▄▄▓██ ░▄█ ▒██  ▀█▄ ▒██▒ ▄██
░▓█▄   ░██░ ▒   ██▒▓▓▄ ▄██▒██   ██▒██▀▀█▄ ░▓█▄   ▌   ░ ▓██▓ ░▒██   ██▓██ █▄▒▓█  ▄▓██▒  ▐▌██▒   ░▓█  ██▒██▀▀█▄ ░██▄▄▄▄██▒██░█▀  
░▒████▓░██▒██████▒▒ ▓███▀ ░ ████▓▒░██▓ ▒██░▒████▓      ▒██▒ ░░ ████▓▒▒██▒ █░▒████▒██░   ▓██░   ░▒▓███▀░██▓ ▒██▒▓█   ▓██░▓█  ▀█▓
 ▒▒▓  ▒░▓ ▒ ▒▓▒ ▒ ░ ░▒ ▒  ░ ▒░▒░▒░░ ▒▓ ░▒▓░▒▒▓  ▒      ▒ ░░  ░ ▒░▒░▒░▒ ▒▒ ▓░░ ▒░ ░ ▒░   ▒ ▒     ░▒   ▒░ ▒▓ ░▒▓░▒▒   ▓▒█░▒▓███▀▒
 ░ ▒  ▒ ▒ ░ ░▒  ░ ░ ░  ▒    ░ ▒ ▒░  ░▒ ░ ▒░░ ▒  ▒        ░     ░ ▒ ▒░░ ░▒ ▒░░ ░  ░ ░░   ░ ▒░     ░   ░  ░▒ ░ ▒░ ▒   ▒▒ ▒░▒   ░ 
 ░ ░  ░ ▒ ░  ░  ░ ░       ░ ░ ░ ▒   ░░   ░ ░ ░  ░      ░     ░ ░ ░ ▒ ░ ░░ ░   ░     ░   ░ ░    ░ ░   ░  ░░   ░  ░   ▒   ░    ░ 
   ░    ░       ░ ░ ░         ░ ░    ░       ░                   ░ ░ ░  ░     ░  ░        ░          ░   ░          ░  ░░      
 ░                ░                        ░                                                                                 ░ 
                                                                                                                               

";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(promt);
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Exit");
            Console.Write("Please select an option: ");
            String input = Console.ReadLine();
            Console.ResetColor();
switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(promt);
                    Console.Write("\nPlease enter your discord email: ");
                    string email = Console.ReadLine();
                    Console.Write("Please enter your discord password: ");
                    string password = Console.ReadLine();
                    await getToken(email, password);
                    Console.ResetColor();
                    break;
                case "2":
                    Console.Clear();    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(promt);
                    Console.WriteLine("\nQuitting...");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        async static Task getToken(String email, String password)
        {
            String url = "https://discord.com/api/v9/auth/login";
            var payload = new Dictionary<string, string>
            {
                {"email", email},{"password", password}
            };
            String jsonPayload = JsonConvert.SerializeObject(payload);
            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nLogin Successful!");
                String responseString = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseString);
                Console.WriteLine($"\nUser ID: {json["user_id"]}");
                Console.WriteLine($"Token:  {json["token"]}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
    }
}
