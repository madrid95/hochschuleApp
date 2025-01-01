using Autofac;
using HochschuleApp.context;
using HochschuleApp.repository;
using HochschuleApp.screens;
using HochschuleApp.service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HochschuleApp
{
    /// <summary>
    /// Stellt Methoden zur Konfiguration des Autofac-Containers bereit.
    /// </summary>
    public static class ConfigureClass
    {
        /// <summary>
        /// Konfiguriert den Autofac-Container und registriert die benötigten Typen.
        /// </summary>
        /// <returns>Der konfigurierte Autofac-Container.</returns>
        public static Autofac.IContainer Configure(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            // Registrierung der HochschuleContext
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<HochschuleContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); // Use the connection string from configuration
                return new HochschuleContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            // Registrierung der Repositories
            builder.RegisterType<CourseRepository>();
            builder.RegisterType<StudentRepository>();
            builder.RegisterType<SemesterRepository>();
            builder.RegisterType<LecturerRepository>();

            // Registrierung der Services
            builder.RegisterType<CourseService>();
            builder.RegisterType<StudentService>();
            builder.RegisterType<SemesterService>();
            builder.RegisterType<LecturerService>();

            // Registrierung der Screens
            builder.RegisterType<CourseScreen>();
            builder.RegisterType<StudentScreen>();
            builder.RegisterType<SemesterScreen>();
            builder.RegisterType<LecturerScreen>();
            builder.RegisterType<HochschuleAppMainScreen>();

            // Registrierung der Application
            builder.RegisterType<Application>();

            return builder.Build();
        }
    }
}
