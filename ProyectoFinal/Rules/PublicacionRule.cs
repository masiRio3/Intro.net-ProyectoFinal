using Dapper;
using ProyectoFinal.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoFinal.Rules
{
    public class PublicacionRule
    {
        private readonly IConfiguration _configuration;

        public PublicacionRule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Publicacion GetOnePostRandom()
        {
            var connectionString = _configuration.GetConnectionString("BlogDatabase");

            using var connection = new SqlConnection(connectionString);
            {
                connection.Open();
                var posts = connection.Query<Publicacion>("SELECT TOP 1 * FROM Publicacion ORDER BY NEWID()");

                return posts.First();
            }


        }

        public List <Publicacion> GetPostHome()
        {
            var connectionString = _configuration.GetConnectionString("BlogDatabase");

            using var connection = new SqlConnection(connectionString);
            {
                connection.Open();
                var posts = connection.Query<Publicacion>("SELECT TOP 4 * FROM Publicacion ORDER BY Creacion DESC");
                return posts.ToList();
            }


        }

        public void InsertPost(Publicacion data)
        {
            var connectionString = _configuration.GetConnectionString("BlogDatabase");
            using var connection = new SqlConnection(connectionString);
            {
                connection.Open();
                var queryInsert = "INSERT INTO Publicacion (Titulo, Subtitulo, Creador, Cuerpo, Creacion, Imagen) VALUES (@titulo, @subtitulo, @creador, @cuerpo, @creacion, @imagen) ";
                var result = connection.Execute(queryInsert, new
                {
                    titulo = data.Titulo,
                    subtitulo = data.SubTitulo,
                    creador = data.Creador,
                    cuerpo = data.Cuerpo,
                    creacion = data.Creacion,
                    imagen = data.Imagen

                });
            }

        }


        public Publicacion GetPostById(int id)
        {
            var connectionString = _configuration.GetConnectionString("BlogDatabase");

            using var connection = new SqlConnection(connectionString);
            {
                connection.Open();

                var query = "SELECT * FROM Publicacion WHERE Id=@id";
                var posts = connection.QueryFirstOrDefault<Publicacion>(query, new {id});


                //if (posts==null)
                //{
                //    return HttpNotFound();
                //}

                return posts;
            }


        }

    }
}
