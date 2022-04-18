namespace L3 //4 вариант - прямоугольник
{
    //[Serializable]
    public class CannotBeBuildException : Exception
    {
        public CannotBeBuildException() { }
        public CannotBeBuildException(string message)
            : base(message) { }
        public CannotBeBuildException(string message, Exception inner)
            : base(message, inner) { }
    }
    public delegate void EventHandler(double a);
    public delegate void ExceptionEventHandler();
    public class Point
    {
        private string name;
        private double x;
        private double y;
        public Point(string name = "", double x = double.MaxValue, double y = double.MaxValue)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        public Point(double x = double.MaxValue, double y = double.MaxValue)
        {
            name = "";
            this.x = x;
            this.y = y;
        }
        public string Name { get { return name; } set { name = value; } }
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
    }
    public class Rectangle
    {
        private Point[] coordinates = new Point[4];
        double area;
        public event ExceptionEventHandler Ex;
        public Point this[int index] { get { return coordinates[index]; } set { coordinates[index] = value; } }//небезопасное изменение координат
        public Rectangle(string name1 = "A", double x1 = 0, double y1 = 0, string name2 = "B", double x2 = 0, double y2 = 0, string name3 = "C", double x3 = 0, double y3 = 0, string name4 = "D", double x4 = 0, double y4 = 0)
        {
            coordinates[0] = new Point(name1, x1, y1);
            coordinates[1] = new Point(name2, x2, y2);
            coordinates[2] = new Point(name3, x3, y3);
            coordinates[3] = new Point(name4, x4, y4);
            ReloadArea();// AreaIsOne();

        }
        public Rectangle(double x1 = 0, double y1 = 0, double x2 = 0, double y2 = 0, double x3 = 0, double y3 = 0, double x4 = 0, double y4 = 0)
        {
            coordinates[0] = new Point("A", x1, y1);
            coordinates[1] = new Point("B", x2, y2);
            coordinates[2] = new Point("C", x3, y3);
            coordinates[3] = new Point("D", x4, y4);
            ReloadArea();
            //AreaIsOne();
        }
        public void ChangePoint(int index, double x, double y)
        {
            double TempX = coordinates[index].X;
            double TempY = coordinates[index].Y;
            try
            {
                coordinates[index].X = x;
                coordinates[index].Y = y;
                ReloadArea(); //AreaIsOne();

            }
            catch (Exception /*e*/)
            {
                //Console.WriteLine(e.Message);
                coordinates[index].X = TempX;
                coordinates[index].Y = TempY;
                Console.WriteLine("Не удалось. Изменения не применены");
                throw;
            }

        }//безопасное изменение координат
        public double Area { get { ReloadArea(); /*AreaIsOne();*/ return area; } }
        public event EventHandler Ac;
        private double oldArea;
        private void AreaIsOne() { if (area == 1) { Ac(oldArea); oldArea = area; } }
        public void ReloadArea()
        {
            AreaIsOne();
            oldArea = area;
            CanBuild();
            System.Numerics.Vector2[] side = new System.Numerics.Vector2[6];
            side[0] = new System.Numerics.Vector2((float)(this[0].X - this[1].X), (float)(this[0].Y - this[1].Y));
            side[1] = new System.Numerics.Vector2((float)(this[1].X - this[2].X), (float)(this[1].Y - this[2].Y));
            side[2] = new System.Numerics.Vector2((float)(this[2].X - this[3].X), (float)(this[2].Y - this[3].Y));
            side[3] = new System.Numerics.Vector2((float)(this[3].X - this[0].X), (float)(this[3].Y - this[0].Y));
            side[4] = new System.Numerics.Vector2((float)(this[0].X - this[2].X), (float)(this[0].Y - this[2].Y));
            side[5] = new System.Numerics.Vector2((float)(this[1].X - this[3].X), (float)(this[1].Y - this[3].Y));

            int TotallyNotADiagonal = FindNotDiagonal(side);
            System.Numerics.Vector2 side2 = new System.Numerics.Vector2((float)(this[1].X - this[2].X), (float)(this[1].Y - this[2].Y));
            if (System.Numerics.Vector2.Dot(side[TotallyNotADiagonal], side2) == 0) { area = side[TotallyNotADiagonal].Length() * side2.Length(); return; }
            side2 = new System.Numerics.Vector2((float)(this[1].X - this[3].X), (float)(this[1].Y - this[3].Y));
            if (System.Numerics.Vector2.Dot(side[TotallyNotADiagonal], side2) == 0) { area = side[TotallyNotADiagonal].Length() * side2.Length(); return; }
            side2 = new System.Numerics.Vector2((float)(this[2].X - this[3].X), (float)(this[2].Y - this[3].Y));
            if (System.Numerics.Vector2.Dot(side[TotallyNotADiagonal], side2) == 0) { area = side[TotallyNotADiagonal].Length() * side2.Length(); return; }
            side2 = new System.Numerics.Vector2((float)(this[2].X - this[0].X), (float)(this[2].Y - this[0].Y));
            if (System.Numerics.Vector2.Dot(side[TotallyNotADiagonal], side2) == 0) { area = side[TotallyNotADiagonal].Length() * side2.Length(); return; }
            side2 = new System.Numerics.Vector2((float)(this[3].X - this[0].X), (float)(this[3].Y - this[0].Y));
            if (System.Numerics.Vector2.Dot(side[TotallyNotADiagonal], side2) == 0) { area = side[TotallyNotADiagonal].Length() * side2.Length(); return; }
            throw new Exception("Что-то не так");//что-то пошло не так

        }
        private void CanBuild()
        {
            System.Numerics.Vector2 side1 = new System.Numerics.Vector2((float)(this[0].X - this[1].X), (float)(this[0].Y - this[1].Y));
            System.Numerics.Vector2 side2 = new System.Numerics.Vector2((float)(this[1].X - this[2].X), (float)(this[1].Y - this[2].Y));
            System.Numerics.Vector2 side3 = new System.Numerics.Vector2((float)(this[2].X - this[3].X), (float)(this[2].Y - this[3].Y));
            System.Numerics.Vector2 side4 = new System.Numerics.Vector2((float)(this[3].X - this[0].X), (float)(this[3].Y - this[0].Y));
            System.Numerics.Vector2 side5 = new System.Numerics.Vector2((float)(this[0].X - this[2].X), (float)(this[0].Y - this[2].Y));
            System.Numerics.Vector2 side6 = new System.Numerics.Vector2((float)(this[1].X - this[3].X), (float)(this[1].Y - this[3].Y));
            if (side1.Length() == 0 || side2.Length() == 0 || side3.Length() == 0 || side4.Length() == 0 || side5.Length() == 0 || side6.Length() == 0)
            {
                Ex();
                throw new CannotBeBuildException("Нельзя построить");
            }
            double[] sides = new double[] { side1.Length(), 0, 0 };
            int[] NumberOfSides = new int[] { 1, 0, 0 };
            if (side2.Length() == sides[0]) { NumberOfSides[0]++; } else { NumberOfSides[1]++; sides[1] = side2.Length(); }

            if (side3.Length() == sides[0]) { NumberOfSides[0]++; }
            else
            {
                if (sides[1] == 0) { sides[1] = side3.Length(); }
                if (side3.Length() == sides[1]) { NumberOfSides[1]++; } else { NumberOfSides[2]++; sides[2] = side3.Length(); }
            }

            if (side4.Length() == sides[0]) { NumberOfSides[0]++; }
            else
            {
                if (sides[1] == 0) { sides[1] = side4.Length(); }
                if (side4.Length() == sides[1]) { NumberOfSides[1]++; }
                else
                {
                    if (sides[2] == 0) { sides[2] = side4.Length(); }
                    if (side4.Length() == sides[2]) { NumberOfSides[2]++; }
                    else
                    {
                        Ex();
                        throw new CannotBeBuildException("Нельзя построить");
                    }
                }
            }
            if (side5.Length() == sides[0]) { NumberOfSides[0]++; }
            else
            {
                if (sides[1] == 0) { sides[1] = side5.Length(); }
                if (side5.Length() == sides[1]) { NumberOfSides[1]++; }
                else
                {
                    if (sides[2] == 0) { sides[2] = side5.Length(); }
                    if (side5.Length() == sides[2]) { NumberOfSides[2]++; } else { Ex(); throw new CannotBeBuildException("Нельзя построить"); }
                }
            }
            if (side6.Length() == sides[0]) { NumberOfSides[0]++; }
            else
            {
                if (sides[1] == 0) { sides[1] = side6.Length(); }
                if (side6.Length() == sides[1]) { NumberOfSides[1]++; }
                else
                {
                    if (sides[2] == 0) { sides[2] = side6.Length(); }
                    if (side6.Length() == sides[2]) { NumberOfSides[2]++; } else { Ex(); throw new CannotBeBuildException("Нельзя построить"); }
                }
            }//заполнение закончено, теперь проверка
            if ((NumberOfSides[0] == 2 && NumberOfSides[1] == 2 && NumberOfSides[2] == 2) ||
                (NumberOfSides[0] == 4 && NumberOfSides[1] == 2) ||
                (NumberOfSides[0] == 2 && NumberOfSides[1] == 4)) {/*всё хорошо*/ }
            else
            { Ex(); throw new CannotBeBuildException("Нельзя построить"); }
            //если дошли до сюда - можно построить
        }
        private int FindNotDiagonal(System.Numerics.Vector2[] side)
        {
            int MinI = 0;
            for (int i = 1; i < side.Length; i++)
            {
                if (side[i].Length() < side[MinI].Length()) { MinI = i; }
            }
            return MinI;
        }
    }
    public class Programm
    {
        static void handler_area(double area) { Console.WriteLine(area + " -> Событие. Площадь = 1"); }
        static void ExHandler() { Console.WriteLine("Событие. ЗАПРЕЩЕНО!!!!!1111"); }
        public static void Main()
        {
            Rectangle f = new Rectangle(0, 0, 0.0000000000000000000001D, 0.0000000000000000000001D, 0, 0.0000000000000000000001D, 0.0000000000000000000001D, 0);
            f.Ac += new EventHandler(handler_area);
            f.Ex += new ExceptionEventHandler(ExHandler);
            Console.Write("Введите координаты четырёх точек : \n");
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.Write("Введите координаты {0} точки: ", i + 1);
                    if (i == 0) Console.Write("(x и y через пробел)");
                    var coordRow = Console.ReadLine();
                    var coord = coordRow.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    coord[0] = coord[0].ToString().Replace(".", ",");
                    coord[1] = coord[1].ToString().Replace(".", ",");
                    f[i].X = Double.Parse(coord[0]);
                    f[i].Y = Double.Parse(coord[1]);
                }
                f.ReloadArea();
                //Console.WriteLine("Создано");
                //Thread.Sleep(500);
                Console.ReadKey(); //Console.ReadKey();

            }
            catch (CannotBeBuildException)
            {
                Console.WriteLine("Нельзя построить прямоугольник из этих точек");
            }
            catch (FormatException) { Console.WriteLine("Не удалось преобразовать при вводе"); }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Не удалось преобразовать при вводе");
            } 
            catch (Exception)
            {
                Console.WriteLine("Что-то пошло не так");
            }

            while (true)
            {
                char menu;
                
                Console.WriteLine("\tСегодня в меню:\n" +
                    "1 - Создать прямоугольник\n" +
                    "2 - Узнать его площадь\n" +
                    "3 - Изменить фигуру\n" +
                    "4 - Показать точки");
                try
                {
                    menu = Console.ReadLine()[0];
                }
                catch (Exception)
                {
                    Console.WriteLine("Опечатка");
                    menu = ' ';
                }
                switch (menu)
                {
                    case '1':
                        {
                            Console.Write("Введите координаты четырёх точек: \n");
                            try
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.Write("Введите координаты {0} точки: ", i + 1);
                                    var coordRow = Console.ReadLine();
                                    var coord = coordRow.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    coord[0] = coord[0].ToString().Replace(".", ",");
                                    coord[1] = coord[1].ToString().Replace(".", ",");
                                    f[i].X = Double.Parse(coord[0]);
                                    f[i].Y = Double.Parse(coord[1]);
                                }
                                f.ReloadArea();
                                Console.WriteLine("Создано");
                                Console.ReadKey();
                            }
                            catch (CannotBeBuildException)
                            {
                                Console.WriteLine("Нельзя построить прямоугольник из этих точек");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Не удалось преобразовать при вводе");
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Не удалось преобразовать при вводе");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Что-то пошло не так");
                            }
                            //Console.Clear();
                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine("Площадь = " + f.Area.ToString("g5"));
                            Console.ReadKey(); Console.Clear();
                            break;
                        }
                    case '3':
                        {
                            while (true)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.WriteLine("Точка {0}: x = {1}, y = {2}", i + 1, f[i].X, f[i].Y);
                                }

                                int ind;
                                while (true)
                                {
                                    Console.Write("Номер какой точки изменить? ");
                                    try
                                    {
                                        ind = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Опечатка");
                                    }
                                }
                                try
                                {
                                    Console.Write("Введите координаты {0} точки: ", ind);
                                    var coordRow = Console.ReadLine();
                                    var coord = coordRow.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    coord[0] = coord[0].ToString().Replace(".", ",");
                                    coord[1] = coord[1].ToString().Replace(".", ",");
                                    f[ind - 1].X = Double.Parse(coord[0]);
                                    f[ind - 1].Y = Double.Parse(coord[1]);
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Не удалось преобразовать при вводе");
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Не удалось преобразовать при вводе");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Что-то пошло не так");
                                }
                                Console.WriteLine("Изменить ещё (y/n): ");
                                string y_n = Console.ReadLine();
                                if (y_n == "y" || y_n == "yes") { } else break;
                            }
                            try
                            {
                                f.ReloadArea();

                            }
                            catch (Exception)
                            {

                                
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                    case '4':
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Console.WriteLine("Точка {0}: x = {1}, y = {2}", i + 1, f[i].X, f[i].Y);
                            }
                            break;
                        }
                }

            }
        }
    }
}
