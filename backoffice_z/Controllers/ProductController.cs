using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backoffice_z.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return Ok(_context.Products);
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
    }
}
