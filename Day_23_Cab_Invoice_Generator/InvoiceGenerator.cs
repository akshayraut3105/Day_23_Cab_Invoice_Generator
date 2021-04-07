using System;
using System.Collections.Generic;
using System.Text;
namespace Day_23_Cab_Invoice_Generator
{
    // Class to compute the total fare when the user passes the distance and time of the fare and generate invoice
    public class InvoiceGenerator
    {
        // Declaring the object of the class RideType which has different  time and distance 
        public RideType rideType;
        private readonly RideRepository rideRepository;
        // Read-Only attributes acting as constant variable
        // to be initialised at run time using a parameterised constructor
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_KM;
        private readonly double MINIMUM_FARE;
        // Default constructor of the Invoice Generator Class
        public InvoiceGenerator()
        {

        }
        // Parameterised constructor of the Invoice Generator Class
        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.MINIMUM_COST_PER_KM = 10;
            this.COST_PER_KM = 1;
            this.MINIMUM_FARE = 5;
            this.rideRepository = new RideRepository();
        }
        // Method to Compute the total fare of the cab journey when passed eith distance and time
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            // Exception handling for the invalid  distance and time
            try
            {
                // formulae to calculate total fare for the ride
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_KM;
            }
            catch (CabInvoiceException)
            {
                if (distance <= 0)
                {
                    // invalid distance exception
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time <= 0)
                {
                    // invalid time exception
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            // Math function to get the maximum
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            // Exception handling for the invalid  distance and time
            try
            {
                // Using foreach loop to take one ride each time
                foreach (Ride ride in rides)
                {
                    // returning total fare
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides passed are null..");
                }
            }
            // returning invoice summary which has total fare and number of rides
            return new InvoiceSummary(totalFare, rides.Length);
        }
        // UC3- Method to calculate Average Fare
        public InvoiceSummary CalculateAvgFare(Ride[] rides)
        {
            double totalFare = 0;
            // Defining variable to compute average fare
            double averageFare = 0;
            try
            {
                // Using foreach loop to take one ride each time
                foreach (Ride ride in rides)
                {
                    // returning total fare
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
                // Computing average fare = (total fare/ number of rides)
                averageFare = (totalFare / rides.Length);
            }
            // Catching Exception for the invalid  distance and time
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides passed are null..");
                }
            }
            // Returning the invoice summary with average fare 
            return new InvoiceSummary(totalFare, rides.Length, averageFare);
        }
        // Method to add the Customer info to the ride repository as a dictionary with key as UserID and value as ride history
        public void AddRides(string userID, Ride[] rides)
        {
            // While adding the data to the dictionary with use Id and ride history
            try
            {
                // Calling the Add ride method of Ride Repository class
                rideRepository.AddRide(userID, rides);
            }
            // Catching Exception  for null rides
            catch (CabInvoiceException)
            {
                // Returning the custom exception in case the rides are null
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides passed are null..");
                }
            }
        }
        // Method to return the invoice summary when passed with user ID
        public InvoiceSummary GetInvoiceSummary(string userID)
        {
            // Returning the Invoice Summary with the attributes of total fare, number of rides and average fare
            try
            {
                // Calculating Average fare
                double averageFare = (Convert.ToDouble(this.CalculateFare(rideRepository.GetRides(userID)))) / (rideRepository.GetRides(userID).Length);
                // returning invoice summary
                return new InvoiceSummary(Convert.ToDouble(this.CalculateFare(rideRepository.GetRides(userID))), rideRepository.GetRides(userID).Length, averageFare);
            }
            // Catching the custom exception of invalid user id
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "ID passed for User is Invalid");
            }
        }
    }
}