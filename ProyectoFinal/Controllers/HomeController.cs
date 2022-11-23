﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Rules;
using System.Diagnostics;

namespace ProyectoFinal.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration )
        {
            _logger = logger;
            _configuration = configuration;
        }

       
        public IActionResult Index()
        {
            var rule = new PublicacionRule(_configuration);
            var posts = rule.GetPostHome();
            return View(posts);
        }

        public IActionResult Publicaciones(int cant= 5, int pagina=0 )
        {
            var rule = new PublicacionRule(_configuration);
            var posts = rule.GetPublicaciones( cant, pagina);
            return View(posts);
        }


        [Authorize]
        public IActionResult Nuevo()
        {
         
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Publicacion data)
        {
            var rule = new PublicacionRule(_configuration);
            rule.InsertPost(data);
            return RedirectToAction("Index");
        }


        public IActionResult Post(int id)
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetPostById(id);
            if (post== null)
            {
                return View("404");
            }

            return View(post);
        }


        public IActionResult Suerte()
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetOnePostRandom();
            return View(post);
        }

        public IActionResult AcercaDe()
        {
            return View();
        }


        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return View("Contacto", contacto);
            }

            var rule = new ContactoRule(_configuration);
            var mensaje = @"<h1>Gracias por el contacto </h1>
                           <p>Hemos recibido tu correo exitosamente. </p> 
                           <p>Nos pondremos en contacto a la brevedad </p> 
                            <hr><p>Saludos</p><p><b>Polo MC</b></p>  ";
            rule.SendEmail(contacto.Email, mensaje, "Mensaje Recibido", "Polo Mincha Claverto", "polo@poloMc.com.ar");
            rule.SendEmail("mauridemasi88@gmail.com",contacto.Mensaje, "Nuevo Contacto", contacto.Nombre, contacto.Email);


            return View("Contacto");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}