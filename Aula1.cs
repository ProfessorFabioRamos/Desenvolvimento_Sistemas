using System;

class Pessoa{
    // Atributos da classe
    public string nome = "";
    public int idade = 0;

    // Método para exibir informações
    public void Apresentar(){
        Console.WriteLine($"Olá, meu nome é: \n{nome} e tenho {idade} anos.");
    }
}

class Program{
    static void Main(string[] args){
        Pessoa pessoa_1 = new Pessoa();
        pessoa_1.nome = "João";
        pessoa_1.idade = 30;

        pessoa_1.Apresentar();
    }
}
