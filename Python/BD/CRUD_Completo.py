import sqlite3
conexao = sqlite3.connect("Inventario.db")
cursor = conexao.cursor()

# Função para criar tabela
def iniciar_banco():
  conexao = sqlite3.connect("Inventario.db")
  cursor = conexao.cursor()
  
  cursor.execute("""
  CREATE TABLE IF NOT EXISTS itens(
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      nome TEXT UNIQUE NOT NULL,
      tipo TEXT NOT NULL,
      valor INTEGER
      )
  """)

# CREATE - Inserir item
def inserir_item(nome,tipo,valor):
    try:
        cursor.execute("INSERT INTO itens (nome,tipo,valor) VALUES(?,?,?)", 
                       (nome,tipo,valor))
        conexao.commit()
        print("Item inserido com sucesso")
    except sqlite3.IntegrityError:
        print("Erro: Item já existente")

# READ - Listar ou ler itens 
def listar_itens():
    cursor.execute("SELECT * FROM itens")
    itens = cursor.fetchall()
    for i in itens:
        print(i)

#UDPATE - Atualizar instancia(linha) do item
def atualizar_item(id_item,novo_nome,novo_tipo, novo_valor):
    cursor.execute("UPDATE itens SET nome =?, tipo=?,valor=? WHERE id=?",
     (novo_nome,novo_tipo, novo_valor, id_item))
    
    if cursor.rowcount > 0:
        print("Item atualizado com sucesso!")
    else:
        print("Item não encontrado!")

#DELETE - Excluir item
def excluir_item(id_item):
    cursor.execute("DELETE FROM itens WHERE id = ?",(id_item,))

    if cursor.rowcount > 0:
        print("Item excluido com sucesso!")
    else:
        print("Item não encontrado!")

conexao.commit()
conexao.close()
