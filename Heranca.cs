using System;

// Classe Super
class Animal{
    public string nome = "";

    public virtual void EmitirSom(){
        Console.WriteLine("Animal: "+nome);
    }
    //Sobrecaraga do método EmitirSom
    public virtual void EmitirSom(string msg){
        Console.WriteLine("Animal está dizendo: "+msg);
    }

    public virtual void Comer(){
        Console.WriteLine($"Animal: {nome} está comendo");
    }
}

// Subclasse
class Cachorro : Animal{
    public string raca = "";

    //Sobrescrevendo o método EmitirSom (Polimorfismo)
    public override void EmitirSom(){
        Console.WriteLine("Au Au");
    }

    public override void Comer(){
        base.Comer();
        Console.WriteLine($"{nome} está comendo ração.");
    }
}

class Program{
    public static void Main(string[] args){
        Animal animal_1 = new Animal();
        animal_1.nome = "Animal 1";
        animal_1.EmitirSom();
        animal_1.EmitirSom("Olá");
        animal_1.Comer();
        Cachorro cachorro_1 = new Cachorro();
        cachorro_1.nome = "Totó";
        cachorro_1.EmitirSom();
        cachorro_1.Comer();
        cachorro_1.raca = "Basset";
    }
}
