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
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void UpdateCustomer()
    {
        try
        {
            Console.Write("Customer Id to update: ");
            int id = int.Parse(Console.ReadLine());
            Customer customer = CustomerDao.GetCustomerById(id);

            if (customer == null) throw new InvalidInputException("Customer not found.");

            Console.Write("New Email: ");
            customer.Email = Console.ReadLine();
            Console.Write("New Phone: ");
            customer.PhoneNumber = Console.ReadLine();

            CustomerDao.UpdateCustomer(customer);
            Console.WriteLine("Customer updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void DeleteCustomer()
    {
        try
        {
            Console.Write("Customer Id: ");
            int id = int.Parse(Console.ReadLine());
            CustomerDao.DeleteCustomer(id);
            Console.WriteLine("Customer deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void ShowAvailableVehicles()
    {
        try
        {
            var vehicles = VehicleDao.GetAvailableVehicles();
            foreach (var v in vehicles)
            {
                Console.WriteLine($"{v.VehicleId}: {v.Make} {v.Model}, Rs{v.DailyRate}/day");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void AddVehicle()
    {
        try
        {
            Vehicle vehicle = new Vehicle();
            Console.Write("Make: ");
            vehicle.Make = Console.ReadLine();
            Console.Write("Model: ");
            vehicle.Model = Console.ReadLine();
            Console.Write("Year: ");
            vehicle.Year = Console.ReadLine();
            Console.Write("Color: ");
            vehicle.Color = Console.ReadLine();
            Console.Write("Registration Number: ");
            vehicle.RegistrationNumber = Console.ReadLine();
            Console.Write("Rate: ");
            vehicle.DailyRate = decimal.Parse(Console.ReadLine());
            vehicle.Availability = true;

            VehicleDao.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void UpdateVehicle()
    {
        try
        {
            Console.Write("Vehicle Id: ");
            int id = int.Parse(Console.ReadLine());
            Vehicle vehicle = VehicleDao.GetVehicleById(id);
            if (vehicle == null) throw new InvalidInputException("Vehicle not found.");

            Console.Write("New Color: ");
            vehicle.Color = Console.ReadLine();
            Console.Write("New Rate: ");
            vehicle.DailyRate = decimal.Parse(Console.ReadLine());
            VehicleDao.UpdateVehicle(vehicle);
            Console.WriteLine("Vehicle updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void DeleteVehicle()
    {
        try
        {
            Console.Write("Vehicle Id: ");
            int id = int.Parse(Console.ReadLine());
            VehicleDao.RemoveVehicle(id);
            Console.WriteLine("Vehicle removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MakeReservation()
    {
        try
        {
            Console.Write("Customer Username: ");
            string uname = Console.ReadLine();
            Customer c = CustomerDao.GetCustomerByUsername(uname);
            if (c == null) throw new InvalidInputException("Customer not found.");

            Console.Write("Vehicle ID: ");
            int vid = int.Parse(Console.ReadLine());
            var vehicle = VehicleDao.GetVehicleById(vid);
            if (vehicle == null) throw new InvalidInputException("Vehicle not found.");

            Console.Write("Start Date (yyyy-mm-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.Write("End Date (yyyy-mm-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            if (start >= end)
                throw new ReservationException("Start date must be before end date.");

            Reservation reservation = new Reservation
            {
                CustomerId = c.CustomerId,
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
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void ViewReservationById()
    {
        try
        {
            Console.Write("Reservation Id: ");
            int id = int.Parse(Console.ReadLine());
            var r = ReservationDao.GetReservationById(id);
            Console.WriteLine($"{r.ReservationId}: Vehicle {r.VehicleId}, {r.StartDate:dd/MM} - {r.EndDate:dd/MM}, Rs{r.TotalCost}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void ViewReservationsByCustomer()
    {
        try
        {
            Console.Write("Customer Id: ");
            int id = int.Parse(Console.ReadLine());
            var list = ReservationDao.GetReservationsByCustomerId(id);
            foreach (var r in list)
            {
                Console.WriteLine($"{r.ReservationId}: Vehicle {r.VehicleId}, Rs{r.TotalCost}, Status: {r.Status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void UpdateReservation()
    {
        try
        {
            Console.Write("Reservation Id: ");
            int id = int.Parse(Console.ReadLine());
            var r = ReservationDao.GetReservationById(id);
            Console.Write("New Status (Confirmed/Canceled): ");
            r.Status = Console.ReadLine();
            ReservationDao.UpdateReservation(r);
            Console.WriteLine("Reservation updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void CancelReservation()
    {
        try
        {
            Console.Write("Reservation Id: ");
            int id = int.Parse(Console.ReadLine());
            ReservationDao.CancelReservation(id);
            Console.WriteLine("Reservation canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void AdminLogin()
    {
        try
        {
            Console.Write("Admin Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            IAdminDao<Admin> adminService = new AdminDao(); 
            Admin admin = adminService.AdminLogin(username, password);
            Console.WriteLine("Login successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void RegisterAdmin()
    {
        try
        {
            Admin admin = new Admin();
            Console.Write("Username: ");
            admin.Username = Console.ReadLine();
            Console.Write("Password: ");
            admin.Password = Console.ReadLine();
            AdminDao.RegisterAdmin(admin);
            Console.WriteLine("Admin registered successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void UpdateAdmin()
    {
        try
        {
            Console.Write("Admin Username: ");
            string username = Console.ReadLine();
            Admin admin = AdminDao.GetAdminByUsername(username);
            if (admin == null) throw new InvalidInputException("Admin not found.");

            Console.Write("New Password: ");
            admin.Password = Console.ReadLine();
            AdminDao.UpdateAdmin(admin);
            Console.WriteLine("Admin updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void DeleteAdmin()
    {
        try
        {
            Console.Write("Admin Id: "); 
            int adminId = int.Parse(Console.ReadLine()); 
            bool v = AdminDao.DeleteAdmin(adminId); 
            Console.WriteLine("Admin deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
