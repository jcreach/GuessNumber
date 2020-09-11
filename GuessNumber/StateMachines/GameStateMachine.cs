using GuessNumber.States;
using System;

namespace GuessNumber.StateMachines
{
    public class GameStateMachine : StateMachine<GameStates>
    {

        private int numberToGuess;
        private int gameLimit;
        private int turnToVictory;

        public GameStateMachine() : base(GameStates.Init)
        {

        }
            
        private void InitTransition(GameStates previousState) 
            => Console.WriteLine($"=> Game initialisation");

        private void InitState()
        {
            Console.Write(" > Enter the max limit : ");
            var entry = Console.ReadLine();
            if (!int.TryParse(entry, out gameLimit))
            {
                Console.WriteLine("Wrong type");
                TransitionAction(GameStates.Init);
                return;
            }

            Console.WriteLine($" > In this game you should guess a number between  0 and {gameLimit}");

            var rdm = new Random();
            numberToGuess = rdm.Next(0, gameLimit);

            TransitionAction(GameStates.Playing);
        }

        private void PlayingTransition(GameStates previousState)
            => Console.WriteLine($"=> Game start");

        private void PlayingState()
        {
            turnToVictory = 0;
            int number;
            do
            {
                Console.Write($" > Please enter a number between 0 and {gameLimit} : ");
                var entry = Console.ReadLine();

                if (!int.TryParse(entry, out number))
                {
                    Console.WriteLine(" > Wrong type");
                    continue;
                }

                if (number < 0 || number > gameLimit)
                {
                    Console.WriteLine(" > Out of bounds");
                    continue;
                }

                if (number > numberToGuess)
                    Console.WriteLine(" > It's Less !");
                else if(number < numberToGuess)
                    Console.WriteLine(" > It's more !");

                turnToVictory++;

            } while (number != numberToGuess);

            TransitionAction(GameStates.Win);
        }

        private void WinTransition(GameStates previousState)
            => Console.WriteLine($"=> Game finish - You Win in {turnToVictory} turns !!!");

        private void WinState()
        {
            //Console.WriteLine($" => Game finish - You Win in {turnToVictory} turns !!!");
            TransitionAction(GameStates.Finish);
        }
        
        private void LooseTransition(GameStates previousState)
            => Console.WriteLine($"=> Game finish - You Loose !!!");

        private void LooseState()
        {
            //Console.WriteLine(" => Game finish - You Loose !!!");
        }

        public bool IsGameFinish()
        {
            return State == GameStates.Finish;
        }
    }
}
