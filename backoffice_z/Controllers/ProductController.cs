using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backoffice_z.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace backoffice_z.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        private ModelContext _context;
        private string _session;
        public ProductController(ModelContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet]
        public IActionResult GetProducts(string session)
        {
            if (session == null)
            {
                return Ok("Session can not be null");
            }
            _session = session;
            return Ok(_context.Products);
        }

        [Produces("application/json")]
        [HttpPut]
        public IActionResult ChangeStatus(string session, long productId, string status)
        {
            if (session == null)
            {
                return Ok("Session can not be null");
            }
            _session = session;
            Product _product = GetProduct(productId);
            _product.ProductStatus = status;
            _context.Products.Update(_product);
            _context.SaveChanges();
            Workflow currentWorkflow = GetWorkflow(session);
            if (RunWorkflow(currentWorkflow, _product) != 0)
            {
                return Ok("Product has been bought");
            }
            return Ok("Unknown error");
        }
        private int RunWorkflow(Workflow workflow, Product product)
        {
            string applications = workflow.Applications;
            JToken applicationsObject = JToken.Parse(applications);
            App app = GetAppInfo(product);
            if (applicationsObject.Value<string>("Backoffice") != null)
            {
                string shopJson = (string)applicationsObject["Backoffice"];
                JToken productsObject = JToken.Parse(shopJson);
                string productsArrJson = (string)productsObject["Products"];
                JArray productsArr = JArray.Parse(productsArrJson);
                string productJson = JsonConvert.SerializeObject(product);
                JToken productObject = JToken.Parse(productJson);
                productsArr.Add(productObject);
                string productsJson = JsonConvert.SerializeObject(productsArr);
                productsObject["Products"] = productsJson;
                string productsObjectJson = JsonConvert.SerializeObject(productsObject);
                applicationsObject["Backoffice"] = productsObjectJson;
            }
            else
            {
                applicationsObject["Backoffice"] = JsonConvert.SerializeObject(app);
            }
            workflow.Applications = JsonConvert.SerializeObject(applicationsObject);
            workflow.CurrentStage = "Backoffice";
            _context.Workflows.Update(workflow);
            return _context.SaveChanges();
        }


        private User GetUser()
        {
            Session session = _context.Sessions.FirstOrDefault(x => x.Token == _session);
            long user_id = long.Parse(session.UserId);
            User user = _context.Users.FirstOrDefault(x => x.Id == user_id);
            return user;
        }

        private Product GetProduct(long Id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == Id);
        }

        private Workflow GetWorkflow(string session)
        {
            Workflow workflow = _context.Workflows.FirstOrDefault(x => x.Session == session);
            return workflow;
        }
        private App GetAppInfo(Product order)
        {
            App app = new App();
            app.Name = "Back Office";
            app.Version = "1.0.0";
            app.StartedAt = DateTime.UtcNow.ToString();
            app.Action = "Status chnaged";
            Product[] products = new Product[1];
            products[0] = order;
            app.Products = JsonConvert.SerializeObject(products);
            return app;
        }

    }
}
