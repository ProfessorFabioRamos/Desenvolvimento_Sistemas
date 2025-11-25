using Microsoft.EntityFrameworkCore;
using CadastroProdutos.Models;

namespace CadastroProdutos.Data;

public class AppDbContext : DbContext{
    // Tabela de Produtos no Banco
    public DbSet<Product> Products {get;set;}

    // Configuração do arquivo Sqlite(.db)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=produtos.db");
    }
}