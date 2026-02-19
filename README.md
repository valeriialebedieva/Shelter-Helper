# ShelterHelper - Pet Adoption Management System

## Overview

ShelterHelper is a comprehensive .NET Blazor web application designed to help animal shelters manage their pet inventory and facilitate pet adoptions. The application provides a user-friendly interface for browsing available pets, managing shelter operations, and connecting potential adopters with their perfect companion.

## Key Features

- **Pet Management**: Complete CRUD operations for managing pet profiles
- **User Authentication**: Secure login system for staff and administrators
- **Pet Search & Filter**: Search pets by name, species, and availability status
- **Pet of the Week**: Highlight special pets for increased adoption visibility
- **Image Upload**: Add photos to pet profiles for better presentation
- **Responsive Design**: Optimized for desktop, tablet, and mobile devices
- **WCAG 2.1 Level AA Compliance**: Accessible to all users, including those with disabilities

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- SQL Server or compatible database (for production deployment)

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/Shelter-Helper.git
   cd Shelter-Helper/ShelterHelper
   ```

2. **Install Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the Project**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

5. **Access the Application**
   Open your browser and navigate to `https://localhost:5269`

## Usage Guide

### For Visitors

1. **Browse Pets**: Visit the home page to see all available pets for adoption
2. **Search & Filter**: Use the filter buttons to search by species (Dogs, Cats)
3. **View Pet Details**: Click on a pet card to see more information
4. **Request Adoption**: Contact the shelter through the provided contact information

### For Staff

1. **Admin Login**: Navigate to the admin panel (URL: `/admin`)
2. **Add New Pet**: Fill out the pet form with details and upload a photo
3. **Manage Pets**: View, edit, or delete pet listings
4. **Pet of the Week**: Mark special pets for featured placement

## Project Architecture

### Structure

```
ShelterHelper/
├── Controllers/           # MVC Controllers for request handling
│   ├── HomeController.cs  # Home page and navigation
│   └── PetController.cs   # Pet management routes
├── Models/               # Data models
│   ├── Pet.cs           # Pet entity with validation
│   └── ErrorViewModel.cs # Error handling
├── Services/            # Business logic layer
│   └── PetService.cs    # Pet data management
├── Views/               # Razor views for rendering
│   ├── Home/            # Home page views
│   ├── Pet/             # Pet management views
│   └── Shared/          # Shared layout and components
├── wwwroot/            # Static files
│   ├── css/            # Stylesheets
│   ├── js/             # Client-side scripts
│   └── images/         # Pet photos
└── Pages/              # Razor Pages (future Blazor integration)
```

### Key Components

- **PetService.cs**: Handles all pet-related business logic and CRUD operations
- **Pet.cs**: Defines the pet data model with validation attributes
- **Program.cs**: Application startup configuration and dependency injection
- **Controllers**: Handle HTTP requests and coordinate with services

## Code Comments

The codebase includes comprehensive XML documentation comments for:
- All public classes and methods
- Service operations and their parameters
- Model properties and validation rules
- Complex business logic

## Performance Optimization

- **Lazy Loading**: Pet images are loaded asynchronously
- **Minimal CSS**: Only necessary styles are loaded
- **Efficient Queries**: Service filters data at the collection level
- **Responsive Images**: Optimized image sizes for different devices

## Accessibility

The application complies with WCAG 2.1 Level AA standards:

- **Semantic HTML**: Proper use of heading levels, labels, and form elements
- **ARIA Labels**: Descriptive labels for interactive elements
- **Color Contrast**: Minimum 4.5:1 contrast ratio for text
- **Keyboard Navigation**: All features accessible via keyboard
- **Alt Text**: Descriptive alt text for all images
- **Form Validation**: Clear error messages for form validation

## Browser Support

- Chrome/Chromium (latest 2 versions)
- Firefox (latest 2 versions)
- Safari (latest 2 versions)
- Edge (latest 2 versions)

## Testing

Run the application in development mode for testing:

```bash
dotnet run --environment Development
```

The development environment includes detailed error pages and logging for debugging.

## Deployment

### Cloud Deployment

The application can be deployed to:
- Azure App Service
- AWS Elastic Beanstalk
- Google Cloud App Engine
- Any hosting service supporting .NET Core

### Steps for Azure Deployment

1. Create an Azure App Service
2. Configure connection strings in application settings
3. Deploy using Visual Studio or Azure CLI
4. Configure SSL certificates for HTTPS

## Maintenance

### Database Backups
- Scheduled daily backups (configure based on your hosting provider)

### Security Updates
- Regularly update NuGet packages: `dotnet package update`
- Review and apply security patches
- Implement rate limiting and input validation

### Logging
- Application logs are stored in the configured log provider
- Monitor application health and performance

## Troubleshooting

### Issue: "Unable to resolve service for type 'PetService'"
**Solution**: Ensure PetService is registered in Program.cs:
```csharp
builder.Services.AddScoped<PetService>();
```

### Issue: Images not loading
**Solution**: Check that the `wwwroot/images` directory exists and has proper permissions

### Issue: Authentication not working
**Solution**: Verify that authentication middleware is configured in Program.cs before authorization

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues, questions, or suggestions, please open an issue on the GitHub repository.

## Team

- **Project Manager**: [Trello Board Link]
- **Lead Developer**: Your Name
- **Contributors**: Team members

## Acknowledgments

- Bootstrap for responsive UI framework
- .NET team for the excellent framework
- Community contributors and testers

## Changelog

### Version 1.0.0 (Initial Release)
- Pet browsing and filtering
- Admin panel for pet management
- User authentication
- WCAG 2.1 Level AA compliance
- Responsive design for all devices
