using System;

namespace P1
{
    /**
     * This class should have the following properties
     *      Name (String)
     *      Description (String)
     *      Colour (String)
     *      Price (Decimal)
     */
    class Product
    {
        private int fId;
        private String fName;
        private String fDescription;
        private String fColour;
        private Decimal fPrice;

        public Product()
        {
            fId = 0;
            fName = "";
            fDescription = "";
            fColour = "";
            fPrice = 0.00m;
        }
        public Product(String name, String description, String colour, Decimal price)
        {
            // To be modified
            fId = 0;
            fName = name;
            fDescription = description;
            fColour = colour;
            fPrice = price;
        }

        public int Id
        {
            get { return fId; }
            set { fId = value; }
        }

        public String Name
        {
            get { return fName; }
            set { fName = value; }
        }
        public String Description
        {
            get { return fDescription; }
            set { fDescription = value; }
        }
        public String Colour
        {
            get { return fColour; }
            set { fColour = value; }
        }
        public Decimal Price
        {
            get { return fPrice; }
            set { fPrice = value; }
        }

        public override string ToString()
        {
            // To be modified
            // You can choose to print in json string or any other human readable format
            return $"Id: {fId}\nName: {fName}\nPrice: {fPrice}\nColour: {fColour}\nDescription: {fDescription}";
        }
    }
}
