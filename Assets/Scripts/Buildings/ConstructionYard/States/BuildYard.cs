namespace ConstructionYardStates
{
    public class BuildYard : ConstructionYardState
    {
        private bool _hasConstructionStarted;

        public override void Process()
        {
            if (_constructionYard.Garage.IsBotAvailable && _constructionYard.AvailableResources >= _constructionYard.YardBuilder.BuildCost)
            {
                if (_hasConstructionStarted == false)
                {
                    _hasConstructionStarted = true;
                    _constructionYard.YardBuilder.Build();
                }
            }

            if (_constructionYard.Garage.IsBotAvailable && _constructionYard.Scanner.ScannedResourceCount > 0)
                _constructionYard.Gatherer.Gather();
        }

        public override void Exit()
        {
            _hasConstructionStarted = false;
        }
    }
}