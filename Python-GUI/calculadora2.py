# Biblioteca Simples para GUI
import tkinter as tk

# Cria a janela principal
root = tk.Tk()

# Título da janela
root.title("Calculadora 4 Operacoes")

#Tamanho da Janela
root.geometry("220x300")

cor_do_fundo = "#e2eb94"

#Cor de fundo
root.configure(bg= cor_do_fundo)

result =0

title = tk.Label(root, text= "CALCULADORA", 
                font = ("Comic Sans MS", 16, "bold"), bg=cor_do_fundo,
                fg= "#ffffff")
title.grid(row = 0, column = 0, columnspan = 2, pady = 10)

entry1 = tk.Entry(root, width = 10)
entry1.grid(row = 1, column = 0, padx=20, sticky = "e") 
# Alinhamento e = East(leste, direita)

entry2 = tk.Entry(root, width = 10)
entry2.grid(row = 1, column = 1, padx=20, sticky = "e") 

def operacao(id):
    num1 = float(entry1.get())
    num2 = float(entry2.get())
    match(id):
        case 0:
            result = num1+num2
        case 1:
            result = num1-num2
        case 2:
            result = num1*num2
        case 3:
            result = num1/num2
        case _:
            result = num1+num2

    label_result.config(text = f"Resultado: {result:.2f}")

#Botão de soma
sum_button = tk.Button(root,text="+")
sum_button.grid(row=2, column= 0, padx=10, pady=10)
sum_button.config(command = lambda:operacao(0))

#Botão de subtração
sub_button = tk.Button(root,text="-")
sub_button.grid(row=2, column= 1, padx=10, pady=10)
sub_button.config(command = lambda:operacao(1))

#Botão de multiplicação
mult_button = tk.Button(root,text="x")
mult_button.grid(row=3, column= 0, padx=10, pady=10)
mult_button.config(command = lambda:operacao(2))

#Botão de divisão
div_button = tk.Button(root,text="/")
div_button.grid(row=3, column= 1, padx=10, pady=10)
div_button.config(command = lambda:operacao(3))

label_result = tk.Label(root, text = "Resultado: ", font=("Bloody Terror",
                        20),bg = cor_do_fundo)
label_result.grid(row=4,column=0,columnspan=2,pady=20)

# Loop de eventos, mantem a janela aberta
root.mainloop()
