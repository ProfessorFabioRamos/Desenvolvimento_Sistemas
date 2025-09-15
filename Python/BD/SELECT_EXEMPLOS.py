import sqlite3

# Cria o banco de dados
conexao = sqlite3.connect("empresa.db")
# Objeto para enviar e receber dados do banco
cursor = conexao.cursor()

cursor.execute("""
CREATE TABLE IF NOT EXISTS clientes(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nome VARCHAR(100) NOT NULL,
    idade INTEGER,
    email TEXT UNIQUE) 
""")

def inserir_cliente(n,i,e): #nome, idade, email
    cursor.execute("INSERT INTO clientes (nome,idade,email) VALUES(?,?,?)",
        (n,i,e))
''' Executar apenas 1 vez para popular o banco
inserir_cliente("Kyle",16,"kyle@email.com")
inserir_cliente("Helena",16,"helena@email.com")
inserir_cliente("Manuel",45,"manuel@email.com")
inserir_cliente("Cartman",23,"cartman@email.com")
inserir_cliente("Alice",45,"alice@email.com")
inserir_cliente("Jenny",34,"jenny@email.com")
'''

cursor.execute("SELECT * FROM clientes") # SELECIONA TODOS OS REGISTROS
cursor.execute("SELECT nome, idade FROM clientes") # APENAS COLUNAS ESPECÍFICAS
cursor.execute("SELECT * FROM clientes ORDER BY idade ASC") # ORDENA RESULTADOS (CRESCENTE)
cursor.execute("SELECT * FROM clientes ORDER BY idade DESC") # ORDENA RESULTADOS (DECRESCENTE)
cursor.execute("SELECT * FROM clientes WHERE idade >30") # FILTRA RESULTADOS POR VALOR
cursor.execute("SELECT * FROM clientes WHERE email LIKE '%@gmail.com'") # FILTRA RESULTADOS POR VALOR FINAL DA STRING
cursor.execute("SELECT * FROM clientes WHERE idade >20 AND email LIKE '%@email.com'") # FILTRA RESULTADOS POR 2 FILTROS
cursor.execute("SELECT * FROM clientes LIMIT 3") #LIMITA OS RESULTADOS
cursor.execute("SELECT * FROM clientes ORDER BY idade ASC LIMIT 2") #LIMITA OS RESULTADOS

resultados = cursor.fetchall()
for r in resultados:
    print(r)

conexao.commit()
conexao.close()
