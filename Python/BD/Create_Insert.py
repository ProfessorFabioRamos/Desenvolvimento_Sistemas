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

#cursor.execute("INSERT INTO clientes (nome,idade,email) VALUES(?,?,?)",
               #("Alice",30,"alice@gmail.com"))
#cursor.execute("INSERT INTO clientes (nome,idade,email) VALUES('Alice',30,'alice@hotmail.com')")
#cursor.execute("INSERT INTO clientes VALUES(NULL,'Alice',30,'alice@yahoo.com')")       
cursor.execute("""INSERT INTO clientes (nome,idade,email) VALUES
               ('Alice', 30, 'alice@outlook.com'),
               ('Bob', 26, 'bob@outlook.com'),
               ('Eva', 19, 'eva@outlook.com')
""")
conexao.commit()
conexao.close()
