namespace IMS_Einleitung.Model
{
    internal class Address  
    {
        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"AddressId: {Id} {Street} {HouseNumber} {Zip} {City}";
        }
    }
}