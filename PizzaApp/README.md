# PizzaApp

## Version: 1.2.1 Stable

### About PizzaApp:

**PizzaApp** redefines pizza order management in the MVC realm. This prototype empowers you to seamlessly handle orders, users, promotions, and details. From classic Margherita to innovative creations, PizzaApp streamlines the process, offering a responsive design. Manage users, orders, and pizzas with ease, while tracking promotions and order counts. With clean code and mobile responsiveness, it provides the perfect solution for pizza shop owners.

### Key Features:

- Major Code Optimization for Improved Performance
- Comprehensive XML Documentation for Clear Understanding
- Stylish UI Enhancements for a Sleek User Experience
- Bug Fixes and Enhancements for Smoother Operations
- Fully Mobile-Responsive Design
- Entity Framework Integration

### How to Run the App:

Follow these steps to run PizzaApp locally:

1. **Clone the Repository:** Start by cloning this repository to your local machine using the following command: `git clone https://github.com/Biohazardx44/MVC-Projects.git`
2. **Install Dependencies:** Ensure you have the necessary dependencies installed, including [Visual Studio](https://visualstudio.microsoft.com/downloads/) and [SQL Server/ SSMS](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. **Start the Solution:** Navigate to the directory where you cloned the repository and open the solution in Visual Studio
4. **Change the ConnectionString:** Navigate to `PizzaApp.Web > appsettings.json` and update the connection string to match your local database setup
5. **Open NuGet Package Console:** Navigate to `Tools > NuGet Package Manager > Package Manager Console`
6. **Setup Database:** Set PizzaApp.Web as the default project in the console and setup a database with `add-migration <DB Name>` and `update-database` commands in the console
7. **Access PizzaApp:** Finally, launch the application by clicking on PizzaApp.Web button or by finding the url in `PizzaApp.Web > Properties > launchSettings.json`