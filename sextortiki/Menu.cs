using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace sextortiki
{
    internal class Menu
    {
        private static void DopMenu(Order menu, int position)
        {
            foreach (var item in menu.orders)
            {
                Console.Write("   ");
                Console.WriteLine(item);
            }

            ConsoleKeyInfo strelka = Console.ReadKey(true);
            ChangePosition(strelka, page);
        }

        public static int page = 0;
        public static int cost = 0;
        public static int pos = 0;
        public static string Order = "";

        public static Order form = new Order()
        {
            cost = new[] { 100, 150, 200 },
            orders = new[] { "Круглый - 100", "Квадратный - 150", "Треугольный - 200" }
        };

        public static Order size = new Order()
        {
            cost = new[] { 50, 100, 200 },
            orders = new[] { "Маленький - 50", "Средний - 100", "Большой - 200" }
        };

        public static Order corzh = new Order()
        {
            cost = new[] { 30, 60, 90 },
            orders = new[] { "2 коржа - 30", "3 коржа - 60", "4 коржа - 90" }
        };

        public static Order glaze = new Order()
        {
            cost = new[] { 100, 150, 200 },
            orders = new[] { "Классическая - 100", "Черная - 150", "Белая - 200" }
        };

        public static void ChangePosition(ConsoleKeyInfo strelka, int page, string empty = "\0\0", string arrow = "->")
        {
            while (strelka.Key != ConsoleKey.Enter)
            {
                strelka = Console.ReadKey(true);
                if (page == 0)
                {
                    switch (strelka.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (pos < 4)
                            {
                                Console.SetCursorPosition(0, pos);
                                Console.Write(empty);
                                Console.SetCursorPosition(0, ++pos);
                                Console.Write(arrow);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (pos > 0)
                            {
                                Console.SetCursorPosition(0, pos);
                                Console.Write(empty);
                                Console.SetCursorPosition(0, --pos);
                                Console.Write(arrow);
                            }
                            break;
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                }
                else if (page > 0)
                {
                    switch (strelka.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (pos < 4)
                            {
                                Console.SetCursorPosition(0, pos);
                                Console.Write(empty);
                                Console.SetCursorPosition(0, ++pos);
                                Console.Write(arrow);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (pos > 0)
                            {
                                Console.SetCursorPosition(0, pos);
                                Console.Write(empty);
                                Console.SetCursorPosition(0, --pos);
                                Console.Write(arrow);
                            }
                            break;
                    }
                }
            }

            page++;
            podpod(strelka);
        }

        public static void GlavMenu()
        {
            string[] tipsi = new[] { "Форма", "Размер", "Кол-во коржей", "Глазурь", "Конец заказа" };

            foreach (string item in tipsi)
            {
                Console.SetCursorPosition(3, pos);
                Console.WriteLine(item);
                pos++;
            }

            Console.WriteLine("-------------------");
            Console.SetCursorPosition(3, 6);
            Console.WriteLine($"Сумма заказа: {cost}");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine($"Ваш заказ: {Order}");
            ConsoleKeyInfo key = Console.ReadKey(true);
            ChangePosition(key, page);
        }

        public static void podpod(ConsoleKeyInfo knopka)
        {
            while (knopka.Key != ConsoleKey.Escape)
            {
                if (page == 0)
                {
                    switch (pos)
                    {
                        case 0:
                            page = 1;
                            Console.Clear();
                            DopMenu(form, pos);
                            break;
                        case 1:
                            page = 2;
                            Console.Clear();
                            DopMenu(size, pos);
                            break;
                        case 2:
                            page = 3;
                            Console.Clear();
                            DopMenu(corzh, pos);
                            break;
                        case 3:
                            page = 4;
                            Console.Clear();
                            DopMenu(glaze, pos);
                            break;
                        case 4:
                            Console.Clear();
                            string order = $"\n Заказ от {DateTime.Now}" +
                                           $"\n\t Заказ: {Order}" +
                                           $"\n\t Цена: {cost}";
                            File.AppendAllText("C:\\Users\\79663\\Desktop\\Заказ.txt", order);
                            Console.WriteLine("Ваш заказ принят." +
                                              "\nЧтобы сделать новый заказ, нажмите ESC");

                            knopka = Console.ReadKey(true);
                            switch (knopka.Key)
                            {
                                case ConsoleKey.Escape:
                                    {
                                        cost = 0;
                                        Order = "";
                                        pos = 0;
                                        page = 0;
                                        Console.Clear();
                                        GlavMenu();
                                    }
                                    break;
                            }
                            break;
                            
                    }
                }
                if (page > 0)
                {
                    Order sec_menu = default;
                    
                    if (page == 1)
                    {
                        sec_menu = form;
                    }
                    else if (page == 2)
                    {
                        sec_menu = size;
                    }
                    else if (page == 3)
                    {
                        sec_menu = corzh;
                    }
                    else if (page == 4)
                    {
                        sec_menu = glaze;
                    }

                    switch (knopka.Key)
                    {
                        case ConsoleKey.UpArrow:
                            ChangePosition(knopka,page);
                            break;
                        case ConsoleKey.DownArrow:
                            ChangePosition(knopka,page);

                            break;
                        case ConsoleKey.Enter:
                            Console.SetCursorPosition(0, 5);
                            Console.WriteLine("Успешно добавлено");
                            knopka = Console.ReadKey(true);
                            if (pos == 0)
                            {
                                cost += sec_menu.cost[0];
                                Order += sec_menu.orders[0] + "; ";
                            }
                            else if (pos == 1)
                            {
                                cost += sec_menu.cost[1];
                                Order += sec_menu.orders[1] + "; ";
                            }
                            else if (pos == 2)
                            {
                                cost += sec_menu.cost[2];
                                Order += sec_menu.orders[2] + "; ";
                            }
                            else if (pos == 3)
                            {
                                cost += sec_menu.cost[3];
                                Order += sec_menu.orders[3] + "; ";
                            }

                            break;
                    }
                }
            }

            pos = 0;
            page = 0;
            Console.Clear();
            GlavMenu();
        }
    }
}

