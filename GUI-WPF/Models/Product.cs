namespace CadastroProdutos.Models;

public class Product{
    public int Id {get;set;} // Obrigatório para o banco
    public string Name{get;set;} = "";
    public string Category{get;set;} = "";
    public float Price{get;set;} = 0;
    public int Quantity{get;set;} = 0;
}
