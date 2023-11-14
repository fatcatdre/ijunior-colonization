public class ResourceGatherer : ConstructionYardModule
{
    public void Gather()
    {
        Bot bot = _constructionYard.RequestBot();

        if (bot == null)
            return;

        Resource resource = _constructionYard.FindResource();

        if (resource == null)
        {
            _constructionYard.AddBot(bot);
            return;
        }

        bot.Gather(resource);
    }
}
