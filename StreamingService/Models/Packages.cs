namespace StreamingService.Models
{
    // Should this be it's own database entry. 
    // With additional properties like free song allowence
    // Rather than a bunch of magic numbers in the user service
    public enum Packages
    {
        Freemium = 1,
        Premium = 2,
        Unlimitted = 3,
    }
}
