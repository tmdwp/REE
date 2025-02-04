namespace Ree
{
    class Program
    {
        internal class Game
        {
            static void Main(string[] args)
            {
                Console.WriteLine("당신의 이름을 입력해 주세요.");
                string playerName = Console.ReadLine(); // 이름 짓기

                while (true)
                {
                    Console.WriteLine("스파르타 마을에 오신 것을 환영합니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                    Console.WriteLine("1.상태 보기");
                    Console.WriteLine("2.인벤토리");
                    Console.WriteLine("3.상점\n");
                    Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
                    int Move;
                    while (!int.TryParse(Console.ReadLine(), out Move))
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    switch (move)
                    {
                        case 1:
                            Console.Clear();
                            player.StatusOpen();
                            break;
                        case 2:
                            Console.Clear();
                            player.OpenInventory();
                            break;
                        case 3:
                            Console.Clear();
                            shop.ShopMenu(player);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            return;
                    }
                }
            }

            public class player
            {
                public int Level { get; set; }
                public string Name { get; }
                public string Job { get; }
                public int Health { get; set; }
                public int Attack { get; set; }
                public int Defense { get; set; }
                public int Gold { get; set; }

                public player(string name)
                {
                    //Hp, 공격력, 방어력, 레벨, 골드, 이름이 들어가야 함
                    Level = 1;
                    Name = name;
                    Job = "전사";
                    Attack = 10;
                    Defense = 5;
                    Health = 100;
                    Gold = 1500;
                }
            }
        }
    }
    
}