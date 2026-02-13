

using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.Formatters;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data
{
    public static class Seed
    {
        public static void Initialize(ReservasDbContext context)
        {
            if (context.Usuarios.Any()) return;


        var recursos = new List<Recurso>
        {
                new() { Nombre = "Sala de Conferencias A", Tipo = "Sala", Capacidad = 20 },
                new() { Nombre = "Sala de Reuniones B", Tipo = "Sala", Capacidad = 6 },
                new() { Nombre = "Proyector Epson X41", Tipo = "Equipo", Capacidad = 1 },
                new() { Nombre = "Laptop Dell XPS 15", Tipo = "Equipo", Capacidad = 1 },
                new() { Nombre = "Cancha de Fútbol 5", Tipo = "Espacio", Capacidad = 10 },
                new() { Nombre = "Cancha de Pádel 1", Tipo = "Espacio", Capacidad = 4 },
                new() { Nombre = "Cancha de Pádel 2", Tipo = "Espacio", Capacidad = 4 },
                new() { Nombre = "Estudio de Grabación", Tipo = "Sala", Capacidad = 3 },
                new() { Nombre = "Cámara Sony A7III", Tipo = "Equipo", Capacidad = 1 },
                new() { Nombre = "Auditorio Principal", Tipo = "Sala", Capacidad = 100 }
        };
            context.Recursos.AddRange(recursos);

            var usuarios = new List<Usuario>();

            for (int i = 1; i<=10; i++)
            {
                usuarios.Add(new Usuario
                {
                    Nombre = $"Usuario Test {i}",
                    PasswordHash = "hash_simulado_123", // mas adelante lo reemplazare con un sistema de hasheo
                    RolId= (i % 3) + 1,
                    FechaRegistro = DateTime.Now
                });
            }

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            foreach (var user in usuarios)
            {
                context.UsuarioEmails.Add(new UsuarioEmail
                {
                    UsuarioId = user.UsuarioId,
                    Email = $"user{user.UsuarioId}@email.com",
                    EsPrincipal = true
                });
            }
            context.SaveChanges();
        }

        

        

    }
}
