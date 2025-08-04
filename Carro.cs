using System;

class Carro{
    // Atributos da classe carro
    private string marca = "";
    private string modelo = "";
    private int anoDeFabricacao = 0;

    // Método para ligar o carro
    public void Ligar(){
        Console.WriteLine($"{marca} {modelo} está ligado!");
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
}

class Program{
    static void Main(string[] args){
        Carro carro_1 = new Carro();
        carro_1.SetMarca("Toyota");
        carro_1.SetModelo("Corolla");
        //carro_1.anoDeFabricacao = 2016;
        carro_1.Ligar();
    }
}
