# BurgerApp

## Version: 1.3.1 Stable

### About BurgerApp:

**BurgerApp** is a cutting-edge MVC prototype designed for burger shops. From managing locations, orders, and burgers, to tracking the most popular burger, average order price, all locations, and total order amount. Streamline your burger business with BurgerApp's clean and optimized code and user interface, fully compatible with mobile devices and boasting beautiful styling for both desktop and mobile users. Experience the simplicity and power of BurgerApp - the ultimate burger-ordering solution for your Burger Shop.

### Key Features:

- Major Code Optimization for Improved Performance
- Comprehensive XML Documentation for Clear Understanding
- Stylish UI Enhancements for a Sleek User Experience
- Bug Fixes and Enhancements for Smoother Operations
- Fully Mobile-Responsive Design
- Entity Framework Integration

### How to Run the App:

Follow these steps to run BurgerApp locally:

1. **Clone the Repository:** Start by cloning this repository to your local machine using the following command: `git clone https://github.com/Biohazardx44/MVC-Projects.git`
2. **Install Dependencies:** Ensure you have the necessary dependencies installed, including [Visual Studio](https://visualstudio.microsoft.com/downloads/) and [SQL Server/ SSMS](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. **Start the Solution:** Navigate to the directory where you cloned the repository and open the solution in Visual Studio
4. **Change the ConnectionString:** Navigate to `BurgerApp.Web > appsettings.json` and update the connection string to match your local database setup
5. **Open NuGet Package Console:** Navigate to `Tools > NuGet Package Manager > Package Manager Console`
6. **Setup Database:** Set BurgerApp.Web as the default project in the console and setup a database with `add-migration <DB Name>` and `update-database`
7. **Access BurgerApp:** Finally, launch the application by clicking on BurgerApp.Web button or by finding the url in `BurgerApp.Web > Properties > launchSettings.json`