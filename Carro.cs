class Carro{
    // Atributos
    private string marca = "";
    private string modelo = "";
    private int anoDeFabricacao = 0;
    // Método
    public void LigarCarro(){
        Console.WriteLine($"{marca} {modelo} ano {anoDeFabricacao} está ligando!");
    }

    public void SetMarca(string novaMarca){
        if(novaMarca != ""){
            marca = novaMarca;
        }
    }
    public void SetModelo(string novoModelo){
        if(novoModelo != ""){
            modelo = novoModelo;
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
        Carro carro_1 = new Carro();
        carro_1.SetMarca("Toyota");
        carro_1.SetModelo("Corolla");
        carro_1.SetAno(2015);
        carro_1.LigarCarro();
    }
}
