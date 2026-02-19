# ShelterHelper - Quick Start Guide

## 🚀 Getting Started in 5 Minutes

### Prerequisites
- .NET 10.0 SDK installed ([Download](https://dotnet.microsoft.com/download))
- Visual Studio Code or Visual Studio 2022
- Basic command line knowledge

### Installation Steps

1. **Clone or Download the Project**
   ```bash
   cd your-projects-folder
   ```

2. **Navigate to the ShelterHelper Directory**
   ```bash
   cd Shelter-Helper/ShelterHelper
   ```

3. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

4. **Build the Project**
   ```bash
   dotnet build
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Open in Browser**
   Navigate to: `https://localhost:5269`

---

## 📖 Basic Usage

### As a Visitor

1. **Home Page**: View featured pets and the Pet of the Week
2. **Browse Pets**: Click the "Pets" menu to see all available animals
3. **Filter**: Use the filter buttons to search by species (Dogs, Cats)
4. **View Details**: Click any pet card to see more information

### As Staff/Admin

1. **Access Admin Panel**: Navigate to `https://localhost:5269/admin`
2. **Add Pet**: Fill in pet details and upload a photo
3. **Manage Pets**: View all pets with edit/delete options
4. **Mark Pet of Week**: Set featured pets for homepage display

---

## 🗂️ Project Structure

```
ShelterHelper/
├── Controllers/      # Request handlers
├── Models/          # Data entities
├── Services/        # Business logic
├── Views/           # UI templates
├── wwwroot/         # Static files (CSS, JS, images)
└── Pages/           # Razor Pages
```

---

## 🔧 Common Tasks

### Add a New Pet

1. Go to Admin Panel
2. Fill in the form:
   - **Name**: Pet's name
   - **Species**: Dog or Cat
   - **Breed**: (Optional)
   - **Age**: In years
   - **Notes**: Personality or health info
   - **Photo**: Upload image (max 5MB)
3. Click "Save Pet"

### Search for Pets

1. Click "Pets" in navigation
2. Use the search bar to find by name
3. Or use filter buttons to browse by type

### Mark Pet of the Week

1. Admin Panel → Pet List
2. Click pet you want to feature
3. Check "Pet of the Week" checkbox
4. Save changes

---

## 🐛 Troubleshooting

### Port Already in Use
```bash
# Kill process on port 5269
lsof -ti:5269 | xargs kill -9

# Try a different port
dotnet run --urls "https://localhost:5270"
```

### Build Fails
```bash
# Clean and rebuild
dotnet clean
dotnet build
```

### Images Not Loading
- Check `wwwroot/images` folder exists
- Ensure folder permissions are correct
- Clear browser cache

### NuGet Package Errors
```bash
# Clear NuGet cache
dotnet nuget locals all --clear
dotnet restore
```

---

## 📊 Database (Development)

Currently uses **in-memory storage**. For production:

1. Configure SQL Server connection string
2. Create database migrations:
   ```bash
   dotnet ef migrations add Initial
   dotnet ef database update
   ```
3. Update `Program.cs` to use DbContext

---

## 🔐 Security Notes

- **Authentication**: Enable before production deployment
- **File Upload**: Currently accepts all image types (configure validation)
- **HTTPS**: Required for production
- **Input Validation**: All fields have validation - review and enhance as needed

---

## 📱 Testing on Mobile

1. Find your local IP:
   ```bash
   # Windows
   ipconfig
   
   # Mac/Linux
   ifconfig
   ```

2. Run with your IP:
   ```bash
   dotnet run --urls "https://0.0.0.0:5269"
   ```

3. Access from mobile: `https://YOUR-IP:5269`

---

## 🌐 Browser Support

- Chrome/Chromium (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

---

## 📝 Tips & Best Practices

✅ **Do:**
- Use meaningful pet names
- Add descriptive notes about temperament
- Use clear, well-lit pet photos
- Test on mobile devices

❌ **Don't:**
- Upload images larger than 5MB
- Use special characters in pet names
- Deploy without proper security setup
- Store passwords in code

---

## 🆘 Getting Help

- Check the full [README.md](README.md) for detailed documentation
- Review code comments in Services and Models
- Check ASP.NET Core documentation: https://docs.microsoft.com/aspnet/core

---

## 📚 Next Steps

1. ✅ Run the application locally
2. ✅ Add a few test pets
3. ✅ Test all CRUD operations
4. ✅ Review accessibility features (Tab navigation, screen reader)
5. ✅ Test responsive design (resize browser window)
6. ✅ Deploy to Azure or your hosting service

---

## 🎉 You're All Set!

Your ShelterHelper application is now running. Start adding pets and managing adoptions!

For detailed information, see [README.md](README.md)
