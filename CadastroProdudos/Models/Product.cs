namespace CadastroProdutos.Models;

public class Product{
    public int Id {get;set;} // Obrigatório para o banco
    public string Name{get;set;} = string.Empty;
    public string Category{get;set;} = string.Empty;
    public float Price{get;set;} = 0;
    public int Quantity{get;set;} = 0;
}