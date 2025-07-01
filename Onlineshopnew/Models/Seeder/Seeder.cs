namespace Onlineshopnew.Models.Seeder
{
    public class Seeder
    {
        public static void SeedBrands(ContextDB context)
        {
            if (!context.TblBrands.Any())
            {
                context.TblBrands.AddRange(
                    new TblBrand { Name = "Samsung" },
                    new TblBrand { Name = "Apple" },
                    new TblBrand { Name = "Xiaomi" }
                    );
                context.SaveChanges();
            }

        }
        public static void SeedRoles(ContextDB context)
        {
            if (!context.TblRoles.Any())
            {
                var Roles = new List<TblRole>
                {
                     new TblRole {Id = 1,Name = "Admin",Title ="Admin" },
                     new TblRole {Id = 2,Name = "Seller",Title ="Seller" },
                     new TblRole{Id = 3,Name = "Costumer",Title="Costumer"
                        } };
                context.TblRoles.AddRange(Roles);
                context.SaveChanges();
            }


        }




    }
}
