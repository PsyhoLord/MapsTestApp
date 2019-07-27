using System.Collections.Generic;

namespace MapsTestApp.Models
{
    public class AddressModel
    {
        public AddressModel(string caption)
        {
            Caption = caption;
        }

        public string Caption { get; private set; }

        public override string ToString()
        {
            return Caption;
        }

        public override bool Equals(object obj)
        {
            var rhs = obj as AddressModel;
            if (rhs == null)
                return false;
            return rhs.Caption == Caption;
        }

        public override int GetHashCode()
        {
            if (Caption == null)
                return 0;
            return Caption.GetHashCode();
        }

        public Candidate MapsCandidate { get; set; }
    }
}
