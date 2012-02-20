using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using GoatFish.net;

namespace GoatFish.net_cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Process[] theProcesses = Process.GetProcessesByName("GoatFish.net-cli.exe");
            theProcesses = Process.GetProcesses();

            var fi = new FileInfo("GoatFish.net-cli.exe");
            string fileName = fi.Name.Replace(fi.Extension, string.Empty);
            fi = null;
            int currentID = Process.GetCurrentProcess().Id;
            bool alreadyExists = theProcesses.Any(running => running.ProcessName == fileName && currentID != running.Id);
            theProcesses = null;
            if (alreadyExists)
            {
                Console.WriteLine("GoatFish.net CLI client already running...");
            }
            else
            {
                var model = new Models();
  
                foreach (var v in args)
                {
                    InterpretCommand(v);
                }
                string argument = ReadInput();
            }
        }

        private static string ReadInput()
        {
            Console.Write("goatfish.net " + Models.GetDbName() + "> ");
            Func<string> read = Console.ReadLine;
            string argument = read.Invoke();
            InterpretCommand(argument);
            return argument;
        }

        private static void InterpretCommand(string s)
        {
            try
            {
                if (s.ToLower().StartsWith("set"))
                {
                    var put = new char[3] {'s', 'e', 't'};
                    var sb = new StringBuilder();
                    s = s.TrimStart(put);
                    s = s.Trim();
                    var s1 = s.Split(' ');
                    for (var i = 1; i <= s1.Length -1 ; i++ )
                    {
                        sb.Append(s1[i] + " ");
                    }
                        Models.Save(s1[0], sb.ToString());
                    Console.WriteLine("OK");
                }


                else if (s.ToLower().StartsWith("get"))
                {
                    var put = new char[3] {'g', 'e', 't'};

                    s = s.TrimStart(put);
                    s = s.Trim();
                    Console.WriteLine(Models.Find(s).Value);
                }


                else if (s.ToLower().StartsWith("del"))
                {
                    var put = new char[3] {'d', 'e', 'l'};

                    s = s.TrimStart(put);
                    s = s.Trim();
                    Models.Delete(s);
                    Console.WriteLine("OK");
                }

                else if (s.ToLower().StartsWith("exit"))
                {
                    Environment.Exit(1);
                }
                else if (s.ToLower().StartsWith("clear"))
                {
                    Console.Write("Are you sure you want to delete all entities? (Y/N): ");
                    var choice = Console.ReadLine();
                    if (choice.ToLower().Contains("y"))
                    {
                        Models.Clear();
                    }
                }
                else if (s.ToLower().StartsWith("all"))
                {
                    foreach (var v in Models.Find())
                    {
                        Console.WriteLine(v);
                    }
                }
                else if (s.ToLower().StartsWith("help"))
                {
                    Console.WriteLine(
                        "GoatFish.net Help -- COMMANDS\n set entry -- 'set key value'\n get entry by key -- 'get key' \n delete entry --'del key' \n" +
                        " get all entries -- 'all'\n clear entries -- 'clear'\n exit -- 'exit'");
                }

                else
                {
                    Console.WriteLine("(error) ERR unknown command '{0}' -- type 'help' for commands.", s);
                }
                ReadInput();
            }
            catch (Exception)
            {
                Console.WriteLine("(error) ERR incorrect syntax -- type 'help' for commands.", s);
                ReadInput();
            }
        }
    }
}