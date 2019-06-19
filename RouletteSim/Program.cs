using System;
using System.Collections.Generic;

namespace RouletteSim
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                int OuterIterations = 0;
                List<(bool, decimal)> Results = new List<(bool, decimal)>();

                int winCount = 0;
                int lossCount = 0;

                decimal OverallBiggestBet = 0;
                decimal AverageBalance = 0;

                decimal LossTotal = 0;
                decimal WinTotal = 0;
                do
                {
                    OuterIterations += 1;
                    Random r = new Random();
                    decimal initialBal = 10000m;
                    decimal bal = initialBal;
                    decimal initialBet = 1m;
                    decimal bet = initialBet;
                    int iteration = 0;
                    int maxIterations = 10;
                    List<int> Reds = new List<int> { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
                    List<int> Blacks = new List<int> { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };


                    bool win;
                    int wins = 0;
                    int losses = 0;
                    decimal biggestBet = 1;

                    int winStreak = 0;

                    while (bal > 0 && iteration < maxIterations)
                    {
                        iteration += 1;
                        bal -= bet; 
                        var num = r.Next(0, 37);
                        if (bet > biggestBet)
                            biggestBet = bet;
                        if (Reds.Contains(num))
                        {
                            winStreak += 1;
                            bal += 2 * bet;
                            //bet = initialBet;
                            if(winStreak < 4)
                            {
                                if ((bet * 2) > bal)
                                    bet = bal;
                                else
                                    bet *= 2;
                            }
                            
                            win = true;
                            wins += 1;
                        }
                        else
                        {
                            bet = initialBet;

                            //if ((bet * 2) > bal)
                            //    bet = bal;
                            //else
                            //    bet *= 2;
                            win = false;
                            losses += 1;
                        }
                        //System.Threading.Thread.Sleep(5000);
                        //Console.WriteLine($"Balance: {bal} \n Bet: {bet} \n Iteration: {iteration} \n Win: {win}");
                        //Console.WriteLine();
                    }
                    Console.WriteLine($"Wins: {wins}, Losses: {losses}, BiggestBet: {biggestBet}");
                    if (biggestBet > OverallBiggestBet)
                        OverallBiggestBet = biggestBet;

                    if (bal < initialBal)
                    {
                        Results.Add((false, bal));
                        LossTotal = (LossTotal - initialBal) + bal; 
                        lossCount += 1;
                    }

                    else {
                        Results.Add((true, bal));
                        WinTotal += bal - initialBal;
                        winCount += 1;
                        AverageBalance += bal;
                    }
                }
                while (OuterIterations < 1000);
                foreach (var item in Results)
                    Console.WriteLine($"Success: {item.Item1}, Balance: {item.Item2}");

                Console.WriteLine();
                Console.WriteLine($"{winCount}W, \n{lossCount}L, \nBiggestBet = £{OverallBiggestBet}, \nAverageBalance = £{AverageBalance/winCount}, \nWinTotal = £{WinTotal}, \nLossTotal = £{LossTotal}, \nAggregate = £{WinTotal + LossTotal}");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            }

            
        }
    }

