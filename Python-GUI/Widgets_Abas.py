import tkinter as tk
from tkinter import ttk

def change_slider(valor): #valor = string
    valor_float = f"{float(valor):.0f}"
    label_valor_slider.config(text=f"Valor: {valor_float}")

def select_combo(event):
    valor = combo_opcoes.get()
    if valor != "Selecione uma opção":
        label_valor_combo.config(text = f"Estado Civil: {valor}")

root = tk.Tk()
root.title("Exemplo de Abas")
root.geometry("450x300")

# Criar o notebook (conteiner de abas)
notebook = ttk.Notebook(root)

# Criar as abas(frames)
aba1 = ttk.Frame(notebook)
aba2 = ttk.Frame(notebook)
aba3 = ttk.Frame(notebook)

# Adicionar os frames ao notebook
notebook.add(aba1, text="Cadastro")
notebook.add(aba2, text="Consulta")
notebook.add(aba3, text="Configurações")

# Empacotar o notebook
notebook.pack(pady=10,padx=10, expand=True, fill= "both")

# Widgets da Aba 1
label_aba_1 = ttk.Label(aba1, text="Formulário de Cadastro")
label_aba_1.pack(padx=20, pady=20)
entry_nome = ttk.Entry(aba1,width=40)
entry_nome.pack(padx = 20,pady=5)
btn_salvar = ttk.Button(aba1,text = "Salvar")
btn_salvar.pack(pady=10)

# Widgets da Aba 2
label_aba_2 = ttk.Label(aba2, text="Área de Consulta")
label_aba_2.pack(padx =20, pady =20)
btn_buscar = ttk.Button(aba2, text="Buscar")
btn_buscar.pack(pady =10)

# Varável de controle Boolean
var_check = tk.BooleanVar()
var_check.set(False)
#Widgets Aba 3
check_notification = ttk.Checkbutton(aba3,
                    text = "Receber Notificações", variable= var_check)
check_notification.pack(padx=20, pady=30)

# Função para dar get no valor boolean (true/false)
#ligado = check_notification.getboolean()
# Dropdown widget (Combobox)
frame = ttk.LabelFrame(aba3, text = "Opções")
frame.pack(padx=10,pady=10)

lista_estado_civil = [
    "Selecione uma opção",
    "Solteiro(a)",
    "Casado(a)",
    "Divorciado(a)",
    "Viúvo(a)"
]

combo_opcoes = ttk.Combobox(frame, values=lista_estado_civil,
                            state="readonly")
combo_opcoes.pack(padx=5,pady=10)
combo_opcoes.current(0)
label_valor_combo = ttk.Label(frame, text="Estado Civil: ")
label_valor_combo.pack(padx=10, pady=5)
combo_opcoes.bind("<<ComboboxSelected>>", select_combo)

# Widget slider (Scale)
slider = ttk.Scale(
    frame,
    from_=0,
    to=100,
    orient="horizontal",
    command=change_slider
)
slider.pack(padx=5, pady=10, fill= "x")

label_valor_slider = ttk.Label(frame, text="Valor: 0")
label_valor_slider.pack(padx=10, pady=5)

root.mainloop()
