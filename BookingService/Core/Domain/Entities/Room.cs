using Domain.ValueObjects;

namespace Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenence { get; set; }
    public Price Price { get; set; }
    public bool IsAvailabel
    {
        get
        {
            if (InMaintenence || HasGest)
            {
                return false;
            }
            return true;
        }
    }

    public bool HasGest
    {
        //Verificar se existem bookins abertos para este room
        get { return true; }
    }
}
