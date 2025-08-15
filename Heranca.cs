class Animal{
    public string nome = "";

    public virtual void EmitirSom(){
        Console.WriteLine("Animal: "+nome);
    }
    //Sobrecarga Polimorfismo Estático
    public virtual void EmitirSom(string msg){
        Console.WriteLine("Animal disse: "+msg);
    }

    public virtual void Comer(){
        Console.WriteLine($"{nome} está comendo.");
    }
}

class Cachorro : Animal{
    public string raca ="";

    //Sobrescrita Polimorfismo Dinâmico
    public override void EmitirSom(){
        base.EmitirSom();
        Console.WriteLine("Au Au");
    }

    public override void Comer(){
        Console.WriteLine($"{nome} da raça {raca} está comendo ração.");
    }
}

class Program{
    public static void Main(string[] args){
        Animal animal_1 = new Animal();
        animal_1.nome = "Cavalo";
        animal_1.EmitirSom();
        animal_1.EmitirSom("Salve!");
        animal_1.Comer();

        Cachorro cachorro_1 = new Cachorro();
        cachorro_1.nome = "Totó";
        cachorro_1.raca = "Basset Hound";
        cachorro_1.EmitirSom();
        cachorro_1.Comer();
    }
}
