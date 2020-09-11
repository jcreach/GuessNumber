using System;
using System.Collections.Generic;
using System.Reflection;

namespace GuessNumber.StateMachines
{
    public class StateMachine<T> where T : struct, IConvertible
    {
        public T State { get; private set; }
        private const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;
        private readonly Dictionary<T, MethodInfo> States = new Dictionary<T, MethodInfo>();
        private readonly Dictionary<T, MethodInfo> Transitions = new Dictionary<T, MethodInfo>();

        public StateMachine(T initState)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumaration");

            foreach (T state in typeof(T).GetEnumValues())
            {
                var stateMethod = GetType().GetMethod($"{state}State", FLAGS);
                if (stateMethod != null)
                    States.Add(state, stateMethod);

                var transitionMethod = GetType().GetMethod($"{state}Transition", FLAGS);
                if (transitionMethod != null)
                    Transitions.Add(state, transitionMethod);
            }

            State = initState;
        }

        public void StateAction()
        {
            if (States.TryGetValue(State, out MethodInfo action))
                action.Invoke(this, null);
        }

        public void TransitionAction(T nextState)
        {
            if (Transitions.TryGetValue(nextState, out MethodInfo action))
                action.Invoke(this, new object[] { State });

            State = nextState;
        }
    }
}
