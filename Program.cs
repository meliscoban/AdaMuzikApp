using Microsoft.EntityFrameworkCore;
using AdaMuzik.Data;
using AdaMuzik.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdaMuzikContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdaDB")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
#region Insert Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AdaMuzikContext>();

    if (!context.Sanatcilar.Any())
    {
        var morVeOtesi = new Sanatci { Ad = "Mor ve Ötesi", KurulusTarihi = new DateTime(1995, 1, 1) };
        var sebnemFerah = new Sanatci { Ad = "Þebnem Ferah", KurulusTarihi = new DateTime(1996, 1, 1) };
        context.Sanatcilar.AddRange(morVeOtesi, sebnemFerah);

        var dunya = new Album { Ad = "Dünya Yalan Söylüyor", CikisTarihi = new DateTime(2004, 4, 16), Sanatci = morVeOtesi };
        var sirenler = new Album { Ad = "Sirenler", CikisTarihi = new DateTime(2022, 1, 21), Sanatci = morVeOtesi };
        var canKiriklari = new Album { Ad = "Can Kýrýklarý", CikisTarihi = new DateTime(2005, 1, 1), Sanatci = sebnemFerah };
        var kadin = new Album { Ad = "Kadýn", CikisTarihi = new DateTime(2013, 1, 1), Sanatci = sebnemFerah };
        context.Albumler.AddRange(dunya, sirenler, canKiriklari, kadin);

        var sarkilar = new List<Sarki>
        {
            new() { Ad = "Bir Derdim Var", Album = dunya, Sanatci = morVeOtesi },
            new() { Ad = "Cambaz", Album = dunya, Sanatci = morVeOtesi },
            new() { Ad = "Sevda Çiçeði", Album = dunya, Sanatci = morVeOtesi },
            new() { Ad = "Forsa", Album = sirenler, Sanatci = morVeOtesi },
            new() { Ad = "Ýstiklal", Album = sirenler, Sanatci = morVeOtesi },
            new() { Ad = "Çakýl Taþlarý", Album = canKiriklari, Sanatci = sebnemFerah },
            new() { Ad = "Delgeç", Album = canKiriklari, Sanatci = sebnemFerah },
            new() { Ad = "Hoþçakal", Album = canKiriklari, Sanatci = sebnemFerah },
            new() { Ad = "Okyanus", Album = canKiriklari, Sanatci = sebnemFerah },
            new() { Ad = "Bu Aþk Fazla", Album = kadin, Sanatci = sebnemFerah },
            new() { Ad = "Yaðmurlar", Album = kadin, Sanatci = sebnemFerah },
        };
        context.Sarkilar.AddRange(sarkilar);

        var rock = new CalmaListesi { Ad = "Yüksek Ses Rock" };
        var kadinGucu = new CalmaListesi { Ad = "Yalnýz Kadýn Gücü" };
        context.CalmaListeleri.AddRange(rock, kadinGucu);

        var cls = new List<CalmaListesiSarkisi>
        {
            new() { CalmaListesi = rock, Sarki = sarkilar[0] },
            new() { CalmaListesi = rock, Sarki = sarkilar[1] },
            new() { CalmaListesi = rock, Sarki = sarkilar[7] },
            new() { CalmaListesi = rock, Sarki = sarkilar[9] },
            new() { CalmaListesi = kadinGucu, Sarki = sarkilar[6] },
            new() { CalmaListesi = kadinGucu, Sarki = sarkilar[8] },
            new() { CalmaListesi = kadinGucu, Sarki = sarkilar[9] },
        };
        context.CalmaListeleriSarkilari.AddRange(cls);

        context.SaveChanges();
    }
}
#endregion
*/

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
