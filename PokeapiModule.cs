using Nancy;
using System;
using System.Linq;
using System.Collections.Generic;
using ApiCaller;
namespace HelloNancy
{
    public class PokeapiModule : NancyModule
    {
        public PokeapiModule()
        
        {
            Get("/", args => {
                System.Console.WriteLine("IN ROOT");
                string Name = "?";
                string Weight = "?";
                string Height = "?";
                ViewBag.name = Name;
                ViewBag.weight = Weight;
                ViewBag.height = Height;
                return View["index"]; 
            });
            Get("/{num}", async args =>{
                string Name = "";
                long Weight = 0;
                long Height = 0;
                string pokeID = args.num;
                // Our anonymous function is a parameter of type Action that returns a Dictionary
                await WebRequest.SendRequest("http://pokeapi.co/api/v2/pokemon/"+pokeID, new Action<Dictionary<string, object>>( JsonResponse =>
                    {
                        // System.Console.WriteLine((Array)JsonResponse["types"]);
                        Name = (string)JsonResponse["name"];
                        Weight = (long)JsonResponse["weight"];    
                        Height = (long)JsonResponse["height"];
                    }
                ));
                Console.WriteLine(Name);
                ViewBag.name = Name;
                ViewBag.weight = Weight;
                ViewBag.height = Height;
                return View["index"]; 
            });
        }
    }
}
