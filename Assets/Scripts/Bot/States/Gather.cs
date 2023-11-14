using UnityEngine;

namespace BotStates
{
    public class Gather : BotState
    {
        private Transform _resource;

        public override void Enter()
        {
            if (_bot.TargetResource == null)
                ChangeState("Idle");

            _resource = _bot.TargetResource.transform;
        }

        public override void Process()
        {
            if (_resource == null)
                ChangeState("Return");

            _bot.LookAt(_resource);
            _bot.MoveTowards(_resource);

            TryPickup(_resource);
        }

        private void TryPickup(Transform target)
        {
            float distanceSquared = Vector3.SqrMagnitude(_bot.transform.position - target.position);

            if (distanceSquared > _bot.InteractDistance * _bot.InteractDistance)
                return;

            target.parent = _bot.ResourceHolder;
            target.localPosition = Vector3.zero;

            ChangeState("Return");
        }
    }
}
