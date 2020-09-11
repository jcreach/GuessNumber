using GuessNumber.States;
using System;

namespace GuessNumber.StateMachines
{
    public class AppStateMachine : StateMachine<AppStates>
    {

        public AppStateMachine() : base(AppStates.Idle)
        {

        }

        private void IdleTransition(AppStates previousState)
            => Console.WriteLine("> I'm waiting dude ...");

        private void IdleState()
        {
            TransitionAction(AppStates.Init);
        }

        private void InitTransition(AppStates previousState)
            => Console.WriteLine("> I'm initializing dude ...");

        private void InitState()
        {
            Console.Write("> Press [p/P] to play, [q/Q] to exit :");

            var entry = Console.ReadKey();
            Console.WriteLine();

            switch (entry.Key)
            {
                case ConsoleKey.P:
                    TransitionAction(AppStates.Guessing);
                    break;

                case ConsoleKey.Q:
                    TransitionAction(AppStates.Terminated);
                    break;
                default:
                    TransitionAction(AppStates.Init);
                    break;
            }
        }

        private void GuessingTransition(AppStates previousState)
            => Console.WriteLine("> hey dude play with me ...");

        private void GuessingState()
        {
            var gameStateMachine = new GameStateMachine();

            gameStateMachine.TransitionAction(GameStates.Init);

            while (!gameStateMachine.IsGameFinish())
            {
                gameStateMachine.StateAction();
            }

            TransitionAction(AppStates.Exiting);
        }

        private void ExitingTransition(AppStates previousState)
            => Console.WriteLine($"> Do you want to play again ? Yes : [y/Y], No [n/N] : ");

        private void ExitingState()
        {
            var entry = Console.ReadKey();
            Console.WriteLine();

            switch (entry.Key)
            {
                case ConsoleKey.Y:
                    TransitionAction(AppStates.Guessing);
                    break;

                case ConsoleKey.N:
                    TransitionAction(AppStates.Terminated);
                    break;

                default:
                    break;
            }

        }

        private void TerminatedTransition(AppStates previousState)
            => Console.WriteLine($"> Press enter to exit ...");

        private void TerminatedState()
        {
            var entry = Console.ReadKey();

            if (entry.Key != ConsoleKey.Enter)
            {
                Console.WriteLine();
                Console.WriteLine($"> \"{entry.Key}\" is not the \"Enter\" key dude ...");
                TransitionAction(AppStates.Terminated);
                return;
            }

            Console.WriteLine("> Ok bye ...");
            Environment.Exit(0);
        }
    }
}
