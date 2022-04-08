using ISSB.Web.Models.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISSB.Web.Models.Data
{
    public class SeederDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeederDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedDbAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.checkRoleAsync("Admin");
            await _userHelper.checkRoleAsync("Customer");

            //Add user with role
            var user = await _userHelper.GetUserByEmailAsync("karenonaly@gmail.com");
            
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Karen",
                    LastName = "Bautista",
                    Email = "karenonaly@gmail.com",
                    UserName = "KarenBautista",
                    PhoneNumber = "8299851507"
                };

                var result = await _userHelper.AddUserAsync(user, "12345678");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("No se pudo crear el usuario en el seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            // Add Products
            if (!_context.Products.Any())
            {
                this.AddProduct(
                    "I'm from - Tonico de Arroz 150ml", 
                    "Tonico blanqueador, iluminador e hidratante formulado con un 77% de extracto de arroz.",
                    2078.00M);
                this.AddProduct(
                    "iUnik - Serum Rose Galactomyces Synergy 50ml",
                    "Un serum extremadamente hidratante y nutritivo que proporciona una mejora completa de la piel con la union de la rosa y la levadura.",
                    1550.00M);
                this.AddProduct(
                    "Secret Key - Gel exfoliante Lemon Sparkling Peeling Gel 120ml",
                    "Con agua y extracto de limon, este gel exfolia la queratina, la grasa y las impurezas para que la piel se sienta fresca e hidratada.",
                    860.00M);
                this.AddProduct(
                    "Etude House - Tratamiento contra granitos, espinillas y acne localizado 15ml",
                    "Es un tratamiento suave contra granitos, espinillas y acne localizado que contiene acido salicilico, aceite de arbol de te y madecassoside que aporta energia extraida de las plantas naturales para que las pieles con imperfecciones se mantengas limpias, eliminando las areas afectadas",
                    1130.00M);
                this.AddProduct("TIA'M - Escencia de Baba de caracol azuleno y Azulene Snail & Azulene Water 180ml",
                        "Escencia acuosa con un 87% de filtrado de baba de caracol y " +
                        "azuleno que previene la perdida de humedad y restaura el equilibrio agua-aceite " +
                        "de la piel, mientras la mantiene calmada e hidratada. Libre de Alcohol y adecuada " +
                        "para pieles sensibles",
                        1330.00M);
                this.AddProduct("iUnik - Serum antiedad Noni Light oil 150ml",
                    "Serum infundido conextracto de fruta morinda y aceites " +
                        "de origen vegetal para proporcionar efectos antiedad que mejoren significativamente " +
                        "las lineas finas y arrugas, mientras hidratan y nutren la piel.",
                    1480.00M);
                this.AddProduct("MIZON - parche de gel para ojos intensivo reparador de caracol",
                        "Este lujoso parche para ojos esta formulado para ofrecer " +
                        "nutricion y tonificar de forma radical la zona del contorno de los ojos.",
                        1340.00M);
                this.AddProduct("Some by MI - Protector Solar",
                        "Protector solar respetuoso con los arrecifes de coral que forma una pantalla protectora contra los rayos UVA y UVB sin dejar restos blaquesinos en la piel.",
                        1060.00M);

                await _context.SaveChangesAsync(); 
            }
        }

        private void AddProduct(string productName, string productDescription, decimal price)
        {
            _context.Products.Add(new Product
            {
                ProductName = productName,
                ProductDescription = productDescription,
                Price = price,
                Stock = _random.Next(100)
            });
        }
    }
}
