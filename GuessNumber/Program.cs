using GuessNumber.StateMachines;
using GuessNumber.States;
using System;

namespace GuessNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var appStateMachine = new AppStateMachine();

            appStateMachine.TransitionAction(AppStates.Idle);

            while (true)
            {
                appStateMachine.StateAction();
            }
        }
    }
}
