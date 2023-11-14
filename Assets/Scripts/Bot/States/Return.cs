using UnityEngine;

namespace BotStates
{
    public class Return : BotState
    {
        private Transform _home;

        public override void Enter()
        {
            if (_bot.Home == null)
                ChangeState("Idle");

            _home = _bot.Home.transform;
        }

        public override void Process()
        {
            _bot.LookAt(_home);
            _bot.MoveTowards(_home);

            TryDeposit(_home);
        }

        private void TryDeposit(Transform target)
        {
            float distanceSquared = Vector3.SqrMagnitude(_bot.transform.position - target.position);

            if (distanceSquared > _bot.InteractDistance * _bot.InteractDistance)
                return;

            if (_bot.TargetResource != null)
                _bot.Home.Deposit(_bot, _bot.TargetResource);

            _bot.ClearGoals();

            ChangeState("Idle");
        }
    }
}
