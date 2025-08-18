interface IVeiculo{
    string Nome{get;set;}

    // Assinatura de métodos (sem corpo)
    void Ligar();
    void Desligar();
}

class Aviao : IVeiculo{
    public string Nome{get;set;} = "";
    private bool ligado = false;

    public void Ligar(){
        Console.WriteLine($"Avião: {Nome} está ligando...");
        ligado = true;
    }

    public void Desligar(){
        Console.WriteLine($"Avião: {Nome} está desligando...");
        ligado = false;
    }

    public string Info(){
        return "Avião ligado: "+ligado; 
    }
}
class Program{
    public static void Main(string[] args){
        //IVeiculo veiculo_1 = new Aviao();
        Aviao aviao_1 = new Aviao();
        aviao_1.Nome = "Boeing";
        aviao_1.Ligar();
        Console.WriteLine(aviao_1.Info());
        aviao_1.Desligar();
        Console.WriteLine(aviao_1.Info());
    }
}
