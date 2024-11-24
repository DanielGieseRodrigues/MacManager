var builder = WebApplication.CreateBuilder(args);

// Nao mexi em NADA aqui, usei o razor mais para subir o server e permitir navegacao e cors do que qualquer coisa, nao pretendo usar  o two way databinding, acho que o jquery + bootstrap vai ser mais pratico pra demonstrar a api.
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
