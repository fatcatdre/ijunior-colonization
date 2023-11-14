using UnityEngine;

namespace BotStates
{
    public class Build : BotState
    {
        private Transform _newYardTransform;

        public override void Enter()
        {
            _newYardTransform = _bot.NewYardTransform;
        }

        public override void Process()
        {
            if (_newYardTransform == null || _bot.NewYard == null)
            {
                _bot.ClearGoals();
                ChangeState("Return");
            }

            _bot.LookAt(_newYardTransform);
            _bot.MoveTowards(_newYardTransform);

            TryBuild();
        }

        private void TryBuild()
        {
            float distanceSquared = Vector3.SqrMagnitude(_bot.transform.position - _newYardTransform.position);

            if (distanceSquared > _bot.InteractDistance * _bot.InteractDistance)
                return;

            ConstructionYard newYard = Instantiate(_bot.NewYard, _newYardTransform.position, Quaternion.identity);

            _bot.Home.FinishBuilding();
            _bot.SetHome(newYard);

            _bot.ClearGoals();
            ChangeState("Idle");
        }
    }
}
