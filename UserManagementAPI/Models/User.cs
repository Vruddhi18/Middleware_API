using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UserManagementAPI.Models;

public class User
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; } // Email should be unique in the database, and also should be encrypted in a real application, to avoid exposing it to hackers.

    [Required]
    [StringLength(100, MinimumLength = 8)] // Additional security like special characters can be enforced in the password policy on the frontend side.
    public required string Password { get; set; }   // Password should be hashed and salted in a real application, should not be stored in plain text.

    // Optional properties
    public string? PhoneNumber { get; set; } // Optional, but can be useful for account recovery or two-factor authentication. Should be encrypted since hackers can use it to reset passwords.
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? StateOrProvince { get; set; }
    public string? Country { get; set; }
    public DateTime? DateOfBirth { get; set; }  // Optional, but can be useful for age verification or personalized content, should also be encrypted.
    public string? ProfilePictureUrl { get; set; }
    public string? Role { get; set; } // e.g., "Admin", "User", etc.

    // Additional properties can be added as needed
    // For example, you might want to include additional data fields like: AccountCreationDate, LastLoginDate, etc.
    // AccountCreationDate is usually set by the database when the record is created.
    // LastLoginDate can be updated whenever the user logs in.
    // We'll skip these for now to keep the model simple.
    
    // List of roles, populated with default values
    private static List<string> Roles { get; set; } = new List<string>
    {
        "Admin", "Moderator", "Executive", "Boss", "Employee"
    }; // Default roles, can be extended as needed.
    
    // Empty Constructor
    public User()
    {
        // This constructor is intentionally left empty.
        // It can be used for deserialization or when you want to create an instance without setting properties immediately.
    }
    
    // Main Constructor
    [SetsRequiredMembers]
    public User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
    
    // Full Constructor
    [SetsRequiredMembers]
    public User(string firstName, string lastName, string email, string password, string? phoneNumber = null, string? 
        address = null, string? city = null, string? stateOrProvince = null, string? country = null, 
        DateTime? dateOfBirth = null, string? profilePictureUrl = null, string? role = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Address = address;
        City = city;
        StateOrProvince = stateOrProvince;
        Country = country;
        DateOfBirth = dateOfBirth;
        ProfilePictureUrl = profilePictureUrl;
        Role = role; 
    }
    
    // Static Dictionary of users, populated with some values.
    public static Dictionary<int, User> Users = new Dictionary<int, User>
    {
        { 1, new User("John", "Doe", "john.doe@example.com", "Password123", "123-456-7890", "123 Main St", "Anytown", "Anystate", "USA", new DateTime(1990, 1, 1), "http://example.com/johndoe.jpg", Roles[0]) },
        { 2, new User("Jane", "Smith", "jane.smith@example.com", "Password123", "987-654-3210", "456 Elm St", "Othertown", "Otherstate", "USA", new DateTime(1985, 5, 15), "http://example.com/janesmith.jpg", Roles[4]) },
        { 3, new User("Alice", "Johnson", "alice.johnson@example.com", "Password123", "555-555-5555", "789 Oak St", "Sometown", "Somestate", "USA", new DateTime(1992, 3, 10), "http://example.com/alicejohnson.jpg", Roles[1]) },
        { 4, new User("Bob", "Brown", "bob.brown@example.com", "Password123", "111-222-3333", "101 Pine St", "Newtown", "Newstate", "USA", new DateTime(1988, 7, 20), "http://example.com/bobbrown.jpg", Roles[2]) },
        { 5, new User("Charlie", "Davis", "charlie.davis@example.com", "Password123", "222-333-4444", "202 Maple St", "Oldtown", "Oldstate", "USA", new DateTime(1995, 11, 30), "http://example.com/charliedavis.jpg", Roles[3]) },
        { 6, new User("Diana", "Evans", "diana.evans@example.com", "Password123", "333-444-5555", "303 Birch St", "Smalltown", "Smallstate", "USA", new DateTime(1991, 4, 25), "http://example.com/dianaevans.jpg", Roles[4]) },
        { 7, new User("Eve", "Foster", "eve.foster@example.com", "Password123", "444-555-6666", "404 Cedar St", "Bigtown", "Bigstate", "USA", new DateTime(1983, 9, 5), "http://example.com/evefoster.jpg", Roles[0]) }
    };

}