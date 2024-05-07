using SQLite;

namespace CRUDTS5DANAMISE.Models
{
    [Table("people")]
    public class Person
    {
       

     [PrimaryKey, AutoIncrement]
     public int Id { get; set; }

     [MaxLength(250), Unique]
     public string Name { get; set; }

    }
}
