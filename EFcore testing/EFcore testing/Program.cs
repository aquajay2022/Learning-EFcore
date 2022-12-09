using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace EFcore_testing
{
    public class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        static void Main(string[] args)
        {
            Console.WriteLine("Please be aware that this is a testing version, thank you");
            Console.Write("read, write, edit or login: ");
            string choice = Console.ReadLine();
            if (choice == "write")
            {
                Console.WriteLine();
                Console.Write("Username: ");
                string Username = Console.ReadLine();
                Console.Write("Password: ");
                string Password = Console.ReadLine();
                using (MyDB database = new MyDB())
                {
                    try
                    {
                        database.Database.EnsureCreated();  
                        database.Users.Add(new User() { Username = Username, Password = Password });
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox(IntPtr.Zero, ex.Message, "Unhandled Exception", 0);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unhandled exception, the program has crashed");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                }
            }
            if (choice == "read")
            {
                using (MyDB database = new MyDB())
                {
                    Console.WriteLine();
                    foreach(User user in database.Users)
                    {
                        Console.WriteLine($"ID: {user.Id}");
                        Console.WriteLine($"Username: {user.Username}");
                        Console.WriteLine($"Password: {user.Password}");
                        Console.WriteLine();
                    }
                }
            }
            if (choice == "edit")
            {
                using (MyDB db = new MyDB())
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Enter your user: ");
                        string editchoice = Console.ReadLine();
                        User user = db.Users.Single(x => x.Username.Contains(editchoice));
                        if (editchoice != user?.Username)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No such user exists");
                            Console.ForegroundColor = ConsoleColor.White;
                            Environment.Exit(0);
                        }
                        Console.Write($"Hello {user.Username} do you want to change your password or username? ");
                        string usernameorpass = Console.ReadLine();
                        if (usernameorpass == "username")
                        {
                            Console.Write("Enter a new username: ");
                            user.Username = Console.ReadLine();
                            db.SaveChanges();
                        }
                        if (usernameorpass == "password")
                        {
                            Console.Write("Enter a new password: ");
                            user.Password = Console.ReadLine();
                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox(IntPtr.Zero, ex.Message, "Unhandled Exception", 0);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unhandled exception, the program has crashed");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                }
            }
            if (choice == "login")
            {
                using (MyDB db = new MyDB())
                {
                    try
                    {
                        Console.Write("Enter your username: ");
                        string loginchoice = Console.ReadLine();
                        User user = db.Users.SingleOrDefault(x => x.Username.Contains(loginchoice));
                        User user1 = db.Users.Single(x => x.Username.Contains(loginchoice));
                        if (user?.Username == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("User does not exist");
                            Console.ForegroundColor = ConsoleColor.White;
                            Environment.Exit(0);
                        }
                        if (loginchoice == user1.Username)
                        {
                            Console.Write($"Hello {loginchoice} please enter your password: ");
                            string loginpassword = Console.ReadLine();
                            User password = db.Users.SingleOrDefault(x => x.Password.Contains(loginpassword));
                            if (loginpassword == user.Password)
                            {
                                Console.Clear();
                                Console.WriteLine("You have succesfully signed in");
                                Console.WriteLine($"Welcome back {loginchoice}");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Incorrect password");
                                Console.ForegroundColor = ConsoleColor.White;
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Incorrect username");
                            Console.ForegroundColor = ConsoleColor.White;
                            Environment.Exit(0);
                        }
                }
                    catch (Exception ex)
                    {
                        MessageBox(IntPtr.Zero, ex.Message, "Unhandled Exception", 0);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unhandled exception, the program has crashed");
                        Console.ForegroundColor = ConsoleColor.White;
                        Environment.Exit(0);
                    }
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All tasks suceeded");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}