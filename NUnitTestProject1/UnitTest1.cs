using NUnit.Framework;
using Day_23_Cab_Invoice_Generator;
namespace NUnitTestProject
{
    public class Tests
    {
        // Initialising the instance of the Invoice generator class and assigning a null value to its reference
        public InvoiceGenerator invoiceGenerator = null;
        // TC 1- Given the distance and time returning the total fare by calculating through the calculate fare method
        [Test]
        public void GivenDistanceAndTime_ReturnsTotalFare()
        {
            double distance = 5.0;
            int time = 5;
            // Initializing the object with normal ride type
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            // Invoking the Calculate Fare method to get the total actual fare
            double totalActualFare = invoiceGenerator.CalculateFare(distance, time);
            double totalExpectedFare = 55.0;
            // Asserting with the expected value
            Assert.AreEqual(totalExpectedFare, totalActualFare);
        }
        // TC.2- GivenMultipleRides_ShouldReturnInvoiceSummary
        [Test]
        public void GivenMultipleRides_ShouldReturnInvoiceSummary()
        {
            // Arrange
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            // Act
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateFare(rides);
            // if both the objects are equal then get the same HashCode for both the objects
            var resultHashCode = invoiceSummary.GetHashCode();
            InvoiceSummary expectedInvoiceSummary = new InvoiceSummary(30.0, 2);
            var resulExpectedHashCode = expectedInvoiceSummary.GetHashCode();
            // Assert
            Assert.AreEqual(expectedInvoiceSummary, invoiceSummary);
        }
        // TC.3- GivenMultipleRides_ShouldReturnInvoiceSummaryWithAvg
        [Test]
        public void GivenMultipleRides_ShouldReturnInvoiceSummaryWithAvg()
        {
            // Arrange
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            // Act
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateAvgFare(rides);
            // if both the objects are equal then get the same HashCode for both the objects
            var resultHashCode = invoiceSummary.GetHashCode();
            InvoiceSummary expectedInvoiceSummary = new InvoiceSummary(30.0, 2, 15.0);
            var resulExpectedHashCode = expectedInvoiceSummary.GetHashCode();
            // Assert
            Assert.AreEqual(expectedInvoiceSummary, invoiceSummary);
        }
        // TC.4- GivenUserId_ShouldReturnInvoiceSummary
        [Test]
        public void GivenUserId_ShouldReturnInvoiceSummary()
        {
            // Arrange 
            // initialising the object of the invoice generator class
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            // Creating the object of the ride repository
            RideRepository repository = new RideRepository();
            // Initialising the user ID
            string userID = "akash";
            // Initialising the ride array with details of the ride
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            // Adding the ride data for the user to the ride repository
            repository.AddRide(userID, rides);
            // Getting the ride data from the ride repository class using the user ID
            Ride[] rideData = repository.GetRides(userID);
            // Getting the invoice summary when passing the ride Data
            InvoiceSummary invoiceSummary = invoiceGenerator.CalculateAvgFare(rideData);
            // Expected value of the Invoice Summary
            InvoiceSummary expectedInvoiceSummary = new InvoiceSummary(30.0, 2, 15.0);
            // Asserting the comparison between the expected and actual invoice summary
            Assert.AreEqual(expectedInvoiceSummary, invoiceSummary);
        }
        // TC.5-GeneratesInvoiceForRideType-Premium Ride
        [Test]
        public void GeneratesInvoiceForRideType()
        {
            double distance = 2.0;
            int time = 5;
            // Initializing the object with normal ride type
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            // Invoking the Calculate Fare method to get the total actual fare
            double totalActualFare = invoiceGenerator.CalculateFare(distance, time);
            double totalExpectedFare = 40.0;
            // Asserting with the expected value
            Assert.AreEqual(totalExpectedFare, totalActualFare);
        }
    }
}