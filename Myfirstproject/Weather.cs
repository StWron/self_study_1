using System;
using System.Collections.Generic;

namespace Myfirstproject
{
    public class WeatherTable
    {
        private string[,] _data;
        private List<string[]> _deletedHistory;

        // 처음에는 0번(목요일)을 가리킵니다.
        private int _startIndex = 0;

        // 전체 데이터 개수 (7개 고정)
        private int _totalCount = 7;

        public WeatherTable()
        {
            _data = new string[,]
            {
                { "목", "금", "토", "일", "월", "화", "수" },
                { "3", "5", "2", "3", "4", "6", "8" },
                { "1", "-1", "-1", "1", "1", "3", "2" },
                { "5", "6", "8", "4", "6", "5", "6" },
                { "맑음", "흐림", "맑음", "흐림", "맑음", "흐림", "비" },
                { "60", "30", "50", "50", "30", "30", "50" }
            };
            _deletedHistory = new List<string[]>();
        }

        // 기능: 데이터 추가
        public void AddDataAndShift(string[] newData)
        {
            int rows = _data.GetLength(0);

            string[] deletedColumn = new string[rows];
            for (int r = 0; r < rows; r++)
            {
                deletedColumn[r] = _data[r, _startIndex];
            }
            _deletedHistory.Add(deletedColumn);
            if (_deletedHistory.Count > 7) _deletedHistory.RemoveAt(0);

            for (int r = 0; r < rows; r++)
            {
                _data[r, _startIndex] = newData[r];
            }

            // (_startIndex + 1) % 7 을 하면 6 다음이 자동으로 0이 됩니다.
            _startIndex = (_startIndex + 1) % _totalCount;
        }

        // 기능: 요일 찾기 (읽을 때도 계산이 필요함)
        public int FindDayIndex(string dayName)
        {
            // 사용자는 0~6번 순서대로 있다고 생각하지만, 실제 데이터는 뒤섞여 있습니다.
            // 그래서 i를 실제 배열 위치(realIndex)로 바꿔줘야 합니다.
            for (int i = 0; i < _totalCount; i++)
            {
                // [공식] (시작점 + 떨어진거리) % 전체개수
                int realIndex = (_startIndex + i) % _totalCount;

                if (_data[0, realIndex] == dayName)
                {

                    return realIndex;
                }
            }
            return -1;
        }

        // 기능: 출력 (내부 인덱스 그대로 사용)
        public string GetFormattedData(int realIndex)
        {
            return $"{_data[0, realIndex]} (현재:{_data[1, realIndex]}, 최저:{_data[2, realIndex]}, 최고:{_data[3, realIndex]}, 날씨:{_data[4, realIndex]}, 습도:{_data[5, realIndex]})";
        }

        public double GetAverageMaxTemp()
        {
            double sum = 0;
            for (int i = 0; i < _totalCount; i++) sum += double.Parse(_data[3, i]);
            return sum / _totalCount;
        }

        public double GetAverageMinTemp()
        {
            double sum = 0;
            for (int i = 0; i < _totalCount; i++) sum += double.Parse(_data[2, i]);
            return sum / _totalCount;
        }

        // 전체 출력 (순서대로 보이게 하기 위해 연산 필요)
        public void PrintAll()
        {
            int rows = _data.GetLength(0);

            Console.WriteLine("\n--- WeatherTable 데이터 상태 (원형 버퍼) ---");
            Console.WriteLine("요일\t현재\t최저\t최고\t날씨\t습도");

            // i는 0(가장 과거) ~ 6(가장 최신) 순서
            for (int i = 0; i < _totalCount; i++)
            {
                // 실제 배열에서의 위치 계산
                int realIndex = (_startIndex + i) % _totalCount;

                for (int r = 0; r < rows; r++)
                {
                    Console.Write($"{_data[r, realIndex]}\t");
                }
                Console.WriteLine();
            }
        }

        public void PrintHistory()
        {
            Console.WriteLine("\n--- 최근 삭제된 데이터 기록 ---");
            if (_deletedHistory.Count == 0) { Console.WriteLine("기록 없음"); return; }

            int count = 1;
            for (int i = _deletedHistory.Count - 1; i >= 0; i--)
            {
                string[] col = _deletedHistory[i];
                Console.WriteLine($"[{count++}번째 전] {col[0]} / 날씨: {col[4]}");
            }
        }
    }
}