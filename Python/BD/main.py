import sys
import sqlite3 
import banco as inventario

def mostrar_menu():
    print("***Inventário***")
    print("1 - Inserir Item")
    print("2 - Listar Item")
    print("3 - Atualizar Item")
    print("4 - Excluir Item")
    print("0 - Sair")

def insert():
    nome = input("Digite o nome do item: ").strip()
    tipo = input("Digite o tipo do item: ").strip()
    try:
        valor = float(input("Digite o valor do item: "))
    except ValueError:
        print("Valor inválido. Use um valor float")
        return
    
    inventario.inserir_item(nome,tipo,valor)

def main():
    inventario.iniciar_banco()
    while(True):
        mostrar_menu()
        opcao = input("Digite a opção:")
        match opcao:
            case "1":
                insert()
            case "2":
                print("Listar dados")   #Placeholder
            case "3":
                print("Atualizar dados")#Placeholder
            case "4":
                print("Excluir dados")  #Placeholder
            case "0":
               sys.exit(0) 

main()











'''
inventario.iniciar_banco()

inventario.inserir_item("Espada Vorpal", "Arma", 1000)
inventario.inserir_item("Escudo de Madeira",
                        "Escudo", 5.2)
inventario.inserir_item("Poção de Cura Menor","Poção",20.5)

inventario.listar_itens()
inventario.atualizar_item(1, "Espada Vorpal", "Arma", 1500)
inventario.atualizar_item(2, "Escudo de Madeira",
                          "Proteção", 6)

inventario.listar_itens()
inventario.excluir_item(2)
inventario.listar_itens()
'''
