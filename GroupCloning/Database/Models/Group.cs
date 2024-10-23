using System.ComponentModel.DataAnnotations;

namespace GroupCloning.Database.Models;

public class Group
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int GroupNumber { get; set; }
    [Required]
    public int IdentifierInGroup { get; set; }
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
    public string Prop3 { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Group other)
        {
            return IdentifierInGroup == other.IdentifierInGroup
                   && Prop1 == other.Prop1
                   && Prop2 == other.Prop2
                   && Prop3 == other.Prop3;
        }
        return false;
    }

    public override string ToString()
    {
        return "Group: "+ GroupNumber+ "; " + IdentifierInGroup + "; " + Prop1 + "; " + Prop2 + "; " + Prop3 + ";";
    }
}