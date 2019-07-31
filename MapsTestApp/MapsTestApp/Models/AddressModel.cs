using System.Collections.Generic;

namespace MapsTestApp.Models
{
    public class AddressModel
    {
        public AddressModel(string caption)
        {
            Caption = caption;
        }

        public string Caption { get; }

        public override string ToString() => Caption;

        public override bool Equals(object obj)
        {
            if (!(obj is AddressModel rhs))
                return false;
            return rhs.Caption == Caption;
        }

        public override int GetHashCode() => Caption == null ? 0 : Caption.GetHashCode();

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
