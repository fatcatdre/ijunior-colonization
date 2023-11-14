namespace ConstructionYardStates
{
    public class BuildBot : ConstructionYardState
    {
        public override void Process()
        {
            if (_constructionYard.Garage.IsBotAvailable && _constructionYard.Scanner.ScannedResourceCount > 0)
                _constructionYard.Gatherer.Gather();

            if (_constructionYard.AvailableResources >= _constructionYard.Garage.BotCost)
                _constructionYard.Garage.BuildBot();
        }
    }
}
