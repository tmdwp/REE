﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ree
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

    public class Player : ICharacter
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

        public Player(string name)
        {
            //Hp, 공격력, 방어력, 레벨, 골드, 이름이 들어가야 함
            Level = 1;
            Name = name;
            Job = "전사";
            Attack = 10;
            Defense = 5;
            Health = 100;
            Gold = 1500;
            Inventory = new Dictionary<int, MyItem>();
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

                foreach (MyItem item in Inventory.Values) //여기서 생성된 item은 foreach문을 벗어나면 삭제!
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
                    int num = 1;
                    foreach (MyItem item in Inventory.Values) //수정 : item 정보 호출을 위해 다시 foreach문으로!
                    {
                        Console.Write($"- {num} ");// 수정 : 아이템 번호 출력
                        item.Inform();
                        num++; // 수정 : num은 아이템 번호를 알려주기 위해 추가
                    }

                    // } <- 장비 장착까지는 else if (input == 1) 안에 있어야 하니 중괄호 위치 바꾸기!

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
                     /* 수정 : 이 부분은 어차피 안보이기에 삭제할게요! 
                       else
                        {
                            Console.WriteLine("잘못된 입력입니다!!! ");
                        } 
                     */
                    }
                } // 수정 : 장비 장착까지는 else if (input == 1) 안에 있어야 하니 중괄호 위치 바꾸기!
                input = -1;
            } while (input != 0);
        }
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
        Armor
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

        public void Equip(Player player)
        {
            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    if (IsEquip == false)
                    {
                        player.Attack += Stat;
                        player.EquipAttack += Stat;
                        IsEquip = true;
                    }
                    else
                    {
                        player.Attack -= Stat;
                        player.EquipAttack -= Stat;
                        IsEquip = false;
                    }

                    break;
            }
        }
        public void Inform()    //수정 Inform 메소드를 MyItem 클래스 안으로
        {
            if (IsEquip == true)
            {
                Console.Write("[E]"); //수정 아이템 정보 바로 앞에 출력되어야 하니 Line은 삭제!
            }

            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    Console.WriteLine(Name + " | 공격력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.Armor:
                    Console.WriteLine(Name + " | 방어력 +" + Stat + " | " + Descript);
                    break;
            }
        }
    }



    public class ShopItem : Item
    {
        public string Name { get; set; }
        public int Category { get; set; }
        public int Stat { get; set; }
        public string Descript { get; set; }
        public bool IsBuy;
        public int Price;

        public ShopItem(string name, int category, int stat, string descript, int price)
        {
            Name = name;
            Category = category;
            Stat = stat;
            Descript = descript;
            IsBuy = false;
            Price = price;
        }

        public void Inform()
        {
            switch (Category)
            {
                case (int)ItemCategory.Weapon:
                    Console.Write(Name + " | 공격력 +" + Stat + " | " + Descript);
                    break;
                case (int)ItemCategory.Armor:
                    Console.Write(Name + " | 방어력 +" + Stat + " | " + Descript);
                    break;
            }

            if (IsBuy)
            {
                Console.WriteLine(" | 구매 완료!");
            }
            else Console.WriteLine(" | " + Price + "G");
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

        public void SettingShop()
        {
            shopList.Add(new ShopItem("수련 갑옷", (int)ItemCategory.Armor, 5, "수련자용 갑옷이다.", 500));
            shopList.Add(new ShopItem("무쇠 갑옷", (int)ItemCategory.Armor, 9, "무쇠로 만든 갑옷이다.", 1000));
            shopList.Add(new ShopItem("강철 갑옷", (int)ItemCategory.Armor, 15, "강철로 만든 갑옷이다.", 2000));
            shopList.Add(new ShopItem("동검", (int)ItemCategory.Weapon, 3, "생각보다 단단한 동검이다.", 500));
            shopList.Add(new ShopItem("훈련용 검", (int)ItemCategory.Weapon, 5, "훈련용이지만 날이 서있는 검이다.", 1200));
            shopList.Add(new ShopItem("매우 무거운 검", (int)ItemCategory.Weapon, 15, "많이 무거운 검이다. 다치지 않게 조심하자.", 2300));
        }

        public void ShopMenu(Player player)
        {
            int count = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("\n-------------------------------------------\n");
                Console.WriteLine("\t\t상점");
                Console.WriteLine("\n-------------------------------------------");

                foreach (ShopItem item in shopList)
                {
                    Console.Write("-" + count + " "); // 수정 : 아이템 정보 앞에 번호가 나와야 하니 Line 삭제!
                    item.Inform();
                    count++;
                }

                ShopBuy(player);
                if (buy != 0)
                    break;
                count = 1;
            } while (buy != 0);
        }

        public void ShopBuy(Player player) // 구매!!!
        {
            Console.WriteLine("\n-------------------------------------------\n");
            Console.WriteLine("현재 G : " + player.Gold + "G\n");
            Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + shopList.Count + " - 구매할 아이템 번호)");
            while (!int.TryParse(Console.ReadLine(), out buy))
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("구매할 아이템을 선택하세요 ( 0 - 나가기 | 1 ~ " + shopList.Count + " - 구매할 아이템 번호)");
            }

            if (buy > 0 && buy <= shopList.Count)
            {
                buy--;
                if (!shopList[buy].IsBuy)
                {
                    if (player.Gold >= shopList[buy].Price)
                    {
                        ShopItem selected = shopList[buy];
                        player.Gold -= selected.Price;
                        MyItem buyitem = new MyItem(selected.Name, selected.Category, selected.Stat,
                            selected.Descript);
                        player.Inventory.Add(player.Inventory.Count, buyitem);
                        selected.IsBuy = true;

                        Console.WriteLine("아이템을 구매하였습니다.");
                        Console.WriteLine("현재 G : " + player.Gold + "G\n");
                    }
                    else
                    {
                        Console.WriteLine("가지고 있는 골드가 부족해요!!");
                    }
                }
                else
                {
                    Console.WriteLine("이미 가지고 있는 아이템인데요!");
                }

                buy++;
                Console.ReadLine();
            }
            else if (buy != 0)
            {
                Console.WriteLine("없는 아이템입니다!!!");
            }
        }

    }

    // 마을
    public class Town
    {
        public void Start(Player player, Shop shop)
        {

            while (true)
            {
                Console.WriteLine("\n-------------------------------------------");
                Console.WriteLine("\t여기는 마을입니다.");
                Console.WriteLine("던전에 들어가기 전 행동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점\n");
                Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
                Console.WriteLine("다른 번호를 입력하면 게임이 종료됩니다.");
                int move; //선택한 행동
                while (!int.TryParse(Console.ReadLine(), out move))
                {
                    Console.WriteLine("숫자가 아닙니다.");
                    Console.WriteLine("원하는 행동을 선택해주세요.(1~3)");
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
                    case 1010:
                        player.Gold += 50000;
                        Console.WriteLine("test");
                        break;
                    default:
                        Console.WriteLine("게임을 종료합니다.");
                        Console.WriteLine("안녕히 가세요.\n\n");
                        return;

                }
            }
        }

        internal class Program //수정 : 맨 위의 class Program 삭제하고 이 줄의 RPG를 Program으로
        {
            static void Main(string[] args)
            {
                Console.WriteLine("당신의 이름은 뭔가요?");
                string name = Console.ReadLine();
                Player player = new Player(name);
                // 수정 : 여기서 아이템 추가하지 않아도 인벤토리 작동 확인 가능해서 삭제!
                //MyItem item = new MyItem("나무 막대", 1, 1, "연습용 막대기이다.");
                //player.Inventory.Add(player.Inventory.Count, item);
                Town town = new Town();
                Shop shop = new Shop();
                shop.SettingShop();
                town.Start(player, shop);
            }
        }
    } //수정 : 중괄호 하나 삭제
}