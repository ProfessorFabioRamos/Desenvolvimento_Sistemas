# Biblioteca Simples para GUI
import tkinter as tk

# Cria a janela principal
root = tk.Tk()

# Título da janela
root.title("Calculadora")

root.geometry("300x250")

def calcular_soma():
    num1 = float(entry1.get())
    num2 = float(entry2.get())
    soma = num1+num2
    label_result.config(text = f"Resultado: {soma}")

tk.Label(root, text= "CALCULADORA", font = "Arial, 16").pack(pady=5)

entry1 = tk.Entry(root)
entry1.pack(pady=5)

tk.Label(root, text= "+", font = "Arial, 12").pack(pady=5)

entry2 = tk.Entry(root)
entry2.pack(pady=5)

button = tk.Button(root, text = "Somar", font = "Arial,12",
                    command=calcular_soma)
button.pack(pady=5)

label_result = tk.Label(root, text = "Resultado: ", font ="Arial, 16")
label_result.pack(pady=5)

# Loop de eventos, mantem a janela aberta
root.mainloop()
