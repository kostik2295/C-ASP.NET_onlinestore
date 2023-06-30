using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data.Entity;
using System.Xml.Linq;
using ToyStoreWEB.Models;
using System.IO;
using Aspose.Pdf;

namespace ToyStoreWEB.Pages
{
    public class AdminsModel : PageModel
    {
        StoreDbContext db = new StoreDbContext();
        public  List<Category> Categories { get; set; } = new List<Category>();
        public List<Producer> Producers { get; set; } = new List<Producer>();
        public void Load()
        {
            db.Products.Load();
            db.Categories.Load();
            db.Producers.Load();

            Categories = db.Categories.ToList();
            Producers = db.Producers.ToList();
        }
        public void OnGet()
        {
            Load();
            UserInfo.Status = UserStatus.AdminExit;
        }

        public void OnPostProduct(string name, string price, string description, string url, int categoryId, int producerId)
        {
            Load();
            Category currentCategory = db.Categories.FirstOrDefault(x => x.Id == categoryId);
            Producer currentProducer = db.Producers.FirstOrDefault(x => x.Id == producerId);
            db.Products.Add(new Product { Name = name, Description = description, Price = Convert.ToDouble(price), Url = url, Category = currentCategory, Producer = currentProducer});
            db.SaveChanges();
            Load();
        }

        public void OnPostProducer(string name, string company)
        {
            Load();
            db.Producers.Add(new Producer { Name = name, Company = company });
            db.SaveChanges();
            Load();
        }

        public void OnPostCategory(string name)
        {
            Load();
            db.Categories.Add(new Category { Name = name});
            db.SaveChanges();
            Load();
        }



        public IActionResult OnPostExport(DateTime after, DateTime before)
        {
            Load();
            db.Orders.Include(x => x.User).Include(x => x.Products).Load();
            DateTime end = before.AddDays(1);
            List<Order> orders = db.Orders.Where(x => x.Date >= after && x.Date <= end).ToList();

            Document document = new Document();

            Aspose.Pdf.Page page = document.Pages.Add();

            string title = $"Отчет: {after.ToShortDateString()} - {before.ToShortDateString()}";

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(title));

            foreach (Order order in orders)
            {
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(""));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Заказ №{order.Id}"));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Пользователь: {order.User.Login}"));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment($"Дата: {order.Date}"));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(""));

                Aspose.Pdf.Table table = new Aspose.Pdf.Table();

                table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));

                table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));

                Aspose.Pdf.Row row = table.Rows.Add();
                row.Cells.Add("Название");
                row.Cells.Add("Цена");
                row.Cells.Add("Количество");
                row.Cells.Add("Сумма");
                double total = 0;
                foreach (ProductInOrder productInOrder in order.Products)
                {
                    Aspose.Pdf.Row row1 = table.Rows.Add();
                    row1.Cells.Add(productInOrder.Product.Name);
                    row1.Cells.Add(productInOrder.Product.Price.ToString());
                    row1.Cells.Add(productInOrder.Count.ToString());
                    double sum = (productInOrder.Product.Price * productInOrder.Count);
                    total += sum;
                    row1.Cells.Add(sum.ToString());
                }

                Aspose.Pdf.Row row2 = table.Rows.Add();
                row2.Cells.Add("");
                row2.Cells.Add("");
                row2.Cells.Add("Итого");
                row2.Cells.Add(total.ToString());

                page.Paragraphs.Add(table);
            }

            document.Save($"{title}.pdf");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "", $"{title}.pdf");

            
            IActionResult file = PhysicalFile(filePath, "application/octet-stream", Path.GetFileName(filePath));
            return file;
        }
    }
}
