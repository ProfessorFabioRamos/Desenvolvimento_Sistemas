from flask import Flask, request, jsonify

app = Flask(__name__)

produtos = [
    {"id":1, "nome":"Espada","preco":100.40},
    {"id":2, "nome":"Escudo","preco":200.50},
    {"id":3, "nome":"Armadura","preco":300.30}
]

@app.route("/")
def home():
     return "Bem-vindo à Loja!"

@app.route("/produto", methods = ["GET"])
def get_produtos():
    return jsonify(produtos)

@app.route("/produto/<int:id>", methods = ["GET"])
def get_produto(id):
    for p in produtos:
        if p["id"] == id:
            return jsonify(p)
    return jsonify({"erro": "produto não encontrado"}), 404

@app.route("/produto", methods = ["POST"])
def adicionar_produto():
    novo = request.get_json()
    produtos.append(novo)
    return jsonify(novo), 201

@app.route("/produto/<int:id>", methods = ["PUT"])
def atualizar_produto(id):
    dados_novo = request.get_json()
    for p in produtos:
        if p["id"] == id:
            p.update(dados_novo)
            return jsonify(p)
    return jsonify({"erro": "produto não encontrado"}), 404

@app.route("/produto/<int:id>", methods = ["DELETE"])
def deletar_produto(id):
    for p in produtos:
        if p["id"] == id:
            produtos.remove(p)
            return jsonify({"mensagem":"Produto Removido"}), 204
    return jsonify({"erro": "produto não encontrado"}), 404

if __name__ == "__main__":
    app.run(debug=True)


# from flask import Flask

# app = Flask(__name__)

# #Home page
# @app.route("/")
# def home():
#     return "Hello World!"

# if __name__ == "__main__":
#     app.run(debug=True)
