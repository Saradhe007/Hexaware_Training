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
    static long GetLongFromUser(string prompt)
    {
        long result;
        while (true)
        {
            Console.Write(prompt);
            if (long.TryParse(Console.ReadLine(), out result))
                return result;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }


    static void UpdateCustomer()
    {
        try
        {
            long id = GetLongFromUser("Customer Id to update: ");
            Customer customer = CustomerDao.GetCustomerById(id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("New Email: ");
            customer.Email = Console.ReadLine();
            Console.Write("New Phone: ");
            customer.PhoneNumber = Console.ReadLine();
            CustomerDao.UpdateCustomer(customer);
            Console.WriteLine("Customer updated successfully.");
        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format. Please ensure the ID is a number.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    static void DeleteCustomer()
    {
        try
        {
            long id = GetLongFromUser("Enter Customer Id to delete: ");
            bool success = CustomerDao.DeleteCustomer((int)id);

            if (success)
            {
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found. Deletion failed.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format. Please ensure the ID is a number.");
        }
        catch (InvalidInputException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }

    static void ShowAvailableVehicles()
    {
        try
        {
            var vehicles = VehicleDao.GetAvailableVehicles();
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

    static void UpdateVehicle()
    {
        try
        {
            long id = GetLongFromUser("Vehicle Id: "); // Use GetLongFromUser instead of GetIntFromUser
            Vehicle vehicle = VehicleDao.GetVehicleById(id);

            if (vehicle == null)
            {
                Console.WriteLine("Vehicle not found.");
                return;
            }

            vehicle.Color = GetStringFromUser("New Color: ");
            vehicle.DailyRate = decimal.Parse(GetStringFromUser("New Rate: "));
            VehicleDao.UpdateVehicle(vehicle);
            Console.WriteLine("Vehicle updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void DeleteVehicle()
    {
        try
        {
            long id = GetLongFromUser("Vehicle Id: "); // Use GetLongFromUser instead of GetIntFromUser
            VehicleDao.RemoveVehicle(id);
            Console.WriteLine("Vehicle removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void MakeReservation()
    {
        try
        {
            string uname = GetStringFromUser("Customer Username: ");
            Customer customer = CustomerDao.GetCustomerByUsername(uname);

            if (customer == null) throw new InvalidInputException("Customer not found.");

            int vid = GetIntFromUser("Vehicle ID: ");
            var vehicle = VehicleDao.GetVehicleById(vid);

            if (vehicle == null) throw new InvalidInputException("Vehicle not found.");

            DateTime start = GetDateFromUser("Start Date (yyyy-mm-dd): ");
            DateTime end = GetDateFromUser("End Date (yyyy-mm-dd): ");

            if (start >= end)
                throw new ReservationException("Start date must be before end date.");

            Reservation reservation = new Reservation
            {
                CustomerId = (int)customer.CustomerId,
                VehicleId = vid,
                StartDate = start,
                EndDate = end,
                TotalCost = (end - start).Days * vehicle.DailyRate,
                Status = "Confirmed"
            };

            ReservationDao.CreateReservation(reservation);
            Console.WriteLine("Reservation successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ViewReservationById()
    {
        try
        {
            long reservationId = GetLongFromUser("Reservation Id: "); // Use GetLongFromUser instead of GetIntFromUser
            Reservation reservation = ReservationDao.GetReservationById(reservationId);

            if (reservation == null)
            {
                Console.WriteLine("Reservation not found.");
            }
            else
            {
                Console.WriteLine($"Reservation Details: {reservation.ReservationId}, {reservation.StartDate}, {reservation.EndDate}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    static void ViewReservationsByCustomer()
    {
        try
        {
            long id = GetLongFromUser("Customer Id: ");
            var reservations = ReservationDao.GetReservationsByCustomerId(id);

            if (reservations == null || reservations.Count == 0)
            {
                Console.WriteLine("No reservations found for this customer.");
                return;
            }

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

    static void UpdateReservation()
    {
        try
        {
            long id = GetLongFromUser("Reservation Id: ");
            var reservation = ReservationDao.GetReservationById(id);

            if (reservation == null) throw new InvalidInputException("Reservation not found.");

            string status = GetStringFromUser("New Status (Confirmed/Canceled): ");
            reservation.Status = status;

            ReservationDao.UpdateReservation(reservation);
            Console.WriteLine("Reservation updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CancelReservation()
    {
        try
        {
            long id = GetLongFromUser("Reservation Id: ");
            bool success = ReservationDao.CancelReservation(id);
            if (success)
                Console.WriteLine("Reservation canceled.");
            else
                Console.WriteLine("Reservation not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    static void AdminLogin()
    {
        try
        {
            string username = GetStringFromUser("Admin Username: ");
            string password = GetStringFromUser("Password: ");

            Admin admin = AdminDao.AdminLogin(username, password);

            Console.WriteLine("Login successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void RegisterAdmin()
    {
        try
        {
            Admin admin = new Admin
            {
                Username = GetStringFromUser("Username: "),
                Password = GetStringFromUser("Password: ")
            };

            AdminDao.RegisterAdmin(admin);
            Console.WriteLine("Admin registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

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

    static void DeleteAdmin()
    {
        try
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