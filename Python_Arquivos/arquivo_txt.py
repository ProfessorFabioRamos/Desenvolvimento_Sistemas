# Escrita de arquivo .txt
linhas_para_escrever = [
    "Olá Mundo!\n",
    "Segunda linha.\n",
    "Teste de acentuação e ç."
]

# "w" = Modo Write (escrita) 
# "r" = Modo Read (leitura) 

try:
    with open("Meu_Arquivo.txt", "w", encoding= "utf-8") as f:
        f.write("Este é o cabeçalho.\n")
        f.write("-------------------\n")
        f.writelines(linhas_para_escrever)
    print("Arquivo: Meu_Arquivo.txt foi escrito com sucesso!")
except Exception as e:
    print(f"Ocorreu um erro ao escrever o arquivo: {e}")
######################################################################
# Leitura de arquivo
try:
    with open("Meu_Arquivo.txt", "r", encoding= "utf-8") as f:
        print("-- Lendo Arquivo linha por linha -- ")
        for linha in f:
            print(f"{linha.strip()}")
except FileNotFoundError:
    print("Arquivo não encontrado")
except Exception as e:
    print(f"Ocorreu um erro ao ler o arquivo: {e}")
