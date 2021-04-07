using System;
using System.Collections.Generic;
using System.Text;
namespace Day_23_Cab_Invoice_Generator
{
    // Class to store the data of the customer id and his ride history
    public class RideRepository
    {
        // Dictionary storing key as the user Id and the ride list as value
        Dictionary<string, List<Ride>> userRides = null;
        // Default constructor Initialising the instance of the User Ride Dictionary
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        // Method to add the ride data onto the dictionary
        public void AddRide(string userID, Ride[] rides)
        {
            //Checking customer data on basis of his id as the key
            bool rideList = this.userRides.ContainsKey(userID);
            // Catching the custom exception when the rides being passed is null
            try
            {
                // If the ride is not null
                if (!rideList)
                {
                    // Creating the list as listOfRides
                    List<Ride> listOfRides = new List<Ride>();
                    // Adding the array to the list
                    listOfRides.AddRange(rides);
                    // Adding to the dictionary with userId as the key and list of rides as the value
                    this.userRides.Add(userID, listOfRides);
                }
            }
            // Catch exception if the list of ride details is null
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides passed are null..");
            }
        }
        // Method to get the details of the ride history with userID as key
        public Ride[] GetRides(string userID)
        {
            // Returning the ride history as array of Ride class type
            return this.userRides[userID].ToArray();
        }
    }
}