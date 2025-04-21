using CarConnect.Dao;
using CarConnect.Entity;
using CarConnect.exception;
using System;
using System.Collections.Generic;

public class MainModule
{
    static ICustomerDao CustomerDao;
    static IVehicleDao<Vehicle> VehicleDao;
    static IReservationDao<Reservation> ReservationDao;
    static IAdminDao<Admin> AdminDao;

    public static void Main(string[] args)
    {
        try
        {
            CustomerDao = new CustomerDao();
            VehicleDao = new VehicleDao();
            ReservationDao = new ReservationDao();
            AdminDao = new AdminDao();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Initialization failed: " + ex.Message);
            return;
        }

        while (true)
        {
            Console.WriteLine("\n--- CarConnect Rental System ---");
            Console.WriteLine("1. Register Customer");
            Console.WriteLine("2. Update Customer");
            Console.WriteLine("3. Delete Customer");
            Console.WriteLine("4. View Available Vehicles");
            Console.WriteLine("5. Add Vehicle");
            Console.WriteLine("6. Update Vehicle");
            Console.WriteLine("7. Delete Vehicle");
            Console.WriteLine("8. Make Reservation");
            Console.WriteLine("9. View Reservation by ID");
            Console.WriteLine("10. View Reservations by Customer");
            Console.WriteLine("11. Update Reservation");
            Console.WriteLine("12. Cancel Reservation");
            Console.WriteLine("13. Admin Login");
            Console.WriteLine("14. Register Admin");
            Console.WriteLine("15. Update Admin");
            Console.WriteLine("16. Delete Admin");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
           
            // Validate the input
            switch (choice)
            {
                case "1": RegisterCustomer(); break;
                case "2": UpdateCustomer(); break;
                case "3": DeleteCustomer(); break;
                case "4": ShowAvailableVehicles(); break;
                case "5": AddVehicle(); break;
                case "6": UpdateVehicle(); break;
                case "7": DeleteVehicle(); break;
                case "8": MakeReservation(); break;
                case "9": ViewReservationById(); break;
                case "10": ViewReservationsByCustomer(); break;
                case "11": UpdateReservation(); break;
                case "12": CancelReservation(); break;
                case "13": AdminLogin(); break;
                case "14": RegisterAdmin(); break;
                case "15": UpdateAdmin(); break;
                case "16": DeleteAdmin(); break;
                case "0":
                    Console.WriteLine("Thank you for using CarConnect!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
    /// Method to register a new customer
    static void RegisterCustomer()
    {
        try
        {
            Console.Write("First Name: ");
            string fname = Console.ReadLine();
            Console.Write("Last Name: ");
            string lname = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Customer customer = new Customer
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                PhoneNumber = phone,
                Address = address,
                Username = username,
                Password = password,
                RegistrationDate = DateTime.Now
            };

            CustomerDao.RegisterCustomer(customer);
            Console.WriteLine("Customer registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while registering the customer: " + ex.Message);
        }
    }
    /// Method to update customer information
    static void UpdateCustomer()
    {
        try
        {
            Console.Write("Customer Id to update: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))  // Validate the ID input
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric customer ID.");
                return;
            }

            Customer customer = CustomerDao.GetCustomerById(id);   // Fetch the customer

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }
            Console.Write("New Email: ");     // Update customer information
            customer.Email = Console.ReadLine();
            Console.Write("New Phone: ");
            customer.PhoneNumber = Console.ReadLine();
            CustomerDao.UpdateCustomer(customer);  // Update the customer in the database
            Console.WriteLine("Customer updated successfully.");
        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine("Error: " + ex.Message); // Custom error message for InvalidInputException
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid format. Please ensure the ID is a number.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    /// Method to delete a customer
    static void DeleteCustomer()
    {
        try
        {
            Console.Write("Enter Customer Id to delete: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))// Validate the input for valid integer
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric customer ID.");
                return;
            }
            bool success = CustomerDao.DeleteCustomer(id);// Attempt to delete the customer

            if (success)
            {
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found. Deletion failed.");
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Invalid format. Please ensure the ID is a number.");
        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine("Error: " + ex.Message);  // Handle specific custom exception
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    /// Method to show available vehicles
    static void ShowAvailableVehicles()
    {
        try
        {
            var vehicles = VehicleDao.GetAvailableVehicles(); // Get the list of available vehicles

            if (vehicles == null || vehicles.Count == 0)
            {
                Console.WriteLine("No vehicles are currently available.");
                return;
            }

            Console.WriteLine("Available Vehicles:");
            foreach (var v in vehicles)
            {
                
                Console.WriteLine($"{v.VehicleId}: {v.Make} {v.Model}, Rs {v.DailyRate:F2}/day");
            }
        }
        catch (ArgumentNullException ex)
        {
       
            Console.WriteLine("Error: The vehicle data could not be loaded. Please try again later.");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine("An error occurred while retrieving the available vehicles: " + ex.Message);
        }
    }


    // Helper method to safely parse integers
    static int GetIntFromUser(string prompt)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out result))
                return result;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    // Helper method to safely parse dates
    static DateTime GetDateFromUser(string prompt)
    {
        DateTime result;
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out result))
                return result;
            Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
        }
    }

    // Helper method for reading a string input with validation
    static string GetStringFromUser(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
    //  method to add a vehicle
    static void AddVehicle()
    {
        try
        {
            Vehicle vehicle = new Vehicle
            {
                Make = GetStringFromUser("Make: "),
                Model = GetStringFromUser("Model: "),
                Year = GetStringFromUser("Year: "),
                Color = GetStringFromUser("Color: "),
                RegistrationNumber = GetStringFromUser("Registration Number: "),
                DailyRate = decimal.Parse(GetStringFromUser("Rate: ")),
                Availability = true
            };

            VehicleDao.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to update vehicle information
    static void UpdateVehicle()
    {
        try
        {
            int id = GetIntFromUser("Vehicle Id: ");// Get vehicle ID from user
            Vehicle vehicle = VehicleDao.GetVehicleById(id);

            if (vehicle == null) throw new InvalidInputException("Vehicle not found.");

            vehicle.Color = GetStringFromUser("New Color: ");
            vehicle.DailyRate = decimal.Parse(GetStringFromUser("New Rate: "));

            VehicleDao.UpdateVehicle(vehicle);// Update the vehicle in the database
            Console.WriteLine("Vehicle updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to delete a vehicle
    static void DeleteVehicle()
    {
        try
        {
            int id = GetIntFromUser("Vehicle Id: ");
            VehicleDao.RemoveVehicle(id);
            Console.WriteLine("Vehicle removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to make a reservation
    static void MakeReservation()
    {
        try
        {
            string uname = GetStringFromUser("Customer Username: "); // Get customer username
            Customer customer = CustomerDao.GetCustomerByUsername(uname);

            if (customer == null) throw new InvalidInputException("Customer not found.");

            int vid = GetIntFromUser("Vehicle ID: ");// Get vehicle ID
            var vehicle = VehicleDao.GetVehicleById(vid);

            if (vehicle == null) throw new InvalidInputException("Vehicle not found.");

            DateTime start = GetDateFromUser("Start Date (yyyy-mm-dd): ");
            DateTime end = GetDateFromUser("End Date (yyyy-mm-dd): ");

            if (start >= end)
                throw new ReservationException("Start date must be before end date.");

            Reservation reservation = new Reservation
            {
                CustomerId = customer.CustomerId,
                VehicleId = vid,
                StartDate = start,
                EndDate = end,
                TotalCost = (end - start).Days * vehicle.DailyRate,
                Status = "Confirmed"
            };

            ReservationDao.CreateReservation(reservation);// Create the reservation
            Console.WriteLine("Reservation successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to view reservation by ID
    static void ViewReservationById()
    {
        try
        {
            int id = GetIntFromUser("Reservation Id: ");
            var reservation = ReservationDao.GetReservationById(id);// Get reservation by ID

            if (reservation == null)
                throw new InvalidInputException("Reservation not found.");

            Console.WriteLine($"{reservation.ReservationId}: Vehicle {reservation.VehicleId}, {reservation.StartDate:dd/MM} - {reservation.EndDate:dd/MM}, Rs{reservation.TotalCost}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to view reservations by customer
    static void ViewReservationsByCustomer()
    {
        try
        {
            int id = GetIntFromUser("Customer Id: ");
            var reservations = ReservationDao.GetReservationsByCustomerId(id);

            if (reservations == null || reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for this customer.");
                return;
            }
            // Display the reservations
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"{reservation.ReservationId}: Vehicle {reservation.VehicleId}, Rs{reservation.TotalCost}, Status: {reservation.Status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to update reservation status
    static void UpdateReservation()
    {
        try
        {// Get reservation ID
            int id = GetIntFromUser("Reservation Id: ");
            var reservation = ReservationDao.GetReservationById(id);

            if (reservation == null) throw new InvalidInputException("Reservation not found.");

            string status = GetStringFromUser("New Status (Confirmed/Canceled): ");
            reservation.Status = status;
            // Update the reservation status
            ReservationDao.UpdateReservation(reservation);
            Console.WriteLine("Reservation updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to cancel a reservation
    static void CancelReservation()
    {
        try
        {
            int id = GetIntFromUser("Reservation Id: ");
            ReservationDao.CancelReservation(id);
            Console.WriteLine("Reservation canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to handle admin login
    static void AdminLogin()
    {
        try
        {
            string username = GetStringFromUser("Admin Username: ");
            string password = GetStringFromUser("Password: ");

            IAdminDao<Admin> adminService = new AdminDao();
            Admin admin = adminService.AdminLogin(username, password);

            Console.WriteLine("Login successful.");// Display admin details
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to register a new admin
    static void RegisterAdmin()
    {
        try
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("PhoneNumber: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Admin admin = new Admin
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                Role = "Admin",
                JoinDate = DateTime.Now
            };

            AdminDao.RegisterAdmin(admin);
            Console.WriteLine("Admin registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to update admin information
    static void UpdateAdmin()
    {
        try
        {
            string username = GetStringFromUser("Admin Username: ");
            Admin admin = AdminDao.GetAdminByUsername(username);

            if (admin == null) throw new InvalidInputException("Admin not found.");

            admin.Password = GetStringFromUser("New Password: ");
            AdminDao.UpdateAdmin(admin);
            Console.WriteLine("Admin updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // Method to delete an admin
    static void DeleteAdmin()
    {
        try// Validate the input for valid integer
        {
            int adminId = GetIntFromUser("Admin Id: ");
            bool success = AdminDao.DeleteAdmin(adminId);

            if (success)
                Console.WriteLine("Admin deleted.");
            else
                Console.WriteLine("Admin deletion failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}