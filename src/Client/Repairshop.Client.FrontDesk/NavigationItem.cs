namespace Repairshop.Client.FrontDesk;

public class NavigationItem
{
    public NavigationItem(string name, Action navigationAction)
    {
        Name = name;
        NavigationAction = navigationAction;
    }

    public string Name { get; private set; }
    public Action NavigationAction { get; private set; }
}
