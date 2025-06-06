using MVC_SomerenProject.Repositories;
using MVC_SomerenProject.Repositories.ActivitiesRepo;
using MVC_SomerenProject.Repositories.DrinksRepo;
using MVC_SomerenProject.Repositories.LecturersRepo;
using MVC_SomerenProject.Repositories.ParticipantsRepo;
using MVC_SomerenProject.Repositories.RoomsRepo;
using MVC_SomerenProject.Repositories.StudentsRepo;
using MVC_SomerenProject.Repositories.SupervisorsRepo;

namespace MVC_SomerenProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddSingleton<IStudentsRepository, StudentsRepository>();
            builder.Services.AddSingleton<ILecturersRepository, LecturersRepository>();
            builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();
            builder.Services.AddSingleton<IDrinksRepository, DrinksRepository>();
            builder.Services.AddSingleton<IActivitiesRepository, ActivitiesRepository>();
            builder.Services.AddSingleton<ISupervisorsRepository, SupervisorsRepository>();
			builder.Services.AddSingleton<IParticipantsRepository,ParticipantsRepository>();
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
