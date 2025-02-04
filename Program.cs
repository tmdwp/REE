namespace Ree
{
    class Program
    {
        internal class Game
        {
            //캐릭터 관련
            public interface ICharacter
            {
                string Name { get; }
                int Health { get; set; }
                int Attack { get; }
                int Defense { get; }

                string Job { get; }
            }
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
                
                public Dictionary<int, MyItem> Inventory { get; set; }

                public int EquipAttack = 0;
                public int EquipDefense = 0;
                
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

                public void StatusOpen()
                {
                    int select;
                    do
                    {
                        select = -1;
                        Console.Clear();
                        Console.WriteLine("\n-------------------------------------------\n");
                        Console.WriteLine("\t\t상태 보기");
                        Console.WriteLine("\n-------------------------------------------");
                        Console.WriteLine("Lv." + Level);
                        Console.WriteLine(Name + "(" + Job + ")");
                        Console.Write("공격력: " + Attack);
                        if (EquipAttack != 0)
                        {
                            Console.WriteLine("(+" + EquipAttack + ")");
                        }
                        else Console.WriteLine();
                        Console.Write("방어력: " + Defense);
                        if (EquipDefense != 0)
                        {
                            Console.WriteLine("(+" + EquipAttack + ")");
                        }
                        else Console.WriteLine();
                        Console.WriteLine("체력:" + Health);
                        Console.WriteLine("Gold:" + Gold);
                        Console.WriteLine("\n-------------------------------------------");

                        if (select == 0)
                            break;
                        Console.WriteLine("0. 돌아가기");
                        Console.Write("원하는 행동을 입력해주세요: ");
                        
                    } while ((!int.TryParse(Console.ReadLine(), out select)) || select != 0);
                    
                }

                public void OpenInventory()
                {
                    int input = -1;
                    do
                    {
                      Console.Clear();
                      Console.WriteLine("\n-------------------------------------------" +
                                        "\n\t\t인벤토리\n\n-------------------------------------------" +
                                        "\n\t\t[ 아이템 목록 ]\n");

                      foreach (MyItem item in Inventory.Values)
                      {
                          item.Inform();
                      }
                      Console.WriteLine("\n-------------------------------------------\n");

                      Console.WriteLine("\n다음 행동? 0 : 돌아가기 1: 장착 관리");
                      while (!int.TryParse(Console.ReadLine(), out input))
                      {
                          Console.WriteLine("잘못된 입력입니다!!!");
                      }

                      if (input == 0)
                          break;
                      else if (input == 1)
                      {
                          Console.Clear();
                          item.Inform();
                          input++;
                      }
                      Console.WriteLine("\n-------------------------------------------\n");
                      Console.Write("\n 장착/장착 해제할 아이템 : (0 - 나가기 | 장비 번호 - 장착/해제)");
                      while (!int.TryParse(Console.ReadLine(), out input))
                      {
                          Console.WriteLine("잘못된 입력입니다!!!!!");
                      }

                      if (input != 0)
                      {
                          input--;
                          if (input < Inventory.Count)
                          {
                              Inventory[input].Equip(this);
                              Console.WriteLine("아이템 장착 완료");
                          }
                          else
                          {
                              Console.WriteLine("잘못된 입력입니다!!! 마을로 귀환합니다.");
                          }
                      }

                      input = -1;
                    } 
                }while (input != 0);
            }
            
            public interface Item
            {
                public string Name { get; }
                public int Category { get; }
                public int Stat { get; }
                public string Descript { get; }
            }
            
            //분류
            public enum ItemCategory 
            {
                Weapon = 1,
                Armor,
                chaos
            }
            
            // 보유 중
            public class MyItem : Item
            {
                public string Name { get; set; }
                public int Category { get; set; }
                public int Stat { get; set; }
                public string Descript { get; set; }
                public bool IsEquip;

                public MyItem(string name, int category, int stat, string descript)
                {
                    Name = name;
                    Category = category;
                    Stat = stat;
                    Descript = descript;
                    IsEquip = false;
                }
            }
            public class Shop
            {
                private List<ShopItem> shopList;
                private int buy;

                public Shop()
                {
                    shopList = new List<ShopItem>();
                    
                }
                public void Setting Shop()
                {
                    shopList.Add(new ShopItem("수련 갑옷", (int)ItemCategory.Armor, 5, "수련자용 갑옷이다.", 500));
                    shopList.Add(new ShopItem("무쇠 갑옷", (int)ItemCategory.Armor, 9, "무쇠로 만든 갑옷이다.", 1000));
                    shopList.Add(new ShopItem("강철 갑옷", (int)ItemCategory.Armor, 15, "강철로 만든 갑옷이다.", 2000));
                    shopList.Add(new ShopItem("동검", (int)ItemCategory.Weapon, 3, "생각보다 단단한 동검이다.", 500));
                    shopList.Add(new ShopItem("훈련용 검", (int)ItemCategory.Weapon, 5, "훈련용이지만 날이 서있는 검이다.", 1200));
                    shopList.Add(new ShopItem("매우 무거운 검", (int)ItemCategory.Weapon, 15, "많이 무거운 검이다. 다치지 않게 조심하자.", 2300));
                }
                
            }
        }
    }
    
}