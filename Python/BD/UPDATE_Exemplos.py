import sqlite3

conexao = sqlite3.connect('loja.db')
cursor = conexao.cursor()

cursor.execute("""
CREATE TABLE IF NOT EXISTS pedidos(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    cliente_id INTEGER NOT NULL,
    valor REAL CHECK (valor>0),
    status TEXT CHECK (status IN('pendente','concluido','cancelado'))
    )           
""")

def inserir_pedidos(c,v,s):
    cursor.execute("INSERT INTO pedidos (cliente_id, valor, status) VALUES(?,?,?)",
        (c,v,s)) 
                   
inserir_pedidos(22, 400.50,'pendente')
inserir_pedidos(22, 28.50,'concluido')
inserir_pedidos(14, 39.50,'concluido')
inserir_pedidos(42, 1000.00,'cancelado')

#Atualizar campo pelo ID
cursor.execute("UPDATE pedidos SET status = 'concluido' WHERE id=1")
# Atualizar multiplos campos ao mesmo tempo
cursor.execute("UPDATE pedidos SET valor = 33.33, status = 'pendente' WHERE id=2")
# Atualiza Multiplas linhas com a mesma condição
cursor.execute("UPDATE pedidos SET status = 'pendente' WHERE status= 'concluido'")
#Atualiza um campo por outro valor(cliente_id)
cursor.execute("UPDATE pedidos SET status = 'cancelado' WHERE cliente_id = 22")
# Atualizar com uma condição
cursor.execute("UPDATE pedidos SET status = 'concluido' WHERE valor >100")

conexao.commit()
conexao.close()
