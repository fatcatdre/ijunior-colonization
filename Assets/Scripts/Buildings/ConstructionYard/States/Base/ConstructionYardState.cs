using UnityEngine;
using FSM;

namespace ConstructionYardStates
{
    public abstract class ConstructionYardState : State
    {
        protected ConstructionYard _constructionYard;

        protected override void Awake()
        {
            base.Awake();

            _constructionYard = GetComponentInParent<ConstructionYard>(true);

            if (_constructionYard == null)
                Debug.LogError($"State {name} couldn't find a ConstructionYard script on any parent objects.");
        }
    }
}
