public class Country
{
    public int Id {get;set;}
    public string? CountryName {get;set;}
    public Country(int Id , string CountryName)
    {
        this.Id = Id;
        this.CountryName = CountryName;
    }
}