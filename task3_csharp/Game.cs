using System;
using System.Security.Cryptography;
using System.Text;

namespace task3_csharp
{
    public class Game
    {
        private readonly String[] _moves;
        private readonly int _size;
        private byte[] _key;
        private byte[] _hmac;
        private int _yourMove;
        private int _computerMove;

        public Game()
        {
            _moves = null;
            _size = 0;
            _yourMove = 0;
            _computerMove = 0;
            _key = null;
            _hmac = null;
        }

        public Game(String[] move)
        {
            _size = move.Length;
            _moves = new string[_size];
            for (var i = 0; i < _size; i++)
            {
                _moves[i] = move[i].Substring(0, move[i].Length);
            }
            _yourMove = -1;
            _computerMove = -1;
            _key = null;
            _hmac = null;
        }

        public void Start()
        {
            while (true)
            {
                GenerateComputerMove();
                GenerateKey(32);
                GenerateHmac();
                Console.Out.WriteLine($"HMAC: {BitConverter.ToString(_hmac).Replace("-","")}");
                PrintMenu();   
                GetUserMove();
                if (_yourMove == -1)
                {
                    return;
                }
                Console.Out.WriteLine($"Your move: {_moves.GetValue(_yourMove)}");
                Console.Out.WriteLine($"Computer move: {_moves.GetValue(_computerMove)}");
                WhoWins();
                Console.Out.WriteLine($"HMAC key: {BitConverter.ToString(_key).Replace("-","")}");
                Console.Out.WriteLine("______________________________________________________________________________________________________________________________________");
                Console.Out.WriteLine("................................................Press any key to continue.............................................................");
                Console.Out.WriteLine("______________________________________________________________________________________________________________________________________");
                Console.In.ReadLine();
            } 
        }

        private void WhoWins()
        {
            if (_computerMove < _yourMove)
            {
                _computerMove += _size;
            }
            if (_computerMove == _yourMove)
            {
                Console.Out.WriteLine("Draw");
            }
            else if (_computerMove - _yourMove > _size / 2)
            {
                Console.Out.WriteLine("You Lose");
            }
            else
            {
                Console.Out.WriteLine("You Win");
            }
        }
        
        private void PrintMenu()
        {
            Console.Out.WriteLine("Available moves:");
            for (var i = 1; i <= _size; i++)
            {
                Console.Out.WriteLine($"{i} - {_moves[i - 1]}");
            }
            Console.Out.WriteLine("0 - Exit");
        }

        private void GenerateComputerMove()
        {
            _computerMove = RandomNumberGenerator.GetInt32(_size);
        }
        
        private void GetUserMove()
        {
            Console.Out.WriteLine("Please enter your move");
            String temp = Console.In.ReadLine();
            while (!int.TryParse(temp, out _yourMove) || (_yourMove < 0) || (_yourMove > _size))
            {
                Console.Out.WriteLine($"Please print numbers from 0 to {_size}");
                temp = Console.In.ReadLine();
            }
            if (_yourMove == 0)
            {
                Console.Out.WriteLine("Bye");
            }
            _yourMove--;
        }

        private void GenerateHmac()
        {
            using HMACSHA512 hmacGen = new HMACSHA512(_key);
            _hmac = hmacGen.ComputeHash(Encoding.Default.GetBytes(_moves[_computerMove]));
        }
        
        private void GenerateKey(int size)
        {
            using RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            _key = new byte[size];
            provider.GetBytes(_key);
        }
    }
}