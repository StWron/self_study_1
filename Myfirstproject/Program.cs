using System;
using System.Collections.Generic;
using Myfirstproject; // 같은 namespace 사용

namespace Myfirstproject
{
    class Program
    {
        // 프로그램 시작점 (Main)
        static void Main(string[] args)
        {
            // Weather.cs에 있는 WeatherTable을 불러옵니다.
            WeatherTable table = new WeatherTable();

            while (true)
            {
                Console.WriteLine("\n=== 통합된 날씨 관리 시스템 (Myfirstproject) ===");
                Console.WriteLine("1. 날씨 검색 (요일 입력)");
                Console.WriteLine("2. 평균 최고온도 보기");
                Console.WriteLine("3. 평균 최저온도 보기");
                Console.WriteLine("4. 새 날씨 추가 (오래된 데이터 삭제됨)");
                Console.WriteLine("5. 전체 데이터 보기");
                Console.WriteLine("6. 삭제된 기록 보기 (History)");
                Console.WriteLine("0. 종료");
                Console.Write("메뉴 선택: ");

                string menu = Console.ReadLine();

                if (menu == "0") break;

                else if (menu == "1") // 기능1: 날짜 데이터 출력
                {
                    Console.Write("검색할 요일 입력: ");
                    string input = Console.ReadLine();
                    int index = table.FindDayIndex(input);

                    if (index != -1)
                    {
                        Console.WriteLine($"결과: {table.GetFormattedData(index)}");
                    }
                    else
                    {
                        Console.WriteLine($"오류: '{input}' 정보를 찾을 수 없습니다.");
                    }
                }
                else if (menu == "2") // 기능2: 평균 최고온도
                {
                    double avgMax = table.GetAverageMaxTemp();
                    Console.WriteLine($"일주일간 평균 최고온도: {avgMax:F1}도");
                }
                else if (menu == "3") // 기능3: 평균 최저온도
                {
                    double avgMin = table.GetAverageMinTemp();
                    Console.WriteLine($"일주일간 평균 최저온도: {avgMin:F1}도");
                }
                else if (menu == "4") // 추가
                {
                    Console.WriteLine("\n[새로운 데이터 입력]");
                    string[] newData = new string[6];
                    string[] prompts = { "요일", "현재온도", "최저온도", "최고온도", "하늘상태", "습도" };

                    for (int i = 0; i < 6; i++)
                    {
                        Console.Write($"{prompts[i]} 입력: ");
                        newData[i] = Console.ReadLine();
                    }

                    table.AddDataAndShift(newData);
                    Console.WriteLine("추가 완료! 맨 앞의 데이터가 삭제되고 기록에 저장되었습니다.");
                }
                else if (menu == "5") // 전체 보기
                {
                    table.PrintAll();
                }
                else if (menu == "6") // 히스토리 보기
                {
                    table.PrintHistory();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
    }
}