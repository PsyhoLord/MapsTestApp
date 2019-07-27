using SQLite;

namespace MapsTestApp.Models
{
    [Table("Address")]
    public class AddressDbModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public double lat { get; set; }
        public double lng { get; set; }

        public override string ToString()
        {
            return $"[Person: Id={Id}, Name={Name}, lat={lat}, lng={lng}]";
        }
    }
}
