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
            /// Adding a variable to compute average fare
            double averageFare = 0;
            /// Exception handling for the invalid  distance and time
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
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides passed are null..");
                }
            }
            // Returning the invoice summary with average fare too
            return new InvoiceSummary(totalFare, rides.Length, averageFare);
        }
    }
}