using System;
using System.Collections.Generic;
using System.Text;

public class Address
{
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public bool IsInUsa()
    {
        return _country.ToUpper() == "USA" || _country.ToUpper() == "UNITED STATES";
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
    }
}

public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public bool IsInUsa()
    {
        return _address.IsInUsa();
    }

    public string GetName()
    {
        return _name;
    }

    public Address GetAddress()
    {
        return _address;
    }
}

public class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public double GetTotalCost()
    {
        return _price * _quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }
}

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double totalCost = 0;
        foreach (Product product in _products)
        {
            totalCost += product.GetTotalCost();
        }

        double shippingCost = _customer.IsInUsa() ? 5.00 : 35.00;
        
        return totalCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder packingLabel = new StringBuilder();
        packingLabel.AppendLine("Packing Label:");
        foreach (Product product in _products)
        {
            packingLabel.AppendLine($"- {product.GetName()} (ID: {product.GetProductId()})");
        }
        return packingLabel.ToString();
    }

    public string GetShippingLabel()
    {
        StringBuilder shippingLabel = new StringBuilder();
        shippingLabel.AppendLine("Shipping Label:");
        shippingLabel.AppendLine(_customer.GetName());
        shippingLabel.AppendLine(_customer.GetAddress().GetFullAddress());
        return shippingLabel.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // ----------------------------------------------------
        // Order 1: Customer in the USA
        // ----------------------------------------------------
        Address address1 = new Address("123 Elm Street", "Springfield", "IL", "USA");
        Customer customer1 = new Customer("Alice Johnson", address1);
        Order order1 = new Order(customer1);
        
        order1.AddProduct(new Product("Laptop", "TECH-001", 1200.50, 1));
        order1.AddProduct(new Product("Wireless Mouse", "TECH-002", 25.00, 2));

        // ----------------------------------------------------
        // Order 2: International Customer
        // ----------------------------------------------------
        Address address2 = new Address("456 Maple Drive", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Bob Smith", address2);
        Order order2 = new Order(customer2);
        
        order2.AddProduct(new Product("Desk Lamp", "HOME-101", 45.99, 1));
        order2.AddProduct(new Product("Notebook", "OFFC-201", 5.50, 4));
        order2.AddProduct(new Product("Pens", "OFFC-202", 2.99, 10));

        // ----------------------------------------------------
        // Display Results
        // ----------------------------------------------------
        Console.WriteLine("========================================");
        Console.WriteLine("ORDER 1 DETAILS");
        Console.WriteLine("========================================");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Order Cost: ${order1.CalculateTotalCost():0.00}\n");

        Console.WriteLine("========================================");
        Console.WriteLine("ORDER 2 DETAILS");
        Console.WriteLine("========================================");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Order Cost: ${order2.CalculateTotalCost():0.00}\n");
    }
}