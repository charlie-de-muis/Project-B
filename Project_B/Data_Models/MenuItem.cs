public class MenuItem
{
    // vegetarian, vegan, glutenfree, dairy free?
    public int ID;
    public string Name;
    public List<string> Ingredients;
    public double Price = 0.00;
    public List<string> DietaryInfo = new();

    public MenuItem(int id, string name, List<string> ingredients, double price, List<string> dietaryinfo)
    {
        this.ID = id;
        this.Name = name;
        this.Ingredients = ingredients;
        this.Price = price;
        this.DietaryInfo = dietaryinfo;
    }
}