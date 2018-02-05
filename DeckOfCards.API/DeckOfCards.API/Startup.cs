using DeckOfCards.API.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DeckOfCards.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSwaggerGen(opt =>
            {
               opt.SwaggerDoc("1.0.0.0", new Info { Title = "Deck of Cards API", Version="1.0.0.0", Description = "A web API for a deck of cards, where you can create a standard 52 playing card card, shuffle, cut and deal the top card from the deck." });
            });
            services.AddDbContext<DeckOfCardsContext>(opt =>
            {
                opt.UseInMemoryDatabase("Decks");
            });
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseSwagger();
            app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/swagger/1.0.0.0/swagger.json","The Deck of Cards API"));
            app.UseMvc();
        }
    }
}
