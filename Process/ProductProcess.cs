using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Process
{
    public class ProductProcess
    {
        public async Task<ApiResponse> GetProduct()
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Succes };
            try
            {
                using DefaultDbContext db = new();
                resp.Data = await db.Products.ToListAsync();
            }
            catch(Exception ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = $"An error occured while fetching products:{ex.Message}";
            }
            return resp;
        }
        public async Task<ApiResponse> SaveProduct(Product prod)
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Succes };
            try
            {
                using DefaultDbContext db = new();
                if (prod.Product_Id == 0 && !await db.Products.AnyAsync(s => s.Product_Name == prod.Product_Name)) { _ = await db.Products.AddAsync(prod); }
                else if (prod.Product_Id != 0 && !await db.Products.AnyAsync(s => s.Product_Id == prod.Product_Id)) { db.Products.Update(prod); }
                await db.SaveChangesAsync();
                resp.Data = await db.Products.FirstOrDefaultAsync(d => d.Product_Id == prod.Product_Id);
            }
            catch(Exception ex)
            {
                resp.Status = (byte)StatusFlags.Succes;
                resp.Message = $"An error occured while saving products:{ex.Message}";
            }
            return resp;
        }
        public async Task<ApiResponse> DeleteProduct(int id)
        {
            ApiResponse resp = new() { Status = (byte)StatusFlags.Succes };
            try
            {
                using DefaultDbContext db = new();
                Product prod = await db.Products.FirstOrDefaultAsync(s => s.Product_Id == id);
                if (prod == await db.Products.FirstOrDefaultAsync(s => s.Product_Id == id)) { db.Products.Remove(prod); db.SaveChanges(); }
                resp.Data = prod;
            }
            catch(Exception ex)
            {
                resp.Status = (byte)StatusFlags.Failed;
                resp.Message = $"An error occured while Deleting the products:{ex.Message}";
            }
            return resp;
        }
    }
}
