import tkinter as tk
from tkinter import ttk

lista_cadastro_clientes = [
    "Nome: Bruno Costa, Idade: 54, Email: bruno.c@email.com",
    "Nome: Carla Fernandes, Idade: 27, Email: carla.f@email.com",
    "Nome: Francisco Mendes, Idade: 32, Email: francisco.m@email.com",
    "Nome: Eliz Silva, Idade: 19, Email: eliz.s@email.com"
]

def show_data():
    # Modo normal para poder editar
    caixa_texto.config(state = "normal")
    # Limpar a caixa antes de inserir dados da linha 1 até o final
    caixa_texto.delete("1.0", tk.END)
    # Junta todos os elementos da lista mas adiciona uma quebra de linha entre eles
    texto_formatado = "\n".join(lista_cadastro_clientes)
    # Insere texto formatado na caixa (END=início)
    caixa_texto.insert(tk.END, texto_formatado)
    # Desliga aopção de edição
    #caixa_texto.config(state = "disabled")

def change_slider(valor): #valor = string
    valor_float = f"{float(valor):.0f}"
    label_valor_slider.config(text=f"Valor: {valor_float}")

def select_combo(event):
    valor = combo_opcoes.get()
    if valor != "Selecione uma opção":
        label_valor_combo.config(text = f"Estado Civil: {valor}")

root = tk.Tk()
root.title("Exemplo de Abas e Widgets")
root.geometry("450x320")

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
btn_buscar.config(command=show_data)
# Caixa de texto que recebe um cadastro
caixa_texto = tk.Text(
    aba2,
    height= 20,
    width=60,
    wrap="word",
    font = ("Arial",10)
)
caixa_texto.pack(pady=20, fill="both", expand=True)
caixa_texto.config(state= "disabled")

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
