import sqlite3

conexao = sqlite3.connect("empresa.db")
cursor = conexao.cursor()

cursor.execute("""
CREATE TABLE IF NOT EXISTS clientes(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nome TEXT NOT NULL,
    cpf BIGINT UNIQUE)                
""")

cursor.execute("""
CREATE TABLE IF NOT EXISTS pedidos(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    cliente_id INTEGER,
    valor REAL,
    FOREIGN KEY (cliente_id) REFERENCES clientes(id))
""")

# cursor.execute("""INSERT INTO clientes(nome,cpf) VALUES
#     ('Eduardo', 45682154689),
#     ('Marta', 12547896542),
#     ('Marcos', 78951236470)
# """)

# cursor.execute("""INSERT INTO pedidos(cliente_id,valor) VALUES
#     (1, 200.50),
#     (1, 150.00),
#     (2, 300.00),
#     (4, 40.50)
# """)

# JOIN COM INNER - Registros que casam nas duas tabelas
# cursor.execute("""SELECT clientes.nome, pedidos.valor
#     FROM pedidos
#     INNER JOIN clientes ON pedidos.cliente_id = clientes.id
# """)

#JOIN COM LEFT - Todos os registros da tabela da esquerda mesmo que não
#tenham valor na direita
# cursor.execute("""SELECT clientes.nome, pedidos.valor
#     FROM pedidos
#     LEFT JOIN clientes ON pedidos.cliente_id = clientes.id
# """)

#JOIN COM RIGHT - Todos os registros da tabela da direita mesmo que não
#tenham valor na esquerda
cursor.execute("""SELECT clientes.nome, pedidos.valor
    FROM pedidos
    RIGHT JOIN clientes ON pedidos.cliente_id = clientes.id
""")

for i in cursor.fetchall():
    print(i)

conexao.commit()
conexao.close()
