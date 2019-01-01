using System;
using System.IO;
using System.Linq;

class Program
{
    private static void Main(string[] args)
    {
        Start();
    }

    static void Start()
    {
        Console.Clear();
        Console.WriteLine("\nWYBIERZ APLIKACJE:\n");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n[1] - FizzBuzz.");
        Console.WriteLine("\n[2] - DeepDive.");
        Console.WriteLine("\n[3] - DrownITDown.");
        Console.WriteLine("\n[4] - Exit.\n\n");
        Console.ResetColor();

        short choseApp = short.Parse(Console.ReadLine());

        switch(choseApp)
        {
            case 1:
                FizzBuzz();
                break;
            case 2:
                DeepDive();
                break;
            case 3:
                DrownItDown();
                break;
            default :
                Exit();
                break;
        }
         
    }

    static void FizzBuzz()
    {
        Console.Clear();
        Console.WriteLine("FIZZBUZZ: \nPodaj liczbe i sprawdz czy jest FizzBuzz!!\nA moze jest tylko Fizz, a moze tylko Buzz.\n");

        string FizzBuzzString = "";

        try
        {
            short number = Convert.ToInt16(Console.ReadLine());

            if (number < 0 || number > 1000)
            {

                Console.WriteLine("\nLiczba poza zakresem. Zakres jest od 0 do 1000\nSprobuj jeszcz raz.");
                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
                Console.ReadKey();

                FizzBuzz();
            }

            if (number % 2 == 0)
                FizzBuzzString = "Fizz";

            if (number % 3 == 0)
                FizzBuzzString += "Buzz";

            Console.Write("A wiec: ");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FizzBuzzString);
            Console.ResetColor();
            


            if (FizzBuzzString.Equals(""))
                Console.WriteLine("\n:[");

            Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
            Console.ReadKey();

            Start();

        }
        catch (FormatException)
        {
            Console.WriteLine("Nie podano liczby. Sprobuj jeszce raz.");
            Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");

            Console.ReadKey();
            FizzBuzz();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeepDive()
    {
        Console.Clear();
        Console.WriteLine("DEEP DIVE: \nIle ma powstac zagniezdzonych folderow?\n");

        try
        {
            short i = Convert.ToInt16(Console.ReadLine());
            if (i < 0 || i > 5)
            {
                Console.WriteLine("\nLiczba poza zakresem.\nSprobuj jeszcz raz.");
                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");

                Console.ReadKey();

                DeepDive();
            }
            string path = Directory.GetCurrentDirectory();

            try
            {
                string[] dirs = new string[1];
                
                dirs = Directory.GetDirectories(path);

                Guid testerGuid;
                if (!Guid.TryParse(dirs[0].Split(Path.DirectorySeparatorChar).Last(), out testerGuid))
                    throw new IndexOutOfRangeException();

                path = dirs[0];
                
            }
            catch (IndexOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                RecDeepDive(ref i, ref path);
                Console.ResetColor();

                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
                
                Console.ReadKey();
                Start();
            }


            
            Directory.Delete(path, recursive: true);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Skasowanno folder: ");
            Console.WriteLine(path);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            path = Directory.GetCurrentDirectory();

            RecDeepDive(ref i, ref path);
            Console.ResetColor();
            Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");

            Console.ReadKey();

            Start();


        }

        catch (Exception ex)
        {

            Console.WriteLine("Blad: ");

            Console.Write(ex.Message);
            Console.WriteLine("\nSprobuj jeszcze raz.");
            Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
            Console.ReadKey();
        }

       

        void RecDeepDive(ref short i, ref string path) //Importal//////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (i > 0)
            {
                
                DirectoryInfo directory = new DirectoryInfo(path);
                Guid guid = Guid.NewGuid();
                


                directory.CreateSubdirectory(guid.ToString());
                path = Path.Combine(path, guid.ToString());

                i--;

                Console.WriteLine("\nUtworzono folder: ");
                Console.WriteLine(path);

                RecDeepDive(ref i, ref path);
            }

        }
    }

    static void DrownItDown()
    {
        Console.Clear();
        Console.WriteLine("DROWN IT DOWN: \nNa którym zagniezdzonym folderze ma powstac plik?\n");

        try
        {

            short i = Convert.ToInt16(Console.ReadLine());
            if (i < 0 || i > 5)
            {
                Console.WriteLine("Liczba poza zakresem.\nZakres od 0 do 5");
                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
                Console.ReadKey();
                DrownItDown();
            }


            string path = Directory.GetCurrentDirectory();
            RecDrownITDown(ref i, ref path);


            string filepath = Path.Combine(path, "Plik.txt");

            if (File.Exists(filepath))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Plik istnieje.");
                Console.ResetColor();
                Console.WriteLine("\nCzy chcesz zastapic plik\n[Z]astap lub kliknij dowolny inny przycisk, aby wybrac inny.");



                if(Console.ReadKey().Equals('z'))
                {
                    File.Create(filepath);
                    
                    Console.WriteLine("Utworzono plik:\n" + filepath);
                    
                    Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
                    Console.ReadKey();

                    
                }

                Start();
                
            }
            else
            {

                File.Create(filepath).Dispose();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Utworzono plik:\n" + filepath);
                Console.ResetColor();
                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");
                Console.ReadKey();

                Start();
            }


        }
        catch (FormatException)
        {
            Console.WriteLine("Nie podano liczby. Sprobuj jeszce raz.");
            Console.ReadKey();
            DrownItDown();
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Nie ma tyle plikow.\nCzy chcesz uzyc DeepDive, aby je stworzyc?\n[T]ak\n[N]ie");

            char c = Console.ReadKey().KeyChar;



            if (c.Equals('t') || c.Equals('T'))
            {
                DeepDive();

            }
            else if (c.Equals('n') || c.Equals('N'))
            {
                Console.WriteLine("\nNacisnij dowolny przycisk, aby kontynuowac.");

                Console.ReadKey();
                Start();
            }
            else
            {
                Console.WriteLine("\nNiewlasciwy znak");
                Console.WriteLine("Nacisnij dowolny przycisk, aby kontynuowac.");

                Console.ReadKey();
                Start();
            }


        }
        
        catch (Exception ex)
        {

            Console.WriteLine("Blad: ");

            Console.Write(ex.Message);
            Console.WriteLine("\nSprobuj jeszcze raz.");
            Console.WriteLine("Nacisnij dowolny przycisk, aby kontynuowac.");

            Console.ReadKey();
            DrownItDown();
        }



        void RecDrownITDown(ref short i, ref string path) //Importal//////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (i > 0)
            {
                string[] dirs = new string[1];
                dirs = Directory.GetDirectories(path);

                Guid testerGuid;
                if (!Guid.TryParse(dirs[0].Split(Path.DirectorySeparatorChar).Last(), out testerGuid))
                    throw new IndexOutOfRangeException();

                path = dirs[0];

                i--;
                RecDrownITDown(ref i, ref path);

            }


        }
    }

    static void Exit()
    {
        Console.WriteLine("\nZakonczenie programu.");
        Console.ReadKey();
        System.Environment.Exit(1);
    }

}

