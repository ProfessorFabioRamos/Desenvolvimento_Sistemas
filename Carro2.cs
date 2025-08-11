using System;

class Carro{
    // Atributos da classe carro
    private string marca = "";
    private string modelo = "";
    private int anoDeFabricacao = 0;

    // Construtor com argumentos
    public Carro(string marca, string modelo, int anoDeFabricacao){
        this.marca = marca;
        this.modelo = modelo;
        this.anoDeFabricacao = anoDeFabricacao;
    }

    public string GetMarca(){
        return marca;
    }
    public string GetModelo(){
        return modelo;
    }
    public int GetAno(){
        return anoDeFabricacao;
    }

    // Método para ligar o carro
    public void Ligar(){
        Console.WriteLine($"{marca} {modelo} do ano {anoDeFabricacao} está ligado!");
    }

    public void SetModelo(string novoModelo){
        if(novoModelo != ""){
            modelo = novoModelo;
        }
    }

    public void SetMarca(string novaMarca){
        if(novaMarca != ""){
            marca = novaMarca;
        }
    }

    public void SetAno(int novoAno){
        if(novoAno >= 2010){
            anoDeFabricacao = novoAno;
        }
    }
}

class Program{
    static void Main(string[] args){
        Carro carro_1 = new Carro("Honda","Civic",2014);
        carro_1.Ligar();
        carro_1.SetMarca("Toyota");
        carro_1.SetModelo("Corolla");
        carro_1.SetAno(2016);
        carro_1.Ligar();
        Console.WriteLine($"Marca: {carro_1.GetMarca()} \nModelo: {carro_1.GetModelo()} \nAno de Fabricação: {carro_1.GetAno()}");
    }
}
