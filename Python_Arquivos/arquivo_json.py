import json

dados_universidade = {
    "universidade" : "CEUB",
    "ano_letivo" : 2025,
    "aprovados" : True,
    "alunos" : [
        {"nome" : "Ana Silva", "nota" : 9.4, "presenca": "100%"},
        {"nome" : "Carlos Souza", "nota" : 8.0, "presenca": "92%"},
        {"nome" : "João Nogueira", "nota" : 7.5, "presenca": "80%"}
    ]
}
# Escrita de JSON
try:
    with open("dados_universidade.json","w", encoding="utf-8") as f:
        json.dump(dados_universidade, f, indent=4, ensure_ascii=False)
    print("Arquivo criado com sucesso")
except Exception as e:
    print("Erro:",e)
######################################################################
# Leitura de JSON
try:
    with open("dados_universidade.json","r", encoding="utf-8") as f:
        dados = json.load(f)
        print(f"Universidade: {dados["universidade"]}")
        print("\nLista de Alunos:")
        for aluno in dados["alunos"]:
            print(f"- {aluno["nome"]}, Nota: {aluno["nota"]}, Presença:{aluno["presenca"]}")
except FileNotFoundError:
    print("Arquivo não encontrado")
except json.JSONDecodeError:
    print("O arquivo existe mas não é um json válido")
